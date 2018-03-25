using System.Net;
using System.Net.Sockets;

namespace NetworkSocket
{
    public partial class SocketLib
    {
        public delegate void ConnectHandler(UserToken userToken);
        public ConnectHandler connectHandler { get; set; }

        public void ConnectAsync(string ip, int port)
        {
            m_localEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            SocketAsyncEventArgs args = new SocketAsyncEventArgs {
                RemoteEndPoint = m_localEndPoint
            };
            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            args.Completed += ConnectComplete;
            args.AcceptSocket = m_socket;

            m_socket.ConnectAsync(args);
        }

        public void ConnectComplete(object sender, SocketAsyncEventArgs e)
        {
            Socket connectSocket = e.AcceptSocket;

            //send + receive
            byte[] buffer = new byte[m_bufferSize * 2];

            //비동기 버퍼 설정
            SocketAsyncEventArgs receiveSaea = new SocketAsyncEventArgs();
            SocketAsyncEventArgs sendSaea = new SocketAsyncEventArgs();

            var userToken = new UserToken();

            receiveSaea.SetBuffer(buffer, 0, m_bufferSize);
            receiveSaea.Completed += ReceiveComplete;
            receiveSaea.AcceptSocket = connectSocket;
            receiveSaea.UserToken = userToken;

            sendSaea.SetBuffer(buffer, m_bufferSize, m_bufferSize);
            sendSaea.Completed += SendComplete;
            sendSaea.UserToken = userToken;

            userToken.Init(connectSocket, sendSaea, receiveSaea);

            connectHandler?.Invoke(userToken);
            connectSocket.ReceiveAsync(receiveSaea);
        }
    }
}
