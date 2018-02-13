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

        public void SetMoveInput(MoveData moveData)
        {
            lock (m_moveQueue) {
                m_moveQueue.Enqueue(moveData);
            }
        }
        
        public void EnterRoom(RoomType type, PlayerObject player)
        {
            m_enterPlayerQueue.Enqueue(new QueueData {
                player = player,
                type = type
            });

            testCount++;
        }

        void BroadCastObjectInfo()
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
            foreach (var roomTypes in m_roomList) {
                foreach (var room in roomTypes.Value) {
                    room.Update(deltaTime);
                }
            }

            if(testCount == 3) {
                testCount++;
                BroadCastObjectInfo();
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
