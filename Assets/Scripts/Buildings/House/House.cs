using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private bool completed;

    // Update is called once per frame
    void Update()
    {
        if (completed)
            return;
        if (!GetComponent<DissolveController>().CheckifBuild())
            return;
        GameObject rest = new GameObject("RestPlace");
        rest.transform.parent = transform;
        rest.transform.localPosition = new Vector3(-4,0,0);
        rest.tag = "Rest";
        completed = true;
    }
}
