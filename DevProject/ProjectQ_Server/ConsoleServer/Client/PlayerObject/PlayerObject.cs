using BaseClient;
using BaseObject;
using System.Collections.Generic;

namespace Player
{
    public class PlayerObject : IBaseObject
    {
        public Client Client { get; private set; }
        public PlayerData PlayerData { get; private set; }
        public int Handle { get; private set; }
        public byte RoomNo { get; private set; }

        public Dictionary<int, PlayerObject> RoomInObjList { get; private set; }

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
            RoomInObjList = new Dictionary<int, PlayerObject>();

            //Client.SendPacket(new Packet.PK_CS_CLIENT_ACCOUNT)
        }

        public void Update(double deltaTime)
        {
            if (PlayerData != null)
                PlayerData.Update(deltaTime);

            if(RoomInObjList != null) {
                foreach (var obj in RoomInObjList) {
                    obj.Value.Update(deltaTime);
                }
            }
        }

        public void AddRoomInObject(int handle, PlayerObject obj)
        {
            RoomInObjList.Add(handle, obj);
        }

        public void SetRoomNo(byte roomNo)
        {
            RoomNo = roomNo;
        }
    }
}
