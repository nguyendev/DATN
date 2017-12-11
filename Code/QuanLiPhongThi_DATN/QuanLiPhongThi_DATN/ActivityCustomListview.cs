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
        private List<System.String> listitem;
        public ActivityCustomListview(Activity context, List<string> listitem)
        {
            this.context = context;
            this.listitem = listitem;

        }

        public override string this[int position]
        {
            get
            {
                return listitem[position];
            }
        }
        public override int Count
        {
            get
            {
                return listitem.Count;
            }
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return listitem.Count;
        }

        public override long GetItemId(int position)
        {
            return listitem.Count;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
                var view = context.LayoutInflater.Inflate(Resource.Layout.Overviewroom, parent, false);
            TextView txt1 = (TextView)view.FindViewById(Resource.Id.textView5);  // vắng
            TextView txt2 = (TextView)view.FindViewById(Resource.Id.textView6);  // phạm luật
            TextView txt3 = (TextView)view.FindViewById(Resource.Id.textView7); // làm xong
            TextView txt4 = (TextView)view.FindViewById(Resource.Id.textView8);  // Phòng thi
            txt1.Text = (listitem[position]).ToString();
            txt2.Text = (listitem[position]).ToString();
            txt3.Text = (listitem[position]).ToString();
            txt4.Text = (listitem[position]).ToString();
            return view;
            
            
                
            
        }
    }

}