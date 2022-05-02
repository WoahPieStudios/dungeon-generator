using UnityEngine;

namespace DungeonGenerator.Old.Extensions
{
    public static class SpriteRendererExtensions
    {
        public static void SetColor(this SpriteRenderer spriteRenderer, Color color)
        {
            spriteRenderer.color = color;
        }
    }
}