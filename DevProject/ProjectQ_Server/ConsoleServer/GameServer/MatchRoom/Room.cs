using GameServer.Player;
using System.Collections.Generic;

namespace GameServer.MatchRoom
{
    public class Room
    {
        public bool IsWaiting;
        List<PlayerObject> roomPlayerList = new List<PlayerObject>();

        public void EnterRoom(PlayerObject player)
        {
            roomPlayerList.Add(player);
        }

        public void Update(double deltaTime)
        {

        }
    }
}
