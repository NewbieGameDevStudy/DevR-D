using NetworkSocket;
using Packet;
using Player;

namespace BaseClient
{
    public partial class Client
    {
        SocketLib m_clientSocket;
        PacketMethod m_packetMethod;
        UserToken m_userToken;

        public PlayerObject Player { get; private set; }
        public bool isConnected { get; private set; }
        public object objectType { get; private set; }

        public void Init(object obj, string methodName)
        {
            m_clientSocket = new SocketLib();
            m_clientSocket.connectHandler += ConnectedToken;

            PacketList.InitPacketList();
            PacketParser.InitGenericParseMethod();

            m_packetMethod = new PacketMethod();
            //m_packetMethod.SetMethod(typeof(Client), "OnReceivePacket");
            m_packetMethod.SetMethod(obj.GetType(), methodName);
            objectType = obj;
        }

        public void Connect(string connectHost = "127.0.0.1", int port = 5050)
        {
            m_clientSocket.ConnectAsync(connectHost, port);
        }

        void ConnectedToken(UserToken token)
        {
            m_userToken = token;
            m_userToken.ReceiveDispatch = ReceiveDispatch;
            isConnected = true;

            //툴에서 사용하는 것
            Player = new PlayerObject();
            Player.SetClient(this);
        }

        public void Update(double deltaTime)
        {
            m_userToken?.ReceiveProcess();
            Player?.Update(deltaTime);
            HttpReqUpdate();
        }

        void ReceiveDispatch(int packetId, object[] parameters)
        {
            m_packetMethod?.MethodDispatch(packetId)?.Invoke(objectType, parameters);
        }

        public void SendPacket(PK_BASE pks)
        {
            m_userToken.OnSend(pks);
        }
    }
}
