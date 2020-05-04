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
        if (Input.GetKeyDown(KeyCode.V)) { this._test.Add(SpawnController.Instance.Spawn(this.capsuleJesus, new Vector3(0, 0, 0)).id); }

        if (Input.GetKeyDown(KeyCode.G))
        {
            foreach (int current in this._test) { SpawnController.Instance.DestroyEntity(current); }

            this._test.Clear();
        }
    }
}