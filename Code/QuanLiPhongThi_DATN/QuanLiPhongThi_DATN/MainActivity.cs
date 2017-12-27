using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.IO;
using Android.Support.V7.App;
using Steelkiwi.Com.Library;
using System.Threading.Tasks;

namespace QuanLiPhongThi_DATN
{
    [Activity(Label = "QuanLiPhongThi_DATN", MainLauncher = true, Icon ="@drawable/icon",
        Theme = "@android:style/Theme.Holo.Light.NoActionBar.Fullscreen")]
    public class MainActivity : Activity
    {
        DotsLoaderView dlv;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button login = FindViewById<Button>(Resource.Id.button1);
            Button logincamera = FindViewById<Button>(Resource.Id.button2);
            //dlv = FindViewById<DotsLoaderView>(Resource.Id.dotsloader);
            //DemoDownload();

            logincamera.Click += (s, e) =>
            {
                Intent Selectfunction = new Intent(this, typeof(Selectfunction));
                StartActivity(Selectfunction);
            };
            login.Click += (s, e) =>
            {
                //Intent Managerment = new Intent(this, typeof(Managerment));
                //StartActivity(Managerment);
                var uri = new Uri(string.Format(Global.SEVER_URL + Global.LOGIN_URL, string.Empty));
                var user = FindViewById<EditText>(Resource.Id.editText1).Text;
                var pass = FindViewById<EditText>(Resource.Id.editText2).Text;
                LoginViewModel model = new LoginViewModel
                {
                    UserName = user.ToString(),
                    Password = pass.ToString(),
                    RememberMe = false,

                };
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client;
                client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                HttpResponseMessage response = null;
                response = client.PostAsync(uri, content).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                
                //if (responseString == "true")
                //{
                    logincamera.Text = responseString;
                    //Intent Managerment = new Intent(this, typeof(Managerment));
                    //StartActivity(Managerment);
                //}
                //else
                //logincamera.Text = "nguyen";

            };

        }
        private void DemoDownload()
        {
            dlv.Show();
            Task.Delay(5000).ContinueWith(t => { dlv.Hide(); });
        }

    }
}