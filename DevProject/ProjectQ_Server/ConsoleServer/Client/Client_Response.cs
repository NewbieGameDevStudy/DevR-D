using Packet;
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
            Player.InitPlayerInfo(new Player.PlayerData {
                Exp = pks.Exp,
                Level = pks.Level
            });
        }
    }
}
