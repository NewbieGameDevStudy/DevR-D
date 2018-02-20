using GameServer.Connection;
using Packet;

namespace GameServer.ServerClient
{
    public partial class Client
    {
        void OnReceivePacket(PK_CS_ENTERROOM pks)
        {
            /*
            var testDTO = new TestDTO();
            testDTO.a = 123123;
            m_httpConnection.HttpConnectAsync(testDTO, (result) => {
                var dffd = result;
            });*/
            
            var testPOST = new TestDTO_Post();
            testPOST.userId = 11;
            testPOST.strNick = "testID";
            m_httpConnection.HttpConnectAsync(testPOST, (result) => {
                var dffd = result;
            });

            switch (pks.type) {
                case PK_CS_ENTERROOM.RoomType.COMMON_SENSE:
                    m_baseServer.RoomManager.EnterRoom(MatchRoom.RoomManager.RoomType.COMMON_SENSE, Player);
                    break;

                case PK_CS_ENTERROOM.RoomType.GAME:
                    m_baseServer.RoomManager.EnterRoom(MatchRoom.RoomManager.RoomType.GAME, Player);
                    break;

                case PK_CS_ENTERROOM.RoomType.SOCIAL:
                    m_baseServer.RoomManager.EnterRoom(MatchRoom.RoomManager.RoomType.SOCIAL, Player);
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
