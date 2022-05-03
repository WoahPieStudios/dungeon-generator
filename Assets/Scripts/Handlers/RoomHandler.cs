using System;
using DungeonGenerator.Types;
using UnityEngine;

namespace DungeonGenerator.Handlers
{
    public class RoomHandler : MonoBehaviour
    {
        [SerializeField] private Vector2 size;
        [SerializeField] private Vector2 position;
        [SerializeField] private RoomType roomType;
        [SerializeField] private DoorDirections doorDirections;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public Vector2 Size => size;
        public Vector2 Position => position;
        public DoorDirections DoorDirections => doorDirections;

        private void Reset()
        {
            size = Vector2.one;
            roomType = 0;
            doorDirections = 0;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnValidate()
        {
            transform.localScale = size;
        }

        public void SetPosition(Vector2 currentPosition)
        {
            position = currentPosition;
        }
        
        public void SetRoomType(RoomType type)
        {
            roomType = type;
        }

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }
        
        public void SetDoorDirections(DoorDirections directions)
        {
            doorDirections = directions;

            float colorValue = 0f;
            
            if(doorDirections.HasFlag(DoorDirections.Up)) colorValue += 0.25f;
            if(doorDirections.HasFlag(DoorDirections.Down)) colorValue += 0.25f;
            if(doorDirections.HasFlag(DoorDirections.Left)) colorValue += 0.25f;
            if(doorDirections.HasFlag(DoorDirections.Right)) colorValue += 0.25f;

            spriteRenderer.color = Color.Lerp(Color.black, Color.white, colorValue);
        }
    }
}