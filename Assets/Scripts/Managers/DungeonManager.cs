using System.Collections;
using System.Collections.Generic;
using DungeonGenerator.Data;
using DungeonGenerator.Objects;
using DungeonGenerator.Types;
using UnityEngine;

namespace DungeonGenerator.Managers
{
    public class DungeonManager : MonoBehaviour
    {
        [Header("Debugging")]
        [SerializeField] private bool dungeonStep;
        [SerializeField] private float dungeonStepDuration = 0.1f;

        [Header("Dungeon Settings")]
        [SerializeField] private DungeonData dungeonData;
        [SerializeField] private Vector2 startingPosition = Vector2.zero;
        [SerializeField] private RoomObject roomPrefab;

        [Header("Lists")]
        private readonly List<Vector2> _roomPositions = new List<Vector2>();
        private readonly List<RoomObject> _roomManagers = new List<RoomObject>();
        private readonly List<RoomObject> _combatRooms = new List<RoomObject>();
        private readonly List<RoomObject> _bossRooms = new List<RoomObject>();
        private readonly List<RoomObject> _itemRooms = new List<RoomObject>();

        private void Start()
        {
            StartCoroutine(GenerateDungeonPositions());
        }

        private IEnumerator GenerateDungeonPositions()
        {
            var currentPosition = startingPosition;
            while (_roomManagers.Count != dungeonData.AllRooms)
            {
                if (_roomPositions.Contains(currentPosition))
                {
                    if (dungeonData.RandomJump)
                        currentPosition = _roomPositions[Random.Range(0, _roomPositions.Count)];
                }
                else
                {
                    var room = Instantiate(roomPrefab, transform);
                    room.SetPosition(currentPosition);
                    _roomPositions.Add(currentPosition);
                    _roomManagers.Add(room);
                }
                currentPosition += GetRandomDirection((Orientation)Random.Range(0, 4), roomPrefab.size);
                yield return new WaitForSeconds(dungeonStep ? dungeonStepDuration : 0f);
            }

            print("Dungeons generated.");
            StartCoroutine(SetRoomTypes());
        }

        private IEnumerator SetRoomTypes()
        {
            print("Setting room types.");
            while (_combatRooms.Count + _bossRooms.Count + _itemRooms.Count != dungeonData.AllRooms - dungeonData.StartingRoom)
            {
                _roomManagers[0].SetRoomType(RoomType.Empty);
                if (_bossRooms.Count != dungeonData.BossRooms)
                {
                    var roomIndex = Random.Range((int)Mathf.Floor(_roomManagers.Count * dungeonData.BossDungeonRange), _roomManagers.Count);
                    var room = _roomManagers[roomIndex];
                    room.SetRoomType(RoomType.Boss);
                    _bossRooms.Add(room);
                    _roomManagers.Remove(room);
                }
                if (_itemRooms.Count != dungeonData.ItemRooms)
                {
                    var roomIndex = Random.Range(1, _roomManagers.Count);
                    var room = _roomManagers[roomIndex];
                    room.SetRoomType(RoomType.Item);
                    _itemRooms.Add(room);
                    _roomManagers.Remove(room);
                }
                if (_combatRooms.Count != dungeonData.CombatRooms)
                {
                    var roomIndex = Random.Range(1, _roomManagers.Count);
                    var room = _roomManagers[roomIndex];
                    room.SetRoomType(RoomType.Combat);
                    _combatRooms.Add(room);
                    _roomManagers.Remove(room);
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
}
