using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1
{
    public class DbHelperClass : SQLiteOpenHelper
    {
        //Step 1:
        private static string _DatabaseName = "mydatabase.db";
        private const string TableName = "persontable";
        private const string ColumnID = "id";
        private const string ColumnName = "name";
        private const string ColumnEmail = "eMail";
        private const string ColumnRole = "role";
        private const string ColumnAge = "age";
        private const string ColumnPhone = "phone";
        private const string ColumnPassword = "password";

        private const string TableNameJobs = "jobstable";
        private const string ColumnJobID = "jobid";
        private const string ColumnJobName = "title";
        private const string ColumnJobDescription = "description";
        private const string ColumnJobType = "jobtype";
        private const string ColumnJobImage = "jobimage";
        private const string ColumnEmployerId = "employer_id";

        private const string TableNameJobApplication = "job_applications";
        private const string ColumnAppliedJobID = "jobid";
        private const string ColumnAppliedEmpId = "empid";
        private const string ColumnAppliedJobName = "title";
        private const string ColumnAppliedJobDescription = "description";
        private const string ColumnAppliedJobImage = "jobimage";
        private const string ColumnAppliedJobType = "jobtype";

        private const string TableNameSavedJobApplications = "savedjob_applications";
        private const string ColumnSavedJobID = "jobid";
        private const string ColumnSavedEmpId = "empid";
        private const string ColumnSavedJobName = "title";
        private const string ColumnSavedJobDescription = "description";
        private const string ColumnSavedJobImage = "jobimage";
        private const string ColumnSavedJobEmployerEmail = "employer_mail";

        public const string CreatePersonTableQuery = "CREATE TABLE " +
       TableName + " ( " + ColumnID + " INTEGER,"
           + ColumnName + " TEXT,"
           + ColumnEmail + " TEXT,"
           + ColumnRole + " TEXT,"
           + ColumnAge + " INTEGER,"
           + ColumnPhone + " INTEGER,"
           + ColumnPassword + " TEXT)";

        public const string CreateJobsTableQuery = "CREATE TABLE " +
        TableNameJobs + " ( " + ColumnJobID + " INTEGER,"
            + ColumnJobName + " TEXT,"
            + ColumnJobDescription + " TEXT,"
            + ColumnJobType + " TEXT,"
            + ColumnJobImage + " INTEGER,"
            + ColumnEmployerId + " INTEGER)";
        public const string CreateAppliedJobsTableQuery = "CREATE TABLE " +
       TableNameJobApplication + " ( " + ColumnAppliedJobID + " INTEGER,"
           + ColumnAppliedEmpId + " INTEGER,"
            + ColumnAppliedJobName + " TEXT,"
            + ColumnAppliedJobType + " TEXT,"
             + ColumnAppliedJobImage + " INTEGER,"
           + ColumnAppliedJobDescription + " TEXT)";
        public const string CreateSavedJobsTableQuery = "CREATE TABLE " +
      TableNameSavedJobApplications + " ( " + ColumnSavedJobID + " INTEGER,"
          + ColumnSavedEmpId + " INTEGER,"
           + ColumnSavedJobName + " TEXT,"
           + ColumnSavedJobEmployerEmail + " TEXT,"
             + ColumnSavedJobImage + " INTEGER,"
          + ColumnSavedJobDescription + " TEXT)";

        public const string UserDeleteQuery = "DROP TABLE IF EXISTS " + TableName;
        public const string jobDeleteQuery = "DROP TABLE IF EXISTS " + TableNameJobs;
        public const string ApplicationDeleteQuery = "DROP TABLE IF EXISTS " + TableNameJobApplication;
        public const string SavedApplicationDeleteQuery = "DROP TABLE IF EXISTS " + TableNameSavedJobApplications;

        SQLiteDatabase myDBObj;

        public DbHelperClass(Context context) :
         base(context, name: _DatabaseName, factory: null, version: 1) //Step 2;
        {
            myDBObj = WritableDatabase; // Step: 3 create a DB objects
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            // Step: 4 create a table for your database.
            db.ExecSQL(CreatePersonTableQuery);
            db.ExecSQL(CreateJobsTableQuery);
            db.ExecSQL(CreateAppliedJobsTableQuery);
            db.ExecSQL(CreateSavedJobsTableQuery);

            var insertEmployer="INSERT INTO " + TableName + " values(101,'admin','khurmiparul@gmail.com','employer'," +
                27 + "," + 1234567890 + ",'admin')";
            var insertEmployee = "INSERT INTO " + TableName + " values(201,'employee','lizzaparmar30@gmail.com','employee'," +
              24 + "," + 1234589088 + ",'employee')";
            var insertJob1 = "INSERT INTO " + TableNameJobs + " values(1001,'Developer','Develop the Application','FullTime',"+Resource.Drawable.dev1+",101)";
            var insertJob2 = "INSERT INTO " + TableNameJobs + " values(1002,'Analyst','Analyse the Application','PartTime',"+Resource.Drawable.analyst+",101)";
            var insertJob3 = "INSERT INTO " + TableNameJobs + " values(1003,'Tester','Test the Application','FullTime',"+Resource.Drawable.tester+",101)";
            var insertJob4 = "INSERT INTO " + TableNameJobs + " values(1004,'Manager','Manage the Application','FullTime',"+Resource.Drawable.xamarin+",101)";
            var insertJob5 = "INSERT INTO " + TableNameJobs + " values(1005,'Internship','Application Development','PartTime',"+Resource.Drawable.dev2+",101)";
            var insertJob6 = "INSERT INTO " + TableNameJobs + " values(1006,'Director','Direction to System','FullTime',"+Resource.Drawable.admin_pic+",101)";
            var insertJob7 = "INSERT INTO " + TableNameJobs + " values(1007,'Designer','Design the Application','FullTime',"+Resource.Drawable.designer+",101)";

            db.ExecSQL(insertEmployer);
            db.ExecSQL(insertEmployee);
            db.ExecSQL(insertJob1);
            db.ExecSQL(insertJob2);
            db.ExecSQL(insertJob3);
            db.ExecSQL(insertJob4);
            db.ExecSQL(insertJob5);
            db.ExecSQL(insertJob6);
            db.ExecSQL(insertJob7);

        }

        //Insert value into the database
        public bool insertMyValues(String name, String email, String age, String passwd, String role, String phone)
        {

            Random rnd = new Random();
            var id = rnd.Next(1, 10000);

            // Insert into persontable values (1232, 'john', 'john@gmail.com');           
            string insertStm = "INSERT INTO " + TableName + " values(" + id + ",'" + name + "','" + email + "','" + role + "'," + age + "," + phone + ",'" + passwd + "')";
            System.Console.WriteLine("My SQL INSERT -->>>>" + insertStm);
            myDBObj.ExecSQL(insertStm);

            return true;
        }
        User usr;
        public User selectMyValues(String name, String paswd)
        {
            usr = new User();
            ICursor myDBValue = myDBObj.RawQuery("Select * from " + TableName +
               " where " + ColumnEmail + "='" + name + "' AND " + ColumnPassword + "='" + paswd + "'", null);
            while (myDBValue.MoveToNext())
            {
                var IDfromDB = myDBValue.GetInt(myDBValue.GetColumnIndexOrThrow(ColumnID));
                System.Console.WriteLine(" Value Of ID FROM DB --> " + IDfromDB);
                var NamefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnName));
                System.Console.WriteLine(" Value  Of Name  FROM DB  --> " + NamefromDB);
                var AgefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnAge));
                System.Console.WriteLine(" Value  Of Age  FROM DB --> " + AgefromDB);
                var EmailfromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnEmail));
                System.Console.WriteLine(" Value  Of Email  FROM DB --> " + EmailfromDB);
                var PasswordfromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnPassword));
                System.Console.WriteLine(" Value  Of Password  FROM DB --> " + PasswordfromDB);
                var RolefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnRole));
                System.Console.WriteLine(" Value  Of Role  FROM DB --> " + RolefromDB);
                var PhonefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnPhone));
                System.Console.WriteLine(" Value  Of Phone  FROM DB --> " + PhonefromDB);
                usr.uderId = IDfromDB;
                usr.name = NamefromDB;
                usr.password = PasswordfromDB;
                usr.role = RolefromDB;
                usr.email = EmailfromDB;
                usr.age = AgefromDB;
                usr.phone = PhonefromDB;
            }
            return usr;
        }

        public User updateMyValues(int userId, string username, string passwd, string email, string age, string phone)
        {
            usr = new User();
            string updateUser = "Update " + TableName + " SET " + ColumnName + "='" + username + "',"
                + ColumnPassword + "='" + passwd + "'," + ColumnEmail + "='" + email + "',"
                + ColumnAge + "='" + age + "'," + ColumnPhone + "='" + phone + "'" + " where " + ColumnID + "=" + userId;

            ICursor myDBValue = myDBObj.RawQuery(updateUser, null);

            while (myDBValue.MoveToNext())
            {
                var IDfromDB = myDBValue.GetInt(myDBValue.GetColumnIndexOrThrow(ColumnID));
                var NamefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnName));
                var AgefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnAge));
                var EmailfromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnEmail));
                var PasswordfromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnPassword));
                var PhonefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnPhone));
                usr.uderId = IDfromDB;
                usr.name = NamefromDB;
                usr.password = PasswordfromDB;
                usr.email = EmailfromDB;
                usr.age = AgefromDB;
                usr.phone = PhonefromDB;
            }
            return usr;
        }

        public bool IsAlreayRegistered(String name, String email)
        {
            //string stmnt = "Select * from " + TableName + " where " + ColumnName + "='" + name + "' & " + ColumnEmail + "='" + email + "'";
            //System.Console.WriteLine("My SQL INSERT -->>>>" + stmnt);
            ICursor myDBSet = myDBObj.RawQuery("Select * from " + TableName +
                " where " + ColumnName + "='" + name + "' AND " + ColumnEmail + "='" + email + "'", null);
            if (myDBSet.Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool IsValidUsername(String name)
        {
            ICursor userDBSet = myDBObj.RawQuery("Select * from " + TableName +
               " where " + ColumnEmail + "='" + name + "'", null);

            if (userDBSet.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }


        }
        public User getUserById(int userid)
        {
            ICursor myDBValue = myDBObj.RawQuery("Select * from " + TableName + " where " + ColumnID + "=" + userid, null);
            usr = new User();
            while (myDBValue.MoveToNext())
            {
                var IDfromDB = myDBValue.GetInt(myDBValue.GetColumnIndexOrThrow(ColumnID));
                System.Console.WriteLine(" Value Of ID FROM DB --> " + IDfromDB);
                var NamefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnName));
                System.Console.WriteLine(" Value  Of Name  FROM DB  --> " + NamefromDB);
                var AgefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnAge));
                System.Console.WriteLine(" Value  Of Age  FROM DB --> " + AgefromDB);
                var EmailfromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnEmail));
                System.Console.WriteLine(" Value  Of Email  FROM DB --> " + EmailfromDB);
                var PasswordfromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnPassword));
                System.Console.WriteLine(" Value  Of Password  FROM DB --> " + PasswordfromDB);
                var RolefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnRole));
                System.Console.WriteLine(" Value  Of Role  FROM DB --> " + RolefromDB);
                var PhonefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnPhone));
                System.Console.WriteLine(" Value  Of Phone  FROM DB --> " + PhonefromDB);
                usr.uderId = IDfromDB;
                usr.name = NamefromDB;
                usr.password = PasswordfromDB;
                usr.role = RolefromDB;
                usr.email = EmailfromDB;
                usr.age = AgefromDB;
                usr.phone = PhonefromDB;
            }
            return usr;
        }

        public bool IsValidUsr(String name, String paswd)
        {
            ICursor userValidationSet = myDBObj.RawQuery("Select * from " + TableName +
               " where " + ColumnEmail + "='" + name + "' AND " + ColumnPassword + "='" + paswd + "'", null);
            if (userValidationSet.Count == 0)
            {
                return false;
            }
            else
            {
                while (userValidationSet.MoveToNext())
                {
                    var NamefromDB = userValidationSet.GetString(userValidationSet.GetColumnIndexOrThrow(ColumnName));
                    var PasswordfromDB = userValidationSet.GetString(userValidationSet.GetColumnIndexOrThrow(ColumnPassword));
                    System.Console.WriteLine("User Logged In with UserName --> " + NamefromDB);
                    System.Console.WriteLine("User Logged In with Password --> " + PasswordfromDB);
                    // selectMyValues();
                }
                return true;
            }
        }
        //First list of jobs on home page
        public List<Jobs> selectAllJobs()
        {
            List<Jobs> jobList = new List<Jobs>();
            ICursor myDBValue = myDBObj.RawQuery("Select * from " + TableNameJobs, null);
            while (myDBValue.MoveToNext())
            {
                var JobIDfromDB = myDBValue.GetInt(myDBValue.GetColumnIndexOrThrow(ColumnJobID));
                System.Console.WriteLine(" Value Of ID FROM DB --> " + JobIDfromDB);
                var JobNamefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnJobName));
                System.Console.WriteLine(" Value  Of Name  FROM DB  --> " + JobNamefromDB);
                var JobDescriptionfromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnJobDescription));
                System.Console.WriteLine(" Value  Of Age  FROM DB --> " + JobDescriptionfromDB);
                var JobTypefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnJobType));
                System.Console.WriteLine(" Value  Of Email  FROM DB --> " + JobTypefromDB);
                var JobImagefromDB = myDBValue.GetInt(myDBValue.GetColumnIndexOrThrow(ColumnJobImage));
                System.Console.WriteLine(" Value  Of Email  FROM DB --> " + JobImagefromDB);
                jobList.Add(new Jobs(JobIDfromDB, JobNamefromDB, JobDescriptionfromDB, JobImagefromDB, JobTypefromDB));
            }
            return jobList;
        }
        //select All Jobs Applied By JobSeeker
        public List<Jobs> selectAllJobsAppliedByJobSeeker(int userid)
        {
            List<Jobs> jobList = new List<Jobs>();
            ICursor myDBValue = myDBObj.RawQuery("Select * from " + TableNameJobApplication + " where " + ColumnAppliedEmpId + "=" + userid, null);
            while (myDBValue.MoveToNext())
            {
                var appliedJobIDfromDB = myDBValue.GetInt(myDBValue.GetColumnIndexOrThrow(ColumnAppliedJobID));
                System.Console.WriteLine(" Value Of ID FROM DB --> " + appliedJobIDfromDB);
                var appliedJobNamefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnAppliedJobName));
                System.Console.WriteLine(" Value  Of Name  FROM DB  --> " + appliedJobNamefromDB);
                var appliedJobDescriptionfromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnAppliedJobDescription));
                System.Console.WriteLine(" Value  Of Age  FROM DB --> " + appliedJobDescriptionfromDB);
                var JobImagefromDB = myDBValue.GetInt(myDBValue.GetColumnIndexOrThrow(ColumnAppliedJobImage));
                System.Console.WriteLine(" Value  Of Email  FROM DB --> " + JobImagefromDB);
                var appliedJobTypefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnAppliedJobType));
                System.Console.WriteLine(" Value  Of Email  FROM DB --> " + appliedJobTypefromDB);
                jobList.Add(new Jobs(appliedJobIDfromDB, appliedJobNamefromDB, appliedJobDescriptionfromDB, JobImagefromDB, appliedJobTypefromDB));
            }
            return jobList;
        }
        //Insert value into the database
        public bool insertJobApplication(int jobid, int empid, String title, String description,int img, String jobtype)
        {
            // Insert into persontable values (1232, 'john', 'john@gmail.com');           
            string insertStm = "INSERT INTO " + TableNameJobApplication + " values(" + jobid + "," + empid + ",'" + title + "','" + description + "',"+img+",'" + jobtype + "')";
            System.Console.WriteLine("My SQL INSERT -->>>>" + insertStm);
            myDBObj.ExecSQL(insertStm);

            return true;
        }
        //select All Jobs Saved By JobSeeker
        public List<Jobs> selectAllJobsSavedByJobSeeker(int userid)
        {
            List<Jobs> jobList = new List<Jobs>();
            ICursor myDBValue = myDBObj.RawQuery("Select * from " + TableNameSavedJobApplications + " where " + ColumnSavedEmpId + "=" + userid, null);
            while (myDBValue.MoveToNext())
            {
                var SavedJobIDfromDB = myDBValue.GetInt(myDBValue.GetColumnIndexOrThrow(ColumnSavedJobID));
                System.Console.WriteLine(" Value Of ID FROM DB --> " + SavedJobIDfromDB);
                var SavedJobNamefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnSavedJobName));
                System.Console.WriteLine(" Value  Of Name  FROM DB  --> " + SavedJobNamefromDB);
                var SavedJobDescriptionfromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnSavedJobDescription));
                System.Console.WriteLine(" Value  Of Age  FROM DB --> " + SavedJobDescriptionfromDB);
                var JobImagefromDB = myDBValue.GetInt(myDBValue.GetColumnIndexOrThrow(ColumnSavedJobImage));
                System.Console.WriteLine(" Value  Of Email  FROM DB --> " + JobImagefromDB);
                var SavedJobTypefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnSavedJobEmployerEmail));
                System.Console.WriteLine(" Value  Of Email  FROM DB --> " + SavedJobTypefromDB);
                jobList.Add(new Jobs(SavedJobIDfromDB, SavedJobNamefromDB, SavedJobDescriptionfromDB, JobImagefromDB, SavedJobTypefromDB));
            }
            return jobList;
        }

        //Insert value into the database
        public bool insertSavedJobApplication(int jobid, int empid, String title, String description,int img, String jobtype)
        {
            // Insert into persontable values (1232, 'john', 'john@gmail.com');           
            string insertStm = "INSERT INTO " + TableNameSavedJobApplications + " values(" +
                jobid + "," + empid + ",'" + title + "','" + description + "',"+img+",'" + jobtype + "')";
            System.Console.WriteLine("My SQL INSERT -->>>>" + insertStm);
            myDBObj.ExecSQL(insertStm);

            return true;
        }

        //Job Details by job Id
        public List<Jobs> selectJobDetailsByJobId(int jobid)
        {
            List<Jobs> jobList = new List<Jobs>();
            ICursor myDBValue = myDBObj.RawQuery("Select * from " + TableNameJobs+" where "+ColumnJobID+"="+jobid, null);
            while (myDBValue.MoveToNext())
            {
                var JobIDfromDB = myDBValue.GetInt(myDBValue.GetColumnIndexOrThrow(ColumnJobID));
                System.Console.WriteLine(" Value Of ID FROM DB --> " + JobIDfromDB);
                var JobNamefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnJobName));
                System.Console.WriteLine(" Value  Of Name  FROM DB  --> " + JobNamefromDB);
                var JobDescriptionfromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnJobDescription));
                System.Console.WriteLine(" Value  Of Age  FROM DB --> " + JobDescriptionfromDB);
                var JobTypefromDB = myDBValue.GetString(myDBValue.GetColumnIndexOrThrow(ColumnJobType));
                System.Console.WriteLine(" Value  Of Email  FROM DB --> " + JobTypefromDB);
                var JobImagefromDB = myDBValue.GetInt(myDBValue.GetColumnIndexOrThrow(ColumnJobImage));
                System.Console.WriteLine(" Value  Of Email  FROM DB --> " + JobImagefromDB);
                jobList.Add(new Jobs(JobIDfromDB, JobNamefromDB, JobDescriptionfromDB, JobImagefromDB, JobTypefromDB));

            }
            return jobList;
        }
        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            throw new NotImplementedException();
        }
    }

}