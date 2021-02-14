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
    /// Interakční logika pro RozvrhView.xaml
    /// </summary>
    public partial class Results : UserControl
    {
        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;
        public Results()
        {
            InitializeComponent();
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_RESULTS_LOADBASERESULTS();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           
        }

        private void printresults_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printdialog = new PrintDialog();
            if (printdialog.ShowDialog() == true)
            {
                printdialog.PrintVisual(dataGrid1, "test");

            }
        }
    }
}
