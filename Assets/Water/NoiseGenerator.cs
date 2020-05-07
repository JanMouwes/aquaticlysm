using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    public float power = 0;
    public float scale = 0;
    public float timeScale = 1;

    private float xOffset;
    private float yOffset;
    private MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateNoise();
        xOffset += Time.deltaTime * timeScale;
        //yOffset += Time.deltaTime * timeScale;

        if (yOffset <= 0.1) 
            yOffset += Time.deltaTime * timeScale;
        if (yOffset >= power)
            yOffset -= Time.deltaTime * timeScale;

    }

    void GenerateNoise() 
    {
        Vector3[] vertices = meshFilter.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            float perlinNoise = Mathf.PerlinNoise(vertices[i].x * scale + xOffset, vertices[i].z * scale + yOffset);
            vertices[i].y = perlinNoise * power;
        }

        // Replace vertices
        meshFilter.mesh.vertices = vertices;
    }
}
