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
        public int portrait { get; set; }
        public int bestRecord { get; set; }
        public int winRecord { get; set; }
        public int continueRecord { get; set; }
        public int level { get; set; }
        public int exp { get; set; }
        public int gameMoney { get; set; }
        public string name { get; set; }
    }
}
