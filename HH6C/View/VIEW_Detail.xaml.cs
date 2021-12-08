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
using System.IO;


namespace WpfApp6.View
{
    /// <summary>
    /// Interakční logika pro FirstView.xaml
    /// </summary>
    ///  string html_all;
    ///  

    public partial class Detail : UserControl
    {
        string html_all;
        string zvolenypohled;
        private static readonly Random getrandom = new Random();

        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;
        public Detail()
        {

            InitializeComponent();
        }



        private async void statistics_landing_Click(object sender, RoutedEventArgs e)
        {
            zvolenypohled = "landing";

            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Vytvářím velmi zajmavou statistiku");
            controller.SetProgress(0);
            await Task.Delay(300);
            controller.SetProgress(0.5);


            datagrid_statistiky.Columns[4].Header = "Záznamů";
            datagrid_statistiky.Columns[5].Header = "ø Průměr";
            datagrid_statistiky.Columns[6].Header = "---";
            datagrid_statistiky.Columns[7].Header = "Σ Suma";
            datagrid_statistiky.Columns[8].Header = "---";
            datagrid_statistiky.Columns[9].Header = "Hodnoty";
            datagrid_statistiky.Columns[10].Header = "---";
            datagrid_statistiky.Columns[11].Header = "Hodnoty";
            controller.SetProgress(0.7);


            datagrid_statistiky.Columns[4].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[5].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[6].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[7].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[8].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[9].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[10].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[11].Visibility = Visibility.Visible;
            VM.FUNCTION_RESULTS_LOADBASERESULTS("statistics_averagelandings");
            await Task.Delay(300);
            controller.SetProgress(0.9);
            await controller.CloseAsync();

        }

        private async void statistics_averageheight_Click(object sender, RoutedEventArgs e)
        {
            zvolenypohled = "averageheights";

            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Vytvářím velmi zajmavou statistiku");
            controller.SetProgress(0);
            await Task.Delay(300);
            controller.SetProgress(0.5);




            datagrid_statistiky.Columns[4].Header = "Záznamů";
            datagrid_statistiky.Columns[5].Header = "ø Průměr";
            datagrid_statistiky.Columns[6].Header = "---";
            datagrid_statistiky.Columns[7].Header = "Σ Suma";
            datagrid_statistiky.Columns[8].Header = "---";
            datagrid_statistiky.Columns[9].Header = "Hodnoty";
            datagrid_statistiky.Columns[10].Header = "---";
            datagrid_statistiky.Columns[11].Header = "Hodnoty";

            datagrid_statistiky.Columns[4].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[5].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[6].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[7].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[8].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[9].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[10].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[11].Visibility = Visibility.Visible;

            VM.FUNCTION_RESULTS_LOADBASERESULTS("statistics_averageheights");



            await Task.Delay(300);
            controller.SetProgress(0.9);
            await controller.CloseAsync();



        }

        private void statistics_maxheight_Click(object sender, RoutedEventArgs e)
        {



            datagrid_statistiky.Columns[4].Header = "Záznamů";
            datagrid_statistiky.Columns[5].Header = "↑ Max výška";
            datagrid_statistiky.Columns[6].Header = "---";
            datagrid_statistiky.Columns[7].Header = "Σ Celková výška";
            datagrid_statistiky.Columns[8].Header = "---";
            datagrid_statistiky.Columns[9].Header = "ø Průměr";
            datagrid_statistiky.Columns[10].Header = "---";
            datagrid_statistiky.Columns[11].Header = "Hodnoty";

            datagrid_statistiky.Columns[4].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[5].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[6].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[7].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[8].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[9].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[10].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[11].Visibility = Visibility.Visible;

            VM.FUNCTION_RESULTS_LOADBASERESULTS("statistics_maxheights");

        }

        private void statistics_minheight_Click(object sender, RoutedEventArgs e)
        {

            datagrid_statistiky.Columns[4].Header = "Záznamů";
            datagrid_statistiky.Columns[5].Header = "Minimální výška";
            datagrid_statistiky.Columns[6].Header = "Σ Celková doba";
            datagrid_statistiky.Columns[7].Header = "--y-";
            datagrid_statistiky.Columns[8].Header = "Σ výšky / Σ bodů";
            datagrid_statistiky.Columns[9].Header = "Bodů za metr";
            datagrid_statistiky.Columns[10].Header = "---";
            datagrid_statistiky.Columns[11].Header = "Hodnoty";

            datagrid_statistiky.Columns[4].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[5].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[6].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[7].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[8].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[9].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[10].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[11].Visibility = Visibility.Visible;


            VM.FUNCTION_RESULTS_LOADBASERESULTS("statistics_minheights");
        }

