using SocketLib;
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
            SocketLib.SocketLib d = new SocketLib.SocketLib();
            d.InitListen();
            d.StartListen();
        }
    }
}
