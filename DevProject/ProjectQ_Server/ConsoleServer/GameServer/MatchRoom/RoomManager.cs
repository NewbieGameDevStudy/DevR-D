using GameServer.Player;
using Server;
using System;
using System.Collections.Generic;
using Packet;

namespace GameServer.MatchRoom
{
    public class RoomManager
    {
        Dictionary<int, Room> m_roomList = new Dictionary<int, Room>();
        Queue<PlayerObject> m_waitingPlayerQueue = new Queue<PlayerObject>();
        List<Room> m_sortedRoomList = new List<Room>();

        const double WAIT_TIME_INTERVAL = 5;        // 대기 반감기
        const byte NEED_USER_COUNT = 100;           // 초기 필요 인원

        public RoomManager()
        {
            
        }
        
        public void EnterWaitRoom(PlayerObject player)
        {
            // 중복 검사 - 방에 참가 인원에 대해서도 필요함.
            if (m_waitingPlayerQueue.Contains(player))
            {
                PlayerManager.SendCannotMatchingPacket(player, PK_SC_CANNOT_MATCHING_GAME.MatchingErrorType.PLAYER_OVERLAPPED);
                return;
            }

            //웹서버 정보 요청.


            m_waitingPlayerQueue.Enqueue(player);
            Console.WriteLine("WaitUserCount {0}", m_waitingPlayerQueue.Count);

            var info = new PK_SC_CLIENT_SERVER_ID
            {
                AccountIDServer = player.AccountID
            };
            player.Client?.SendPacket(info);
        }

        public void FindMatchingRoom()
        {
            if (m_waitingPlayerQueue.Count == 0)
                return;

            PlayerObject player = m_waitingPlayerQueue.Dequeue();
            
            foreach (var tempRoom in m_roomList.Values)
            {
                if (tempRoom.CurrentRoomState == Room.RoomState.ROOM_WAITING)
                {
                    m_sortedRoomList.Add(tempRoom);
                }
            }

            if (m_sortedRoomList.Count == 0)
            {
                // 대기중인 방이 없을 때
                MakeNewRoom(player);
                return;
            }

            m_sortedRoomList.Sort(delegate (Room a, Room b)
            {
                return a.CurrentUserCount.CompareTo(b.CurrentUserCount);
            });

            m_sortedRoomList[0]?.EnterRoom(player);
            Console.WriteLine("RoomEnter {0} : UserID {0}", m_sortedRoomList[0].RoomNo, player.AccountIDClient);
            m_sortedRoomList.Clear();
        }

        public void MakeNewRoom(PlayerObject player)
        {
            Room tempRoom = new Room(WAIT_TIME_INTERVAL, NEED_USER_COUNT)
            {
                RoomNo = (byte)(m_roomList.Count + 1)
            };
            
            m_roomList.Add(tempRoom.RoomNo, tempRoom);
            tempRoom.EnterRoom(player);

            Console.WriteLine("Make New Room : {0} : UserID {0}", tempRoom.RoomNo, player.AccountIDClient);
        }

        public void CancelMatching(PlayerObject player)
        {
            // 대기중 취소
            foreach (var member in m_waitingPlayerQueue)
            {
                if (member.AccountIDClient == player.AccountIDClient)
                {
                    // Queue에서 어떻게 제거한다..?
                }
            }

            // 방에서 취소
            m_roomList[player.EnteredRoomNo]?.RemoveUserFromRoom(player);
            
           // 이미 처리됨
           // PlayerManager 에서 처리
        }

        public void ReadyForGame(byte roomNo, PlayerObject player)
        {
            m_roomList[roomNo]?.ReadyForGame(player);
        }

        public void MovePosition(byte roomNo, PlayerObject player)
        {
            m_roomList[roomNo]?.MovePosition(player);
        }

        public void RoomUpdate(double deltaTime)
        {
            foreach (var tempRoom in m_roomList.Values)
            {
                tempRoom.Update(deltaTime);
            }
        }

        public void Update(double deltaTime)
        {
            FindMatchingRoom();
            RoomUpdate(deltaTime);
        }       
    }
}

/* public void AddMoveInput(MoveData moveData)
        {
            lock (m_moveQueue) {
                m_moveQueue.Enqueue(moveData);
            }
        }
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

        void BroadCastRoomInObjectInfo()
            {
                //TODO : 현재는 단순히 들어온 큐에 대해서만 나중에 룸이 작업되면 해당 방안에 있는 사람들만 한다
                PK_SC_OBJECTS_INFO pks = new PK_SC_OBJECTS_INFO();
                pks.m_objectList = new List<PK_SC_OBJECT_INFO>();

                int index = 1;
                foreach (var obj in m_waitingPlayerQueue) {
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

                foreach (var obj in m_waitingPlayerQueue) {
                    obj.player.Client.SendPacket(pks);
                }
            }

    //TODO : 방에 입장 완료 후 게임시작이 되었을때 보내야한다.
            /*
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

public void BroadCastPosition(Queue<MoveData> tempQueue)
{
    var pks = new PK_SC_OBJECTS_POSITION();
    pks.m_objectList = new List<PK_SC_TARGET_POSITION>();
    foreach (var data in tempQueue)
    {
        var pksPos = new PK_SC_TARGET_POSITION();
        pksPos.handle = data.handle;
        pksPos.xPos = data.xPos;
        pksPos.yPos = data.yPos;
        pks.m_objectList.Add(pksPos);
    }

    foreach (var obj in m_waitingPlayerQueue)
    {
        obj.player.Client.SendPacket(pks);
    }
} */
