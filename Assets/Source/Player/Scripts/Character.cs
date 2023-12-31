using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public string CharacterName;
    public int CharacterHealth;
    public int CharacterPrice;
    public GameObject CharacterModel;
    public bool IsBuyed = false;
    public int CharacterRank;
}
