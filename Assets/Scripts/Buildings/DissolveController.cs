using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    private Renderer[] _renderers;

    public float progress;
    public string completionTag;

    // Start is called before the first frame update
    void Start()
    {
        progress = 1;
        _renderers = GetComponentsInChildren<Renderer>();
    }

    /// <summary>
    /// Adds up to the progress till it hits 1.
    /// </summary>
    /// <param name="addProgress">Percentage of the amount of work put into the building.</param>
    /// <returns>If the build is complete.</returns>
    public bool Build(float addProgress)
    {
        progress -= addProgress;
        progress = Mathf.Min(progress, 1);

        foreach (Renderer renderer in _renderers)
        {
            renderer.materials[0].SetFloat("transparant", Mathf.Clamp(progress, 0, 0.6f));
            renderer.materials[1].SetFloat("Dissolve", progress);
        }

        bool isDone = progress <= 0;
        if (progress == 0)
        {
            if (completionTag != "")
                this.tag = completionTag;
            return true;
        }

        if (isDone) { this.tag = completionTag; }

        return isDone;
    }

    public bool CheckifBuild()
    {
        return progress == 0;
    }
}