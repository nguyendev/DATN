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
    class ActivityCustomSpinner : BaseAdapter
    {
        private Activity context;
        private string[] listitem;
        public ActivityCustomSpinner(Activity context, string[] listitem)
        {
            this.context = context;
            this.listitem = listitem;

        }
        public override int Count
        {
            get
            {
                return listitem.Length;
            }
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return listitem.Length;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            
            if (position > 0)
            {
                var view = context.LayoutInflater.Inflate(Resource.Layout.dropdown, parent, false);
                TextView txtTitle = (TextView)view.FindViewById(Resource.Id.textView1);
                Button btnDongy = (Button)view.FindViewById(Resource.Id.button1);
                txtTitle.Text = (listitem[position]).ToString();
                btnDongy.Click += (s, e) => {
                    Toast.MakeText(context, "You Clicked at", ToastLength.Long).Show();
                };
                return view;
            }
            else
            {
                var view = context.LayoutInflater.Inflate(Resource.Layout.choose, parent, false);

                return view;    
            }
                
            
        }
    }

}