using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace TPV_OSIS
{
    public class Server
    {
        private readonly int port;
        private TcpListener listener;
        private Thread acceptThread;
        private readonly List<ClientHandler> clients = new List<ClientHandler>();
        private bool running = false;

        public Server(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            if (running) return;
            listener = new TcpListener(IPAddress.Loopback, port);
            listener.Start();
            running = true;
            acceptThread = new Thread(AcceptLoop) { IsBackground = true };
            acceptThread.Start();
        }

        public void Stop()
        {
            running = false;
            try { listener?.Stop(); } catch { }
            lock (clients)
            {
                foreach (var c in clients.ToArray()) c.Disconnect();
                clients.Clear();
            }
        }

        private void AcceptLoop()
        {
            try
            {
                while (running)
                {
                    var tcp = listener.AcceptTcpClient();
                    var handler = new ClientHandler(tcp, this);
                    lock (clients) { clients.Add(handler); }
                    handler.Start();
                }
            }
            catch (SocketException) { /* listener stopped */ }
            catch (Exception ex) { Console.WriteLine("Server accept loop error: " + ex); }
        }

        internal void Broadcast(string message, ClientHandler except = null)
        {
            lock (clients)
            {
                foreach (var c in clients.ToArray())
                {
                    if (c != except)
                    {
                        c.Send(message);
                    }
                }
            }
        }

        internal void RemoveClient(ClientHandler client)
        {
            lock (clients) { clients.Remove(client); }
        }
    }
}
