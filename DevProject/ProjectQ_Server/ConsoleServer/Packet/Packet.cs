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
}
