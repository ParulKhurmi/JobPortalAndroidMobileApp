using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1
{
    [Activity(Label = "JobDetailActivity")]
    public class JobDetailActivity : Activity
    {
        Button btnApplyJobsID;       
        TextView lbljobInfo;
        List<Jobs> jobList = new List<Jobs>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.JobDetail);
            btnApplyJobsID = FindViewById<Button>(Resource.Id.btnApplyJobsID);
            lbljobInfo = FindViewById<TextView>(Resource.Id.lbljobInfo);

            int jobid = Intent.GetIntExtra("jobid", 0);

            DbHelperClass dbhelper = new DbHelperClass(this);
            jobList = dbhelper.selectJobDetailsByJobId(jobid);

            foreach (var item in jobList) {
                lbljobInfo.Text = "\n Job Title : "+item.title +"\n Job Description : "+item.description+"\n Job Type : "+item.jobtype;
            }
           

            btnApplyJobsID.Click += delegate
            {
                Intent newScreen = new Intent(this, typeof(LoginActivity));              
                StartActivity(newScreen);
            };
        }
   }
}