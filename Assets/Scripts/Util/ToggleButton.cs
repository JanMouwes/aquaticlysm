using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
