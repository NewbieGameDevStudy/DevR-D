using NetworkSocket;
using Packet;
using ServerClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseServer {
    public class GameServer {
        SocketLib serverSocket;
        PacketMethod packetMethod;
        Thread updateThread;

        Dictionary<int, Client> clientList = new Dictionary<int, Client>();
        Dictionary<int, Client> addClientList = new Dictionary<int, Client>();

        int clientAccount;

        public void InitServer(int port) {
            serverSocket = new SocketLib();
            serverSocket.acceptHandler = AcceptClient;

            PacketList.InitPacketList();
            PacketParser.InitGenericParseMethod();  //Parser 함수는 MethodList 생성 이후 호출해야한다

            packetMethod = new PacketMethod();
            packetMethod.SetMethod(typeof(Client), "OnReceivePacket");

            serverSocket.InitServer(port);
            updateThread = new Thread(Update);
        }

        public void RunServer() {
            updateThread.Start();
            serverSocket.StartListen();
        }

        void AcceptClient(UserToken userToken) {
            Client client = new Client(userToken, clientAccount);
            client.PacketDispatch = PacketMethodDispatch;
            lock (addClientList) {
                addClientList.Add(clientAccount, client);
            }

            clientAccount++;
        }

        public void Update() {
            while (true) {
                lock (addClientList) {
                    foreach (var client in addClientList) {
                        if (!clientList.ContainsKey(client.Key))
                            clientList.Add(client.Key, client.Value);
                    }
                    addClientList.Clear();
                }

                foreach (var client in clientList) {
                    client.Value.Update();
                }
            }
        }

        void PacketMethodDispatch(int packetId, object caller, object[] parameters) {
            packetMethod.MethodDispatch(packetId)?.Invoke(caller, parameters);
        }
    }
}
