using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace TPV_OSIS
{
    public class ErabiltzaileKudeaketa
    {
        private readonly TcpClient erabiltzailea;
        private readonly Zerbitzaria zerbitzaria;
        private Thread haria;
        private StreamReader irakurlea;
        private StreamWriter idazlea;
        public string izena;

        public ErabiltzaileKudeaketa(TcpClient erabiltzailea, Zerbitzaria zerbitzaria)
        {
            this.erabiltzailea = erabiltzailea;
            this.zerbitzaria = zerbitzaria;
        }

        public void Hasi()
        {
            haria = new Thread(Run) { IsBackground = true };
            haria.Start();
        }

        private void Run()
        {
            NetworkStream ns = null;
                try
            {
                ns = erabiltzailea.GetStream();
                irakurlea = new StreamReader(ns);
                idazlea = new StreamWriter(ns) { AutoFlush = true };

                zerbitzaria.Transmisioa($"{izena} txatera sartu da.");

                string lerroa;
                while ((lerroa = irakurlea.ReadLine()) != null)
                {
                    zerbitzaria.Transmisioa($"{izena}: {lerroa}");
                }
            }
            catch (IOException) { 
            }
            catch (Exception ex) { Console.WriteLine("Errorea erabiltzaileen kudeaketan: " + ex); }
            finally
            {
                zerbitzaria.Transmisioa($"{izena} txatetik atera da.");
                if (ns != null)
                {
                    ns.Dispose();
                }
                Deskonektatu();
            }
        }

        public void Bidali(string mezua)
        {
            try
            {
                idazlea?.WriteLine(mezua);
            }
            catch { 
            }
        }

        public void Deskonektatu()
        {
            try { erabiltzailea.Close(); } catch { }
            zerbitzaria.ErabiltzaileaDeskonektatu(this);
        }
    }
}
