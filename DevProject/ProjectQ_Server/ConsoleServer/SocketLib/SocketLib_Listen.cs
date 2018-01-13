using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSocket {
    public partial class SocketLib {

        object listenSocket = new object();
        BufferManager bufferManager;
        SocketAsyncEventArgsPool saeapRecvPool;
        SocketAsyncEventArgsPool saeapSendPool;

        int maxConnection = 1000;

        public void InitServer(int port) {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            localEndPoint = new IPEndPoint(ipAddress, port);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 2 : send + recv
            bufferManager = new BufferManager(maxConnection * bufferSize * 2, bufferSize);
            bufferManager.InitBuffer();

            saeapRecvPool = new SocketAsyncEventArgsPool(maxConnection);
            saeapSendPool = new SocketAsyncEventArgsPool(maxConnection);

            //recv, send 각각 초기화
            for (int i = 0; i < maxConnection; ++i) {
                var userToken = new UserToken();

                SocketAsyncEventArgs recvArgs = new SocketAsyncEventArgs();
                recvArgs.Completed += ReceiveComplete;
                recvArgs.UserToken = userToken;
                bufferManager.SetBuffer(recvArgs);
                saeapRecvPool.Push(recvArgs);

                SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();
                sendArgs.Completed += SendComplete;
                sendArgs.UserToken = userToken;
                bufferManager.SetBuffer(sendArgs);
                saeapSendPool.Push(sendArgs);
            }
        }

        public void StartListen() {
            try {
                socket.Bind(localEndPoint);
                socket.Listen(10);

                Console.WriteLine("GameServer...");

                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += AcceptComplete;
                socket.AcceptAsync(args);
                Console.ReadLine();
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        void AcceptComplete(object sender, SocketAsyncEventArgs e) {
            Socket acceptSocket = e.AcceptSocket;
            SocketAsyncEventArgs receiveSaea = saeapRecvPool.Pop();
            SocketAsyncEventArgs sendSaea = saeapSendPool.Pop();

            var userToken = receiveSaea.UserToken as UserToken;
            userToken.Init(acceptSocket, sendSaea, receiveSaea);

            acceptHandler?.Invoke(userToken);

            receiveSaea.AcceptSocket = acceptSocket;
            acceptSocket.ReceiveAsync(receiveSaea);

            e.AcceptSocket = null;

            lock (listenSocket) {
                socket.AcceptAsync(e);
            }
        }
    }
}
