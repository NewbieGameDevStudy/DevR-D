using Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSocket {
    public class UserToken {
        SocketData socketData;
        Socket socket;
        SocketAsyncEventArgs sendSaea;
        SocketAsyncEventArgs receiveSaea;

        readonly Queue<byte[]> sendQueue = new Queue<byte[]>();

        public UserToken() {
            socketData = new SocketData();
        }

        public void Init(Socket socket, SocketAsyncEventArgs sendArgs, SocketAsyncEventArgs receiveArgs) {
            this.socket = socket;
            sendSaea = sendArgs;
            receiveSaea = receiveArgs;
        }

        public void OnBufferOffset(byte[] buffer, int offset, int count) {
            socketData.OffSetBuffer(buffer, offset, count);
        }

        public void OnReceive(int totalBytes) {
            socketData.ReceiveBuffer(totalBytes);

            //패킷에 대한 처리를 하면될듯한데..
        }

        public void OnSend(SocketData data) {
            if (data == null)
                return;

            int packetId = data.PacketId;
            var length = (int)data.MsLength;

            byte[] buffer = null;
            byte[] headerByte = PacketParser.Serializer_ConvertByte(length, packetId);

            //buffer = new byte[headerByte.Length + length];
            //Buffer.BlockCopy(headerByte, 0, buffer, 0, headerByte.Length);
            buffer = new byte[headerByte.Length];
            Array.Copy(headerByte, 0, buffer, 0, headerByte.Length);
            data.ReadBuffer(buffer);

            lock (sendQueue) {
                if (sendQueue.Count <= 0) {
                    sendQueue.Enqueue(buffer);
                    SendProcess();
                    return;
                }

                sendQueue.Enqueue(buffer);
            }
        }

        private void SendProcess() {
            lock (sendQueue) {
                var sendPacket = sendQueue.Peek();
                Array.Copy(sendPacket, 0, sendSaea.Buffer, 0, sendPacket.Length);
                socket.SendAsync(sendSaea);
            }
        }

        public void SendDequeue() {
            lock (sendQueue) {
                sendQueue.Dequeue();

                if (sendQueue.Count > 0) {
                    SendProcess();
                }
            }
        }
    }
}
