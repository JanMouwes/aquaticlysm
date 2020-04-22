using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectDemo : MonoBehaviour
{
    public GameObject a;
    private GameObject variableForPrefab;
    public SpawnController script;
    // Start is called before the first frame update
    void Start()
    {
        variableForPrefab = (GameObject)Resources.Load("CapsuleJesus", typeof(GameObject));
        script = a.GetComponent<SpawnController>();
    }

    // Update is called once per frame
    private List<int> test = new List<int>();

    public GameObject PrefabGameObject;
  
    private int i =0;
    private void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            test.Add(script.Spawn(variableForPrefab, new Vector3(0,0,0)));
        }
        if (Input.GetKey(KeyCode.G))
        {
            for (int i = 0; i < test.Count; i++)
            {
                script.DestroyGameObject(test[i]);
            }
            test.Clear();
        }
    }
}
