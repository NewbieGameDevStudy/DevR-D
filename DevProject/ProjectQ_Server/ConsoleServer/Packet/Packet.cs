using ProtoBuf;
using System.Collections.Generic;

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

    [ProtoContract]
    public class PK_CS_INPUT_POSITION : PK_BASE
    {
        [ProtoMember(1)]
        public float xPos { get; set; }
        [ProtoMember(2)]
        public float yPos { get; set; }
    }

    //서버 -> 클라이언트 패킷

    [ProtoContract]
    public class PK_SC_PLAYERINFO_LOAD : PK_BASE
    {
        [ProtoMember(1)]
        public int handle { get; set; }
        [ProtoMember(2)]
        public int Level { get; set; }
        [ProtoMember(3)]
        public int Exp { get; set; }
    }

    [ProtoContract]
    public class PK_SC_TARGET_POSITION : PK_BASE
    {
        [ProtoMember(1)]
        public int handle { get; set; }
        [ProtoMember(2)]
        public float xPos { get; set; }
        [ProtoMember(3)]
        public float yPos { get; set; }
    }

    [ProtoContract]
    public class PK_SC_OBJECT_INFO : PK_BASE
    {
        [ProtoMember(1)]
        public int handle { get; set; }
        [ProtoMember(2)]
        public PK_SC_PLAYERINFO_LOAD info {get; set;}
        [ProtoMember(3)]
        public PK_SC_TARGET_POSITION position { get; set; }
    }

    [ProtoContract]
    public class PK_SC_OBJECTS_INFO : PK_BASE
    {
        [ProtoMember(1)]
        public List<PK_SC_OBJECT_INFO> m_objectList { get; set; }
    }

    [ProtoContract]
    public class PK_SC_OBJECTS_POSITION : PK_BASE
    {
        [ProtoMember(1)]
        public List<PK_SC_TARGET_POSITION> m_objectList { get; set; }
    }
}
