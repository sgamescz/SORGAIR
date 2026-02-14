using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using WpfApp6.Model;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Threading;
using System.Globalization;



namespace SORGAIR
{
    /// <summary>
    /// Interakční logika pro languageselector.xaml
    /// </summary>
    public partial class categoryeditor : MetroWindow
    {


        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;


        public categoryeditor()
        {
            this.DataContext = new MODEL_ViewModel();
            var langcode = SORGAIR.Properties.Settings.Default.Languagecode;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langcode);

            InitializeComponent();
            InitializeDatabaseAsync();
        }

        private async void InitializeDatabaseAsync()
        {
            await VM.SQL_OPENCONNECTION("SORG");
            VM.SQL_READSORGDATA("select hodnota from nastaveni where polozka='pozadi'", "pozadi");
            VM.SQL_READSORGDATA("select hodnota from nastaveni where polozka='popredi' ", "popredi");

            await VM.SQL_OPENCONNECTION("RULES");
            Console.WriteLine(VM.SQL_READSORGDATA("select category from rules;", ""));
            VM.FUNCTION_LOAD_CATEGORIES();
            VM.FUNCTION_LOAD_CALENDAR_COUNTRY_SOURCES();


        }

        private void saverules_Click(object sender, RoutedEventArgs e)
        {





            //VM.BIND_SQL_SOUTEZ_KATEGORIE = "F5J";



            VM.SQL_SAVESORGDATA("update rules set points_under_limit1='" + VM.MODEL_CATEGORY_RULES[0].TIME1UNDER.ToString(new CultureInfo("en-US")) + "', limit1='" + VM.MODEL_CATEGORY_RULES[0].TIME1LIMIT.ToString(new CultureInfo("en-US")) + "', points_over_limit1='" + VM.MODEL_CATEGORY_RULES[0].TIME1OVER.ToString(new CultureInfo("en-US")) + "', " +
                        "heightunder='" + VM.MODEL_CATEGORY_RULES[0].HEIGHTUNDER.ToString(new CultureInfo("en-US")) + "', heightlimit='" + VM.MODEL_CATEGORY_RULES[0].HEIGHTLIMIT.ToString(new CultureInfo("en-US")) + "', heightover='" + VM.MODEL_CATEGORY_RULES[0].HEIGHTOVER.ToString(new CultureInfo("en-US")) + "', entryheight='" + VM.MODEL_CATEGORY_RULES[0].ENTRYHEIGHT + "'" +
                        ", limit2='" + VM.MODEL_CATEGORY_RULES[0].TIME2LIMIT.ToString(new CultureInfo("en-US")) + "', points_under_limit2='" + VM.MODEL_CATEGORY_RULES[0].TIME2UNDER.ToString(new CultureInfo("en-US")) + "', points_over_limit2='" + VM.MODEL_CATEGORY_RULES[0].TIME2OVER.ToString(new CultureInfo("en-US")) + "', " +
                        "sub_from_landing1='" + VM.MODEL_CATEGORY_RULES[0].SUBFROMLANDING1.ToString(new CultureInfo("en-US")) + "' , sub_from_landing2='" + VM.MODEL_CATEGORY_RULES[0].SUBFROMLANDING2.ToString(new CultureInfo("en-US")) + "', sub_from_time1='" + VM.MODEL_CATEGORY_RULES[0].SUBFROMTIME1.ToString(new CultureInfo("en-US")) + "', " +
                        "sub_from_time2='" + VM.MODEL_CATEGORY_RULES[0].SUBFROMTIME2.ToString(new CultureInfo("en-US")) + "' , delete_landing1='" + VM.MODEL_CATEGORY_RULES[0].DELETELANDING1 + "', delete_landing2='" + VM.MODEL_CATEGORY_RULES[0].DELETELANDING2 + "' " +
                        ", delete_time1='" + VM.MODEL_CATEGORY_RULES[0].DELETETIME1 + "' , delete_time2='" + VM.MODEL_CATEGORY_RULES[0].DELETETIME2 + "' , delete_all1='" + VM.MODEL_CATEGORY_RULES[0].DELETEALL1 + "' , delete_all2='" + VM.MODEL_CATEGORY_RULES[0].DELETEALL2 + 
                        "', category='" + VM.MODEL_CATEGORY_RULES[0].CATEGORY + "', BASEROUNDLENGHT='" + VM.MODEL_CATEGORY_RULES[0].BASEROUNDLENGHT + "' , BASEROUNDMAXTIME='" + VM.MODEL_CATEGORY_RULES[0].BASEROUNDMAXTIME + 
                        "', FINALROUNDLENGHT='" + VM.MODEL_CATEGORY_RULES[0].FINALROUNDLENGHT + "', FINALROUNDMAXTIME='" + VM.MODEL_CATEGORY_RULES[0].FINALROUNDMAXTIME + "',bonusonlyforfinalist='" + VM.MODEL_CATEGORY_RULES[0].BONUSONLYFORFINALIST + "',RECTO1000FROMABSMAX='" + VM.MODEL_CATEGORY_RULES[0].RECTO1000FROMABSMAX + "',RECOUNTSCORETO1000='" + VM.MODEL_CATEGORY_RULES[0].RECOUNTSCORETO1000 + "' where id = '" + VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID + "';");
            VM.FUNCTION_LOAD_CATEGORIES();


            categorylist.SelectedIndex = 0;
        }


