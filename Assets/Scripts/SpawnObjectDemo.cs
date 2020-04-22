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
    private List<int> _test = new List<int>();
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            _test.Add(SpawnController.Instance.Spawn(capsuleJesus, Vector3.zero));
        }
        if (Input.GetKey(KeyCode.B))
        {
            _test.Add(SpawnController.Instance.Spawn(boat, Vector3.zero));
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            foreach (var i in _test)
            {
                SpawnController.Instance.DestroyGameObject(i);
            }
            _test.Clear();
        }
    }
}
