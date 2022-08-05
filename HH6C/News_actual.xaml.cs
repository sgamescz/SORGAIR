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
    public partial class News_actual : MetroWindow
    {

        string html_main;
        string html_body;
        string html_all;
        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;


        public News_actual()
        {
            this.DataContext = new MODEL_ViewModel();

            InitializeComponent();

        }



        private void load_news(object sender, RoutedEventArgs e)
        {


            VM.SQL_OPENCONNECTION("SORG");
            VM.typpozadi = VM.SQL_READSORGDATA("select hodnota from nastaveni where polozka='pozadi'", "");
            Console.WriteLine(VM.typpozadi);
            Console.WriteLine("xx");
            VM.SQL_CLOSECONNECTION("SORG");



            string major = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Major.ToString().PadLeft(2, '0');
            string minor = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Minor.ToString().PadLeft(2, '0');
            string build = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Build.ToString().PadLeft(2, '0');
            string revision = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Revision.ToString().PadLeft(2, '0');

            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Console.WriteLine(major + "." + minor + "." + build + "." + revision);

            string tmp_verze = major +  minor + build + revision;

            Console.WriteLine("http://sorgair.com/api/news_show.php?version=" + tmp_verze + "&background=" + VM.typpozadi + "&type=actual_and_older");
            test.Navigate("http://sorgair.com/api/news_show.php?version=" + tmp_verze + "&background=" + VM.typpozadi + "&type=actual_and_older");



        


        }

        private void refresh_news(object sender, RoutedEventArgs e)
        {
            test.Refresh(true);
        }
    }
}
