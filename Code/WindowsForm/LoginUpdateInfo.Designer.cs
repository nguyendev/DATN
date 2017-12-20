namespace WindowsForm
{
    partial class LoginUpdateInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginUpdateInfo));
            this.lbCodeUpdate = new System.Windows.Forms.Label();
            this.txtCodeUpdate = new System.Windows.Forms.TextBox();
            this.lbProtect = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProtectCode = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnReloadCapcha = new System.Windows.Forms.Button();
            this.IMG_Capcha = new System.Windows.Forms.PictureBox();
            this.lbNotify = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.IMG_Capcha)).BeginInit();
            this.SuspendLayout();
            // 
            // lbCodeUpdate
            // 
            this.lbCodeUpdate.AutoSize = true;
            this.lbCodeUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbCodeUpdate.Location = new System.Drawing.Point(33, 42);
            this.lbCodeUpdate.Name = "lbCodeUpdate";
            this.lbCodeUpdate.Size = new System.Drawing.Size(67, 13);
            this.lbCodeUpdate.TabIndex = 1;
            this.lbCodeUpdate.Text = "Mã cập nhật";
            // 
            // txtCodeUpdate
            // 
            this.txtCodeUpdate.Location = new System.Drawing.Point(128, 43);
            this.txtCodeUpdate.Name = "txtCodeUpdate";
            this.txtCodeUpdate.Size = new System.Drawing.Size(204, 20);
            this.txtCodeUpdate.TabIndex = 1;
            // 
            // lbProtect
            // 
            this.lbProtect.AutoSize = true;
            this.lbProtect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbProtect.Location = new System.Drawing.Point(33, 121);
            this.lbProtect.Name = "lbProtect";
            this.lbProtect.Size = new System.Drawing.Size(58, 13);
            this.lbProtect.TabIndex = 3;
            this.lbProtect.Text = "Mã bảo vệ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(33, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nhập mã bảo vệ để xác thực";
            // 
            // txtProtectCode
            // 
            this.txtProtectCode.Location = new System.Drawing.Point(189, 215);
            this.txtProtectCode.Name = "txtProtectCode";
            this.txtProtectCode.Size = new System.Drawing.Size(100, 20);
            this.txtProtectCode.TabIndex = 2;
            // 
            // btnRemove
            // 
            this.btnRemove.Image = global::WindowsForm.Properties.Resources.delete;
            this.btnRemove.Location = new System.Drawing.Point(222, 272);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(54, 23);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Image = global::WindowsForm.Properties.Resources.done_tick;
            this.btnLogin.Location = new System.Drawing.Point(150, 272);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(52, 23);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnReloadCapcha
            // 
            this.btnReloadCapcha.Image = global::WindowsForm.Properties.Resources.refresh;
            this.btnReloadCapcha.Location = new System.Drawing.Point(296, 213);
            this.btnReloadCapcha.Name = "btnReloadCapcha";
            this.btnReloadCapcha.Size = new System.Drawing.Size(36, 23);
            this.btnReloadCapcha.TabIndex = 5;
            this.btnReloadCapcha.Text = "\r\n";
            this.btnReloadCapcha.UseVisualStyleBackColor = true;
            this.btnReloadCapcha.Click += new System.EventHandler(this.btnReloadCapcha_Click);
            // 
            // IMG_Capcha
            // 
            this.IMG_Capcha.Location = new System.Drawing.Point(128, 86);
            this.IMG_Capcha.Name = "IMG_Capcha";
            this.IMG_Capcha.Size = new System.Drawing.Size(204, 90);
            this.IMG_Capcha.TabIndex = 0;
            this.IMG_Capcha.TabStop = false;
            // 
            // lbNotify
            // 
            this.lbNotify.AutoSize = true;
            this.lbNotify.Location = new System.Drawing.Point(91, 249);
            this.lbNotify.Name = "lbNotify";
            this.lbNotify.Size = new System.Drawing.Size(0, 13);
            this.lbNotify.TabIndex = 8;
            this.lbNotify.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoginUpdateInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 324);
            this.ControlBox = false;
            this.Controls.Add(this.lbNotify);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnReloadCapcha);
            this.Controls.Add(this.txtProtectCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbProtect);
            this.Controls.Add(this.txtCodeUpdate);
            this.Controls.Add(this.lbCodeUpdate);
            this.Controls.Add(this.IMG_Capcha);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginUpdateInfo";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật thông tin";
            this.Load += new System.EventHandler(this.LoginUpdateInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IMG_Capcha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox IMG_Capcha;
        private System.Windows.Forms.Label lbCodeUpdate;
        private System.Windows.Forms.TextBox txtCodeUpdate;
        private System.Windows.Forms.Label lbProtect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProtectCode;
        private System.Windows.Forms.Button btnReloadCapcha;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lbNotify;
    }
}