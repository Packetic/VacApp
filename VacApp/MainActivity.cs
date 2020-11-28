using System;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace VacApp
{
    [Activity(Label = "XMLParser", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView id_1, id_2, id_3, id_4, id_5, id_6, ser_field;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            id_1 = FindViewById<TextView>(Resource.Id.Offer1);
            id_2 = FindViewById<TextView>(Resource.Id.Offer2);
            id_3 = FindViewById<TextView>(Resource.Id.Offer3);
            id_4 = FindViewById<TextView>(Resource.Id.Offer4);
            id_5 = FindViewById<TextView>(Resource.Id.Offer5);
            id_6 = FindViewById<TextView>(Resource.Id.Offer6);

            ser_field = FindViewById<TextView>(Resource.Id.SerializeField);

            FindViewById<Button>(Resource.Id.ButtonParse).Click += async (o, e) =>
            {
                var result = await XmlParser("https://yastatic.net/market-export/_/partner/help/YML.xml");
                string[] IdList = result.Split(" ");
                id_1.Text = IdList[0];
                id_2.Text = IdList[1];
                id_3.Text = IdList[2];
                id_4.Text = IdList[3];
                id_5.Text = IdList[4];
                id_6.Text = IdList[5];
            };
        }

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
