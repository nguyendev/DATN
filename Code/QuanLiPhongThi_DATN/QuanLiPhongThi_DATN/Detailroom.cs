using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace QuanLiPhongThi_DATN
{
    [Activity(Label = "Detailroom", Theme = "@android:style/Theme.Holo.Light.NoActionBar.Fullscreen")]
    public class Detailroom : Activity
    {
        List<ttts> ttts = new List<ttts>();
        ActivityCustomListviewDetailRoom Adapter;
        TextView txtRoom;
        ListView lv;
        string connectstring = @"data source=125.212.221.212;initial catalog=admin_thitoeic;user id=thitoeic;password=T@Nguyenkinh15;Connect Timeout=60";
        string id = "ban đầu";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Detailroom);
            lv = FindViewById<ListView>(Resource.Id.listView1);
            txtRoom = FindViewById<TextView>(Resource.Id.textView1);
            int i = Convert.ToInt32(Intent.GetStringExtra("Room" ?? "Not recv"));
            i++;
            txtRoom.Text = "Phòng "+ i;
            string sql1 = "select thitoeic.Contact.FirstMidName, thitoeic.Contact.LastName, thitoeic.Contact.MobilePhone, "
                        + "thitoeic.Contact.DateofBirth, thitoeic.Contact.Sex"
                        + " from thitoeic.Contact, thitoeic.Enrollment, thitoeic.Course"
                        + " where thitoeic.Contact.OwnerID = thitoeic.Enrollment.StudentID AND thitoeic.Course.CourseID = thitoeic.Enrollment.CourseID"
                        + " AND(thitoeic.Course.ExamTime BETWEEN '2017-12-30 13:30:00.0000000' AND '2017-12-30 16:30:00.0000000')"
                        + " AND thitoeic.Enrollment.RoomID = "+i+
                            " AND thitoeic.Contact.OwnerID"
                        + " NOT IN(select  thitoeic.Contact.OwnerID"
                        + " from thitoeic.Contact, thitoeic.Enrollment, thitoeic.HistoryLogin"
                        + " where thitoeic.Contact.OwnerID = thitoeic.Enrollment.StudentID AND thitoeic.Contact.OwnerID = thitoeic.HistoryLogin.StudentID"
                        + " AND(thitoeic.HistoryLogin.DateTime BETWEEN '2017-12-30 13:30:00.0000000' AND '2017-12-30 16:30:00.0000000')"
                        + " AND thitoeic.Enrollment.RoomID = "+i+")";
            string sql2 = "select  thitoeic.Contact.FirstMidName, thitoeic.Contact.LastName, thitoeic.Contact.MobilePhone,"
                            + " thitoeic.Contact.DateofBirth, thitoeic.Contact.Sex "
                            + " from thitoeic.Contact, thitoeic.Enrollment, thitoeic.HistoryLogin"
                            + " where thitoeic.Contact.OwnerID = thitoeic.Enrollment.StudentID AND thitoeic.Contact.OwnerID = thitoeic.HistoryLogin.StudentID"
                            + " AND(thitoeic.HistoryLogin.DateTime BETWEEN '2017-12-30 13:30:00.0000000' AND '2017-12-30 16:30:00.0000000')"
                            + "AND thitoeic.Enrollment.RoomID =" + i;
        
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
                                ttts tttsdetail = new ttts()
                                {
                                    Midname = reader.GetString(0),
                                    Name = reader.GetString(1),
                                    Phone = reader.GetString(2),
                                    Sex = reader.GetInt32(4),
                                    absent = 1 //1 là đã vào phòng // 0 là chưa vào phòng 
                                };

                                ttts.Add(tttsdetail);
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
                                ttts tttsdetail = new ttts()
                                {
                                    Midname = reader.GetString(0),
                                    Name = reader.GetString(1),
                                    Phone = reader.GetString(2),
                                    //Birday = reader.GetDateTime(3),
                                    Sex = reader.GetInt32(4),
                                    absent = 0 //1 là đã vào phòng // 0 là chưa vào phòng 
                                };

                                ttts.Add(tttsdetail);
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
            Adapter = new ActivityCustomListviewDetailRoom(this, ttts);
            lv.SetAdapter(Adapter);



        }

    }
}