        private void statistics_timevsheight_Click(object sender, RoutedEventArgs e)
        {

            datagrid_statistiky.Columns[4].Header = "Záznamů";
            datagrid_statistiky.Columns[5].Header = "Minimální výška";
            datagrid_statistiky.Columns[6].Header = "ø čas v kole";
            datagrid_statistiky.Columns[7].Header = "ø výška v kole";
            datagrid_statistiky.Columns[8].Header = "Σ výšky / Σ bodů";
            datagrid_statistiky.Columns[9].Header = "na 10 minut\nje třeba metrů";
            datagrid_statistiky.Columns[10].Header = "---";
            datagrid_statistiky.Columns[11].Header = "Ze 100 metrů";

            datagrid_statistiky.Columns[4].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[5].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[6].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[7].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[8].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[9].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[10].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[11].Visibility = Visibility.Visible;

            VM.FUNCTION_RESULTS_LOADBASERESULTS("statistics_timevsheight");
        }

        private void statistics_enemykiled_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_RESULTS_LOADBASERESULTS("statistics_enemykiled");
        }

        private async void statistics_flighttime_Click(object sender, RoutedEventArgs e)
        {
            zvolenypohled = "flighttime";
            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Vytvářím velmi zajmavou statistiku");
            controller.SetProgress(0);
            await Task.Delay(300);
            controller.SetProgress(0.5);



            datagrid_statistiky.Columns[4].Header = "Záznamů";
            datagrid_statistiky.Columns[5].Header = "---";
            datagrid_statistiky.Columns[6].Header = "Σ Celková doba";
            datagrid_statistiky.Columns[7].Header = "---";
            datagrid_statistiky.Columns[8].Header = "ø Průměr kola";
            datagrid_statistiky.Columns[9].Header = "---";
            datagrid_statistiky.Columns[10].Header = "---";
            datagrid_statistiky.Columns[11].Header = "Hodnoty";

            datagrid_statistiky.Columns[4].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[5].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[6].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[7].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[8].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[9].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[10].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[11].Visibility = Visibility.Visible;

            VM.FUNCTION_RESULTS_LOADBASERESULTS("statistics_flighttime");



            await Task.Delay(300);
            controller.SetProgress(0.9);
            await controller.CloseAsync();


        }

        private async void print_statistics_btn_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Vytvářím velmi zajmavou statistiku");
            controller.SetProgress(0);
            await Task.Delay(300);
            controller.SetProgress(0.5);


            string[] headers = {
            datagrid_statistiky.Columns[0].Header.ToString(),
            datagrid_statistiky.Columns[1].Header.ToString(),
            datagrid_statistiky.Columns[2].Header.ToString(),
            datagrid_statistiky.Columns[3].Header.ToString(),
            datagrid_statistiky.Columns[4].Header.ToString(),

            datagrid_statistiky.Columns[5].Header.ToString(),
            datagrid_statistiky.Columns[6].Header.ToString(),
            datagrid_statistiky.Columns[7].Header.ToString(),
            datagrid_statistiky.Columns[8].Header.ToString(),
            datagrid_statistiky.Columns[9].Header.ToString(),
            datagrid_statistiky.Columns[10].Header.ToString(),
            datagrid_statistiky.Columns[11].Header.ToString()};


            string[] visibility = {
                datagrid_statistiky.Columns[5].Visibility.ToString(),
                datagrid_statistiky.Columns[6].Visibility.ToString(),
                datagrid_statistiky.Columns[7].Visibility.ToString(),
                datagrid_statistiky.Columns[8].Visibility.ToString(),
                datagrid_statistiky.Columns[9].Visibility.ToString(),
                datagrid_statistiky.Columns[10].Visibility.ToString(),
                datagrid_statistiky.Columns[11].Visibility.ToString()
            };


            //VM.print_statistics("statistics_" + zvolenypohled, "statistics_" + zvolenypohled, "html", headers, visibility);
            VM.print_statistics("frame_with_contest_info", "data_empty", "print_complete_resuls", "statistics_" + zvolenypohled,"NECO", "html", headers, visibility);

