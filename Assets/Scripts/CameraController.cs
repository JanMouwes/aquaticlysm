using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float movementSpeed;
    void Start()
    {
        movementSpeed = 8.0f;
    }

    void Update()
    {
        transform.Translate(
            Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime , 
            0, 
            Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime
            );

    }

}
