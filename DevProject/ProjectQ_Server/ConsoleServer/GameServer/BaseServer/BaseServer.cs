using GameServer.MatchRoom;
using GameServer.ServerClient;
using GameServer.WebHttp;
using GuidGen;
using Http;
using MetaData;
using NetworkSocket;
using Packet;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Server
{
    public class BaseServer
    {
        SocketLib m_serverSocket;
        GuidGen.Guid m_guid;
        PacketMethod m_packetMethod;
        Dictionary<int, Client> m_clientList = new Dictionary<int, Client>();
        Dictionary<int, Client> m_addClientList = new Dictionary<int, Client>();
        private int m_userHandle;

        public RoomManager RoomManager { get; private set; }
        public HttpWebReq HttpWebReq { get; private set; }

        public void InitServer(int port, byte machiedId)
        {
            m_serverSocket = new SocketLib {
                acceptHandler = AcceptClient,
                closeHandler = CloseClient               
            };

            PacketList.InitPacketList();
            PacketParser.InitGenericParseMethod();  //Parser 함수는 MethodList 생성 이후 호출해야한다

            m_packetMethod = new PacketMethod();
            m_packetMethod.SetMethod(typeof(Client), "OnReceivePacket");

            m_serverSocket.InitServer(null, port);

            try {
                MetaDataMgr.Inst.InitMetaData("MetaData.Data", @"\..\..\..\CsvDataTable");
            } catch (System.Exception e){
                System.Console.WriteLine("MetaData File not found : {0}", e.ToString());
            }

            //var dd = MetaDataMgr.Inst.GetMetaData<AnimalDataTable>(2);
            HttpWebReq = new HttpWebReq();
            HttpWebReq.Connect("http://localhost", 5000);            

            RoomManager = new RoomManager(HttpWebReq);

            m_guid = new GuidGen.Guid(machiedId);
        }

        public void RunServer()
        {
            m_serverSocket.StartListen();
        }

        void AcceptClient(UserToken userToken)
        {
            lock (m_addClientList) {
                Client client = new Client(this, userToken, m_userHandle) {
                    PacketDispatch = PacketMethodDispatch
                };
                m_addClientList.Add(m_userHandle, client);
                m_userHandle++;
            }

            System.Console.WriteLine("접속된 클라이언트 수 : {0}", m_userHandle);
        }

        void CloseClient()
        {
            Interlocked.Decrement(ref m_userHandle);
            System.Console.WriteLine("접속된 클라이언트 수 : {0}", m_userHandle);
        }

        public void Update(double deltaTime)
        {
            UpdateClient(deltaTime);
            HttpWebReq.UpdateWebReq();
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

            foreach (var client in m_clientList.Values) {
                client.Update(deltaTime);
            }
        }

        void PacketMethodDispatch(int packetId, object caller, object[] parameters)
        {
            m_packetMethod.MethodDispatch(packetId)?.Invoke(caller, parameters);
        }
    }
}
