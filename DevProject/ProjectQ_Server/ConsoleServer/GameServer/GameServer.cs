using NetworkSocket;
using Packet;
using ServerClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseServer {
    public class GameServer {
        SocketLib serverSocket;
        PacketMethod packetMethod;

        Dictionary<int, Client> clientList = new Dictionary<int, Client>();

        int clientAccount;

        public void InitServer(int port) {
            serverSocket = new SocketLib();
            serverSocket.acceptHandler = AcceptClient;

            PacketList.InitPacketList();
            PacketParser.InitGenericParseMethod();  //Parser 함수는 MethodList 생성 이후 호출해야한다
            packetMethod = new PacketMethod();

            serverSocket.InitServer(port);
            packetMethod.SetMethod(typeof(Client), "OnReceivePacket");
        }

        void AcceptClient(UserToken userToken) {
            Client client = new Client(userToken);
            clientList.Add(clientAccount, client);
            clientAccount++;
        }


    }
}
