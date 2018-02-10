using System;
using System.Net.Sockets;

namespace NetworkSocket
{
    public partial class SocketLib
    {
        #region Receive 함수
        void ReceiveComplete(object sender, SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred > 0
                && e.SocketError == SocketError.Success
                && e.LastOperation == SocketAsyncOperation.Receive) {

                var userToken = e.UserToken as UserToken;
                userToken.OnReceive(e.Buffer, e.Offset, e.BytesTransferred);

                e.AcceptSocket.ReceiveAsync(e);
            } else {
                CloseSocket(e.AcceptSocket);
            }
        }

        #endregion

        #region Send 함수

        void SendComplete(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success
                && e.LastOperation == SocketAsyncOperation.Send) {
                var userToken = e.UserToken as UserToken;
                userToken.SendDequeue();
            }
        }

        #endregion

        void CloseSocket(Socket socket)
        {
            if (socket == null || !socket.Connected)
                return;

            try {
                socket.Shutdown(SocketShutdown.Both);
                socket.Disconnect(true);
            } catch (Exception e) {
                Console.WriteLine(e);
            } finally {
                if (socket.Connected)
                    socket.Close();
            }
        }
    }
}
