using GameServer.Player;
using System.Collections.Generic;
using Packet;

namespace GameServer.MatchRoom
{
    public class Room
    {
        // 매칭까지 인원수
        // 매칭까지 걸리는 시간. 
        // 시간이 지날때까지 매칭이 되지 않으면 인원수를 절반으로 감소 시킨다. -> 2 까지
        // 매칭 제한시간...?

        const byte GAME_MAX_WAIT_TIME = 10;
        public enum RoomState
        {
            ROOM_WAITING,
            ROOM_PLAYING,
            ROOM_GAME_ENDED
        }

        public byte GameNeedUserCount { get; private set; }
        public RoomState CurrentRoomState { get; private set; }
        public int CurrentUserCount
        {
            get
            {
                return m_roomPlayerList.Count;
            }
        }

        double GameWaitTime;
        double WaitElapsedTime;
        double CheckedTime;

        byte WaitMaxLimitCount;

        // test
        const double GAME_PLAY_TIME_TEST = 10;
        double GameBeginTime;
        bool isGameStarted;

        List<PlayerObject> m_roomPlayerList = new List<PlayerObject>();

        public Room(double waitTime, byte gameNeedUserCount)
        {
            this.GameWaitTime = waitTime;
            this.GameNeedUserCount = gameNeedUserCount;

            WaitElapsedTime = 0;
            CheckedTime = 0;
            CurrentRoomState = Room.RoomState.ROOM_WAITING;

            WaitMaxLimitCount = 0;

            // test
            GameBeginTime = 0;
            isGameStarted = false;
        }

        public void EnterRoom(PlayerObject player)
        {
            if (m_roomPlayerList.Contains(player))
                return;
            
            m_roomPlayerList.Add(player);
        }

        public void MatchimgGame(double deltaTime)
        {
            if (m_roomPlayerList.Count >= GameNeedUserCount)
            {
                CurrentRoomState = Room.RoomState.ROOM_PLAYING;
                System.Console.WriteLine("Match! : {0}", GameNeedUserCount);
                BeginTheGame(deltaTime);
            }
        }

        public void BeginTheGame(double deltaTime)
        {
            // 게임 시작 - 방정보 send
            isGameStarted = true;
            GameBeginTime = deltaTime;
        }

        public void Update(double deltaTime)
        {
            switch (CurrentRoomState)
            {
                case RoomState.ROOM_WAITING:
                {
                    WaitElapsedTime = deltaTime;
                    if (WaitElapsedTime - CheckedTime >= GameWaitTime)
                    {
                        GameNeedUserCount = (byte)(GameNeedUserCount / 2) > 1 ? (byte)(GameNeedUserCount / 2) : (byte)2;
                        WaitMaxLimitCount = (GameNeedUserCount == 2) ? ++WaitMaxLimitCount : (byte)0;

                        if (WaitMaxLimitCount > GAME_MAX_WAIT_TIME)
                        {
                            // 매칭 안됨
                            CannotMatchingGame();
                        }

                        CheckedTime = WaitElapsedTime;
                    }

                    MatchimgGame(deltaTime);
                }
                break;
                case RoomState.ROOM_PLAYING:
                {
                    ProcessGamePlaying(deltaTime);
                    
                }
                break;
                case RoomState.ROOM_GAME_ENDED:
                {

                }
                break;
            }
        }

        void ProcessGamePlaying(double deltaTime)
        {
            // 인게임 플레이
            if (deltaTime - GameBeginTime > GAME_PLAY_TIME_TEST)
            {
                ProcessGameEnd();
            }
        }

        void ProcessGameEnd()
        {
            // 게임 전체 종료.
        }

        void CannotMatchingGame()
        {
            var info = new PK_SC_CANNOT_MATCHING_GAME
            {
                type = PK_SC_CANNOT_MATCHING_GAME.MatchingErrorType.TEST2
            };

            foreach (var player in m_roomPlayerList)
            {
                player.Client?.SendPacket(info);
            }
        }

        public void RemoveUserFromRoom()
        {

        }

        public void ClearRoomData()
        {
            this.GameWaitTime = 0;
            this.GameNeedUserCount = 0;
            WaitElapsedTime = 0;
            CheckedTime = 0;
            CurrentRoomState = Room.RoomState.ROOM_WAITING;
            WaitMaxLimitCount = 0;
            m_roomPlayerList.Clear();

            // test
            GameBeginTime = 0;
            isGameStarted = false;
        }
    }
}
