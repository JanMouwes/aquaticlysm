using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movementSpeed;
    public float orbitSpeed = 90f;
    public GameObject innerCamera;

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

        UpdateCameraOrbit();
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

        float deltaOrbit = 0;

        if (Input.GetKey(KeyCode.E)) { deltaOrbit += orbitSpeed * Time.deltaTime; }

        if (Input.GetKey(KeyCode.Q)) { deltaOrbit -= orbitSpeed * Time.deltaTime; }

        this.gameObject.transform.RotateAround(mapTarget, Vector3.up, deltaOrbit);
    }
}