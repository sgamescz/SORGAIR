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
    public partial class Results_final : UserControl
    {
        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;
        public Results_final()
        {
            InitializeComponent();
        }

       

        private void printresults_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printdialog = new PrintDialog();
            if (printdialog.ShowDialog() == true)
            {
                printdialog.PrintVisual(datagrid_vysledky_finalistu, "test");

            }
        }

        private void results_users_Click(object sender, RoutedEventArgs e)
        {


            R1VISIBILITYFINAL_FR.Visibility = Visibility.Hidden;
            R2VISIBILITYFINAL_FR.Visibility = Visibility.Hidden;
            R3VISIBILITYFINAL_FR.Visibility = Visibility.Hidden;
            R4VISIBILITYFINAL_FR.Visibility = Visibility.Hidden;
            R5VISIBILITYFINAL_FR.Visibility = Visibility.Hidden;

            R1VISIBILITY.Visibility = Visibility.Hidden;
            R2VISIBILITY.Visibility = Visibility.Hidden;
            R3VISIBILITY.Visibility = Visibility.Hidden;
            R4VISIBILITY.Visibility = Visibility.Hidden;
            R5VISIBILITY.Visibility = Visibility.Hidden;
            R6VISIBILITY.Visibility = Visibility.Hidden;
            R7VISIBILITY.Visibility = Visibility.Hidden;
            R8VISIBILITY.Visibility = Visibility.Hidden;
            R9VISIBILITY.Visibility = Visibility.Hidden;
            R10VISIBILITY.Visibility = Visibility.Hidden;


            for (int i = 1; i < VM.BIND_SQL_SOUTEZ_ROUNDS + 1; i++)
            {
                if (i == 1) { R1VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 2) { R2VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 3) { R3VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 4) { R4VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 5) { R5VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 6) { R6VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 7) { R7VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 8) { R8VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 9) { R9VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 10) { R10VISIBILITY.Visibility = Visibility.Visible; }
            }


            for (int i = 1; i < VM.BIND_SQL_SOUTEZ_ROUNDSFINALE + 1; i++)
            {
                if (i == 1) { R1VISIBILITYFINAL_FR.Visibility = Visibility.Visible; }
                if (i == 2) { R2VISIBILITYFINAL_FR.Visibility = Visibility.Visible; }
                if (i == 3) { R3VISIBILITYFINAL_FR.Visibility = Visibility.Visible; }
                if (i == 4) { R4VISIBILITYFINAL_FR.Visibility = Visibility.Visible; }
                if (i == 5) { R5VISIBILITYFINAL_FR.Visibility = Visibility.Visible; }
            }

            datagrid_vysledky_celkove.Visibility = Visibility.Visible;
            datagrid_vysledky_finalistu.Visibility = Visibility.Collapsed;
            table_filter_complete.Visibility = Visibility.Visible;
            table_filter_final.Visibility = Visibility.Collapsed;

            VM.FUNCTION_RESULTS_LOADBASERESULTS("users_complete");

        }

        private void results_teams_Click(object sender, RoutedEventArgs e)
        {
            //VM.FUNCTION_RESULTS_LOADBASERESULTS("teams");
            //VM.FUNCTION_ROUNDS_LOAD_FINAL_ROUNDS();
        }

        private void results_final_users_Click(object sender, RoutedEventArgs e)
        {
            R1VISIBILITYFINAL.Visibility = Visibility.Hidden;
            R2VISIBILITYFINAL.Visibility = Visibility.Hidden;
            R3VISIBILITYFINAL.Visibility = Visibility.Hidden;
            R4VISIBILITYFINAL.Visibility = Visibility.Hidden;
            R5VISIBILITYFINAL.Visibility = Visibility.Hidden;


            for (int i = 1; i < VM.BIND_SQL_SOUTEZ_ROUNDSFINALE + 1; i++)
            {
                if (i == 1) { R1VISIBILITYFINAL.Visibility = Visibility.Visible; }
                if (i == 2) { R2VISIBILITYFINAL.Visibility = Visibility.Visible; }
                if (i == 3) { R3VISIBILITYFINAL.Visibility = Visibility.Visible; }
                if (i == 4) { R4VISIBILITYFINAL.Visibility = Visibility.Visible; }
                if (i == 5) { R5VISIBILITYFINAL.Visibility = Visibility.Visible; }
            }

            datagrid_vysledky_celkove.Visibility = Visibility.Collapsed;
            datagrid_vysledky_finalistu.Visibility = Visibility.Visible;
            table_filter_complete.Visibility = Visibility.Collapsed;
            table_filter_final.Visibility = Visibility.Visible;

            VM.FUNCTION_RESULTS_LOADBASERESULTS("final_users");
        }

        private void ToggleSwitch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (datagrid_vysledky_celkove != null)
            {

                MahApps.Metro.Controls.ToggleSwitch tagbutton = sender as MahApps.Metro.Controls.ToggleSwitch;

                var index = datagrid_vysledky_celkove.Columns.Single(c => c.Header.ToString() == tagbutton.Content.ToString()).DisplayIndex;

                if (tagbutton.IsOn == true)
                {
                    datagrid_vysledky_celkove.Columns[index].Visibility = Visibility.Visible;
                }
                else
                {
                    datagrid_vysledky_celkove.Columns[index].Visibility = Visibility.Collapsed;
                }
            }


        }


        private void ToggleSwitchfinal_Toggled(object sender, RoutedEventArgs e)
        {
            if (datagrid_vysledky_finalistu != null)
            {

                MahApps.Metro.Controls.ToggleSwitch tagbutton = sender as MahApps.Metro.Controls.ToggleSwitch;

                var index = datagrid_vysledky_finalistu.Columns.Single(c => c.Header.ToString() == tagbutton.Content.ToString()).DisplayIndex;

                if (tagbutton.IsOn == true)
                {
                    datagrid_vysledky_finalistu.Columns[index].Visibility = Visibility.Visible;
                }
                else
                {
                    datagrid_vysledky_finalistu.Columns[index].Visibility = Visibility.Collapsed;
                }
            }

        }
    }
}
