using Packet;
using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClient
{
    public partial class Client
    {
        void OnReceivePacket(PK_SC_PLAYERINFO_LOAD pks)
        {
            Player.InitPlayerInfo(new PlayerData(pks.Level, pks.Exp), pks.handle);
        }

        void OnReceivePacket(PK_SC_TARGET_POSITION pks)
        {
            Player.PlayerData.SetTargetPosition(pks.xPos, pks.yPos);
        }

        void OnReceivePacket(PK_SC_OBJECTS_INFO pks)
        {
            foreach (var obj in pks.m_objectList) {
                if (obj.handle == Player.Handle)
                    continue;

                var otherObj = new PlayerObject();
                otherObj.InitPlayerInfo(new PlayerData(obj.info.Level, obj.info.Exp), obj.handle);
                Player.SetCurrentObject(obj.handle, otherObj);
            }
        }
    }
}
