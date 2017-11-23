namespace WindowsForm
{
    partial class TestToeicForm
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.TileItemElement tileItemElement1 = new DevExpress.XtraEditors.TileItemElement();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestToeicForm));
            DevExpress.XtraEditors.TileItemElement tileItemElement2 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement3 = new DevExpress.XtraEditors.TileItemElement();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.tileGroup1 = new DevExpress.XtraEditors.TileGroup();
            this.tileBar1 = new DevExpress.XtraBars.Navigation.TileBar();
            this.tileBarItemUserID = new DevExpress.XtraBars.Navigation.TileBarItem();
            this.tileBarItemRequestAdmin = new DevExpress.XtraBars.Navigation.TileBarItem();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tileBarGroup4 = new DevExpress.XtraBars.Navigation.TileBarGroup();
            this.tileBarGroup3 = new DevExpress.XtraBars.Navigation.TileBarGroup();
            this.tileBarGroup2 = new DevExpress.XtraBars.Navigation.TileBarGroup();
            this.tileBarItemTimer = new DevExpress.XtraBars.Navigation.TileBarItem();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinMaskColor = System.Drawing.Color.White;
            // 
            // tileGroup1
            // 
            this.tileGroup1.Name = "tileGroup1";
            // 
            // tileBar1
            // 
            this.tileBar1.AllowDrag = false;
            this.tileBar1.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.tileBar1.BackColor = System.Drawing.Color.White;
            this.tileBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tileBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tileBar1.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            this.tileBar1.Groups.Add(this.tileBarGroup3);
            this.tileBar1.Groups.Add(this.tileBarGroup4);
            this.tileBar1.Groups.Add(this.tileBarGroup2);
            this.tileBar1.HorizontalContentAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tileBar1.Location = new System.Drawing.Point(0, 0);
            this.tileBar1.Margin = new System.Windows.Forms.Padding(1);
            this.tileBar1.MaxId = 7;
            this.tileBar1.Name = "tileBar1";
            this.tileBar1.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.tileBar1.Size = new System.Drawing.Size(724, 88);
            this.tileBar1.TabIndex = 1;
            this.tileBar1.Text = "tileBar1";
            // 
            // tileBarItemUserID
            // 
            this.tileBarItemUserID.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.tileBarItemUserID.AppearanceItem.Normal.BackColor = System.Drawing.Color.Green;
            this.tileBarItemUserID.AppearanceItem.Normal.BackColor2 = System.Drawing.Color.Lime;
            this.tileBarItemUserID.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Black;
            this.tileBarItemUserID.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileBarItemUserID.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.tileBarItemUserID.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement1.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement1.Image")));
            tileItemElement1.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileItemElement1.Text = "MSDT";
            tileItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.tileBarItemUserID.Elements.Add(tileItemElement1);
            this.tileBarItemUserID.Id = 4;
            this.tileBarItemUserID.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Wide;
            this.tileBarItemUserID.Name = "tileBarItemUserID";
            // 
            // tileBarItemRequestAdmin
            // 
            this.tileBarItemRequestAdmin.AppearanceItem.Normal.BackColor = System.Drawing.Color.Blue;
            this.tileBarItemRequestAdmin.AppearanceItem.Normal.BackColor2 = System.Drawing.Color.Blue;
            this.tileBarItemRequestAdmin.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Black;
            this.tileBarItemRequestAdmin.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileBarItemRequestAdmin.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.tileBarItemRequestAdmin.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement2.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement2.Image")));
            tileItemElement2.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopCenter;
            tileItemElement2.Text = "Liên hệ giám thị";
            tileItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            this.tileBarItemRequestAdmin.Elements.Add(tileItemElement2);
            this.tileBarItemRequestAdmin.Id = 5;
            this.tileBarItemRequestAdmin.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Wide;
            this.tileBarItemRequestAdmin.Name = "tileBarItemRequestAdmin";
            this.tileBarItemRequestAdmin.Tag = 321;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 88);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(724, 353);
            this.webBrowser1.TabIndex = 2;
            this.webBrowser1.Url = new System.Uri("https://www.google.com.vn/", System.UriKind.Absolute);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // tileBarGroup4
            // 
            this.tileBarGroup4.Items.Add(this.tileBarItemRequestAdmin);
            this.tileBarGroup4.Name = "tileBarGroup4";
            // 
            // tileBarGroup3
            // 
            this.tileBarGroup3.Items.Add(this.tileBarItemUserID);
            this.tileBarGroup3.Name = "tileBarGroup3";
            // 
            // tileBarGroup2
            // 
            this.tileBarGroup2.Items.Add(this.tileBarItemTimer);
            this.tileBarGroup2.Name = "tileBarGroup2";
            // 
            // tileBarItemTimer
            // 
            this.tileBarItemTimer.AppearanceItem.Normal.BackColor = System.Drawing.Color.Red;
            this.tileBarItemTimer.AppearanceItem.Normal.BackColor2 = System.Drawing.Color.Red;
            this.tileBarItemTimer.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileBarItemTimer.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement3.Image = ((System.Drawing.Image)(resources.GetObject("tileItemElement3.Image")));
            tileItemElement3.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileItemElement3.Text = "10:00";
            tileItemElement3.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.tileBarItemTimer.Elements.Add(tileItemElement3);
            this.tileBarItemTimer.Id = 6;
            this.tileBarItemTimer.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Wide;
            this.tileBarItemTimer.Name = "tileBarItemTimer";
            // 
            // TestToeicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(724, 441);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.tileBar1);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "TestToeicForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TestToeicForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraEditors.TileGroup tileGroup1;
        private DevExpress.XtraBars.Navigation.TileBar tileBar1;
        private DevExpress.XtraBars.Navigation.TileBarItem tileBarItemUserID;
        private DevExpress.XtraBars.Navigation.TileBarItem tileBarItemRequestAdmin;
        private DevExpress.XtraBars.Navigation.TileBarGroup tileBarGroup3;
        private DevExpress.XtraBars.Navigation.TileBarGroup tileBarGroup4;
        private DevExpress.XtraBars.Navigation.TileBarGroup tileBarGroup2;
        private DevExpress.XtraBars.Navigation.TileBarItem tileBarItemTimer;
        public System.Windows.Forms.WebBrowser webBrowser1;
    }
}