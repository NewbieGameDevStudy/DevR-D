using ProtoBuf;

namespace Packet
{
    public class PK_BASE
    {

    }

    //클라이언트 -> 서버 패킷
    [ProtoContract]
    public class PK_CS_ENTERROOM : PK_BASE
    {
        public enum RoomType
        {
            COMMON_SENSE,
            SOCIAL,
            GAME
        }

        [ProtoMember(1)]
        public RoomType type { get; set; }
    }

    //서버 -> 클라이언트 패킷

    [ProtoContract]
    public class PK_SC_PLAYERINFO_LOAD : PK_BASE
    {
        [ProtoMember(1)]
        public int Level { get; set; }

        [ProtoMember(2)]
        public int Exp { get; set; }
    }
}
