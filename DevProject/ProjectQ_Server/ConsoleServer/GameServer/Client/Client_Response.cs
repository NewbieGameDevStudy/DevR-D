using GameServer.Connection;
using Packet;

namespace GameServer.ServerClient
{
    public partial class Client
    {
        void OnReceivePacket(PK_CS_ENTERROOM pks)
        {
            switch (pks.type) { 
                case PK_CS_ENTERROOM.RoomType.COMMON_SENSE:
                    m_baseServer.RoomManager.EnterWaitRoom(MatchRoom.RoomManager.RoomType.COMMON_SENSE, Player);
                    break;

                case PK_CS_ENTERROOM.RoomType.GAME:
                    m_baseServer.RoomManager.EnterWaitRoom(MatchRoom.RoomManager.RoomType.GAME, Player);
                    break;

                case PK_CS_ENTERROOM.RoomType.SOCIAL:
                    m_baseServer.RoomManager.EnterWaitRoom(MatchRoom.RoomManager.RoomType.SOCIAL, Player);
                    break;
            }
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
