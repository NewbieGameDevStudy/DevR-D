using GameObject;
using GameServer.ServerClient;
using Packet;
using Server;

namespace GameServer.Player
{
    public class PlayerObject : IGameObject
    {
        PlayerData playerData;
        Client m_client;
        BaseServer m_baseServer;

        public PlayerObject(Client client, BaseServer baseServer)
        {
            m_client = client;
            m_baseServer = baseServer;
        }

        public void LoadPlayerInfo(IPlayerInfo info)
        {
            playerData = new PlayerData {
                Exp = info.Exp,
                Level = info.Level
            };

            //TODO : DB이전까지만 임시 더미데이터 사용
            m_client.SendPacket(new PK_SC_PLAYERINFO_LOAD {
                Exp = 2,
                Level = 3
            });
        }

        public void Update(double deltaTime)
        {

        }
    }


}
