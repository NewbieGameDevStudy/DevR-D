using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuidGen
{
    //Guid 구조는 총 64비트 ulong
    //42비트 timestemp, 14비트 uniqueCount 8비트 machieId
    public class Guid
    {
        DateTime m_startDate;
        ushort m_uniqueCount;
        byte m_machieId;

        ulong m_maxTimeStamp;
        int m_maxUniqueCount;

        object m_lock = new object();

        public Guid(byte machieId)
        {
            if (machieId > 255) {
                Console.WriteLine("머신 아이디 초과");
                return;
            }

            m_machieId = machieId;
            m_uniqueCount = 0;
            m_startDate = new DateTime(2013, 1, 1);

            m_maxTimeStamp = (ulong)Math.Pow(2, 42) - 1;
            m_maxUniqueCount = (int)Math.Pow(2, 14) - 1;
        }

        public ulong GuidCreate()
        {
            ulong guid = 0;
            lock (m_lock) {
                var timeStamp = (ulong)((DateTime.Now - m_startDate).TotalMilliseconds);

                if (m_maxTimeStamp < timeStamp) {
                    m_startDate = DateTime.Now;
                    Thread.Sleep(100);
                    timeStamp = (ulong)((DateTime.Now - m_startDate).TotalMilliseconds);
                }

                if (m_maxUniqueCount < m_uniqueCount) {
                    m_uniqueCount = 0;
                    Thread.Sleep(100);
                    timeStamp = (ulong)((DateTime.Now - m_startDate).TotalMilliseconds);
                }

                m_uniqueCount++;

                guid = timeStamp << 22;   //timestamp 22 ~ 63비트까지
                guid |= (ulong)m_uniqueCount << 8; //unique 8 ~ 22
                guid |= (ulong)m_machieId;  //machiedId 8 ~ 0

                ///아래는 복호화 테스트 문 나중에 단위테스트로 옮길예정
                ///타임스탬프
                //var testlong = guid >> 22;

                ////카운트
                //ulong origin1 = ulong.MaxValue;
                //origin1 = ~(origin1 << 14);
                //var testUnique = guid >> 8;
                //testUnique = origin1 & testUnique;

                ///머신아이디
                //ulong origin2 = ulong.MaxValue;
                //origin2 = ~(origin2 << 8);
                //var testMacheidId = guid;
                //testMacheidId = origin2 & testMacheidId;
            }

            return guid;
        }
    }
}
