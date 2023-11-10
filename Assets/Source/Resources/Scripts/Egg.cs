using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Resource
{
    public static event Action EggCollected;

    protected override void NoticeResource()
    {
        EggCollected?.Invoke();
    }
}
