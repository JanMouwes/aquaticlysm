using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    [Tooltip("The amplification of the PerlinNoise.")]
    public float power = 1.5f;

    [Tooltip("The size of the PerlinNoise from 0 to 1.")]
    public float scale = 1;

    [Tooltip("How fast the noise changes.")]
    public float timeScale = 0.5f;


    private float xOffset;
    private float yOffset;
    private MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the mesh
        meshFilter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateNoise();

        // Calculate the offset with physics (aka time hehe)
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
            // Calculate the perlin noise
            float perlinNoise = Mathf.PerlinNoise(vertices[i].x * scale + xOffset, vertices[i].z * scale + yOffset);
            
            // Set and amplificate the perlin noise
            vertices[i].y = perlinNoise * power;
        }

        // Replace vertices
        meshFilter.mesh.vertices = vertices;
    }
}
