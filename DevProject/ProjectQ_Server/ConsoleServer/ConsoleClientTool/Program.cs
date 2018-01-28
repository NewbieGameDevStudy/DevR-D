using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleClientTool {
    class Program {
        static void Main(string[] args) {

            int a = 0;
            while (true) {
                Thread.Sleep(10);
                Thread d = new Thread(StartClient);
                d.Start();
                a++;
                if (a == 1)
                    break;
            }
            Console.ReadLine();
        }

        private static void StartClient() {
            var localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5050);
            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.RemoteEndPoint = localEndPoint;

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(localEndPoint);

            SocketAsyncEventArgs receiveAsync = new SocketAsyncEventArgs();
            receiveAsync.Completed += receiveAsync_Completed;
            receiveAsync.SetBuffer(new byte[1024], 0, 1024);
            receiveAsync.UserToken = socket;
            socket.ReceiveAsync(receiveAsync);

        }


        private static void receiveAsync_Completed(object sender, SocketAsyncEventArgs e) {
            Socket client = (Socket)sender;
            if (client.Connected && e.BytesTransferred > 0) {
                byte[] lengthByte = e.Buffer;
                //int length = BitConverter.ToInt32(lengthByte, 0);
                //byte[] data = new byte[length];
                //client.Receive(data, length, SocketFlags.None);

                SocketAsyncEventArgs sendAsync = new SocketAsyncEventArgs();
                var msg = BitConverter.GetBytes(10);
                var id = BitConverter.GetBytes(2);
                var blength = BitConverter.GetBytes(msg.Length + id.Length);
                byte[] buffer = new byte[blength.Length + msg.Length + id.Length];

                Array.Copy(blength, 0, buffer, 0, blength.Length);
                Array.Copy(id, 0, buffer, blength.Length, id.Length);
                Array.Copy(msg, 0, buffer, blength.Length + id.Length, msg.Length);

                sendAsync.SetBuffer(buffer, 0, buffer.Length);
                client.SendAsync(sendAsync);
            }

            client.ReceiveAsync(e);

        }
    }

}
