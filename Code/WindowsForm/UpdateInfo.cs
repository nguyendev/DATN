using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForm
{

    public partial class UpdateInfo : Form
    {
        private LoginUpdateInfo _mainForm;
        private string SEVER_URL = "http://localhost:51989/";
        public UpdateInfo(LoginUpdateInfo mainForm, string code)
        {
            _mainForm = mainForm;
            InitializeComponent();
            LoadFormImage(code);
        }
        private void LoadFormImage(string code)
        {
            admin_thitoeicEntities db = new admin_thitoeicEntities();
            var user = db.AspNetUsers.Single(p => p.Code == code);
            bool contact = db.Contacts.Any(p => p.OwnerID == user.Id);
            if (contact)
            {

            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(SEVER_URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var login = new Dictionary<string, string>
            {
               { "UserID",  user.Id}
            };
                var content = new FormUrlEncodedContent(login);

                var response = client.PostAsync("api/ContactAPI", content).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                if (responseString == "true")
                {

                }
                else
                {
                    MessageBox.Show("Tạo danh sách liên hệ thất bại");
                }
            }
        }
    }
}