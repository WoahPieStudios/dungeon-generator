using UnityEngine;

namespace DungeonGenerator.Data
{
    [CreateAssetMenu(fileName = "Floor Data 1", menuName = "Dungeon Generator/New Floor Data")]
    public class FloorData : ScriptableObject
    {
        [Header("Properties")]
        public string seed;
        public Vector2 floorSize;
    }
}