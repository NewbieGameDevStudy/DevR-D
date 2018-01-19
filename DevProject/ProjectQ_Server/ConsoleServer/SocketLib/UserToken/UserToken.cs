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
        readonly Queue<SocketData> receiveQueue = new Queue<SocketData>();

        public Action<int, object[]> ReceiveDispatch;

        public UserToken() {
            socketData = new SocketData();
        }

        public void Init(Socket socket, SocketAsyncEventArgs sendArgs, SocketAsyncEventArgs receiveArgs) {
            this.socket = socket;
            sendSaea = sendArgs;
            receiveSaea = receiveArgs;
        }

        public void OnReceive(byte[] buffer, int offset, int totalBytes) {
            socketData.ReceiveBuffer(buffer, offset, totalBytes);
            lock (receiveQueue) {
                //TODO : Clone 부하는??
                receiveQueue.Enqueue(socketData.Clone() as SocketData);
            }
        }

        public void ReceiveProcess() {
            Queue<SocketData> receiveTemp = null;
            lock (receiveQueue) {
                if (receiveQueue.Count <= 0)
                    return;

                receiveTemp = new Queue<SocketData>(receiveQueue);
                receiveQueue.Clear();
            }

            if (receiveTemp == null)
                return;

            foreach(var data in receiveTemp) {
                var pks = PacketParser.Deserializer_Parser(data.PacketId, data.Ms);
                if (pks == null) {
                    Console.WriteLine("패킷오류");
                    continue;
                }
                ReceiveDispatch?.Invoke(data.PacketId, new object[] { pks });
            }
        }

        public void OnSend(PK_BASE pks) {
            //TODO : 리펙토링이 필요함
            var sendBuffer = socketData.SendBuffer(pks);
            lock (sendQueue) {
                if (sendQueue.Count <= 0) {
                    sendQueue.Enqueue(sendBuffer);
                    SendProcess();
                    return;
                }

                sendQueue.Enqueue(sendBuffer);
            }
        }

        private void SendProcess() {
            var sendPacket = sendQueue.Peek();
            Array.Copy(sendPacket, 0, sendSaea.Buffer, sendSaea.Offset, sendPacket.Length);
            socket.SendAsync(sendSaea);
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
