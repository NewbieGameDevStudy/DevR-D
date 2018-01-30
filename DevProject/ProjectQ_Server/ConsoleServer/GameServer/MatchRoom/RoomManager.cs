using GameServer.Player;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.MatchRoom
{
    public class RoomManager
    {
        public enum RoomType {
            COMMON_SENSE,
            SOCIAL,
            GAME,
        }

        public class QueueData {
            public RoomType type;
            public PlayerObject player;
        }

        Dictionary<RoomType, List<Room>> m_roomList = new Dictionary<RoomType, List<Room>>();
        Queue<QueueData> m_enterPlayerQueue = new Queue<QueueData>();

        public void EnterRoom(RoomType type, PlayerObject player)
        {
            m_enterPlayerQueue.Enqueue(new QueueData {
                player = player,
                type = type
            });
        }

        void EnterRoomSerach()
        {
            if (m_enterPlayerQueue.Count > 0) {

            }
        }

        public void Update(double deltaTime)
        {
            foreach (var roomTypes in m_roomList) {
                foreach (var room in roomTypes.Value) {
                    room.Update(deltaTime);
                }
            }
        }
    }
}
