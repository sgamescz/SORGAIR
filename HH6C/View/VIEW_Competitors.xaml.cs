using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfApp6.Model;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WpfApp6.View
{
    /// <summary>
    /// Interakční logika pro SecondView.xaml
    /// </summary>
    public partial class Soutezici : UserControl
    {

        string html_all;
        bool is_recording = false;

        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;





    public Soutezici()

        {
            InitializeComponent();

            

        }


        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);



        private void Tile_Click(object sender, RoutedEventArgs e)
        {

        }


        private void competitorlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (competitorlist.SelectedIndex>= 0) 
            {
                userdetail_id.Count = VM.Players[competitorlist.SelectedIndex].ID.ToString();
                userdetail_competitorname.Title = VM.Players[competitorlist.SelectedIndex].LASTNAME.ToString() + " " + VM.Players[competitorlist.SelectedIndex].FIRSTNAME.ToString();
                userdetail_agecat.Title   = "Věková kategorie : " + VM.Players[competitorlist.SelectedIndex].AGECAT .ToString();
                userdetail_NATLIC.Title = VM.Players[competitorlist.SelectedIndex].NACLIC  .ToString();
                userdetail_FAILIC.Title = VM.Players[competitorlist.SelectedIndex].FAILIC  .ToString();
                userdetail_club.Title = "Klub : " + VM.Players[competitorlist.SelectedIndex].CLUB .ToString();
                userdetail_freq .Title = "Frekvence : " + VM.Players[competitorlist.SelectedIndex].FREQ.ToString();
                userdetail_ch1 .Title = VM.Players[competitorlist.SelectedIndex].CH1 .ToString();
                userdetail_ch2 .Title = VM.Players[competitorlist.SelectedIndex].CH2.ToString();
                userdetail_country.Title = VM.Players[competitorlist.SelectedIndex].COUNTRY.ToString();
                VM.BIND_FLAG = VM.Players[competitorlist.SelectedIndex].COUNTRY.ToString();
                VM.BIND_PAID = VM.Players[competitorlist.SelectedIndex].PAIDSTR  .ToString();
                ispaid.IsEnabled = true;
                edituser.IsEnabled = true;
                delete_competitor.IsEnabled = true;


            }
            else
            {
                edituser.IsEnabled = false;
                ispaid.IsEnabled = false ;
                delete_competitor.IsEnabled = false ;
            }

        }


        private void l_FUNCTION_clear_all_newuser_fields()
        {
            l_firstname.Text = "";
            L_lastname.Text = "";
            l_country.SelectedIndex = 58;
            l_chanel1.Value = null ;
            l_chanel2.Value = null;
            l_club.Text = "";
            l_failic.Text = "";
            l_naclic.Text = "";
            l_agecat.SelectedIndex = -1;
            l_freq.SelectedIndex = -1;
            l_registered.IsOn  = false;
            l_nextid.Count = VM.SQL_READSOUTEZDATA("SELECT seq+1 FROM SQLITE_SEQUENCE where name='users'", "");



        }

        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            VM.Players[3].FIRSTNAME = "AAXXX";
        }

        private void landingoptions_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ispaid_Click(object sender, RoutedEventArgs e)
        {
            if ( competitorlist.SelectedIndex >= 0) {

                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var directory = System.IO.Path.GetDirectoryName(path);



                string check = (sender as MahApps.Metro.Controls.ToggleSwitch).IsOn.ToString();
                VM.Players[competitorlist.SelectedIndex].PAIDSTR = check;
                VM.Players[competitorlist.SelectedIndex].PAID = directory+ "/flags/" + check + ".png";
                VM.FUNCTION_COMPETITOR_UPDATE("Paid", check, VM.Players[competitorlist.SelectedIndex].ID);
            }
        }

        private void SplitButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string L_flag = (sender as MahApps.Metro.Controls.SplitButton).SelectedIndex.ToString();
            
            if (Int32.Parse(L_flag) >= 0 )
            {
                Console.WriteLine(L_flag);
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var directory = System.IO.Path.GetDirectoryName(path);

                L_newuser_flag.Source = new BitmapImage(new Uri(directory+ "/flags/" + VM.MODEL_Contest_FLAGS[Int32.Parse(L_flag)].FILENAME + ".png"));
            }

        }

        private void L_savenewuser_Click(object sender, RoutedEventArgs e)
        {
            if (__SAVE_NEW_USER() == true)
            {

                l_FUNCTION_clear_all_newuser_fields();
                firstFlyout.IsOpen = false;
                competitorlist.SelectedIndex = competitorlist.Items.Count - 1;
                competitorlist.ScrollIntoView(competitorlist.Items[competitorlist.SelectedIndex]);
            }

        }

        private void Tile_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private async  void delete_competitor_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            if (competitorlist.SelectedIndex >= 0)
            {
                MessageDialogResult result = await currentWindow.ShowMessageAsync("Smazání soutěžícího", "Opravdu smazat soutěžícího s ID: " + VM.Players[competitorlist.SelectedIndex].ID + " : " + VM.Players[competitorlist.SelectedIndex].FIRSTNAME + " " + VM.Players[competitorlist.SelectedIndex].LASTNAME + "?", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Negative)
                {
                    Console.WriteLine("No");
                }
                else
                {
                    Console.WriteLine("yes");
                    VM.FUNCTION_USERS_DELETE_COMPETITOR(VM.Players[competitorlist.SelectedIndex].ID);
                    competitorlist.SelectedIndex = competitorlist.Items.Count - 1;
                    competitorlist.ScrollIntoView(competitorlist.Items[competitorlist.SelectedIndex]);

                }
            }
            else
            {
                await currentWindow.ShowMessageAsync("Nikdo není vybrán", "Vyber prosím, koho chceš smazat");
            }
    
        }

        private void addnewuser_Click(object sender, RoutedEventArgs e)
        {
            //VM.save_new_competitor("firstname", "lastname", "FIN");
            firstFlyout.IsOpen = true;
            l_nextid.Count  = VM.SQL_READSOUTEZDATA("SELECT seq+1 FROM SQLITE_SEQUENCE where name='users'", "");

        }

        private void check_if_exist_name_sound(int idsouteziciho)
        {

            if (File.Exists("Audio\\NAMES\\" + idsouteziciho + ".wav"))
            {
                play_name_sound.IsEnabled = true;
                delete_name_sound.IsEnabled =true;
            }
            else
            {
                play_name_sound.IsEnabled = false;
                delete_name_sound.IsEnabled = false;

            }


        }

        private bool  __SAVE_NEW_USER()
        {
            bool results = true ;
            string _failictmp = "";


            if (VM.BIND_SQL_SOUTEZ_REQUIREFAILICENCE == false & l_failic.Text == "")
            {
                _failictmp = "---";
            }
            else
            {
                _failictmp = l_failic.Text;
            }



            if (l_firstname.Text == "" || L_lastname.Text == "" || l_agecat.SelectedIndex == -1 || l_club.Text == "" || l_country.SelectedIndex == -1 || _failictmp == "" || l_freq.SelectedIndex == -1 || l_naclic.Text == "")
            {
                results = false;
                var currentWindow = this.TryFindParent<MetroWindow>();
                currentWindow.ShowMessageAsync("Nelze uložit", "Nejsou vyplněné všechny údaje soutěžícího. Nelze uložit");

            }
            else
            {
                if (l_freq.SelectedIndex == 0) 
                {
                    l_chanel1.Value = 0;
                    l_chanel2.Value = 0;
                }

                Console.WriteLine("l_freq.SelectedIndex" + l_freq.SelectedIndex);
                Console.WriteLine("l_chanel1.Value" + l_chanel1.Value);
                Console.WriteLine("l_chanel2.Value" + l_chanel2.Value);
                if (l_freq.SelectedIndex != 0 && (l_chanel1.Value is null || l_chanel2.Value is null))
                {
                    results = false;
                    var currentWindow = this.TryFindParent<MetroWindow>();
                    currentWindow.ShowMessageAsync("Nelze uložit", "Nejsou vyplněné kanály u  zvolené frekvence");
                }
                else
                {
                    VM.FUNCTION_USERS_CREATE_NEW(l_firstname.Text, L_lastname.Text, VM.MODEL_Contest_FLAGS[l_country.SelectedIndex].FILENAME, VM.MODEL_Contest_AGECATEGORIES[l_agecat.SelectedIndex].ID, VM.MODEL_Contest_FREQUENCIES[l_freq.SelectedIndex].ID, Convert.ToInt32(l_chanel1.Value), Convert.ToInt32(l_chanel2.Value), _failictmp, l_naclic.Text, l_club.Text, Convert.ToBoolean(l_registered.IsOn), 0, 1);
                }



            }
            return results;
        }

        private bool __EDIT_NEW_USER()
        {
            bool results = true;
            string _failictmp = "";

            if (VM.BIND_SQL_SOUTEZ_REQUIREFAILICENCE == false)
            {
                if (l_failic_edit.Text == "")
                {
                    _failictmp = "---";
                }
                else
                {
                    _failictmp = l_failic_edit.Text;
                }

            }
            else
            {
                _failictmp = l_failic_edit.Text;
            }

            if (l_firstname_edit.Text == "" || L_lastname_edit.Text == "" || l_agecat_edit.SelectedIndex == -1 || l_club_edit.Text == "" || l_country_edit.SelectedIndex == -1 || _failictmp == "" || l_freq_edit.SelectedIndex == -1 || l_naclic_edit.Text == "")
            {
                results = false;
                var currentWindow = this.TryFindParent<MetroWindow>();
                currentWindow.ShowMessageAsync("Nelze uložit", "Nejsou vyplněné všechny údaje soutěžícího. Nelze uložit");

            }
            else
            {
                if (l_freq_edit.SelectedIndex == 0)
                {
                    l_chanel1_edit.Value = 0;
                    l_chanel2_edit.Value = 0;
                }



                if (l_freq_edit.SelectedIndex != 0)
                {
                    if (l_chanel1_edit.Value is null || l_chanel2_edit.Value is null)
                    {
                        results = false;
                        var currentWindow = this.TryFindParent<MetroWindow>();
                        currentWindow.ShowMessageAsync("Nelze uložit", "EDIT Nejsou vyplněné kanály u  zvolené frekvence");

                    }
                }

                if (results != false)
                {
                VM.FUNCTION_USERS_CREATE_EDIT(int.Parse(l_nextid_edit.Count), l_firstname_edit.Text, L_lastname_edit.Text, VM.MODEL_Contest_FLAGS[l_country_edit.SelectedIndex].FILENAME, VM.MODEL_Contest_AGECATEGORIES[l_agecat_edit.SelectedIndex].ID, VM.MODEL_Contest_FREQUENCIES[l_freq_edit.SelectedIndex].ID, Convert.ToInt32(l_chanel1_edit.Value), Convert.ToInt32(l_chanel2_edit.Value), _failictmp, l_naclic_edit.Text, l_club_edit.Text, Convert.ToBoolean(l_registered_edit.IsOn), 1);
                }



            }
            return results;
        }


        private void L_savenewuserandagain_Click(object sender, RoutedEventArgs e)
        {

            if (__SAVE_NEW_USER() == true)
            {
                l_FUNCTION_clear_all_newuser_fields();
                firstFlyout.IsOpen = true;
                competitorlist.SelectedIndex = competitorlist.Items.Count - 1;
                competitorlist.ScrollIntoView(competitorlist.Items[competitorlist.SelectedIndex]);
            }
        }

        private void L_savenewuser_clearall_Click(object sender, RoutedEventArgs e)
        {
            l_FUNCTION_clear_all_newuser_fields();
        }

        private void L_close_Click(object sender, RoutedEventArgs e)
        {
            l_FUNCTION_clear_all_newuser_fields();
            firstFlyout.IsOpen = false;
        }

        private void edituser_Click(object sender, RoutedEventArgs e)
        {

            l_nextid_edit.Count = VM.Players[competitorlist.SelectedIndex].ID.ToString();
            L_lastname_edit.Text = VM.Players[competitorlist.SelectedIndex].LASTNAME.ToString();
            l_firstname_edit.Text = VM.Players[competitorlist.SelectedIndex].FIRSTNAME.ToString();
            l_naclic_edit.Text = VM.Players[competitorlist.SelectedIndex].NACLIC.ToString();
            l_failic_edit.Text = VM.Players[competitorlist.SelectedIndex].FAILIC.ToString();
            l_club_edit.Text = VM.Players[competitorlist.SelectedIndex].CLUB.ToString();
            l_chanel1_edit.Value = VM.Players[competitorlist.SelectedIndex].CH1;
            l_chanel2_edit.Value = VM.Players[competitorlist.SelectedIndex].CH2;
            l_freq_edit.SelectedIndex = VM.Players[competitorlist.SelectedIndex].FREQID;
            foreach (var stat in VM.MODEL_Contest_FLAGS)
            {
                if (stat.FILENAME == VM.Players[competitorlist.SelectedIndex].COUNTRY.ToString()) { l_country_edit.SelectedIndex = stat.ID; }
            }

            l_agecat_edit.SelectedIndex = VM.Players[competitorlist.SelectedIndex].AGECATID;
            l_registered_edit.IsOn = bool.Parse(VM.Players[competitorlist.SelectedIndex].PAIDSTR);

            firstFlyout_edit.IsOpen = true;

            //VM.BIND_FLAG = VM.Players[competitorlist.SelectedIndex].COUNTRY.ToString();
            //VM.BIND_PAID = VM.Players[competitorlist.SelectedIndex].PAIDSTR.ToString();
            //ispaid.IsEnabled = true;



        }

        private void L_back_edit_Click(object sender, RoutedEventArgs e)
        {
            firstFlyout_edit.IsOpen = false;
        }

        private void L_savenewuser_edit_Click(object sender, RoutedEventArgs e)
        {

            int _selectedusr = competitorlist.SelectedIndex;
            if (__EDIT_NEW_USER() == true)
            {
                firstFlyout_edit.IsOpen = false;
                competitorlist.SelectedIndex = _selectedusr;
                competitorlist.ScrollIntoView(competitorlist.Items[competitorlist.SelectedIndex]);
            }

        }

        private void l_country_edit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string L_flag = (sender as MahApps.Metro.Controls.SplitButton).SelectedIndex.ToString();

            if (Int32.Parse(L_flag) >= 0)
            {
                Console.WriteLine(L_flag);
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var directory = System.IO.Path.GetDirectoryName(path);

                L_newuser_flag_edit.Source = new BitmapImage(new Uri(directory + "/flags/" + VM.MODEL_Contest_FLAGS[Int32.Parse(L_flag)].FILENAME + ".png"));
            }

        }

        private void L_savenewuser_edxxit_Click(object sender, RoutedEventArgs e)
        {
        }


        private void printusercards_Click(object sender, RoutedEventArgs e)
        {
            firstFlyout_print_scorecards.IsOpen = true;

        }


        private async void print_scorecards(string template_name, string output_type, int  cutcounter)
        {



            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Generuji karty soutěžících k tisku");
            await Task.Delay(300);
            controller.SetProgress(0);


            string html_main;
            string html_body;
            string html_body_withrightdata;
            Console.WriteLine("VM.Players.Count" + VM.Players.Count);

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            int pocetnastranku = 0;
            if (cutcounter == 1) { pocetnastranku = int.Parse(pocetnastranku1_updown.Value.ToString());}
            if (cutcounter == 2) { pocetnastranku = int.Parse(pocetnastranku2_updown.Value.ToString()); }
            int tmp_pocetnastranku = 0;

            html_main = File.ReadAllText(directory + "/Print_templates/"+ template_name + "_frame.html", Encoding.UTF8);

            html_body = File.ReadAllText(directory + "/Print_templates/" + template_name + "_data.html", Encoding.UTF8);
            string html_body_complete = "";
            for (int i = 0; i < VM.Players.Count(); i++)
            {

                controller.SetProgress(double.Parse(decimal.Divide(i, VM.Players.Count()).ToString()));
                Console.WriteLine(decimal.Divide(i, VM.Players.Count()));
                await Task.Delay(100);
                string tabulkaletu = "";

                for (int x = 1; x < VM.BIND_SQL_SOUTEZ_ROUNDS + 1; x++)
                {
                    string tmp_grp_stp = VM.SQL_READSOUTEZDATA("select grp || '/' || stp from matrix where user = " + VM.Players[i].ID + " and rnd = " + x, "");
                    tabulkaletu = tabulkaletu + $@"<tr>
<td class='gray'>{x}</td>
<td class='gray'>{tmp_grp_stp}</td>
<td></td>
<td></td>
<td></td>
<td></td>
<td></td>
<td></td>
<td></td>
</tr>";
                }



                for (int x = 1; x < VM.BIND_SQL_SOUTEZ_ROUNDSFINALE + 1; x++)
                {
                    string tmp_grp_stp = VM.SQL_READSOUTEZDATA("select grp || '/' || stp from matrix where user = " + VM.Players[i].ID + " and rnd = " + x + 100, "");
                    tabulkaletu = tabulkaletu + $@"<tr>
<td class='gray'>F{x}</td>
<td class='gray'>{tmp_grp_stp}</td>
<td></td>
<td></td>
<td></td>
<td></td>
<td></td>
<td></td>
<td></td>
</tr>";
                }




                tmp_pocetnastranku += 1;

                html_body_withrightdata = html_body;

                html_body_withrightdata = html_body_withrightdata.Replace("@ID", VM.Players[i].ID.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@USERNAME", VM.Players[i].LASTNAME + " " + VM.Players[i].FIRSTNAME);
                html_body_withrightdata = html_body_withrightdata.Replace("@CONTESTNAME", VM.BIND_SQL_SOUTEZ_NAZEV + " - " + VM.BIND_SQL_SOUTEZ_KATEGORIE);
                html_body_withrightdata = html_body_withrightdata.Replace("@COUNTRY", VM.Players[i].COUNTRY);
                html_body_withrightdata = html_body_withrightdata.Replace("@NATLIC", VM.Players[i].NACLIC);
                html_body_withrightdata = html_body_withrightdata.Replace("@NACLIC", VM.Players[i].NACLIC);
                html_body_withrightdata = html_body_withrightdata.Replace("@FAILIC", VM.Players[i].FAILIC);
                html_body_withrightdata = html_body_withrightdata.Replace("@AGECAT", VM.Players[i].AGECAT);
                html_body_withrightdata = html_body_withrightdata.Replace("@CLUB", VM.Players[i].CLUB);
                html_body_withrightdata = html_body_withrightdata.Replace("@TEAM", "tym");
                html_body_withrightdata = html_body_withrightdata.Replace("@FREQUENCY", VM.Players[i].FREQ);




                byte[] imageArray = System.IO.File.ReadAllBytes(directory + "/flags/" + VM.Players[i].COUNTRY + ".png");
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                Console.WriteLine(base64ImageRepresentation);
                html_body_withrightdata = html_body_withrightdata.Replace("@FLAG", "data:image/png;base64," + base64ImageRepresentation);

                html_body_withrightdata = html_body_withrightdata.Replace("@MATRIX", tabulkaletu);


                html_body_complete = html_body_complete + html_body_withrightdata;

                if (tmp_pocetnastranku == pocetnastranku)
                {
                    tmp_pocetnastranku = 0;
                    html_body_complete = html_body_complete + "<div class='pagebreak'>---- ✂ ---- ✂ ---- cut here ---- ✂ ---- ✂ ----</div>";
                }

            }


            html_all = html_main.Replace("@BODY", html_body_complete);

            if (output_type == "pdf")
            {

                SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
                SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html_all);
                doc.Save(directory + "/Print/" + template_name + ".pdf");
                doc.Close();

                System.Diagnostics.Process.Start(directory + "/Print/" + template_name + ".pdf");
            }


            if (output_type=="html") {

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory + "/Print/" + template_name + ".html"))
                {
                    file.WriteLine(html_all);
                }
                System.Diagnostics.Process.Start(directory + "/Print/" + template_name + ".html");
            }
            await controller.CloseAsync();
            await Task.Delay(300);



        }



        private async void print_scorecards_type2(string template_name, string output_type, string typrazeni)
        {



            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Generuji karty soutěžících k tisku");
            await Task.Delay(300);
            controller.SetProgress(0);


            string html_main;
            string html_body;
            string html_body_withrightdata;
            Console.WriteLine("VM.Players.Count" + VM.Players.Count);

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            int pocetnastranku = int.Parse(pocetnastranku3_updown.Value.ToString());
            int tmp_pocetnastranku = 0;

            html_main = File.ReadAllText(directory + "/Print_templates/" + template_name + "_frame.html", Encoding.UTF8);

            html_body = File.ReadAllText(directory + "/Print_templates/" + template_name + "_data.html", Encoding.UTF8);
            string html_body_complete = "";

            if (typrazeni == "competitors")
            {

                for (int i = 0; i < VM.Players.Count(); i++)
                {

                    controller.SetProgress(double.Parse(decimal.Divide(i, VM.Players.Count()).ToString()));
                    Console.WriteLine(decimal.Divide(i, VM.Players.Count()));
                    await Task.Delay(100);
                    string tabulkaletu = "";

                    for (int x = 1; x < VM.BIND_SQL_SOUTEZ_ROUNDS + 1; x++)
                    {
                        string tmp_grp_stp = VM.SQL_READSOUTEZDATA("select grp || '/' || stp from matrix where user = " + VM.Players[x].ID + " and rnd = " + x, "");

                        tmp_pocetnastranku += 1;

                        html_body_withrightdata = html_body;

                        html_body_withrightdata = html_body_withrightdata.Replace("@ID", VM.Players[x].ID.ToString());
                        html_body_withrightdata = html_body_withrightdata.Replace("@USERNAME", VM.Players[x].LASTNAME + " " + VM.Players[x].FIRSTNAME);
                        html_body_withrightdata = html_body_withrightdata.Replace("@CONTESTNAME", VM.BIND_SQL_SOUTEZ_NAZEV + " - " + VM.BIND_SQL_SOUTEZ_KATEGORIE);
                        html_body_withrightdata = html_body_withrightdata.Replace("@COUNTRY", VM.Players[x].COUNTRY);
                        html_body_withrightdata = html_body_withrightdata.Replace("@RND", x.ToString());
                        html_body_withrightdata = html_body_withrightdata.Replace("@GRP", tmp_grp_stp);
                        html_body_withrightdata = html_body_withrightdata.Replace("@NATLIC", VM.Players[x].NACLIC);
                        html_body_withrightdata = html_body_withrightdata.Replace("@NACLIC", VM.Players[x].NACLIC);
                        html_body_withrightdata = html_body_withrightdata.Replace("@FAILIC", VM.Players[x].FAILIC);
                        html_body_withrightdata = html_body_withrightdata.Replace("@AGECAT", VM.Players[x].AGECAT);
                        html_body_withrightdata = html_body_withrightdata.Replace("@CLUB", VM.Players[x].CLUB);
                        html_body_withrightdata = html_body_withrightdata.Replace("@TEAM", "tym");
                        html_body_withrightdata = html_body_withrightdata.Replace("@FREQUENCY", VM.Players[x].FREQ);




                        byte[] imageArray = System.IO.File.ReadAllBytes(directory + "/flags/" + VM.Players[x].COUNTRY + ".png");
                        string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                        Console.WriteLine(base64ImageRepresentation);
                        html_body_withrightdata = html_body_withrightdata.Replace("@FLAG", "data:image/png;base64," + base64ImageRepresentation);

                        html_body_withrightdata = html_body_withrightdata.Replace("@MATRIX", tabulkaletu);


                        html_body_complete = html_body_complete + html_body_withrightdata;

                        if (tmp_pocetnastranku == pocetnastranku)
                        {
                            tmp_pocetnastranku = 0;
                            html_body_complete = html_body_complete + "<div class='pagebreak'>---- ✂ ---- ✂ ---- cut here ---- ✂ ---- ✂ ----</div>";
                        }


                    }



                }

            }



            if (typrazeni == "rounds")
            {

                for (int i = 1; i < VM.BIND_SQL_SOUTEZ_ROUNDS + 1; i++)
                {

                    controller.SetProgress(double.Parse(decimal.Divide(i, VM.Players.Count()).ToString()));
                    Console.WriteLine(decimal.Divide(i, VM.Players.Count()));
                    await Task.Delay(100);
                    string tabulkaletu = "";

                    for (int x = 0; x < VM.Players.Count() ; x++)
                    {
                        string tmp_grp_stp = VM.SQL_READSOUTEZDATA("select grp || '/' || stp from matrix where user = " + VM.Players[x].ID + " and rnd = " + i, "");

                        tmp_pocetnastranku += 1;

                        html_body_withrightdata = html_body;

                        html_body_withrightdata = html_body_withrightdata.Replace("@ID", VM.Players[x].ID.ToString());
                        html_body_withrightdata = html_body_withrightdata.Replace("@USERNAME", VM.Players[x].LASTNAME + " " + VM.Players[x].FIRSTNAME);
                        html_body_withrightdata = html_body_withrightdata.Replace("@CONTESTNAME", VM.BIND_SQL_SOUTEZ_NAZEV + " - " + VM.BIND_SQL_SOUTEZ_KATEGORIE);
                        html_body_withrightdata = html_body_withrightdata.Replace("@COUNTRY", VM.Players[x].COUNTRY);
                        html_body_withrightdata = html_body_withrightdata.Replace("@RND", i.ToString());
                        html_body_withrightdata = html_body_withrightdata.Replace("@GRP", tmp_grp_stp);
                        html_body_withrightdata = html_body_withrightdata.Replace("@NATLIC", VM.Players[x].NACLIC);
                        html_body_withrightdata = html_body_withrightdata.Replace("@NACLIC", VM.Players[x].NACLIC);
                        html_body_withrightdata = html_body_withrightdata.Replace("@FAILIC", VM.Players[x].FAILIC);
                        html_body_withrightdata = html_body_withrightdata.Replace("@AGECAT", VM.Players[x].AGECAT);
                        html_body_withrightdata = html_body_withrightdata.Replace("@CLUB", VM.Players[x].CLUB);
                        html_body_withrightdata = html_body_withrightdata.Replace("@TEAM", "tym");
                        html_body_withrightdata = html_body_withrightdata.Replace("@FREQUENCY", VM.Players[x].FREQ);




                        byte[] imageArray = System.IO.File.ReadAllBytes(directory + "/flags/" + VM.Players[x].COUNTRY + ".png");
                        string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                        Console.WriteLine(base64ImageRepresentation);
                        html_body_withrightdata = html_body_withrightdata.Replace("@FLAG", "data:image/png;base64," + base64ImageRepresentation);

                        html_body_withrightdata = html_body_withrightdata.Replace("@MATRIX", tabulkaletu);


                        html_body_complete = html_body_complete + html_body_withrightdata;

                        if (tmp_pocetnastranku == pocetnastranku)
                        {
                            tmp_pocetnastranku = 0;
                            html_body_complete = html_body_complete + "<div class='pagebreak'>---- ✂ ---- ✂ ---- cut here ---- ✂ ---- ✂ ----</div>";
                        }


                    }

                 if (cut_levels.IsOn)
                    {
                        tmp_pocetnastranku = 0;
                        html_body_complete = html_body_complete + "<div class='pagebreak'>---- ✂ ---- ✂ ---- cut here ---- ✂ ---- ✂ ----</div>";
                    }

                }

            }



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



        private async void print_userslist(string output_type)
        {



            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Generuji seznam soutěžících k tisku");
            await Task.Delay(300);
            controller.SetProgress(0);

            VM.print_userslist("frame_small_info", "data_userlist", "print_userlist", "Seznam soutěžících", "html");

            await controller.CloseAsync();
            await Task.Delay(300);



        }

        private void print_to_html_Click(object sender, RoutedEventArgs e)
        {
            print_scorecards("scorecard_long","html",1);

        }

      

        private void print_to_html_userslist_Click(object sender, RoutedEventArgs e)
        {
            print_userslist("html");
        }

     

      

        private void print_to_html_cut_competitors_Click(object sender, RoutedEventArgs e)
        {
            print_scorecards_type2("scorecard_cut_competitors", "html", "competitors");

        }

        private void print_to_html_cut_timekeepers_Click(object sender, RoutedEventArgs e)
        {
            print_scorecards("scorecard_cut_timekeepers", "html",2);

        }

        private void rec_audio_Click(object sender, RoutedEventArgs e)
        {
            if (is_recording == false)
            {
                is_recording = true;
                mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
                mciSendString("record recsound", "", 0, 0);
                rec_audio_label.Content = "Zastavit nahrávání";
            }
            else
            {
                is_recording = false;
                mciSendString("save recsound Audio\\NAMES\\" + VM.SQL_READSOUTEZDATA("SELECT seq+1 FROM SQLITE_SEQUENCE where name='users'", "") + ".wav", "", 0, 0);
                mciSendString("close recsound ", "", 0, 0);
                rec_audio_label.Content = "Nahrát jméno soutěžcího";
                check_if_exist_name_sound(int.Parse(VM.SQL_READSOUTEZDATA("SELECT seq+1 FROM SQLITE_SEQUENCE where name='users'", "")));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void play_name_sound_Click(object sender, RoutedEventArgs e)
        {

            mciSendString("play Audio\\NAMES\\" + VM.SQL_READSOUTEZDATA("SELECT seq+1 FROM SQLITE_SEQUENCE where name='users'", "") + ".wav  wait", null, 0, 0);
            mciSendString("close MyFile", null, 0, 0);




        }

        private void delete_name_sound_Click(object sender, RoutedEventArgs e)
        {
            File.Delete("Audio\\NAMES\\" + VM.SQL_READSOUTEZDATA("SELECT seq + 1 FROM SQLITE_SEQUENCE where name = 'users'", "") + ".wav");
            check_if_exist_name_sound(int.Parse(VM.SQL_READSOUTEZDATA("SELECT seq+1 FROM SQLITE_SEQUENCE where name='users'", "")));

        }

        private void rec_audio2_Click(object sender, RoutedEventArgs e)
        {
            if (is_recording == false)
            {
                is_recording = true;
                mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
                mciSendString("record recsound", "", 0, 0);
                rec_audio_label.Content = "Zastavit nahrávání";
            }
            else
            {
                is_recording = false;
                mciSendString("save recsound Audio\\NAMES\\" + VM.SQL_READSOUTEZDATA("SELECT seq+1 FROM SQLITE_SEQUENCE where name='users'", "") + ".wav", "", 0, 0);
                mciSendString("close recsound ", "", 0, 0);
                rec_audio_label.Content = "Nahrát jméno soutěžcího";
                check_if_exist_name_sound(int.Parse(VM.SQL_READSOUTEZDATA("SELECT seq+1 FROM SQLITE_SEQUENCE where name='users'", "")));
            }
        }

        private void play_name_sound2_Click(object sender, RoutedEventArgs e)
        {
            mciSendString("play Audio\\NAMES\\" + VM.SQL_READSOUTEZDATA("SELECT seq+1 FROM SQLITE_SEQUENCE where name='users'", "") + ".wav  wait", null, 0, 0);
            mciSendString("close MyFile", null, 0, 0);
        }

        private void delete_name_sound2_Click(object sender, RoutedEventArgs e)
        {
            File.Delete("Audio\\NAMES\\" + VM.SQL_READSOUTEZDATA("SELECT seq + 1 FROM SQLITE_SEQUENCE where name = 'users'", "") + ".wav");
            check_if_exist_name_sound(int.Parse(VM.SQL_READSOUTEZDATA("SELECT seq+1 FROM SQLITE_SEQUENCE where name='users'", "")));
        }

        private void print_to_html_cut_competitors_round_Click(object sender, RoutedEventArgs e)
        {
            print_scorecards_type2("scorecard_cut_competitors", "html", "rounds");

        }
    }


}
