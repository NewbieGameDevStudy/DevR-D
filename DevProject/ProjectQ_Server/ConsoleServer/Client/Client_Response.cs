using Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClient {
    public partial class Client {

        void OnReceivePacket(PK_TEST pks) {
            int a = 0;
        }

        void OnReceivePacket(PK_TEST2 pks) {
            int a = 0;
        }

        void OnReceivePacket(PK_CS_PING pks) {
            SendPacket(new PK_SC_PING {
                receiveId = pks.clientAccountId,
                str = "Send Server -> Client",
            });
        }
    }
}
