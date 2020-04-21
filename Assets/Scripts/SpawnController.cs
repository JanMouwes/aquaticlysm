using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public List<GameObject> entities;
    public GameObject PrefabGameObject;

    private void Awake()
    {
        entities = new List<GameObject>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(PrefabGameObject, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));

            entities.Add(PrefabGameObject);
        }

        if (entities.Count > 0)
        {
            entities[0].transform.Translate(0, 1, 0);
        }
    }
}
