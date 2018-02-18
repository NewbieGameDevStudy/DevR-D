using GameObject;
using System;
using System.Collections.Generic;
using Util;

namespace Player
{
    public class PlayerData : IPlayerInfo
    {
        Vec2 m_currentPos;
        Vec2 m_targetPos;
        Vec2 m_dirNormalVec;

        public double Xpos => m_currentPos.X;
        public double Ypos => m_currentPos.Y;

        public int Level { get; private set; }
        public int Exp { get; private set; }

        int m_speed = 200;
        float m_linearValue;

        public PlayerData(int level, int exp)
        {
            Level = level;
            Exp = exp;
        }

        public void SetCurrentPosition(Vec2 position)
        {
            m_currentPos = position;
        }

        public void Update(double deltaTime)
        {
            if (m_targetPos == Vec2.zero)
                return;

            UpdateMovePos(deltaTime);

            //LinearInterpolation(deltaTime);
        }

        public void SetTargetPosition(double xPos, double yPos)
        {
            m_targetPos = new Vec2(xPos, yPos);

            var dirVec = m_targetPos - m_currentPos;
            m_dirNormalVec = dirVec.Normalize();
        }

        private void UpdateMovePos(double deltaTime)
        {
            //이동할 좌표를 구하고
            var xValue = m_dirNormalVec.X * m_speed * deltaTime;
            var yValue = m_dirNormalVec.Y * m_speed * deltaTime;

            var movePos = new Vec2(Xpos + xValue, Ypos + yValue);

            //목표좌표와 이동할 좌표의 방향벡터를 구하고
            var dirNormalVec = (m_targetPos - movePos).Normalize();

            //그 방향 벡터와 플레이어가 가지고 있는 방향벡터를 비교해서 다르면 지나간 방향
            if ((m_dirNormalVec - dirNormalVec).magnitude > 0.001) {
                //타켓의 최종 좌표로 현재 좌표를 변경해준다.
                m_currentPos = m_targetPos;
                m_targetPos = Vec2.zero;
                return;
            }

            m_currentPos = movePos;
        }

        void LinearInterpolation(double deltaTime)
        {
            //m_linearValue += (float)deltaTime;

            //if (m_linearValue > 1.0f) {
            //    m_linearValue = 0;
            //    m_targetPosition = null;
            //    m_curretPosition.xPos = Xpos;
            //    m_curretPosition.yPos = Ypos;
            //    return;
            //}

            //Xpos = m_curretPosition.xPos * (1.0f - m_linearValue) + m_targetPosition.xPos * m_linearValue;
            //Ypos = m_curretPosition.yPos * (1.0f - m_linearValue) + m_targetPosition.yPos * m_linearValue;

        }
    }
}
