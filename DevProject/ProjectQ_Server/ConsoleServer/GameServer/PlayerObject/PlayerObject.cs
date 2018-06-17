using BaseObject;
using GameServer.WebHttp;
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
        public ulong WebAccountId { get; set; }
        public int Handle => Client.Handle;

        public byte EnteredRoomNo { get; set; }             // 게임 참가 방번호
        public byte PlayerIndex { get; set; }               // 방에서 몇번째로 들어왔는지

        BaseServer m_baseServer;

        Dictionary<Type, BaseComponent> m_components = new Dictionary<Type, BaseComponent>();

        public PlayerObject(Client client, BaseServer baseServer)
        {
            Client = client;
            m_baseServer = baseServer;
            PlayerData = new PlayerData();
        }

        public void Update(double deltaTime)
        {
            foreach (var component in m_components) {
                component.Value.Update(deltaTime);
            }
        }
    }
}
