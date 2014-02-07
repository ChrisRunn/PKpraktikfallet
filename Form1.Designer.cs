namespace praktikfall
{
    partial class formLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formLogin));
            this.btnLoginLogin = new System.Windows.Forms.Button();
            this.lblLoginUsername = new System.Windows.Forms.Label();
            this.lblLoginPw = new System.Windows.Forms.Label();
            this.tbLoginUsername = new System.Windows.Forms.TextBox();
            this.tbLoginPw = new System.Windows.Forms.TextBox();
            this.pbLoginLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoginLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoginLogin
            // 
            this.btnLoginLogin.Location = new System.Drawing.Point(213, 224);
            this.btnLoginLogin.Name = "btnLoginLogin";
            this.btnLoginLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLoginLogin.TabIndex = 0;
            this.btnLoginLogin.Text = "Logga in";
            this.btnLoginLogin.UseVisualStyleBackColor = true;
            this.btnLoginLogin.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // lblLoginUsername
            // 
            this.lblLoginUsername.AutoSize = true;
            this.lblLoginUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginUsername.Location = new System.Drawing.Point(112, 134);
            this.lblLoginUsername.Name = "lblLoginUsername";
            this.lblLoginUsername.Size = new System.Drawing.Size(95, 13);
            this.lblLoginUsername.TabIndex = 1;
            this.lblLoginUsername.Text = "Användarnamn:";
            // 
            // lblLoginPw
            // 
            this.lblLoginPw.AutoSize = true;
            this.lblLoginPw.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginPw.Location = new System.Drawing.Point(112, 178);
            this.lblLoginPw.Name = "lblLoginPw";
            this.lblLoginPw.Size = new System.Drawing.Size(63, 13);
            this.lblLoginPw.TabIndex = 2;
            this.lblLoginPw.Text = "Lösenord:";
            // 
            // tbLoginUsername
            // 
            this.tbLoginUsername.Location = new System.Drawing.Point(213, 134);
            this.tbLoginUsername.Name = "tbLoginUsername";
            this.tbLoginUsername.Size = new System.Drawing.Size(100, 20);
            this.tbLoginUsername.TabIndex = 3;
            // 
            // tbLoginPw
            // 
            this.tbLoginPw.Location = new System.Drawing.Point(213, 178);
            this.tbLoginPw.Name = "tbLoginPw";
            this.tbLoginPw.PasswordChar = '*';
            this.tbLoginPw.Size = new System.Drawing.Size(100, 20);
            this.tbLoginPw.TabIndex = 4;
            // 
            // pbLoginLogo
            // 
            this.pbLoginLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLoginLogo.Image")));
            this.pbLoginLogo.Location = new System.Drawing.Point(111, 22);
            this.pbLoginLogo.Name = "pbLoginLogo";
            this.pbLoginLogo.Size = new System.Drawing.Size(261, 75);
            this.pbLoginLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLoginLogo.TabIndex = 5;
            this.pbLoginLogo.TabStop = false;
            // 
            // formLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(501, 355);
            this.Controls.Add(this.pbLoginLogo);
            this.Controls.Add(this.tbLoginPw);
            this.Controls.Add(this.tbLoginUsername);
            this.Controls.Add(this.lblLoginPw);
            this.Controls.Add(this.lblLoginUsername);
            this.Controls.Add(this.btnLoginLogin);
            this.Name = "formLogin";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pbLoginLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoginLogin;
        private System.Windows.Forms.Label lblLoginUsername;
        private System.Windows.Forms.Label lblLoginPw;
        private System.Windows.Forms.TextBox tbLoginUsername;
        private System.Windows.Forms.TextBox tbLoginPw;
        private System.Windows.Forms.PictureBox pbLoginLogo;


    }
}

