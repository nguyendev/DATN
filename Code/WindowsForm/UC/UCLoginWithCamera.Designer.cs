namespace WindowsForm.UC
{
    partial class UCLoginWithCamera
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLoginWithPass = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.image_PICBX = new System.Windows.Forms.PictureBox();
            this.labal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.image_PICBX)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoginWithPass
            // 
            this.btnLoginWithPass.Location = new System.Drawing.Point(17, 275);
            this.btnLoginWithPass.Name = "btnLoginWithPass";
            this.btnLoginWithPass.Size = new System.Drawing.Size(129, 23);
            this.btnLoginWithPass.TabIndex = 1;
            this.btnLoginWithPass.Text = "Sử dụng mật khẩu";
            this.btnLoginWithPass.UseVisualStyleBackColor = true;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(152, 275);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(124, 23);
            this.btnRegister.TabIndex = 2;
            this.btnRegister.Text = "Đăng ký mới";
            this.btnRegister.UseVisualStyleBackColor = true;
            // 
            // image_PICBX
            // 
            this.image_PICBX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.image_PICBX.Location = new System.Drawing.Point(17, 65);
            this.image_PICBX.Name = "image_PICBX";
            this.image_PICBX.Size = new System.Drawing.Size(259, 190);
            this.image_PICBX.TabIndex = 0;
            this.image_PICBX.TabStop = false;
            // 
            // labal
            // 
            this.labal.AutoSize = true;
            this.labal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labal.Location = new System.Drawing.Point(12, 15);
            this.labal.Name = "labal";
            this.labal.Size = new System.Drawing.Size(272, 29);
            this.labal.TabIndex = 3;
            this.labal.Text = "Đăng nhập với camera";
            this.labal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UCLoginWithCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labal);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnLoginWithPass);
            this.Controls.Add(this.image_PICBX);
            this.Name = "UCLoginWithCamera";
            this.Size = new System.Drawing.Size(290, 310);
            ((System.ComponentModel.ISupportInitialize)(this.image_PICBX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox image_PICBX;
        private System.Windows.Forms.Button btnLoginWithPass;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label labal;
    }
}
