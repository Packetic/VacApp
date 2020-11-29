using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VacApp
{
    [Activity(Label = "JSON")]
    public class SerializeActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.SerializeLayout);

            // Throwing Sample to SerializeLayout
            var JsonField = FindViewById<TextView>(Resource.Id.JsonField);
            string sample = Intent.GetStringExtra("Sample");
            JsonField.Text = sample;

            // Back button
            FindViewById<Button>(Resource.Id.back_to_main).Click += delegate { StartActivity(typeof(MainActivity)); };


        }
    }
}