using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private bool completed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (completed)
            return;
        if (!GetComponent<DissolveController>().CheckifBuild())
            return;
        GameObject test = new GameObject("RestPlace");
        test.transform.parent = transform;
        test.transform.localPosition = new Vector3(-4,0,0);
        test.tag = "Rest";
        completed = true;
    }
}
