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
using Android.Provider;
using Android.Support.V7.App;
using Android.Graphics;


namespace QuanLiPhongThi_DATN
{
    [Activity(Label = "Recognitionface", Theme = "@android:style/Theme.Holo.Light.NoActionBar.Fullscreen")]
    public class Recognitionface : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Recognitionface);
            // Create your application here

            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }

        
    }
}