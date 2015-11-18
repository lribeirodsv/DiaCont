using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DataManager;


namespace DiaCont
{
    [Activity(Label = "DiaCont", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            
            base.OnCreate(bundle);
            DatabaseManager mydata = new DatabaseManager();
            mydata.SetContext(this);
            ActionBar.Hide();

            //Set our view from the "main" layout resource
            SetContentView(Resource.Layout.login);
            /*
            SlidingTabFragment fragment = new SlidingTabFragment();
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.login_layout, fragment);
            transaction.Commit();
            */

            // Get our button from the layout resource,
            // and attach an event to it
            // Button button = FindViewById<Button>(Resource.Id.loginLoginButton);
            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
             return base.OnCreateOptionsMenu(menu);
        }

    }
}

