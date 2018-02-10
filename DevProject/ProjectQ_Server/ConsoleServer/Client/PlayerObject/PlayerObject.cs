using BaseClient;
using GameObject;

namespace Player
{
    public class PlayerObject : IGameObject
    {
        Client m_client;
        PlayerData playerData;

        public PlayerObject(Client client)
        {
            m_client = client;
        }

        public void InitPlayerInfo(PlayerData info)
        {
            playerData = info;
        }

        public void Update(double deltaTime)
        {
            
        }
    }
}
