using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packet;

namespace GameServer.Player
{
    public sealed class PlayerManager
    {
        private static readonly PlayerManager m_Instance = new PlayerManager();
        private PlayerManager() { }
        public static PlayerManager Instance
        {
            get
            {
                return m_Instance;
            }
        }

        Dictionary<ulong, PlayerObject> m_connectedPlayer = new Dictionary<ulong, PlayerObject>();

        void InsertPlayer()
        {

        }

        void RemovePlayer()
        {

        }

        void RequestPlayerInfo()
        {

        }

        void UpdatePlayerInfo()
        {

        }

        // 
        public static void SendCannotMatchingPacket(PlayerObject player, PK_SC_CANNOT_MATCHING_GAME.MatchingErrorType eErrorType)
        {
            player.Client?.SendPacket(new PK_SC_CANNOT_MATCHING_GAME
            {
                type = eErrorType,
                accountId = player.WebAccountId
            });
        }


    }
}
