using DungeonGenerator.Data;
using DungeonGenerator.Tools;
using UnityEngine;

namespace DungeonGenerator.Managers
{
    public class FloorManager : MonoBehaviour
    {
        [SerializeField] private FloorData floorData;
        [SerializeField] private string seed;
        
        public void GenerateFloor()
        {
            seed = string.IsNullOrEmpty(floorData.Seed) ? RandomStringGenerator.GenerateString() : floorData.Seed;

            Random.InitState(seed.GetHashCode());

            int[] numbers = new int[10];
            string numberText = string.Empty;
            
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = Random.Range(0, 100);
                numberText += $"{numbers[i]} ";
            }
            
            Debug.Log(numberText);
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
            floorData.SaveSeed(seed);
            Debug.Log($"Seed cleared from {floorData.name}");
        }
        
        private void OnDrawGizmos()
        {
            // Prevents the gizmos from drawing while there is no data yet.
            if (floorData == null) return;
            
            Gizmos.color = floorData.GizmoColor;
            Vector2 drawPosition = Vector2.zero;
            for (int i = 0; i < floorData.FloorSize.y; i++)
            {
                for (int j = 0; j < floorData.FloorSize.x; j++)
                {
                    drawPosition.x = j;
                    drawPosition.y = i;
                    Gizmos.DrawWireCube(drawPosition * floorData.RoomSize + (Vector2)transform.position, floorData.RoomSize);
                }
            }
        }
    }
}