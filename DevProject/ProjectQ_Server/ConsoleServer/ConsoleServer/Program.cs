using BaseServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleServer {
    class Program {
        static void Main(string[] args)
        {
            GameServer server = new GameServer();
            server.InitServer(5050);
        }
    }
}
