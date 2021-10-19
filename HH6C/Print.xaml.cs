using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using WpfApp6.Model;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Threading;
using System.Globalization;
using System.IO;
using Microsoft.Win32;


namespace SORGAIR
{
    /// <summary>
    /// Interakční logika pro Print.xaml
    /// </summary>
    public partial class Print : MetroWindow
    {

        string html_main;
        string html_body;
        string html_all;
        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;


        public Print()
        {
            InitializeComponent();

        }



        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);


            html_main = File.ReadAllText(directory + "/Print_templates/main_template.html", Encoding.UTF8);

            html_body = File.ReadAllText(directory + "/Print_templates/scorecard_long.html", Encoding.UTF8);
            string html_body_complete = "";
            for (int i = 1; i < VM.Players.Count() + 1; i++)
            {
                html_body_complete = html_body_complete + html_body;
            }


            html_all = html_main.Replace("@BODY", html_body_complete);

            test.NavigateToString(html_all);
            
        }

     

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            html_body = html_body.Replace("@USERNAME", "Aleš Krátký");
            html_body = html_body.Replace("@CONTESTNAME", "Dinosoutěž 1");
            html_body = html_body.Replace("@COUNTRY", "CZE");
            html_body = html_body.Replace("@NATLIC", "CZE-211-999");
            html_body = html_body.Replace("@NACLIC", "123456789");
            html_body = html_body.Replace("@FAILIC", "CZE-1492");
            html_body = html_body.Replace("@AGECAT", "Senior");
            html_body = html_body.Replace("@CLUB", "MK RCMania");
            html_body = html_body.Replace("@TEAM", "Sorg air team");
            html_body = html_body.Replace("@FREQUENCY", "2,4GHz");

            html_all = html_main.Replace("@BODY", html_body);
            test.NavigateToString(html_all);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);


            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html_body);
            doc.Save(directory + "/Print/scorecard_long.pdf");
            doc.Close();

            System.Diagnostics.Process.Start(directory + "/Print/scorecard_long.pdf");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
