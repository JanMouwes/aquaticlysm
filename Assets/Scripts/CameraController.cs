using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camera;

    public float movementSpeed;
    public float rotateSpeed;

    public float minRotation = -45;
    public float maxRotation = 45;
    public float rotationHeight;

    public float minHeight;
    public float maxHeight;

    private float _zoom;
    private float stepheight;
    private void Start() { }

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
        stepheight = _zoom * rotateSpeed * 100f * Time.deltaTime;
        transform.Translate(
            0, stepheight, 0
        );
        Vector3 oldPosition = transform.position;
        float newY = Mathf.Clamp(oldPosition.y, minHeight, maxHeight);

        transform.position = new Vector3(oldPosition.x, newY, oldPosition.z);
    }

    /// <summary>
    ///
    /// </summary>
    private void ZoomRotate()
    {
        if (_zoom != 0)
        {
            var slope = (maxRotation - minRotation) / (rotationHeight - minHeight);
            camera.transform.Rotate(Vector3.right, _zoom * (maxRotation - minRotation) / (rotationHeight - minHeight) * stepheight);
            Vector3 currentRotation = camera.transform.localRotation.eulerAngles;
            if (currentRotation.x > maxRotation + rotateSpeed)
            {
                currentRotation.x = 0f;
            }
            currentRotation.x              = Mathf.Clamp(currentRotation.x, minRotation, maxRotation);
            camera.transform.localRotation = Quaternion.Euler(currentRotation);
            Debug.Log(_zoom * rotateSpeed * 100f * Time.deltaTime);
            Debug.Log(_zoom);
            Debug.Log(_zoom * (maxRotation - minRotation) / ((rotationHeight - minHeight)) * _zoom * rotateSpeed * 100f * Time.deltaTime);
        }
    }
}