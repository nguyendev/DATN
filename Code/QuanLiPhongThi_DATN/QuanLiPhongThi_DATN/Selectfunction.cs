using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace QuanLiPhongThi_DATN
{
    [Activity(Label = "Selectfunction", Theme = "@android:style/Theme.Holo.Light.NoActionBar.Fullscreen")]
    public class Selectfunction : Activity
    {
        List<string> data;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Selectfunction);

            // Create your application here

            var lv = FindViewById<ListView>(Resource.Id.listView1);

            data = new List<string>();
            data.Add("Lấy dữ liệu thí sinh");
            data.Add("Nhận diện thí sinh");
            data.Add("Quản lí phòng thi");

            lv.Adapter = new ArrayAdapter(this,Android.Resource.Layout.SimpleExpandableListItem1,data);
            lv.ItemClick += Lv_ItemClick;

        }
        private void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Toast.MakeText(this, "Bạn vừa chọn "+ e.Position, ToastLength.Short).Show();
            switch (e.Position)
            {
                case 0:
                    Intent Getdataexaminer = new Intent(this, typeof(Getdataexaminer));
                    StartActivity(Getdataexaminer);
                    break;

                case 1:
                    Intent Recognitionface = new Intent(this, typeof(Recognitionface));
                    StartActivity(Recognitionface);
                    break;

                case 2:
                    Intent Managerment = new Intent(this, typeof(Managerment));
                    StartActivity(Managerment);
                    break;

            }
        }

    }
}