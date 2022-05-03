namespace DungeonGenerator.Types
{
    [System.Flags]
    public enum DoorDirections
    {
        Up = 1 << 1,
        Down = 1 << 2,
        Left = 1 << 3,
        Right = 1 << 4
    }
}