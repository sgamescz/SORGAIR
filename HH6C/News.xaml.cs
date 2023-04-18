using System.Net.Cache;
using System.Net;
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
    public partial class News : MetroWindow
    {

        string html_main;
        string html_body;
        string html_all;
        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;


        public News()
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

            Console.WriteLine("http://sorgair.com/api/news_show.php?version=" + tmp_verze + "&background=" + VM.typpozadi + "&type=newest_than_actual");
            test.Navigate("http://sorgair.com/api/news_show.php?version=" + tmp_verze + "&background=" + VM.typpozadi + "&type=newest_than_actual");


            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Console.WriteLine();
            string remoteUrl = "http://sorgair.com/api/version.php";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            HttpWebRequest.DefaultCachePolicy = policy;
            httpRequest.CachePolicy = policy;
            WebResponse response = httpRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();


            if (result != major + "." + minor + "." + build + "." + revision)
            {
                downloadbutton.IsEnabled = true;
                Console.WriteLine("true");
            }
            else
            {
                downloadbutton.IsEnabled = false;
                Console.WriteLine("false");
            }




        }


      

        private void refresh_news(object sender, RoutedEventArgs e)
        {
            test.Refresh(true);

            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
                wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
                wc.DownloadFileAsync(new Uri("http://sorgair.com/sorgair.zip"), "sorgair.zip");
            }

        }


        private void Wc_DownloadProgressChanged(object sender,
 System.Net. DownloadProgressChangedEventArgs e)
        {

            progress.Value = e.ProgressPercentage;

        }



        private void Wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

            if (e.Cancelled)
            {
                MessageBox.Show("Canceled", "Message",MessageBoxButton.OK);
                return;
            }

            if (e.Error != null)
            {
                MessageBox.Show("Somethings wrong, check your internet", "Chyba", MessageBoxButton.OK);
                
                return;
            }

            MessageBox.Show("Staženo. Nyní se vypnu, a provedu aktualizaci", "Hotovo", MessageBoxButton.OK);
            System.Diagnostics.Process.Start("_autoupdate\\autoupdate.exe");
            System.Environment.Exit(0);

        }



    }
}
