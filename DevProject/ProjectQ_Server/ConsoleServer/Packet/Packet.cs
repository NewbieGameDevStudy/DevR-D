using ProtoBuf;
using System.Collections.Generic;

namespace Packet
{
    public class PK_BASE
    {

    }

    //클라이언트 -> 서버 패킷
    [ProtoContract]
    public class PK_CS_ENTERROOM : PK_BASE                  // 매칭 입장
    {
        [ProtoMember(1)]
        public ulong AccountIDClient { get; set; }
    }

    [ProtoContract]
    public class PK_CS_CANCEL_MATCHING : PK_BASE            // 매칭 취소
    {
        [ProtoMember(1)]
        public ulong AccountIDClient { get; set; }
    }

    [ProtoContract]
    public class PK_CS_READY_COMPLETE_FOR_GAME : PK_BASE
    {
        [ProtoMember(1)]
        public byte RoomNo { get; set; }
        [ProtoMember(2)]
        public ulong AccountIDClient { get; set; }
    }

    [ProtoContract]
    public class PK_CS_MOVE_POSITION : PK_BASE
    {
        [ProtoMember(1)]
        public byte RoomNo { get; set; }
        [ProtoMember(2)]
        public ulong AccountIDClient { get; set; }
        [ProtoMember(3)]
        public float xPos { get; set; }
        [ProtoMember(4)]
        public float yPos { get; set; }
    }

    [ProtoContract]
    public class PK_CS_QUIZ_ANSWER : PK_BASE
    {
        public enum QuizAnswer
        {
            ANSWER_O,
            ANSWER_X
        };

        [ProtoMember(1)]
        public byte RoomNo { get; set; }
        [ProtoMember(2)]
        public QuizAnswer Answer { get; set; }
    }

    /*
    [ProtoContract]
    public class PK_CS_INPUT_POSITION : PK_BASE
    {
        [ProtoMember(1)]
        public float xPos { get; set; }
        [ProtoMember(2)]
        public float yPos { get; set; }
    }

    [ProtoContract]
    public class PK_CS_LOAD_PLAYERINFO : PK_BASE
    {
        [ProtoMember(1)]
        public ulong accountId { get; set; }
    }*/

    //서버 -> 클라이언트 패킷
    /*
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
    }*/

    [ProtoContract]
    public class PK_SC_CLIENT_SERVER_ID : PK_BASE
    {
        [ProtoMember(1)]
        public ulong AccountIDServer { get; set; }
    }

    [ProtoContract]
    public class PK_SC_CANNOT_MATCHING_GAME : PK_BASE           // 매칭 캔슬
    {
        public enum MatchingErrorType
        {
            PLAYER_OVERLAPPED,                  // 중복 매칭 시도
            MAX_WAIT_TIME,                      // 대기 시간 초과 (현재 2명이므로 1명이서 초과시간 대기)
            CANCEL_WAIT,                        // 대기실에서 방 찾는중 취소
            CANCEL_ROOM                         // 방에 입장후 경기 시작전 취소
        }

        [ProtoMember(1)]
        public MatchingErrorType type { get; set; }
        [ProtoMember(2)]
        public ulong AccountIDClient { get; set; }
    }

    [ProtoContract]
    public class PK_SC_READY_FOR_GAME : PK_BASE                 // 게임 시작 준비
    {
        [ProtoMember(1)]
        public int GameUserCount { get; set; }
        [ProtoMember(2)]
        public byte RoomNo { get; set; }
    }

    [ProtoContract]
    public class PK_SC_MATCHING_MEMBER_INFO : PK_BASE           // 게임 멤버 기본 정보
    {
        [ProtoMember(1)]
        public ulong AccountIDClient { get; set; }
        [ProtoMember(2)]
        public ulong AccountIDServer { get; set; }
        [ProtoMember(3)]
        public string NickName { get; set; }
        [ProtoMember(4)]
        public int PortRaitNo { get; set; }
    }

    [ProtoContract]
    public class PK_SC_MATCHING_ROOM_INFO : PK_BASE             // 게임 멤버 기본 정보 리스트
    {
        [ProtoMember(1)]
        public List<PK_SC_MATCHING_MEMBER_INFO> m_memberList { get; set; }
    }

    [ProtoContract]
    public class PK_SC_MOVE_POSITION : PK_BASE
    {
        [ProtoMember(1)]
        public byte RoomNo { get; set; }
        [ProtoMember(2)]
        public ulong AccountIDClient { get; set; }
        [ProtoMember(3)]
        public float xPos { get; set; }
        [ProtoMember(4)]
        public float yPos { get; set; }
    }

    [ProtoContract]
    public class PK_SC_QUIZ_TEXT : PK_BASE
    {
        [ProtoMember(1)]
        public string strQuiz { get; set; }
    }

    [ProtoContract]
    public class PK_SC_QUIZ_MOVE_END_TIME : PK_BASE
    {
        [ProtoMember(1)]
        public byte QuizEndTimeDelay { get; set; }
    }

    [ProtoContract]
    public class PK_SC_QUIZ_RESULT : PK_BASE
    {
        public enum QuizResult
        {
            ALIVE,
            DEAD
        };

        [ProtoMember(1)]
        public Dictionary<ulong, QuizResult> MemberQuizResult { get; set; }
    }

    [ProtoContract]
    public class PK_SC_GAME_END : PK_BASE
    {
        [ProtoMember(1)]
        public byte Rank { get; set; }
    }
}
