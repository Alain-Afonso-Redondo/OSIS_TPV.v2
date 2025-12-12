using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace TPV_OSIS
{
    public class Zerbitzaria
    {
        private readonly int portua;
        private TcpListener listener;
        private Thread hariaBaimendu;
        private readonly List<ErabiltzaileKudeaketa> erabiltzaileak = new List<ErabiltzaileKudeaketa>();
        private bool running = false;

        public Zerbitzaria(int portua)
        {
            this.portua = portua;
        }

        public void Hasi()
        {
            if (running) return;
            listener = new TcpListener(IPAddress.Loopback, portua);
            listener.Start();
            running = true;
            hariaBaimendu = new Thread(BaimenduZirkuitua) { IsBackground = true };
            hariaBaimendu.Start();
        }

        public void Gelditu()
        {
            running = false;
            try { listener?.Stop(); } catch { }
            lock (erabiltzaileak)
            {
                foreach (var c in erabiltzaileak.ToArray()) c.Deskonektatu();
                erabiltzaileak.Clear();
            }
        }

        private void BaimenduZirkuitua()
        {
            try
            {
                while (running)
                {
                    var tcp = listener.AcceptTcpClient();
                    var manager = new ErabiltzaileKudeaketa(tcp, this);
                    lock (erabiltzaileak) { erabiltzaileak.Add(manager); }
                    manager.Hasi();
                }
            }
            catch (SocketException) { 
            }
            catch (Exception ex) { Console.WriteLine("Zerbitzarian errorea zirkuitu baimentzean: " + ex); }
        }

        internal void Transmisioa(string message, ErabiltzaileKudeaketa except = null)
        {
            lock (erabiltzaileak)
            {
                foreach (var c in erabiltzaileak.ToArray())
                {
                    if (c != except)
                    {
                        c.Bidali(message);
                    }
                }
            }
        }

        internal void ErabiltzaileaDeskonektatu(ErabiltzaileKudeaketa erabiltzaile)
        {
            lock (erabiltzaileak) { erabiltzaileak.Remove(erabiltzaile); }
        }
    }
}
