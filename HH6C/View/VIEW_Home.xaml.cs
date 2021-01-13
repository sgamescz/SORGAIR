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
using WpfApp6.Model;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Threading;


namespace WpfApp6.View
{
    /// <summary>
    /// Interakční logika pro FirstView.xaml
    /// </summary>
    public partial class Uvod : UserControl
    {

        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;
//        ViewModel XX = new ViewModel(DialogCoordinator.Instance);

        public Uvod()
        {

            InitializeComponent();
            //DataContext = VM ;
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


            Console.WriteLine(VM.MODEL_CONTESTS_FILES[competitionlist.SelectedIndex].FILENAME);
            VM.SQL_OPENCONNECTION(VM.MODEL_CONTESTS_FILES[competitionlist.SelectedIndex].FILENAME);

            VM.FUNCTION_LOADCONTEST();
            VM.FUNCTION_USERS_LOAD_ALLCOMPETITORS();
            VM.FUNCTION_TEAM_LOAD_TEAMS();
            VM.FUNCTION_ROUNDS_LOAD_ROUNDS();
            VM.FUNCTION_LOAD_DEFAULT_ROUNDSANDGROUPS();
            //VM.BIND_VYBRANEKOLOMENU = "Vybrané kolo: 1/4";
            VM.clock_create ();
            VM.BIND_MENU_ENABLED_nastavenisouteze = true;
            VM.BIND_MENU_ENABLED_audioadalsi = true;
            VM.BIND_MENU_ENABLED_hardware = true;
            VM.BIND_MENU_ENABLED_soutezici = true;
            VM.BIND_MENU_ENABLED_rozlosovani = true;
            VM.BIND_MENU_ENABLED_vybranekolo = true;
            VM.BIND_MENU_ENABLED_seznamkol = true;
            VM.BIND_MENU_ENABLED_vysledky = true;
            VM.BIND_MENU_ENABLED_online = false;
            VM.BIND_MENU_ENABLED_detailyastatistiky = false;
            VM.BIND_MENU_ENABLED_finale  = false ;


        }



        public void thread2()
        {
            var result = string.Empty;
            using (var webClient = new System.Net.WebClient())
            {
                webClient.Encoding = System.Text.Encoding.UTF8;
                result = webClient.DownloadString("http://sorgair.com/api/version.php");
            }

            this.Invoke(() => VM.BIND_VERZE_SORGU_LAST = result);

        }
        public void download_news(object sender, RoutedEventArgs e)
        {

            Thread test = new Thread(new ThreadStart(thread2));
            test.Start();
            //VM.FUNCTION_LOAD_MATRIX_FILES();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_LOAD_CONTESTS_FILES();

        }
    }
}
