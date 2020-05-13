using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface ISelectable
{
    bool Selected { get; set; }
    void DoAction(string tag, Vector3 position, bool priority);
}

