using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    private Renderer[] materials;

    public float progress;

    // Start is called before the first frame update
    void Start()
    {
        progress = 1;
        materials = GetComponentsInChildren<Renderer>();
    }

    public bool Build(float addprogress)
    {
        progress -= addprogress;
        progress = Mathf.Clamp(progress, 0, 1);

        foreach (Renderer material in materials)
            material.material.SetFloat("Dissolve", progress);
        
        return progress == 0;
    }
}
