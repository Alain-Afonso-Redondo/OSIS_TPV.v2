namespace TPV_OSIS
{
    public static class ServerManager
    {
        private static Server _server;
        private static readonly object _lock = new object();

        public static void EnsureStarted(int port = 5555)
        {
            lock (_lock)
            {
                if (_server == null)
                {
                    _server = new Server(port);
                    _server.Start();
                }
            }
        }

        public static void Stop()
        {
            lock (_lock)
            {
                if (_server != null)
                {
                    _server.Stop();
                    _server = null;
                }
            }
        }
    }
}
