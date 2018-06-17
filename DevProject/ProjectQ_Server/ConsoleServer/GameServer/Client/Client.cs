using GameServer.Player;
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
        public int Handle { get; private set; }
        public PlayerObject Player { get; private set; }
        public Action<int, object, object[]> PacketDispatch;

        public Client(BaseServer baseServer, UserToken userToken, int handle)
        {
            m_baseServer = baseServer;
            m_userToken = userToken;
            userToken.ReceiveDispatch = ReceiveDispatch;
            Handle = handle;

            Player = new PlayerObject(this, m_baseServer);

            SendPacket(new PK_SC_CLIENT_HANDLE {
                serverHandle = handle,
            });
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
