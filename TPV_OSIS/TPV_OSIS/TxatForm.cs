using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace TPV_OSIS
{
    public class TxatForm : Form
    {
        private TextBox txtIzena;
        private TextBox txtMezuak;
        private TextBox txtInput;
        private Button btnBidali;
        private Button btnKonektatu;
        private TcpClient erabiltzailea;
        private StreamReader irakurlea;
        private StreamWriter idazlea;
        private Thread entzunHaria;
        private string erabiltzaileIzena;

        public TxatForm(string erabiltzaileIzena)
        {
            this.erabiltzaileIzena = erabiltzaileIzena;
            Text = $"Txat - {erabiltzaileIzena}";
            Width = 600; Height = 450;
            StartPosition = FormStartPosition.CenterScreen;

            var lbl = new Label() { Left = 10, Top = 10, Text = "Erabiltzailea:", Width = 60 };
            txtIzena = new TextBox() { Left = 75, Top = 8, Width = 200, Text = erabiltzaileIzena, ReadOnly = true };
            btnKonektatu = new Button() { Left = 290, Top = 6, Width = 120, Text = "Konektatu" };
            btnKonektatu.Click += BtnConnect_Click;

            txtMezuak = new TextBox() { Left = 10, Top = 40, Width = 560, Height = 300, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical };
            txtInput = new TextBox() { Left = 10, Top = 350, Width = 460, Height = 30 };
            btnBidali = new Button() { Left = 480, Top = 350, Width = 90, Height = 30, Text = "Bidali" };
            btnBidali.Click += BtnBidali_Klik;
            btnBidali.Enabled = false;

            Controls.Add(lbl);
            Controls.Add(txtIzena);
            Controls.Add(btnKonektatu);
            Controls.Add(txtMezuak);
            Controls.Add(txtInput);
            Controls.Add(btnBidali);

            FormClosing += TxataItxi;
        }

        private void TxataItxi(object sender, FormClosingEventArgs e)
        {
            Deskonektatu();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (erabiltzailea != null)
            {
                Deskonektatu();
                btnKonektatu.Text = "Konektatu";
                btnBidali.Enabled = false;
                return;
            }

            try
            {
                erabiltzailea = new TcpClient("localhost", 3306);
                var ns = erabiltzailea.GetStream();
                irakurlea = new StreamReader(ns);
                idazlea = new StreamWriter(ns) { AutoFlush = true };
                idazlea.WriteLine(erabiltzaileIzena);
                entzunHaria = new Thread(ListenLoop) { IsBackground = true };
                entzunHaria.Start();
                btnKonektatu.Text = "Deskonektatu";
                btnBidali.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ezin da konektatu: " + ex.Message);
                erabiltzailea = null;
            }
        }

        private void ListenLoop()
        {
            try
            {
                string line;
                while (erabiltzailea != null && irakurlea != null && (line =irakurlea.ReadLine()) != null)
                {
                    AppendText(line + Environment.NewLine);
                }
            }
            catch { }
            finally
            {
                AppendText("Zerbitzaritik deskonektatuta." + Environment.NewLine);
                BeginInvoke(new Action(() => { btnBidali.Enabled = false; btnKonektatu.Text = "Konektatu"; erabiltzailea = null; }));
            }
        }

        private void AppendText(string textua)
        {
            if (txtMezuak.InvokeRequired)
            {
                txtMezuak.BeginInvoke(new Action(() => txtMezuak.AppendText(textua)));
            }
            else txtMezuak.AppendText(textua);
        }

        private void BtnBidali_Klik(object sender, EventArgs e)
        {
            if (erabiltzailea == null || idazlea == null) return;
            var msg = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(msg)) return;
            try
            {
                idazlea.WriteLine(msg);
                txtInput.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Send failed: " + ex.Message);
            }
        }

        private void Deskonektatu()
        {
            try { erabiltzailea?.Close(); } catch { }
            erabiltzailea = null;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ChatForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "ChatForm";
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.ResumeLayout(false);

        }

        private void ChatForm_Load(object sender, EventArgs e)
        {

        }
    }
}
