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

namespace WpfApp6.View
{
    /// <summary>
    /// Interakční logika pro FirstView.xaml
    /// </summary>
    public partial class Detail : UserControl
    {
        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;
        public Detail()
        {

            InitializeComponent();
        }



        private void statistics_landing_Click(object sender, RoutedEventArgs e)
        {
           
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
            datagrid_statistiky.Columns[9].Visibility= Visibility.Hidden ;
            datagrid_statistiky.Columns[10].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[11].Visibility = Visibility.Visible;

            VM.FUNCTION_RESULTS_LOADBASERESULTS("statistics_averagelandings");
        }

        private void statistics_averageheight_Click(object sender, RoutedEventArgs e)
        {
            
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
            datagrid_statistiky.Columns[9].Visibility = Visibility.Hidden ;
            datagrid_statistiky.Columns[10].Visibility = Visibility.Hidden;
            datagrid_statistiky.Columns[11].Visibility = Visibility.Visible;

            VM.FUNCTION_RESULTS_LOADBASERESULTS("statistics_averageheights");
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
            datagrid_statistiky.Columns[6].Visibility = Visibility.Hidden ;
            datagrid_statistiky.Columns[7].Visibility = Visibility.Hidden ;
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

        private void statistics_flighttime_Click(object sender, RoutedEventArgs e)
        {


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
            datagrid_statistiky.Columns[10].Visibility = Visibility.Hidden ;
            datagrid_statistiky.Columns[11].Visibility = Visibility.Visible;

            VM.FUNCTION_RESULTS_LOADBASERESULTS("statistics_flighttime");




        }
    }
}
