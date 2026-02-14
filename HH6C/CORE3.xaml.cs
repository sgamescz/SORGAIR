using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

using MahApps.Metro.Controls;
using System.Data.SQLite;
using WpfApp6.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp6.Model;
using MahApps.Metro.Controls.Dialogs;
using System.Globalization;
using System.IO;
using System.Net.Cache;
using System.Net;
using Microsoft.Win32;


namespace WpfApp6
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    /// 
   
    public partial class Core : MetroWindow
    {

        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;


        public Core()
        {
            //SORGAIR.Properties.Settings.Default.Languagecode = "en-US";
            //SORGAIR.Properties.Settings.Default.Save();
            this.DataContext = new MODEL_ViewModel();
            var langcode = SORGAIR.Properties.Settings.Default.Languagecode;

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langcode);


            Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator = ".";

            InitializeComponent();
            InitializeDatabaseAsync();
        }

        private async void InitializeDatabaseAsync()
        {
            await VM.SQL_OPENCONNECTION("SORG");
            VM.SQL_READSORGDATA("select hodnota from nastaveni where polozka='pozadi'", "pozadi");
            VM.SQL_READSORGDATA("select hodnota from nastaveni where polozka='popredi' ", "popredi");


            string major = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Major.ToString().PadLeft(2, '0');
            string minor = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Minor.ToString().PadLeft(2, '0');
            string build = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Build.ToString().PadLeft(2, '0');
            string revision = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Revision.ToString().PadLeft(2, '0');

            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Console.WriteLine(major + "." + minor + "." + build + "." + revision);


            Console.WriteLine(System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString().PadLeft(2, '0'));

            VM.BIND_VERZE_SORGU = major + "." + minor + "." + build + "." + revision;
            Console.WriteLine(System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());
            VM.FUNCTION_LOAD_CONTESTS_FILES();

            
            if (CheckForInternetConnection(1000, "http://sorgair.com/") is true)
            {
                VM.BINDING_IS_INTERNET = true;
                Thread get_version = new Thread(new ThreadStart(thread_getsorgversion));
                get_version.Start();
                Thread get_news_actual = new Thread(new ThreadStart(thread_getnewscount_actual));
                get_news_actual.Start();
                Thread get_news_next = new Thread(new ThreadStart(thread_getnewscount_next));
                get_news_next.Start();
                show_new_version_message();
            }
            else
            {
                VM.BINDING_IS_INTERNET = false;
            }


        }


        public static bool CheckForInternetConnection(int timeoutMs = 10000, string url = null)
        {
            Console.WriteLine("checking internet connection");
            try
            {




                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = timeoutMs;
                Console.WriteLine("OK");
                using (var response = (HttpWebResponse)request.GetResponse())
                    return true;
            }
            catch
            {
                Console.WriteLine("not ok");
                return false;
            }
        }





        public async void show_new_version_message()
        {

            string major = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Major.ToString().PadLeft(2, '0');
            string minor = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Minor.ToString().PadLeft(2, '0');
            string build = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Build.ToString().PadLeft(2, '0');
            string revision = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Revision.ToString().PadLeft(2, '0');

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

                var currentWindow = this;
                var results = await currentWindow.ShowMessageAsync("Je nová verze", "K dispozici je nová verze. Chceš si přečíst co je nového a stáhnout ji ?", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { AnimateShow = true, AnimateHide = true });
                if (results == null)
                    return;
                if (results == MessageDialogResult.Affirmative)
                {
                    Window printwindow = new SORGAIR.News();
                    printwindow.Show();

                }



            }

        }

        public async void thread_getsorgversion()
        {
            Thread.Sleep(2000);
            string remoteUrl = "http://sorgair.com/api/version.php";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            HttpWebRequest.DefaultCachePolicy = policy;

            httpRequest.CachePolicy = policy;
            WebResponse response = httpRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();
            Console.WriteLine(result);


            this.Invoke(() => VM.BIND_VERZE_SORGU_LAST = result);
            string major = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Major.ToString().PadLeft(2, '0');
            string minor = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Minor.ToString().PadLeft(2, '0');
            string build = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Build.ToString().PadLeft(2, '0');
            string revision = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Revision.ToString().PadLeft(2, '0');

            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Console.WriteLine();

          




        }

        public void thread_getnewscount_actual()
        {
            Thread.Sleep(2500);


            string major = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Major.ToString().PadLeft(2, '0');
            string minor = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Minor.ToString().PadLeft(2, '0');
            string build = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Build.ToString().PadLeft(2, '0');
            string revision = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Revision.ToString().PadLeft(2, '0');

            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Console.WriteLine(major + "." + minor + "." + build + "." + revision);


            string tmp_verze = major + minor + build + revision;


            string remoteUrl = "http://sorgair.com/api/news.php?version=" + tmp_verze + "&type=actual_and_older";
            Console.WriteLine(remoteUrl);
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            HttpWebRequest.DefaultCachePolicy = policy;

            httpRequest.CachePolicy = policy;
            WebResponse response = httpRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();
            Console.WriteLine(result);


            this.Invoke(() => VM.BIND_NEWS_COUNT_ACTUAL = result);

        }




        public void thread_getnewscount_next()
        {
            Thread.Sleep(2500);


            string major = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Major.ToString().PadLeft(2, '0');
            string minor = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Minor.ToString().PadLeft(2, '0');
            string build = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Build.ToString().PadLeft(2, '0');
            string revision = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Revision.ToString().PadLeft(2, '0');

            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Console.WriteLine(major + "." + minor + "." + build + "." + revision);


            string tmp_verze = major + minor + build + revision;
            string remoteUrl = "http://sorgair.com/api/news.php?version=" + tmp_verze + "&type=newest_than_actual";
            Console.WriteLine(remoteUrl);
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            HttpWebRequest.DefaultCachePolicy = policy;

            httpRequest.CachePolicy = policy;
            WebResponse response = httpRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();
            Console.WriteLine(result);


            this.Invoke(() => VM.BIND_NEWS_COUNT_NEXT = result);

        }


        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            //this.HamburgerMenuControl.Content = e.InvokedItem;
            //this.HamburgerMenuControl.DataContext = Pohledy.Test.DataContextProperty;
        }







        private async void core_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
                VM.SQL_CLOSECONNECTION("SORG");
                VM.SQL_CLOSECONNECTION("SOUTEZ");

        }

        private void CLICK_changeforeground(object sender, RoutedEventArgs e)
        {
            VM.Function_global_changeforeground = VM.Function_global_changeforeground + 1;
        }

        private void CLICK_changebackground(object sender, RoutedEventArgs e)
        {
            VM.Function_global_changebackground = VM.Function_global_changebackground + 1;
        }




        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //            this.Show();
            //          System.Threading.Thread.Sleep(500);
            HamburgerMenuControl.SelectedIndex = VM.BINDING_selectedmenuindex;
            this.SizeChanged += Form1_ResizeEnd;

            int RegVal;
            RegVal = 11001;
            using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", RegistryKeyPermissionCheck.ReadWriteSubTree))
                if (Key.GetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe") == null)
                    Key.SetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe", RegVal, RegistryValueKind.DWord);

            Console.WriteLine("aaa");

            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            if (screenHeight < 930)
            {
                VM.Function_global_resizemode = "Fill";
                this.Height = screenHeight;
                this.Width = 1270 * (screenHeight / 930);

            }
            else
            {
                VM.Function_global_resizemode = "None";
                main_master_grid.Width = this.ActualWidth;
                main_master_grid.Height = this.ActualHeight;

            }


        }

        public void changeselectedmenu(int id)
        {
            HamburgerMenuControl.SelectedIndex = id;

        }






        private void Form1_ResizeEnd(Object sender, EventArgs e)
        {



            
           if (VM.Function_global_resizemode != "Fill")
            {

                VM.Function_global_resizemode = "None";
                main_master_grid.Width = this.ActualWidth;
                main_master_grid.Height = this.ActualHeight;

            }
            Console.WriteLine("aaa");
        }


        private void CLICK_originalresize(object sender, RoutedEventArgs e)
        {

            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            if (screenHeight < 930)
            {
                VM.Function_global_resizemode = "Fill";
                this.Height = screenHeight;
                this.Width = 1270 * (screenHeight / 930);

            }
            else
            {
                this.Width = 1270;
                this.Height = 930;
            }


            this.WindowState = WindowState.Normal;
         
        }

        private void CLICK_resizemode(object sender, RoutedEventArgs e)
        {

            if (VM.Function_global_resizemode == "None")
            {
                VM.Function_global_resizemode = "Fill";
            }
            else
            {
                VM.Function_global_resizemode = "None";
                main_master_grid.Width = this.ActualWidth ;
                main_master_grid.Height = this.ActualHeight;

            }
        }

     
    }
}
