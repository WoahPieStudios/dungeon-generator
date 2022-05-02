using UnityEngine;

namespace DungeonGenerator.Data
{
    [CreateAssetMenu(fileName = "Floor Data 01", menuName = "Dungeon Generator/New Floor Data")]
    public class FloorData : ScriptableObject
    {
        [Header("Properties")]
        [SerializeField] private string seed;
        [SerializeField] private Vector2 floorSize = Vector2.one;
        [SerializeField] private Vector2 roomSize = Vector2.one;
        
        [Header("Debugging")]
        [SerializeField] private Color gizmoColor = Color.white;

        public string Seed => seed;
        public Vector2 FloorSize => floorSize;
        public Vector2 RoomSize => roomSize;
        public Color GizmoColor => gizmoColor;

        private void Reset()
        {
            floorSize = Vector2.one;
            roomSize = Vector2.one;
            seed = string.Empty;
            gizmoColor = Color.white;
        }

        public void SaveSeed(string seedValue)
        {
            seed = seedValue;
        }
    }
}