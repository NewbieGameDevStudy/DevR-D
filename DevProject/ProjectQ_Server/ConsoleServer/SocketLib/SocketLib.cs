using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketLib {
    public class SocketLib {
        Socket socket;
        IPEndPoint localEndPoint;
        BufferManager bufferManager;
        SocketAsyncEventArgsPool saepPool;
        int maxConnection = 1000;
        int bufferSize = 1024;

        public delegate void AcceptHandler(Socket acceptSocket);
        public AcceptHandler acceptHandler;

        public void InitListen() {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            localEndPoint = new IPEndPoint(ipAddress, 5050);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void StartListen() {
            try {
                socket.Bind(localEndPoint);
                socket.Listen(10);

                // 2 : send + recv
                bufferManager = new BufferManager(maxConnection * bufferSize * 2, bufferSize);
                saepPool = new SocketAsyncEventArgsPool(maxConnection);

                Console.WriteLine("Waiting for a connection...");

                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += AcceptCallback;
                socket.AcceptAsync(args);
            }
            catch(Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        private void AcceptCallback(object sender, SocketAsyncEventArgs e) {
            Socket acceptSocket = e.AcceptSocket;


            acceptHandler?.Invoke(acceptSocket);

            e.AcceptSocket = null;
            socket.AcceptAsync(e);
        }
        
    }
}
