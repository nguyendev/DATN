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
    class ActivityCustomListview : BaseAdapter<string>
    {
        private Activity context;
        List<qlp> qlp;
        public ActivityCustomListview(Activity context, List<qlp> qlp)
        {
            this.context = context;
            this.qlp = qlp;

        }

        public override string this[int position]
        {
            get
            {
                return qlp[position].ToString();
            }
        }
        public override int Count
        {
            get
            {
                return qlp.Count;
            }
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return qlp.Count;
        }

        public override long GetItemId(int position)
        {
            return qlp.Count;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = context.LayoutInflater.Inflate(Resource.Layout.Overviewroom, parent, false);
            TextView txt1 = (TextView)view.FindViewById(Resource.Id.textView5);  // vắng
            TextView txt2 = (TextView)view.FindViewById(Resource.Id.textView6);  // phạm luật
            TextView txt3 = (TextView)view.FindViewById(Resource.Id.textView7); // làm xong
            TextView txt4 = (TextView)view.FindViewById(Resource.Id.textView8);  // Phòng thi
            txt1.Text = (qlp[position].Absent.ToString());
            //txt2.Text = (qlp[position].Offense.ToString());
            //txt3.Text = (qlp[position].Done.ToString());
            txt4.Text = (qlp[position].Rooname.ToString());
            return view;
            
            
                
            
        }
    }

}