using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packet {
    public class PK_BASE {

    }

    [ProtoContract]
    public class PK_TEST : PK_BASE {
        [ProtoMember(1)]
        public int Id { get; set; }
    }

    [ProtoContract]
    public class PK_TEST2 : PK_BASE {
        [ProtoMember(1)]
        public int Id { get; set; }
    }

    //클라이언트 -> 서버 패킷
    [ProtoContract]
    public class PK_CS_PING : PK_BASE {
        [ProtoMember(1)]
        public int clientAccountId { get; set; }
    }

    [ProtoContract]
    public class PK_SC_PING : PK_BASE {
        [ProtoMember(1)]
        public int receiveId { get; set; }

        [ProtoMember(2)]
        public string str { get; set; }
    }

    //서버 -> 클라이언트 패킷
}
