using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
using System.IO;
using System.IO.IsolatedStorage;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Remindmyclass
{
    public partial class MainPage : PhoneApplicationPage
    {    private const string strConnectionString = @"isostore:/AttendanceDB.sdf";
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }


        [Table]
        public class attendance
        {
            [Column(IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false, AutoSync = AutoSync.OnInsert)]
            public int uid
            {
                get;
                set;
            }
                 [Column(CanBeNull = false)]
            public double total
            {
                get;
                set;
            }
            [Column(CanBeNull = false)]
            public double attend
            {
                get;
                set;
            }

            [Column(CanBeNull = false)]
            public double percent
            {
                get;
                set;
            }
            [Column(CanBeNull = false)]
            public DateTime time
            {
                get;
                set;
            }
            [Column(CanBeNull = false)]
            public int recur
            {
                get;
                set;
            }

        }
            public class AttendanceDataContext:DataContext
            {
                public AttendanceDataContext(string connectionstring)
                    : base(connectionstring)
                    {
                    }
                    public Table<attendance> Attendancee
                    {
                     get
                    {
                     return this.GetTable<attendance>();
                     }
                }
            }

          // AttendanceDataContext db = new AttendanceDataContext("Data Source=’isostore:/ToDo.sdf’");
            private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
            {
                using (AttendanceDataContext atd = new AttendanceDataContext(strConnectionString))
                {
                    if (atd.DatabaseExists() == false)
                        atd.CreateDatabase();
                }
            }
       
        private void ApplicationBarIconButton_Click_3(object sender, EventArgs e)
        {
            using (AttendanceDataContext atd = new AttendanceDataContext(strConnectionString))
            {
                string str = "";
                IList<attendance> list = null;
                IQueryable<attendance> query = from c in atd.Attendancee select c;
                list = query.ToList();
                foreach (attendance att in list)
                {
                    str = str + att.uid.ToString() + " " + " " + att.percent.ToString() + " " + att.total.ToString() + " " + att.attend.ToString() + " " + att.time.ToString() + " " + att.recur.ToString();
                }
                MessageBox.Show(str);

            }
        }
        int recur1;

   //     IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Days.xaml", UriKind.RelativeOrAbsolute));
        }

      
        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        { 
            String name = System.Guid.NewGuid().ToString();
            String name2 = System.Guid.NewGuid().ToString();
            DateTime date = (DateTime)Date1.Value;
            DateTime time = (DateTime)Time1.Value;
            DateTime beginTime = date + time.TimeOfDay;
            
            if (beginTime < DateTime.Now)
            {
                MessageBox.Show("PUT FUTURE DATE");
                return;
            }
            RecurrenceInterval recurrence = RecurrenceInterval.None;
            recur1 = 0;
            if (radioButton1.IsChecked == true)
            {
                recurrence = RecurrenceInterval.Daily; recur1 = 1;
            }
            else if (radioButton2.IsChecked == true)
            {
                recurrence = RecurrenceInterval.Weekly; recur1 = 2;
            }
            else if (radioButton3.IsChecked == true)
            {
                recurrence = RecurrenceInterval.Monthly; recur1 = 3;
            }
            using (AttendanceDataContext atd = new AttendanceDataContext(strConnectionString))
            {
                atd.CreateDatabase();
                attendance a = new attendance();
                a.total = 0;
                a.attend = 0;
                a.percent = 0;
                a.time = beginTime;
                a.recur = recur1;
                atd.Attendancee.InsertOnSubmit(a);
                atd.SubmitChanges();
            }
           
            Reminder reminder1 = new Reminder(name);
            Reminder reminder2 = new Reminder("new reminder12");
            // reminder2[230] = new Reminder(name);
            reminder1.Title = "Click here to update and check attendance ";
            reminder1.Content = textBox4.Text;
            reminder1.BeginTime = beginTime;
            Uri navigationuri = new Uri("/BunkCounter.xaml", UriKind.Relative);
            reminder1.NavigationUri = navigationuri;
            double ab = 0, b = 0, c = 0;
            //reminder.ExpirationTime = expirationTime;
            reminder1.RecurrenceType = recurrence;
            reminder2.Title = "Click here to update and check attendance ";
            reminder2.Content = textBox4.Text;
            ScheduledActionService.Add(reminder1);
            reminder2.NavigationUri = navigationuri;
            for (int i = 0; i < 5; i++)
            {
              DateTime beginTime2 = date + time.TimeOfDay;
            beginTime2.AddMinutes(1);
             reminder2.BeginTime =beginTime2;
              ScheduledActionService.Add(reminder2);
              ScheduledActionService.LaunchForTest(name, System.TimeSpan.FromMinutes(1));

            }
            //FileStream stream = store.OpenFile("test.txt", FileMode.Create, FileAccess.ReadWrite);
            //BinaryWriter writer = new BinaryWriter(stream);
            //writer.Write(ab);
            //writer.Write(b);
            //writer.Write(c);
            //MessageBox.Show("Reminder saved");
            //writer.Close();
            
            
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox6.Text = "";
            radioButton1.IsChecked = false;
            radioButton2.IsChecked = false;
            radioButton3.IsChecked = false;
            

        }

       
        
    }
}