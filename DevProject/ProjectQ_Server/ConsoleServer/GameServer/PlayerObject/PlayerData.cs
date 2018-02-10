using GameObject;

namespace GameServer.Player
{
    public class PlayerData : IPlayerInfo
    {
        public float Xpos { get; set; }
        public float Ypos { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
    }
}
