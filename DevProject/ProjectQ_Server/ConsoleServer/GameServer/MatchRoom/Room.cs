using GameServer.Player;
using System;
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
            ROOM_READY_GAME,
            ROOM_PLAYING,
            ROOM_GAME_ENDED
        }

        public byte RoomNo { get; set; }

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

        List<PlayerObject> m_roomPlayerList = new List<PlayerObject>();
        List<bool> m_readyForGame = new List<bool>();

        public Room(double waitTime, byte gameNeedUserCount)
        {
            m_roomPlayerList.Clear();
            m_readyForGame.Clear();

            this.GameWaitTime = waitTime;
            this.GameNeedUserCount = gameNeedUserCount;

            RoomNo = 0;

            WaitElapsedTime = 0;
            CheckedTime = 0;
            CurrentRoomState = Room.RoomState.ROOM_WAITING;

            WaitMaxLimitCount = 0;
        }

        public void ResetRoomPlayerListIndex()
        {
            int iIndex = m_roomPlayerList.Count;
            for (int i = 0; i < iIndex; ++i)
            {
                m_roomPlayerList[i].PlayerIndex = (byte)(i + 1);
            }
        }

        public void EnterRoom(PlayerObject player)
        {
            Random portRait = new Random(100);
            if (m_roomPlayerList.Contains(player))
            {
                Console.WriteLine("RoomNo : {0}, Already in {0} ", RoomNo, player.UserSequence);
                return;
            }

            // 신규 참가 인원 : 기존 방인원 정보 send
            // 기존 참가 인원 : 신규 인원 정보 send

            PK_SC_MATCHING_ROOM_INFO pks = new PK_SC_MATCHING_ROOM_INFO();
            pks.m_memberList = new List<PK_SC_MATCHING_MEMBER_INFO>();

            PK_SC_MATCHING_MEMBER_INFO newInfo = new PK_SC_MATCHING_MEMBER_INFO
            {
                strNickName = "test1",
                portRaitNo = portRait.Next(1, 100)
            };

            foreach (var info in m_roomPlayerList)
            {
                info.Client?.SendPacket(newInfo);

                PK_SC_MATCHING_MEMBER_INFO tempInfo = new PK_SC_MATCHING_MEMBER_INFO
                {
                    strNickName = "info" + info.AccountID,           // info 닉네임
                    portRaitNo = portRait.Next(1, 100)              // info 초상화
                };

                pks.m_memberList.Add(tempInfo);
            }

            player.Client?.SendPacket(pks);

            m_roomPlayerList.Add(player);
            player.EnteredRoomNo = this.RoomNo;
            player.PlayerIndex = (byte)m_roomPlayerList.Count;
        }

        public void MatchimgGame(double deltaTime)
        {
            if (m_roomPlayerList.Count >= GameNeedUserCount)
            {
                CurrentRoomState = Room.RoomState.ROOM_READY_GAME;
                System.Console.WriteLine("Match! : Need - {0}, User - {0}", GameNeedUserCount, m_roomPlayerList.Count);

                // 게임 시작 준비
                ReadyForGame();
            }
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
                case RoomState.ROOM_READY_GAME:
                {
                    bool isReady = false;
                    foreach (var playerAccept in m_readyForGame)
                    {
                        if (playerAccept == false)
                        {
                            break;
                        }
                        isReady = true;
                    }

                    if (isReady)
                        CurrentRoomState = RoomState.ROOM_PLAYING;
                }
                break;
                case RoomState.ROOM_PLAYING:
                {
                    if (m_roomPlayerList.Count == 1)
                    {
                        

                    }
                }
                break;
                case RoomState.ROOM_GAME_ENDED:
                {

                }
                break;
            }
        }

        void ReadyForGame()
        {
            var info = new PK_SC_READY_FOR_GAME
            {
                gameUserCount = this.m_roomPlayerList.Count,
                roomNo = this.RoomNo
            };

            foreach (var player in m_roomPlayerList)
            {
                player.Client?.SendPacket(info);
            }
        }

        void CannotMatchingGame()
        {
            Console.WriteLine("Matching Fail! RoomNo : {0}", RoomNo);
            var info = new PK_SC_CANNOT_MATCHING_GAME
            {
                type = PK_SC_CANNOT_MATCHING_GAME.MatchingErrorType.MAX_WAIT_TIME
            };

            foreach (var player in m_roomPlayerList)
            {
                info.userSequence = player.UserSequence;
                player.Client?.SendPacket(info);
            }

            ClearRoomData();
        }

        public void RemoveUserFromRoom(PlayerObject player)
        {
            m_roomPlayerList.Remove(player);
            player.EnteredRoomNo = 0;
            player.PlayerIndex = 0;

            // 다른 인원들한테 방 나간 사람 보내줌.
            // 방 나간 인원한테는 그냥 보내줌.

            var info = new PK_SC_CANNOT_MATCHING_GAME
            {
                type = PK_SC_CANNOT_MATCHING_GAME.MatchingErrorType.CANCEL_ROOM,
                userSequence = player.UserSequence
            };
            player.Client?.SendPacket(info);

            foreach (var user in m_roomPlayerList)
            {
                user.Client?.SendPacket(info);
            }
            
            ResetRoomPlayerListIndex();
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
            m_readyForGame.Clear();
        }
    }
}
