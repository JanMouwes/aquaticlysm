﻿using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Inner camera object
    /// </summary>
    public new GameObject innerCamera;

    /// <summary>
    /// Camera movement speed per second
    /// </summary>
    public float movementSpeed;

    /// <summary>
    /// Orbit speed in degrees per second
    /// </summary>
    public float orbitSpeed = 90f;

    /// <summary>
    /// Camera zoom speed (y-change)
    /// </summary>
    public float zoomSpeed = 10;

    /// <summary>
    /// Rotation when the camera's y-level is at minHeight
    /// </summary>
    public float minRotation = 15;

    /// <summary>
    /// Rotation when the camera's y-level is at rotationHeight or above
    /// </summary>
    public float maxRotation = 80;

    /// <summary>
    /// Height below which the camera should rotate
    /// </summary>
    public float rotationHeight;

    /// <summary>
    /// Minimum camera height
    /// </summary>
    public float minHeight;

    /// <summary>
    /// Maximum camera height
    /// </summary>
    public float maxHeight;

    private void Update()
    {
        // X, Z axis movement of CameraController
        transform.Translate(
            Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime,
            0,
            Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime
        );


        // Zoom camera based on scroll wheel input
        Zoom();

        // Determine rotation according to zoom
        ZoomRotate();

        UpdateCameraOrbit();
    }

    /// <summary>
    /// Determines y-level according to zoom-input
    /// </summary>
    private void Zoom()
    {
        float zoom = -Input.GetAxis("Mouse ScrollWheel");
        float heightChange = zoom * this.zoomSpeed;
        transform.Translate(0, heightChange, 0);
        Vector3 oldPosition = transform.position;
        float newY = Mathf.Clamp(oldPosition.y, minHeight, maxHeight);

        transform.position = new Vector3(oldPosition.x, newY, oldPosition.z);
    }

    /// <summary>
    /// Sets x-axis rotation for the camera 
    /// </summary>
    /// <param name="newRotation">New rotation in degrees</param>
    private void SetRotation(float newRotation)
    {
        Vector3 rotationVector = this.innerCamera.transform.rotation.eulerAngles;
        rotationVector.x = newRotation;

        this.innerCamera.transform.eulerAngles = rotationVector;
    }

    /// <summary>
    /// Updates zoom-rotation
    /// </summary>
    private void ZoomRotate()
    {
        // Maps y levels to camera-rotation
        float GetRotationForY(float y)
        {
            if (y < this.minHeight) { return this.minRotation; }

            if (y > this.rotationHeight) { return this.maxRotation; }

            float rotationRange = this.maxRotation - this.minRotation;

            float rotationHeightRange = this.rotationHeight - this.minHeight;

            float slope = rotationRange / rotationHeightRange;

            return this.minRotation + (slope * (y - this.minHeight));
        }

        float currentHeight = this.transform.position.y;
        float newRotation = GetRotationForY(currentHeight);

        SetRotation(newRotation);
    }

    private static Vector3 CalculateMapTarget(Vector3 cameraPosition, Vector3 cameraRotation)
    {
        // Calculate x-offset from the ground in radians
        float thetaX = (90f - cameraRotation.x) * Mathf.Deg2Rad;
        float thetaY = (cameraRotation.y)       * Mathf.Deg2Rad;

        double localTargetDist = cameraPosition.y * Math.Tan(thetaX);

        double localTargetZ = localTargetDist * Math.Cos(thetaY);
        double localTargetX = localTargetDist * Math.Sin(thetaY);

        return new Vector3
        {
            x = cameraPosition.x + (float) localTargetX,
            y = 0,
            z = cameraPosition.z + (float) localTargetZ
        };
    }

    private void UpdateCameraOrbit()
    {
        Vector3 cameraPosition = this.gameObject.transform.position;
        Vector3 cameraRotation = this.innerCamera.transform.rotation.eulerAngles;

        Vector3 mapTarget = CalculateMapTarget(cameraPosition, cameraRotation);

        float deltaOrbit = Input.GetAxis("Orbit") * this.orbitSpeed * Time.deltaTime;

        this.gameObject.transform.RotateAround(mapTarget, Vector3.up, deltaOrbit);
    }
}