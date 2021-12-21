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
            VM.SQL_OPENCONNECTION("SORG");
            VM.SQL_READSORGDATA("select hodnota from nastaveni where polozka='pozadi'", "pozadi");
            VM.SQL_READSORGDATA("select hodnota from nastaveni where polozka='popredi' ", "popredi");
            VM.BIND_VERZE_SORGU = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
            Console.WriteLine(System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());
            VM.FUNCTION_LOAD_CONTESTS_FILES();
            Thread get_version = new Thread(new ThreadStart(thread_getsorgversion));
            get_version.Start();
            Thread get_news = new Thread(new ThreadStart(thread_getnewscount));
            get_news.Start();

        }

        public void thread_getsorgversion()
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

        }

        public void thread_getnewscount()
        {
            Thread.Sleep(2500);
            string tmp_verze = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
            tmp_verze = tmp_verze.Replace(".", "");
            string remoteUrl = "http://sorgair.com/api/news.php?version=" + tmp_verze;
            Console.WriteLine(remoteUrl);
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            HttpWebRequest.DefaultCachePolicy = policy;

            httpRequest.CachePolicy = policy;
            WebResponse response = httpRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();
            Console.WriteLine(result);


            this.Invoke(() => VM.BIND_NEWS_COUNT = result);

        }



        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            //this.HamburgerMenuControl.Content = e.InvokedItem;
            //this.HamburgerMenuControl.DataContext = Pohledy.Test.DataContextProperty;
        }







        private async void core_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to close?", "SORG AIR", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }

            else
            {
                VM.SQL_CLOSECONNECTION("SORG");
                VM.SQL_CLOSECONNECTION("SOUTEZ");
            }



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


        }

        public void changeselectedmenu(int id)
        {
            HamburgerMenuControl.SelectedIndex = id;

        }






        private void Form1_ResizeEnd(Object sender, EventArgs e)
        {



            
           if (VM.Function_global_resizemode != "Fill")
            {


                main_master_grid.Width = this.ActualWidth;
                main_master_grid.Height = this.ActualHeight;

            }

        }


        private void CLICK_originalresize(object sender, RoutedEventArgs e)
        {
            this.Width = 1230;
            this.Height =  900;
            this.WindowState = WindowState.Normal;
         
        }

        private void CLICK_resizemode(object sender, RoutedEventArgs e)
        {

            if (VM.Function_global_resizemode == "Uniform")
            {
                VM.Function_global_resizemode = "Fill";
            }
            else
            {
                VM.Function_global_resizemode = "Uniform";
                main_master_grid.Width = this.Width;
                main_master_grid.Height = this.Height;

            }
        }
    }
}
