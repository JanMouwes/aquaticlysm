using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    private Renderer[] _materials;

    public float progress;
    public string completionTag;

    // Start is called before the first frame update
    void Start()
    {
        progress = 1;
        _materials = GetComponentsInChildren<Renderer>();
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

        foreach (Renderer material in _materials)
            material.material.SetFloat("Dissolve", progress);

        if (progress == 0)
        {
            this.tag = completionTag;
            return true;
        }

        return false;
    }
}
