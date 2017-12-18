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
    class qlp
    {
        public string Rooname { get; set; }  // tên phòng
        public int Absent { get; set; }  // thí sinh vắng mặt
        public string Offense { get; set; } // phạm luật
        public string Done { get; set; }    // hoàn thành bài thi
        public int Present { get; set; } // thí sinh có mặt
        public int Sum { get; set; } //Tổng thí sinh dk tại phòng







    }
}