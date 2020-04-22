using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectDemo : MonoBehaviour
{
    /**
     * PREFABS
     */
    public GameObject capsuleJesus;
    public GameObject boat;
    
    /**
     * OBJECT LISTS
     */
    List<int> test;
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            test.Add(SpawnController.Instance.Spawn(capsuleJesus, new Vector3(0,0,0)));
        }
        if (Input.GetKey(KeyCode.B))
        {
            test.Add(SpawnController.Instance.Spawn(boat, new Vector3(0, 0, 0)));
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
