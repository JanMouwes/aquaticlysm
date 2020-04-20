using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movementSpeed;

    private void Start()
    {
        movementSpeed = 8.0f;
    }

    private void Update()
    {
        transform.Translate(
            Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime,
            0,
            Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime
        );
    }
}