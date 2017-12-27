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
using Java.Lang;

namespace QuanLiPhongThi_DATN
{
    class ActivityCustomListviewDetailRoom : BaseAdapter<string>
    {
        private Activity context;
        List<ttts> ttts;
        public ActivityCustomListviewDetailRoom(Activity context, List<ttts> ttts)
        {
            this.context = context;
            this.ttts = ttts;

        }

        public override string this[int position]
        {
            get
            {
                return ttts[position].ToString();
            }
        }
        public override int Count
        {
            get
            {
                return ttts.Count;
            }
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return ttts.Count;
        }

        public override long GetItemId(int position)
        {
            return ttts.Count;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = context.LayoutInflater.Inflate(Resource.Layout.Detailroomfinal, parent, false);
            if(ttts[position].absent == 0)
                view.SetBackgroundColor(Android.Graphics.Color.YellowGreen);
            TextView txt1 = (TextView)view.FindViewById(Resource.Id.textView5);  // ho va ten
            TextView txt2 = (TextView)view.FindViewById(Resource.Id.textView6);  // so dien thoai
            //TextView txt3 = (TextView)view.FindViewById(Resource.Id.textView7); // ngay sinh
            TextView txt4 = (TextView)view.FindViewById(Resource.Id.textView8);  // gioi tinh
            txt1.Text = (ttts[position].Midname +" " +ttts[position].Name);
            txt2.Text = (ttts[position].Phone.ToString());
            //txt3.Text = (ttts[position].Birday.ToString());
            if(ttts[position].Sex == 0)
            txt4.Text = ("Nam");
            else
                txt4.Text = ("Nữ");
            return view;
            
            
                
            
        }
    }

}