using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace QuanLiPhongThi_DATN
{
    [Activity(Label = "QuanLiPhongThi_DATN", MainLauncher = true,
        Theme = "@android:style/Theme.Holo.Light.NoActionBar.Fullscreen")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button login = FindViewById<Button>(Resource.Id.button1);
            Button logincamera = FindViewById<Button>(Resource.Id.button2);

            logincamera.Click += (s, e) => {
                Intent Selectfunction = new Intent(this, typeof(Selectfunction));
                StartActivity(Selectfunction);
            };
            //login.Click += (s, e) => {
            //    Intent Managerment = new Intent(this, typeof(Managerment));
            //    StartActivity(Managerment);
            //};

        }
    }
}

