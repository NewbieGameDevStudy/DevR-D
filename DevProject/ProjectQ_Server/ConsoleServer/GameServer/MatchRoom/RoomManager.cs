using GameServer.Player;
using Server;
using System;
using System.Collections.Generic;
using Packet;

namespace GameServer.MatchRoom
{
    public class RoomManager
    {
        /* 클라이언트 섹션 바뀌면 여기도 바꿔줘야함.
        public enum eQuestionType
        {
            Society,        //사회
            Entertainment,  //연예
            General,        //일반
            Common,         //상식
            History,        //역사
            Science,        //과학
            Sports,         //스포츠
            Animal,         //동물
        }*/
        public enum RoomType {
            SOCIETY,
            ENTERTAINMENT,
            GENERAL,
            COMMON,
            HISTORY,
            SCIENCE,
            SPORTS,
            ANIMAL
        }

        public class QueueData {
            public RoomType type;
            public PlayerObject player;
        }

        Dictionary<RoomType, List<Room>> m_roomList = new Dictionary<RoomType, List<Room>>();
        Dictionary<ulong, PlayerObject> m_connectionPlayerList = new Dictionary<ulong, PlayerObject>();
        Queue<QueueData> m_waitingPlayerQueue = new Queue<QueueData>();


        const double WAIT_TIME_INTERVAL = 5;        // 대기 반감기
        const byte NEED_USER_COUNT = 100;           // 초기 필요 인원

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
            if (m_connectionPlayerList.ContainsKey(player.AccountID))
            {
                //중복 접속.
                var info = new PK_SC_CANNOT_MATCHING_GAME
                {
                    type = PK_SC_CANNOT_MATCHING_GAME.MatchingErrorType.PLAYER_OVERLAPPED,
                    accountID = player.AccountID
                };

                player.Client?.SendPacket(info);
                return;
            }

            m_connectionPlayerList.Add(player.AccountID, player);
            m_waitingPlayerQueue.Enqueue(new QueueData {
                player = player,
                type = type
            });

            testCount++;
            Console.WriteLine("WaitUserCount {0}", testCount);

            // 입장 플레이어 정보 웹서버로 요청.
            // 이시점에 할지 receive쪽에서 할지...?
        }

        public void FindMatchingRoom()
        {
            if (m_waitingPlayerQueue.Count == 0)
                return;

            QueueData playerQueue = m_waitingPlayerQueue.Dequeue();
            if (m_connectionPlayerList.ContainsKey(playerQueue.player.AccountID) == false)
            {
                // 이미 대기중 취소한 녀석.
                return;
            }

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

            TempRoomList[0]?.EnterRoom(playerQueue.player);
            Console.WriteLine("RoomEnter {0}", playerQueue.type);
        }

        public void MakeNewRoom(ref QueueData playerQueue)
        {
            Room TempRoom = new Room(WAIT_TIME_INTERVAL, NEED_USER_COUNT);
            TempRoom.EnterRoom(playerQueue.player);
            m_roomList[playerQueue.type].Add(TempRoom);
            TempRoom.RoomNo = (byte)m_roomList[playerQueue.type].Count;

            Console.WriteLine("Make New Room Player : {0} ", playerQueue.player.Client.AccountId);
        }

        public void CancelMatching(RoomType eRoomType, PlayerObject player)
        {
            if (m_connectionPlayerList.Remove(player.AccountID) == false)
            {
                // 이미 제거된 녀석.
                return;
            }

            if (player.EnteredRoomNo == 0)
            {
                // 대기실에서 제거됨.
                var info = new PK_SC_CANNOT_MATCHING_GAME
                {
                    type = PK_SC_CANNOT_MATCHING_GAME.MatchingErrorType.CANCEL_WAIT,
                    accountID = player.AccountID
                };

                player.Client?.SendPacket(info);
                return;
            }
            else
            {
                Room tempRoom = m_roomList[eRoomType][player.EnteredRoomNo];
                tempRoom?.RemoveUserFromRoom(player);
            }
        }

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

            BroadCastPosition(tempQueue);*/
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

            foreach (var obj in m_waitingPlayerQueue) {
                obj.player.Client.SendPacket(pks);
            }
        }
    }
}
