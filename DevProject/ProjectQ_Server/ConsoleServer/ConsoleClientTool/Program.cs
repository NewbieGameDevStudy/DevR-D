﻿using System;
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
            // Connect to a remote device.
            try {
                // Establish the remote endpoint for the socket.
                // The name of the 
                // remote device is "host.contoso.com".
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                var localEndPoint = new IPEndPoint(ipAddress, 5050);

                // Create a TCP/IP socket.
                Socket client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.
                client.BeginConnect(localEndPoint,
                    new AsyncCallback(ConnectCallback), client);

                Console.ReadLine();

                int a = 3;
                int b = 0;
                string c = "abc";

                var buffer = BitConverter.GetBytes(a);
                var d = BitConverter.GetBytes(b);
                var cc = Encoding.UTF8.GetBytes(c);
                var ff = new byte[buffer.Length + d.Length + cc.Length];

                Array.Copy(buffer, 0, ff, 0, buffer.Length);
                Array.Copy(d, 0, ff, buffer.Length, d.Length);
                Array.Copy(cc, 0, ff, buffer.Length + d.Length, cc.Length);

                client.Send(ff, 0, ff.Length, SocketFlags.None);

                Console.ReadLine();
                // Release the socket.
                client.Shutdown(SocketShutdown.Both);
                client.Close();

            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ConnectCallback(IAsyncResult ar) {
            try {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }
    }

}
