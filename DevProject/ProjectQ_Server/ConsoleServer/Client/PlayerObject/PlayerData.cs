using GameObject;
using System;
using System.Collections.Generic;

namespace Player
{
    public class PlayerData : IPlayerInfo
    {
        public float Xpos { get; private set; }
        public float Ypos { get; private set; }
        public int Level { get; private set; }
        public int Exp { get; private set; }

        public PlayerData(int level, int exp)
        {
            Level = level;
            Exp = exp;
        }

        public void Update(double deltaTime)
        {

        }

        public void SetTargetPosition(float xPos, float yPos)
        {
            Xpos = xPos;
            Ypos = yPos;
        }
    }
}
