using Packet;

namespace GameServer.ServerClient
{
    public partial class Client
    {
        void OnReceivePacket(PK_CS_CLIENT_ACCOUNT pks)
        {
            if (Player == null)
                return;

            Player.WebAccountId = pks.accountId;
        }

        void OnReceivePacket(PK_CS_ENTERROOM pks)
        {
            if (Player == null)
                return;

            m_baseServer.RoomManager.EnterWaitRoom(Player);
        }

        void OnReceivePacket(PK_CS_CANCEL_MATCHING pks)
        {
            if (Player.WebAccountId != pks.accountId)
                return;
            
            m_baseServer.RoomManager.CancelMatching(Player);
        }

        void OnReceivePacket(PK_CS_READY_COMPLETE_FOR_GAME pks)
        {
            if (Player.WebAccountId != pks.accountId)
                return;

            m_baseServer.RoomManager.ReadyForGame(pks.RoomNo, Player);
        }

        void OnReceivePacket(PK_CS_MOVE_POSITION pks)
        {
            if (Player.WebAccountId != pks.accountId)
                return;

            Player.PlayerData.Xpos = pks.xPos;
            Player.PlayerData.Ypos = pks.yPos;

            m_baseServer.RoomManager.MovePosition(pks.RoomNo, Player);
        }

        void OnReceivePacket(PK_CS_QUIZ_ANSWER pks)
        {
            m_baseServer.RoomManager.PlayerAnswerReceive(pks.RoomNo, pks.Answer, Player);
            m_baseServer.RoomManager.ReadyForGame(pks.RoomNo, Player);
        }

        /*
        void OnReceivePacket(PK_CS_INPUT_POSITION pks)
        {
            
            Player.Client.m_baseServer.RoomManager.AddMoveInput(new MatchRoom.RoomManager.MoveData {
                handle = Player.Handle,
                xPos = pks.xPos,
                yPos = pks.yPos
            });
        }*/
    }
}
