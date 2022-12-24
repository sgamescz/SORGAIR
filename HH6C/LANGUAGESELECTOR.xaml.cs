
using System.Windows;
using System;
using System.Threading;

using MahApps.Metro.Controls;

using WpfApp6.Model;
using System.IO;
using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using MahApps.Metro.Controls.Dialogs;
using System.Threading;
using System.Threading.Tasks;

namespace SORGAIR
{
    /// <summary>
    /// Interakční logika pro languageselector.xaml
    /// </summary>
    public partial class languageselector : MetroWindow
    {

        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;


        public languageselector()
        {
            this.DataContext = new MODEL_ViewModel();
            var langcode = SORGAIR.Properties.Settings.Default.Languagecode;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langcode);

            InitializeComponent();
            VM.SQL_OPENCONNECTION("SORG");
            VM.SQL_READSORGDATA("select hodnota from nastaveni where polozka='pozadi'", "pozadi");
            VM.typpozadi = VM.SQL_READSORGDATA("select hodnota from nastaveni where polozka='pozadi'", "");
            Console.WriteLine(VM.typpozadi);
            Console.WriteLine("xx");

            VM.SQL_READSORGDATA("select hodnota from nastaveni where polozka='popredi' ", "popredi");
            VM.SQL_CLOSECONNECTION("SORG");


            if (System.IO.Directory.Exists("autoupdate"))
            {
                if (System.IO.Directory.Exists("_autoupdate"))
                {
                    Directory.Delete("_autoupdate", true);
                }
                Directory.Move("autoupdate", "_autoupdate");
            }


            if (System.IO.File.Exists(@"sorgair.zip"))
            {

                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "_autoupdate\\autoupdate.exe");

                System.Diagnostics.Process.Start("_autoupdate\\autoupdate.exe");
                Application.Current.Shutdown();
            }


        }


       


        private async void nacti_core(string jazyk)
        {
            var currentWindow = this;
            var controller = await currentWindow.ShowProgressAsync("Starting", "SORG AIR is loading, please wait...");
            controller.SetProgress(0.2);
            await Task.Delay(1000);
            SORGAIR.Properties.Settings.Default.Languagecode = jazyk;
            SORGAIR.Properties.Settings.Default.Save();
            controller.SetProgress(0.4);
            await Task.Delay(1000);
            MetroWindow f2 = new WpfApp6.Core();
            controller.SetProgress(0.6);
            controller.SetMessage("Done. Let's go flying...");
            await Task.Delay(500);
            controller.CloseAsync();
            await Task.Delay(500);
            all.Visibility = Visibility.Collapsed;
            f2.Show(); // Shows Form2
            this.Close();
            await Task.Delay(100);
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MetroWindow f2 = new SORGAIR.exres();
            f2.Show(); // Shows Form2
        }

        private void lang_cze(object sender, RoutedEventArgs e)
        {
            nacti_core("cs-CZ");

        }

        private void lang_svk(object sender, RoutedEventArgs e)
        {
            nacti_core("sk-SK");

        }

        private void lang_eng(object sender, RoutedEventArgs e)
        {
            nacti_core("en-US");
        }

        private void lang_ger(object sender, RoutedEventArgs e)
        {
            nacti_core("de-DE");

        }

        private void lang_hun(object sender, RoutedEventArgs e)
        {
            nacti_core("hu-HU");
        }
    }
}
