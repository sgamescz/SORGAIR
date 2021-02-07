
using System.Windows;
using System;
using System.Threading;

using MahApps.Metro.Controls;

using WpfApp6.Model;
using System.IO;
using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;




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
            VM.SQL_READSORGDATA("select hodnota from nastaveni where polozka='popredi' ", "popredi");

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            SORGAIR.Properties.Settings.Default.Languagecode = "cs-CZ";
            SORGAIR.Properties.Settings.Default.Save();
            MetroWindow f2 = new WpfApp6.Core();
            f2.Show(); // Shows Form2

            this.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            SORGAIR.Properties.Settings.Default.Languagecode = "en-US";
            SORGAIR.Properties.Settings.Default.Save();

            MetroWindow f2 = new WpfApp6.Core ();
            f2.Show (); // Shows Form2

            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SORGAIR.Properties.Settings.Default.Languagecode = "sk-SK";
            SORGAIR.Properties.Settings.Default.Save();

            MetroWindow f2 = new WpfApp6.Core();
            f2.Show();
            this.Close();
        }

       
    }
}
