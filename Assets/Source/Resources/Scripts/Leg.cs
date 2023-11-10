using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : Resource
{
    public static event Action LegCollected;

    protected override void NoticeResource()
    {
        LegCollected?.Invoke();
    }
}
