using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using Android.Views;
using System;

namespace App1
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        ListView myList;
        Button loginBtn;
        Button registerBtn;
        SearchView search_job;
        List<Jobs> jobList = new List<Jobs>();
        List<Jobs> filterList;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            //  SetContentView(Resource.Layout.activity_main);

          

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            search_job = FindViewById<SearchView>(Resource.Id.mySearch);
            loginBtn = FindViewById<Button>(Resource.Id.btnLogin);
            registerBtn = FindViewById<Button>(Resource.Id.btnRegisterID);
            myList = FindViewById<ListView>(Resource.Id.listViewID);
            // Get our button from the layout resource,
            registerBtn.Click += delegate
            {
                Intent registerScreen = new Intent(this, typeof(RegisterActivity));
                StartActivity(registerScreen);
            };
            loginBtn.Click += delegate
            {
                Intent loginScreen = new Intent(this, typeof(LoginActivity));
                StartActivity(loginScreen);
            };
            DbHelperClass dbhelper = new DbHelperClass(this);
            jobList = dbhelper.selectAllJobs();           
            var myAdatper = new MyCustomerAdapter(this, jobList);
            myList.SetAdapter(myAdatper);
            myList.ItemClick += myListViewEvent;
            search_job.QueryTextChange += mySearchBarAction;

            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.job_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("Selected Job is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
            filterList = new List<Jobs>();
            var value = spinner.GetItemAtPosition(e.Position).ToString();
            System.Console.WriteLine("value entered " + value);
            foreach (Jobs myObj in jobList)
            {
                if (myObj.title.ToLower().Contains(value.ToLower()))
                {
                    filterList.Add(myObj);
                }
            }
            var myNewAdatper = new MyCustomerAdapter(this, filterList);
            myList.SetAdapter(myNewAdatper);
        }
        public void mySearchBarAction(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            filterList = new List<Jobs>();
            var value = e.NewText;
            System.Console.WriteLine("value entered " + value);
            foreach (Jobs myObj in jobList)
            {
                if (myObj.title.ToLower().Contains(value.ToLower()))
                {
                    filterList.Add(myObj);
                }
            }
            var myNewAdatper = new MyCustomerAdapter(this, filterList);
            myList.SetAdapter(myNewAdatper);
        }
        public void myListViewEvent(object sender, AdapterView.ItemClickEventArgs e)
        {            
            Intent newScreen = new Intent(this, typeof(JobDetailActivity));
            var index = e.Position;
            Jobs value = jobList[index];
           
                newScreen.PutExtra("jobid", value.jobid);
        
            StartActivity(newScreen);
        }      
    }
    }