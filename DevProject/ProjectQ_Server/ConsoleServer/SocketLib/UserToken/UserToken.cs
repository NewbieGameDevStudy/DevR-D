using Packet;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace NetworkSocket
{
    public class UserToken
    {
        SocketData m_socketData;
        Socket m_socket;
        public SocketAsyncEventArgs SendSaea { get; private set; }
        public SocketAsyncEventArgs ReceiveSaea { get; private set; }

        readonly Queue<byte[]> m_sendQueue = new Queue<byte[]>();
        readonly Queue<SocketData> m_receiveQueue = new Queue<SocketData>();

        public Action<int, object[]> ReceiveDispatch;

        public UserToken()
        {
            m_socketData = new SocketData();
        }

        public void Init(Socket socket, SocketAsyncEventArgs sendArgs, SocketAsyncEventArgs receiveArgs)
        {
            m_socket = socket;
            SendSaea = sendArgs;
            ReceiveSaea = receiveArgs;
        }

        public void OnReceive(byte[] buffer, int offset, int totalBytes)
        {
            if (m_socketData.ReceiveBuffer(buffer, offset, totalBytes) == false)
                return;
            lock (m_receiveQueue) {
                //TODO : Clone 부하는??
                m_receiveQueue.Enqueue(m_socketData.Clone() as SocketData);
            }
        }

        public void ReceiveProcess()
        {
            Queue<SocketData> receiveTemp = null;
            lock (m_receiveQueue) {
                if (m_receiveQueue.Count <= 0)
                    return;

                receiveTemp = new Queue<SocketData>(m_receiveQueue);
                m_receiveQueue.Clear();
            }

            if (receiveTemp == null)
                return;

            foreach (var data in receiveTemp) {
                var pks = PacketParser.Deserializer_Parser(data.PacketId, data.Ms);
                if (pks == null) {
                    Console.WriteLine("패킷오류");
                    continue;
                }
                ReceiveDispatch?.Invoke(data.PacketId, new object[] { pks });
            }
        }

        public void OnSend(PK_BASE pks)
        {
            //TODO : 리펙토링이 필요함
            var sendBuffers = m_socketData.GetSendBuffer(pks);

            lock (m_sendQueue) {
                if(m_sendQueue.Count <= 0) {
                    foreach (var sendBuffer in sendBuffers)
                        m_sendQueue.Enqueue(sendBuffer);
                    SendProcess();
                    return;
                }

                foreach (var sendBuffer in sendBuffers)
                    m_sendQueue.Enqueue(sendBuffer);
            }

            //var sendBuffer = m_socketData.SendBuffer(pks);


            //lock (m_sendQueue) {
            //    if (m_sendQueue.Count <= 0) {
            //        m_sendQueue.Enqueue(sendBuffer);
            //        SendProcess();
            //        return;
            //    }
            //    m_sendQueue.Enqueue(sendBuffer);
            //}
        }

        private void SendProcess()
        {
            var sendPacket = m_sendQueue.Peek();
            Array.Copy(sendPacket, 0, SendSaea.Buffer, SendSaea.Offset, sendPacket.Length);
            m_socket.SendAsync(SendSaea);
        }

        public void SendDequeue()
        {
            lock (m_sendQueue) {
                m_sendQueue.Dequeue();

                if (m_sendQueue.Count > 0) {
                    SendProcess();
                }
            }
        }
    }
}
