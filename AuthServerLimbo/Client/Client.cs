using System.Net.Sockets;

namespace AuthServerLimbo.Client
{
    public class Client
    {
        private readonly Socket _clientSocket;
        private string _username;
        private string _uuid;
        private int _entityId;
        private ClientState _state;

        public Client(Socket clientSocket)
        {
            _clientSocket = clientSocket;
            _state = ClientState.None;
        }

        public Socket GetClientSocket() => _clientSocket;

        public ClientState GetState() => _state;
        public ClientState SetState(ClientState state) => _state = state;
    }
}
