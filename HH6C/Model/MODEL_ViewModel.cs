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


namespace WpfApp6.Model
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    /// 
    public class MODEL_ViewModel : INotifyPropertyChanged
    {
        SQLiteConnection DBSORG_Connection;
        SQLiteConnection DBSOUTEZ_Connection;
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
        public int BIND_SQL_SOUTEZ_ROUNDS_value;
        public int BIND_SQL_SOUTEZ_GROUPS_value;
        public int BIND_SQL_SOUTEZ_STARTPOINTS_value;
        public int BIND_SQL_SOUTEZ_DELETES_value;
        public int BIND_SQL_SOUTEZ_ROUNDSFINALE_value;
        public int BIND_SQL_SOUTEZ_STARTPOINTSFINALE_value;
        public int BIND_SQL_SOUTEZ_DELETESFINALE_value;
        public string BIND_VYBRANEKOLOMENU_value = "Vybrané kolo : 0/0";
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


        public int BIND_SELECTED_ROUND_value;
        public int BIND_SELECTED_GROUP_value;

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

        public void FUNCTION_LOADCONTEST()
        {



            mArrayOfflags = Directory.GetFiles(@"E:\SORGAIR\SORGAIR\HH6C\bin\Debug\flags\", "*.*", SearchOption.TopDirectoryOnly);
            foreach (var file in mArrayOfflags)
            {
                FileInfo info = new FileInfo(file);
                var players_flags = new MODEL_Player_flags()
                { FILENAME = Path.GetFileNameWithoutExtension(info.Name) };
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
            BIND_SQL_SOUTEZ_STARTPOINTS = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Startpoints'", ""));
            BIND_SQL_SOUTEZ_DELETES = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Deletes'", ""));
            BIND_SQL_SOUTEZ_ROUNDSFINALE = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Roundsfinale'", ""));
            BIND_SQL_SOUTEZ_STARTPOINTSFINALE = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Startpointsfinale'", ""));
            BIND_SQL_SOUTEZ_DELETESFINALE = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Deletesfinale'", ""));
            BIND_CONTESTBEGIN = SQL_READSOUTEZDATA("select value from contest where item='contestbegin'", "");
            BIND_USEAUDIO = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='useaudio'", ""));
            BIND_SQL_SOUTEZ_ENTRYSTYLE  = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Entrystyle'", ""));
            BIND_SQL_SOUTEZ_ENTRYSTYLENEXT = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Entrystylenext'", ""));
            BIND_SQL_AUDIO_COMPNUMBERS = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Audiocumpetitornumber'", ""));
            BIND_SQL_AUDIO_RNDGRPFLIGHT = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Rndgrpflight'", ""));
            BIND_SQL_AUDIO_RNDGRPPREP = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Rndgrpprep'", ""));
            BIND_SQL_AUTO_USEPREPTIME  = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Usepreptime'", ""));
            BIND_SQL_AUTO_RUNPREPTIMENEXTROUND  = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Runnextroundafterpreptime'", ""));
            BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME   = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Runpreptime'", ""));
            BIND_SQL_AUTO_PREPTIMELENGHT = SQL_READSOUTEZDATA("select value from contest where item='Preptimelenght'", "");
            BIND_SQL_AUTO_PREPTIMESTART = SQL_READSOUTEZDATA("select value from contest where item='Preptimestart'", "");



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
                return "Letový čas : " + elapsed.ToString("mm':'ss':'ff") + " (zbývá : "+ rozdil.ToString("mm':'ss':'ff")+")";
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
            set { Console.WriteLine(value); BIND_FLAG_value = @"E:\SORGAIR\SORGAIR\HH6C\bin\Debug\flags\" + value + ".png"; OnPropertyChanged("BIND_FLAG"); }
        }

        public string BIND_PAID
        {
            get { return BIND_PAID_value; }
            set { Console.WriteLine(value); BIND_PAID_value =value; OnPropertyChanged("BIND_PAID");}
            
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
            get { return "Vybrané kolo:" + BIND_SELECTED_ROUND + "/" +  BIND_SELECTED_GROUP; }
        }


        public string BIND_POCETSOUTEZICICHMENU
        {
            get { return "Soutěžící [" + BIND_POCETSOUTEZICICHMENU_value + "]"; }
            set { BIND_POCETSOUTEZICICHMENU_value = value; OnPropertyChanged("BIND_POCETSOUTEZICICHMENU"); }
        }


        public int BIND_POCETSOUTEZICICH
        {
            get { return BIND_POCETSOUTEZICICH_value ; }
            set { BIND_POCETSOUTEZICICH_value = value; OnPropertyChanged("BIND_POCETSOUTEZICICH"); }
        }


        public int BIND_SQL_SOUTEZ_ROUNDS
        {
            get { return BIND_SQL_SOUTEZ_ROUNDS_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Rounds'"); BIND_SQL_SOUTEZ_ROUNDS_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_ROUNDS"); }
        }


        public int BIND_SELECTED_ROUND
        {
            get { return BIND_SELECTED_ROUND_value; }
            set { BIND_SELECTED_ROUND_value = value; OnPropertyChanged("BIND_SELECTED_ROUND");OnPropertyChanged("BIND_VYBRANEKOLOMENU");Console.WriteLine("BIND_SELECTED_ROUND:" + BIND_SELECTED_ROUND); }
        }

        public int BIND_SELECTED_GROUP
        {
            get { return BIND_SELECTED_GROUP_value; }
            set { BIND_SELECTED_GROUP_value = value; OnPropertyChanged("BIND_SELECTED_GROUP"); OnPropertyChanged("BIND_VYBRANEKOLOMENU"); Console.WriteLine("BIND_SELECTED_GROUP" + BIND_SELECTED_GROUP); }
        }



        public int BIND_VIEWED_ROUND
        {
            get { return BIND_VIEWED_ROUND_value; }
            set { BIND_VIEWED_ROUND_value = value; OnPropertyChanged("BIND_VIEWED_ROUND"); Console.WriteLine("BIND_VIEWED_ROUND:" + BIND_VIEWED_ROUND); }
        }

        public int BIND_VIEWED_GROUP
        {
            get { return BIND_VIEWED_GROUP_value; }
            set { BIND_VIEWED_GROUP_value = value; OnPropertyChanged("BIND_VIEWED_GROUP");  Console.WriteLine("BIND_VIEWED_GROUP" + BIND_VIEWED_GROUP); }
        }



        public int BIND_SQL_SOUTEZ_GROUPS
        {
            get { return BIND_SQL_SOUTEZ_GROUPS_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Groups'"); BIND_SQL_SOUTEZ_GROUPS_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_GROUPS"); }
        }

        public int BIND_SQL_SOUTEZ_STARTPOINTS
        {
            get { return BIND_SQL_SOUTEZ_STARTPOINTS_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Startpoints'"); BIND_SQL_SOUTEZ_STARTPOINTS_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_STARTPOINTS"); }
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

        public bool  BIND_SQL_SOUTEZ_ENTRYSTYLE
        {
            get { return BIND_SQL_SOUTEZ_ENTRYSTYLE_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Entrystyle'"); BIND_SQL_SOUTEZ_ENTRYSTYLE_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_ENTRYSTYLE"); }
        }

        public bool BIND_SQL_SOUTEZ_ENTRYSTYLENEXT
        {
            get { return BIND_SQL_SOUTEZ_ENTRYSTYLENEXT_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Entrystylenext'"); BIND_SQL_SOUTEZ_ENTRYSTYLENEXT_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_ENTRYSTYLENEXT"); }
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
                DBSORG_Connection = new SQLiteConnection("Data Source=" + directory + "/db/data.db;");
                DBSORG_Connection.Open();

            }

            if (KTERADB == "SOUTEZ")
            {
                DBSOUTEZ_Connection = new SQLiteConnection("Data Source=" + directory + "/db/soutez.db;");
                DBSOUTEZ_Connection.Open();
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

            vysledek = "";




        }





        

        public string SQL_READSOUTEZDATA(string sqltext, string kamulozitvysledek)
        {

            Console.WriteLine("SQL_READSOUTEZDATA [SQL] : " + sqltext + " >>>> " + kamulozitvysledek);
            string vysledek = "";
            SQLiteCommand command = new SQLiteCommand(sqltext, DBSOUTEZ_Connection);

            SQLiteDataReader sqlite_datareader;
            try
            {
                sqlite_datareader = command.ExecuteReader();
                while (sqlite_datareader.Read())
                {

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
                            FLAG = @"E:\SORGAIR\SORGAIR\HH6C\bin\Debug\flags\" + country + ".png"
                        ,
                            PAID = @"E:\SORGAIR\SORGAIR\HH6C\bin\Debug\flags\" + paid + ".png",
                            PAIDSTR = paid,
                            TEAM = team,
                            CUSTOMAGECAT = customagecat
                        };
                        Players.Add(player);
                        vysledek = "get_players";

                    }


                    if (kamulozitvysledek == "get_players_actual")
                    {

                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + sqltext + " >>>> " + kamulozitvysledek);

                        var player_actual = new MODEL_Player_actual()
                        {
                            ID = sqlite_datareader.GetInt32(0),
                            STARTPOINT = sqlite_datareader.GetInt32(1),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname")) + "  " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname")),
                            RAWSCORE = 145,
                            PREPSCORE = 988
                        };
                        Players_Actual.Add(player_actual);
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
                            ROUNDZADANO = sqlite_datareader.GetInt32(4)
                        };
                        MODEL_CONTEST_ROUNDS.Add(rounds);
                        vysledek = kamulozitvysledek;
                    }

                    if (kamulozitvysledek == "get_groups")
                    {

                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + sqltext + " >>>> " + kamulozitvysledek);


                        var groups = new MODEL_Contest_Groups()
                        {
                            ID = sqlite_datareader.GetInt32(3),
                            GROUPNAME = sqlite_datareader.GetString(4),
                            GROUPTYPE= sqlite_datareader.GetString(5),
                            GROUPLENGHT= sqlite_datareader.GetInt32(6),
                            GROUPZADANO = sqlite_datareader.GetInt32(7)
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

            vysledek = "";




        }

        #endregion

        #region zmeny_barev_FNC
        public void FUNCTION_Changeforegroundcolor()
        {




            if (pouzitabarva == 23)
            {
                pouzitabarva = 0;
            }


            MahApps.Metro.ThemeManager.ChangeTheme(System.Windows.Application.Current, pozadi[pouzitepozadi], barva[pouzitabarva]);
            SQL_SAVESORGDATA("update nastaveni set hodnota = " + pouzitabarva + " where polozka='popredi'");

        }

        public void clock_create()
        {
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
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
            MahApps.Metro.ThemeManager.ChangeTheme(System.Windows.Application.Current, pozadi[pouzitepozadi].ToString(), barva[pouzitabarva].ToString());

        }

        #endregion


        #region team_section

        public ObservableCollection<MODEL_Team> Teams { get; set; } = new ObservableCollection<MODEL_Team>();

        public void FUNCTION_TEAM_LOAD_TEAMS()
        {
            Teams.Clear();
            SQL_READSOUTEZDATA("select distinct id, name, (select count(userid) from users_in_teams where teamid = t.id ) pocet from teams T;", "get_teams");
        }


        
        public ObservableCollection<MODEL_usersinteam> Usersinteams { get; set; } = new ObservableCollection<MODEL_usersinteam>();

        public void FUNCTION_TEAM_SHOW_USERS_IN_TEAMS(int idteamu)
        {
            Usersinteams.Clear();
            SQL_READSOUTEZDATA("select * from users U left join users_in_teams T on U.id = T.userid where teamid = "+idteamu+";", "get_usersinteam");

            UsersNOTinteams.Clear();
            SQL_READSOUTEZDATA("select * from users U left join users_in_teams T on U.id = T.userid where teamid = 0", "get_usersnotinteam");


        }


        public ObservableCollection<MODEL_usersnotinteam> UsersNOTinteams { get; set; } = new ObservableCollection<MODEL_usersnotinteam>();





        public string BIND_kolikjelidivteamu(int idteamu)
        {
                return SQL_READSOUTEZDATA("select count(userid) from users_in_teams where teamid = "+ idteamu + ";", "");

        }

        public void FUNCTION_team_move_user_into_team(int id_competitor, int id_new_team, int id_old_team)
        {
            SQL_SAVESOUTEZDATA("update users_in_teams set teamid = "+id_new_team+" where userid = "+ id_competitor + ";");
            FUNCTION_TEAM_SHOW_USERS_IN_TEAMS(id_old_team);
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
            SQL_SAVESOUTEZDATA("update users_in_teams set teamid = 0 where teamid= " + id_team + ";");
            SQL_SAVESOUTEZDATA("delete from teams where id='" + id_team  + "';");
            FUNCTION_TEAM_LOAD_TEAMS();
            FUNCTION_TEAM_SHOW_USERS_IN_TEAMS(9999);
            
        }



        #endregion





        #region Players
        public ObservableCollection<MODEL_Player> Players { get; set; } = new ObservableCollection<MODEL_Player>();
        public ObservableCollection<MODEL_Player_actual> Players_Actual { get; set; } = new ObservableCollection<MODEL_Player_actual>();

        public void FUNCTION_USERS_LOAD_ALLCOMPETITORS()
        {
            Players.Clear();
            SQL_READSOUTEZDATA("select ID, Firstname, Lastname, Country,(select name from Agecategories A  where A.id=U.Agecat) Agecat, (select name from Frequencies F  where F.id=U.Freq) Freq, Ch1, Ch2, Failic, Naclic, Club, Paid, Team, Customagecat from users U; ", "get_players");
            BIND_POCETSOUTEZICICHMENU = SQL_READSOUTEZDATA("select count(id) pocet from users", "");
            BIND_POCETSOUTEZICICH = Int32.Parse(SQL_READSOUTEZDATA("select count(id) pocet from users", ""));
            
        }

        public void FUNCTION_USERS_CREATE_NEW(string firstname, string lastname, string country , int agecat, int freq, int chanel1, int chanel2, string failic, string naclic , string club, bool registered, int team, int customagecat )
        {

            SQL_SAVESOUTEZDATA("insert into users values (null,'"+ firstname + "', '" + lastname  + "', '" + country  + "', '" + agecat  + "', '" + freq  + "', '" + chanel1  + "', '" + chanel2 + "' , '" + failic + "', '" + naclic + "', '" + club + "' , '" + registered + "', '" + team + "', '" + customagecat + "' );");
            Players.Clear();
            SQL_READSOUTEZDATA("select ID, Firstname, Lastname, Country,(select name from Agecategories A  where A.id=U.Agecat) Agecat, (select name from Frequencies F  where F.id=U.Freq) Freq, Ch1, Ch2, Failic, Naclic, Club, Paid, Team, Customagecat from users U;", "get_players");
            BIND_POCETSOUTEZICICHMENU = SQL_READSOUTEZDATA("select count(id) pocet from users", "");
            BIND_POCETSOUTEZICICH = Int32.Parse(SQL_READSOUTEZDATA("select count(id) pocet from users", ""));

        }

        public void FUNCTION_USERS_DELETE_COMPETITOR(int idsouteziciho)
        {
            SQL_READSOUTEZDATA("delete from users where id="+idsouteziciho +"", "");
            Players.Clear();
            SQL_READSOUTEZDATA("select ID, Firstname, Lastname, Country,(select name from Agecategories A  where A.id=U.Agecat) Agecat, (select name from Frequencies F  where F.id=U.Freq) Freq, Ch1, Ch2, Failic, Naclic, Club, Paid, Team, Customagecat from users U;", "get_players");
            BIND_POCETSOUTEZICICHMENU = SQL_READSOUTEZDATA("select count(id) pocet from users", "");
            BIND_POCETSOUTEZICICH = Int32.Parse(SQL_READSOUTEZDATA("select count(id) pocet from users", ""));

        }



        public ObservableCollection<MODEL_Contest_Rounds> MODEL_CONTEST_ROUNDS { get; set; } = new ObservableCollection<MODEL_Contest_Rounds>();
        public ObservableCollection<MODEL_Contest_Groups> MODEL_CONTEST_GROUPS { get; set; } = new ObservableCollection<MODEL_Contest_Groups>();

        public ObservableCollection<MODEL_Player_flags> MODEL_Contest_FLAGS { get; set; } = new ObservableCollection<MODEL_Player_flags>();

        public ObservableCollection<MODEL_Player_agecategories> MODEL_Contest_AGECATEGORIES { get; set; } = new ObservableCollection<MODEL_Player_agecategories>();
        public ObservableCollection<MODEL_Player_frequencies> MODEL_Contest_FREQUENCIES { get; set; } = new ObservableCollection<MODEL_Player_frequencies>();

        public void FUNCTION_ROUNDS_LOAD_ROUNDS()
        {
            MODEL_CONTEST_ROUNDS.Clear();
            SQL_READSOUTEZDATA("select * from rounds;", "get_rounds");
        }


        public void FUNCTION_ROUNDS_LOAD_GROUPS(int  kolo)
        {
            MODEL_CONTEST_GROUPS.Clear();
            SQL_READSOUTEZDATA("select * from matrix M left join groups G on M.grp = G.id where M.rnd="+kolo +";", "get_groups");
        }


        public void FUNCTION_TEAM_ACTIVEMEMBERS(int rnd, int grp)
        {
            if (rnd==0) { rnd = BIND_SELECTED_ROUND; }
            if (grp== 0) { grp = BIND_SELECTED_GROUP; }
            Players_Actual.Clear();
            SQL_READSOUTEZDATA("select U.ID,D.sp,U.Firstname,U.Lastname from draw D left join users U on D.user = U.id where D.rnd = " + rnd  +  " and D.grp = "+ grp +" order by sp asc;", "get_players_actual");
        }


        public void FUNCTION_COMPETITOR_UPDATE(string what, string value, int competiroid)
        {
            SQL_SAVESOUTEZDATA("update users set "+ what +" = '"+ value +"' where ID="+competiroid);

        }

        #endregion


       

    }
}
