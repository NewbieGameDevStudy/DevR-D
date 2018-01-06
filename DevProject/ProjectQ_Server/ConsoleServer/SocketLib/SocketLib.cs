using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketLib {
    public partial class SocketLib {
        Socket socket;

        IPEndPoint localEndPoint;
        int bufferSize = 1024;

        public delegate void AcceptHandler(Socket acceptSocket);
        public delegate void ReceiveHandler();
        public AcceptHandler acceptHandler;


        #region Receive 함수
        void ReceiveComplete(object sender, SocketAsyncEventArgs e) {
            if (e.BytesTransferred > 0 
                && e.SocketError == SocketError.Success
                && e.LastOperation == SocketAsyncOperation.Receive) {

                RecevieByteProcess(e);
            }
        }

        void RecevieByteProcess(SocketAsyncEventArgs e) {
            var totalBytes = e.BytesTransferred;
            if (totalBytes < sizeof(int)) {
                //여기서 다시 receive를 호출해야할듯함.
                return;
            }

            int offset = 0;
            while (true) {
                if(offset == 0) {
                    
                }
            }
        }

        #endregion
        void SendComplete(object sender, SocketAsyncEventArgs e) {
            if (e.SocketError == SocketError.Success
                && e.LastOperation == SocketAsyncOperation.Send
                ) {

            }
        }

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
