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
    public class FragmentThree:Fragment
    {
        List<Jobs> jobList = new List<Jobs>();
        Activity context;
        List<Jobs> filterList;
        SearchView search_job;
        ListView myListView;
        public FragmentThree(List<Jobs> theJobList, Activity maincontext)
        {
            jobList = theJobList;
            context = maincontext;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            //  return base.OnCreateView(inflater, container, savedInstanceState);
            View thirdView = inflater.Inflate(Resource.Layout.ThreeFragment, container, false);
            myListView = thirdView.FindViewById<ListView>(Resource.Id.listView1);
            search_job = thirdView.FindViewById<SearchView>(Resource.Id.mySearch);

            int empid = context.Intent.GetIntExtra("recentuserid", 0);

            DbHelperClass dbhelper = new DbHelperClass(context);
            jobList = dbhelper.selectAllJobsAppliedByJobSeeker(empid);
            //if (jobList.Count < 1)
            //{
            //    jobList.Add(new Jobs(1001, "JobTitle1", "JobDescription1", "JobType1"));
            //    jobList.Add(new Jobs(1002, "JobTitle2", "JobDescription2", "JobType3"));
            //    jobList.Add(new Jobs(1003, "JobTitle3", "JobDescription2", "JobType3"));
            //}

            var myAdatper = new MyCustomerAdapter(context, jobList);

            myListView.SetAdapter(myAdatper);
            search_job.QueryTextChange += mySearchBarAction;
            return thirdView;
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
            var myNewAdatper = new MyCustomerAdapter(context, filterList);
            myListView.SetAdapter(myNewAdatper);
        }
    }
}