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
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        EditText nameOfUser;
        EditText ageOfUser;
        EditText emailOfUser;
        EditText PasswordOfUser;
        EditText phoneOfUser;
        Button registerBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.register);

            nameOfUser = FindViewById<EditText>(Resource.Id.txtNameID);
            ageOfUser = FindViewById<EditText>(Resource.Id.txtAgeID);
            emailOfUser = FindViewById<EditText>(Resource.Id.txtEmailID);
            PasswordOfUser = FindViewById<EditText>(Resource.Id.txtPasswordID);
            registerBtn = FindViewById<Button>(Resource.Id.btnRegisterID);
            phoneOfUser = FindViewById<EditText>(Resource.Id.txtPhoneID);
            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = dialog.Create();

            RadioButton radio_employee = FindViewById<RadioButton>(Resource.Id.radio_employee);
            RadioButton radio_employer = FindViewById<RadioButton>(Resource.Id.radio_employer);
            string role="";

            radio_employee.Click += delegate {
                role = "employee";
            };
            radio_employer.Click += delegate {
                role = "employer";
            };

            registerBtn.Click += delegate
            {
                var userName = nameOfUser.Text;
                var userAge = ageOfUser.Text;
                var userEmail = emailOfUser.Text;
                var paswd = PasswordOfUser.Text;
                var phone = phoneOfUser.Text;
                if (String.IsNullOrEmpty(nameOfUser.Text))
                {
                    alert.SetTitle("Error");
                    alert.SetMessage("Please enter the username");
                    alert.SetButton("OK", (c, ev) => { });
                    alert.Show();
                }
                else if (String.IsNullOrEmpty(ageOfUser.Text))
                {
                    alert.SetTitle("Error");
                    alert.SetMessage("Please enter the age");
                    alert.SetButton("OK", (c, ev) => { });
                    alert.Show();
                }
                else if (String.IsNullOrEmpty(emailOfUser.Text))
                {
                    alert.SetTitle("Error");
                    alert.SetMessage("Please enter the Email");
                    alert.SetButton("OK", (c, ev) => { });
                    alert.Show();
                }
                else if (String.IsNullOrEmpty(PasswordOfUser.Text))
                {
                    alert.SetTitle("Error");
                    alert.SetMessage("Please enter the password");
                    alert.SetButton("OK", (c, ev) => { });
                    alert.Show();
                }
                else
                {
                    DbHelperClass objDBHelper = new DbHelperClass(this);
                    bool IsRegistered = objDBHelper.IsAlreayRegistered(userName, userEmail);
                    bool NoUniqueName = objDBHelper.IsValidUsername(userName);
                    
                    if (IsRegistered)
                    {
                        alert.SetTitle("Already Registered");
                        alert.SetMessage("Sorry! You are already registered.");
                        alert.SetButton("OK", (c, ev) =>
                        {
                            Intent loginScreen = new Intent(this, typeof(LoginActivity));
                            StartActivity(loginScreen);
                        });
                        alert.Show();

                    }
                    else
                    {
                        if (NoUniqueName)
                        {
                            alert.SetTitle("Sorry");
                            alert.SetMessage("Please enter unique username");
                            alert.SetButton("OK", (c, ev) => { });
                            alert.Show();
                        }
                        else
                        {
                            objDBHelper.insertMyValues(userName, userEmail, userAge, paswd, role,phone);
                            //objDBHelper.selectMyValues();
                            Intent loginScreen = new Intent(this, typeof(LoginActivity));
                            StartActivity(loginScreen);
                        }
                    }
                }
            };
        }
    }
}