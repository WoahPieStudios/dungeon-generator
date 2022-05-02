using DungeonGenerator.Types;
using UnityEngine;

namespace DungeonGenerator.Handlers
{
    public class RoomHandler : MonoBehaviour
    {
        [SerializeField] private Vector2 size;
        [SerializeField] private RoomTypes roomType;
        [SerializeField] private DoorDirections doorDirections;

        public Vector2 Size => size;

        private void Reset()
        {
            size = Vector2.one;
            roomType = 0;
            doorDirections = 0;
        }

        private void OnValidate()
        {
            transform.localScale = size;
        }
        
        public void SetRoomType(RoomTypes type)
        {
            roomType = type;
        }
        
        public void SetDoorDirections(DoorDirections directions)
        {
            doorDirections = directions;
        }
    }
}