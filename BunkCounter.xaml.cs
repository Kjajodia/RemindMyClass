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
using System.IO.IsolatedStorage;
using System.IO;

namespace Remindmyclass
{
    public partial class BunkCounter : PhoneApplicationPage
    {
        public BunkCounter()
        {
            InitializeComponent();
        }
        float total = 0, attendance = 0;

        IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
       
        
        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            attendance++;
            total++;
            MessageBox.Show("Attendance Updated in records");

        }

        private void Bunking_Checked(object sender, RoutedEventArgs e)
        {
            total++;
            MessageBox.Show("Attendance Updated in records");
            
        }
       

        private void Holiday_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Enjoy Your Holiday");
        }
        //void showattendance()
        //{
            
        //  textBox1.Text = "your attendance is \n " + percentage.ToString() + "%";
        //}

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            double a, b, c;
            FileStream stream = store.OpenFile("test.txt", FileMode.Open, FileAccess.Read);
           BinaryReader reader = new BinaryReader(stream);
           a = reader.ReadDouble();      
           b = reader.ReadDouble();
           c = reader.ReadDouble();
           a += total; total = 0;
           b += attendance; attendance = 0;
           c= ((b/a)*100);
           MessageBox.Show("total Classes="+a.ToString()+" Classes Attended="+b.ToString()+" Attendance percentage="+c.ToString()+"%");
           reader.Close();
            stream = store.OpenFile("test.txt", FileMode.Create, FileAccess.Write);
           BinaryWriter writer = new BinaryWriter(stream);
           writer.Write(a);
           writer.Write(b);
           writer.Write(c);
           writer.Close();
           

        }

      
       
    }
}