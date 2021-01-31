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
using MahApps.Metro.Controls;
using System.Threading;
using System.Net;
using System;
using System.Net;
using System.IO;
using System.Net.Cache;
using System.Globalization;

namespace WpfApp6.View
{
    /// <summary>
    /// Interakční logika pro FirstView.xaml
    /// </summary>
    public partial class Uvod : UserControl
    {

        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;
//        ViewModel XX = new ViewModel(DialogCoordinator.Instance);

        public Uvod()
        {

            InitializeComponent();
            //DataContext = VM ;
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


            Console.WriteLine(VM.MODEL_CONTESTS_FILES[competitionlist.SelectedIndex].FILENAME);
            VM.SQL_OPENCONNECTION(VM.MODEL_CONTESTS_FILES[competitionlist.SelectedIndex].FILENAME);

           

            VM.FUNCTION_LOADCONTEST();
            VM.FUNCTION_LOAD_RULES();
            VM.FUNCTION_USERS_LOAD_ALLCOMPETITORS();
            VM.FUNCTION_TEAM_LOAD_TEAMS();
            VM.FUNCTION_ROUNDS_LOAD_ROUNDS();
            VM.FUNCTION_LOAD_DEFAULT_ROUNDSANDGROUPS();
            //VM.BIND_VYBRANEKOLOMENU = "Vybrané kolo: 1/4";
            VM.clock_create ();
            VM.BINDING_selectedmenuindex = 1;




        }



        public void thread2()
        {

            string remoteUrl = "http://sorgair.com/api/version.php";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            HttpWebRequest.DefaultCachePolicy = policy;

            httpRequest.CachePolicy = policy;
            WebResponse response = httpRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();
            Console.WriteLine(result);


            this.Invoke(() => VM.BIND_VERZE_SORGU_LAST = result);

        }
        public void download_news(object sender, RoutedEventArgs e)
        {

            Thread test = new Thread(new ThreadStart(thread2));
            test.Start();
            //VM.FUNCTION_LOAD_MATRIX_FILES();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_LOAD_CONTESTS_FILES();

        }



        private async void nastaveni_soutez_Click(object sender, RoutedEventArgs e)
        {

            string content = (sender as Tile).Tag.ToString();
            string[] TAGY = content.Split('|');
            Console.WriteLine(TAGY.Length);
            int a = TAGY.Length / 3;

            for (int i = 0; i < a; i++)
            {
                string vyplnenyinput = "";



                if (TAGY[(i * 3) + 2] == "BIND_NEWCONTEST_NAME") { vyplnenyinput = VM.BIND_NEWCONTEST_NAME; }
                if (TAGY[(i * 3) + 2] == "BIND_NEWCONTEST_LOCATION") { vyplnenyinput = VM.BIND_NEWCONTEST_LOCATION; }
                if (TAGY[(i * 3) + 2] == "BIND_NEWCONTEST_DATE") { vyplnenyinput = VM.BIND_NEWCONTEST_DATE; }

                var currentWindow = this.TryFindParent<MetroWindow>();
                var result = await currentWindow.ShowInputAsync(TAGY[(i * 3)], TAGY[(i * 3) + 1], new MetroDialogSettings() { DefaultText = vyplnenyinput });
                if (result == null)
                    return;

                if (TAGY[(i * 3) + 2] == "BIND_NEWCONTEST_NAME") { 
                    VM.BIND_NEWCONTEST_NAME = result;


                }
                if (TAGY[(i * 3) + 2] == "BIND_NEWCONTEST_LOCATION") { VM.BIND_NEWCONTEST_LOCATION = result; }
                if (TAGY[(i * 3) + 2] == "BIND_NEWCONTEST_DATE") { VM.BIND_NEWCONTEST_DATE = result; }
            }




        }


