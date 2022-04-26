using DungeonGenerator.Extensions;
using UnityEngine;

namespace DungeonGenerator.Data
{
    /// <summary>
    /// Contains the amount of rooms for the dungeon.
    /// </summary>
    [CreateAssetMenu(fileName = "Dungeon Data 01", menuName = "Dungeon Generator/New Dungeon Data")]
    public class DungeonData : ScriptableObject
    {
        [Header("Properties")]
        [SerializeField, Tooltip("Generator selects a random position to walk if it walks over an existing position.")]
        private bool randomJump;
        
        [Header("Room Details")]
        [SerializeField, ReadOnly, Tooltip("The number of starting rooms. This is where the player will spawn.")]
        private int startingRoom = 1;
        [SerializeField, Tooltip("The number of combat rooms. All enemies inside these rooms need to be killed in order to proceed.")]
        private int combatRooms;
        [SerializeField, Tooltip("The number of boss rooms. Only one room will be selected as the end of the dungeon.")]
        private int bossRooms;
        [SerializeField, Range(0.5f, 0.95f), Tooltip("The range which the boss room will spawn. The larger the number, the closer the boss room would be to the final room created")]
        private float bossDungeonRange;
        [SerializeField, Tooltip("The number of item rooms. In these rooms, players would only collect items without fighting enemies.")]
        private int itemRooms;

        /// <summary>
        /// Generator selects a random position to walk if it walks over an existing position.
        /// </summary>
        public bool RandomJump => randomJump;

        /// <summary>
        /// The number of starting rooms.
        /// </summary>
        /// <remarks>
        /// This is where the player will spawn.
        /// </remarks>
        public int StartingRoom => startingRoom;
        
        /// <summary>
        /// The number of combat rooms.
        /// </summary>
        /// <remarks>
        /// All enemies inside these rooms need to be killed in order to proceed.
        /// </remarks>
        public int CombatRooms => combatRooms;
        
        /// <summary>
        /// The number of boss rooms.
        /// </summary>
        /// <remarks>
        /// Only one room will be selected as the end of the dungeon.
        /// </remarks>
        public int BossRooms => bossRooms;
        
        /// <summary>
        /// The number of item rooms.
        /// </summary>
        /// <remarks>
        /// In these rooms, players would only collect items without fighting enemies.
        /// </remarks>
        public int ItemRooms => itemRooms;
        
        /// <summary>
        /// The number of all rooms in the dungeon.
        /// </summary>
        public int AllRooms => startingRoom + combatRooms + bossRooms + itemRooms;

        /// <summary>
        /// The range which the boss room will spawn.
        /// </summary>
        /// <remarks>
        /// The larger the number, the closer the boss room would be to the final room created
        /// </remarks>
        public float BossDungeonRange => bossDungeonRange;
    }
}