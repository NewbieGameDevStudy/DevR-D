﻿using ProtoBuf;
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
        /* 클라이언트 섹션 바뀌면 여기도 바꿔줘야함.
       public enum eQuestionType
       {
           Society,        //사회
           Entertainment,  //연예
           General,        //일반
           Common,         //상식
           History,        //역사
           Science,        //과학
           Sports,         //스포츠
           Animal,         //동물
       }*/
        public enum RoomType
        {
            SOCIETY,
            ENTERTAINMENT,
            GENERAL,
            COMMON,
            HISTORY,
            SCIENCE,
            SPORTS,
            ANIMAL
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

    [ProtoContract]
    public class PK_SC_CANNOT_MATCHING_GAME : PK_BASE
    {
        public enum MatchingErrorType
        {
            MAX_WAIT_TIME,
            TEST2,
            TEST3
        }

        [ProtoMember(1)]
        public MatchingErrorType type { get; set; }
    }

    [ProtoContract]
    public class PK_SC_MATCHING_ROOM_INFO : PK_BASE
    {
        
    }
}
