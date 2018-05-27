using BaseObject;
using GameServer.Connection;
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
        public int Handle => Client.AccountCount;
        public ulong AccountID => Client.AccountId;

        public byte EnteredRoomNo { get; set; }             // 게임 참가 방번호
        public byte PlayerIndex { get; set; }               // 방에서 몇번째로 들어왔는지

        BaseServer m_baseServer;

        Dictionary<Type, BaseComponent> m_components = new Dictionary<Type, BaseComponent>();

        public PlayerObject(Client client, BaseServer baseServer)
        {
            Client = client;
            m_baseServer = baseServer;

            EnteredRoomNo = 0;
            PlayerIndex = 0;

            //TODO : 컴퍼넌트가 늘어난다면 별도의 Create로 분리하는것도 고려해볼것
            var moveComp = new MoveComponent(this);
        }

        public void LoadPlayerInfo()
        {
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
