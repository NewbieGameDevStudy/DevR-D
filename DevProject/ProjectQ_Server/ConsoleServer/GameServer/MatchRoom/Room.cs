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

        Dictionary<ulong, PlayerObject> m_roomPlayerList = new Dictionary<ulong, PlayerObject>();
        Dictionary<ulong, bool> m_readyForGame = new Dictionary<ulong, bool>();

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

        public void ResetRoomState()
        {
            
        }

        public void EnterRoom(PlayerObject player)
        {
            Random portRait = new Random(100);
            if (m_roomPlayerList.ContainsValue(player))
            {
                Console.WriteLine("RoomNo : {0}, Already in {0} ", RoomNo, player.AccountIDClient);
                return;
            }

            // 신규 참가 인원 : 기존 방인원 정보 send
            // 기존 참가 인원 : 신규 인원 정보 send

            PK_SC_MATCHING_ROOM_INFO pks = new PK_SC_MATCHING_ROOM_INFO();
            pks.m_memberList = new List<PK_SC_MATCHING_MEMBER_INFO>();

            PK_SC_MATCHING_MEMBER_INFO newInfo = new PK_SC_MATCHING_MEMBER_INFO
            {
                NickName = player.PlayerData.name,
                PortRaitNo = player.PlayerData.iPortrait,
                AccountIDClient = player.AccountIDClient
            };

            foreach (var info in m_roomPlayerList)
            {
                info.Value.Client?.SendPacket(pks);

                PK_SC_MATCHING_MEMBER_INFO tempInfo = new PK_SC_MATCHING_MEMBER_INFO
                {
                    PortRaitNo = info.Value.PlayerData.iPortrait,
                    NickName = info.Value.PlayerData.name,
                    AccountIDClient = info.Value.AccountIDClient
                };

                pks.m_memberList.Add(tempInfo);
            }

            player.Client?.SendPacket(pks);

            m_roomPlayerList.Add(player.AccountIDClient, player);
            player.EnteredRoomNo = this.RoomNo;
        }

        public void MatchimgGame(double deltaTime)
        {
            if (m_roomPlayerList.Count >= GameNeedUserCount)
            {
                CurrentRoomState = Room.RoomState.ROOM_READY_GAME;
                System.Console.WriteLine("Match! : Need - {0}, User - {0}", GameNeedUserCount, m_roomPlayerList.Count);

                // 게임 시작 준비
                ReadyForGameCheck();
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
                    bool isReadyComplete = false;
                    foreach (var ready in m_readyForGame)
                    {
                        if (ready.Value == false)
                        {
                            isReadyComplete = false;
                            break;
                        }
                        isReadyComplete = true;
                    }

                    if (isReadyComplete)
                        CurrentRoomState = RoomState.ROOM_PLAYING;
                }
                break;
                case RoomState.ROOM_PLAYING:
                {
                    if (m_roomPlayerList.Count == 1)
                    {
                        // Game End
                        GameEndProcess();
                    }
                }
                break;
                case RoomState.ROOM_GAME_ENDED:
                {

                }
                break;
            }
        }

        void ReadyForGameCheck()
        {
            var info = new PK_SC_READY_FOR_GAME
            {
                GameUserCount = this.m_roomPlayerList.Count,
                RoomNo = this.RoomNo
            };

            foreach (var player in m_roomPlayerList)
            {
                player.Value.Client?.SendPacket(info);
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
                info.AccountIDClient = player.Value.AccountIDClient;
                player.Value.Client?.SendPacket(info);
            }

            ClearRoomData();
        }

        public void MovePosition(PlayerObject player)
        {
            if (m_roomPlayerList.ContainsValue(player) == false)
                return;

            var info = new PK_SC_MOVE_POSITION
            {
                AccountIDClient = player.AccountIDClient,
                RoomNo = player.EnteredRoomNo,
                xPos = (float)player.PlayerData.Xpos,
                yPos = (float)player.PlayerData.Ypos
            };

            foreach (var tempPlayer in m_roomPlayerList)
            {
                tempPlayer.Value.Client?.SendPacket(info);
            }            
        }

        public void GameEndProcess(PlayerObject player = null)
        {
            var info = new PK_SC_GAME_END
            {
                Rank = (byte)m_roomPlayerList.Count,
            };

            if (player != null)
            {
                player.Client?.SendPacket(info);
                m_roomPlayerList.Remove(player.AccountIDClient);
            }
            else
            {
                foreach (var tempPlayer in m_roomPlayerList)
                {
                    tempPlayer.Value.Client?.SendPacket(info);
                }
                ClearRoomData();
            }
        }

        public void ReadyForGame(PlayerObject player)
        {
            m_readyForGame.Add(player.AccountIDClient, true);
        }

        public void RemoveUserFromRoom(PlayerObject player)
        {
            m_roomPlayerList.Remove(player.AccountIDClient);
            m_readyForGame.Remove(player.AccountIDClient);
            player.EnteredRoomNo = 0;

            // 다른 인원들한테 방 나간 사람 보내줌.
            // 방 나간 인원한테는 그냥 보내줌.

            var info = new PK_SC_CANNOT_MATCHING_GAME
            {
                type = PK_SC_CANNOT_MATCHING_GAME.MatchingErrorType.CANCEL_ROOM,
                AccountIDClient = player.AccountIDClient
            };
            player.Client?.SendPacket(info);

            foreach (var user in m_roomPlayerList)
            {
                user.Value.Client?.SendPacket(info);
            }
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
