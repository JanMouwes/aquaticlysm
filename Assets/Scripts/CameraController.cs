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
    private void Start()
    {
        
    }

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
        if (transform.position.y <= rotationHeight)
        {
            ZoomRotate();
        }

    }

    /// <summary>
    ///
    /// </summary>
    private void Zoom()
    {
        _zoom = -Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(
            0, _zoom * rotateSpeed * 100f * Time.deltaTime, 0
        );

        transform.position = new Vector3(transform.position.x,
            Mathf.Clamp(
                transform.position.y,
                minHeight,
                maxHeight
            ),
            transform.position.z
        );

    }

    /// <summary>
    ///
    /// </summary>
    private void ZoomRotate()
    {
        camera.transform.Rotate(Vector3.right, _zoom * rotateSpeed);
       
        Vector3 currentRotation = camera.transform.localRotation.eulerAngles;
        if (currentRotation.x > maxRotation + (maxRotation - minRotation) / (rotationHeight - minHeight))
        {
            currentRotation.x = 0f;
        }
        currentRotation.x = Mathf.Clamp(currentRotation.x, minRotation, maxRotation);
        camera.transform.localRotation = Quaternion.Euler(currentRotation);

        Debug.Log(currentRotation.x);
    }
}