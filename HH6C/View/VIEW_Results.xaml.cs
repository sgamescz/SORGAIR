using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Model;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.IO;



namespace WpfApp6.View
{
    /// <summary>
    /// Interakční logika pro RozvrhView.xaml
    /// </summary>
    public partial class Results : UserControl
    {
        string html_all;

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

        private void udelej_zobrazeni_vysledku()
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


            for (int i = 1; i < VM.BIND_ROUNDS_IN_RESULTS + 1; i++)
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




            VM.FUNCTION_RESULTS_LOAD_RESULTS("users", VM.BIND_ROUNDS_IN_RESULTS);

            //dataGrid_clasic_results.Columns[11].Width = new 
            //      DataGridLength(1, DataGridLengthUnitType.SizeToHeader);

        }
        private void results_users_Click(object sender, RoutedEventArgs e)
        {
            VM._ZOBRAZIT_ZAKLADNI_VYSLEDKY_S_SKRTACKAMA = false;
            VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'False'");

            udelej_zobrazeni_vysledku();
        }

        private void results_teams_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_RESULTS_LOAD_RESULTS("teams", VM.BIND_ROUNDS_IN_RESULTS);
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

        private async void print_baseresults_btn_Click(object sender, RoutedEventArgs e)
        {

            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Připravuji výsledky k tisku");
            controller.SetProgress(0.1);
            await Task.Delay(300);


            string[] visibility = {
                "False",
                "False", 
                "False",
                "True",
                "True",
                "kolo1",
                "kolo2",
                "kolo3",
                "kolo4",
                "kolo5",
                "kolo6",
                "k7",
                "k8",
                "k9",
                "k10"
            };

            if (p_stat.IsOn is true) { visibility[0] = "True"; } else { visibility[0] = "False"; }
            if (p_id.IsOn is true) { visibility[1] = "True"; } else { visibility[1] = "False"; }
            if (p_agecat.IsOn is true) { visibility[2] = "True"; } else { visibility[2] = "False"; }
            if (p_gpen.IsOn is true) { visibility[3] = "True"; } else { visibility[3] = "False"; }
            if (p_ztrata.IsOn is true) { visibility[4] = "True"; } else { visibility[4] = "False"; }
            if (1 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[5] = "True"; } else { visibility[5] = "False"; }
            if (2 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[6] = "True"; } else { visibility[6] = "False"; }
            if (3 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[7] = "True"; } else { visibility[7] = "False"; }
            if (4 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[8] = "True"; } else { visibility[8] = "False"; }
            if (5 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[9] = "True"; } else { visibility[9] = "False"; }
            if (6 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[10] = "True"; } else { visibility[10] = "False"; }
            if (7 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[11] = "True"; } else { visibility[11] = "False"; }
            if (8 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[12] = "True"; } else { visibility[12] = "False"; }
            if (9 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[13] = "True"; } else { visibility[13] = "False"; }
            if (10 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[14] = "True"; } else { visibility[14] = "False"; }


            VM.print_basicresults("frame_small_info", "data_empty", "print_basic_resuls", "Základní výsledky", "html", visibility);


            controller.SetProgress(0.7);
            await Task.Delay(300);
            await controller.CloseAsync();
            await Task.Delay(300);




        }


        private void skrtej_Click(object sender, RoutedEventArgs e)
        {

            VM._ZOBRAZIT_ZAKLADNI_VYSLEDKY_S_SKRTACKAMA = true;

            VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'False'");

            for (int s = 0; s < VM.BIND_SQL_SOUTEZ_DELETES; s++)
            {


                string tmp_kolo_pro_skracku;
                string tmp_grp_pro_skracku;
                for (int i = 0; i < VM.Players_Baseresults.Count(); i++)
                {

                    tmp_kolo_pro_skracku = VM.SQL_READSOUTEZDATA("select rnd,min(prep) from score where userid=" + VM.Players_Baseresults[i].ID + " and skrtacka='False' and refly='False' and rnd <= " + VM.BIND_ROUNDS_IN_RESULTS, "");
                    tmp_grp_pro_skracku = VM.SQL_READSOUTEZDATA("select grp,min(prep) from score where userid=" + VM.Players_Baseresults[i].ID + " and skrtacka='False' and refly='False' and rnd <= " + VM.BIND_ROUNDS_IN_RESULTS, "");
                    VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'True' where rnd='" + tmp_kolo_pro_skracku + "' and grp='" + tmp_grp_pro_skracku + "' and userid=" + VM.Players_Baseresults[i].ID);

                }

            }

            Console.WriteLine("skrtacky zapsany");
            udelej_zobrazeni_vysledku();

        }
    }
}
