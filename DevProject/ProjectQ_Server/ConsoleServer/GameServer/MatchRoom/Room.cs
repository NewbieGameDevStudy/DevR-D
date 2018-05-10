using GameServer.Player;
using System.Collections.Generic;

namespace GameServer.MatchRoom
{
    public class Room
    {
        // 매칭까지 인원수
        // 매칭까지 걸리는 시간. 
        // 시간이 지날때까지 매칭이 되지 않으면 인원수를 절반으로 감소 시킨다. -> 2 까지

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
                return roomPlayerList.Count;
            }
        }

        double GameWaitTime;
        double WaitElapsedTime;
        double CheckedTime;

        List<PlayerObject> roomPlayerList = new List<PlayerObject>();

        public Room(double waitTime, byte gameNeedUserCount)
        {
            this.GameWaitTime = waitTime;
            this.GameNeedUserCount = gameNeedUserCount;

            WaitElapsedTime = 0;
            CheckedTime = 0;
            CurrentRoomState = Room.RoomState.ROOM_WAITING;
        }

        public void EnterRoom(PlayerObject player)
        {
            roomPlayerList.Add(player);
            BeginTheGame();
        }

        public bool BeginTheGame()
        {
            if (roomPlayerList.Count >= GameNeedUserCount)
            {
                // 게임 시작
                CurrentRoomState = Room.RoomState.ROOM_PLAYING;
                System.Console.WriteLine("Match! : {0}", GameNeedUserCount);
                return true;
            }

            return false;
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
                            CheckedTime = WaitElapsedTime;
                        }

                        BeginTheGame();
                }
                break;
                case RoomState.ROOM_PLAYING:
                {

                }
                break;
                case RoomState.ROOM_GAME_ENDED:
                {

                }
                break;
            }
        }
    }
}
