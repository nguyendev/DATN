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
using Java.Sql;

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
        string connectstring = @"data source=125.212.221.212;initial catalog=admin_thitoeic;user id=thitoeic;password=T@Nguyenkinh15;Connect Timeout=60";
        string id = "ban đầu";
        List<qlp> listsum = new List<qlp>();
        List<qlp> listpresent = new List<qlp>();
        List<qlp> qlp = new List<qlp>();
        // chỉ hiện thông tin của thí sinh và + thêm chuỗi "đã thi xong xin được ra về" nên mảng vầy là đủ
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
            //chuẩn bị dữ liệu thô cho listsum, listpresent
            for (int i = 1; i < 10 ;i++)
            {
                qlp sum = new qlp()
                {
                    Rooname = i.ToString(),
                    Sum = 0
                };
                qlp present = new qlp()
                {
                    Rooname = i.ToString(),
                    Present = 0
                };
                listsum.Add(sum);
                listpresent.Add(present);
            }
            //Danh sách tổng số lượng thí sinh đã đăng kí tại phòng x vào ngày x ca x
            string sql1 = "select thitoeic.Enrollment.RoomID,count(*) "
                            + " from thitoeic.AspNetUsers, thitoeic.Enrollment, thitoeic.Course"
                            + " where thitoeic.AspNetUsers.id = thitoeic.Enrollment.StudentID AND thitoeic.Course.CourseID = thitoeic.Enrollment.CourseID"
                            + " AND(thitoeic.Course.ExamTime BETWEEN '2017-12-13 07:30:00.0000000' AND '2017-12-13 09:30:00.0000000')"
                            + " GROUP BY thitoeic.Enrollment.RoomID"
                            + " ORDER BY thitoeic.Enrollment.RoomID";
            using (SqlConnection con = new SqlConnection(connectstring))
            {
                try
                {
                    con.Open();

                    using (SqlCommand comando = new SqlCommand(sql1, con))
                    {
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                qlp sum = new qlp()
                                {
                                    Rooname = reader.GetInt32(0).ToString(),
                                    Sum = reader.GetInt32(1)
                                };
                                listsum[Convert.ToInt32(sum.Rooname)].Rooname = sum.Rooname;
                                listsum[Convert.ToInt32(sum.Rooname)].Sum = sum.Sum;
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

            //Danh sách tổng số lượng thí sinh đã vào phòng x vào ngày x ca x
            string sql3 = "select thitoeic.Enrollment.RoomID,count(thitoeic.HistoryLogin.HistoryLoginID)"
                            + " from thitoeic.HistoryLogin,thitoeic.Enrollment"
                            + " where   thitoeic.Enrollment.StudentID = thitoeic.HistoryLogin.StudentID"
                            + " AND(thitoeic.HistoryLogin.DateTime BETWEEN '2017-12-13 07:30:00.0000000' AND '2017-12-13 09:30:00.0000000')"
                            + " GROUP BY thitoeic.Enrollment.RoomID"
                            + " ORDER BY thitoeic.Enrollment.RoomID";
            using (SqlConnection con = new SqlConnection(connectstring))
            {
                try
                {
                    con.Open();

                    using (SqlCommand comando = new SqlCommand(sql3, con))
                    {
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                qlp present = new qlp()
                                {
                                    Rooname = reader.GetInt32(0).ToString(),
                                    Present = reader.GetInt32(1)
                                };

                                listpresent[Convert.ToInt32(present.Rooname)].Rooname = present.Rooname;
                                listpresent[Convert.ToInt32(present.Rooname)].Present = present.Present;
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

            //SQL lấy danh sách phòng
            int j = 1;
            string sql2 = "select thitoeic.Room.Name from thitoeic.Room, thitoeic.Course, thitoeic.CourseRoom where thitoeic.Room.ID = thitoeic.CourseRoom.RoomID AND thitoeic.Course.CourseID = thitoeic.CourseRoom.CourseID AND(thitoeic.Course.ExamTime BETWEEN '2017-12-13 07:30:00.0000000' AND '2017-12-13 09:30:00.0000000') ORDER BY thitoeic.Room.ID";
            using (SqlConnection con = new SqlConnection(connectstring))
            {
                try
                {
                    con.Open();

                    using (SqlCommand comando = new SqlCommand(sql2, con))
                    {
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            
                            while (reader.Read())
                            {


                                qlp qlpdetail = new qlp()
                                {
                                    Rooname = reader.GetString(0),
                                    Absent =  listsum[j].Sum - listpresent[j].Present

                                };
                                j++;
                                qlp.Add(qlpdetail);
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


            txt.Text = "Danh sách phòng: \nNgày: " + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + "\nCa: " + DateTime.Now.Hour.ToString() + "h " + DateTime.Now.Minute.ToString() + "p";
            lvAdapter = new ActivityCustomListview(this, qlp);
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
                    txt.Text = "Danh sách phòng: \nNgày: "+ DateTime.Now.Day.ToString() +"/" +DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString()  + "\nCa: " +
                     DateTime.Now.Hour.ToString() + "h " + DateTime.Now.Minute.ToString()+ "p";

                    //string connectstring = @"data source=125.212.221.212;initial catalog=admin_thitoeic;user id=thitoeic;password=T@Nguyenkinh15;Connect Timeout=60";
                    //string id = "ban đầu";
                    //string sql = "SELECT * FROM Image";

                    //using (SqlConnection con = new SqlConnection(connectstring))
                    //{
                    //    try
                    //    {
                    //        con.Open();

                    //        using (SqlCommand comando = new SqlCommand(sql, con))
                    //        {
                    //            using (SqlDataReader reader = comando.ExecuteReader())
                    //            {
                    //                while (reader.Read())
                    //                {
                    //                    id = reader.GetString(1);
                    //                }
                    //            }
                    //        }

                    //        con.Close();
                    //    }
                    //    catch (SqlException)
                    //    {
                    //        id = "không connect";
                    //    }

                    //}
                    //items[5] = id;
                    //listitem[1] = id;
                    //spinAdapter.NotifyDataSetChanged();
                    //lvAdapter.NotifyDataSetChanged();
                });
            }
           
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
         {
            Intent Detailroom = new Intent(this, typeof(Detailroom));
            Detailroom.PutExtra("Room",e.Position.ToString());
            StartActivity(Detailroom);
        }

        void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //Spinner spinner = (Spinner)sender;
            //string toast = string.Format("Selected car is {0}", listitem[e.Position]);
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

    }
}