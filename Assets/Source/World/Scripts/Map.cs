using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Map", menuName = "Scriptable Objects/Maps")]
public class Map : ScriptableObject
{
    public int mapIndex;
    public string mapName;
    public Color nameColor;
    public Sprite mapImage;
    public Object sceneToLoad;
}
