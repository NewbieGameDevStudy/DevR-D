using Packet;
using Player;
using System;

namespace BaseClient
{
    public partial class Client
    {
        void OnReceivePacket(PK_SC_CLIENT_HANDLE pks)
        {
            Player.InitPlayerInfo(new PlayerData(1, 1), pks.serverHandle);
        }

        void OnReceivePacket(PK_SC_CANNOT_MATCHING_GAME pks)
        {
            if (pks == null)
                return;

            string strTemp = "";
            switch (pks.type)
            {
                case PK_SC_CANNOT_MATCHING_GAME.MatchingErrorType.CANCEL_ROOM:
                    strTemp = "CANCEL_ROOM";
                    break;
                case PK_SC_CANNOT_MATCHING_GAME.MatchingErrorType.CANCEL_WAIT:
                    strTemp = "CANCEL_ROOM";
                    break;
                case PK_SC_CANNOT_MATCHING_GAME.MatchingErrorType.MAX_WAIT_TIME:
                    strTemp = "CANCEL_ROOM";
                    break;
                case PK_SC_CANNOT_MATCHING_GAME.MatchingErrorType.PLAYER_OVERLAPPED:
                    strTemp = "CANCEL_ROOM";
                    break;
                default:
                    break;
            }

            Console.WriteLine(strTemp, "{0}", pks.accountId);
            // disconnect;
        }

        void OnReceivePacket(PK_SC_MATCHING_ROOM_INFO pks)
        {
            if (pks == null || pks.memberList == null)
                return;

            foreach (var member in pks.memberList)
            {
                Console.WriteLine("{0}", member.handle);
            }
        }

        void OnReceivePacket(PK_SC_READY_FOR_GAME pks)
        {
            if (pks == null)
                return;

            //pks.roomNo
            Console.WriteLine("Room Ready : {0}", pks.RoomNo);
        }
    }
}
