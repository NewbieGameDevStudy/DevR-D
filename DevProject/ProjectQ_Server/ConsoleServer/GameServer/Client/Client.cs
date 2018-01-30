using GameServer.Player;
using NetworkSocket;
using Packet;
using Server;
using System;

namespace GameServer.ServerClient
{
    public partial class Client
    {
        int m_accountId;

        UserToken m_userToken;
        BaseServer m_baseServer;

        public PlayerObject Player { get; private set; }
        public Action<int, object, object[]> PacketDispatch;

        public Client(UserToken userToken, int accountValue, BaseServer baseServer)
        {
            m_baseServer = baseServer;
            m_accountId = accountValue;
            m_userToken = userToken;
            userToken.ReceiveDispatch = ReceiveDispatch;

            Player = new PlayerObject(this, m_baseServer);

            //TODO : 여기서 웹서버로 부터 해당 플레이어 정보를 가져오고
            //이후에 LoadPlayerInfo 처리로 플레이어를 서버쪽에 저장해둔다
            PlayerData d = new PlayerData();
            Player.LoadPlayerInfo(d);
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
