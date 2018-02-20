
namespace ConsoleServer
{
    class Program {
        static void Main(string[] args)
        {
            GameServer.GameServer server = new GameServer.GameServer();
            server.InitGameServer(5050, 1);
            server.RunGameServer();
        }
    }
}
