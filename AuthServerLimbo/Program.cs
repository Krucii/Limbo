using System;
using System.Reflection;
using static AuthServerLimbo.Server.Server;
using static AuthServerLimbo.Logger.Logger;

namespace AuthServerLimbo
{
    internal class Program
    {
        private static void Main()
        {
            var assembly = Assembly.GetExecutingAssembly().GetName();
            if (assembly.Version != null)
                InfoLog($"Starting {assembly.Name}, Build {assembly.Version.Build}.{assembly.Version.Revision}");
            SetupServer();
            Console.ReadLine(); // When we press enter close everything
            InfoLog("Shutting down...");
            CloseAllSockets();
        }
    }
}
