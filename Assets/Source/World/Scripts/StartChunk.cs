using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChunk : Chunk
{
    public bool IsStartChunk { get; private set; }

    private void Start()
    {
        IsStartChunk = true;
    }
}
