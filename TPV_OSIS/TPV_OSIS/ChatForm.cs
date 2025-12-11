using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace TPV_OSIS
{
    public class ChatForm : Form
    {
        private TextBox txtName;
        private TextBox txtMessages;
        private TextBox txtInput;
        private Button btnSend;
        private Button btnConnect;
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private Thread listenThread;
        private string username;

        public ChatForm(string username)
        {
            this.username = username;
            Text = $"Chat - {username}";
            Width = 600; Height = 450;
            StartPosition = FormStartPosition.CenterScreen;

            var lbl = new Label() { Left = 10, Top = 10, Text = "Usuario:", Width = 60 };
            txtName = new TextBox() { Left = 75, Top = 8, Width = 200, Text = username, ReadOnly = true };
            btnConnect = new Button() { Left = 290, Top = 6, Width = 120, Text = "Connect" };
            btnConnect.Click += BtnConnect_Click;

            txtMessages = new TextBox() { Left = 10, Top = 40, Width = 560, Height = 300, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical };
            txtInput = new TextBox() { Left = 10, Top = 350, Width = 460, Height = 30 };
            btnSend = new Button() { Left = 480, Top = 350, Width = 90, Height = 30, Text = "Send" };
            btnSend.Click += BtnSend_Click;
            btnSend.Enabled = false;

            Controls.Add(lbl);
            Controls.Add(txtName);
            Controls.Add(btnConnect);
            Controls.Add(txtMessages);
            Controls.Add(txtInput);
            Controls.Add(btnSend);

            FormClosing += ChatForm_FormClosing;
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                Disconnect();
                btnConnect.Text = "Connect";
                btnSend.Enabled = false;
                return;
            }

            try
            {
                client = new TcpClient("127.0.0.1", 5555);
                var ns = client.GetStream();
                reader = new StreamReader(ns);
                writer = new StreamWriter(ns) { AutoFlush = true };
                // send name
                writer.WriteLine(username);
                listenThread = new Thread(ListenLoop) { IsBackground = true };
                listenThread.Start();
                btnConnect.Text = "Disconnect";
                btnSend.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not connect: " + ex.Message);
                client = null;
            }
        }

        private void ListenLoop()
        {
            try
            {
                string line;
                while (client != null && reader != null && (line = reader.ReadLine()) != null)
                {
                    AppendText(line + Environment.NewLine);
                }
            }
            catch { }
            finally
            {
                AppendText("Disconnected from server." + Environment.NewLine);
                BeginInvoke(new Action(() => { btnSend.Enabled = false; btnConnect.Text = "Connect"; client = null; }));
            }
        }

        private void AppendText(string text)
        {
            if (txtMessages.InvokeRequired)
            {
                txtMessages.BeginInvoke(new Action(() => txtMessages.AppendText(text)));
            }
            else txtMessages.AppendText(text);
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (client == null || writer == null) return;
            var msg = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(msg)) return;
            try
            {
                writer.WriteLine(msg);
                txtInput.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Send failed: " + ex.Message);
            }
        }

        private void Disconnect()
        {
            try { client?.Close(); } catch { }
            client = null;
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
