using System.Collections.Generic;
using DungeonGenerator.Data;
using DungeonGenerator.Handlers;
using DungeonGenerator.Tools;
using UnityEngine;

namespace DungeonGenerator.Managers
{
    public class FloorManager : MonoBehaviour
    {
        [SerializeField] private FloorData floorData;
        [SerializeField] private string seed;
        [SerializeField] private RoomHandler roomHandlerPrefab;
        [SerializeField] private List<RoomHandler> rooms = new List<RoomHandler>();

        public void GenerateFloor()
        {
            if (floorData == null)
            {
                Debug.LogWarning("No floor data found, please provide a floor data to the floor manager.");
                return;
            }
            
            seed = string.IsNullOrEmpty(floorData.Seed) ? RandomStringGenerator.GenerateString() : floorData.Seed;

            Random.InitState(seed.GetHashCode());

            if (roomHandlerPrefab == null)
            {
                Debug.LogWarning("No room prefab found, please provide a room to the floor manager.");
                return;
            }
            
            Vector2 currentPosition = Vector2.zero;

            for (int i = 0; i < floorData.FloorSize.y; i++)
            {
                for (int j = 0; j < floorData.FloorSize.x; j++)
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
            foreach (var room in rooms)
            {
                DestroyImmediate(room.gameObject);
            }
            rooms.Clear();
            ClearSeed();
        }
        
        public void SaveSeed()
        {
            if (string.IsNullOrEmpty(seed))
                Debug.LogWarning("No seed found, please generate a room first");
            else
            {
                floorData.SaveSeed(seed);
                Debug.Log($"Seed saved to {floorData.name}");
            }
        }

        public void ClearSeed()
        {
            seed = string.Empty;
            
            if (string.IsNullOrEmpty(seed)) return;
            floorData.SaveSeed(seed);
            Debug.Log($"Seed cleared from {floorData.name}");
        }
        
        private void OnDrawGizmos()
        {
            // Prevents the gizmos from drawing while there is no data yet.
            if (floorData == null)
            {
                Debug.LogWarning("No floor data found, please provide a floor data to the floor manager.");
                return;
            }
            
            if (roomHandlerPrefab == null)
            {
                Debug.LogWarning("No room prefab found, please provide a room to the floor manager.");
                return;
            }
            
            Gizmos.color = floorData.GizmoColor;
            Vector2 drawPosition = Vector2.zero;
            for (int i = 0; i < floorData.FloorSize.y; i++)
            {
                for (int j = 0; j < floorData.FloorSize.x; j++)
                {
                    drawPosition.x = j;
                    drawPosition.y = i;
                    Gizmos.DrawWireCube(drawPosition * roomHandlerPrefab.Size + (Vector2)transform.position, roomHandlerPrefab.Size);
                }
            }
        }
    }
}