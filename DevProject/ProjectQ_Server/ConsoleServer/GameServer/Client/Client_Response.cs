﻿using Packet;

namespace GameServer.ServerClient
{
    public partial class Client
    {
        void OnReceivePacket(PK_CS_ENTERROOM pks)
        {
            if (Player == null)
                return;

            Player.UserSequence = pks.userSequence;
            m_baseServer.RoomManager.EnterWaitRoom(Player);
        }

        void OnReceivePacket(PK_CS_LONGPACKET_TEST pks)
        {
            if (Player == null)
                return;

            var d = pks.longData.Length;
            int a = 0;
        }

        void onReceivePacket(PK_CS_CANCEL_MATCHING pks)
        {
            if (Player.UserSequence != pks.userSequence)
                return;

            m_baseServer.RoomManager.CancelMatching(Player);
        }

        void onReceivePacket(PK_CS_READY_COMPLETE_FOR_GAME pks)
        {
            if (Player.UserSequence != pks.userSequence)
                return;

            //pks.roomNo;
            //pks.userSequence;
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
