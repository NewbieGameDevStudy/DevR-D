using Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSocket {
    public partial class SocketLib {
        Socket socket;

        IPEndPoint localEndPoint;
        int bufferSize = 1024;

        public delegate void AcceptHandler(Socket acceptSocket);
        public delegate void ReceiveHandler();
        public AcceptHandler acceptHandler;

        void InitiInteranl() {

        }

        #region Receive 함수
        void ReceiveComplete(object sender, SocketAsyncEventArgs e) {
            if (e.BytesTransferred > 0 
                && e.SocketError == SocketError.Success
                && e.LastOperation == SocketAsyncOperation.Receive) {

                var userToken = e.UserToken as UserToken;
                userToken.OnBufferOffset(e.Buffer, e.Offset, e.Count);
                userToken.OnReceive(e.BytesTransferred);

                e.AcceptSocket.ReceiveAsync(e);

            } else {
                //TODO : 종료처리를 해야한다.
            }
        }

        #endregion

        #region Send 함수

        void SendComplete(object sender, SocketAsyncEventArgs e) {
            if (e.SocketError == SocketError.Success
                && e.LastOperation == SocketAsyncOperation.Send) {
                var userToken = e.UserToken as UserToken;
                userToken.SendDequeue();
            }
        }

        #endregion

        void CloseSocket(Socket socket) {
            if (socket == null || !socket.Connected)
                return;

            try {
                socket.Shutdown(SocketShutdown.Both);
                socket.Disconnect(true);
            }
            catch (Exception e) {

            }
            finally {
                if (socket.Connected)
                    socket.Close();
            }
        }
    }
}
