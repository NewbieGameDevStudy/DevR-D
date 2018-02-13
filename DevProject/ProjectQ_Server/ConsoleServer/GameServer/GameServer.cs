using RestSharp;
using Server;
using System;
using System.Threading;

namespace GameServer
{
    public class GameServer
    {
        BaseServer m_baseServer;
        
        Thread m_updateThread;

        public void InitGameServer(int port, byte machiedId)
        {
            m_baseServer = new BaseServer();
            m_baseServer.InitServer(port, machiedId);
            
            m_updateThread = new Thread(Update);
        }

        public void RunGameServer()
        {
            m_updateThread.Start();
            m_baseServer.RunServer();
        }

        public void Update()
        {
            double t = 0.0;
            var prevTime = DateTime.Now;

            while (true) {
                var nowTime = DateTime.Now;
                var time = nowTime - prevTime;
                prevTime = nowTime;
                t += time.TotalSeconds;

                //서버 업데이트 목록
                m_baseServer.Update(t);
            }
        }
    }
}
