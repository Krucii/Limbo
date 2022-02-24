using System.Net.Sockets;

namespace AuthServerLimbo.Client
{
    public class Client
    {
        private readonly Socket _clientSocket;
        private string _username;
        private string _uuid;
        private ClientState _state;

        public Client(Socket clientSocket)
        {
            _clientSocket = clientSocket;
            _state = ClientState.None;
        }

        public Socket GetClientSocket() => _clientSocket;

        public void SetUuid(string uuid) => _uuid = uuid;
        public string GetUuid() => _uuid;

        public void SetUsername(string username) => _username = username;
        public string GetUsername() => _username;

        public ClientState GetState() => _state;
        public void SetState(ClientState state) => _state = state;
    }
}
