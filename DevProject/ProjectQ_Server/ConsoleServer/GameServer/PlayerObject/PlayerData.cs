using BaseObject;

namespace GameServer.Player
{
    public class PlayerData : IPlayerInfo
    {
        public class UserInfo
        {
            public string Name { get; set; }
            public int Level { get; set; }
            public int Portrait { get; set; }
            public int Exp { get; set; }
        }

        public float Xpos { get; set; }
        public float Ypos { get; set; }

        // inven
        public int Slot0 { get; set; }
        public int Slot1 { get; set; }

        public UserInfo info = new UserInfo();
    }
}
