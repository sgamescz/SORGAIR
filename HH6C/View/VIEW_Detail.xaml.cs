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


            datagrid_statistiky.Width = 0;
            maingrid.UpdateLayout();
            await Task.Delay(500);
            datagrid_statistiky.Width = maingrid.ActualWidth;
            await Task.Delay(300);


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
            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_averagelandings", VM.BIND_ROUNDS_IN_STATISTICS,99);
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
            datagrid_statistiky.Width = 0;
            maingrid.UpdateLayout();
            await Task.Delay(500);
            datagrid_statistiky.Width = maingrid.ActualWidth;
            await Task.Delay(300);




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

            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_averageheights", VM.BIND_ROUNDS_IN_STATISTICS, 99);



            await Task.Delay(300);
            controller.SetProgress(0.9);
            await controller.CloseAsync();



        }

        private async void statistics_maxheight_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Vytvářím velmi zajmavou statistiku");
            controller.SetProgress(0);
            await Task.Delay(300);
            controller.SetProgress(0.5);

            datagrid_statistiky.Width = 0;
            maingrid.UpdateLayout();
            await Task.Delay(500);
            datagrid_statistiky.Width = maingrid.ActualWidth;
            await Task.Delay(300);




            zvolenypohled = "maxheights";


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
            controller.SetProgress(0.7);


            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_maxheights", VM.BIND_ROUNDS_IN_STATISTICS, 99);

            await Task.Delay(300);
            controller.SetProgress(0.9);
            await controller.CloseAsync();
        }

        private async void statistics_minheight_Click(object sender, RoutedEventArgs e)
        {

            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Vytvářím velmi zajmavou statistiku");
            controller.SetProgress(0);
            await Task.Delay(300);
            controller.SetProgress(0.5);
            datagrid_statistiky.Width = 0;
            maingrid.UpdateLayout();
            await Task.Delay(500);
            datagrid_statistiky.Width = maingrid.ActualWidth;
            await Task.Delay(300);

            zvolenypohled = "minheights";
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

            controller.SetProgress(0.7);
            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_minheights", VM.BIND_ROUNDS_IN_STATISTICS, 99);

            await Task.Delay(300);
            controller.SetProgress(0.9);
            await controller.CloseAsync();

        }

        private async void statistics_timevsheight_Click(object sender, RoutedEventArgs e)
        {

            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Vytvářím velmi zajmavou statistiku");
            controller.SetProgress(0);
            await Task.Delay(300);
            controller.SetProgress(0.5);
            datagrid_statistiky.Width = 0;
            maingrid.UpdateLayout();
            await Task.Delay(500);
            datagrid_statistiky.Width = maingrid.ActualWidth;
            await Task.Delay(300);


            zvolenypohled = "timevsheight";
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
            controller.SetProgress(0.7);
            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_timevsheight", VM.BIND_ROUNDS_IN_STATISTICS, 99);

            await Task.Delay(300);
            controller.SetProgress(0.9);
            await controller.CloseAsync();

        }

        private async void statistics_enemykiled_Click(object sender, RoutedEventArgs e)
        {

            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Vytvářím velmi zajmavou statistiku");
            controller.SetProgress(0);
            await Task.Delay(300);
            controller.SetProgress(0.5);

            zvolenypohled = "killedenemies";
            controller.SetProgress(0.7);
            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_enemykiled", VM.BIND_ROUNDS_IN_STATISTICS, 99);


            await Task.Delay(300);
            controller.SetProgress(0.9);
            await controller.CloseAsync();

        }

        private async void statistics_flighttime_Click(object sender, RoutedEventArgs e)
        {
            zvolenypohled = "flighttime";
            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Vytvářím velmi zajmavou statistiku");
            controller.SetProgress(0);
            await Task.Delay(300);
            controller.SetProgress(0.5);
            datagrid_statistiky.Width = 0;
            maingrid.UpdateLayout();
            await Task.Delay(500);
            datagrid_statistiky.Width = maingrid.ActualWidth;
            await Task.Delay(300);



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

            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_flighttime", VM.BIND_ROUNDS_IN_STATISTICS,99);



            await Task.Delay(300);
            controller.SetProgress(0.9);
            await controller.CloseAsync();


        }

        private async void print_statistics_btn_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Vytvářím zajmavou statistiku");
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

            if (zvolenypohled == "landing")
            {
                VM.print_statistics("frame_small_info", "data_empty", "print_statistics_landing", "statistics_landing", "Přistání", "html", headers, visibility, VM.BIND_ROUNDS_IN_STATISTICS);
            }

            if (zvolenypohled == "flighttime")
            {
                VM.print_statistics("frame_small_info", "data_empty", "print_statistics_flighttime", "statistics_flighttime", "Letový čas", "html", headers, visibility, VM.BIND_ROUNDS_IN_STATISTICS);
            }


            if (zvolenypohled == "averageheights")
            {
                VM.print_statistics("frame_small_info", "data_empty", "print_statistics_averageheights", "statistics_averageheights", "Průměrné výšky", "html", headers, visibility, VM.BIND_ROUNDS_IN_STATISTICS);
            }

            if (zvolenypohled == "maxheights")
            {
                VM.print_statistics("frame_small_info", "data_empty", "print_statistics_maxheights", "statistics_maxheights", "Gagarin (max.výška)", "html", headers, visibility, VM.BIND_ROUNDS_IN_STATISTICS);
            }


            if (zvolenypohled == "minheights")
            {
                VM.print_statistics("frame_small_info", "data_empty", "print_statistics_minheights", "statistics_minheights", "Krtek (min.výška)", "html", headers, visibility, VM.BIND_ROUNDS_IN_STATISTICS);
            }


            if (zvolenypohled == "timevsheight")
            {
                VM.print_statistics("frame_small_info", "data_empty", "print_statistics_timevsheights", "statistics_timevsheights", "Čas vs. výška", "html", headers, visibility, VM.BIND_ROUNDS_IN_STATISTICS);
            }


            if (zvolenypohled == "killedenemies")
            {
                VM.print_statistics("frame_small_info", "data_empty", "print_statistics_killedenemies", "statistics_killedenemies", "Počet poražených soupeřů", "html", headers, visibility, VM.BIND_ROUNDS_IN_STATISTICS);
            }

            //VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_averagelandings", VM.BIND_ROUNDS_IN_STATISTICS, true);

            //VM.print_statistics("statistics_" + zvolenypohled, "statistics_" + zvolenypohled, "html", headers, visibility);

            await Task.Delay(300);
            controller.SetProgress(0.9);
            await controller.CloseAsync();

        }

        private async void print_statistics_btnall_Click(object sender, RoutedEventArgs e)
        {
         
        }
    }
}
