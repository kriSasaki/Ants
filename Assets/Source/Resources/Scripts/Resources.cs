using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    private Mushroom _mushroom;

    private ResourcesFeature _resourcesFeature;

    private void Start()
    {
        var resourceMushroom = new Resource(ResourceType.Mushroom, 0);
        var resourceLeg = new Resource(ResourceType.Leg, 0);
        var resourceEgg = new Resource(ResourceType.Egg, 0);


    }
}
