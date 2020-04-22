using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Child camera object
    /// </summary>
    public GameObject childCamera;
    public float movementSpeed;
    public float orbitSpeed = 90f;
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
    /// Below this point the camera should rotate
    /// </summary>
    public float rotationHeight;
    public float minHeight;
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

        // Update the orbit of the camera
        UpdateCameraOrbit();
    }

    /// <summary>
    /// Determines y-level according to zoom-input
    /// </summary>
    private void Zoom()
    {
        float zoom = -Input.GetAxisRaw("Mouse ScrollWheel") * Time.deltaTime *100;
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
        Vector3 rotationVector = this.childCamera.transform.rotation.eulerAngles;
        rotationVector.x = newRotation;

        this.childCamera.transform.eulerAngles = rotationVector;
    }

    /// <summary>
    /// Updates the rotation of the camera when camera is below the rotationHeight
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
        // The current height of CameraController
        float currentHeight = this.transform.position.y;
        // the rotation of the camera based on the currentHeight
        float newRotation = GetRotationForY(currentHeight);

        SetRotation(newRotation);
    }

    /// <summary>
    /// calcutalates the target what the will camera orbits
    /// </summary>
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

    /// <summary>
    /// Updates The Orbit of the Camera
    /// </summary>
    private void UpdateCameraOrbit()
    {
        Vector3 cameraPosition = this.gameObject.transform.position;
        Vector3 cameraRotation = this.childCamera.transform.rotation.eulerAngles;

        Vector3 mapTarget = CalculateMapTarget(cameraPosition, cameraRotation);

        float deltaOrbit = Input.GetAxis("Orbit") * this.orbitSpeed * Time.deltaTime;

        this.gameObject.transform.RotateAround(mapTarget, Vector3.up, deltaOrbit);
    }
}