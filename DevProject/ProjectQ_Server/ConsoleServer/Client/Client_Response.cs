using Packet;
using Player;
using System;

namespace BaseClient
{
    public partial class Client
    {
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

            Console.WriteLine(strTemp, "{0}", pks.AccountIDClient);
            // disconnect;
        }

        void OnReceivePacket(PK_SC_MATCHING_ROOM_INFO pks)
        {
            if (pks == null || pks.m_memberList == null)
                return;

            foreach (var member in pks.m_memberList)
            {
                Console.WriteLine("{0}", member.AccountIDClient);
            }
        }

        void OnReceivePacket(PK_SC_READY_FOR_GAME pks)
        {
            if (pks == null)
                return;

            //pks.roomNo
            Console.WriteLine("Room Ready : {0}", pks.RoomNo);
        }

        

        //void OnReceivePacket(PK_SC_PLAYERINFO_LOAD pks)
        //{
        //    Player.InitPlayerInfo(new PlayerData(pks.Level, pks.Exp), pks.handle);
        //}

        //void OnReceivePacket(PK_SC_OBJECTS_INFO pks)
        //{
        //    foreach (var obj in pks.m_objectList) {
        //        if (obj.handle == Player.Handle) {
        //            Player.PlayerData.SetCurrentPosition(new Util.Vec2(obj.position.xPos, obj.position.yPos));
        //            Player.isEnterComplete = true;
        //            continue;
        //        }

        //        var otherObj = new PlayerObject();
        //        otherObj.InitPlayerInfo(new PlayerData(obj.info.Level, obj.info.Exp), obj.handle);
        //        otherObj.PlayerData.SetCurrentPosition(new Util.Vec2(obj.position.xPos, obj.position.yPos));
        //        Player.AddRoomInObject(obj.handle, otherObj);
        //    }
        //}

        //void OnReceivePacket(PK_SC_OBJECTS_POSITION pks)
        //{
        //    foreach (var obj in pks.m_objectList) {
        //        if (obj.handle == Player.Handle)
        //            continue;

        //        if (!Player.RoomInObjList.ContainsKey(obj.handle))
        //            continue;

        //        var otherObj = Player.RoomInObjList[obj.handle];
        //        otherObj.PlayerData.SetTargetPosition(obj.xPos, obj.yPos);
        //    }
        //}
    }
}
