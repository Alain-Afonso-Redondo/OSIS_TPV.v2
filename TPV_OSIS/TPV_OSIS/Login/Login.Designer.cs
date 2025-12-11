namespace TPV_OSIS
{
    partial class Login
    {

        private System.ComponentModel.IContainer components = null;





        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms


        private void InitializeComponent()
        {
            this.txbErab = new System.Windows.Forms.TextBox();
            this.txbPasa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSartu = new System.Windows.Forms.Button();
            this.lblMezua = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txbErab
            // 
            this.txbErab.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txbErab.Location = new System.Drawing.Point(150, 98);
            this.txbErab.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txbErab.Name = "txbErab";
            this.txbErab.Size = new System.Drawing.Size(151, 25);
            this.txbErab.TabIndex = 0;
            // 
            // txbPasa
            // 
            this.txbPasa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txbPasa.Location = new System.Drawing.Point(150, 154);
            this.txbPasa.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txbPasa.Name = "txbPasa";
            this.txbPasa.PasswordChar = '*';
            this.txbPasa.Size = new System.Drawing.Size(151, 25);
            this.txbPasa.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(150, 77);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Erabiltzailea";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(150, 134);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pasahitza";
            // 
            // btnSartu
            // 
            this.btnSartu.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSartu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSartu.ForeColor = System.Drawing.Color.White;
            this.btnSartu.Location = new System.Drawing.Point(150, 195);
            this.btnSartu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSartu.Name = "btnSartu";
            this.btnSartu.Size = new System.Drawing.Size(150, 28);
            this.btnSartu.TabIndex = 4;
            this.btnSartu.Text = "Sartu";
            this.btnSartu.UseVisualStyleBackColor = false;
            this.btnSartu.Click += new System.EventHandler(this.btnSartu_Click);
            // 
            // lblMezua
            // 
            this.lblMezua.AutoSize = true;
            this.lblMezua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblMezua.ForeColor = System.Drawing.Color.Red;
            this.lblMezua.Location = new System.Drawing.Point(150, 236);
            this.lblMezua.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMezua.Name = "lblMezua";
            this.lblMezua.Size = new System.Drawing.Size(0, 15);
            this.lblMezua.TabIndex = 5;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(450, 325);
            this.Controls.Add(this.lblMezua);
            this.Controls.Add(this.btnSartu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbPasa);
            this.Controls.Add(this.txbErab);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.TextBox txbErab;
        private System.Windows.Forms.TextBox txbPasa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSartu;
        private System.Windows.Forms.Label lblMezua;
    }
}