        public static bool IsAllLettersOrDigits(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsLetterOrDigit(c) && c != ' ')
                    return false;
            }
            return true;
        }




        private void cateditoropen_Click(object sender, RoutedEventArgs e)
        {

            MetroWindow cateditor = new SORGAIR.categoryeditor();
            cateditor.Show(); // Shows Form2

           //categoryeditor.IsOpen = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
        private void saverules_Click(object sender, RoutedEventArgs e)
        {
        
    }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void newcontestcreate_Click(object sender, RoutedEventArgs e)
        {
            VM.SQL_OPENCONNECTION("RULES");
            VM.FUNCTION_LOAD_CATEGORIES();
            VM.SQL_CLOSECONNECTION("RULES");
            VM.BIND_NEWCONTEST_NAME = "Zadej název";
            VM.BIND_NEWCONTEST_CATEGORY = "---";
            VM.BIND_NEWCONTEST_LOCATION = "Zadej lokaci";

            newcontest.IsOpen = true;
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();

            if (VM.BIND_NEWCONTEST_NAME == "Zadej název" || VM.BIND_NEWCONTEST_LOCATION == "Zadej lokaci" || VM.BIND_NEWCONTEST_CATEGORY == "---")
            {
                await currentWindow.ShowMessageAsync("Není vše vyplněno", "Vyplň všechny požadované položky");
                return;
            }
            
            



            string newdbname = RemoveDiacritics(RemoveDiacritics(VM.BIND_NEWCONTEST_NAME) + "_" + DateTime.Now.ToString("yyyy_MM_dd") + "_" + VM.BIND_NEWCONTEST_CATEGORY).ToLower();
            Console.WriteLine("newdbname:" + newdbname);

            for (int x = 0; x < VM.MODEL_CONTESTS_FILES.Count; x++)
            {
                Console.WriteLine("VM.MODEL_CONTESTS_FILES[x].FILENAME:" + VM.MODEL_CONTESTS_FILES[x].FILENAME);
                if (newdbname == VM.MODEL_CONTESTS_FILES[x].FILENAME)
                {
                    await currentWindow.ShowMessageAsync("Nesprávný název", "Tento název již existuje");
                    return;
                }
            }

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            File.Copy(directory + "/Data/config/empty.db", directory + "/Data/" + newdbname + ".db");
            string catidindb;

            VM.SQL_OPENCONNECTION(newdbname);
            //VM.SQL_OPENCONNECTION("RULES");
            VM.SQL_OPENCONNECTION("RULES");
            catidindb = VM.SQL_READSORGDATA("select id from rules where category = '" + VM.BIND_NEWCONTEST_CATEGORY + "'","");
            VM.SQL_CLOSECONNECTION("RULES");

            VM.SQL_SAVESOUTEZDATA("ATTACH DATABASE '"+ directory + "/Data/config/rules.db'" + " AS rulesdb;");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO penalisations(id, value, textvalue, delete_landing, delete_time, delete_all) SELECT P.id, P.value, P.textvalue, P.delete_landing, P.delete_time, P.delete_all FROM rulesdb.penalisations P where P.category = '"+ catidindb +"';");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO penalisationsglobal(id, value, textvalue, delete_landing, delete_time, delete_all) SELECT P.id, P.value, P.textvalue, P.delete_landing, P.delete_time, P.delete_all FROM rulesdb.penalisationsglobal P where P.category = '" + catidindb + "';");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO landings(id, value, textvalue, lenght) SELECT L.id, L.value, L.textvalue, L.lenght FROM rulesdb.landings L where L.category = '" + catidindb + "';");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO rules SELECT * FROM rulesdb.rules where id = '" + catidindb + "';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='" + VM.BIND_NEWCONTEST_CATEGORY + "' where item='Category';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='" + VM.BIND_NEWCONTEST_LOCATION + "' where item='Location';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='" + VM.BIND_NEWCONTEST_DATE + "' where item='Date';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='" + VM.BIND_NEWCONTEST_NAME + "' where item='Name';");
            VM.SQL_CLOSECONNECTION("SOUTEZ");

            newcontest.IsOpen = false;
            VM.FUNCTION_LOAD_CONTESTS_FILES();
        }


        static string RemoveDiacritics(string text)
        {




            char[] arr = text.Where(c => (char.IsLetterOrDigit(c) || c == '-' || c == '_' )).ToArray();

            text = new string(arr);




            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {

                        stringBuilder.Append(c);
                }
            }

            string fixcontestname = stringBuilder.ToString().Normalize(NormalizationForm.FormC);
            fixcontestname = fixcontestname.Replace("Nazev souteze : ", string.Empty);
            fixcontestname = fixcontestname.Replace(" ", string.Empty);
            return fixcontestname;
        }




        private void categorylist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (categorylist.SelectedIndex>= 0)
                    {
                VM.BIND_NEWCONTEST_CATEGORY = VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].CATEGORY;
            }

        }

        private async void newcontestdelete_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            if (competitionlist.SelectedIndex >= 0)
            {
                Console.WriteLine(VM.BIND_SQL_SOUTEZ_DBFILE);
                Console.WriteLine(VM.MODEL_CONTESTS_FILES[competitionlist.SelectedIndex].FILENAME);

                if (VM.BIND_SQL_SOUTEZ_DBFILE== VM.MODEL_CONTESTS_FILES[competitionlist.SelectedIndex].FILENAME)
                {
                    await currentWindow.ShowMessageAsync("Nelze smazat", "Nelze smazat soutěž, která je právě načtena");
                }
                else
                {
                    MessageDialogResult result = await currentWindow.ShowMessageAsync("Smazání soutěže", "Opravdu smazat soutěž : " + VM.MODEL_CONTESTS_FILES[competitionlist.SelectedIndex].NAME + "?", MessageDialogStyle.AffirmativeAndNegative);
                    if (result == MessageDialogResult.Negative)
                    {
                        Console.WriteLine("No");
                    }
                    else
                    {
                        Console.WriteLine("yes");

                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var directory = System.IO.Path.GetDirectoryName(path);


                        File.Delete(directory + "/Data/" + VM.MODEL_CONTESTS_FILES[competitionlist.SelectedIndex].FILENAME + ".db");

                        VM.FUNCTION_LOAD_CONTESTS_FILES();
                        if (competitionlist.Items.Count > 0)
                        {
                            competitionlist.SelectedIndex = competitionlist.Items.Count - 1;
                            competitionlist.ScrollIntoView(competitionlist.Items[competitionlist.SelectedIndex]);
                        }

                    }
                }

              
            }
            else
            {
                await currentWindow.ShowMessageAsync("Není nic vybráno", "Vyber prosím, co chceš smazat");
            }
        }

        private void competitionlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (competitionlist.SelectedIndex >= 0)
            {
                newcontestload.IsEnabled = true;
                newcontestdelete.IsEnabled = true;
            }
            else
            {
                newcontestload.IsEnabled = false;
                newcontestdelete.IsEnabled = false;

            }
        }
    }
}
