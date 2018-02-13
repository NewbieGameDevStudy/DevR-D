using GameObject;
using GameServer.Player.Component;
using GameServer.ServerClient;
using Packet;
using Server;
using System;
using System.Collections.Generic;

namespace GameServer.Player
{
    public class PlayerObject : IGameObject
    {
        public Client Client { get; private set; }
        public PlayerData PlayerData { get; private set; }

        BaseServer m_baseServer;

        Dictionary<Type, BaseComponent> m_components = new Dictionary<Type, BaseComponent>();

        public PlayerObject(Client client, BaseServer baseServer)
        {
            Client = client;
            m_baseServer = baseServer;

            //TODO : 컴퍼넌트가 늘어난다면 별도의 Create로 분리하는것도 고려해볼것
            var moveComp = new MoveComponent(this);
        }

        //TODO : 아래 로드부분도 마찬가지로 늘어난다면 별도로 분리
        public void LoadPlayerInfo()
        {
            //TODO : 웹서버로 db부분 구현전까진 더미데이터 활용
            //var testDTO = new TestDTO();
            //testDTO.a = 123123;
            //m_httpConnection.HttpConnectAsync(testDTO, (result) => {
            //    var dffd = result;
            //});

            //더미데이터
            PlayerData = new PlayerData();
            PlayerData.Level = 10;
            PlayerData.Exp = 100;
            PlayerData.Xpos = 20;
            PlayerData.Ypos = 20;

            Client.SendPacket(new PK_SC_PLAYERINFO_LOAD {
                handle = Client.AccountCount,
                Exp = PlayerData.Exp,
                Level = PlayerData.Level,
            });
        }

        public void Update(double deltaTime)
        {
            foreach (var component in m_components) {
                component.Value.Update(deltaTime);
            }
        }
    }
}
