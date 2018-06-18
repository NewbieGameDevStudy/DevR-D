using System;
using System.Net;
using System.Net.Sockets;

namespace NetworkSocket
{
    public partial class SocketLib
    {
        Socket m_socket;

        BufferManager m_bufferManager;
        SocketAsyncEventArgsPool m_saeapRecvPool;
        SocketAsyncEventArgsPool m_saeapSendPool;

        int m_maxConnection = 5000;
        int m_bufferSize = 1024;
        IPEndPoint m_localEndPoint;
        object m_saeaLock = new object();

        public delegate void AcceptHandler(UserToken userToken);
        public delegate void CloseHandler();

        public CloseHandler closeHandler { get; set; }
        public AcceptHandler acceptHandler { get; set; }

        public void InitServer(string ip, int port)
        {
            if (string.IsNullOrEmpty(ip))
                ip = "192.168.0.2";//"127.0.0.1";

            m_localEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 2 : send + recv
            m_bufferManager = new BufferManager(m_maxConnection * m_bufferSize * 2, m_bufferSize);
            m_bufferManager.InitBuffer();

            m_saeapRecvPool = new SocketAsyncEventArgsPool(m_maxConnection);
            m_saeapSendPool = new SocketAsyncEventArgsPool(m_maxConnection);

            //recv, send 각각 초기화
            for (int i = 0; i < m_maxConnection; ++i) {
                var userToken = new UserToken();

                SocketAsyncEventArgs recvArgs = new SocketAsyncEventArgs();
                recvArgs.Completed += ReceiveComplete;
                recvArgs.UserToken = userToken;
                m_bufferManager.SetBuffer(recvArgs);
                m_saeapRecvPool.Push(recvArgs);

                SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();
                sendArgs.Completed += SendComplete;
                sendArgs.UserToken = userToken;
                m_bufferManager.SetBuffer(sendArgs);
                m_saeapSendPool.Push(sendArgs);
            }
        }

        public void StartListen()
        {
            try {
                m_socket.Bind(m_localEndPoint);
                m_socket.Listen(100);

                Console.WriteLine("Start GameServer");

                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += AcceptComplete;
                m_socket.AcceptAsync(args);

            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }

        void AcceptComplete(object sender, SocketAsyncEventArgs e)
        {
            Socket acceptSocket = e.AcceptSocket;
            acceptSocket.NoDelay = true;

            SocketAsyncEventArgs receiveSaea;
            SocketAsyncEventArgs sendSaea;

            lock (m_saeaLock) {
                 receiveSaea = m_saeapRecvPool.Pop();
                 sendSaea = m_saeapSendPool.Pop();
            }

            var userToken = receiveSaea.UserToken as UserToken;
            userToken.Init(acceptSocket, sendSaea, receiveSaea);

            acceptHandler?.Invoke(userToken);

            receiveSaea.AcceptSocket = acceptSocket;
            acceptSocket.ReceiveAsync(receiveSaea);

            e.AcceptSocket = null;
            m_socket.AcceptAsync(e);
        }
    }
}
