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

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnSartu_Klik(object sender, EventArgs e)
        {
            string erabiltzailea = txbErab.Text.Trim();
            string pasahitza = txbPasa.Text.Trim();

            var login = new ErabiltzaileController();
            bool loginBalidatu = login.BalidatuLogin(erabiltzailea, pasahitza);

            if (loginBalidatu)
            {
                MessageBox.Show("Ondo logeatu zara! Ongi etorri " + erabiltzailea,
                    "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                var txat = new TxatForm(erabiltzailea);
                txat.Show();

                this.Hide();
            }
            else
            {
                lblMezua.Text = "Erabiltzaile edo pasahitza okerra.";
                lblMezua.ForeColor = Color.Red;
            }
        }


    }
}
