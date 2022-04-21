using System;
using System.Collections;
using System.Collections.Generic;
using DungeonGenerator.Data;
using DungeonGenerator.Types;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonGenerator.Managers
{
    public class DungeonManager : MonoBehaviour
    {
        [Header("Debugging")] 
        [SerializeField] private bool dungeonStep;
        [SerializeField] private float dungeonStepDuration = 0.1f;

        [SerializeField] private DungeonData dungeonData;
        [SerializeField] private Vector2 startingPosition = Vector2.zero;
        [SerializeField] private RoomManager roomPrefab;
        [SerializeField] private List<Vector2> roomPositions = new List<Vector2>();
        [SerializeField] private List<RoomManager> roomManagers = new List<RoomManager>();
        [SerializeField] private List<RoomManager> combatRooms = new List<RoomManager>();
        [SerializeField] private List<RoomManager> bossRooms = new List<RoomManager>();
        [SerializeField] private List<RoomManager> itemRooms = new List<RoomManager>();

        private void Start()
        {
            StartCoroutine(GenerateDungeon());
        }

        private IEnumerator GenerateDungeon()
        {
            var currentPosition = startingPosition;
            while(roomManagers.Count != dungeonData.AllRooms)
            {
                if (roomPositions.Contains(currentPosition))
                {
                    currentPosition += GetRandomDirection((Orientation) Random.Range(0, 4), roomPrefab.size);
                }
                else
                {
                    var room = Instantiate(roomPrefab, transform);
                    room.SetPosition(currentPosition);
                    roomPositions.Add(currentPosition);
                    roomManagers.Add(room);
                }
                yield return new WaitForSeconds(dungeonStep ? dungeonStepDuration : 0f);
            }

            print("Dungeons generated,");
            StartCoroutine(SetRoomTypes());
        }

        private IEnumerator SetRoomTypes()
        {
            print("Setting room types.");
            foreach (var room in roomManagers)
            {
                if (room.position == startingPosition)
                {
                    room.SetRoomType(RoomType.Empty);
                    continue;
                }
                
                yield return new WaitForSeconds(dungeonStep ? dungeonStepDuration : 0f);
            }
        }

        private Vector2 GetRandomDirection(Orientation orientation, Vector3 size)
        {
            var direction = orientation switch
            {
                Orientation.Up => Vector3.up,
                Orientation.Down => Vector3.down,
                Orientation.Left => Vector3.left,
                Orientation.Right => Vector3.right,
                _ => Vector3.zero
            };
            return new Vector2(direction.x * size.x, direction.y * size.y);
        }
    }

    public enum Orientation
    {
        Up = 0,
        Down = 1, 
        Left = 2, 
        Right = 3
    }
}
