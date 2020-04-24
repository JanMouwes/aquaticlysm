using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectDemo : MonoBehaviour
{
    private readonly List<int> _test = new List<int>();

    #region prefabs

    public GameObject boat;
    public GameObject capsuleJesus;

    #endregion
    
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            _test.Add(SpawnController.Instance.Spawn(capsuleJesus, new Vector3(0, 0, 0)));

        if (Input.GetKeyDown(KeyCode.G))
        {
            foreach (int current in _test)
                SpawnController.Instance.DestroyGameObject(current);
            _test.Clear();
        }
    }
}