        private void categorylist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categorylist.SelectedIndex >= 0)
            {
                Console.WriteLine(VM.BIND_MENU_ENABLED_nastavenisouteze);
                VM.FUNCTION_LOAD_CATEGORY_RULES(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].CATEGORY);
                VM.FUNCTION_LOAD_CATEGORY_LANDING(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
                VM.FUNCTION_LOAD_CATEGORY_SOUNDLISTS(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
                VM.FUNCTION_LOAD_CATEGORY_PENALISATION(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
                VM.FUNCTION_LOAD_CATEGORY_PENALISATIONGLOBAL(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
                VM.FUNCTION_LOAD_CATEGORY_BONUSPOINTS(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
                VM.BIND_CATEGORYEDITOR_ENABLED = true;
            }
        }

        private async void saverules_new_Click(object sender, RoutedEventArgs e)
        {

            var currentWindow = this;
            var _textvalue = await currentWindow.ShowInputAsync("Název kategorie", "Zadej název kategorie", new MetroDialogSettings() { DefaultText = "Nová kategorie" });

            if (_textvalue == null)
                return;


            VM.SQL_SAVESORGDATA("insert into rules (category,points_under_limit1, limit1,points_over_limit1,heightunder,heightlimit,heightover,entryheight,limit2,points_under_limit2,points_over_limit2," +
                "sub_from_landing1,sub_from_landing2,sub_from_time1,sub_from_time2,delete_landing1,delete_landing2,delete_time1,delete_time2,delete_all1,delete_all2,BASEROUNDLENGHT,BASEROUNDMAXTIME,FINALROUNDLENGHT,FINALROUNDMAXTIME,bonusonlyforfinalist,RECTO1000FROMABSMAX) " +
                "values('" + _textvalue + "'," + "'" + VM.MODEL_CATEGORY_RULES[0].TIME1UNDER.ToString(new CultureInfo("en-US")) + "','" + VM.MODEL_CATEGORY_RULES[0].TIME1LIMIT.ToString(new CultureInfo("en-US")) + "', '" + VM.MODEL_CATEGORY_RULES[0].TIME1OVER.ToString(new CultureInfo("en-US")) + "', " +
            "'" + VM.MODEL_CATEGORY_RULES[0].HEIGHTUNDER.ToString(new CultureInfo("en-US")) + "', '" + VM.MODEL_CATEGORY_RULES[0].HEIGHTLIMIT.ToString(new CultureInfo("en-US")) + "', '" + VM.MODEL_CATEGORY_RULES[0].HEIGHTOVER.ToString(new CultureInfo("en-US")) + "', '" + VM.MODEL_CATEGORY_RULES[0].ENTRYHEIGHT + "'" +
            ", '" + VM.MODEL_CATEGORY_RULES[0].TIME2LIMIT.ToString(new CultureInfo("en-US")) + "', '" + VM.MODEL_CATEGORY_RULES[0].TIME2UNDER.ToString(new CultureInfo("en-US")) + "', '" + VM.MODEL_CATEGORY_RULES[0].TIME2OVER.ToString(new CultureInfo("en-US")) + "', " +
            "'" + VM.MODEL_CATEGORY_RULES[0].SUBFROMLANDING1.ToString(new CultureInfo("en-US")) + "' , '" + VM.MODEL_CATEGORY_RULES[0].SUBFROMLANDING2.ToString(new CultureInfo("en-US")) + "', '" + VM.MODEL_CATEGORY_RULES[0].SUBFROMTIME1.ToString(new CultureInfo("en-US")) + "', " +
            "'" + VM.MODEL_CATEGORY_RULES[0].SUBFROMTIME2.ToString(new CultureInfo("en-US")) + "' , '" + VM.MODEL_CATEGORY_RULES[0].DELETELANDING1 + "', '" + VM.MODEL_CATEGORY_RULES[0].DELETELANDING2 + "' " +
            ", '" + VM.MODEL_CATEGORY_RULES[0].DELETETIME1 + "' , '" + VM.MODEL_CATEGORY_RULES[0].DELETETIME2 + "' , '" + VM.MODEL_CATEGORY_RULES[0].DELETEALL1 + "' , '" + VM.MODEL_CATEGORY_RULES[0].DELETEALL2 + "'" +
            ", '" + VM.MODEL_CATEGORY_RULES[0].BASEROUNDLENGHT + "', '" + VM.MODEL_CATEGORY_RULES[0].BASEROUNDMAXTIME + "', '" + VM.MODEL_CATEGORY_RULES[0].FINALROUNDLENGHT + "', '" + VM.MODEL_CATEGORY_RULES[0].FINALROUNDMAXTIME + "', '" + VM.MODEL_CATEGORY_RULES[0].BONUSONLYFORFINALIST+ "', '" + VM.MODEL_CATEGORY_RULES[0].RECTO1000FROMABSMAX + "');");

            int newid = Int32.Parse(VM.SQL_READSORGDATA("select id from rules where category = '" + _textvalue + "'", ""));
            savepenalisation(newid);
            savepenalisationglobal(newid);
            savelanding(newid);

            //int newsoundid = Int32.Parse(VM.SQL_READSORGDATA("select max(id) from Soundlist","")) ;
            for (int i = 0; i < soundlist_seznam.Items.Count; i++)
            {
                VM.SQL_SAVESORGDATA("insert into soundlist (category,soundname) select " + newid + ",soundname from soundlist where id = "+ VM.MODEL_CONTESTS_SOUNDLISTS[i].ID + " order by soundname;");
                int newiddd = Int32.Parse(VM.SQL_READSORGDATA("select id from soundlist where category=" + newid + " and soundname ='" + VM.MODEL_CONTESTS_SOUNDLISTS[i].TEXTVALUE + "' ; ", ""));
                VM.SQL_SAVESORGDATA("insert into sounds (category,id,second,filename,filedesc,todel) select " + newid + ","+newiddd+",second,filename,filedesc,todel from sounds where id = " + VM.MODEL_CONTESTS_SOUNDLISTS[i].ID + ";");
            }


            //VM.SQL_SAVESORGDATA("insert into soundlist (category,soundname) select "+newid+",soundname from soundlist where category = "+ VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID + " order by soundname;");

            //VM.SQL_READSORGDATA("select * from soundlist where category=" + newid +"; ","copysounds");



            VM.FUNCTION_LOAD_CATEGORIES();

            categorylist.SelectedIndex = 0;

        }

        private void deleterules_Click(object sender, RoutedEventArgs e)
        {
            VM.SQL_SAVESORGDATA("delete from rules where id=" + VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID + ";");
            VM.SQL_SAVESORGDATA("delete from Landings where category=" + VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID + ";");
            VM.SQL_SAVESORGDATA("delete from Penalisations where category=" + VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID + ";");
            VM.SQL_SAVESORGDATA("delete from Penalisationsglobal where category=" + VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID + ";");
            VM.SQL_SAVESORGDATA("delete from soundlist where category=" + VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID + ";");
            VM.SQL_SAVESORGDATA("delete from sounds where category=" + VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID + ";");
            VM.FUNCTION_LOAD_CATEGORIES();
            categorylist.SelectedIndex = 0;

        }

        private async void addlanding_Click(object sender, RoutedEventArgs e)
        {

            var currentWindow = this;

            var _id = await currentWindow.ShowInputAsync("Pozice", "Zadej číslo, na které pozici nový záznam bude", new MetroDialogSettings() { DefaultText = "0" });
            var _value = await currentWindow.ShowInputAsync("Body", "Zadej body (číslo)", new MetroDialogSettings() { DefaultText = "100" });
            var _textvalue = await currentWindow.ShowInputAsync("Textová hodnota", "Zadej slovní popis bodů. Např '100 bodů'", new MetroDialogSettings() { DefaultText = "100 bodů" });
            var _lenght = await currentWindow.ShowInputAsync("Délková hodnota", "Zadej délkovou hodnotu. Např 'Méně než 1 metr' nebo '< 1 m'", new MetroDialogSettings() { DefaultText = "< 1 m" });

            if (_id == null | _value == null | _textvalue == null | _lenght == null)
                return;


            var landing = new MODEL_CATEGORY_LANDING()
            {



                CATEGORY = 1,
                ID = int.Parse(_id),
                VALUE = int.Parse(_value),
                TEXTVALUE = _textvalue,
                LENGHT = _lenght,
                TODEL = 0

            };
            VM.MODEL_CATEGORY_LANDING.Add(landing);

            savelanding(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
        }

        private void deletelanding_Click(object sender, RoutedEventArgs e)
        {

                savelanding(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);

        }

        private void savelanding(int categoryid)
        {

            VM.SQL_SAVESORGDATA("delete from Landings where category = " + categoryid + ";");

            for (int i = 0; i < VM.MODEL_CATEGORY_LANDING.Count; i++)
            {

                if (VM.MODEL_CATEGORY_LANDING[i].TODEL == 0)
                {
                    VM.SQL_SAVESORGDATA("insert into Landings (category,id, value,textvalue,lenght) " +
                    "values('" + categoryid + "'," + "'" + VM.MODEL_CATEGORY_LANDING[i].ID + "','" + VM.MODEL_CATEGORY_LANDING[i].VALUE + "', '" + VM.MODEL_CATEGORY_LANDING[i].TEXTVALUE + "', " +
                "'" + VM.MODEL_CATEGORY_LANDING[i].LENGHT + "');");
                }



            }



            VM.FUNCTION_LOAD_CATEGORY_LANDING(categoryid);

        }



        private void savebonuspoints(int categoryid)
        {

            VM.SQL_SAVESORGDATA("delete from bonuspoints where category = " + categoryid + ";");

            for (int i = 0; i < VM.MODEL_CATEGORY_BONUSPOINTS.Count; i++)
            {

                if (VM.MODEL_CATEGORY_BONUSPOINTS[i].TODEL == 0)
                {
                    VM.SQL_SAVESORGDATA("insert into bonuspoints (category,id, value) " +
                    "values('" + categoryid + "'," + "'" + VM.MODEL_CATEGORY_BONUSPOINTS[i].ID + "','" + VM.MODEL_CATEGORY_BONUSPOINTS[i].VALUE + "');");
                }



            }



            VM.FUNCTION_LOAD_CATEGORY_BONUSPOINTS(categoryid);

        }


        private void savesound(int categoryid)
        {

            VM.SQL_SAVESORGDATA("delete from Sounds where category = " + categoryid + " and id='"+ VM.MODEL_CONTESTS_SOUNDLISTS[soundlist_seznam.SelectedIndex].ID  + "';");

            for (int i = 0; i < VM.MODEL_CATEGORY_SOUNDS.Count; i++)
            {

                if (VM.MODEL_CATEGORY_SOUNDS[i].TODEL == 0)
                {
                    VM.SQL_SAVESORGDATA("insert into Sounds (category,id, second,filename,filedesc) " +
                    "values('" + categoryid + "'," + "'" + VM.MODEL_CATEGORY_SOUNDS[i].ID + "','" + VM.MODEL_CATEGORY_SOUNDS[i].VALUE + "', '" + VM.MODEL_CATEGORY_SOUNDS[i].TEXTVALUE + "', " +
                "'" + VM.MODEL_CATEGORY_SOUNDS[i].LENGHT + "');");
                }



            }



            VM.FUNCTION_LOAD_CATEGORY_SOUNDS(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID, VM.MODEL_CONTESTS_SOUNDLISTS[soundlist_seznam.SelectedIndex].ID);

        }

        private void savepenalisation(int categoryid)
        {

            VM.SQL_SAVESORGDATA("delete from penalisations where category = " + categoryid + ";");

            for (int i = 0; i < VM.MODEL_CATEGORY_PENALISATION.Count; i++)
            {

                if (VM.MODEL_CATEGORY_PENALISATION[i].TODEL == 0)
                {
                    VM.SQL_SAVESORGDATA("insert into Penalisations (category,id, value,textvalue,delete_landing,delete_time, delete_all) " +
                    "values('" + categoryid + "'," + "'" + VM.MODEL_CATEGORY_PENALISATION[i].ID + "','" + VM.MODEL_CATEGORY_PENALISATION[i].VALUE + "', '" + VM.MODEL_CATEGORY_PENALISATION[i].TEXTVALUE + "', " +
                "'" + VM.MODEL_CATEGORY_PENALISATION[i].DELETE_LANDING + "'," + "'" + VM.MODEL_CATEGORY_PENALISATION[i].DELETE_TIME + "'," + "'" + VM.MODEL_CATEGORY_PENALISATION[i].DELETE_ALL + "');");
                }



            }



            VM.FUNCTION_LOAD_CATEGORY_PENALISATION(categoryid);

        }


        private void savepenalisationglobal(int categoryid)
        {

            VM.SQL_SAVESORGDATA("delete from penalisationsglobal where category = " + categoryid + ";");

            for (int i = 0; i < VM.MODEL_CATEGORY_PENALISATIONGLOBAL.Count; i++)
            {

                if (VM.MODEL_CATEGORY_PENALISATIONGLOBAL[i].TODEL == 0)
                {
                    VM.SQL_SAVESORGDATA("insert into Penalisationsglobal (category,id, value,textvalue,delete_landing,delete_time, delete_all) " +
                    "values('" + categoryid + "'," + "'" + VM.MODEL_CATEGORY_PENALISATIONGLOBAL[i].ID + "','" + VM.MODEL_CATEGORY_PENALISATIONGLOBAL[i].VALUE + "', '" + VM.MODEL_CATEGORY_PENALISATIONGLOBAL[i].TEXTVALUE + "', " +
                "'" + VM.MODEL_CATEGORY_PENALISATIONGLOBAL[i].DELETE_LANDING + "'," + "'" + VM.MODEL_CATEGORY_PENALISATIONGLOBAL[i].DELETE_TIME + "'," + "'" + VM.MODEL_CATEGORY_PENALISATIONGLOBAL[i].DELETE_ALL + "');");
                }



            }



            VM.FUNCTION_LOAD_CATEGORY_PENALISATIONGLOBAL(categoryid);

        }


        private void landinglist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(landinglist.SelectedIndex);
        }

        private void savelanding_Click(object sender, RoutedEventArgs e)
        {
            savelanding(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);

        }

        private async void addpenalisation_Click(object sender, RoutedEventArgs e)
        {

            var currentWindow = this;

            var _id = await currentWindow.ShowInputAsync("Pozice", "Zadej číslo, na které pozici nový záznam bude", new MetroDialogSettings() { DefaultText = "0" });
            var _value = await currentWindow.ShowInputAsync("Výše penalizace", "Zadej body (číslo) odpovídající penalizaci (např -50)", new MetroDialogSettings() { DefaultText = "-100" });
            var _textvalue = await currentWindow.ShowInputAsync("Textová hodnota", "Popis za co penalizace je. Např. 'Převrácení modelu'", new MetroDialogSettings() { DefaultText = "Převrácení modelu" });

            if (_id == null | _value == null | _textvalue == null)
                return;


            var landing = new MODEL_CATEGORY_PENALISATIONS()
            {



                CATEGORY = 1,
                ID = int.Parse(_id),
                VALUE = int.Parse(_value),
                TEXTVALUE = _textvalue,
                DELETE_LANDING = "False",
                DELETE_TIME = "False",
                DELETE_ALL = "False",
                TODEL = 0

            };
            VM.MODEL_CATEGORY_PENALISATION.Add(landing);
            savepenalisation(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
        }

        private void deletepenalisation_Click(object sender, RoutedEventArgs e)
        {
            savepenalisation(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
        }

        private void savepenalisation_Click(object sender, RoutedEventArgs e)
        {
            savepenalisation(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
        }

        private async void addpenalisationglobal_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this;

            var _id = await currentWindow.ShowInputAsync("Pozice", "Zadej číslo, na které pozici nový záznam bude", new MetroDialogSettings() { DefaultText = "0" });
            var _value = await currentWindow.ShowInputAsync("Výše penalizace", "Zadej body (číslo) odpovídající penalizaci (např -50)", new MetroDialogSettings() { DefaultText = "-100" });
            var _textvalue = await currentWindow.ShowInputAsync("Textová hodnota", "Popis za co penalizace je. Např. 'Převrácení modelu'", new MetroDialogSettings() { DefaultText = "Převrácení modelu" });

            if (_id == null | _value == null | _textvalue == null)
                return;


            var landing = new MODEL_CATEGORY_PENALISATIONS()
            {



                CATEGORY = 1,
                ID = int.Parse(_id),
                VALUE = int.Parse(_value),
                TEXTVALUE = _textvalue,
                DELETE_LANDING = "False",
                DELETE_TIME = "False",
                DELETE_ALL = "False",
                TODEL = 0

            };
            VM.MODEL_CATEGORY_PENALISATIONGLOBAL.Add(landing);
            savepenalisationglobal(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
        }

        private void deletepenalisationglobal_Click(object sender, RoutedEventArgs e)
        {
            savepenalisationglobal(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);

        }

        private void savepenalisationglobal_Click(object sender, RoutedEventArgs e)
        {
            savepenalisationglobal(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
        }



        private  void savesound_Click(object sender, RoutedEventArgs e)
        {
            savesound(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
        }

        private async void addsound_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this;

            
            var _value = await currentWindow.ShowInputAsync("Vteřina", "Zadej vteřinu, ve které se zvuk spustí", new MetroDialogSettings() { DefaultText = "60" });
            var _textvalue = await currentWindow.ShowInputAsync("Název souboru", "Zadej název mp3 souboru bez koncovky (např 5cz)", new MetroDialogSettings() { DefaultText = "5cz" });
            var _lenght = await currentWindow.ShowInputAsync("Slovní popis", "Zadej slovní popis zvuku", new MetroDialogSettings() { DefaultText = "---" });

            if ( _value == null | _textvalue == null | _lenght == null)
                return;


            var sound = new MODEL_CATEGORY_LANDING()
            {



                CATEGORY = 1,
                ID = VM.MODEL_CONTESTS_SOUNDLISTS[soundlist_seznam.SelectedIndex].ID,
                VALUE = int.Parse(_value),
                TEXTVALUE = _textvalue,
                LENGHT = _lenght,
                TODEL = 0

            };
            VM.MODEL_CATEGORY_SOUNDS.Add(sound);

            savesound(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
        }

        private void deletesound_Click(object sender, RoutedEventArgs e)
        {
            savesound(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
        }

        private void soundlist_seznam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (soundlist_seznam.SelectedIndex >= 0)
            {
                VM.FUNCTION_LOAD_CATEGORY_SOUNDS(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID, VM.MODEL_CONTESTS_SOUNDLISTS[soundlist_seznam.SelectedIndex].ID);
            }
        }

        private async void addsoundlist_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this;
            var _textvalue = await currentWindow.ShowInputAsync("Název zvuku", "Zadej název audio stopy", new MetroDialogSettings() { DefaultText = "100 bodů" });

            if (_textvalue == null)
                return;

            VM.SQL_SAVESORGDATA("insert into soundlist (category,soundname) values ("+ VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID + ",'" + _textvalue + "');");
            VM.FUNCTION_LOAD_CATEGORY_SOUNDLISTS(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
        }

        private void deletesoundlist_Click(object sender, RoutedEventArgs e)
        {
            VM.SQL_SAVESORGDATA("delete from sounds where category=" + VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID + " and id='" + VM.MODEL_CONTESTS_SOUNDLISTS[soundlist_seznam.SelectedIndex].ID + "';");
            VM.SQL_SAVESORGDATA("delete from soundlist where id=" + VM.MODEL_CONTESTS_SOUNDLISTS[soundlist_seznam.SelectedIndex].ID + ";");
            VM.FUNCTION_LOAD_CATEGORY_SOUNDLISTS(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
        }

        private async void renamesoundlist_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this;
            var _textvalue = await currentWindow.ShowInputAsync("Název zvuku", "Zadej název audio stopy", new MetroDialogSettings() { DefaultText = "100 bodů" });

            if ( _textvalue == null )
                return;

            VM.SQL_SAVESORGDATA("update soundlist set soundname = '"+ _textvalue +"' where id=" + VM.MODEL_CONTESTS_SOUNDLISTS[soundlist_seznam.SelectedIndex].ID + ";");
            VM.FUNCTION_LOAD_CATEGORY_SOUNDLISTS(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);

        }

        private async void  copysoundlist_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this;
            var _textvalue = await currentWindow.ShowInputAsync("Název zvuku", "Zadej název audio stopy", new MetroDialogSettings() { DefaultText = "Nový zvuk" });

            if (_textvalue == null)
                return;

            var _category = await currentWindow.ShowInputAsync("Do jiné kategorie?", "Zadej PŘESNÝ NÁZEV KATEGORIE do které chceš zvuk nakopírovat. Pokud chceš mít zvuk v této, tak tam nechej mínusku", new MetroDialogSettings() { DefaultText = "-" });

            if (_category == null)
                return;


            int destinationid = VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID;
            if (_category != "-")
            {
                destinationid = Int32.Parse( VM.SQL_READSORGDATA("select id from rules where category='" + _category + "' ; ", ""));
            }



            VM.SQL_SAVESORGDATA("insert into soundlist (category,soundname) select "+ destinationid + ",'"+ _textvalue+"' from soundlist where id = " + VM.MODEL_CONTESTS_SOUNDLISTS[soundlist_seznam.SelectedIndex].ID + " order by soundname;");
            int newiddd = Int32.Parse(VM.SQL_READSORGDATA("select id from soundlist where category=" + destinationid + " and soundname ='" + _textvalue + "' ; ", ""));
            VM.SQL_SAVESORGDATA("insert into sounds (category,id,second,filename,filedesc,todel) select "+ destinationid + "," + newiddd + ",second,filename,filedesc,todel from sounds where id = " + VM.MODEL_CONTESTS_SOUNDLISTS[soundlist_seznam.SelectedIndex].ID + ";");

            //VM.SQL_SAVESORGDATA("update soundlist set soundname = '" + _textvalue + "' where id=" + VM.MODEL_CONTESTS_SOUNDLISTS[soundlist_seznam.SelectedIndex].ID + ";");
            VM.FUNCTION_LOAD_CATEGORY_SOUNDLISTS(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);

        }

        private async void addbonuspoints_Click(object sender, RoutedEventArgs e)
        {

            var currentWindow = this;

            var _id = await currentWindow.ShowInputAsync("Pozice", "Zadej pozici soutěžícího, ke kterému se tyto budy připíšou", new MetroDialogSettings() { DefaultText = "0" });
            var _value = await currentWindow.ShowInputAsync("Body", "Zadej bonusové body (číslo)", new MetroDialogSettings() { DefaultText = "100" });

            if (_id == null | _value == null)
                return;


            var landing = new MODEL_CATEGORY_PENALISATIONS()
            {



                CATEGORY = 1,
                ID = int.Parse(_id),
                VALUE = int.Parse(_value),
                TEXTVALUE = "bonus",
                DELETE_LANDING = "False",
                DELETE_TIME = "False",
                DELETE_ALL = "False",
                TODEL = 0
            };

            VM.MODEL_CATEGORY_BONUSPOINTS.Add(landing);

            savebonuspoints(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
        }

        private void deletebonuspoints_Click(object sender, RoutedEventArgs e)
        {
            savebonuspoints(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
        }

        private void savebonuspointsall_Click(object sender, RoutedEventArgs e)
        {
            savebonuspoints(VM.MODEL_CONTESTS_CATEGORIES[categorylist.SelectedIndex].ID);
        }
    }
}
