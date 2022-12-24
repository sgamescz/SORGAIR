
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
    /// Interakční logika pro exres.xaml
    /// </summary>
    public partial class exres : MetroWindow
    {

        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;


        public exres()
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
        }
    }
}
