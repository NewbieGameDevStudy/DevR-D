using Packet;
using Player;

namespace BaseClient
{
    public partial class Client
    {
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
