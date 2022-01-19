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


            VM._ZOBRAZIT_ZAKLADNI_VYSLEDKY_S_SKRTACKAMA = true;

            VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'False'");

            for (int s = 0; s < VM.BIND_SQL_SOUTEZ_DELETES; s++)
            {


                string tmp_kolo_pro_skracku;
                for (int i = 0; i < VM.Players.Count(); i++)
                {

                    tmp_kolo_pro_skracku = VM.SQL_READSOUTEZDATA("select rnd,min(prep) from score where userid=" + VM.Players[i].ID + " and skrtacka='False' and rnd < 100 ", "");
                    VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'True' where rnd='" + tmp_kolo_pro_skracku + "' and userid=" + VM.Players[i].ID);

                }

            }




            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete",99,false);

        }

        private void results_teams_Click(object sender, RoutedEventArgs e)
        {
            //VM.FUNCTION_RESULTS_LOAD_RESULTS("teams");
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

            VM.FUNCTION_RESULTS_LOAD_RESULTS("final_users",99,false);
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


            if (1 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE ) { visibility[8] = "True"; } else { visibility[8] = "False"; }
            if (2 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[9] = "True"; } else { visibility[9] = "False"; }
            if (3 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[10] = "True"; } else { visibility[10] = "False"; }
            if (4 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[11] = "True"; } else { visibility[11] = "False"; }
            if (5 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[12] = "True"; } else { visibility[12] = "False"; }


            if (p_bonus.IsOn is true) { visibility[13] = "True"; } else { visibility[13] = "False"; }
            if (p_1000.IsOn is true) { visibility[14] = "True"; } else { visibility[14] = "False"; }
            if (p_zscore.IsOn is true) { visibility[15] = "True"; } else { visibility[15] = "False"; }
            if (p_zztrata.IsOn is true) { visibility[16] = "True"; } else { visibility[16] = "False"; }




            if (1 <= VM.BIND_SQL_SOUTEZ_ROUNDS ) { visibility[17] = "True"; } else { visibility[17] = "False"; }
            if (2 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[18] = "True"; } else { visibility[18] = "False"; }
            if (3 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[19] = "True"; } else { visibility[19] = "False"; }
            if (4 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[20] = "True"; } else { visibility[20] = "False"; }
            if (5 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[21] = "True"; } else { visibility[21] = "False"; }
            if (6 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[22] = "True"; } else { visibility[22] = "False"; }
            if (7 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[23] = "True"; } else { visibility[23] = "False"; }
            if (8 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[24] = "True"; } else { visibility[24] = "False"; }
            if (9 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[25] = "True"; } else { visibility[25] = "False"; }
            if (10 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[26] = "True"; } else { visibility[26] = "False"; }



            VM.print_completeresults("frame_with_contest_info", "data_empty", "print_complete_resuls", "Celkové výsledky soutěže", "html", visibility );
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


            VM.print_matrix("frame_empty", "data_matrix", "print_basic_resuls", "Rozlosování", "memory");


            #region Základní výsledky


            VM.FUNCTION_RESULTS_LOAD_RESULTS("users", VM.BIND_SQL_SOUTEZ_ROUNDS,false );

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




            VM.print_basicresults("frame_empty", "data_empty", "print_basic_resuls", "Základní výsledky", "memory", visibility);

            #endregion



            #region celkové výsledky





            VM._ZOBRAZIT_ZAKLADNI_VYSLEDKY_S_SKRTACKAMA = true;

            VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'False'");

            for (int s = 0; s < VM.BIND_SQL_SOUTEZ_DELETES; s++)
            {


                string tmp_kolo_pro_skracku;
                for (int i = 0; i < VM.Players.Count(); i++)
                {

                    tmp_kolo_pro_skracku = VM.SQL_READSOUTEZDATA("select rnd,min(prep) from score where userid=" + VM.Players[i].ID + " and skrtacka='False' and rnd < 100 ", "");
                    VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'True' where rnd='" + tmp_kolo_pro_skracku + "' and userid=" + VM.Players[i].ID);

                }

            }




            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, false);




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
            if (p_natlic.IsOn is true) { visibility[2] = "True"; } else { visibility[2] = "False"; }
            if (p_failic.IsOn is true) { visibility[3] = "True"; } else { visibility[3] = "False"; }
            if (p_agecat.IsOn is true) { visibility[4] = "True"; } else { visibility[4] = "False"; }
            if (p_gpen.IsOn is true) { visibility[5] = "True"; } else { visibility[5] = "False"; }
            if (p_fztrata.IsOn is true) { visibility[6] = "True"; } else { visibility[6] = "False"; }
            if (p_fscore.IsOn is true) { visibility[7] = "True"; } else { visibility[7] = "False"; }


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





            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", "Celkové výsledky", "memory", visibility);


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

            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_averagelandings", VM.BIND_SQL_SOUTEZ_ROUNDS,true);
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

            
            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_flighttime", VM.BIND_SQL_SOUTEZ_ROUNDS,false);
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



            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_averageheights", VM.BIND_SQL_SOUTEZ_ROUNDS,false);
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



            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_maxheights", VM.BIND_SQL_SOUTEZ_ROUNDS, false);
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



            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_minheights", VM.BIND_SQL_SOUTEZ_ROUNDS, false);
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



            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_timevsheight", VM.BIND_SQL_SOUTEZ_ROUNDS, false);
            //VM.print_statistics("statistics_c_averageheights", "statistics_averageheights", "memory", headers, visibility);

            VM.print_statistics("frame_empty", "data_empty", "print_statistics_timevsheights", "statistics_timevsheights", "Čas vs. výška", "memory", headers, visibility, VM.BIND_SQL_SOUTEZ_ROUNDS);
            #endregion





            VM.print_memory_to_file("frame_with_contest_info", "data_empty", "print_complete_overview", "CMPLSRES", "html");


            await Task.Delay(300);
            controller.SetProgress(0.9);
            await controller.CloseAsync();

        }
    }
}
