using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClientTool {
    class Program {
        static void Main(string[] args) {
            StartClient();
            Console.ReadLine();
        }

        private static void StartClient() {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            var localEndPoint = new IPEndPoint(ipAddress, 5050);
            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.RemoteEndPoint = localEndPoint;

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(localEndPoint);

            SocketAsyncEventArgs reciveAsync = new SocketAsyncEventArgs();
            reciveAsync.Completed += reciveAsync_Completed;
            reciveAsync.SetBuffer(new byte[1024], 0, 1024);
            reciveAsync.UserToken = socket;
            socket.ReceiveAsync(reciveAsync);

        }


        private static void reciveAsync_Completed(object sender, SocketAsyncEventArgs e) {
            Socket client = (Socket)sender;
            if (client.Connected && e.BytesTransferred > 0) {
                byte[] lengthByte = e.Buffer;
                int length = BitConverter.ToInt32(lengthByte, 0);
                byte[] data = new byte[length];
                client.Receive(data, length, SocketFlags.None);
                StringBuilder sb = new StringBuilder();
                //sb.Append(m_Name);
                //sb.Append(" - ");
                //sb.Append(Encoding.Unicode.GetString(data)); Console.WriteLine(sb.ToString());
            }

            client.ReceiveAsync(e);

        }
    }

}
