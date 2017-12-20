using CefSharp;
using CefSharp.WinForms;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForm
{
    public partial class UpdateInfo : Form
    {
        private UpdateImage _parrent;
        
        private string _code;
        
        private LoginUpdateInfo _loginParrent;
        public ChromiumWebBrowser browser;
        public UpdateInfo(UpdateImage parrent, LoginUpdateInfo loginParrent, string code)
        {
            InitializeComponent();
            _parrent = parrent;
            _code = code;
            _loginParrent = loginParrent;
            InitBrowser();
            Global.ShowWaitForm(this);
        }
        
        
        public void InitBrowser()
        {
            Cef.Initialize(new CefSettings());
            string urlBrowser = Global.SEVER_URL + "/" + Global.CREATE_UPDATE_INFO_URL + "/" + _code;
            browser = new ChromiumWebBrowser(urlBrowser);
            browser.Dock = DockStyle.Fill;
            browser.Show();
            this.Controls.Add(browser);
        }
    }
}
