using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camera;

    public float movementSpeed;
    public float zoomSpeed;
    public float rotateSpeed;

    private void Start()
    {
        movementSpeed = 15.0f;
        zoomSpeed = 15.0f;
        rotateSpeed = 30.0f;
    }

    private void Update()
    {
        transform.Translate(
            Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime,
            0,
            Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime
        );
        float zoom = -Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(
            0,
            Mathf.Clamp(
                zoom * zoomSpeed * 100f * Time.deltaTime, 
                -30f, 30f
                ),
            0
        );
        camera.transform.Rotate(Vector3.right, zoom * rotateSpeed);

        transform.position = new Vector3(transform.position.x,
            Mathf.Clamp(
                transform.position.y,
                1.0f,
                15f
            ),
            transform.position.z
        );

        float minRotation = -45;
        float maxRotation = 45;
        Vector3 currentRotation = camera.transform.localRotation.eulerAngles;
        if (currentRotation.x > maxRotation + rotateSpeed)
        {
            currentRotation.x = 0f;
        }
        currentRotation.x = Mathf.Clamp(currentRotation.x, minRotation, maxRotation);
        camera.transform.localRotation = Quaternion.Euler(currentRotation);

    }
}