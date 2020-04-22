using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectDemo : MonoBehaviour
{
    //public GameObject SpawnController;
    private GameObject variableForPrefab;
    //public SpawnController script;
    // Start is called before the first frame update
    void Start()
    {
        variableForPrefab = (GameObject)Resources.Load("CapsuleJesus", typeof(GameObject));
        //script = SpawnController.GetComponent<SpawnController>();
    }

    // Update is called once per frame
    private List<string> test = new List<string>();
   
    private void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            test.Add(SpawnController.Instance.Spawn(variableForPrefab, new Vector3(0,0,0)));
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            for (int i = 0; i < test.Count; i++)
            {
                SpawnController.Instance.DestroyGameObject(test[i]);
            }
            test.Clear();
        }
    }
}
