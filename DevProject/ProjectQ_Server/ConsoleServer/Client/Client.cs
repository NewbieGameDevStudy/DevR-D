using NetworkSocket;
using Packet;
using Player;
using System;
using System.Collections.Generic;
namespace BaseClient
{
    public partial class Client
    {
        SocketLib m_clientSocket;
        PacketMethod m_packetMethod;
        UserToken m_userToken;

        public PlayerObject Player { get; private set; }

        public void Init()
        {
            m_clientSocket = new SocketLib();
            m_clientSocket.connectHandler += ConnectedToken;

            PacketList.InitPacketList();
            PacketParser.InitGenericParseMethod();

            m_packetMethod = new PacketMethod();
            m_packetMethod.SetMethod(typeof(Client), "OnReceivePacket");
        }

        public void Connect()
        {
            m_clientSocket.ConnectAsync("127.0.0.1", 5050);
        }

        public void ConnectedToken(UserToken token)
        {
            m_userToken = token;
            m_userToken.ReceiveDispatch = ReceiveDispatch;
            Player = new PlayerObject(this);
        }

        public void Update(double deltaTime)
        {
            m_userToken.ReceiveProcess();
            Player.Update(deltaTime);
        }

        public void ReceiveDispatch(int packetId, object[] parameters)
        {
            m_packetMethod?.MethodDispatch(packetId)?.Invoke(this, parameters);
        }

        public void SendPacket(PK_BASE pks)
        {
            m_userToken.OnSend(pks);
        }
    }
}
