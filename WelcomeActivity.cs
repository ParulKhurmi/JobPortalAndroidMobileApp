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
    [Activity(Label = "WelcomeActivitys")]
    public class WelcomeActivity : Activity
    {
        Button editButton;
        Button exploreJobsButton;
        TextView lblurInfo;
        User usrInfo = new User();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.welcome);
            editButton = FindViewById<Button>(Resource.Id.btnEditID);
            exploreJobsButton = FindViewById<Button>(Resource.Id.btnExploreJobsID);
            lblurInfo = FindViewById<TextView>(Resource.Id.lblurInfo);

            
            DbHelperClass objDBHelper = new DbHelperClass(this);

            string name = Intent.GetStringExtra("usrname");
            string password = Intent.GetStringExtra("password");

            usrInfo = objDBHelper.selectMyValues(name, password);
            if (usrInfo.role == "employee")
            {
                exploreJobsButton.Text = "Explore and Apply Jobs";
            }
            else {
                exploreJobsButton.Text = "Manage your Job Postings";
            }

            lblurInfo.Text = "UserName: "+usrInfo.name + "\n Age: " + usrInfo.age +
                             "\n Email: " + usrInfo.email + "\n Password: " + usrInfo.password +
                             "\n Phone Number: " + usrInfo.phone;

            exploreJobsButton.Click += delegate
            {
                if (usrInfo.role == "employee")
                {
                    Intent exploreJobsScreen = new Intent(this, typeof(ExploreJobsToApplyActivity));
                    int userid = Intent.GetIntExtra("recentuserid", 0);
                    if(userid == 0)
                    {
                        exploreJobsScreen.PutExtra("recentuserid", usrInfo.uderId);
                    }
                    StartActivity(exploreJobsScreen);
                }
                else {
                    Intent manageJobsScreen = new Intent(this, typeof(ManageJobPostingsActivity));
                    StartActivity(manageJobsScreen);
                }
            };

           editButton.Click += delegate
            {
                Intent editScreen = new Intent(this, typeof(UpdateUserInfoActivity));
                editScreen.PutExtra("usrname", name);
                editScreen.PutExtra("password", password);
                StartActivity(editScreen);
            };
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            DbHelperClass objDBHelper = new DbHelperClass(this);

            string name = Intent.GetStringExtra("usrname");
            string password = Intent.GetStringExtra("password");

            usrInfo = objDBHelper.selectMyValues(name, password);
            if (usrInfo.role == "employee")
            {
                // set the menu layout on Main Activity  
                MenuInflater.Inflate(Resource.Menu.menu, menu);
                return base.OnCreateOptionsMenu(menu);
            }
            else
            {
                // set the menu layout on Main Activity  
                MenuInflater.Inflate(Resource.Menu.menu, menu);
                return base.OnCreateOptionsMenu(menu);
            }
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuItem1:
                    {
                        // add your code  
                        return true;
                    }
                case Resource.Id.menuItem2:
                    {
                        Intent logoutScreen = new Intent(this, typeof(LoginActivity));
                        logoutScreen.PutExtra("recentuserid", 0);
                        StartActivity(logoutScreen);
                        return true;
                    }
                case Resource.Id.menuItem3:
                    {
                        Intent editScreen = new Intent(this, typeof(UpdateUserInfoActivity));
                        string name = Intent.GetStringExtra("usrname");
                        string password = Intent.GetStringExtra("password");
                        editScreen.PutExtra("usrname", name);
                        editScreen.PutExtra("password", password);
                        StartActivity(editScreen);
                        return true;
                    }
                case Resource.Id.menuItem4:
                    {
                        Intent exploreJobsScreen = new Intent(this, typeof(ExploreJobsToApplyActivity));
                        exploreJobsScreen.PutExtra("recentuserid", usrInfo.uderId);
                        StartActivity(exploreJobsScreen);
                        return true;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}