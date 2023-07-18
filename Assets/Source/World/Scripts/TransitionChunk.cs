using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionChunk : Chunk
{
    public bool IsTransitional { get; private set; }

    private void Start()
    {
        IsTransitional = true;
    }
}
