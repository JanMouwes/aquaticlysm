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


    private float _xOffset;
    private float _yOffset;
    private MeshFilter _meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the mesh
        _meshFilter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        _meshFilter.mesh.vertices = GenerateNoise();

        // Calculate the offset with physics (aka time hehe)
        _xOffset += Time.deltaTime * timeScale;

        // Also a possibility so I left this
        //yOffset += Time.deltaTime * timeScale;

        if (_yOffset <= 0.1) 
            _yOffset += Time.deltaTime * timeScale;
        if (_yOffset >= power)
            _yOffset -= Time.deltaTime * timeScale;
    }

    Vector3[] GenerateNoise() 
    {
        Vector3[] vertices = _meshFilter.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            // Calculate the perlin noise
            float perlinNoise = Mathf.PerlinNoise(vertices[i].x * scale + _xOffset, vertices[i].z * scale + _yOffset);
            
            // Set and amplificate the perlin noise
            vertices[i].y = perlinNoise * power;
        }

        // Return the vertices
        return vertices;
    }
}
