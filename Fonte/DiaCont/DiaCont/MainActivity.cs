using System;
using Android.App;
using Android.Content;
using Android.Runtime;
//using DiaCont.Database;
using DiaCont.Models;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinAndroid;

namespace DiaCont
{
    [Activity(Label = "DiaCont", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //private Button login;
               
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            ActionBar.Hide();
                        
            //Set our view from the "main" layout resource
            SetContentView(Resource.Layout.login);

            FragmentTransaction transaction = this.FragmentManager.BeginTransaction();
            SlidingTabFragment fragment = new SlidingTabFragment();
            transaction.Replace(Resource.Id.login_layout, fragment);
            transaction.Commit();

            //login = FindViewById<Button>(Resource.Id.loginLoginButton);
            //login.Click += (object sender, EventArgs args) =>
            //{
            //    builder.SetMessage("Logando...");
            //};

            // Get our button from the layout resource,
            // and attach an event to it
            // Button button = FindViewById<Button>(Resource.Id.loginLoginButton);
            

        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return base.OnCreateOptionsMenu(menu);
        }
    }
}

