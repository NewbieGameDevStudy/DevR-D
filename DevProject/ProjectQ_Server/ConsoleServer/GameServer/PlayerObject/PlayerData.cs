using BaseObject;

namespace GameServer.Player
{
    public class PlayerData : IPlayerInfo
    {
        // base
        public double Xpos { get; set; }
        public double Ypos { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }

        // from webServer
        public ulong accountId { get; set; }
        public string name { get; set; }
        public int iGamaMoney { get; set; }
        public int iPortrait { get; set; }
        public int iBestRecord { get; set; }
        public int iWinRecord { get; set; }
        public int iContinueRecord { get; set; }

        // inven
        public int iSlot0 { get; set; }
        public int iSlot1 { get; set; }

    }
}
