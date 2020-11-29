using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VacApp
{
    [Activity(Label = "XMLParser", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        // creating fields
        TextView id_1, id_2, id_3, id_4, id_5, id_6;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            // creating field references
            id_1 = FindViewById<TextView>(Resource.Id.Offer1);
            id_2 = FindViewById<TextView>(Resource.Id.Offer2);
            id_3 = FindViewById<TextView>(Resource.Id.Offer3);
            id_4 = FindViewById<TextView>(Resource.Id.Offer4);
            id_5 = FindViewById<TextView>(Resource.Id.Offer5);
            id_6 = FindViewById<TextView>(Resource.Id.Offer6);

            var JsonField = FindViewById<TextView>(Resource.Id.JsonField); // to change text on second window
            var result = await XmlParser("https://yastatic.net/market-export/_/partner/help/YML.xml");
            string[] IdList = result.Split(" ");

            // initializing new intent to navigate between pages
            Intent JsonWindow = new Intent(this, typeof(SerializeActivity));

            // button click
            FindViewById<Button>(Resource.Id.ButtonParse).Click += (o, e) =>
            {
                id_1.Text = IdList[0];
                id_2.Text = IdList[1];
                id_3.Text = IdList[2];
                id_4.Text = IdList[3];
                id_5.Text = IdList[4];
                id_6.Text = IdList[5];
            };

            // First element to JSON
            FindViewById<Button>(Resource.Id.id1).Click += (o, e) => 
            {
                var JsonStr = JsonConvert.SerializeObject(id_1.Text);
                JsonWindow.PutExtra("Sample", JsonStr);
                StartActivity(JsonWindow);
            };

            // Second button
            FindViewById<Button>(Resource.Id.id2).Click += (o, e) =>
            {
                var JsonStr = JsonConvert.SerializeObject(id_2.Text);
                JsonWindow.PutExtra("Sample", JsonStr);
                StartActivity(JsonWindow);
            };

            // Third button 
            FindViewById<Button>(Resource.Id.id3).Click += (o, e) =>
            {
                var JsonStr = JsonConvert.SerializeObject(id_3.Text);
                JsonWindow.PutExtra("Sample", JsonStr);
                StartActivity(JsonWindow);
            };

            // Fourth button
            FindViewById<Button>(Resource.Id.id4).Click += (o, e) =>
            {
                var JsonStr = JsonConvert.SerializeObject(id_4.Text);
                JsonWindow.PutExtra("Sample", JsonStr);
                StartActivity(JsonWindow);
            };

            // Fifth button
            FindViewById<Button>(Resource.Id.id5).Click += (o, e) =>
            {
                var JsonStr = JsonConvert.SerializeObject(id_5.Text);
                JsonWindow.PutExtra("Sample", JsonStr);
                StartActivity(JsonWindow);
            };

            // Sixth button
            FindViewById<Button>(Resource.Id.id6).Click += (o, e) =>
            {
                var JsonStr = JsonConvert.SerializeObject(id_6.Text);
                JsonWindow.PutExtra("Sample", JsonStr);
                StartActivity(JsonWindow);
            };
        }

        // function for parsing an XML file from web via URL
        public async Task<string> XmlParser(string URL)
        {
            // block of settings 
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.XmlResolver = new XmlUrlResolver();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationType = ValidationType.DTD;
            settings.Async = true;
            string XMLtext = "";

            // block of parsing
            using (XmlReader reader = XmlReader.Create(URL, settings))
            {
                while (await reader.ReadAsync())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "offer" && reader.MoveToFirstAttribute())
                    {
                        XMLtext += reader.GetAttribute("id");
                        XMLtext += " ";
                    }
                }
            }
            return XMLtext;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
