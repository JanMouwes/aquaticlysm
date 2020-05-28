using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    public Material[] Materials;

    public float progress;

    // Start is called before the first frame update
    void Start()
    {
        progress = 1;
    }

    /// <summary>
    /// Adds up to the progress till it hits 1.
    /// </summary>
    /// <param name="addProgress">Percentage of the amount of work put into the building.</param>
    /// <returns>If the build is complete.</returns>
    public bool Build(float addProgress)
    {
        progress -= addProgress;
        progress = Mathf.Clamp(progress, 0, 1);

        foreach (Material material in Materials)
            material.SetFloat("Dissolve", progress);
        
        return progress == 0;
    }
}
