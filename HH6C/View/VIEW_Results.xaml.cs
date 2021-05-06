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




            VM.FUNCTION_RESULTS_LOADBASERESULTS("users");
        }
        private void results_users_Click(object sender, RoutedEventArgs e)
        {
            VM._ZOBRAZIT_ZAKLADNI_VYSLEDKY_S_SKRTACKAMA = false;
            VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'False'");

            udelej_zobrazeni_vysledku();
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

        private void print_baseresults_btn_Click(object sender, RoutedEventArgs e)
        {
            print_baseresults("resultsbase", "html");
        }

        private void print_baseresultspdf_btn_Click(object sender, RoutedEventArgs e)
        {
            print_baseresults("resultsbase", "pdf");
        }




        private async void print_baseresults(string template_name, string output_type)
        {



            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Připravuji výsledky k tisku");
            await Task.Delay(300);
            controller.SetProgress(0);


            string html_main;
            string html_body;
            string html_body_withrightdata;


            Console.WriteLine("VM.Players.Count" + VM.Players.Count);

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);


            html_main = File.ReadAllText(directory + "/Print_templates/" + template_name + "_frame.html", Encoding.UTF8);
            html_main = html_main.Replace("@CONTESTNAME", VM.BIND_SQL_SOUTEZ_NAZEV + " - " + VM.BIND_SQL_SOUTEZ_KATEGORIE);

            html_body = File.ReadAllText(directory + "/Print_templates/" + template_name + "_data.html", Encoding.UTF8);
            string html_body_complete = "";


            html_body_complete = $@"<table>
                <th>Pozice</th>
                <th>Soutěžící</th>
                <th class='visibility_{p_stat.IsOn}'>Stát</th>
                <th class='visibility_{p_id.IsOn}'>ID</th>
                <th>Celkové scóre</th>
                <th class='visibility_{p_gpen.IsOn}'>G.Pen</th>
                <th class='visibility_{p_ztrata.IsOn}'>Ztráta</th>
                <th class='iam_{R1VISIBILITY.Visibility}'>Kolo1</th>
                <th class='iam_{R2VISIBILITY.Visibility}'>Kolo 2</th>
                <th class='iam_{R3VISIBILITY.Visibility}'>Kolo 3</th>
                <th class='iam_{R4VISIBILITY.Visibility}'>Kolo 4</th>
                <th class='iam_{R5VISIBILITY.Visibility}'>Kolo 5</th>
                <th class='iam_{R6VISIBILITY.Visibility}'>Kolo 6</th>
                <th class='iam_{R7VISIBILITY.Visibility}'>Kolo 7</th>
                <th class='iam_{R8VISIBILITY.Visibility}'>Kolo 8</th>
                <th class='iam_{R9VISIBILITY.Visibility}'>Kolo 9</th>
                <th class='iam_{R10VISIBILITY.Visibility}'>Kolo 10</th>
                @BODY
          </table>";

            html_body_withrightdata = "";

            for (int i = 0; i < VM.Players_Baseresults.Count(); i++)
            {

                html_body = $@"<tr>
    <td>@POSITION</td>
    <td>@USERNAME</td>
    <td class='visibility_{p_stat.IsOn}'><img class='vlajka' src='@FLAG' /></td>
    <td class='visibility_{p_id.IsOn}'>@ID</td>
    <td>@SCORE</td>
    <td class='visibility_{p_gpen.IsOn}'>@GPEN</td>
    <td class='visibility_{p_ztrata.IsOn}'>@LOST</td>
    <td class='iam_{R1VISIBILITY.Visibility} skrtacka{VM.Players_Baseresults[i].RND1RES_SKRTACKA}'>@R1X</td>
    <td class='iam_{R2VISIBILITY.Visibility} skrtacka{VM.Players_Baseresults[i].RND2RES_SKRTACKA}'>@R2</td>
    <td class='iam_{R3VISIBILITY.Visibility} skrtacka{VM.Players_Baseresults[i].RND3RES_SKRTACKA}'>@R3</td>
    <td class='iam_{R4VISIBILITY.Visibility} skrtacka{VM.Players_Baseresults[i].RND4RES_SKRTACKA}'>@R4</td>
    <td class='iam_{R5VISIBILITY.Visibility} skrtacka{VM.Players_Baseresults[i].RND5RES_SKRTACKA}'>@R5</td>
    <td class='iam_{R6VISIBILITY.Visibility} skrtacka{VM.Players_Baseresults[i].RND6RES_SKRTACKA}'>@R6</td>
    <td class='iam_{R7VISIBILITY.Visibility} skrtacka{VM.Players_Baseresults[i].RND7RES_SKRTACKA}'>@R7</td>
    <td class='iam_{R8VISIBILITY.Visibility} skrtacka{VM.Players_Baseresults[i].RND8RES_SKRTACKA}'>@R8</td>
    <td class='iam_{R9VISIBILITY.Visibility} skrtacka{VM.Players_Baseresults[i].RND9RES_SKRTACKA}'>@R9</td>
    <td class='iam_{R10VISIBILITY.Visibility} skrtacka{VM.Players_Baseresults[i].RND10RES_SKRTACKA}'>@R10</td>
</tr>";


                controller.SetProgress(double.Parse(decimal.Divide(i, VM.Players.Count()).ToString()));
                Console.WriteLine(decimal.Divide(i, VM.Players.Count()));
                await Task.Delay(100);
                string tabulkaletu = "";

            


                    html_body_withrightdata = html_body_withrightdata + html_body;

                    html_body_withrightdata = html_body_withrightdata.Replace("@USERNAME", VM.Players_Baseresults[i].PLAYERDATA);
                    html_body_withrightdata = html_body_withrightdata.Replace("@POSITION", VM.Players_Baseresults[i].POSITION.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@ID", VM.Players_Baseresults[i].ID.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@SCORE", VM.Players_Baseresults[i].PREPSCORE.ToString() );
                html_body_withrightdata = html_body_withrightdata.Replace("@GPEN", VM.Players_Baseresults[i].GPEN.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@LOST", VM.Players_Baseresults[i].PREPSCOREDIFF.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@R1X", VM.Players_Baseresults[i].RND1RES_SCORE+ "<br>" + VM.Players_Baseresults[i].RND1RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R2", VM.Players_Baseresults[i].RND2RES_SCORE + "<br>" + VM.Players_Baseresults[i].RND2RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R3", VM.Players_Baseresults[i].RND3RES_SCORE + "<br>" + VM.Players_Baseresults[i].RND3RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R4", VM.Players_Baseresults[i].RND4RES_SCORE + "<br>" + VM.Players_Baseresults[i].RND4RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R5", VM.Players_Baseresults[i].RND5RES_SCORE + "<br>" + VM.Players_Baseresults[i].RND5RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R6", VM.Players_Baseresults[i].RND6RES_SCORE + "<br>" + VM.Players_Baseresults[i].RND6RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R7", VM.Players_Baseresults[i].RND7RES_SCORE + "<br>" + VM.Players_Baseresults[i].RND7RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R8", VM.Players_Baseresults[i].RND8RES_SCORE + "<br>" + VM.Players_Baseresults[i].RND8RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R9", VM.Players_Baseresults[i].RND9RES_SCORE + "<br>" + VM.Players_Baseresults[i].RND9RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R10", VM.Players_Baseresults[i].RND10RES_SCORE + "<br>" + VM.Players_Baseresults[i].RND10RES_DATA);




                byte[] imageArray = System.IO.File.ReadAllBytes(VM.Players_Baseresults[i].FLAG);
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                    Console.WriteLine(base64ImageRepresentation);
                    html_body_withrightdata = html_body_withrightdata.Replace("@FLAG", "data:image/png;base64," + base64ImageRepresentation);
            }
            html_body_complete = html_body_complete.Replace("@BODY", html_body_withrightdata);



            html_all = html_main.Replace("@BODY", html_body_complete);

            if (output_type == "pdf")
            {

                SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
                SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html_all);
                doc.Save(directory + "/Print/" + template_name + ".pdf");
                doc.Close();

                System.Diagnostics.Process.Start(directory + "/Print/" + template_name + ".pdf");
            }


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

            udelej_zobrazeni_vysledku();

        }
    }
}
