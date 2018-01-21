using NetworkSocket;
using Packet;
using System;

namespace ServerClient
{
    public partial class Client
    {
        UserToken m_userToken;
        int m_accountId;

        public Action<int, object, object[]> PacketDispatch;

        public Client(UserToken userToken, int accountValue)
        {
            m_accountId = accountValue;
            m_userToken = userToken;
            userToken.ReceiveDispatch = ReceiveDispatch;
        }

        int a = 0;
        public void Update()
        {
            m_userToken.ReceiveProcess();

            a++;
            if (a == 1000) {
                SendPacket(new PK_SC_PING { receiveId = m_accountId, str = "DD" });
                a = 0;
            }
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
