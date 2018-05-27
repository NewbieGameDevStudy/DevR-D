using GameServer.Connection;
using Packet;

namespace GameServer.ServerClient
{
    public partial class Client
    {
        void OnReceivePacket(PK_CS_ENTERROOM pks)
        {
            MatchRoom.RoomManager.RoomType eRoomType = (MatchRoom.RoomManager.RoomType)pks.type;
            m_baseServer.RoomManager.EnterWaitRoom(eRoomType, Player);
        }

        void onReceivePacket(PK_CS_CANCEL_MATCHING pks)
        {
            MatchRoom.RoomManager.RoomType eRoomType = (MatchRoom.RoomManager.RoomType)pks.type;
            m_baseServer.RoomManager.CancelMatching(eRoomType, Player);
        }

        void OnReceivePacket(PK_CS_INPUT_POSITION pks)
        {
            Player.Client.m_baseServer.RoomManager.AddMoveInput(new MatchRoom.RoomManager.MoveData {
                handle = Player.Handle,
                xPos = pks.xPos,
                yPos = pks.yPos
            });
        }
    }
}
