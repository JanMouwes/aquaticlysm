using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    [Tooltip("The camera the minimap follows.")]
    public CameraController camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Set the position of the minimap camera
        this.transform.position = new Vector3(camera.mapTarget.x, 20f, camera.mapTarget.z);
    }
}
