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
using System.IO;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace WpfApp6.View
{
    /// <summary>
    /// Interakční logika pro RozvrhView.xaml
    /// </summary>
    public partial class Results_final : UserControl
    {
        string html_all;
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

        private async void results_users_Click(object sender, RoutedEventArgs e)
        {
            int pocetnesenioru = Convert.ToInt32(VM.SQL_READSOUTEZDATA("select count(U.ID) from users U where agecat > 0", ""));

            Console.WriteLine("pocetnesenioru " + pocetnesenioru);
            
            var currentWindow = this.TryFindParent<MetroWindow>();

            if (VM.BINDING_SELECTED_AGECAT_ID == 99 & pocetnesenioru >= 3)
            {

                var result = await currentWindow.ShowMessageAsync("Věkové kategorie", "Je 3 a více soutěžících v neseniorské věkové kategorii. " +
                    "Měl bys vyhlásit výsledky zvláště dle věkových kategorií a nikoliv společnou pro všechny."
                    , MessageDialogStyle.Affirmative, new MetroDialogSettings() { AnimateShow = true, AnimateHide = true });

            }




            var controller = await currentWindow.ShowProgressAsync("Generuji", "Sestavuji, počítám, třídím...");
            controller.SetProgress(0);
            await Task.Delay(300);
            controller.SetProgress(0.5);
            datagrid_vysledky_finalistu.Width = 0;
            datagrid_vysledky_celkove.Width = 0;
            maingrid.UpdateLayout();
            mainstack.UpdateLayout();
            await Task.Delay(500);
            

            datagrid_vysledky_celkove.Width = maingrid.ActualWidth;
            await Task.Delay(300);


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
            controller.SetProgress(0.7);
            await Task.Delay(300);


            VM._ZOBRAZIT_ZAKLADNI_VYSLEDKY_S_SKRTACKAMA = true;

            //VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'False'");

            //for (int s = 0; s < VM.BIND_SQL_SOUTEZ_DELETES; s++)
            //{


              //  string tmp_kolo_pro_skracku;
                //for (int i = 0; i < VM.Players.Count(); i++)
               // {

                 //   tmp_kolo_pro_skracku = VM.SQL_READSOUTEZDATA("select rnd,min(prep) from score where userid=" + VM.Players[i].ID + " and skrtacka='False' and rnd < 100 ", "");
                    //VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'True' where rnd='" + tmp_kolo_pro_skracku + "' and userid=" + VM.Players[i].ID);

                //}

            //}



            VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'False'");

            for (int s = 0; s < VM.BIND_SQL_SOUTEZ_DELETES; s++)
            {


                string tmp_kolo_pro_skracku;
                string tmp_grp_pro_skracku;
                for (int i = 0; i < VM.Players.Count(); i++)
                {

                    tmp_kolo_pro_skracku = VM.SQL_READSOUTEZDATA("select rnd,min(prep) from score where userid=" + VM.Players[i].ID + " and skrtacka='False' and refly='False' and nondeletable = 'False' and rnd <= 100", "");
                    tmp_grp_pro_skracku = VM.SQL_READSOUTEZDATA("select grp,min(prep) from score where userid=" + VM.Players[i].ID + " and skrtacka='False' and refly='False' and nondeletable = 'False' and rnd <= 100", "");
                    VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'True' where rnd='" + tmp_kolo_pro_skracku + "' and grp='" + tmp_grp_pro_skracku + "' and userid=" + VM.Players[i].ID);

                }

            }


            controller.SetProgress(0.8);



            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete",99, Convert.ToInt32(VM.BINDING_SELECTED_AGECAT_ID));
            controller.SetProgress(0.9);
            await Task.Delay(300);
            await controller.CloseAsync();

        }

        private void results_teams_Click(object sender, RoutedEventArgs e)
        {
            //VM.FUNCTION_RESULTS_LOAD_RESULTS("teams");
            //VM.FUNCTION_ROUNDS_LOAD_FINAL_ROUNDS();
        }

        private async void results_final_users_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Sestavuji, počítám, třídím...");
            controller.SetProgress(0);
            await Task.Delay(300);
            controller.SetProgress(0.5);

            datagrid_vysledky_finalistu.Width = 0;
            datagrid_vysledky_celkove.Width = 0;
            maingrid.UpdateLayout();
            mainstack.UpdateLayout();
            await Task.Delay(500);
            datagrid_vysledky_finalistu.Width = maingrid.ActualWidth;


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
            controller.SetProgress(0.8);
            await Task.Delay(300);

            VM.FUNCTION_RESULTS_LOAD_RESULTS("final_users",99,99);
            controller.SetProgress(0.9);
            await Task.Delay(300);
            await controller.CloseAsync();
        }

        private void ToggleSwitch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Console.Write("xxx");
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

        private void print_completeresults_btn_Click(object sender, RoutedEventArgs e)
        {



            string[] visibility = {
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
                "True",
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
            if (p_natlic.IsOn is true) { visibility[2] = "True"; } else { visibility[2] = "False"; }
            if (p_failic.IsOn is true) { visibility[3] = "True"; } else { visibility[3] = "False"; }
            if (p_agecat.IsOn is true) { visibility[4] = "True"; } else { visibility[4] = "False"; }
            if (p_gpen.IsOn is true) { visibility[5] = "True"; } else { visibility[5] = "False"; }
            if (p_fztrata.IsOn is true) { visibility[6] = "True"; } else { visibility[6] = "False"; }
            if (p_fscore.IsOn is true) { visibility[7] = "True"; } else { visibility[7] = "False"; }

            if (VM.BIND_SQL_SOUTEZ_ROUNDSFINALE == 0 ) {
                visibility[6] = "False";
                visibility[7] = "False";
            }
            if (1 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[8] = "True"; } else { visibility[8] = "False"; }
            if (2 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[9] = "True"; } else { visibility[9] = "False"; }
            if (3 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[10] = "True"; } else { visibility[10] = "False"; }
            if (4 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[11] = "True"; } else { visibility[11] = "False"; }
            if (5 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[12] = "True"; } else { visibility[12] = "False"; }


            if (p_bonus.IsOn is true) { visibility[13] = "True"; } else { visibility[13] = "False"; }
            if (p_1000.IsOn is true) { visibility[14] = "True"; } else { visibility[14] = "False"; }
            if (p_zscore.IsOn is true) { visibility[15] = "True"; } else { visibility[15] = "False"; }
            if (p_zztrata.IsOn is true) { visibility[16] = "True"; } else { visibility[16] = "False"; }




            if (1 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[17] = "True"; } else { visibility[17] = "False"; }
            if (2 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[18] = "True"; } else { visibility[18] = "False"; }
            if (3 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[19] = "True"; } else { visibility[19] = "False"; }
            if (4 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[20] = "True"; } else { visibility[20] = "False"; }
            if (5 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[21] = "True"; } else { visibility[21] = "False"; }
            if (6 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[22] = "True"; } else { visibility[22] = "False"; }
            if (7 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[23] = "True"; } else { visibility[23] = "False"; }
            if (8 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[24] = "True"; } else { visibility[24] = "False"; }
            if (9 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[25] = "True"; } else { visibility[25] = "False"; }
            if (10 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[26] = "True"; } else { visibility[26] = "False"; }

            if (11 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[27] = "True"; } else { visibility[27] = "False"; }
            if (12 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[28] = "True"; } else { visibility[28] = "False"; }
            if (13 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[29] = "True"; } else { visibility[29] = "False"; }
            if (14 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[30] = "True"; } else { visibility[30] = "False"; }
            if (15 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[31] = "True"; } else { visibility[31] = "False"; }
            if (16 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[32] = "True"; } else { visibility[32] = "False"; }
            if (17 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[33] = "True"; } else { visibility[33] = "False"; }
            if (18 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[34] = "True"; } else { visibility[34] = "False"; }
            if (19 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[35] = "True"; } else { visibility[35] = "False"; }
            if (20 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[36] = "True"; } else { visibility[36] = "False"; }

            //VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, Convert.ToInt32(VM.BINDING_SELECTED_AGECAT_ID));
            //VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", "bind", "memory", visibility);
            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, 99);
            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", VM.agecatitems[0], "memory", visibility);

            System.Threading.Thread.Sleep(50);

            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, 0);
            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", VM.agecatitems[1], "memory", visibility);

            System.Threading.Thread.Sleep(50);

            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, 1);
            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", VM.agecatitems[2], "memory", visibility);

            System.Threading.Thread.Sleep(50);

            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, 2);
            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", VM.agecatitems[3], "memory", visibility);

            System.Threading.Thread.Sleep(50);


            VM.print_memory_to_file("frame_with_contest_info", "data_empty", "print_complete_resuls", "Celkové oficiální výsledky", "html",true);

        }




        private void print_finalresults_btn_Click(object sender, RoutedEventArgs e)
        {



            string[] visibility = {
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



            if (pf_stat.IsOn is true) { visibility[0] = "True"; } else { visibility[0] = "False"; }
            if (pf_id.IsOn is true) { visibility[1] = "True"; } else { visibility[1] = "False"; }
            if (pf_natlic.IsOn is true) { visibility[2] = "True"; } else { visibility[2] = "False"; }
            if (pf_failic.IsOn is true) { visibility[3] = "True"; } else { visibility[3] = "False"; }
            if (pf_agecat.IsOn is true) { visibility[4] = "True"; } else { visibility[4] = "False"; }
            if (pf_gpen.IsOn is true) { visibility[5] = "True"; } else { visibility[5] = "False"; }
            if (pf_ztrata.IsOn is true) { visibility[6] = "True"; } else { visibility[6] = "False"; }


            if (1 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[7] = "True"; } else { visibility[7] = "False"; }
            if (2 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[8] = "True"; } else { visibility[8] = "False"; }
            if (3 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[9] = "True"; } else { visibility[9] = "False"; }
            if (4 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[10] = "True"; } else { visibility[10] = "False"; }
            if (5 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[11] = "True"; } else { visibility[11] = "False"; }


            VM.print_final_results("frame_small_info", "data_empty", "print_complete_resuls", "Výsledky finále", "html", visibility);


        }

        private async void print_sorgairresults_btn_Click(object sender, RoutedEventArgs e)
        {


            string[] visibility = {
                "Visible"
            };

            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Vytvářím velmi zajmavou statistiku");
            controller.SetProgress(0);
            await Task.Delay(300);
            controller.SetProgress(0.5);



            VM.print_userslist("frame_empty", "data_userlist", "print_userlist", "Seznam soutěžících", "memory");


            //VM.print_userstatistics("frame_empty", "data_userstatistics", "print_userstatistics", "Statistiky uživatelů", "memory");


            VM.print_matrix("frame_empty", "data_matrix", "print_basic_resuls", "Rozlosování", "memory");


            #region Základní výsledky


            VM.FUNCTION_RESULTS_LOAD_RESULTS("users", VM.BIND_SQL_SOUTEZ_ROUNDS, 99);

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



            if (p_stat.IsOn is true) { visibility[0] = "True"; } else { visibility[0] = "False"; }
            if (p_id.IsOn is true) { visibility[1] = "True"; } else { visibility[1] = "False"; }
            if (p_agecat.IsOn is true) { visibility[2] = "True"; } else { visibility[2] = "False"; }
            if (p_gpen.IsOn is true) { visibility[3] = "True"; } else { visibility[3] = "False"; }
            if (p_zztrata.IsOn is true) { visibility[4] = "True"; } else { visibility[4] = "False"; }
            if (1 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[5] = "True"; } else { visibility[5] = "False"; }
            if (2 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[6] = "True"; } else { visibility[6] = "False"; }
            if (3 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[7] = "True"; } else { visibility[7] = "False"; }
            if (4 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[8] = "True"; } else { visibility[8] = "False"; }
            if (5 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[9] = "True"; } else { visibility[9] = "False"; }
            if (6 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[10] = "True"; } else { visibility[10] = "False"; }
            if (7 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[11] = "True"; } else { visibility[11] = "False"; }
            if (8 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[12] = "True"; } else { visibility[12] = "False"; }
            if (9 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[13] = "True"; } else { visibility[13] = "False"; }
            if (10 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[14] = "True"; } else { visibility[14] = "False"; }

            if (11 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[15] = "True"; } else { visibility[15] = "False"; }
            if (12 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[16] = "True"; } else { visibility[16] = "False"; }
            if (13 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[17] = "True"; } else { visibility[17] = "False"; }
            if (14 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[18] = "True"; } else { visibility[18] = "False"; }
            if (15 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[19] = "True"; } else { visibility[19] = "False"; }
            if (16 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[20] = "True"; } else { visibility[20] = "False"; }
            if (17 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[21] = "True"; } else { visibility[21] = "False"; }
            if (18 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[22] = "True"; } else { visibility[22] = "False"; }
            if (19 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[23] = "True"; } else { visibility[23] = "False"; }
            if (20 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[24] = "True"; } else { visibility[24] = "False"; }



            VM.print_basicresults("frame_empty", "data_empty", "print_basic_resuls", "Základní výsledky", "memory", visibility);

            #endregion



            #region celkové výsledky





            VM._ZOBRAZIT_ZAKLADNI_VYSLEDKY_S_SKRTACKAMA = true;



            VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'False'");

            for (int s = 0; s < VM.BIND_SQL_SOUTEZ_DELETES; s++)
            {


                string tmp_kolo_pro_skracku;
                string tmp_grp_pro_skracku;
                for (int i = 0; i < VM.Players.Count(); i++)
                {

                    tmp_kolo_pro_skracku = VM.SQL_READSOUTEZDATA("select rnd,min(prep) from score where userid=" + VM.Players[i].ID + " and skrtacka='False' and refly='False' and nondeletable = 'False' and rnd <= 100", "");
                    tmp_grp_pro_skracku = VM.SQL_READSOUTEZDATA("select grp,min(prep) from score where userid=" + VM.Players[i].ID + " and skrtacka='False' and refly='False' and nondeletable = 'False' and rnd <= 100", "");
                    VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'True' where rnd='" + tmp_kolo_pro_skracku + "' and grp='" + tmp_grp_pro_skracku + "' and userid=" + VM.Players[i].ID);

                }

            }





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




            if (p_stat.IsOn is true) { visibility[0] = "True"; } else { visibility[0] = "False"; }
            if (p_id.IsOn is true) { visibility[1] = "True"; } else { visibility[1] = "False"; }
            if (p_natlic.IsOn is true) { visibility[2] = "True"; } else { visibility[2] = "False"; }
            if (p_failic.IsOn is true) { visibility[3] = "True"; } else { visibility[3] = "False"; }
            if (p_agecat.IsOn is true) { visibility[4] = "True"; } else { visibility[4] = "False"; }
            if (p_gpen.IsOn is true) { visibility[5] = "True"; } else { visibility[5] = "False"; }
            if (p_fztrata.IsOn is true) { visibility[6] = "True"; } else { visibility[6] = "False"; }
            if (p_fscore.IsOn is true) { visibility[7] = "True"; } else { visibility[7] = "False"; }


            if (VM.BIND_SQL_SOUTEZ_ROUNDSFINALE == 0)
            {
                visibility[6] = "False";
                visibility[7] = "False";
            }


            if (1 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[8] = "True"; } else { visibility[8] = "False"; }
            if (2 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[9] = "True"; } else { visibility[9] = "False"; }
            if (3 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[10] = "True"; } else { visibility[10] = "False"; }
            if (4 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[11] = "True"; } else { visibility[11] = "False"; }
            if (5 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[12] = "True"; } else { visibility[12] = "False"; }


            if (p_bonus.IsOn is true) { visibility[13] = "True"; } else { visibility[13] = "False"; }
            if (p_1000.IsOn is true) { visibility[14] = "True"; } else { visibility[14] = "False"; }
            if (p_zscore.IsOn is true) { visibility[15] = "True"; } else { visibility[15] = "False"; }
            if (p_zztrata.IsOn is true) { visibility[16] = "True"; } else { visibility[16] = "False"; }




            if (1 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[17] = "True"; } else { visibility[17] = "False"; }
            if (2 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[18] = "True"; } else { visibility[18] = "False"; }
            if (3 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[19] = "True"; } else { visibility[19] = "False"; }
            if (4 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[20] = "True"; } else { visibility[20] = "False"; }
            if (5 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[21] = "True"; } else { visibility[21] = "False"; }
            if (6 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[22] = "True"; } else { visibility[22] = "False"; }
            if (7 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[23] = "True"; } else { visibility[23] = "False"; }
            if (8 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[24] = "True"; } else { visibility[24] = "False"; }
            if (9 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[25] = "True"; } else { visibility[25] = "False"; }
            if (10 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[26] = "True"; } else { visibility[26] = "False"; }


            if (11 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[27] = "True"; } else { visibility[27] = "False"; }
            if (12 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[28] = "True"; } else { visibility[28] = "False"; }
            if (13 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[29] = "True"; } else { visibility[29] = "False"; }
            if (14 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[30] = "True"; } else { visibility[30] = "False"; }
            if (15 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[31] = "True"; } else { visibility[31] = "False"; }
            if (16 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[32] = "True"; } else { visibility[32] = "False"; }
            if (17 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[33] = "True"; } else { visibility[33] = "False"; }
            if (18 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[34] = "True"; } else { visibility[34] = "False"; }
            if (19 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[35] = "True"; } else { visibility[35] = "False"; }
            if (20 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[36] = "True"; } else { visibility[36] = "False"; }




            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, 99);
            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", "Celkové výsledky - " + VM.agecatitems[0], "memory", visibility);

            System.Threading.Thread.Sleep(50);

            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, 0);
            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", "Celkové výsledky - " + VM.agecatitems[1], "memory", visibility);

            System.Threading.Thread.Sleep(50);

            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, 1);
            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", "Celkové výsledky - " + VM.agecatitems[2], "memory", visibility);

            System.Threading.Thread.Sleep(50);

            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, 2);
            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", "Celkové výsledky - " + VM.agecatitems[3], "memory", visibility);

            System.Threading.Thread.Sleep(50);



            #endregion

            #region prumerne pristani
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

            visibility = new string[] {
                "Visible",
               "Hidden",
                "Visible",
                "Hidden",
                "Hidden",
                "Hidden",
                "Visible"
            };

            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_averagelandings", VM.BIND_SQL_SOUTEZ_ROUNDS,99);
            //VM.print_statistics("statistics_c_landing", "statistics_landing", "memory", headers, visibility);
            VM.print_statistics("frame_empty", "data_empty", "print_complete_resuls", "statistics_landing", "Přistání", "memory", headers, visibility, VM.BIND_SQL_SOUTEZ_ROUNDS);

            ///////////////////////////////////////////////////////////
            #endregion


            #region letovy cas
          
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

            
            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_flighttime", VM.BIND_SQL_SOUTEZ_ROUNDS,99);
            //VM.print_statistics("statistics_c_flighttime", "statistics_flighttime", "memory", headers, visibility);
            VM.print_statistics("frame_empty", "data_empty", "print_complete_resuls", "statistics_flighttime", "Letový čas", "memory", headers, visibility, VM.BIND_SQL_SOUTEZ_ROUNDS);

            #endregion

            #region prumerna vyska
      

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



            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_averageheights", VM.BIND_SQL_SOUTEZ_ROUNDS,99);
            //VM.print_statistics("statistics_c_averageheights", "statistics_averageheights", "memory", headers, visibility);

            VM.print_statistics("frame_empty", "data_empty", "print_complete_resuls", "statistics_averageheights", "Průměrná výška", "memory", headers, visibility, VM.BIND_SQL_SOUTEZ_ROUNDS);
            #endregion



            #region max vyska




            headers = new string[] {
            "Pořadí",
            "Soutěžící",
            "Stát",
            "ID",
            "Záznamů",
            "↑ Max výška",
            "---",
            "Σ Celková výška",
            "---",
            "ø Průměr",
            "---",
            "Hodnoty"
            };

            visibility = new string[]{
               "Visible",
                "Hidden",
                "Visible",
                "Hidden",
                "Visible",
                "Hidden",
                "Visible"
            };



            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_maxheights", VM.BIND_SQL_SOUTEZ_ROUNDS, 99);
            //VM.print_statistics("statistics_c_averageheights", "statistics_averageheights", "memory", headers, visibility);

            VM.print_statistics("frame_empty", "data_empty", "print_statistics_maxheights", "statistics_maxheights", "Gagarin (max.výška)", "memory", headers, visibility, VM.BIND_SQL_SOUTEZ_ROUNDS);
            #endregion


            #region min vyska





            headers = new string[] {
            "Pořadí",
            "Soutěžící",
            "Stát",
            "ID",
            "Záznamů",
            "Minimální výška",
            "Σ Celková doba",
            "---",
            "Σ výšky / Σ bodů",
            "Bodů za metr",
            "---",
            "Hodnoty"
            };

            visibility = new string[]{
                "Visible",
                "Hidden",
                "Hidden",
                "Visible",
                "Visible",
                "Hidden",
                "Visible"
            };



            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_minheights", VM.BIND_SQL_SOUTEZ_ROUNDS, 99);
            //VM.print_statistics("statistics_c_averageheights", "statistics_averageheights", "memory", headers, visibility);

            VM.print_statistics("frame_empty", "data_empty", "print_statistics_minheights", "statistics_minheights", "Krtek (min.výška)", "memory", headers, visibility, VM.BIND_SQL_SOUTEZ_ROUNDS);
            #endregion


            #region čas vs výška



            headers = new string[] {
            "Pořadí",
            "Soutěžící",
            "Stát",
            "ID",
            "Záznamů",
            "Minimální výška",
            "ø čas v kole",
            "ø výška v kole",
            "Σ výšky / Σ bodů",
            "na 10 minut\nje třeba metrů",
            "---",
            "Ze 100 metrů"
            };

            visibility = new string[]{
                "Hidden",
                "Visible",
                "Visible",
                "Hidden",
                "Visible",
                "Hidden",
                "Visible"
            };



            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_timevsheight", VM.BIND_SQL_SOUTEZ_ROUNDS, 99);
            //VM.print_statistics("statistics_c_averageheights", "statistics_averageheights", "memory", headers, visibility);

            VM.print_statistics("frame_empty", "data_empty", "print_statistics_timevsheights", "statistics_timevsheights", "Čas vs. výška", "memory", headers, visibility, VM.BIND_SQL_SOUTEZ_ROUNDS);
            #endregion




            VM.print_memory_to_file("frame_with_contest_info", "data_empty", "print_complete_overview", "SORG AIR Megavýsledovka", "html",true );


            await Task.Delay(300);
            controller.SetProgress(0.9);
            await controller.CloseAsync();

        }



        void selectMeDropDownButton_TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var value = ((TextBlock)e.Source).DataContext;
                Console.WriteLine(value);

                VM.BINDING_SELECTED_AGECAT = value.ToString();
            }
        }



        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("sdasdasd");
        }
    }
}
