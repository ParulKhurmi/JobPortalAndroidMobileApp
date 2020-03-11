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
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        EditText userName;
        EditText password;
        Button loginBtn;
        Button registerBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_Login);

            userName = FindViewById<EditText>(Resource.Id.userNameID);
            password = FindViewById<EditText>(Resource.Id.passwordID);
            loginBtn = FindViewById<Button>(Resource.Id.loginBtnID);
            registerBtn = FindViewById<Button>(Resource.Id.btnRegisterID);

            registerBtn.Click += delegate
            {
                Intent registerScreen = new Intent(this, typeof(RegisterActivity));
                StartActivity(registerScreen);
            };
            User usrInfo;
            loginBtn.Click += delegate
            {
                usrInfo = new User();
                var uName = userName.Text;
                var pswd = password.Text;
                DbHelperClass objDBHelper = new DbHelperClass(this);
                bool IsUserRegistered = objDBHelper.IsValidUsr(uName, pswd);
                bool IsValidName = objDBHelper.IsValidUsername(uName);


                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();

                if (IsUserRegistered)
                {
                    Intent welcomeScreen = new Intent(this, typeof(WelcomeActivity));
                    welcomeScreen.PutExtra("usrname", uName);
                    welcomeScreen.PutExtra("password", pswd);
                    StartActivity(welcomeScreen);
                }
                else if (!IsValidName)
                {
                    alert.SetTitle("Error");
                    alert.SetMessage("Please enter the correct username(Email)");
                    alert.SetButton("OK", (c, ev) => { });
                    alert.Show();
                }
                else if (IsValidName)
                {
                    alert.SetTitle("Error");
                    alert.SetMessage("Please enter the correct password");
                    alert.SetButton("OK", (c, ev) => { });
                    alert.Show();
                }
                else
                {
                    alert.SetTitle("Error");
                    alert.SetMessage("Please enter the correct username and correct password");
                    alert.SetButton("OK", (c, ev) => { });
                    alert.Show();
                }
                //objDBHelper.selectMyValues();

                //string name = "admin";
                //string passwd = "password";
                ////admin
                //if ((uName.ToLower().Equals(name.ToLower())) && (pswd.ToLower().Equals(passwd.ToLower())))
                //{ //equal 
                //    Intent welcomeScreen = new Intent(this, typeof(WelcomeActivity));
                //    StartActivity(welcomeScreen);
                //}
                //else
                //{
                //    //not equal
                //    Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                //    Android.App.AlertDialog alert = dialog.Create();
                //    if (!(uName.ToLower().Equals(name.ToLower())) && (!(pswd.ToLower().Equals(passwd.ToLower()))))
                //    {
                //        alert.SetTitle("Error");
                //        alert.SetMessage("Please enter the correct user name and password");
                //        alert.SetButton("OK", (c, ev) => { });
                //        alert.Show();
                //    }
                //    else if (!(uName.ToLower().Equals(name.ToLower())))
                //    {
                //        alert.SetTitle("Error");
                //        alert.SetMessage("Please enter the correct user name");
                //        alert.SetButton("OK", (c, ev) => { });
                //        alert.Show();
                //    }
                //    else
                //    {
                //        alert.SetTitle("Error");
                //        alert.SetMessage("Please enter the correct password");
                //        alert.SetButton("OK", (c, ev) => { });
                //        alert.Show();
                //    }
                //}

                //System.Console.WriteLine("Click Event");
                //System.Console.WriteLine("Entered UserName is" + uName + " And Password is " + pswd);
            };
        }
    }
}