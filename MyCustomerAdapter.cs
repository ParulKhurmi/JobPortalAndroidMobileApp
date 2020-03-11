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
    public class MyCustomerAdapter : BaseAdapter<Jobs>
    {
        List<Jobs> myJobList;
        Activity localContext;

        public MyCustomerAdapter(Activity myContext, List<Jobs> myUsers) : base()
        {
            localContext = myContext;
            myJobList = myUsers;
        }


        public override Jobs this[int position]
        {
            get { return myJobList[position]; }
        }

        public override int Count
        {
            get { return myJobList.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }



        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            Jobs myObject = myJobList[position];

            View myView = convertView; // re-use an existing view, if one is

            if (myView == null)
            {
                myView = localContext.LayoutInflater.Inflate(Resource.Layout.mylistView, null);
               
                myView.FindViewById<ImageView>(Resource.Id.jobImage).SetImageResource(myObject.jobimage);

                myView.FindViewById<TextView>(Resource.Id.jobTitle).Text = myObject.title;
               // myView.FindViewById<ImageView>(Resource.Id.myImage).SetImageResource(myObject.profileImage);
                myView.FindViewById<TextView>(Resource.Id.jobDescription).Text = myObject.description;
                myView.FindViewById<TextView>(Resource.Id.jobType).Text = myObject.jobtype;
               
            }

            return myView;
        }

    }
}