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
