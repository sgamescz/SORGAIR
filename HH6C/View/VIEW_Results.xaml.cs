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
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;


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

       

        private void printresults_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printdialog = new PrintDialog();
            if (printdialog.ShowDialog() == true)
            {
                printdialog.PrintVisual(dataGrid_clasic_results, "test");

            }
        }

        private void results_users_Click(object sender, RoutedEventArgs e)
        {

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




            VM.FUNCTION_RESULTS_LOADBASERESULTS("users");
        }

        private void results_teams_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_RESULTS_LOADBASERESULTS("teams");
            //VM.FUNCTION_ROUNDS_LOAD_FINAL_ROUNDS();

        }

        private async void results_move_to_final_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            var result = await currentWindow.ShowMessageAsync("Postoupit do finále?", "Opravdu postoupit zobrazené soutěžící do finálových kol?", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { AnimateShow = true, AnimateHide = true});
            if (result == null)
                return;
            if (result == MessageDialogResult.Affirmative)
            {
                VM.FUNCTION_ROUNDS_CREATE_FINAL_ROUNDS();
                VM.BIND_JETREBAROZLOSOVAT_SCORE_FINAL = VM.BIND_SQL_SOUTEZ_ROUNDSFINALE * VM.BIND_SQL_SOUTEZ_STARTPOINTSFINALE;
                VM.FUNCTION_JETREBAROZLOSOVAT_OVER_FINAL();
                VM.BINDING_selectedmenuindex = 10;
            }

        }

        private void ToggleSwitchfinal_Toggled(object sender, RoutedEventArgs e)
        {
            if (dataGrid_clasic_results != null)
            {

                MahApps.Metro.Controls.ToggleSwitch tagbutton = sender as MahApps.Metro.Controls.ToggleSwitch;

                var index = dataGrid_clasic_results.Columns.Single(c => c.Header.ToString() == tagbutton.Content.ToString()).DisplayIndex;

                if (tagbutton.IsOn == true)
                {
                    dataGrid_clasic_results.Columns[index].Visibility = Visibility.Visible;
                }
                else
                {
                    dataGrid_clasic_results.Columns[index].Visibility = Visibility.Collapsed;
                }
            }

        }
    }
}
