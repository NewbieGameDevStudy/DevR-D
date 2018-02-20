using Packet;

namespace BaseClient
{
    public partial class Client
    {
        public void ReqInputTargetMovePos(float xPos, float yPos)
        {
            Player.Client.SendPacket(new PK_CS_INPUT_POSITION {
                xPos = xPos,
                yPos = yPos,
            });
        }
    }
}
