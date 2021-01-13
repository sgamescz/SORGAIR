using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;
using WpfApp6.View;
using System.IO;
using ControlzEx.Theming;
using System.Globalization;


namespace WpfApp6.Model
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    /// 




    public class TodoItem
    {
        public string Title { get; set; }
        public int Completion { get; set; }
        public List<TodoItem2> items2 { get; set; } = new List<TodoItem2>();

    }


    public class TodoItem2
    {
        public string name { get; set; }
        public int userid { get; set; }
        public int startpoint { get; set; }
    }


    

    public class MODEL_ViewModel : INotifyPropertyChanged
    {





        SQLiteConnection DBSORG_Connection;
        SQLiteConnection DBSOUTEZ_Connection;
        SQLiteConnection TMP_Connection;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();


        string[] barva = new string[] { "Red", "Green", "Blue", "Purple", "Orange", "Lime", "Emerald", "Teal", "Cyan", "Cobalt", "Indigo", "Violet", "Pink", "Magenta", "Crimson", "Amber", "Yellow", "Brown", "Olive", "Steel", "Mauve", "Taupe", "Sienna" };
        string[] pozadi = new string[] { "Light", "Dark" };
        int pouzitabarva = 1;
        int pouzitepozadi = 1;

        bool BIND_MENU_ENABLED_online_value = false;
        bool BIND_MENU_ENABLED_finale_value = false;
        bool BIND_MENU_ENABLED_detailyastatistiky_value = false;

        bool BIND_MENU_ENABLED_nastavenisouteze_value = false;
        bool BIND_MENU_ENABLED_audioadalsi_value = false;
        bool BIND_MENU_ENABLED_hardware_value = false;
        bool BIND_MENU_ENABLED_soutezici_value = false;
        bool BIND_MENU_ENABLED_rozlosovani_value = false;
        bool BIND_MENU_ENABLED_vybranekolo_value = false;
        bool BIND_MENU_ENABLED_vysledky_value = false;
        bool BIND_MENU_ENABLED_seznamkol_value = false;



        public string BIND_SQL_SOUTEZ_KATEGORIE_value;
        public string BIND_SQL_SOUTEZ_NAZEV_value;
        public string BIND_SQL_SOUTEZ_LOKACE_value;
        public string BIND_SQL_SOUTEZ_DATUM_value;
        public string BIND_SQL_SOUTEZ_TEPLOTA_value;
        public string BIND_SQL_SOUTEZ_POCASI_value;
        public string BIND_SQL_SOUTEZ_CLUB_value;
        public string BIND_SQL_SOUTEZ_SMCRID_value;
        public string BIND_SQL_SOUTEZ_DIRECTOR_value;
        public string BIND_SQL_SOUTEZ_HEADJURY_value;
        public string BIND_SQL_SOUTEZ_JURY1_value;
        public string BIND_SQL_SOUTEZ_JURY2_value;
        public string BIND_SQL_SOUTEZ_JURY3_value;
        public int BIND_SQL_SOUTEZ_ROUNDS_value = 100;
        public int BIND_SQL_SOUTEZ_GROUPS_value = 100;
        public int BIND_SQL_SOUTEZ_STARTPOINTS_value;
        public int BIND_SQL_SOUTEZ_DELETES_value;
        public int BIND_SQL_SOUTEZ_ROUNDSFINALE_value;
        public int BIND_SQL_SOUTEZ_STARTPOINTSFINALE_value;
        public int BIND_SQL_SOUTEZ_DELETESFINALE_value;
        public string BIND_VYBRANEKOLOMENU_value = SORGAIR.Properties.Lang.Lang.menu_selectedround  + " : 0/0";
        public string BIND_POCETSOUTEZICICHMENU_value = "0";
        public int BIND_POCETSOUTEZICICH_value = 0;
        public string BIND_CONTESTBEGIN_value;
        public bool BIND_USEAUDIO_value;
        public bool BIND_SQL_SOUTEZ_ENTRYSTYLE_value;
        public bool BIND_SQL_SOUTEZ_ENTRYSTYLENEXT_value;
        public bool BIND_SQL_AUDIO_COMPNUMBERS_value;
        public bool BIND_SQL_AUDIO_RNDGRPFLIGHT_value;
        public bool BIND_SQL_AUDIO_RNDGRPPREP_value;
        public string BIND_SORGNEWS_value = "Vítejte v nové verzi SORG AIR pro rok 2020. Aktuální novinky jsou blabla a tak dále a tak dale nějaký text z netu";
        public float BIND_LETOVYCAS_value = 0;
        public float BIND_LETOVYCAS_MAX_value = 600;
        public string BIND_LETOVYCAS_STRING_value = "xxx";
        public float BIND_PROGRESS_1_value = 0;


        public int BIND_SELECTED_ROUND_value = 1;
        public int BIND_SELECTED_GROUP_value = 1;
        public int _BIND_SELECTED_STARTPOINT;
        public int BIND_SELECTED_ROUND_DESC_value;
        public int BIND_SELECTED_GROUP_DECS_value;


        public int BIND_VIEWED_ROUND_value;
        public int BIND_VIEWED_GROUP_value;


        public bool BIND_SQL_AUTO_USEPREPTIME_value;
        public bool BIND_SQL_AUTO_RUNPREPTIMENEXTROUND_value;
        public bool BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME_value;

        public string BIND_SQL_AUTO_PREPTIMELENGHT_value;
        public string BIND_SQL_AUTO_PREPTIMESTART_value;

        public string BIND_FLAG_value;
        public string BIND_PAID_value;

        // Initialize a dynamic array items during declaration  


        private string[] mArrayOfflags = new string[300];


        public MODEL_ViewModel()
        {



        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region ostatni

        string _BIND_NEXTROUND_TEXT = " --- ";
        public string BIND_NEXTROUND_TEXT
        {
            get { return _BIND_NEXTROUND_TEXT; }
            set { _BIND_NEXTROUND_TEXT = value; OnPropertyChanged("BIND_NEXTROUND_TEXT"); }

        }


        int _BINDING_selectedmenuindex = 0;
        public int BINDING_selectedmenuindex
        {
            get { return _BINDING_selectedmenuindex; }
            set { _BINDING_selectedmenuindex = value; OnPropertyChanged("BINDING_selectedmenuindex"); }

        }

        


        string _BIND_PREWROUND_TEXT = " --- ";
        public string BIND_PREWROUND_TEXT
        {
            get { return _BIND_PREWROUND_TEXT; }
            set { _BIND_PREWROUND_TEXT = value; OnPropertyChanged("BIND_PREWROUND_TEXT"); }

        }



        public bool BIND_MENU_ENABLED_finale
        {
            get { return BIND_MENU_ENABLED_finale_value; }
            set { BIND_MENU_ENABLED_finale_value = value; OnPropertyChanged("BIND_MENU_ENABLED_finale"); }
        }

        public bool BIND_MENU_ENABLED_detailyastatistiky
        {
            get { return BIND_MENU_ENABLED_detailyastatistiky_value; }
            set { BIND_MENU_ENABLED_detailyastatistiky_value = value; OnPropertyChanged("BIND_MENU_ENABLED_detailyastatistiky"); }
        }

        public bool BIND_MENU_ENABLED_online
        {
            get { return BIND_MENU_ENABLED_online_value; }
            set { BIND_MENU_ENABLED_online_value = value; OnPropertyChanged("BIND_MENU_ENABLED_online"); }
        }


        public bool BIND_MENU_ENABLED_nastavenisouteze
        {
            get { return BIND_MENU_ENABLED_nastavenisouteze_value; }
            set { BIND_MENU_ENABLED_nastavenisouteze_value = value; OnPropertyChanged("BIND_MENU_ENABLED_nastavenisouteze"); }
        }

        public bool BIND_MENU_ENABLED_audioadalsi
        {
            get { return BIND_MENU_ENABLED_audioadalsi_value; }
            set { BIND_MENU_ENABLED_audioadalsi_value = value; OnPropertyChanged("BIND_MENU_ENABLED_audioadalsi"); }
        }

        public bool BIND_MENU_ENABLED_hardware
        {
            get { return BIND_MENU_ENABLED_hardware_value; }
            set { BIND_MENU_ENABLED_hardware_value = value; OnPropertyChanged("BIND_MENU_ENABLED_hardware"); }
        }

        public bool BIND_MENU_ENABLED_soutezici
        {
            get { return BIND_MENU_ENABLED_soutezici_value; }
            set { BIND_MENU_ENABLED_soutezici_value = value; OnPropertyChanged("BIND_MENU_ENABLED_soutezici"); }
        }

        public bool BIND_MENU_ENABLED_rozlosovani
        {
            get { return BIND_MENU_ENABLED_rozlosovani_value; }
            set { BIND_MENU_ENABLED_rozlosovani_value = value; OnPropertyChanged("BIND_MENU_ENABLED_rozlosovani"); }
        }

        public bool BIND_MENU_ENABLED_vybranekolo
        {
            get { return BIND_MENU_ENABLED_vybranekolo_value; }
            set { BIND_MENU_ENABLED_vybranekolo_value = value; OnPropertyChanged("BIND_MENU_ENABLED_vybranekolo"); }
        }

        public bool BIND_MENU_ENABLED_vysledky
        {
            get { return BIND_MENU_ENABLED_vysledky_value; }
            set { BIND_MENU_ENABLED_vysledky_value = value; OnPropertyChanged("BIND_MENU_ENABLED_vysledky"); }
        }

        public bool BIND_MENU_ENABLED_seznamkol
        {
            get { return BIND_MENU_ENABLED_seznamkol_value; }
            set { BIND_MENU_ENABLED_seznamkol_value = value; OnPropertyChanged("BIND_MENU_ENABLED_seznamkol"); }
        }



        public int Function_global_changeforeground
        {
            get { return pouzitabarva; }
            set { pouzitabarva = value; FUNCTION_Changeforegroundcolor(); }
        }
        public int Function_global_changebackground
        {
            get { return pouzitepozadi; }
            set { pouzitepozadi = value; FUNCTION_Changebackgroundcolor(); }
        }

        #endregion



        #region BIND_Nastavení
        public ObservableCollection<DataObject> xxxx { get; set; } = new ObservableCollection<DataObject>();

        public void FUNCTION_LOADCONTEST()
        {




            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            mArrayOfflags = Directory.GetFiles(directory + "/flags/", "*.*", SearchOption.TopDirectoryOnly);
            
            int _tmpi = -1;

            foreach (var file in mArrayOfflags)
            {
                _tmpi +=1;
                FileInfo info = new FileInfo(file);
                var players_flags = new MODEL_Player_flags()


                { ID = _tmpi, FILENAME = Path.GetFileNameWithoutExtension(info.Name)};
                MODEL_Contest_FLAGS.Add(players_flags);
            }


            var tmp_frequencies = new MODEL_Player_frequencies()
            { ID = 0, NAME = "2,4 GHz" };
            MODEL_Contest_FREQUENCIES.Add(tmp_frequencies);
            tmp_frequencies = new MODEL_Player_frequencies()
            { ID = 1, NAME = "35 MHz" };
            MODEL_Contest_FREQUENCIES.Add(tmp_frequencies);
            tmp_frequencies = new MODEL_Player_frequencies()
            { ID = 2, NAME = "40 MHz" };
            MODEL_Contest_FREQUENCIES.Add(tmp_frequencies);


            var tmp_agecategories = new MODEL_Player_agecategories()
            { ID = 0, NAME = "Senior" };
            MODEL_Contest_AGECATEGORIES.Add(tmp_agecategories);
            tmp_agecategories = new MODEL_Player_agecategories()
            { ID = 1, NAME = "Junior" };
            MODEL_Contest_AGECATEGORIES.Add(tmp_agecategories);
            tmp_agecategories = new MODEL_Player_agecategories()
            { ID = 2, NAME = "Žák" };
            MODEL_Contest_AGECATEGORIES.Add(tmp_agecategories);










            BIND_SQL_SOUTEZ_NAZEV = SQL_READSOUTEZDATA("select value from contest where item='Name'", "");
            BIND_SQL_SOUTEZ_KATEGORIE = SQL_READSOUTEZDATA("select value from contest where item='Category'", "");
            BIND_SQL_SOUTEZ_LOKACE = SQL_READSOUTEZDATA("select value from contest where item='Location'", "");
            BIND_SQL_SOUTEZ_DATUM = SQL_READSOUTEZDATA("select value from contest where item='Date'", "");
            BIND_SQL_SOUTEZ_TEPLOTA = SQL_READSOUTEZDATA("select value from contest where item='Temperature'", "");
            BIND_SQL_SOUTEZ_POCASI = SQL_READSOUTEZDATA("select value from contest where item='Weather'", "");
            BIND_SQL_SOUTEZ_CLUB = SQL_READSOUTEZDATA("select value from contest where item='Club'", "");
            BIND_SQL_SOUTEZ_SMCRID = SQL_READSOUTEZDATA("select value from contest where item='SMCRID'", "");
            BIND_SQL_SOUTEZ_DIRECTOR = SQL_READSOUTEZDATA("select value from contest where item='Director'", "");
            BIND_SQL_SOUTEZ_HEADJURY = SQL_READSOUTEZDATA("select value from contest where item='Headjury'", "");
            BIND_SQL_SOUTEZ_JURY1 = SQL_READSOUTEZDATA("select value from contest where item='Jury1'", "");
            BIND_SQL_SOUTEZ_JURY2 = SQL_READSOUTEZDATA("select value from contest where item='Jury2'", "");
            BIND_SQL_SOUTEZ_JURY3 = SQL_READSOUTEZDATA("select value from contest where item='Jury3'", "");
            BIND_SQL_SOUTEZ_ROUNDS = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Rounds'", ""));
            BIND_SQL_SOUTEZ_GROUPS = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Groups'", ""));
            BIND_SQL_SOUTEZ_STARTPOINTS = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Startpoints'", ""));
            BIND_SQL_SOUTEZ_DELETES = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Deletes'", ""));
            BIND_SQL_SOUTEZ_ROUNDSFINALE = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Roundsfinale'", ""));
            BIND_SQL_SOUTEZ_STARTPOINTSFINALE = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Startpointsfinale'", ""));
            BIND_SQL_SOUTEZ_DELETESFINALE = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Deletesfinale'", ""));
            BIND_CONTESTBEGIN = SQL_READSOUTEZDATA("select value from contest where item='contestbegin'", "");
            BIND_USEAUDIO = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='useaudio'", ""));
            BIND_SQL_SOUTEZ_ENTRYSTYLE = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Entrystyle'", ""));
            BIND_SQL_SOUTEZ_ENTRYSTYLENEXT = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Entrystylenext'", ""));
            BIND_SQL_SOUTEZ_REQUIREFAILICENCE = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='RequireFAILicence'", ""));
            BIND_SQL_AUDIO_COMPNUMBERS = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Audiocumpetitornumber'", ""));
            BIND_SQL_AUDIO_RNDGRPFLIGHT = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Rndgrpflight'", ""));
            BIND_SQL_AUDIO_RNDGRPPREP = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Rndgrpprep'", ""));
            BIND_SQL_AUTO_USEPREPTIME = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Usepreptime'", ""));
            BIND_SQL_AUTO_RUNPREPTIMENEXTROUND = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Runnextroundafterpreptime'", ""));
            BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Runpreptime'", ""));
            BIND_SQL_AUTO_PREPTIMELENGHT = SQL_READSOUTEZDATA("select value from contest where item='Preptimelenght'", "");
            BIND_SQL_AUTO_PREPTIMESTART = SQL_READSOUTEZDATA("select value from contest where item='Preptimestart'", "");

            FUNCTION_LOAD_MATRIX_FILES();
            FUNCTION_LOAD_TIMERS_MINUTES();
            FUNCTION_LOAD_TIMERS_SECONDS();
            FUNCTION_LOAD_TIMERS_HEIGHT();
            FUNCTION_LOAD_TIMERS_LANDINGS();

        }



        public string BIND_LETOVYCAS_STRING
        {
            get
            {
                //                TimeSpan time_elapsed = TimeSpan.FromSeconds(BIND_LETOVYCAS_value);
                var elapsed = timer.Elapsed;

                TimeSpan time_remaining = TimeSpan.FromSeconds(BIND_LETOVYCAS_MAX);
                TimeSpan totalsec = TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds);
                TimeSpan rozdil = time_remaining.Subtract(totalsec);
                //                Console.WriteLine(elapsed.ToString("mm':'ss':'f"));
                return "Letový čas : " + elapsed.ToString("mm':'ss':'ff") + " (zbývá : " + rozdil.ToString("mm':'ss':'ff") + ")";
            }

        }


        public float BIND_LETOVYCAS
        {
            get
            {
                return BIND_LETOVYCAS_value;
            }

            set
            {

                BIND_PROGRESS_1 = value;
                BIND_LETOVYCAS_value = value; OnPropertyChanged("BIND_LETOVYCAS"); OnPropertyChanged("BIND_LETOVYCAS_STRING");

            }

        }

        private string _BIND_VERZE_SORGU_LAST;
        public string BIND_VERZE_SORGU_LAST
        {
            get
            {
                return "Aktuální verze je : "+_BIND_VERZE_SORGU_LAST;
            }

            set
            {
                _BIND_VERZE_SORGU_LAST = value; OnPropertyChanged("BIND_VERZE_SORGU_LAST");
            }

        }



        private string _BIND_VERZE_SORGU;
        public string BIND_VERZE_SORGU
        {
            get
            {
                return "Tvá verze je : "+ _BIND_VERZE_SORGU;
            }

            set
            {
                _BIND_VERZE_SORGU = value; OnPropertyChanged("BIND_VERZE_SORGU"); 
            }

        }

        public float BIND_LETOVYCAS_MAX
        {
            get
            {
                return BIND_LETOVYCAS_MAX_value;
            }

            set
            {
                BIND_LETOVYCAS_MAX_value = value; OnPropertyChanged("BIND_LETOVYCAS_MAX"); OnPropertyChanged("BIND_LETOVYCAS_STRING");
            }

        }


        public float BIND_PROGRESS_1
        {
            get { return BIND_PROGRESS_1_value; }
            set { BIND_PROGRESS_1_value = value; OnPropertyChanged("BIND_PROGRESS_1"); }
        }



        public string BIND_SORGNEWS
        {
            get { return BIND_SORGNEWS_value; }
            set { BIND_SORGNEWS_value = value; OnPropertyChanged("BIND_SORGNEWS"); }
        }

        public string BIND_FLAG
        {
            get { return BIND_FLAG_value; }
            set { Console.WriteLine(value);
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var directory = System.IO.Path.GetDirectoryName(path);

                BIND_FLAG_value = directory + "/flags/" + value + ".png"; OnPropertyChanged("BIND_FLAG"); }
        }

        public string BIND_PAID
        {
            get { return BIND_PAID_value; }
            set { Console.WriteLine(value); BIND_PAID_value = value; OnPropertyChanged("BIND_PAID"); }

        }

        public string BIND_CONTESTBEGIN
        {
            get { return BIND_CONTESTBEGIN_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='contestbegin'"); BIND_CONTESTBEGIN_value = value; OnPropertyChanged("BIND_CONTESTBEGIN"); }
        }
        public bool BIND_USEAUDIO
        {
            get { return BIND_USEAUDIO_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='useaudio'"); BIND_USEAUDIO_value = value; OnPropertyChanged("BIND_USEAUDIO"); }
        }

        public string BIND_VYBRANEKOLOMENU
        {
            get { return SORGAIR.Properties.Lang.Lang.menu_selectedround + ":" + BIND_SELECTED_ROUND + "/" + BIND_SELECTED_GROUP; }
        }

        public string BIND_SELECTED_ROUND_DESC
        {
            get { return  SQL_READSOUTEZDATA("select Name from Rounds where id = "+ BIND_SELECTED_ROUND,""); }
        }

        public string BIND_SELECTED_GROUP_DESC
        {
            get { return SQL_READSOUTEZDATA("select Name from Groups where masterround = " + BIND_SELECTED_GROUP, ""); }
        }



        public string BIND_POCETSOUTEZICICHMENU
        {
            get { 
                
                if (int.Parse( BIND_POCETSOUTEZICICHMENU_value) >= (BIND_SQL_SOUTEZ_GROUPS * BIND_SQL_SOUTEZ_STARTPOINTS)) {
                    BIND_SOUTEZ_JEPLNO = false;
                }
                else
                {
                    BIND_SOUTEZ_JEPLNO = true;
                }
                return  SORGAIR.Properties.Lang.Lang.menu_competitors + " [" + BIND_POCETSOUTEZICICHMENU_value + "/"+(BIND_SQL_SOUTEZ_GROUPS*BIND_SQL_SOUTEZ_STARTPOINTS) + "]"; 
            
            }
            set { BIND_POCETSOUTEZICICHMENU_value = value; OnPropertyChanged("BIND_POCETSOUTEZICICHMENU"); }
        }


        public int BIND_POCETSOUTEZICICH
        {
            get { return BIND_POCETSOUTEZICICH_value; }
            set { BIND_POCETSOUTEZICICH_value = value; OnPropertyChanged("BIND_POCETSOUTEZICICH"); }
        }




        public int BIND_SQL_SOUTEZ_ROUNDS
        {
            get { return BIND_SQL_SOUTEZ_ROUNDS_value; }

            set {
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Rounds'");
                if (value > BIND_SQL_SOUTEZ_ROUNDS_value)
                {
                    SQL_SAVESOUTEZDATA("insert into rounds (id,name,type,lenght,zadano) values (" + value + ",'kolo:a_"+ value + "','auto',600,0);");

                    for (int i = 1; i < BIND_SQL_SOUTEZ_GROUPS+1 ; i++)
                    {
                        SQL_SAVESOUTEZDATA("insert into groups (id,name,type,lenght,zadano, masterround, groupnumber) values (null, 'autogrp_" + i + "','auto',600,0, " + value + " ," + i + ");");
                    }

                }
                SQL_SAVESOUTEZDATA("delete from rounds where id > " + value + ";");
                SQL_SAVESOUTEZDATA("delete from groups where masterround > " + value + ";");

                BIND_SQL_SOUTEZ_ROUNDS_value = value;
                OnPropertyChanged("BIND_SQL_SOUTEZ_ROUNDS");
                FUNCTION_LOAD_MATRIX_FILES();

            }
        }




        public int BIND_SQL_SOUTEZ_GROUPS
        {
            get { return BIND_SQL_SOUTEZ_GROUPS_value; }


            set
            {


                if (value > BIND_SQL_SOUTEZ_GROUPS_value)
                {

                    for (int i = 1; i < BIND_SQL_SOUTEZ_ROUNDS + 1; i++)
                    {
                        SQL_SAVESOUTEZDATA("insert into groups (id,name,type,lenght,zadano, masterround, groupnumber) values (null, 'autogrp_" + value + "','auto',600,0, " + i + " ," + value + ");");
                    }

                }


                //
                if ((value * BIND_SQL_SOUTEZ_STARTPOINTS) >= BIND_POCETSOUTEZICICH)
                {
                    Console.WriteLine("vetsi nez mozno");
                    SQL_SAVESOUTEZDATA("delete from groups where groupnumber > " + value + ";");
                    SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Groups'");
                    BIND_SQL_SOUTEZ_GROUPS_value = value;
                    OnPropertyChanged("BIND_SQL_SOUTEZ_GROUPS");
                    FUNCTION_LOAD_MATRIX_FILES();
                    BIND_POCETSOUTEZICICHMENU = BIND_POCETSOUTEZICICHMENU_value;


                }
                else
                {
                    Console.WriteLine("mensi nez mozno");
                    BIND_SQL_SOUTEZ_GROUPS_value = value+1;
                    OnPropertyChanged("BIND_SQL_SOUTEZ_GROUPS");

                }

                //




            }
        }



        public int BIND_SELECTED_ROUND
        {
            get { return BIND_SELECTED_ROUND_value; }
            set { BIND_SELECTED_ROUND_value = value; OnPropertyChanged("BIND_SELECTED_ROUND"); OnPropertyChanged("BIND_VYBRANEKOLOMENU"); OnPropertyChanged("BIND_SELECTED_ROUND_DESC"); OnPropertyChanged("BIND_SELECTED_GROUP_DESC"); Console.WriteLine("BIND_SELECTED_ROUND:" + BIND_SELECTED_ROUND); }
        }

        public int BIND_SELECTED_STARTPOINT
        {
            get { return _BIND_SELECTED_STARTPOINT; }
            set { _BIND_SELECTED_STARTPOINT = value; OnPropertyChanged("BIND_SELECTED_STARTPOINT"); Console.WriteLine("BIND_SELECTED_STARTPOINT:" + BIND_SELECTED_STARTPOINT); }
        }


        public int BIND_SELECTED_GROUP
        {
            get { return BIND_SELECTED_GROUP_value; }
            set { BIND_SELECTED_GROUP_value = value; OnPropertyChanged("BIND_SELECTED_GROUP"); OnPropertyChanged("BIND_VYBRANEKOLOMENU"); OnPropertyChanged("BIND_SELECTED_ROUND_DESC"); OnPropertyChanged("BIND_SELECTED_GROUP_DESC"); Console.WriteLine("BIND_SELECTED_GROUP" + BIND_SELECTED_GROUP); }
        }



        public int BIND_VIEWED_ROUND
        {
            get { return BIND_VIEWED_ROUND_value; }
            set { BIND_VIEWED_ROUND_value = value; OnPropertyChanged("BIND_VIEWED_ROUND"); Console.WriteLine("BIND_VIEWED_ROUND:" + BIND_VIEWED_ROUND); }
        }


        public bool _BIND_SOUTEZ_JEPLNO = false;
        public bool BIND_SOUTEZ_JEPLNO
        {
            get { return _BIND_SOUTEZ_JEPLNO; }
            set { _BIND_SOUTEZ_JEPLNO = value; OnPropertyChanged("BIND_SOUTEZ_JEPLNO"); }
        }

        public int BIND_VIEWED_GROUP
        {
            get { return BIND_VIEWED_GROUP_value; }
            set { BIND_VIEWED_GROUP_value = value; OnPropertyChanged("BIND_VIEWED_GROUP"); Console.WriteLine("BIND_VIEWED_GROUP" + BIND_VIEWED_GROUP); }
        }

        public int BIND_SQL_SOUTEZ_STARTPOINTS
        {
            get { return BIND_SQL_SOUTEZ_STARTPOINTS_value; }
            set { 

              

                //
                if ((value * BIND_SQL_SOUTEZ_GROUPS) >= BIND_POCETSOUTEZICICH)
                {
                    SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Startpoints'");
                    BIND_SQL_SOUTEZ_STARTPOINTS_value = value;
                    OnPropertyChanged("BIND_SQL_SOUTEZ_STARTPOINTS");
                    FUNCTION_LOAD_MATRIX_FILES();
                    BIND_POCETSOUTEZICICHMENU = BIND_POCETSOUTEZICICHMENU_value;


                }
                else
                {
                    Console.WriteLine("mensi nez mozno");
                    BIND_SQL_SOUTEZ_STARTPOINTS_value = value + 1;
                    OnPropertyChanged("BIND_SQL_SOUTEZ_STARTPOINTS");

                }

                //



            }
        }

        public int BIND_SQL_SOUTEZ_DELETES
        {
            get { return BIND_SQL_SOUTEZ_DELETES_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Deletes'"); BIND_SQL_SOUTEZ_DELETES_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_DELETES"); }
        }

        public int BIND_SQL_SOUTEZ_ROUNDSFINALE
        {
            get { return BIND_SQL_SOUTEZ_ROUNDSFINALE_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Roundsfinale'"); BIND_SQL_SOUTEZ_ROUNDSFINALE_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_ROUNDSFINALE"); }
        }

        public int BIND_SQL_SOUTEZ_STARTPOINTSFINALE
        {
            get { return BIND_SQL_SOUTEZ_STARTPOINTSFINALE_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Startpointsfinale'"); BIND_SQL_SOUTEZ_STARTPOINTSFINALE_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_STARTPOINTSFINALE"); }
        }

        public int BIND_SQL_SOUTEZ_DELETESFINALE
        {
            get { return BIND_SQL_SOUTEZ_DELETESFINALE_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Deletesfinale'"); BIND_SQL_SOUTEZ_DELETESFINALE_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_DELETESFINALE"); }
        }


        public string BIND_SQL_SOUTEZ_JURY1
        {
            get { return BIND_SQL_SOUTEZ_JURY1_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Jury1'"); BIND_SQL_SOUTEZ_JURY1_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_JURY1"); }
        }

        public string BIND_SQL_SOUTEZ_JURY2
        {
            get { return BIND_SQL_SOUTEZ_JURY2_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Jury2'"); BIND_SQL_SOUTEZ_JURY2_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_JURY2"); }
        }

        public string BIND_SQL_SOUTEZ_JURY3
        {
            get { return BIND_SQL_SOUTEZ_JURY3_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Jury3'"); BIND_SQL_SOUTEZ_JURY3_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_JURY3"); }
        }
        public string BIND_SQL_SOUTEZ_SMCRID
        {
            get { return BIND_SQL_SOUTEZ_SMCRID_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='SMCRID'"); BIND_SQL_SOUTEZ_SMCRID_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_SMCRID"); }
        }

        public string BIND_SQL_SOUTEZ_DIRECTOR
        {
            get { return BIND_SQL_SOUTEZ_DIRECTOR_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Director'"); BIND_SQL_SOUTEZ_DIRECTOR_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_DIRECTOR"); }
        }
        public string BIND_SQL_SOUTEZ_HEADJURY
        {
            get { return BIND_SQL_SOUTEZ_HEADJURY_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Headjury'"); BIND_SQL_SOUTEZ_HEADJURY_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_HEADJURY"); }
        }
        public string BIND_SQL_SOUTEZ_KATEGORIE
        {
            get { return BIND_SQL_SOUTEZ_KATEGORIE_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Category'"); BIND_SQL_SOUTEZ_KATEGORIE_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_KATEGORIE"); }
        }
        public string BIND_SQL_SOUTEZ_NAZEV
        {
            get { return "Název soutěže : " + BIND_SQL_SOUTEZ_NAZEV_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Name'"); BIND_SQL_SOUTEZ_NAZEV_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_NAZEV"); }
        }
        public string BIND_SQL_SOUTEZ_LOKACE
        {
            get { return "Lokace : " + BIND_SQL_SOUTEZ_LOKACE_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Location'"); BIND_SQL_SOUTEZ_LOKACE_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_LOKACE"); }
        }



        public string BIND_SQL_SOUTEZ_DATUM
        {
            get { return BIND_SQL_SOUTEZ_DATUM_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Date'"); BIND_SQL_SOUTEZ_DATUM_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_DATUM"); }
        }

        public bool BIND_SQL_SOUTEZ_ENTRYSTYLE
        {
            get { return BIND_SQL_SOUTEZ_ENTRYSTYLE_value; }
            set { 
                

                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Entrystyle'"); 
                BIND_SQL_SOUTEZ_ENTRYSTYLE_value = value; 
                if (value == true) {
                    ScoreEntryType = "Text";
                }
                else
                {
                    ScoreEntryType = "Value";
                }
                OnPropertyChanged("BIND_SQL_SOUTEZ_ENTRYSTYLE");
                OnPropertyChanged("ScoreEntryType");
            }
        }

        public bool BIND_SQL_SOUTEZ_ENTRYSTYLENEXT
        {
            get { return BIND_SQL_SOUTEZ_ENTRYSTYLENEXT_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Entrystylenext'"); BIND_SQL_SOUTEZ_ENTRYSTYLENEXT_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_ENTRYSTYLENEXT"); }
        }

        bool _BIND_SQL_SOUTEZ_REQUIREFAILICENCE = false;
        public bool BIND_SQL_SOUTEZ_REQUIREFAILICENCE
        {
            get { return _BIND_SQL_SOUTEZ_REQUIREFAILICENCE; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='RequireFAILicence'"); _BIND_SQL_SOUTEZ_REQUIREFAILICENCE = value; OnPropertyChanged("BIND_SQL_SOUTEZ_REQUIREFAILICENCE"); }
        }

        public bool BIND_SQL_AUDIO_COMPNUMBERS
        {
            get { return BIND_SQL_AUDIO_COMPNUMBERS_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Audiocumpetitornumber'"); BIND_SQL_AUDIO_COMPNUMBERS_value = value; OnPropertyChanged("BIND_SQL_AUDIO_COMPNUMBERS"); }
        }


        public bool BIND_SQL_AUDIO_RNDGRPFLIGHT
        {
            get { return BIND_SQL_AUDIO_RNDGRPFLIGHT_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Rndgrpflight'"); BIND_SQL_AUDIO_RNDGRPFLIGHT_value = value; OnPropertyChanged("BIND_SQL_AUDIO_RNDGRPFLIGHT"); }
        }

        public bool BIND_SQL_AUDIO_RNDGRPPREP
        {
            get { return BIND_SQL_AUDIO_RNDGRPPREP_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Rndgrpprep'"); BIND_SQL_AUDIO_RNDGRPPREP_value = value; OnPropertyChanged("BIND_SQL_AUDIO_RNDGRPPREP"); }
        }


        public string BIND_SQL_SOUTEZ_TEPLOTA
        {
            get { return BIND_SQL_SOUTEZ_TEPLOTA_value + "°C"; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Temperature'"); BIND_SQL_SOUTEZ_TEPLOTA_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_TEPLOTA"); }
        }

        public string BIND_SQL_SOUTEZ_POCASI
        {
            get { return BIND_SQL_SOUTEZ_POCASI_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Weather'"); BIND_SQL_SOUTEZ_POCASI_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_POCASI"); }
        }

        public string BIND_SQL_SOUTEZ_CLUB
        {
            get { return BIND_SQL_SOUTEZ_CLUB_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Club'"); BIND_SQL_SOUTEZ_CLUB_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_CLUB"); }
        }







        public bool BIND_SQL_AUTO_USEPREPTIME
        {
            get { return BIND_SQL_AUTO_USEPREPTIME_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Usepreptime'"); BIND_SQL_AUTO_USEPREPTIME_value = value; OnPropertyChanged("BIND_SQL_AUTO_USEPREPTIME"); }
        }

        public bool BIND_SQL_AUTO_RUNPREPTIMENEXTROUND
        {
            get { return BIND_SQL_AUTO_RUNPREPTIMENEXTROUND_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Runnextroundafterpreptime'"); BIND_SQL_AUTO_RUNPREPTIMENEXTROUND_value = value; OnPropertyChanged("BIND_SQL_AUTO_RUNPREPTIMENEXTROUND"); }
        }

        public bool BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME
        {
            get { return BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Runpreptime'"); BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME_value = value; OnPropertyChanged("BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME"); }
        }

        public string BIND_SQL_AUTO_PREPTIMELENGHT
        {
            get { return BIND_SQL_AUTO_PREPTIMELENGHT_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Preptimelenght'"); BIND_SQL_AUTO_PREPTIMELENGHT_value = value; OnPropertyChanged("BIND_SQL_AUTO_PREPTIMELENGHT"); }
        }


        public string BIND_SQL_AUTO_PREPTIMESTART
        {
            get { return BIND_SQL_AUTO_PREPTIMESTART_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Preptimestart'"); BIND_SQL_AUTO_PREPTIMESTART_value = value; OnPropertyChanged("BIND_SQL_AUTO_PREPTIMESTART"); }
        }





        #endregion

        #region SQL_funkce
        public async void SQL_OPENCONNECTION(string KTERADB)
        {

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            if (KTERADB == "SORG")
            {
                DBSORG_Connection = new SQLiteConnection("Data Source=" + directory + "/Data/config/sorgair.db;");
                DBSORG_Connection.Open();

            }
            else{
                try
                {
                    DBSOUTEZ_Connection = new SQLiteConnection("Data Source=" + directory + "/Data/"+ KTERADB + ".db;");
                    DBSOUTEZ_Connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                // Error: Use of unassigned local variable 'n'.
            }

            Console.WriteLine("SQL_OPENCONNECTION [OPEN] : " + KTERADB);


        }

        public void SQL_SAVESORGDATA(string sqltext)
        {

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            SQLiteCommand command = new SQLiteCommand(sqltext, DBSORG_Connection);

            Console.WriteLine("SQL_SAVESORGDATA [SQL] : " + sqltext);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SQLiteException myException)
            {
                Console.WriteLine("SQL_SAVESOUTEZDATA [ERROR] : " + myException.Message + "\n");
            }



        }



        public void SQL_SAVESOUTEZDATA(string sqltext)
        {

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            SQLiteCommand command = new SQLiteCommand(sqltext, DBSOUTEZ_Connection);

            Console.WriteLine("SQL_SAVESOUTEZDATA [SQL] : " + sqltext);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SQLiteException myException)
            {
                Console.WriteLine("SQL_SAVESOUTEZDATA [ERROR] : " + myException.Message + "\n");
            }



        }





        public List<TodoItem> SQL__SUBQUERY_ADD_GROUPS(int kolo)

        {




            Console.WriteLine("select rnd,groupnumber,g.* from matrix M left join groups G on M.rnd = G.masterround where M.rnd=" + kolo + " group by groupnumber;");
            List<TodoItem> test = new List<TodoItem>();
            List<TodoItem2> test2 = new List<TodoItem2>();

            SQLiteCommand command = new SQLiteCommand("select rnd,groupnumber,g.* from matrix M left join groups G on M.rnd = G.masterround where M.rnd=" + kolo + " group by groupnumber;", DBSOUTEZ_Connection);
            SQLiteDataReader sqlite_datareader;

            try
            {
                sqlite_datareader = command.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    test2 = SQL__SUBQUERY_ADD_STARTPOINTS(kolo, sqlite_datareader.GetInt32(1));

                    string myreader = sqlite_datareader.GetString(3);
                    test.Add(new TodoItem() { Title = myreader, Completion = 45, items2 = test2 });

                    Console.WriteLine("SQL_READSORGDATA [READ DATA] : " + myreader + " >>>> " + "kamulozitvysledek");
                }
            }
            catch (SQLiteException myException)
            {
                Console.WriteLine("Message: " + myException.Message + "\n");

            }



            return test;





        }





        public List<TodoItem2> SQL__SUBQUERY_ADD_STARTPOINTS(int kolo, int group)

        {




            Console.WriteLine("select * from matrix M left join users U on M.user = U.ID where rnd=" + kolo + " and grp=" + group + ";");
            List<TodoItem2> test2 = new List<TodoItem2>();
            SQLiteCommand command = new SQLiteCommand("select * from matrix M left join users U on M.user = U.ID where rnd = " + kolo + " and grp = " + group + "; ", DBSOUTEZ_Connection);
            SQLiteDataReader sqlite_datareader;

            try
            {
                sqlite_datareader = command.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    string tmpname = sqlite_datareader.GetString(6) + " " + sqlite_datareader.GetString(5);
                    int tmpid = sqlite_datareader.GetInt32(3);
                    int tmpstp = sqlite_datareader.GetInt32(2);
                    test2.Add(new TodoItem2() { name = tmpname, userid = tmpid, startpoint = tmpstp });

                    Console.WriteLine("SQL_READSORGDATA [READ DATA] : " + tmpname + " >>>> " + "kamulozitvysledek");
                }
            }
            catch (SQLiteException myException)
            {
                Console.WriteLine("Message: " + myException.Message + "\n");

            }



            return test2;





        }






        public void SQL_CLOSECONNECTION(string KTERADB)
        {

            if (KTERADB == "SORG")
            {
                DBSORG_Connection.Close();
            }

            if (KTERADB == "SOUTEZ")
            {
                DBSOUTEZ_Connection.Close();
            }

            Console.WriteLine("SQL_OPENCONNECTION [CLOSE] : " + KTERADB);

        }


        public string SQL_READSORGDATA(string sqltext, string kamulozitvysledek)
        {


            string vysledek = "";




            SQLiteCommand command = new SQLiteCommand(sqltext, DBSORG_Connection);

            Console.WriteLine("SQL_READSORGDATA [SQL] : " + sqltext + " >>>> " + kamulozitvysledek);


            SQLiteDataReader sqlite_datareader;
            try
            {
                sqlite_datareader = command.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    string myreader = sqlite_datareader.GetString(0);
                    Console.WriteLine("SQL_READSORGDATA [READ DATA] : " + myreader + " >>>> " + kamulozitvysledek);
                    vysledek = myreader;
                }
            }
            catch (SQLiteException myException)
            {
                Console.WriteLine("Message: " + myException.Message + "\n");

            }





            if (kamulozitvysledek == "pozadi")
            {
                pouzitepozadi = Int32.Parse(vysledek);
                FUNCTION_Changebackgroundcolor();
            }
            if (kamulozitvysledek == "popredi")
            {

                pouzitabarva = Int32.Parse(vysledek);
                FUNCTION_Changeforegroundcolor();
            }

            if (kamulozitvysledek == "selectedsearch")
            {
                Console.WriteLine("selectedsearch");
            }


            return vysledek;





        }





        

        public string SQL_READSOUTEZDATA(string sqltext, string kamulozitvysledek)
        {
            int _results_autoincrement = 0;
            double _results_scoreompare = 1000 * BIND_SQL_SOUTEZ_ROUNDS;

            Console.WriteLine("SQL_READSOUTEZDATA [SQL] : " + sqltext + " >>>> " + kamulozitvysledek);
            string vysledek = "";
            SQLiteCommand command = new SQLiteCommand(sqltext, DBSOUTEZ_Connection);

            SQLiteDataReader sqlite_datareader;
            try
            {
                sqlite_datareader = command.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    _results_autoincrement = _results_autoincrement + 1;

                    if (kamulozitvysledek == "get_players")
                    {
                        string jmeno = sqlite_datareader.GetString(1);
                        string prijmeni = sqlite_datareader.GetString(2);
                        string country = sqlite_datareader.GetString(3);
                        string agecat = sqlite_datareader.GetString(4);
                        string freq = sqlite_datareader.GetString(5);
                        int ch1 = sqlite_datareader.GetInt32 (6);
                        int ch2 = sqlite_datareader.GetInt32 (7);
                        string failic = sqlite_datareader.GetString(8);
                        string naclic = sqlite_datareader.GetString(9);
                        string club = sqlite_datareader.GetString(10);
                        string paid = sqlite_datareader.GetString(11);
                        int team = sqlite_datareader.GetInt32 (12);
                        int customagecat = sqlite_datareader.GetInt32 (12);

                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + jmeno + " >>>> " + kamulozitvysledek);
                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + prijmeni + " >>>> " + kamulozitvysledek);
                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + country + " >>>> " + kamulozitvysledek);

                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var directory = System.IO.Path.GetDirectoryName(path);


                        var player = new MODEL_Player()
                        {
                            ID = sqlite_datareader.GetInt32(0),
                            FIRSTNAME = jmeno,
                            LASTNAME = prijmeni,
                            COUNTRY = country,
                            AGECAT = agecat,
                            FAILIC = failic,
                            NACLIC = naclic,
                            FREQ = freq,
                            CH1 = ch1,
                            CH2 = ch2,
                            CLUB = club,
                            FLAG = directory + "/flags/" + country + ".png",
                            PAID = directory + "/flags/" + paid + ".png",
                            PAIDSTR = paid,
                            TEAM = team,
                            CUSTOMAGECAT = customagecat,
                            FREQID = int.Parse(sqlite_datareader.GetString(14))-1,
                            AGECATID = int.Parse(sqlite_datareader.GetString(15))

                        };
                        Players.Add(player);
                        vysledek = "get_players";

                    }


                    
                    if (kamulozitvysledek == "get_baseresults")
                    {


                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var directory = System.IO.Path.GetDirectoryName(path);



                    var _Players_Baseresults = new MODEL_Player_baseresults()
                        {
                            POSITION = _results_autoincrement,
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname")) + "  " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname")),
                            RAWSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalrawscore")),
                            PREPSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore")),
                        PREPSCOREDIFF = Math.Round(sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore")) - _results_scoreompare,2).ToString("0.00"),
                        RND1RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=1", ""),
                        RND1RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=1", ""),

                        RND2RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=2", ""),
                        RND2RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=2", ""),


                        RND3RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=3", ""),
                        RND3RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=3", ""),

                        RND4RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=4", ""),
                        RND4RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=4", ""),

                        RND5RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=5", ""),
                        RND5RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=5", ""),

                        RND6RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=6", ""),
                        RND6RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=6", ""),

                        FLAG = directory + "/flags/" + SQL_READSOUTEZDATA("select country from users where id = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), "") + ".png"

                        };

                        Console.WriteLine("AAAAAAAAAAAAAA" + SQL_READSOUTEZDATA("select prep from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=1", ""));

                        Players_Baseresults.Add(_Players_Baseresults);
                        _results_scoreompare = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore"));
                        vysledek = kamulozitvysledek;

                    }


                    if (kamulozitvysledek == "get_players_actual")
                    {


                        bool _REALPLAYER = true;
                        if (sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")) == 0)
                        {
                            _REALPLAYER = false;
                        }

                        var player_actual = new MODEL_Player_actual()
                        {
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")),
                            STARTPOINT = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("stp")),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname")) + "  " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname")) + Environment.NewLine  + "[" + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID"))  + "]"+  Environment.NewLine + Environment.NewLine + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("minutes")) + ":" + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("seconds")) + Environment.NewLine + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("landing")) + Environment.NewLine + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("height")),
                            RAWSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("RAWSCORE")),
                            ENTERED = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("entered")),
                            PREPSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("maxrow")),
                            REALPLAYER = _REALPLAYER

                        };
                        Players_Actual.Add(player_actual);

                        float _prep = sqlite_datareader.GetFloat(sqlite_datareader.GetOrdinal("maxrow"));
                        float _raw = sqlite_datareader.GetFloat(sqlite_datareader.GetOrdinal("RAWSCORE"));


                        SQL_SAVESOUTEZDATA("update score set raw=" + _raw .ToString(new CultureInfo("en-US")) + ", prep=" + _prep.ToString(new CultureInfo("en-US")) + " where userid="+ sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")) + " and rnd="+BIND_SELECTED_ROUND+" and grp="+BIND_SELECTED_GROUP+"  ");
                        vysledek = kamulozitvysledek;

                    }


                    if (kamulozitvysledek == "get_player_selected")
                    {

                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + sqltext + " >>>> " + kamulozitvysledek);

                        var _player_selected = new MODEL_Player_selected()
                        {
                            ID = sqlite_datareader.GetInt32(0),
                             FIRSTNAME = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname")),
                             LASTNAME = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname")),
                             WHOLENAME = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname")) + " " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname"))
                        };
                        Player_Selected.Add(_player_selected);
                        vysledek = kamulozitvysledek;

                    }


                    if (kamulozitvysledek == "get_rounds")
                    {


                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + sqltext + " >>>> " + kamulozitvysledek);



                        var rounds = new MODEL_Contest_Rounds()
                        {

                            ID = sqlite_datareader.GetInt32(0),
                            ROUNDNAME = sqlite_datareader.GetString(1),
                            ROUNDTYPE = sqlite_datareader.GetString(2),
                            ROUNDLENGHT = sqlite_datareader.GetInt32(3),
                            ROUNDZADANO = sqlite_datareader.GetInt32(4),
                            items = SQL__SUBQUERY_ADD_GROUPS(sqlite_datareader.GetInt32(0)),
                            ISSELECTED = "notselected"
                     

                        };
                        MODEL_CONTEST_ROUNDS.Add(rounds);
                        vysledek = kamulozitvysledek;
                    }

                    if (kamulozitvysledek == "get_groups")
                    {

                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + sqltext + " AA>>>> " + kamulozitvysledek);


                        var groups = new MODEL_Contest_Groups()
                        {
                            ID = sqlite_datareader.GetInt32(8),
                            GROUPNAME = sqlite_datareader.GetString(3),
                            GROUPTYPE= sqlite_datareader.GetString(4),
                            GROUPLENGHT= sqlite_datareader.GetInt32(5),
                            GROUPZADANO = sqlite_datareader.GetInt32(6),
                            
                        };
                        MODEL_CONTEST_GROUPS.Add(groups);
                        vysledek = kamulozitvysledek;
                    }


                    if (kamulozitvysledek == "get_teams")
                    {

                        string teamname = sqlite_datareader.GetString(1);
                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + teamname + " >>>> " + kamulozitvysledek);
                        var team = new MODEL_Team()
                        {
                            ID = sqlite_datareader.GetInt32(0),
                            TEAMNAME = teamname,
                            POCETCLENU = sqlite_datareader.GetInt32(2).ToString()
                        };
                        Teams.Add(team);
                        vysledek = kamulozitvysledek;
                    }



                    if (kamulozitvysledek == "get_usersinteam")
                    {

                        string username = sqlite_datareader.GetString(1);
                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + username + " >>>> " + kamulozitvysledek);
                        var usrsintm = new MODEL_usersinteam()
                        {
                            ID = sqlite_datareader.GetInt32(0),
                            FIRSTNAME  = sqlite_datareader.GetString(1),
                            LASTNAME = sqlite_datareader.GetString(2)
                        };
                        Usersinteams.Add(usrsintm);
                        vysledek = "get_usersinteam";
                    }

                    if (kamulozitvysledek == "get_usersnotinteam")
                    {

                        string username = sqlite_datareader.GetString(1);
                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + username + " >>>> " + kamulozitvysledek);
                        var usrsnotintm = new MODEL_usersnotinteam()
                        {
                            ID = sqlite_datareader.GetInt32(0),
                            FIRSTNAME = sqlite_datareader.GetString(1),
                            LASTNAME = sqlite_datareader.GetString(2)
                        };
                        UsersNOTinteams .Add(usrsnotintm);
                        vysledek = "get_usersnotinteam";
                    }



                    if (kamulozitvysledek == "")
                    {
                        try
                        {
                            Console.WriteLine("SQL RETURN TYPE:" + sqlite_datareader.GetFieldType(0));


                            if (sqlite_datareader.GetFieldType(0)==typeof(Int64))
                            {
                                long myreader = sqlite_datareader.GetInt64(0);
                                Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + myreader + " >>>> " + kamulozitvysledek);
                                vysledek = myreader.ToString();
                            }

                            if (sqlite_datareader.GetFieldType(0) == typeof(Double))
                            {
                                double myreader = sqlite_datareader.GetDouble(0);
                                Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + myreader + " >>>> " + kamulozitvysledek);
                                vysledek = myreader.ToString();
                            }



                            if (sqlite_datareader.GetFieldType(0)== typeof(string))
                            {
                                string myreader = sqlite_datareader.GetString(0);
                                Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + myreader + " >>>> " + kamulozitvysledek);
                                vysledek = myreader;
                            }



                        }
                        catch (Exception)
                        {
                            Console.Write("Invalid data type.");
                        }
                    }
                }
            }
            catch (SQLiteException myException)
            {
                Console.WriteLine("SQL_READSOUTEZDATA [ERROR] : " + myException.Message + "\n");
            }




            return vysledek;

      




        }




        public string SQL_READTMPDATA(string sqltext)
        {

            Console.WriteLine("SQL_READTMPZDATA [SQL] : " + sqltext);
            string vysledek = "";
            SQLiteCommand command = new SQLiteCommand(sqltext, TMP_Connection);
            SQLiteDataReader sqlite_datareader;
            try
            {
                sqlite_datareader = command.ExecuteReader();
                while (sqlite_datareader.Read())
                {

               
                        try
                        {
                            Console.WriteLine("SQL RETURN TYPE:" + sqlite_datareader.GetFieldType(0));


                            if (sqlite_datareader.GetFieldType(0) == typeof(Int64))
                            {
                                long myreader = sqlite_datareader.GetInt64(0);
                                Console.WriteLine("SQL_READTEMPDATA [READ DATA] : " + myreader );
                                vysledek = myreader.ToString();
                            }

                            if (sqlite_datareader.GetFieldType(0) == typeof(Double))
                            {
                                double myreader = sqlite_datareader.GetDouble(0);
                                Console.WriteLine("SQL_READTEMPDATA [READ DATA] : " + myreader );
                                vysledek = myreader.ToString();
                            }



                            if (sqlite_datareader.GetFieldType(0) == typeof(string))
                            {
                                string myreader = sqlite_datareader.GetString(0);
                                Console.WriteLine("SQL_READTEMPDATA [READ DATA] : " + myreader );
                                vysledek = myreader;
                            }



                        }
                        catch (Exception)
                        {
                            Console.Write("Invalid data type.");
                        }
                   
                }
            }
            catch (SQLiteException myException)
            {
                Console.WriteLine("SQL_READTEMPDATA [ERROR] : " + myException.Message + "\n");
            }




            return vysledek;






        }

        #endregion

        #region zmeny_barev_FNC
        public void FUNCTION_Changeforegroundcolor()
        {




            if (pouzitabarva == 23)
            {
                pouzitabarva = 0;
            }
            
ThemeManager.Current.ChangeTheme(System.Windows.Application.Current, pozadi[pouzitepozadi], barva[pouzitabarva]);
            SQL_SAVESORGDATA("update nastaveni set hodnota = " + pouzitabarva + " where polozka='popredi'");

        }

        public void clock_create()
        {
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }


        public void clock_start()
        {
            timer.Reset();
            dispatcherTimer.Start();
            timer.Start();
        }

        public void clock_stop()
        {
            dispatcherTimer.Stop ();
            timer.Stop ();
        }


        public void clock_pause()
        {
            if (timer.IsRunning) {
                dispatcherTimer.Stop();
                timer.Stop ();
            }
            else
            {
                dispatcherTimer.Start();
                timer.Start ();
            }
        }

        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            BIND_LETOVYCAS = Convert.ToSingle(timer.Elapsed.TotalSeconds);
        }



        public void FUNCTION_Changebackgroundcolor()
        {



            if (pouzitepozadi == 2)
            {
                pouzitepozadi = 0;
            }

            SQL_SAVESORGDATA("update nastaveni set hodnota = " + pouzitepozadi + " where polozka='pozadi'");
            ThemeManager.Current.ChangeTheme(System.Windows.Application.Current, pozadi[pouzitepozadi].ToString(), barva[pouzitabarva].ToString());

        }

        #endregion


        #region team_section

        public ObservableCollection<MODEL_Team> Teams { get; set; } = new ObservableCollection<MODEL_Team>();

        public void FUNCTION_TEAM_LOAD_TEAMS()
        {
            Teams.Clear();
            SQL_READSOUTEZDATA("select distinct id, name, (select count(id) from users where team= t.id ) pocet from teams T;", "get_teams");
            UsersNOTinteams.Clear();
            SQL_READSOUTEZDATA("select * from users U where U.team = 0 and U.id > 0;", "get_usersnotinteam");
        }



        public ObservableCollection<MODEL_usersinteam> Usersinteams { get; set; } = new ObservableCollection<MODEL_usersinteam>();

        public void FUNCTION_TEAM_SHOW_USERS_IN_TEAMS(int idteamu)
        {
            Usersinteams.Clear();
            SQL_READSOUTEZDATA("select * from users U where U.team = "+idteamu+ " and U.id > 0;", "get_usersinteam");

            UsersNOTinteams.Clear();
            SQL_READSOUTEZDATA("select * from users U  where U.team = 0 and U.id > 0;" , "get_usersnotinteam");


        }


        public ObservableCollection<MODEL_usersnotinteam> UsersNOTinteams { get; set; } = new ObservableCollection<MODEL_usersnotinteam>();





        public string BIND_kolikjelidivteamu(int idteamu)
        {
            //return SQL_READSOUTEZDATA("select count(userid) from users_in_teams where teamid = "+ idteamu + ";", "");
            return SQL_READSOUTEZDATA("select count(id) from users where team = " + idteamu + ";", "");

        }

        public void FUNCTION_team_move_user_into_team(int id_competitor, int id_new_team, int id_old_team)
        {
            //SQL_SAVESOUTEZDATA("update users_in_teams set teamid = "+id_new_team+" where userid = "+ id_competitor + ";");
            SQL_SAVESOUTEZDATA("update users set team = "+id_new_team+" where id = "+ id_competitor + ";");
            FUNCTION_TEAM_SHOW_USERS_IN_TEAMS(id_old_team);
        }

        public List<MatrixFiles> Listofmatrixes { get; } = new List<MatrixFiles>();
        public class MatrixFiles
        {
            public string Filename { get; set; }
            public string Autor { get; set; }
            public string Info { get; set; }

            public string all { get; set; }

        }



        public void FUNCTION_LOAD_MATRIX_FILES()
        {
            string[] mArrayOfflags = new string[300];
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Listofmatrixes.Clear();

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            mArrayOfflags = Directory.GetFiles(directory +  "/Matrix/", "*" + BIND_SQL_SOUTEZ_ROUNDS+ "_" + BIND_SQL_SOUTEZ_GROUPS + "_" + BIND_SQL_SOUTEZ_STARTPOINTS + "*.*", SearchOption.TopDirectoryOnly);
            Listofmatrixes.Add(new MatrixFiles() { Filename = "Náhodná rotace SORG AIR", Autor = "SORG AIR", Info = "---", all = "Náhodná rotace SORG AIR" });
            foreach (var file in mArrayOfflags)
            {
                FileInfo info = new FileInfo(file);
                string tmpautor = "autor";
                string tmpinfo = "info";
                Console.WriteLine(info.Name);
                string tmpall = info.Name + " | Score: " + tmpinfo + " | Autor: " + tmpautor;
                Listofmatrixes.Add(new MatrixFiles() { Filename = Path.GetFileNameWithoutExtension(info.Name), Autor = tmpautor, Info = tmpinfo, all = tmpall });
                //filewithmatrix.Items.Add(Path.GetFileNameWithoutExtension(info.Name));

            }
            Console.Write(mArrayOfflags.Length.ToString());
            OnPropertyChanged("Listofmatrixes");
            Listofmatrixes_selectedindex = 0;
            OnPropertyChanged("Listofmatrixes_selectedindex");
            //filewithmatrix.ItemsSource = Listofmatrixes;

        }


        public void FUNCTION_LOAD_CONTESTS_FILES()
        {


            MODEL_CONTESTS_FILES.Clear();
            Console.WriteLine("Searching contests");

            string[] mArrayOfcontests = new string[300];

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            mArrayOfcontests = Directory.GetFiles(directory + "/Data/", "*.db", SearchOption.TopDirectoryOnly);

            foreach (var file in mArrayOfcontests)
            {

                FileInfo info = new FileInfo(file);
                Console.WriteLine("File " + info.Name);
                TMP_Connection = new SQLiteConnection("Data Source=" + directory + "/Data/" + info.Name + ";");
                TMP_Connection.Open();

                var contests = new MODEL_Contests_files()
                        {

                            FILENAME = Path.GetFileNameWithoutExtension(info.Name),
                             CATEGORY = SQL_READTMPDATA("select value from contest where item = 'Category'"),
                              NAME = SQL_READTMPDATA("select value from contest where item = 'Name'"),
                              LOCATION = SQL_READTMPDATA("select value from contest where item = 'Location'"),
                               DATE = SQL_READTMPDATA("select value from contest where item = 'Date'")
                        };
                        MODEL_CONTESTS_FILES.Add(contests);



                TMP_Connection.Close();



            }
            OnPropertyChanged("MODEL_CONTESTS_FILES");
            //filewithmatrix.ItemsSource = Listofmatrixes;

        }





        public void FUNCTION_team_rename(string name, int id_team)
        {
            SQL_SAVESOUTEZDATA("update teams set name = '" + name + "' where id = " + id_team + ";");
            FUNCTION_TEAM_LOAD_TEAMS();
        }

        public void FUNCTION_team_create(string name)
        {
            SQL_SAVESOUTEZDATA("insert into teams (name) values ('" + name + "');");
            FUNCTION_TEAM_LOAD_TEAMS();
        }


        public void FUNCTION_team_delete(int id_team)
        {
            //SQL_SAVESOUTEZDATA("update users_in_teams set teamid = 0 where teamid= " + id_team + ";");
            SQL_SAVESOUTEZDATA("update users set team = 0 where team= " + id_team + ";");
            SQL_SAVESOUTEZDATA("delete from teams where id='" + id_team  + "';");
            FUNCTION_TEAM_LOAD_TEAMS();
            FUNCTION_TEAM_SHOW_USERS_IN_TEAMS(9999);
            
        }



        #endregion





        public ObservableCollection<DataObject> testdata { get; set; } = new ObservableCollection<DataObject>();


        #region Players
        public ObservableCollection<MODEL_Player> Players { get; set; } = new ObservableCollection<MODEL_Player>();
        public ObservableCollection<MODEL_Player_actual> Players_Actual { get; set; } = new ObservableCollection<MODEL_Player_actual>();
        public ObservableCollection<MODEL_Player_selected> Player_Selected { get; set; } = new ObservableCollection<MODEL_Player_selected>();
        public ObservableCollection<MODEL_Player_baseresults> Players_Baseresults { get; set; } = new ObservableCollection<MODEL_Player_baseresults>();

        public void FUNCTION_USERS_LOAD_ALLCOMPETITORS()
        {
            Players.Clear();
            SQL_READSOUTEZDATA("select ID, Firstname, Lastname, Country,(select name from Agecategories A  where A.id=U.Agecat) Agecat, (select name from Frequencies F  where F.id=U.Freq) Freq, Ch1, Ch2, Failic, Naclic, Club, Paid, Team, Customagecat, U.Freq Freqid, U.Agecat agecatid from users U where id > 0; ", "get_players");
            BIND_POCETSOUTEZICICHMENU = SQL_READSOUTEZDATA("select count(id) pocet from users where id > 0", "");
            BIND_POCETSOUTEZICICH = Int32.Parse(SQL_READSOUTEZDATA("select count(id) pocet from users where id > 0", ""));
            
        }


        public int _Listofmatrixes_selectedindex = 0;

        public int Listofmatrixes_selectedindex
        {
            get { return _Listofmatrixes_selectedindex; }
            set { _Listofmatrixes_selectedindex = value; OnPropertyChanged(nameof(Listofmatrixes_selectedindex)); }

        }

        public bool _BIND_SCOREENTRY_OPEN = false; 
        public bool  BIND_SCOREENTRY_OPEN
        {
            get { return _BIND_SCOREENTRY_OPEN; }
            set { _BIND_SCOREENTRY_OPEN = value; OnPropertyChanged(nameof(BIND_SCOREENTRY_OPEN)); }
        }




        public int _bind_scoreentry_selected_minute;
        public int bind_scoreentry_selected_minute
        {
            get { return _bind_scoreentry_selected_minute; }
            set { _bind_scoreentry_selected_minute = value; OnPropertyChanged(nameof(bind_scoreentry_selected_minute)); }
        }

        public int _bind_scoreentry_selected_second;
        public int bind_scoreentry_selected_second
        {
            get { return _bind_scoreentry_selected_second; }
            set { _bind_scoreentry_selected_second = value; OnPropertyChanged(nameof(bind_scoreentry_selected_second)); }
        }


        public int _bind_scoreentry_selected_landing;
        public int bind_scoreentry_selected_landing
        {
            get { return _bind_scoreentry_selected_landing; }
            set { _bind_scoreentry_selected_landing = value; OnPropertyChanged(nameof(bind_scoreentry_selected_landing)); }
        }

        public int _bind_scoreentry_selected_height;
        public int bind_scoreentry_selected_height
        {
            get { return _bind_scoreentry_selected_height; }
            set { _bind_scoreentry_selected_height = value; OnPropertyChanged(nameof(bind_scoreentry_selected_height)); }
        }
        public void FUNCTION_SCOREENTRY_LOAD_USERDATA(int rnd,int grp, int stp)
        {

            Player_Selected.Clear();

            if (rnd == 0) { rnd = BIND_SELECTED_ROUND; }
                if (grp == 0) { grp = BIND_SELECTED_GROUP; }
                if (stp == 0) { stp = BIND_SELECTED_STARTPOINT; }
    
                SQL_READSOUTEZDATA("select U.ID,M.stp,U.Firstname,U.Lastname from matrix M left join users U on M.user = U.id where M.rnd = " + rnd + " and M.grp = " + grp + " and M.stp = " + stp + " order by stp asc;", "get_player_selected");
            bind_scoreentry_selected_minute = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(minutes) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select minutes from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));
            bind_scoreentry_selected_second = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(seconds) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select seconds from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));
            bind_scoreentry_selected_landing = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(landing) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select landing from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));
            bind_scoreentry_selected_height = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(height) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select height from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));

        }


        public void FUNCTION_SCOREENTRY_SAVE_SCORE(int rnd, int grp, int stp, int usrid, int minutes, int seconds, int landing, int height, int heightunder, int heightover, int pen1, int pen2,string  rawscore, string prepscore )
        {
            Console.WriteLine("saving score");
            SQL_SAVESOUTEZDATA("delete from score where rnd="+rnd+" and grp="+grp +" and stp="+stp +";");
            SQL_SAVESOUTEZDATA("insert INTO score (rnd, grp, stp, userid, minutes, seconds, landing, height, heightunder, heightover, pen1, pen2, raw, prep, entered) VALUES("+rnd+ "," + grp + "," + stp  + "," + usrid  + "," + minutes  + "," + seconds + "," + landing  + "," + height +  ", " + heightunder  + "," + heightover + ", " + pen1  + "," + pen2 + ",'" + rawscore + "','" + prepscore  + "', 'True');");
        }


        public void FUNCTION_CHECK_ENTERED(int rnd, int grp)
        {
            string tmp_celkem_vgroupe = SQL_READSOUTEZDATA("select (select count(entered) from score where rnd=" + rnd + " and grp = " + grp + ") celkem", "");
            string tmp_zadano_vgroupe = SQL_READSOUTEZDATA("select (select count(entered) from score where rnd=" + rnd + " and grp = " + grp + " and entered = 'True') zadano", "");

            if (Int32.Parse(tmp_zadano_vgroupe) == 0) { SQL_SAVESOUTEZDATA("update groups set zadano = '0' where masterround=" + rnd + " and groupnumber=" + grp); }
            if (Int32.Parse(tmp_zadano_vgroupe) < Int32.Parse(tmp_celkem_vgroupe)) {SQL_SAVESOUTEZDATA("update groups set zadano = '1' where masterround=" + rnd + " and groupnumber=" + grp);}
            if (Int32.Parse(tmp_zadano_vgroupe) == Int32.Parse(tmp_celkem_vgroupe)) { SQL_SAVESOUTEZDATA("update groups set zadano = '2' where masterround=" + rnd + " and groupnumber=" + grp); }


            string tmp_celkem_vkole = SQL_READSOUTEZDATA("select (select count(zadano) from groups where masterround=" + rnd + ") celkem", "");
            string tmp_zadano_vkole = SQL_READSOUTEZDATA("select (select count(zadano) from groups where masterround=" + rnd + " and zadano = '2') zadano", "");
            string tmp_castecne_zadano_vkole = SQL_READSOUTEZDATA("select (select count(zadano) from groups where masterround=" + rnd + " and zadano = '1') castecnezadano", "");

            if (Int32.Parse(tmp_zadano_vkole) == 0) { SQL_SAVESOUTEZDATA("update rounds set zadano = '0' where id=" + rnd ); }
            if (Int32.Parse(tmp_zadano_vkole) < Int32.Parse(tmp_celkem_vkole)) { SQL_SAVESOUTEZDATA("update rounds set zadano = '1' where id=" + rnd); }
            if (Int32.Parse(tmp_zadano_vkole) == Int32.Parse(tmp_celkem_vkole)) { SQL_SAVESOUTEZDATA("update rounds set zadano = '2' where id=" + rnd ); }

            FUNCTION_ROUNDS_LOAD_ROUNDS();
            FUNCTION_ROUNDS_LOAD_GROUPS(BIND_SELECTED_ROUND_value);
        }






        public void FUNCTION_USERS_CREATE_NEW(string firstname, string lastname, string country , int agecat, int freq, int chanel1, int chanel2, string failic, string naclic , string club, bool registered, int team, int customagecat )
        {

            SQL_SAVESOUTEZDATA("insert into users values (null,'"+ firstname + "', '" + lastname  + "', '" + country  + "', '" + agecat  + "', '" + freq  + "', '" + chanel1  + "', '" + chanel2 + "' , '" + failic + "', '" + naclic + "', '" + club + "' , '" + registered + "', '" + team + "', '" + customagecat + "' , 0 );");
            Players.Clear();
            SQL_READSOUTEZDATA("select ID, Firstname, Lastname, Country,(select name from Agecategories A  where A.id=U.Agecat) Agecat, (select name from Frequencies F  where F.id=U.Freq) Freq, Ch1, Ch2, Failic, Naclic, Club, Paid, Team, Customagecat, U.Freq Freqid, U.Agecat agecatid from users U where id > 0; ", "get_players");
            BIND_POCETSOUTEZICICHMENU = SQL_READSOUTEZDATA("select count(id) pocet from users where id > 0", "");
            BIND_POCETSOUTEZICICH = Int32.Parse(SQL_READSOUTEZDATA("select count(id) pocet from users where id > 0", ""));

        }

        public void FUNCTION_USERS_CREATE_EDIT(int ID, string firstname, string lastname, string country, int agecat, int freq, int chanel1, int chanel2, string failic, string naclic, string club, bool paid, int customagecat)
        {

            SQL_SAVESOUTEZDATA("update users set Firstname='" + firstname + "', Lastname='" + lastname + "', Country='" + country + "', Agecat='" + agecat + "', Freq='" + freq + "', Ch1='" + chanel1 + "', Ch2='" + chanel2 + "' , Failic='" + failic + "', Naclic='" + naclic + "', Club='" + club + "' , Customagecat='" + customagecat + "' , paid='"+paid+"' where ID="+ID+" ;");
            Players.Clear();
            SQL_READSOUTEZDATA("select ID, Firstname, Lastname, Country,(select name from Agecategories A  where A.id=U.Agecat) Agecat, (select name from Frequencies F  where F.id=U.Freq) Freq, Ch1, Ch2, Failic, Naclic, Club, Paid, Team, Customagecat, U.Freq Freqid, U.Agecat agecatid from users U where id > 0; ", "get_players");
            BIND_POCETSOUTEZICICHMENU = SQL_READSOUTEZDATA("select count(id) pocet from users where id > 0", "");
            BIND_POCETSOUTEZICICH = Int32.Parse(SQL_READSOUTEZDATA("select count(id) pocet from users where id > 0", ""));

        }


        public void FUNCTION_USERS_DELETE_COMPETITOR(int idsouteziciho)
        {
            SQL_READSOUTEZDATA("delete from users where id="+idsouteziciho +"", "");
            SQL_READSOUTEZDATA("update matrix set user=0 where user=" + idsouteziciho + "", "");
            SQL_READSOUTEZDATA("delete from score where userid=" + idsouteziciho + "", "");
            Players.Clear();
            SQL_READSOUTEZDATA("select ID, Firstname, Lastname, Country,(select name from Agecategories A  where A.id=U.Agecat) Agecat, (select name from Frequencies F  where F.id=U.Freq) Freq, Ch1, Ch2, Failic, Naclic, Club, Paid, Team, Customagecat, U.Freq Freqid, U.Agecat agecatid from users U where id > 0; ", "get_players");
            BIND_POCETSOUTEZICICHMENU = SQL_READSOUTEZDATA("select count(id) pocet from users where id > 0", "");
            BIND_POCETSOUTEZICICH = Int32.Parse(SQL_READSOUTEZDATA("select count(id) pocet from users where id > 0", ""));

        }


        public ObservableCollection<MODEL_Contests_files> MODEL_CONTESTS_FILES { get; set; } = new ObservableCollection<MODEL_Contests_files>();

        public ObservableCollection<MODEL_Contest_Rounds> MODEL_CONTEST_ROUNDS { get; set; } = new ObservableCollection<MODEL_Contest_Rounds>();
        public ObservableCollection<MODEL_Contest_Groups> MODEL_CONTEST_GROUPS { get; set; } = new ObservableCollection<MODEL_Contest_Groups>();

        public ObservableCollection<MODEL_Player_flags> MODEL_Contest_FLAGS { get; set; } = new ObservableCollection<MODEL_Player_flags>();

        public ObservableCollection<MODEL_Player_agecategories> MODEL_Contest_AGECATEGORIES { get; set; } = new ObservableCollection<MODEL_Player_agecategories>();
        public ObservableCollection<MODEL_Player_frequencies> MODEL_Contest_FREQUENCIES { get; set; } = new ObservableCollection<MODEL_Player_frequencies>();

        public void FUNCTION_LOAD_DEFAULT_ROUNDSANDGROUPS()
        {
            BIND_SELECTED_ROUND = 1;
            BIND_SELECTED_GROUP = 1;
            FUNCTION_SELECTED_ROUND_USERS(0, 0);

            BIND_VIEWED_ROUND = 1;
            BIND_VIEWED_GROUP = 1;
            FUNCTION_ROUNDS_LOAD_GROUPS(BIND_VIEWED_ROUND);
            MODEL_CONTEST_ROUNDS[0].ISSELECTED = "selected";
            MODEL_CONTEST_GROUPS[0].ISSELECTED = "selected";
            BIND_NEXTROUND_TEXT = "Vybrat další let : 1 / 2";
            BIND_PREWROUND_TEXT = "Žádný předchozí let neexistuje";


        }



        public void FUNCTION_ROUNDS_LOAD_ROUNDS()
        {
            MODEL_CONTEST_ROUNDS.Clear();
            SQL_READSOUTEZDATA("select * from rounds;", "get_rounds");


        }


        public void FUNCTION_ROUNDS_LOAD_GROUPS(int  kolo)
        {
            MODEL_CONTEST_GROUPS.Clear();
            SQL_READSOUTEZDATA("select rnd,grp,g.* from matrix M left join groups G on M.rnd = G.masterround where M.rnd=" + kolo + " group by groupnumber;", "get_groups");
        }


        public void FUNCTION_SELECTED_ROUND_USERS(int rnd, int grp)
        {
            if (rnd==0) { rnd = BIND_SELECTED_ROUND; }
            if (grp== 0) { grp = BIND_SELECTED_GROUP; }
            Players_Actual.Clear();
            SQL_READSOUTEZDATA("select ifnull(round((ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*(select heightunder from rules)) -(heightover*(select heightover from rules)) ),0)) / (select max(ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*(select heightunder from rules)) -(heightover*(select heightover from rules)) ),0)) FROM score s where s.rnd = " + rnd + " and s.grp = " + grp + ")*1000,2),round(0,2)) maxrow , ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*0.5) -(heightover*3) ),0) RAWSCORE, U.ID,S.stp,U.Firstname,U.Lastname, ifnull(s.minutes,0) minutes, ifnull(s.seconds,0) seconds, ifnull(s.landing,0) landing, ifnull(s.height,0) height, ifnull(s.pen1,0) pen1, ifnull(s.pen2,0) pen2, ifnull(s.raw,0) war, ifnull(s.prep,0) prep, ifnull(s.entered,'False') entered from score S left join users U on S.userid = U.id where  s.rnd = " + rnd + " and s.grp = " + grp + " order by s.stp asc; ", "get_players_actual");
            //SQL_READSOUTEZDATA("select round((ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*(select heightunder from rules)) -(heightover*(select heightover from rules)) ),0)) / (select max(ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*(select heightunder from rules)) -(heightover*(select heightover from rules)) ),0)) FROM score s where s.rnd = " + rnd + " and s.grp = " + grp + ")*1000,2) maxrow , ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*0.5) -(heightover*3) ),0) RAWSCORE, U.ID,S.stp,U.Firstname,U.Lastname, ifnull(s.minutes,0) minutes, ifnull(s.seconds,0) seconds, ifnull(s.landing,0) landing, ifnull(s.height,0) height, ifnull(s.pen1,0) pen1, ifnull(s.pen2,0) pen2, ifnull(s.raw,0) war, ifnull(s.prep,0) prep, ifnull(s.entered,'False') entered from score S left join users U on S.userid = U.id where  s.rnd = " + rnd + " and s.grp = " + grp + " order by s.stp asc;", "get_players_actual");

        }


        public void FUNCTION_RESULTS_LOADBASERESULTS()
        {
            Players_Baseresults.Clear();
            SQL_READSOUTEZDATA("select (select sum(prep) from score s2 where s2.userid = s1.userid ) overalscore, (select sum(raw) from score s2 where s2.userid = s1.userid ) overalrawscore , s1.*,u.* from score s1 left join users U on S1.userid = U.id group by userid order by overalscore desc", "get_baseresults");
            //SQL_READSOUTEZDATA("select round((ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*(select heightunder from rules)) -(heightover*(select heightover from rules)) ),0)) / (select max(ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*(select heightunder from rules)) -(heightover*(select heightover from rules)) ),0)) FROM score s where s.rnd = " + rnd + " and s.grp = " + grp + ")*1000,2) maxrow , ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*0.5) -(heightover*3) ),0) RAWSCORE, U.ID,S.stp,U.Firstname,U.Lastname, ifnull(s.minutes,0) minutes, ifnull(s.seconds,0) seconds, ifnull(s.landing,0) landing, ifnull(s.height,0) height, ifnull(s.pen1,0) pen1, ifnull(s.pen2,0) pen2, ifnull(s.raw,0) war, ifnull(s.prep,0) prep, ifnull(s.entered,'False') entered from score S left join users U on S.userid = U.id where  s.rnd = " + rnd + " and s.grp = " + grp + " order by s.stp asc;", "get_players_actual");

        }



        public void FUNCTION_COMPETITOR_UPDATE(string what, string value, int competiroid)
        {
            SQL_SAVESOUTEZDATA("update users set "+ what +" = '"+ value +"' where ID="+competiroid);

        }

        #endregion


        private string _scoreentrytype;
        public string ScoreEntryType
        {
            get { return _scoreentrytype; }
            set { _scoreentrytype = value; OnPropertyChanged(nameof(ScoreEntryType)); }
        }





        public List<Timer_minutes_seconds> BINDING_Timer_listofminutes { get; } = new List<Timer_minutes_seconds>();
        public List<Timer_minutes_seconds> BINDING_Timer_listofseconds { get; } = new List<Timer_minutes_seconds>();
        public List<Timer_minutes_seconds> BINDING_Timer_listofheights { get; } = new List<Timer_minutes_seconds>();
        public List<Timer_minutes_seconds> BINDING_Timer_listoflandings { get; } = new List<Timer_minutes_seconds>();
        public class Timer_minutes_seconds
        {
            public int Value { get; set; }
            public string Text { get; set; }

        }

        public void FUNCTION_LOAD_TIMERS_MINUTES()
        {
            Console.WriteLine("MINUTES");
            BINDING_Timer_listofminutes.Clear();

            for (int i = 0; i < 16; i++)
            {
                BINDING_Timer_listofminutes.Add(new Timer_minutes_seconds() { Value = i, Text = i+" Minut" });
            }

        }


        public void FUNCTION_LOAD_TIMERS_SECONDS()
        {
            Console.WriteLine("SECONDS");
            BINDING_Timer_listofseconds.Clear();

            for (int i = 0; i < 60; i++)
            {
                BINDING_Timer_listofseconds.Add(new Timer_minutes_seconds() { Value = i, Text = i+" vteřin" });
            }

        }


        public void FUNCTION_LOAD_TIMERS_HEIGHT()
        {
            Console.WriteLine("HEIGHT");
            BINDING_Timer_listofheights.Clear();

            for (int i = 0; i < 1000; i++)
            {
                BINDING_Timer_listofheights.Add(new Timer_minutes_seconds() { Value = i, Text = i + " metrů" });
            }

        }



        public void FUNCTION_LOAD_TIMERS_LANDINGS()
        {
            Console.WriteLine("HEIGHT");
            BINDING_Timer_listoflandings.Clear();

            for (int i = 0; i < 101; i++)
            {
                BINDING_Timer_listoflandings.Add(new Timer_minutes_seconds() { Value = i, Text = i + " bodů" });
            }

        }




        public void FUNCTION_MOVE_GROUP_UP_DOWN(int posun)
        {



            int _tmp_newgroup = BIND_SELECTED_GROUP + posun;
            int _tmp_newround = BIND_SELECTED_ROUND;

            if (_tmp_newgroup > BIND_SQL_SOUTEZ_GROUPS) { _tmp_newgroup = 1; _tmp_newround += 1; }


            if (_tmp_newgroup <= 0) { _tmp_newgroup = BIND_SQL_SOUTEZ_GROUPS; _tmp_newround -= 1; }



            int _tmp_selected_group_up = _tmp_newgroup + 1;
            int _tmp_selected_round_up = _tmp_newround;
            int _tmp_selected_group_down = _tmp_newgroup - 1;
            int _tmp_selected_round_down = _tmp_newround;


            if (_tmp_selected_group_up > BIND_SQL_SOUTEZ_GROUPS) { _tmp_selected_group_up = 1; _tmp_selected_round_up += 1; }
            if (_tmp_selected_group_down < 1) { _tmp_selected_group_down = BIND_SQL_SOUTEZ_GROUPS; _tmp_selected_round_down -= 1; }





            BIND_PREWROUND_TEXT = "Předchozí let : " + _tmp_selected_round_down + " / " + _tmp_selected_group_down;

            if (_tmp_newround < BIND_SQL_SOUTEZ_ROUNDS + 1 && _tmp_newround > 0)
            {
                BIND_SELECTED_GROUP = _tmp_newgroup;
                BIND_SELECTED_ROUND = _tmp_newround;

                FUNCTION_SELECTED_ROUND_USERS(0, 0);
                FUNCTION_ROUNDS_LOAD_GROUPS(BIND_SELECTED_ROUND);
            }



            if (_tmp_selected_round_up > BIND_SQL_SOUTEZ_ROUNDS)
            {
                BIND_NEXTROUND_TEXT = "Žádný další let neexistuje";

                if (_tmp_selected_round_up == BIND_SQL_SOUTEZ_ROUNDS + 1)
                {
                   BIND_PREWROUND_TEXT = "Předchozí let : " + BIND_SQL_SOUTEZ_ROUNDS + " / " + (BIND_SQL_SOUTEZ_GROUPS - 1);
                }

            }
            else
            {
                BIND_NEXTROUND_TEXT = "Další let : " + _tmp_selected_round_up + " / " + _tmp_selected_group_up;
            }

            Console.WriteLine("_tmp_selected_round_down" + _tmp_selected_round_down);
            Console.WriteLine("_tmp_newgroup" + _tmp_newgroup);
            Console.WriteLine("_tmp_selected_group_down" + _tmp_selected_group_down);




            if (_tmp_selected_round_down < 1)
            {
                BIND_PREWROUND_TEXT = "Žádný předchozí let neexistuje";

                if (_tmp_selected_round_down == 0)
                {
                    BIND_NEXTROUND_TEXT = "Další let : 1 / 2";
                }
            }








            for (int i = 0; i < MODEL_CONTEST_GROUPS.Count; i++)
            {
                MODEL_CONTEST_GROUPS[i].ISSELECTED = "---";
            }
            MODEL_CONTEST_GROUPS[BIND_SELECTED_GROUP - 1].ISSELECTED = "selected";


            for (int i = 0; i < MODEL_CONTEST_ROUNDS.Count; i++)
            {
                MODEL_CONTEST_ROUNDS[i].ISSELECTED = "---";
            }
            MODEL_CONTEST_ROUNDS[BIND_SELECTED_ROUND - 1].ISSELECTED = "selected";


            BIND_VIEWED_ROUND = _tmp_newround;
            BIND_VIEWED_GROUP = _tmp_newgroup;

        }




    }
}
