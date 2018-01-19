using NetworkSocket;
using Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClient {
    public partial class Client {
        UserToken userToken;
        int accountId;

        public Action<int, object, object[]> PacketDispatch;

        public Client(UserToken userToken, int accountValue) {
            accountId = accountValue;
            this.userToken = userToken;
            userToken.ReceiveDispatch = this.ReceiveDispatch;
        }

        int a = 0;
        public void Update() {
            userToken.ReceiveProcess();

            a++;
            if (a == 1000) {
                SendPacket(new PK_SC_PING { receiveId = accountId, str = "DD" });
                a = 0;
            }
        }

        public void ReceiveDispatch(int packetId, object[] parameters) {
            PacketDispatch?.Invoke(packetId, this, parameters);
        }

        public void SendPacket(PK_BASE pks) {
            userToken.OnSend(pks);
        }
    }
}
