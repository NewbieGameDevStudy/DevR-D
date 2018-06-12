using BaseObject;
using GameServer.HttpDTO;
using GameServer.Player.Component;
using GameServer.ServerClient;
using Packet;
using Server;
using System;
using System.Collections.Generic;

namespace GameServer.Player
{
    public class PlayerObject : IBaseObject
    {
        public Client Client { get; private set; }
        public PlayerData PlayerData { get; private set; }
        public ulong AccountIDClient => PlayerData.accountId;   // 매칭용 - 클라에서 받는다.
        public int Handle => Client.AccountCount;
        public ulong AccountID { get; set; }                    // 서버 자체 ID

        public byte EnteredRoomNo { get; set; }                 // 게임 참가 방번호

        BaseServer m_baseServer;

        Dictionary<Type, BaseComponent> m_components = new Dictionary<Type, BaseComponent>();

        public PlayerObject(Client client, BaseServer baseServer)
        {
            Client = client;
            m_baseServer = baseServer;

            EnteredRoomNo = 0;
            
            //TODO : 컴퍼넌트가 늘어난다면 별도의 Create로 분리하는것도 고려해볼것
            var moveComp = new MoveComponent(this);
        }

        public void SetAccountIDClient(ulong AccountIdClient)
        {
            PlayerData.accountId = AccountIdClient;
        }

        public void LoadPlayerInfo()
        {
            if (AccountIDClient == 0)  
                return;

            

            //ReqLoginInfo playerInfo = new ReqLoginInfo
            //{
            //    accountId = this.UserSequence
            //};

            //Client.HttpConnection.HttpConnectAsync(playerInfo, (RespLoginInfo result) =>
            //{
            //    if (result == null)
            //        return;

            //    PlayerData = new PlayerData
            //    {
            //        Xpos = 0,
            //        Ypos = 0,
            //        Exp = result.exp,
            //        Level = result.level,


            //        /*
            //        public ulong accountId { get; set; }
            //        public int portrait { get; set; }
            //        public int bestRecord { get; set; }
            //        public int winRecord { get; set; }
            //        public int continueRecord { get; set; }
            //        public int level { get; set; }
            //        public int exp { get; set; }
            //        public int gameMoney { get; set; }
            //        public string name { get; set; }*/
            //    };
            //});

            //var playerInfo = new ReqPlayerInfo {
            //    accountId = 100,
            //};

            //Client.HttpConnection.HttpConnectAsync(playerInfo, (RespPlayerInfo result) => {
            //    if (result == null)
            //        return;

            //    PlayerData = new PlayerData {
            //        Exp = result.exp,
            //        Level = result.level,
            //    };

            //    Client.SendPacket(new PK_SC_PLAYERINFO_LOAD {
            //        handle = Handle,
            //        Exp = result.exp,
            //        Level = result.level,
            //    });
            //});
        }

        public void Update(double deltaTime)
        {
            foreach (var component in m_components) {
                component.Value.Update(deltaTime);
            }
        }
    }
}
