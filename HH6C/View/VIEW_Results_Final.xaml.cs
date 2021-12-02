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




            VM.FUNCTION_RESULTS_LOADBASERESULTS("users_complete");

        }

        private void results_teams_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_RESULTS_LOADBASERESULTS("teams");
            VM.FUNCTION_ROUNDS_LOAD_FINAL_ROUNDS();
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


            VM.print_completeresults("frame_with_contest_info", "data_empty", "print_complete_resuls", "WHAT333", "html", visibility );
        }




        private void print_finalresults_btn_Click(object sender, RoutedEventArgs e)
        {
            print_finalresults("resultsfinal", "html");
        }




        private async void print_finalresults(string template_name, string output_type)
        {



            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Připravuji finálové výsledky k tisku");
            await Task.Delay(300);
            controller.SetProgress(0);


            string html_main;
            string html_body;
            string html_body_withrightdata;


            Console.WriteLine("VM.Players_Finalresults.Count" + VM.Players_Finalresults.Count);

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);


            html_main = File.ReadAllText(directory + "/Print_templates/" + template_name + "_frame.html", Encoding.UTF8);
            html_main = html_main.Replace("@CONTESTNAME", VM.BIND_SQL_SOUTEZ_NAZEV + " - " + VM.BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@ORGANISATOR", VM.BIND_SQL_SOUTEZ_CLUB);
            html_main = html_main.Replace("@PLACE", VM.BIND_SQL_SOUTEZ_LOKACE);
            html_main = html_main.Replace("@DATE", VM.BIND_SQL_SOUTEZ_DATUM);
            html_main = html_main.Replace("@CONTESTNUMBER", VM.BIND_SQL_SOUTEZ_SMCRID);
            html_main = html_main.Replace("@CATEGORY", VM.BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@DIRECTOR", VM.BIND_SQL_SOUTEZ_DIRECTOR);
            html_main = html_main.Replace("@HEADJURY", VM.BIND_SQL_SOUTEZ_HEADJURY);
            html_main = html_main.Replace("@SUBJURY", VM.BIND_SQL_SOUTEZ_JURY1 + " | " + VM.BIND_SQL_SOUTEZ_JURY2 + " | " + VM.BIND_SQL_SOUTEZ_JURY3);
            html_main = html_main.Replace("@WEATHER", VM.BIND_SQL_SOUTEZ_POCASI);

            html_body = File.ReadAllText(directory + "/Print_templates/" + template_name + "_data.html", Encoding.UTF8);
            string html_body_complete = "";


            html_body_complete = $@"<table>
                <th>Pozice</th>
                <th>Soutěžící</th>
                <th class='visibility_{pf_stat.IsOn}'>Stát</th>
                <th class='visibility_{pf_id.IsOn}'>ID</th>
                <th class='visibility_{pf_natlic.IsOn}'>NAT lic.</th>
                <th class='visibility_{pf_failic.IsOn}'>FAI lic.</th>
                <th class='visibility_{pf_agecat.IsOn}'>AGECAT</th>
                <th class='visibility_{pf_gpen.IsOn}'>G.Pen</th>
                <th class='visibility_{p_fscore.IsOn}'>F.scóre</th>
                <th class='visibility_{pf_ztrata.IsOn}'>F.Ztráta</th>
                <th class='iam_{R1VISIBILITYFINAL.Visibility}'>F1</th>
                <th class='iam_{R2VISIBILITYFINAL.Visibility}'>F2</th>
                <th class='iam_{R3VISIBILITYFINAL.Visibility}'>F3</th>
                <th class='iam_{R4VISIBILITYFINAL.Visibility}'>F4</th>
                <th class='iam_{R5VISIBILITYFINAL.Visibility}'>F5</th>
                @BODY
          </table>";

            html_body_withrightdata = "";

            for (int i = 0; i < VM.Players_Finalresults.Count(); i++)
            {

                html_body = $@"<tr>
    <td>@POSITION</td>
    <td>@USERNAME</td>
    <td class='visibility_{pf_stat.IsOn}'><img class='vlajka' src='@FLAG' /></td>
    <td class='visibility_{pf_id.IsOn}'>@ID</td>
    <td class='visibility_{pf_natlic.IsOn}'>@NATLIC</td>
    <td class='visibility_{pf_failic.IsOn}'>@FAILIC</td>
    <td class='visibility_{pf_agecat.IsOn}'>@AGECAT</td>
    <td class='visibility_{pf_gpen.IsOn}'>@GPEN</td>
    <td class='visibility_{p_fscore.IsOn}'>@FINSCO</td>
    <td class='visibility_{pf_ztrata.IsOn}'>@FINLST</td>
    <td class='iam_{R1VISIBILITYFINAL.Visibility} skrtacka{VM.Players_Finalresults[i].RND1RES_SKRTACKA}'>@F1</td>
    <td class='iam_{R2VISIBILITYFINAL.Visibility} skrtacka{VM.Players_Finalresults[i].RND2RES_SKRTACKA}'>@F2</td>
    <td class='iam_{R3VISIBILITYFINAL.Visibility} skrtacka{VM.Players_Finalresults[i].RND3RES_SKRTACKA}'>@F3</td>
    <td class='iam_{R4VISIBILITYFINAL.Visibility} skrtacka{VM.Players_Finalresults[i].RND4RES_SKRTACKA}'>@F4</td>
    <td class='iam_{R5VISIBILITYFINAL.Visibility} skrtacka{VM.Players_Finalresults[i].RND5RES_SKRTACKA}'>@F5</td>
</tr>";


                controller.SetProgress(double.Parse(decimal.Divide(i, VM.Players_Finalresults.Count()).ToString()));
                Console.WriteLine(decimal.Divide(i, VM.Players_Finalresults.Count()));
                await Task.Delay(100);
                string tabulkaletu = "";




                html_body_withrightdata = html_body_withrightdata + html_body;

                html_body_withrightdata = html_body_withrightdata.Replace("@USERNAME", VM.Players_Finalresults[i].PLAYERDATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@POSITION", VM.Players_Finalresults[i].POSITION.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@ID", VM.Players_Finalresults[i].ID.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@NATLIC", VM.Players_Finalresults[i].NATLIC.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@FAILIC", VM.Players_Finalresults[i].FAILIC.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@AGECAT", VM.Players_Finalresults[i].AGECAT.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@GPEN", VM.Players_Finalresults[i].GPEN.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@F1", VM.Players_Finalresults[i].RND1RES_SCORE + "<br>" + VM.Players_Finalresults[i].RND1RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@F2", VM.Players_Finalresults[i].RND2RES_SCORE + "<br>" + VM.Players_Finalresults[i].RND2RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@F3", VM.Players_Finalresults[i].RND3RES_SCORE + "<br>" + VM.Players_Finalresults[i].RND3RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@F4", VM.Players_Finalresults[i].RND4RES_SCORE + "<br>" + VM.Players_Finalresults[i].RND4RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@F5", VM.Players_Finalresults[i].RND5RES_SCORE + "<br>" + VM.Players_Finalresults[i].RND5RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@FINSCO", VM.Players_Finalresults[i].PREPSCORE.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@FINLST", VM.Players_Finalresults[i].PREPSCOREDIFF.ToString());




                byte[] imageArray = System.IO.File.ReadAllBytes(VM.Players_Finalresults[i].FLAG);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                Console.WriteLine(base64ImageRepresentation);
                html_body_withrightdata = html_body_withrightdata.Replace("@FLAG", "data:image/png;base64," + base64ImageRepresentation);
            }
            html_body_complete = html_body_complete.Replace("@BODY", html_body_withrightdata);



            html_all = html_main.Replace("@BODY", html_body_complete);

       

            if (output_type == "html")
            {

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory + "/Print/" + template_name + ".html"))
                {
                    file.WriteLine(html_all);
                }
                System.Diagnostics.Process.Start(directory + "/Print/" + template_name + ".html");
            }
            await controller.CloseAsync();
            await Task.Delay(300);



        }


    }
}
