using System.Collections.Generic;
using DungeonGenerator.Data;
using DungeonGenerator.Handlers;
using DungeonGenerator.Tools;
using DungeonGenerator.Types;
using UnityEngine;

namespace DungeonGenerator.Managers
{
    public class FloorManager : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private FloorData floorData;
        [SerializeField] private string seed;
        [SerializeField] private Vector2 floorSize = Vector2.one;
        [SerializeField] private RoomHandler roomHandlerPrefab;
        
        [Header("Visualization")]
        [SerializeField] private Color gizmoColor = Color.white;

        [SerializeField] private List<RoomHandler> rooms = new List<RoomHandler>();

        public FloorData FloorData => floorData;
        public string Seed => seed;
        public Vector2 FloorSize => floorSize;

        public void GenerateFloor()
        {
            ClearFloor();

            if (string.IsNullOrEmpty(seed)) seed = floorData == null ? RandomStringGenerator.GenerateString() : floorData.seed;
            
            Random.InitState(seed.GetHashCode());

            if (roomHandlerPrefab == null)
            {
                Debug.LogWarning("No room prefab found, please provide a room to the floor manager.");
                return;
            }
            
            Vector2 currentPosition = Vector2.zero;

            for (int i = 0; i < floorSize.y; i++)
            {
                for (int j = 0; j < floorSize.x; j++)
                {
                    currentPosition.x = j;
                    currentPosition.y = i;

                    var room = Instantiate(roomHandlerPrefab, currentPosition * roomHandlerPrefab.Size, Quaternion.identity, transform);
                    
                    room.name = $"Room {currentPosition}";
                    
                    room.SetPosition(currentPosition);
                    
                    // Sets the directions to everything
                    DoorDirections directions = (DoorDirections) ~0;
                    
                    // Cull the unused directions depending on the position of the room
                    // The following culls the directions of the rooms found on the borders
                    if (currentPosition.x == 0) directions ^= DoorDirections.Left;
                    if (currentPosition.y == 0) directions ^= DoorDirections.Down;
                    if (currentPosition.x == floorSize.x - 1) directions ^= DoorDirections.Right;
                    if (currentPosition.y == floorSize.y - 1) directions ^= DoorDirections.Up;
                    
                    room.SetDoorDirections(directions);

                    print($"{room.name}: Door Directions: {directions}");

                    rooms.Add(room);
                }
            }

            Vector2 startingPosition = new Vector2(Random.Range(0, Mathf.FloorToInt(floorSize.x)), Random.Range(0, Mathf.FloorToInt(floorSize.y)));

            foreach (var room in rooms)
            {
                if (room.Position == startingPosition)
                {
                    room.SetRoomType(RoomType.Start);
                    room.SetColor(Color.green);
                }
            }
        }

        public void ClearFloor()
        {
            foreach (var room in rooms) DestroyImmediate(room.gameObject);
            rooms.Clear();
            seed = string.Empty;
        }

        private void OnDrawGizmosSelected()
        {
            if (roomHandlerPrefab == null)
            {
                Debug.LogWarning("No room prefab found, please provide a room to the floor manager.");
                return;
            }
            
            Gizmos.color = gizmoColor;
            
            Vector2 drawPosition = Vector2.zero;
            
            for (int i = 0; i < floorSize.y; i++)
            {
                for (int j = 0; j < floorSize.x; j++)
                {
                    drawPosition.x = j;
                    drawPosition.y = i;
                    Gizmos.DrawWireCube(drawPosition * roomHandlerPrefab.Size + (Vector2)transform.position, roomHandlerPrefab.Size);
                }
            }
        }
    }
}