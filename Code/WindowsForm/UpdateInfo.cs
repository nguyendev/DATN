using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForm
{
    public partial class UpdateInfo : Form
    {
        private UpdateImage Parrent;
        private LoginUpdateInfo LoginParrent;
        public UpdateInfo(UpdateImage _Parrent, LoginUpdateInfo _LoginParrent)
        {
            InitializeComponent();
            InitBrowser();
            Parrent = _Parrent;
            LoginParrent = _LoginParrent;
        }
        public ChromiumWebBrowser browser;
        public void InitBrowser()
        {
            Cef.Initialize(new CefSettings());
            browser = new ChromiumWebBrowser("https://www.google.com");
            browser.Dock = DockStyle.Fill;
            browser.Show();
            this.Controls.Add(browser);
        }
    }
}
