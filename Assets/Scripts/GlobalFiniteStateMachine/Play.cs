﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : IState
{
    public GlobalState StateType() => GlobalState.Play;

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Start()
    {
        throw new System.NotImplementedException();
    }

    public void Stop()
    {
        throw new System.NotImplementedException();
    }
}
