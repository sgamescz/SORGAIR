using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Model;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Threading;
using System.Net;
using System.IO;
using System.Net.Cache;
using System.Globalization;
using SORGAIR.Properties.Lang;
using ControlzEx.Theming;
using System.Windows.Media;

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

        

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync(SORGAIR.Properties.Lang.Lang.home_load_caption, SORGAIR.Properties.Lang.Lang.home_load_caption);

            controller.SetProgress(0.1);
            controller.SetMessage(string.Format(SORGAIR.Properties.Lang.Lang.home_load_database));
            await Task.Delay(500);

            Console.WriteLine(VM.MODEL_CONTESTS_FILES[competitionlist.SelectedIndex].FILENAME);
            VM.SQL_OPENCONNECTION(VM.MODEL_CONTESTS_FILES[competitionlist.SelectedIndex].FILENAME);


            controller.SetProgress(0.2);
            controller.SetMessage(string.Format(SORGAIR.Properties.Lang.Lang.home_load_rules));
            await Task.Delay(10);

            VM.FUNCTION_LOADCONTEST();
            VM.FUNCTION_LOAD_RULES();
            VM.FUNCTION_SOUND_LOADAUDIO_LANGUAGE();

            controller.SetProgress(0.3);
            controller.SetMessage(string.Format(SORGAIR.Properties.Lang.Lang.home_load_users));
            await Task.Delay(10);

            VM.FUNCTION_USERS_LOAD_ALLCOMPETITORS();
            VM.FUNCTION_TEAM_LOAD_TEAMS();

            controller.SetProgress(0.5);
            controller.SetMessage(string.Format(SORGAIR.Properties.Lang.Lang.home_load_rounds));
            await Task.Delay(10);

            VM.FUNCTION_ROUNDS_LOAD_ROUNDS();

            controller.SetProgress(0.6);
            controller.SetMessage(string.Format(SORGAIR.Properties.Lang.Lang.home_load_sounds));
            await Task.Delay(10);

            VM.FUNCTION_SOUND_LOADSOUNDLIST();


            controller.SetProgress(0.8);
            controller.SetMessage(string.Format(SORGAIR.Properties.Lang.Lang.home_load_details));
            await Task.Delay(10);

            VM.FUNCTION_LOAD_DEFAULT_ROUNDSANDGROUPS();
            //VM.BIND_VYBRANEKOLOMENU = "Vybrané kolo: 1/4";
            VM.BIND_TYPEOFCLOCK = "PRE_MAIN";
            VM.clock_MAIN_create ();
            VM.clock_PREP_create();
            VM.clock_FINAL_MAIN_create();
            VM.clock_FINAL_PREP_create();
            controller.SetProgress(1);
            controller.SetMessage(string.Format(SORGAIR.Properties.Lang.Lang.home_load_complete));
            await Task.Delay(500);
            VM.BINDING_selectedmenuindex = 1;
            await Task.Delay(100);
            await controller.CloseAsync();


        }

       

        public async void thread_getsorgversion()
        {

            var langcode = SORGAIR.Properties.Settings.Default.Languagecode;

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langcode);
            Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(langcode);



            this.Invoke(() => VM.BIND_VERZE_SORGU_LAST = Lang.checking);
            this.Invoke(() => VM.BIND_NEWS_COUNT_ACTUAL = Lang.checking);
            this.Invoke(() => VM.BIND_NEWS_COUNT_NEXT = Lang.checking);
            this.Invoke(() => VM.BIND_NEWS_COUNT_NEXT_NEEDUPDATE = "-");
            this.Invoke(() => VM.BIND_NEWS_COUNT_ACTUAL_NEEDUPDATE = "-");


            Thread.Sleep(2000);
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
            string major = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Major.ToString().PadLeft(2, '0');
            string minor = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Minor.ToString().PadLeft(2, '0');
            string build = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Build.ToString().PadLeft(2, '0');
            string revision = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Revision.ToString().PadLeft(2, '0');

            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Console.WriteLine();

           

        }

        public void thread_getnewscount_next()
        {

            Thread.Sleep(2500);
            string major = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Major.ToString().PadLeft(2, '0');
            string minor = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Minor.ToString().PadLeft(2, '0');
            string build = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Build.ToString().PadLeft(2, '0');
            string revision = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Revision.ToString().PadLeft(2, '0');

            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Console.WriteLine(major + "." + minor + "." + build + "." + revision);


            string tmp_verze = major + minor + build + revision;

            string remoteUrl = "http://sorgair.com/api/news.php?version=" + tmp_verze + "&type=newest_than_actual";
            Console.WriteLine(remoteUrl);
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            HttpWebRequest.DefaultCachePolicy = policy;

            httpRequest.CachePolicy = policy;
            WebResponse response = httpRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();
            Console.WriteLine(result);


            this.Invoke(() => VM.BIND_NEWS_COUNT_NEXT = result);

        }


        public void thread_getnewscount_actual()
        {

            Thread.Sleep(2500);
            string major = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Major.ToString().PadLeft(2, '0');
            string minor = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Minor.ToString().PadLeft(2, '0');
            string build = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Build.ToString().PadLeft(2, '0');
            string revision = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Revision.ToString().PadLeft(2, '0');

            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Console.WriteLine(major + "." + minor + "." + build + "." + revision);


            string tmp_verze = major + minor + build + revision;
            string remoteUrl = "http://sorgair.com/api/news.php?version=" + tmp_verze +"&type=actual_and_older";
            Console.WriteLine(remoteUrl);
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            HttpWebRequest.DefaultCachePolicy = policy;

            httpRequest.CachePolicy = policy;
            WebResponse response = httpRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();
            Console.WriteLine(result);


            this.Invoke(() => VM.BIND_NEWS_COUNT_ACTUAL = result);

        }

        public void download_news(object sender, RoutedEventArgs e)
        {

            if (VM.BINDING_IS_INTERNET is true)
            {

                Thread version = new Thread(new ThreadStart(thread_getsorgversion));
                version.Start();

                Thread newsnext = new Thread(new ThreadStart(thread_getnewscount_next));
                newsnext.Start();

                Thread newsactual = new Thread(new ThreadStart(thread_getnewscount_actual));
                newsactual.Start();


            }



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
                if (TAGY[(i * 3) + 2] == "BIND_NEWCONTEST_COUNTRY") { vyplnenyinput = VM.BIND_NEWCONTEST_COUNTRY; }

                var currentWindow = this.TryFindParent<MetroWindow>();
                var result = await currentWindow.ShowInputAsync(TAGY[(i * 3)], TAGY[(i * 3) + 1], new MetroDialogSettings() { DefaultText = vyplnenyinput });
                if (result == null)
                    return;

                if (TAGY[(i * 3) + 2] == "BIND_NEWCONTEST_NAME") { 
                    VM.BIND_NEWCONTEST_NAME = result;


                }
                if (TAGY[(i * 3) + 2] == "BIND_NEWCONTEST_LOCATION") { VM.BIND_NEWCONTEST_LOCATION = result; }
                if (TAGY[(i * 3) + 2] == "BIND_NEWCONTEST_DATE") { VM.BIND_NEWCONTEST_DATE = result; }
                if (TAGY[(i * 3) + 2] == "BIND_NEWCONTEST_COUNTRY") { VM.BIND_NEWCONTEST_COUNTRY = result; }
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


        private async void newcontestcreate_Click(object sender, RoutedEventArgs e)
        {
            await VM.SQL_OPENCONNECTION("RULES");
            VM.FUNCTION_LOAD_CATEGORIES();
            VM.FUNCTION_LOAD_CALENDAR_COUNTRY_SOURCES();
            VM.SQL_CLOSECONNECTION("RULES");
            VM.BIND_NEWCONTEST_NAME = Lang.enter_contest_name;
            VM.BIND_NEWCONTEST_CATEGORY = "---";
            VM.BIND_NEWCONTEST_LOCATION = Lang.enter_contest_location;

            newcontest.IsOpen = true;
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();

            if (VM.BIND_NEWCONTEST_NAME == Lang.enter_contest_name || VM.BIND_NEWCONTEST_LOCATION == Lang.enter_contest_location || VM.BIND_NEWCONTEST_CATEGORY == "---")
            {
                await currentWindow.ShowMessageAsync(SORGAIR.Properties.Lang.Lang.home_noteverythinkfilled, SORGAIR.Properties.Lang.Lang.home_pleasefillallfields);
                return;
            }





            string newdbname = RemoveDiacritics(RemoveDiacritics(VM.BIND_NEWCONTEST_NAME) + "_" + DateTime.Now.ToString("yyyy_MM_dd") + "_" + VM.BIND_NEWCONTEST_CATEGORY).ToLower();
            Console.WriteLine("newdbname:" + newdbname);

            for (int x = 0; x < VM.MODEL_CONTESTS_FILES.Count; x++)
            {
                Console.WriteLine("VM.MODEL_CONTESTS_FILES[x].FILENAME:" + VM.MODEL_CONTESTS_FILES[x].FILENAME);
                if (newdbname == VM.MODEL_CONTESTS_FILES[x].FILENAME)
                {
                    await currentWindow.ShowMessageAsync(SORGAIR.Properties.Lang.Lang.home_wrongname, SORGAIR.Properties.Lang.Lang.home_thisnamealreadyexist);
                    return;
                }
            }

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            if (File.Exists(directory + "/Data/config/empty_"+ VM.BIND_NEWCONTEST_CATEGORY + ".db"))
            {
                File.Copy(directory + "/Data/config/empty_" + VM.BIND_NEWCONTEST_CATEGORY + ".db", directory + "/Data/" + newdbname + ".db");
            }
            else
            {
                File.Copy(directory + "/Data/config/empty.db", directory + "/Data/" + newdbname + ".db");
            }

            string catidindb;

            await VM.SQL_OPENCONNECTION(newdbname);
            //VM.SQL_OPENCONNECTION("RULES");
            await VM.SQL_OPENCONNECTION("RULES");
            catidindb = VM.SQL_READSORGDATA("select id from rules where category = '" + VM.BIND_NEWCONTEST_CATEGORY + "'","");
            VM.SQL_CLOSECONNECTION("RULES");

            VM.SQL_SAVESOUTEZDATA("ATTACH DATABASE '"+ directory + "/Data/config/rules.db'" + " AS rulesdb;");

            VM.SQL_SAVESOUTEZDATA("delete from Sounds;");
            VM.SQL_SAVESOUTEZDATA("delete from Soundlist;");
            VM.SQL_SAVESOUTEZDATA("delete from landings;");
            VM.SQL_SAVESOUTEZDATA("delete from penalisationsglobal;");
            VM.SQL_SAVESOUTEZDATA("delete from penalisations;");
            VM.SQL_SAVESOUTEZDATA("delete from rules;");
            VM.SQL_SAVESOUTEZDATA("delete from bonuspoints;");
            VM.SQL_SAVESOUTEZDATA("delete from sqlite_sequence;");

            VM.SQL_SAVESOUTEZDATA("INSERT INTO penalisations(id, value, textvalue, delete_landing, delete_time, delete_all) values (0, 0, '---', 'False', 'False', 'False');");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO penalisationsglobal(id, value, textvalue, delete_landing, delete_time, delete_all) values (0, 0, '---', 'False', 'False', 'False');");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO penalisations(id, value, textvalue, delete_landing, delete_time, delete_all) SELECT P.id, P.value, P.textvalue, P.delete_landing, P.delete_time, P.delete_all FROM rulesdb.penalisations P where P.category = '"+ catidindb +"';");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO penalisationsglobal(id, value, textvalue, delete_landing, delete_time, delete_all) SELECT P.id, P.value, P.textvalue, P.delete_landing, P.delete_time, P.delete_all FROM rulesdb.penalisationsglobal P where P.category = '" + catidindb + "';");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO landings(id, value, textvalue, lenght) SELECT L.id, L.value, L.textvalue, L.lenght FROM rulesdb.landings L where L.category = '" + catidindb + "';");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO Sounds(id, second, filename, filedesc) SELECT L.id, L.second, L.filename, L.filedesc FROM rulesdb.sounds L where L.category = '" + catidindb + "';");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO Soundlist(category, id, soundname) SELECT L.category, L.id , L.soundname FROM rulesdb.soundlist L where L.category = '" + catidindb + "';");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO rules SELECT * FROM rulesdb.rules where id = '" + catidindb + "';");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO bonuspoints(id, value) SELECT L.id, L.value FROM rulesdb.bonuspoints L where L.category = '" + catidindb + "';");
            VM.SQL_SAVESOUTEZDATA("delete from users where id>0;");
            VM.SQL_SAVESOUTEZDATA("update matrix set user='0';");
            VM.SQL_SAVESOUTEZDATA("update score set userid='0';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='" + VM.BIND_NEWCONTEST_CATEGORY + "' where item='Category';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='" + VM.BIND_NEWCONTEST_LOCATION + "' where item='Location';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='" + VM.BIND_NEWCONTEST_DATE + "' where item='Date';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='" + VM.BIND_NEWCONTEST_NAME + "' where item='Name';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='" + VM.BIND_NEWCONTEST_COUNTRY + "' where item='country';");

            VM.SQL_SAVESOUTEZDATA("update contest set value='999' where item='Matrix_score';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='999' where item='Matrix_score_final';");



      
      

            //VM.SQL_SAVESOUTEZDATA("delete from Groups;");
            // VM.SQL_SAVESOUTEZDATA("delete from Rounds;");
            //VM.SQL_SAVESOUTEZDATA("delete from Score;");
            //VM.SQL_SAVESOUTEZDATA("delete from final_rounds;");
            //VM.SQL_SAVESOUTEZDATA("delete from Matrix;");

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
            fixcontestname = fixcontestname.Replace(Lang.contest_name + " : ", string.Empty);
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
                    await currentWindow.ShowMessageAsync(SORGAIR.Properties.Lang.Lang.home_cannotbedeleted, SORGAIR.Properties.Lang.Lang.home_cannotbedeletedbecauseisloaded);
                }
                else
                {
                    MessageDialogResult result = await currentWindow.ShowMessageAsync(SORGAIR.Properties.Lang.Lang.home_contest_deleting_title, SORGAIR.Properties.Lang.Lang.home_contest_deleting_question + " : " + VM.MODEL_CONTESTS_FILES[competitionlist.SelectedIndex].NAME + "?", MessageDialogStyle.AffirmativeAndNegative);
                    if (result == MessageDialogResult.Negative)
                    {
                        Console.WriteLine("No");
                    }
                    else
                    {
                        Console.WriteLine("yes");

                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var directory = System.IO.Path.GetDirectoryName(path);

                        try
                        {
                            File.Delete(directory + "/Data/" + VM.MODEL_CONTESTS_FILES[competitionlist.SelectedIndex].FILENAME + ".db");
                        }
                        catch (Exception err)
                        {
                            Console.WriteLine(err.Message);
                        }


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
                await currentWindow.ShowMessageAsync(SORGAIR.Properties.Lang.Lang.home_nothing_selected_title, SORGAIR.Properties.Lang.Lang.home_nothing_selected_info);
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

        private void Button_Click_next(object sender, RoutedEventArgs e)
        {
            Window printwindow = new SORGAIR.News();
            printwindow.Show();
        }

        private void Button_Click_actual(object sender, RoutedEventArgs e)
        {
            Window printwindowact = new SORGAIR.News_actual();
            printwindowact.Show();
        }


        private void categorylistforinternet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categorylistforinternet.SelectedIndex >= 0 & internetcalendarsource.SelectedIndex >= 0)
            {
                VM.BIND_NEWCONTEST_CATEGORY_ONLINE = VM.MODEL_CONTESTS_CATEGORIES[categorylistforinternet.SelectedIndex].CATEGORY;
                VM.BIND_NEWCONTEST_CALENDAR_SOURCE = VM.MODEL_CONTESTS_CALENDAR_COUNTRY_SOURCES[internetcalendarsource.SelectedIndex].ADRESS;
                createonlinecontent.IsEnabled = true;
                VM.FUNCTION_LOAD_CONTESTS_ONLINE(VM.BIND_NEWCONTEST_CATEGORY_ONLINE, VM.BIND_NEWCONTEST_CALENDAR_SOURCE);
            }

        }

        private void listofcontestfrominternet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listofcontestfrominternet.SelectedIndex >= 0)
            {

                VM.BIND_NEWCONTEST_DATE_ONLINE = VM.MODEL_CONTESTS_ONLINE[listofcontestfrominternet.SelectedIndex].DATE;
                VM.BIND_NEWCONTEST_NAME_ONLINE = VM.MODEL_CONTESTS_ONLINE[listofcontestfrominternet.SelectedIndex].NAME;
                VM.BIND_NEWCONTEST_LOCATION_ONLINE = VM.MODEL_CONTESTS_ONLINE[listofcontestfrominternet.SelectedIndex].LOCATION;
                VM.BIND_NEWCONTEST_ID_ONLINE = VM.MODEL_CONTESTS_ONLINE[listofcontestfrominternet.SelectedIndex].FILENAME;
                VM.BIND_NEWCONTEST_SMCR_ONLINE = VM.MODEL_CONTESTS_ONLINE[listofcontestfrominternet.SelectedIndex].SMCRID;
                VM.BIND_NEWCONTEST_COUNTRY_ONLINE = VM.MODEL_CONTESTS_ONLINE[listofcontestfrominternet.SelectedIndex].COUNTRY;
            }
        }

        private async void createonlinecontent_Click(object sender, RoutedEventArgs e)
        {





            string newdbname = RemoveDiacritics(RemoveDiacritics(VM.BIND_NEWCONTEST_NAME_ONLINE) + "_" + DateTime.Now.ToString("yyyy_MM_dd") + "_" + VM.BIND_NEWCONTEST_CATEGORY_ONLINE).ToLower();
            Console.WriteLine("newdbname:" + newdbname);


            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            if (File.Exists(directory + "/Data/config/empty_" + VM.BIND_NEWCONTEST_CATEGORY_ONLINE + ".db"))
            {
                File.Copy(directory + "/Data/config/empty_" + VM.BIND_NEWCONTEST_CATEGORY_ONLINE + ".db", directory + "/Data/" + newdbname + ".db");
            }
            else
            {

                if (File.Exists(directory + "/Data/" + newdbname + ".db"))
                {
                    newdbname = newdbname + DateTime.Now.ToFileTime();
                }

                File.Copy(directory + "/Data/config/empty.db", directory + "/Data/" + newdbname + ".db");
            }

            string catidindb;

            await VM.SQL_OPENCONNECTION(newdbname);
            //VM.SQL_OPENCONNECTION("RULES");
            await VM.SQL_OPENCONNECTION("RULES");
            catidindb = VM.SQL_READSORGDATA("select id from rules where category = '" + VM.BIND_NEWCONTEST_CATEGORY_ONLINE + "'", "");
            VM.SQL_CLOSECONNECTION("RULES");
            VM.SQL_SAVESOUTEZDATA("delete from Sounds;");
            VM.SQL_SAVESOUTEZDATA("delete from Soundlist;");
            VM.SQL_SAVESOUTEZDATA("delete from landings;");
            VM.SQL_SAVESOUTEZDATA("delete from penalisationsglobal;");
            VM.SQL_SAVESOUTEZDATA("delete from penalisations;");
            VM.SQL_SAVESOUTEZDATA("delete from rules;");
            VM.SQL_SAVESOUTEZDATA("delete from bonuspoints;");
            VM.SQL_SAVESOUTEZDATA("delete from sqlite_sequence;");

            VM.SQL_SAVESOUTEZDATA("ATTACH DATABASE '" + directory + "/Data/config/rules.db'" + " AS rulesdb;");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO penalisations(id, value, textvalue, delete_landing, delete_time, delete_all) values (0, 0, '---', 'False', 'False', 'False');");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO penalisationsglobal(id, value, textvalue, delete_landing, delete_time, delete_all) values (0, 0, '---', 'False', 'False', 'False');");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO penalisations(id, value, textvalue, delete_landing, delete_time, delete_all) SELECT P.id, P.value, P.textvalue, P.delete_landing, P.delete_time, P.delete_all FROM rulesdb.penalisations P where P.category = '" + catidindb + "';");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO penalisationsglobal(id, value, textvalue, delete_landing, delete_time, delete_all) SELECT P.id, P.value, P.textvalue, P.delete_landing, P.delete_time, P.delete_all FROM rulesdb.penalisationsglobal P where P.category = '" + catidindb + "';");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO landings(id, value, textvalue, lenght) SELECT L.id, L.value, L.textvalue, L.lenght FROM rulesdb.landings L where L.category = '" + catidindb + "';");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO Sounds(id, second, filename, filedesc) SELECT L.id, L.second, L.filename, L.filedesc FROM rulesdb.sounds L where L.category = '" + catidindb + "';");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO Soundlist(category, id, soundname) SELECT L.category, L.id , L.soundname FROM rulesdb.soundlist L where L.category = '" + catidindb + "';");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO rules SELECT * FROM rulesdb.rules where id = '" + catidindb + "';");
            VM.SQL_SAVESOUTEZDATA("delete from users where id>0;");
            VM.SQL_SAVESOUTEZDATA("update matrix set user='0';");
            VM.SQL_SAVESOUTEZDATA("update score set userid='0';");
            VM.SQL_SAVESOUTEZDATA("INSERT INTO bonuspoints(id, value) SELECT L.id, L.value FROM rulesdb.bonuspoints L where L.category = '" + catidindb + "';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='" + VM.BIND_NEWCONTEST_CATEGORY_ONLINE + "' where item='Category';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='" + VM.BIND_NEWCONTEST_LOCATION_ONLINE + "' where item='Location';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='" + VM.BIND_NEWCONTEST_DATE_ONLINE + "' where item='Date';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='" + VM.BIND_NEWCONTEST_NAME_ONLINE + "' where item='Name';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='" + VM.BIND_NEWCONTEST_SMCR_ONLINE + "' where item='SMCRID';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='" + VM.BIND_NEWCONTEST_COUNTRY_ONLINE + "' where item='country';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='" + VM.BIND_NEWCONTEST_ID_ONLINE+ "' where item='sorgaircalendarcontestid';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='125478' where item='Matrix_score_final';");
            VM.SQL_SAVESOUTEZDATA("update contest set value='125478' where item='Matrix_score';");



            ///////////////

            string[] mArrayOfcontests = new string[300];


            string remoteUrl = "http://api.sorgair.com/api_contestdetail_new.php?id=" + VM.BIND_NEWCONTEST_ID_ONLINE;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            HttpWebRequest.DefaultCachePolicy = policy;

            httpRequest.CachePolicy = policy;
            WebResponse response = httpRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();


            String[] spearator = { "<br>" };
            String[] strlist = result.Split(spearator, 100, StringSplitOptions.None);
            foreach (String soutezici in strlist)
            {
                Console.WriteLine(soutezici);

                String[] spearator_sub = { "|" };

                if (soutezici.Length > 5)
                {

                    string _firstname = soutezici.Split(spearator_sub, 100, StringSplitOptions.None)[0];
                    string _lastname = soutezici.Split(spearator_sub, 100, StringSplitOptions.None)[1];
                    string _country = soutezici.Split(spearator_sub, 100, StringSplitOptions.None)[2];
                    int _agecat = int.Parse(soutezici.Split(spearator_sub, 100, StringSplitOptions.None)[3])-1;
                    int _agecat2 = int.Parse(soutezici.Split(spearator_sub, 100, StringSplitOptions.None)[4]);
                    string _freq = soutezici.Split(spearator_sub, 100, StringSplitOptions.None)[5];
                    string _chanel1 = soutezici.Split(spearator_sub, 100, StringSplitOptions.None)[6];
                    string _chanel2 = soutezici.Split(spearator_sub, 100, StringSplitOptions.None)[7];
                    string _failic = soutezici.Split(spearator_sub, 100, StringSplitOptions.None)[8];
                    string _naclic = soutezici.Split(spearator_sub, 100, StringSplitOptions.None)[9];
                    string _email = soutezici.Split(spearator_sub, 100, StringSplitOptions.None)[10];
                    string _club = soutezici.Split(spearator_sub, 100, StringSplitOptions.None)[11];

                    if (_freq.Contains("2,4")) { _freq = "0"; }
                    if (_freq.Contains("35")) { _freq = "1"; }

                    VM.SQL_SAVESOUTEZDATA("insert into users values (null,'" + _firstname + "', '" + _lastname + "', '" + _country + "', '" 
                        + _agecat + "', '" + _freq + "', '" + _chanel1 + "', '" + _chanel2 + "' , '" + _failic + "', '" + _naclic + "', '" + _club + "' , 'False', '0', '"+ _agecat2 + "' , 0 );");


                }




            }


            ////////////////







            VM.SQL_CLOSECONNECTION("SOUTEZ");

            newcontest.IsOpen = false;
            VM.FUNCTION_LOAD_CONTESTS_FILES();
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            newcontesttab.SelectedIndex = 1;
        }

        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            newcontesttab.SelectedIndex = 2;

        }
    }
}
