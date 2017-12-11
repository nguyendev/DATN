using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.AppCompat;
using Com.Nex3z.Notificationbadge;

using System.Threading;
using System.Timers;

namespace QuanLiPhongThi_DATN
{
    [Activity(Label = "Managerment", Theme = "@android:style/Theme.Holo.Light.NoActionBar.Fullscreen")]
    public class Managerment : Activity
    {
        System.Timers.Timer timer;
        int count = 1;
        Spinner spinner;
        ListView lv;
        TextView txt;
        List<string> items = new List<string>() { "1", "2", "3", "4", "5", "6" };
        string[] listitem = { " C# Corner", " Xamarin", " Google Plus", " Twitter", " Windows", " Bing", " Itunes", " Wordpress", "   Drupal", " Whatapp" };
        NotificationBadge m;
        ActivityCustomListview lvAdapter;
        ActivityCustomSpinner spinAdapter;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Management);
            // Create your application here
            spinner = FindViewById<Spinner>(Resource.Id.spinner);
            lv = FindViewById<ListView>(Resource.Id.listView1);
            txt = FindViewById<TextView>(Resource.Id.textView1);


            lvAdapter = new ActivityCustomListview(this, items);
            spinAdapter = new ActivityCustomSpinner(this, listitem);

            spinner.Adapter = spinAdapter;
            lv.SetAdapter(lvAdapter);

            lv.ItemClick += OnListItemClick;
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            m = FindViewById<NotificationBadge>(Resource.Id.badgle);
            m.SetNumber(count);
        }

        protected override void OnResume()
        {
            base.OnResume();
            
            timer = new System.Timers.Timer();
            timer.Interval = 1000;  //// khoảng thời gian chuyển
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(count !=0)
            {
                count++;
                RunOnUiThread(() =>
                {
                    m.SetNumber(10);
                    //txt.Text = count.ToString();
                    string connectstring = @"data source=125.212.221.212;initial catalog=admin_thitoeic;user id=thitoeic;password=T@Nguyenkinh15;Connect Timeout=60";
                    string id = "ban đầu";
                    string sql = "SELECT * FROM Image";

                    using (SqlConnection con = new SqlConnection(connectstring))
                    {
                        try
                        {
                            con.Open();

                            using (SqlCommand comando = new SqlCommand(sql, con))
                            {
                                using (SqlDataReader reader = comando.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        id = reader.GetString(1);
                                    }
                                }
                            }

                            con.Close();
                        }
                        catch (SqlException)
                        {
                            id = "không connect";
                        }

                    }
                    items[5] = id;
                    listitem[1] = id;
                    spinAdapter.NotifyDataSetChanged();
                    lvAdapter.NotifyDataSetChanged();
                });
            }
           
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
         {
           
         }

        void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //Spinner spinner = (Spinner)sender;
            //string toast = string.Format("Selected car is {0}", listitem[e.Position]);
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

    }
}