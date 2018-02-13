using BaseClient;
using GameObject;
using System.Collections.Generic;

namespace Player
{
    public class PlayerObject : IGameObject
    {
        public Client Client { get; private set; }
        public PlayerData PlayerData { get; private set; }
        public int Handle { get; private set; }

        Dictionary<int, PlayerObject> m_currentObjList = new Dictionary<int, PlayerObject>();

        public void SetClient(Client client)
        {
            Client = client;
        }

        public void InitPlayerInfo(PlayerData info, int handle)
        {
            PlayerData = info;
            Handle = handle;
        }

        public void Update(double deltaTime)
        {
            if (PlayerData != null)
                PlayerData.Update(deltaTime);
        }

        public void SetCurrentObject(int handle, PlayerObject obj)
        {
            m_currentObjList.Add(handle, obj);
        }
    }
}
