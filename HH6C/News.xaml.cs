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



            string tmp_verze = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
            tmp_verze = tmp_verze.Replace(".", "");
            Console.WriteLine("http://sorgair.com/api/news_show.php?version=" + tmp_verze + "&background=" + VM.typpozadi);
            test.Navigate("http://sorgair.com/api/news_show.php?version=" + tmp_verze + "&background=" + VM.typpozadi);
           

            
        }

        private void refresh_news(object sender, RoutedEventArgs e)
        {
            test.Refresh(true);
        }
    }
}
