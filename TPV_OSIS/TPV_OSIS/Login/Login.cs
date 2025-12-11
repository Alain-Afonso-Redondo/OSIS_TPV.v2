using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPV_OSIS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSartu_Click(object sender, EventArgs e)
        {
            string erabiltzailea = txbErab.Text.Trim();
            string pasahitza = txbPasa.Text.Trim();

            var login = new ErabiltzaileController();
            bool loginBalidatu = login.BalidatuLogin(erabiltzailea, pasahitza);

            if (loginBalidatu)
            {
                MessageBox.Show("Ondo logeatu zara! Ongi etorri " + erabiltzailea,
                    "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 🔥 Arranca el servidor solo si aún no está iniciado
                ServerManager.EnsureStarted(5555);

                // 🔥 Abre el chat con el nombre del usuario logeado
                var chat = new ChatForm(erabiltzailea);
                chat.Show();

                // Ocultar el login, baina mantendu aplikazioa irekita
                this.Hide();
            }
            else
            {
                lblMezua.Text = "Erabiltzaile edo pasahitza okerra.";
                lblMezua.ForeColor = Color.Red;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
