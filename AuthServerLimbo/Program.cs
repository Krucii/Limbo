using System;
using static AuthServerLimbo.Server.Server;

namespace AuthServerLimbo
{
    class Program
    {
        static void Main()
        {
            SetupServer();
            Console.ReadLine(); // When we press enter close everything
            CloseAllSockets();
        }
    }
}
