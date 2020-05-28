using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBoat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Go = 0.01f;
    }

    private float Go;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(0,0,Go * Time.deltaTime);
        Go += 0.2f*Time.deltaTime;
    }
}
