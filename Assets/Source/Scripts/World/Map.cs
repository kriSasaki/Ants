using UnityEngine;

namespace Source.Scripts.World
{
    [CreateAssetMenu (fileName = "New Map", menuName = "Scriptable Objects/Maps")]
    public class Map : ScriptableObject
    {
        [SerializeField] private int _mapIndex;
        [SerializeField] private string _mapName;
        [SerializeField] private Color _nameColor;
        [SerializeField] private Sprite _mapImage;

        public int MapIndex => _mapIndex;
        public string MapName => _mapName;
        public Color NameColor => _nameColor;
        public Sprite MapImage => _mapImage;
    }
}
