using UnityEngine;

public class ActionSelection : MonoBehaviour
{ 
    public GameObject ActionPanel;

    // Start is called before the first frame update
    void Start()
    {
        ActionPanel = GameObject.Find("ActionPanel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
