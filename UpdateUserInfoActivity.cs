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

namespace App1
{
    [Activity(Label = "UpdateUserInfoActivity")]
    public class UpdateUserInfoActivity : Activity
    {
        Button btnUpdateID;
        EditText txtUpdateNameID;
        EditText txtUpdateAgeID;
        EditText txtUpdateEmailID;
        EditText txtUpdatePasswordID;
        EditText txtUpdatePhoneID;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.UpdateActivity);

            btnUpdateID = FindViewById<Button>(Resource.Id.btnUpdateID);
            txtUpdateNameID = FindViewById<EditText>(Resource.Id.txtUpdateNameID);
            txtUpdateAgeID = FindViewById<EditText>(Resource.Id.txtUpdateAgeID);
            txtUpdateEmailID = FindViewById<EditText>(Resource.Id.txtUpdateEmailID);
            txtUpdatePasswordID = FindViewById<EditText>(Resource.Id.txtUpdatePasswordID);
            txtUpdatePhoneID = FindViewById<EditText>(Resource.Id.txtUpdatePhoneID);
            txtUpdateEmailID.Enabled = false;

            User usrInfo = new User();
            DbHelperClass objDBHelper = new DbHelperClass(this);

            string name = Intent.GetStringExtra("usrname");
            string password = Intent.GetStringExtra("password");
            usrInfo = objDBHelper.selectMyValues(name, password);

            txtUpdateNameID.Text = usrInfo.name;
            txtUpdatePasswordID.Text = usrInfo.password;
            txtUpdateEmailID.Text = usrInfo.email;
            txtUpdateAgeID.Text = usrInfo.age;
            txtUpdatePhoneID.Text = usrInfo.phone;
            int userId = usrInfo.uderId;

            btnUpdateID.Click += delegate
            {
                string username = txtUpdateNameID.Text;
                string passwd = txtUpdatePasswordID.Text;
                string email = txtUpdateEmailID.Text;
                string age = txtUpdateAgeID.Text;
                string phone = txtUpdatePhoneID.Text;

                usrInfo = objDBHelper.updateMyValues(userId, username, passwd,email,age,phone);

                Intent wlcmScreen = new Intent(this, typeof(WelcomeActivity));
                wlcmScreen.PutExtra("usrname", email);
                wlcmScreen.PutExtra("password", passwd);
                StartActivity(wlcmScreen);
            };
        }
    }
}