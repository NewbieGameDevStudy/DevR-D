using ProtoBuf;

namespace Packet
{
    public class PK_BASE
    {

    }

    //클라이언트 -> 서버 패킷
    [ProtoContract]
    public class PK_CS_PING : PK_BASE
    {
        [ProtoMember(1)]
        public int clientAccountId { get; set; }
    }

    [ProtoContract]
    public class PK_SC_PLAYERINFO_LOAD : PK_BASE
    {
        [ProtoMember(1)]
        public int Level { get; set; }

        [ProtoMember(2)]
        public int Exp { get; set; }
    }

    //서버 -> 클라이언트 패킷
}
