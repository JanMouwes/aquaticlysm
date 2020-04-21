using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Inner camera object
    /// </summary>
    public new GameObject camera;

    /// <summary>
    /// Camera movement speed per second
    /// </summary>
    public float movementSpeed;

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
        Vector3 rotationVector = this.camera.transform.rotation.eulerAngles;
        rotationVector.x = newRotation;

        camera.transform.eulerAngles = rotationVector;
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
}