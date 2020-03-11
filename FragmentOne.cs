using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace App1
{
    public class FragmentOne : Fragment
    {
        List<Jobs> jobList = new List<Jobs>();
        Activity context;
        private object dialogClickListener;
        List<Jobs> filterList;
        SearchView search_job;
        ListView myListView;

        public FragmentOne(List<Jobs> theJobList, Activity maincontext)
        {                     
            jobList = theJobList;
            context = maincontext;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
            View firstView = inflater.Inflate(Resource.Layout.FragmentOne, container, false);
            search_job = firstView.FindViewById<SearchView>(Resource.Id.mySearch);
            myListView = firstView.FindViewById<ListView>(Resource.Id.listView1);

            DbHelperClass dbhelper = new DbHelperClass(context);
            jobList = dbhelper.selectAllJobs();
            //if (jobList.Count < 1)
            //{
            //    jobList.Add(new Jobs(1001, "JobTitle1", "JobDescription1", "JobType1"));
            //    jobList.Add(new Jobs(1002, "JobTitle2", "JobDescription2", "JobType3"));
            //    jobList.Add(new Jobs(1003, "JobTitle3", "JobDescription2", "JobType3"));
            //}

            var myAdatper = new MyCustomerAdapter(context, jobList);

            myListView.SetAdapter(myAdatper);
            myListView.ItemClick += myListViewEvent;
            search_job.QueryTextChange += mySearchBarAction;
            return firstView;
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
        void myListViewEvent(object sender, AdapterView.ItemClickEventArgs e)
        {
            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(context);
            Android.App.AlertDialog alert = dialog.Create();
            var index = e.Position;
            Jobs value = jobList[index];
            //Intent newScreen = new Intent(Activity, typeof(ApplyNowActivity));
            //newScreen.PutExtra("jobid", value.jobid);
            //StartActivity(newScreen);
            alert.SetTitle("Job for: "+value.title);
            alert.SetMessage("Do you want to apply this job or later?");
            alert.SetButton("Apply Now", (c, ev) => {
                int empid = context.Intent.GetIntExtra("recentuserid",0);               
                DbHelperClass dbhelper = new DbHelperClass(context);
                User UsrObj = new User();
                UsrObj=dbhelper.getUserById(empid);
                bool jobApp = dbhelper.insertJobApplication(value.jobid, empid, value.title, value.description,value.jobimage, value.jobtype);
                    SmtpClient client = new SmtpClient();
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;                  
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential("parulkhurmi1991@gmail.com", "Parul@$101");
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(UsrObj.email);
                        mail.To.Add(new MailAddress("khurmiparul@gmail.com"));
                        mail.Subject = "Job Application For "+value.title;
                        mail.Body = "From: " + UsrObj.name + "<br/>Email: " + UsrObj.email + "<br/>Job Description: " + value.description;
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.Normal;                                     
                   client.Send(mail);               
            });
            alert.SetButton2("Save this Job", (c, ev) => {
                //string id = context.Intent.GetStringExtra("recentuserid");
                //int empid = Convert.ToInt32(id);
                int empid = context.Intent.GetIntExtra("recentuserid", 0);
                DbHelperClass dbhelper = new DbHelperClass(context);
                bool saveJob = dbhelper.insertSavedJobApplication(value.jobid, empid, value.title, value.description,value.jobimage, value.jobtype);
                alert.SetTitle("Saved Successfully");
                alert.SetMessage("Selected job saved successfully");
                alert.SetButton("OK", (cd, evv) => { });
                alert.Show();
            });
            alert.SetButton3("Cancel", (c, ev) => { });
            alert.Show();
        }

    }
}