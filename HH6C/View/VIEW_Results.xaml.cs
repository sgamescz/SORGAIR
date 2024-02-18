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

        private async void udelej_zobrazeni_vysledku()
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Sestavuji, počítám, třídím...");
            controller.SetProgress(0);
            await Task.Delay(300);
            controller.SetProgress(0.5);
            dataGrid_clasic_results.Width = 0;
            maingrid.UpdateLayout();
            await Task.Delay(500);
            dataGrid_clasic_results.Width = maingrid.ActualWidth;
            dataGrid_clasic_results.Visibility = Visibility.Visible;
            await Task.Delay(300);
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

            R11VISIBILITY.Visibility = Visibility.Hidden;
            R12VISIBILITY.Visibility = Visibility.Hidden;
            R13VISIBILITY.Visibility = Visibility.Hidden;
            R14VISIBILITY.Visibility = Visibility.Hidden;
            R15VISIBILITY.Visibility = Visibility.Hidden;
            R16VISIBILITY.Visibility = Visibility.Hidden;
            R17VISIBILITY.Visibility = Visibility.Hidden;
            R18VISIBILITY.Visibility = Visibility.Hidden;
            R19VISIBILITY.Visibility = Visibility.Hidden;
            R20VISIBILITY.Visibility = Visibility.Hidden;



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

                if (i == 11) { R11VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 12) { R12VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 13) { R13VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 14) { R14VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 15) { R15VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 16) { R16VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 17) { R17VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 18) { R18VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 19) { R19VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 20) { R20VISIBILITY.Visibility = Visibility.Visible; }

            }




            VM.FUNCTION_RESULTS_LOAD_RESULTS("users", VM.BIND_ROUNDS_IN_RESULTS,99);

            //dataGrid_clasic_results.Columns[11].Width = new 
            //      DataGridLength(1, DataGridLengthUnitType.SizeToHeader);

            controller.SetProgress(0.9);
            await Task.Delay(300);
            await controller.CloseAsync();


        }
        private void results_users_Click(object sender, RoutedEventArgs e)
        {
            VM._ZOBRAZIT_ZAKLADNI_VYSLEDKY_S_SKRTACKAMA = false;
            VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'False'");
            VM.online_updateskrtaci_all(0);
            udelej_zobrazeni_vysledku();
        }

        private void results_teams_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_RESULTS_LOAD_RESULTS("teams", VM.BIND_ROUNDS_IN_RESULTS,99);
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
                "k10",
                "kolo11",
                "kolo12",
                "kolo13",
                "kolo14",
                "kolo15",
                "kolo16",
                "k17",
                "k18",
                "k19",
                "k20"
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

            if (11 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[15] = "True"; } else { visibility[15] = "False"; }
            if (12 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[16] = "True"; } else { visibility[16] = "False"; }
            if (13 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[17] = "True"; } else { visibility[17] = "False"; }
            if (14 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[18] = "True"; } else { visibility[18] = "False"; }
            if (15 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[19] = "True"; } else { visibility[19] = "False"; }
            if (16 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[20] = "True"; } else { visibility[20] = "False"; }
            if (17 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[21] = "True"; } else { visibility[21] = "False"; }
            if (18 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[22] = "True"; } else { visibility[22] = "False"; }
            if (19 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[23] = "True"; } else { visibility[23] = "False"; }
            if (20 <= VM.BIND_ROUNDS_IN_RESULTS) { visibility[24] = "True"; } else { visibility[24] = "False"; }


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
            VM.online_updateskrtaci_all(0);


            for (int s = 0; s < VM.BIND_SQL_SOUTEZ_DELETES; s++)
            {


                string tmp_kolo_pro_skracku;
                string tmp_grp_pro_skracku;
                for (int i = 0; i < VM.Players_Baseresults.Count(); i++)
                {

                    tmp_kolo_pro_skracku = VM.SQL_READSOUTEZDATA("select rnd,min(prep) from score where userid=" + VM.Players_Baseresults[i].ID + " and skrtacka='False' and refly='False' and nondeletable = 'False' and rnd <= " + VM.BIND_ROUNDS_IN_RESULTS, "");
                    tmp_grp_pro_skracku = VM.SQL_READSOUTEZDATA("select grp,min(prep) from score where userid=" + VM.Players_Baseresults[i].ID + " and skrtacka='False' and refly='False' and nondeletable = 'False' and rnd <= " + VM.BIND_ROUNDS_IN_RESULTS, "");
                    VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'True' where rnd='" + tmp_kolo_pro_skracku + "' and grp='" + tmp_grp_pro_skracku + "' and userid=" + VM.Players_Baseresults[i].ID);
                    VM.online_updateskrtaci(int.Parse(tmp_kolo_pro_skracku), int.Parse(tmp_grp_pro_skracku), VM.Players_Baseresults[i].ID, 1);

                }

            }

            Console.WriteLine("skrtacky zapsany");
            udelej_zobrazeni_vysledku();

        }
    }
}
