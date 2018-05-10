using GameServer.Player;
using Server;
using System;
using System.Collections.Generic;
using Packet;

namespace GameServer.MatchRoom
{
    public class RoomManager
    {
        public enum RoomType {
            COMMON_SENSE,
            SOCIAL,
            GAME,
        }

        public class QueueData {
            public RoomType type;
            public PlayerObject player;
        }

        Dictionary<RoomType, List<Room>> m_roomList = new Dictionary<RoomType, List<Room>>();
        Queue<QueueData> m_enterPlayerQueue = new Queue<QueueData>();

        // 인원 감소 주기, 초기 필요 인원.
        const double WaitTimeInterval = 5;
        const byte NeedUserCount = 100;

        /// <summary>
        /// 아래 필드들은 모두 테스트용
        /// </summary>
        int testCount;
        public class MoveData
        {
            public int handle;
            public float xPos;
            public float yPos;
        }

        Queue<MoveData> m_moveQueue = new Queue<MoveData>();

        public RoomManager()
        {
            // 방 리스트는 어차피 타입의 개수만큼 있다. - 미리 만들어 둔다...
            foreach (RoomType eType in Enum.GetValues(typeof(RoomType)))
            {
                List<Room> TempRoomList = new List<Room>();
                m_roomList.Add(eType, TempRoomList);
            }
        }


        public void AddMoveInput(MoveData moveData)
        {
            lock (m_moveQueue) {
                m_moveQueue.Enqueue(moveData);
            }
        }
        
        public void EnterWaitRoom(RoomType type, PlayerObject player)
        {
            m_enterPlayerQueue.Enqueue(new QueueData {
                player = player,
                type = type
            });

            testCount++;
        }

        public void FindMatchingRoom()
        {
            if (m_enterPlayerQueue.Count == 0)
                return;

            QueueData playerQueue = m_enterPlayerQueue.Dequeue();
            List<Room> TempRoomList = m_roomList[playerQueue.type].FindAll(room => room.CurrentRoomState.Equals(Room.RoomState.ROOM_WAITING));

            if (TempRoomList.Count == 0)
            {
                // 대기중인 방이 없을 때
                MakeNewRoom(ref playerQueue);
                return;
            }

            TempRoomList.Sort(delegate (Room a, Room b)
            {
                return a.CurrentUserCount.CompareTo(b.CurrentUserCount);
            });

            TempRoomList[0].EnterRoom(playerQueue.player);
            Console.WriteLine("RoomEnter {0}", playerQueue.type);
        }

        public void MakeNewRoom(ref QueueData playerQueue)
        {
            Room TempRoom = new Room(WaitTimeInterval, NeedUserCount);
            TempRoom.EnterRoom(playerQueue.player);
            m_roomList[playerQueue.type].Add(TempRoom);
        }

        void BroadCastRoomInObjectInfo()
        {
            //TODO : 현재는 단순히 들어온 큐에 대해서만 나중에 룸이 작업되면 해당 방안에 있는 사람들만 한다
            PK_SC_OBJECTS_INFO pks = new PK_SC_OBJECTS_INFO();
            pks.m_objectList = new List<PK_SC_OBJECT_INFO>();

            int index = 1;
            foreach (var obj in m_enterPlayerQueue) {
                var info = new PK_SC_OBJECT_INFO();
                info.handle = obj.player.Handle;
                info.info = new PK_SC_PLAYERINFO_LOAD {
                    Exp = obj.player.PlayerData.Exp,
                    Level = obj.player.PlayerData.Level,
                    handle = obj.player.Handle,
                };

                info.position = new PK_SC_TARGET_POSITION {
                    handle = obj.player.Handle,
                    xPos = 30 * index,
                    yPos = 30 * index,
                };

                pks.m_objectList.Add(info);
                index++;
            }

            foreach (var obj in m_enterPlayerQueue) {
                obj.player.Client.SendPacket(pks);
            }
        }

        public void Update(double deltaTime)
        {
            FindMatchingRoom();

            // 
            foreach (var roomTypes in m_roomList) {
                foreach (var room in roomTypes.Value) {
                    room.Update(deltaTime);
                }
            }

            //TODO : 방에 입장 완료 후 게임시작이 되었을때 보내야한다.
            if(testCount == 2) {
                testCount++;
                BroadCastRoomInObjectInfo();
            }

            Queue<MoveData> tempQueue = null;
            lock (m_moveQueue) {
                if (m_moveQueue.Count > 0)
                    tempQueue = new Queue<MoveData>(m_moveQueue);
                m_moveQueue.Clear();
            }

            if (tempQueue == null)
                return;

            BroadCastPosition(tempQueue);
        }

        public void BroadCastPosition(Queue<MoveData> tempQueue)
        {
            var pks = new PK_SC_OBJECTS_POSITION();
            pks.m_objectList = new List<PK_SC_TARGET_POSITION>();
            foreach (var data in tempQueue) {
                var pksPos = new PK_SC_TARGET_POSITION();
                pksPos.handle = data.handle;
                pksPos.xPos = data.xPos;
                pksPos.yPos = data.yPos;
                pks.m_objectList.Add(pksPos);
            }

            foreach (var obj in m_enterPlayerQueue) {
                obj.player.Client.SendPacket(pks);
            }
        }
    }
}
