using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camera;

    public float movementSpeed;
    public float rotateSpeed;

    public float minRotation = 15;
    public float maxRotation = 80;
    public float rotationHeight;

    public float minHeight;
    public float maxHeight;

    private float _zoom;

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

        // Only rotate camera when within rotationheight range
        if (transform.position.y <= rotationHeight) { ZoomRotate(); }
    }

    /// <summary>
    ///
    /// </summary>
    private void Zoom()
    {
        _zoom = -Input.GetAxis("Mouse ScrollWheel");
        float heightChange = this._zoom * this.rotateSpeed;

        transform.Translate(
            0, heightChange, 0
        );
        Vector3 oldPosition = transform.position;
        float newY = Mathf.Clamp(oldPosition.y, minHeight, maxHeight);

        transform.position = new Vector3(oldPosition.x, newY, oldPosition.z);
    }

    private void SetRotation(float rotation)
    {
        Vector3 newRotation = this.camera.transform.rotation.eulerAngles;
        newRotation.x = rotation;

        camera.transform.eulerAngles = newRotation;
    }

    /// <summary>
    ///
    /// </summary>
    private void ZoomRotate()
    {
        float GetRotationForY(float y)
        {
            if (y < this.minHeight) { return this.minRotation; }

            if (y > this.maxHeight) { return this.maxRotation; }

            float rotationRange = this.maxRotation - this.minRotation;

            float rotationHeightRange = this.rotationHeight - this.minHeight;

            float slope = rotationRange / rotationHeightRange;

            return this.minRotation + (slope * (y - this.minHeight));
        }

        float currentHeight = this.transform.position.y;

        if (currentHeight <= this.maxHeight && currentHeight >= this.minHeight)
        {
            float zoomRotation = GetRotationForY(currentHeight);
            SetRotation(zoomRotation);
        }
    }
}