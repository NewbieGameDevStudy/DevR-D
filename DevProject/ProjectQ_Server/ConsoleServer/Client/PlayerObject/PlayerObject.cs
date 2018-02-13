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

        public Dictionary<int, PlayerObject> CurrentObjList { get; private set; }

        //TODO : 임시변수로 삭제될 수 있음
        public bool isEnterComplete;

        public void SetClient(Client client)
        {
            Client = client;
        }

        public void InitPlayerInfo(PlayerData info, int handle)
        {
            PlayerData = info;
            Handle = handle;
            CurrentObjList = new Dictionary<int, PlayerObject>();
        }

        public void Update(double deltaTime)
        {
            if (PlayerData != null)
                PlayerData.Update(deltaTime);
        }

        public void SetCurrentObject(int handle, PlayerObject obj)
        {
            CurrentObjList.Add(handle, obj);
        }
    }
}
