﻿using GameServer.Player;
using Http;
using NetworkSocket;
using Packet;
using Server;
using System;

namespace GameServer.ServerClient
{
    public partial class Client
    {
        UserToken m_userToken;
        BaseServer m_baseServer;

        //public ulong AccountId { get; private set; }
        public int AccountCount { get; private set; }
        public HttpConnection HttpConnection { get; }
        public PlayerObject Player { get; private set; }
        public Action<int, object, object[]> PacketDispatch;

        public Client(BaseServer baseServer, UserToken userToken, int m_accountCount)
        {
            m_baseServer = baseServer;
            HttpConnection = baseServer.HttpConnection;
            m_userToken = userToken;
            userToken.ReceiveDispatch = ReceiveDispatch;            
            AccountCount = m_accountCount;

            Player = new PlayerObject(this, m_baseServer);
            Player.LoadPlayerInfo();
        }

        public void Update(double deltaTime)
        {
            m_userToken.ReceiveProcess();
            Player.Update(deltaTime);
        }

        public void ReceiveDispatch(int packetId, object[] parameters)
        {
            PacketDispatch?.Invoke(packetId, this, parameters);
        }

        public void SendPacket(PK_BASE pks)
        {
            m_userToken.OnSend(pks);
        }
    }
}
