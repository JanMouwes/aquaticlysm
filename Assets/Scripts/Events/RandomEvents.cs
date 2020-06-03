using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.CreateEvent(2);
    }

    IEnumerator Test()
    {
        //Wait for 4 seconds
        yield return new WaitForSeconds(4);
       
        Start();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
