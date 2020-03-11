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
    [Activity(Label = "ExploreJobsToApplyActivity")]
    public class ExploreJobsToApplyActivity: Activity
    {
        Fragment[] _fragments;

        List<Jobs> myJobList = new List<Jobs>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(Android.Views.WindowFeatures.ActionBar);
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            base.OnCreate(savedInstanceState);

            _fragments = new Fragment[]
              {
                    new FragmentOne(myJobList, this),
                    new FragmentTwo(myJobList, this),
                    new FragmentThree(myJobList, this)
              };

            AddTabToActionBar("Jobs");// Resource.Drawable.thesecret); //First Tab
            AddTabToActionBar("Saved Jobs");//, Resource.Drawable.happiness); //Second Tab
            AddTabToActionBar("Applied Jobs");//, Resource.Drawable.happiness); //Second Tab

            SetContentView(Resource.Layout.explorejobstoapply);
           // string userid = Intent.GetStringExtra("recentuserid");
        }
        void AddTabToActionBar(string tabTitle)//, int ImageId)
        {
            ActionBar.Tab tab = ActionBar.NewTab().SetText(tabTitle);

           // tab.SetIcon(ImageId);

            tab.TabSelected += TabOnTabSelected;

            ActionBar.AddTab(tab);
        }
        void TabOnTabSelected(object sender, ActionBar.TabEventArgs tabEventArgs)
        {
            ActionBar.Tab tab = (ActionBar.Tab)sender;

            //Log.Debug(Tag, "The tab {0} has been selected.", tab.Text);
            Fragment frag = _fragments[tab.Position];

            tabEventArgs.FragmentTransaction.Replace(Resource.Id.frameLayout1, frag);
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // set the menu layout on Main Activity  
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);

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
                        Intent exploreJobsScreen = new Intent(this, typeof(LoginActivity));
                        exploreJobsScreen.PutExtra("recentuserid", 0);
                        StartActivity(exploreJobsScreen);
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
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}