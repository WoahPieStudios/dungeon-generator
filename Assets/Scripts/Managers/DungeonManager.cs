using UnityEngine;

namespace DungeonGenerator.Managers
{
    public class DungeonManager : MonoBehaviour
    {
        [SerializeField] Vector2 dungeonSize = Vector2.one;
        [SerializeField] Vector2 roomSize = Vector2.one;
        [SerializeField] Color gizmoColor = Color.white;

        private void Reset()
        {
            dungeonSize = Vector2.one;
            roomSize = Vector2.one;
            gizmoColor = Color.white;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Vector2 drawPosition = Vector2.zero;
            for (int i = 0; i < dungeonSize.y; i++)
            {
                for (int j = 0; j < dungeonSize.x; j++)
                {
                    drawPosition.x = j;
                    drawPosition.y = i;
                    Gizmos.DrawWireCube(drawPosition * roomSize + (Vector2)transform.position, roomSize);
                }
            }
        }
    }
}