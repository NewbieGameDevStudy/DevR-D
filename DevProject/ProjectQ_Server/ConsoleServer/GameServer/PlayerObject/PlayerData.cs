using GameObject;

namespace GameServer.Player
{
    public class PlayerData : IPlayerInfo
    {
        public double Xpos { get; set; }
        public double Ypos { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
    }
}
