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
        UpdateImage Parrent;
        public UpdateInfo(UpdateImage _Parrent)
        {
            InitializeComponent();
            InitBrowser();
            Parrent = _Parrent;
        }
        public ChromiumWebBrowser browser;
        public void InitBrowser()
        {
            Cef.Initialize(new CefSettings());
            browser = new ChromiumWebBrowser("www.google.com");
            this.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
        }
    }
}
