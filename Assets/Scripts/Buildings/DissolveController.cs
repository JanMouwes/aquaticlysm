using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    private Renderer[] _materials;

    public float progress;

    // Start is called before the first frame update
    void Start()
    {
        progress = 1;
        _materials = GetComponentsInChildren<Renderer>();
    }

    public bool Build(float addProgress)
    {
        progress -= addProgress;
        progress = Mathf.Clamp(progress, 0, 1);

        foreach (Renderer material in _materials)
            material.material.SetFloat("Dissolve", progress);
        
        return progress == 0;
    }
}
