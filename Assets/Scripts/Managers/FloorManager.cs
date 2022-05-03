using System.Collections.Generic;
using DungeonGenerator.Data;
using DungeonGenerator.Handlers;
using DungeonGenerator.Tools;
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
                    
                    rooms.Add(room);
                }
            }
        }

        public void ClearFloor()
        {
            foreach (var room in rooms) DestroyImmediate(room.gameObject);
            rooms.Clear();
            seed = string.Empty;
        }

        private void OnDrawGizmos()
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