            await Task.Delay(300);
            controller.SetProgress(0.9);
            await controller.CloseAsync();

        }

        private async void print_statistics_btnall_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Vytvářím velmi zajmavou statistiku");
            controller.SetProgress(0);
            await Task.Delay(300);
            controller.SetProgress(0.5);


            ////////////////////////////
            ///
            VM.print_userslist("frame_empty", "data_userlist", "print_userlist", "Seznam soutěžících", "memory");


            //////////////////////////////////////////////
            ///

            string[] headers = {
            "Pořadí",
            "Soutěžící",
            "Stát",
            "ID",
            "Záznamů",
            "ø Průměr",
            "---",
            "Σ Suma",
            "---",
            "---",
            "---",
            "Hodnoty"
            };

            string[] visibility = {
                "Visible",
               "Hidden",
                "Visible",
                "Hidden",
                "Hidden",
                "Hidden",
                "Visible"
            };

            VM.FUNCTION_RESULTS_LOADBASERESULTS("statistics_averagelandings");
            //VM.print_statistics("statistics_c_landing", "statistics_landing", "memory", headers, visibility);
            VM.print_statistics("frame_empty", "data_empty", "print_complete_resuls", "statistics_landing", "Přistání", "memory", headers, visibility);
            
            ///////////////////////////////////////////////////////////
            

            datagrid_statistiky.Columns[4].Header = "Záznamů";
            datagrid_statistiky.Columns[5].Header = "---";
            datagrid_statistiky.Columns[6].Header = "Σ Celková doba";
            datagrid_statistiky.Columns[7].Header = "---";
            datagrid_statistiky.Columns[8].Header = "ø Průměr kola";
            datagrid_statistiky.Columns[9].Header = "---";
            datagrid_statistiky.Columns[10].Header = "---";
            datagrid_statistiky.Columns[11].Header = "Hodnoty";

            datagrid_statistiky.Columns[4].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[5].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[6].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[7].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[8].Visibility = Visibility.Visible;
            datagrid_statistiky.Columns[9].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[10].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[11].Visibility = Visibility.Visible;

            headers = new string[] {
            "Pořadí",
            "Soutěžící",
            "Stát",
            "ID",
            "Záznamů",
            "---",
            "Σ Celková doba",
            "---",
            "ø Průměr kola",
            "---",
            "---",
            "Hodnoty"
            };

            visibility = new string[]{
               "Hidden",
                "Visible",
                "Hidden",
                "Visible",
                "Hidden",
                "Hidden",
                "Visible"
            };


            VM.FUNCTION_RESULTS_LOADBASERESULTS("statistics_flighttime");
            //VM.print_statistics("statistics_c_flighttime", "statistics_flighttime", "memory", headers, visibility);
            VM.print_statistics("frame_empty", "data_empty", "print_complete_resuls", "statistics_flighttime", "Letový čas", "memory", headers, visibility);




            ///////////////////////////////////////////////////////////



            headers = new string[] {
            "Pořadí",
            "Soutěžící",
            "Stát",
            "ID",
            "Záznamů",
            "ø Průměr",
            "---",
            "Σ Suma",
            "---",
            "---",
            "---",
            "Hodnoty"
            };

            visibility = new string[]{
               "Visible",
                "Hidden",
                "Visible",
                "Hidden",
                "Hidden",
                "Hidden",
                "Visible"
            };



            VM.FUNCTION_RESULTS_LOADBASERESULTS("statistics_averageheights");
            //VM.print_statistics("statistics_c_averageheights", "statistics_averageheights", "memory", headers, visibility);

            VM.print_statistics("frame_empty", "data_empty", "print_complete_resuls", "statistics_averageheights", "Průměrná výška", "memory", headers, visibility);


            ///////////////////////////////////////////////////////////




            visibility = new string[] {
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True"
            };


            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls","Celkové výsledky", "memory", visibility);



            ///////////////////////////////////////////////////////////




            VM.FUNCTION_RESULTS_LOADBASERESULTS("users");

            visibility = new string[] {
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True"
            };


            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", "Základní výsledky", "memory", visibility);





            VM.print_matrix("frame_empty", "data_matrix", "print_basic_resuls", "Rozlosování", "memory");

            // VM.FUNCTION_RESULTS_LOADBASERESULTS("statistics_maxheights");
            // VM.print_statistics("statistics_flighttime", "statistics_flighttime", "memory", headers, visibility);

            //VM.FUNCTION_RESULTS_LOADBASERESULTS("statistics_minheights");
            //VM.print_statistics("statistics_flighttime", "statistics_flighttime", "memory", headers, visibility);

            //VM.FUNCTION_RESULTS_LOADBASERESULTS("statistics_timevsheight");
            //VM.print_statistics("statistics_flighttime", "statistics_flighttime", "memory", headers, visibility);

            VM.print_memory_to_file("frame_with_contest_info", "data_empty", "print_complete_overview", "CMPLSRES", "html");


            await Task.Delay(300);
            controller.SetProgress(0.9);
            await controller.CloseAsync();
        }
    }
}
