using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Net.Http;
using System.Net.Http.Headers;
using WindowsForm.ViewModels;

namespace WindowsForm.UC
{
    public partial class UCLoginWithPassword : UserControl
    {
        private string SEVER_URL = "http://localhost:51989/";
        private static UCLoginWithPassword _instance;
        public static UCLoginWithPassword Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UCLoginWithPassword();
                return _instance;
            }
        }
        public UCLoginWithPassword()
        {
            InitializeComponent();
            Initialise_capture();
        }

        public void Initialise_capture()
        {
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            txtPassword.Text = "";
            txtUserName.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(SEVER_URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var login = new Dictionary<string, string>
            {
               { "Email", txtUserName.Text },
               { "Password", txtPassword.Text },
               { "RememberMe", "false" }
            };
            var content = new FormUrlEncodedContent(login);

            var response = client.PostAsync("api/AccountAPI", content).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            if (responseString == "true")
            {
                lbStatus.Text = "Đăng nhập thành công, đang chuyển";
                lbStatus.ForeColor = Color.Green;
                admin_thitoeicEntities1 db = new admin_thitoeicEntities1();
                var user = db.AspNetUsers.Single(p => p.Email == txtUserName.Text);
                //var role = db.ASP.Single(p => p.)
            }
            else
            {
                lbStatus.Text = "Sai tên đăng nhập hoặc mật khẩu";
                lbStatus.ForeColor = Color.Red;
            }


        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
