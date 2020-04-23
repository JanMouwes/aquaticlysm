using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SpawnObjectDemo : MonoBehaviour
{
    /**
     * OBJECT LISTS
     */
    private readonly List<int> _test = new List<int>();

    public GameObject boat;

    /**
     * PREFABS
     */
    public GameObject capsuleJesus;

    // Update is called once per frame
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    _test.Add(SpawnController.Instance.Spawn(capsuleJesus, new Vector3(8.3f, 0, 0),
        //                                             Quaternion.Euler(0, 90, 0)));
        //    SpawnController.Instance.GetGameObject(_test[0]).transform.parent = transform;

        //}

        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    foreach (int current in _test)
        //        SpawnController.Instance.DestroyGameObject(current);
        //    _test.Clear();
        //}
    }

}