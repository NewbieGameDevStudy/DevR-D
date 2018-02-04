using GameServer.Connection;
using GameServer.MatchRoom;
using GameServer.ServerClient;
using NetworkSocket;
using Packet;
using System;
using System.Collections.Generic;

namespace Server
{
    public class BaseServer
    {
        SocketLib m_serverSocket;
        PacketMethod m_packetMethod;
        Dictionary<int, Client> m_clientList = new Dictionary<int, Client>();
        Dictionary<int, Client> m_addClientList = new Dictionary<int, Client>();

        //TODO : 임시적 카운트
        int clientAccount;

        public RoomManager RoomManager { get; private set; }
        public HttpConnection HttpConnection { get; private set; }

        public void InitServer(int port)
        {
            m_serverSocket = new SocketLib();
            m_serverSocket.acceptHandler = AcceptClient;

            PacketList.InitPacketList();
            PacketParser.InitGenericParseMethod();  //Parser 함수는 MethodList 생성 이후 호출해야한다

            m_packetMethod = new PacketMethod();
            m_packetMethod.SetMethod(typeof(Client), "OnReceivePacket");

            m_serverSocket.InitServer(null, port);

            RoomManager = new RoomManager();
            HttpConnection = new HttpConnection("http://localhost:5000");
        }

        public void RunServer()
        {
            m_serverSocket.StartListen();
        }

        void AcceptClient(UserToken userToken)
        {
            Client client = new Client(userToken, clientAccount, this);
            client.PacketDispatch = PacketMethodDispatch;
            lock (m_addClientList) {
                m_addClientList.Add(clientAccount, client);
            }

            clientAccount++;
            Console.WriteLine("접속된 클라이언트 수 : {0}", clientAccount);
        }

        public void Update(double deltaTime)
        {
            UpdateClient(deltaTime);
            RoomManager.Update(deltaTime);
        }

        void UpdateClient(double deltaTime)
        {
            lock (m_addClientList) {
                foreach (var client in m_addClientList) {
                    if (!m_clientList.ContainsKey(client.Key))
                        m_clientList.Add(client.Key, client.Value);
                }
                m_addClientList.Clear();
            }

            foreach (var client in m_clientList) {
                client.Value.Update(deltaTime);
            }
        }

        void PacketMethodDispatch(int packetId, object caller, object[] parameters)
        {
            m_packetMethod.MethodDispatch(packetId)?.Invoke(caller, parameters);
        }
    }
}
