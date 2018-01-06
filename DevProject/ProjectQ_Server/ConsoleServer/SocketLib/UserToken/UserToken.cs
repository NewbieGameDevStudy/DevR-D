using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketLib {
    public class UserToken {
        SocketData socketData;

        public UserToken() {
            socketData = new SocketData();
        }

        public void OnReceive(byte[] buffer, int totalBytes) {
            socketData.ReceiveBuffer(buffer, totalBytes);
        }
    }
}
