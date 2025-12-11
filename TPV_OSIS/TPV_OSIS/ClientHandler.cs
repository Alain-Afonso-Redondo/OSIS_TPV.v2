using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace TPV_OSIS
{
    public class ClientHandler
    {
        private readonly TcpClient client;
        private readonly Server server;
        private Thread thread;
        private StreamReader reader;
        private StreamWriter writer;
        public string Name;

        public ClientHandler(TcpClient client, Server server)
        {
            this.client = client;
            this.server = server;
        }

        public void Start()
        {
            thread = new Thread(Run) { IsBackground = true };
            thread.Start();
        }

        private void Run()
        {
            NetworkStream ns = null;
                try
            {
                ns = client.GetStream();
                reader = new StreamReader(ns);
                writer = new StreamWriter(ns) { AutoFlush = true };

                // First line expected is the client name (optional)
                Name = reader.ReadLine() ?? "Anonymous";
                server.Broadcast($"{Name} joined the chat.");

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    server.Broadcast($"{Name}: {line}");
                }
            }
            catch (IOException) { /* connection closed */ }
            catch (Exception ex) { Console.WriteLine("Client handler error: " + ex); }
            finally
            {
                server.Broadcast($"{Name} left the chat.");
                if (ns != null)
                {
                    ns.Dispose();
                }
                Disconnect();
            }
        }

        public void Send(string message)
        {
            try
            {
                writer?.WriteLine(message);
            }
            catch { /* ignore send errors */ }
        }

        public void Disconnect()
        {
            try { client.Close(); } catch { }
            server.RemoveClient(this);
        }
    }
}
