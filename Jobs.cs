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
   public class Jobs
    {
        public int jobid;
        public string title;
        public string description;
        public string jobtype;
        public int jobimage;
        public Jobs(int _jobid, string _title, string _description,int _jobimage, string _jobtype)
        {
            jobid = _jobid;
            title = _title;
            description = _description;
            jobtype = _jobtype;
            jobimage = _jobimage;
        }
        public List<Jobs> createListOfJobs()
        {
            List<Jobs> myJobList = new List<Jobs>();
            myJobList.Add(new Jobs(1,"test1","test2", Resource.Drawable.admin_pic, "test3" ));
            myJobList.Add(new Jobs(2, "test2", "test2", Resource.Drawable.admin_pic, "test2"));
            myJobList.Add(new Jobs(3, "test3", "test3", Resource.Drawable.admin_pic, "test3"));
            myJobList.Add(new Jobs(4, "test4", "test4", Resource.Drawable.admin_pic, "test4"));
            return myJobList;
        }
    }
}