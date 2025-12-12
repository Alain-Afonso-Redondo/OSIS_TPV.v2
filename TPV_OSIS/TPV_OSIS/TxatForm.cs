using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace TPV_OSIS
{
    public partial class TxatForm : Form
    {
        private string username;
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private Thread listenThread;

        public TxatForm(string user)
        {
            InitializeComponent();
            username = user;
            lblUser.Text = user;
        }

        private void TxatForm_Load(object sender, EventArgs e)
        {
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("192.168.115.162", 5555);
                var ns = client.GetStream();

                reader = new StreamReader(ns);
                writer = new StreamWriter(ns) { AutoFlush = true };

                writer.WriteLine(username);

                listenThread = new Thread(ListenLoop);
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar al servidor de chat.\n\n" + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListenLoop()
        {
            try
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    AppendMessage(line);
                }
            }
            catch { }
        }

        private void AppendMessage(string msg)
        {
            if (txtMessages.InvokeRequired)
            {
                txtMessages.Invoke(new Action(() => txtMessages.AppendText(msg + Environment.NewLine)));
            }
            else
            {
                txtMessages.AppendText(msg + Environment.NewLine);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (writer == null) return;

            string msg = txtInput.Text.Trim();
            if (msg == "") return;

            writer.WriteLine(msg);
            txtInput.Text = "";
        }

        private void TxatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { client?.Close(); } catch { }
        }
    }
}
