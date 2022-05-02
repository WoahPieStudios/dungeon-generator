using System;
using Old.Extensions;
using Old.Types;
using UnityEngine;

namespace Old.Objects
{
    public class RoomObject : MonoBehaviour
    {
        public RoomType roomType;
        [SerializeField] private SpriteRenderer spriteRenderer;
        public Vector2 size = Vector2.one;
        public Vector2 position;

        private void Awake()
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetRoomType(RoomType type)
        {
            roomType = type;
            switch (type)
            {
                case RoomType.Empty:
                    spriteRenderer.SetColor(Color.white);
                    break;
                case RoomType.Combat:
                    spriteRenderer.SetColor(Color.yellow);
                    break;
                case RoomType.Boss:
                    spriteRenderer.SetColor(Color.red);
                    break;
                case RoomType.Item:
                    spriteRenderer.SetColor(Color.green);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void SetPosition(Vector3 newPosition)
        {
            position = newPosition;
            transform.position = position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position,size);
        }
    }
}