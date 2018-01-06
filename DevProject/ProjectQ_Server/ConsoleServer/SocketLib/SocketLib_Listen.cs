﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketLib {
    public partial class SocketLib {

        object listenSocket = new object();
        BufferManager bufferManager;
        SocketAsyncEventArgsPool saepRecvPool;
        SocketAsyncEventArgsPool saepSendPool;

        int maxConnection = 1000;

        public void InitListen() {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            localEndPoint = new IPEndPoint(ipAddress, 5050);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 2 : send + recv
            bufferManager = new BufferManager(maxConnection * bufferSize * 2, bufferSize);
            saepRecvPool = new SocketAsyncEventArgsPool(maxConnection);
            saepSendPool = new SocketAsyncEventArgsPool(maxConnection);

            //recv, send 각각 초기화
            for (int i = 0; i < maxConnection; ++i) {
                SocketAsyncEventArgs recvArgs = new SocketAsyncEventArgs();
                //recvArgs.Completed = 
                bufferManager.SetBuffer(recvArgs);
                saepRecvPool.Push(recvArgs);
            }

            for (int i = 0; i < maxConnection; ++i) {
                SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();
                //sendArgs.Completed = 
                bufferManager.SetBuffer(sendArgs);
                saepSendPool.Push(sendArgs);
            }
        }

        public void StartListen() {
            try {
                socket.Bind(localEndPoint);
                socket.Listen(10);

                Console.WriteLine("Waiting for a connection...");

                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += AcceptComplete;
                socket.AcceptAsync(args);
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        void AcceptComplete(object sender, SocketAsyncEventArgs e) {
            Socket acceptSocket = e.AcceptSocket;
            acceptHandler?.Invoke(acceptSocket);

            SocketAsyncEventArgs receiveEventArgs = saepRecvPool.Pop();
            acceptSocket.ReceiveAsync(receiveEventArgs);

            e.AcceptSocket = null;

            lock (listenSocket) {
                socket.AcceptAsync(e);
            }
        }
    }
}
