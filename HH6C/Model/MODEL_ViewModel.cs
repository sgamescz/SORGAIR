using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using System.IO;
using ControlzEx.Theming;
using System.Globalization;
using NAudio.Wave;
using System.Net.Cache;
using System.Net;




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

    public class SoundList
    {
        public int Id { get; set; }
        public string SoundName { get; set; }
    }


    public class TodoItem2
    {
        public string name { get; set; }
        public int userid { get; set; }
        public string startpoint { get; set; }
        public string startpoint_data { get; set; }
    }




    public class MODEL_ViewModel : INotifyPropertyChanged
    {



        //System.Windows.Media.MediaPlayer[] SoundDB = new System.Windows.Media.MediaPlayer[100];
        WaveOutEvent[] maintimewaveout = new WaveOutEvent[300];
        WaveOutEvent[] preptimewaveout = new WaveOutEvent[300];

        WaveOutEvent[] final_maintimewaveout = new WaveOutEvent[300];
        WaveOutEvent[] final_preptimewaveout = new WaveOutEvent[300];

        WaveOutEvent[] roundgroupwav_actual = new WaveOutEvent[50];
        WaveOutEvent[] roundgroupwav_next = new WaveOutEvent[50];

        WaveOutEvent[] competitorswav_actual = new WaveOutEvent[50];
        WaveOutEvent[] competitorswav_next = new WaveOutEvent[50];

        WaveOutEvent[] roundgroupwav_final_actual = new WaveOutEvent[50];
        WaveOutEvent[] roundgroupwav_final_next = new WaveOutEvent[50];

        WaveOutEvent[] competitorswav_final_actual = new WaveOutEvent[50];
        WaveOutEvent[] competitorswav_final_next = new WaveOutEvent[50];
 
        NAudio.Wave.WaveFileReader[] wav_maintime = new NAudio.Wave.WaveFileReader[300];
        NAudio.Wave.WaveFileReader[] wav_preptime = new NAudio.Wave.WaveFileReader[300];

        NAudio.Wave.WaveFileReader[] wav_final_maintime = new NAudio.Wave.WaveFileReader[300];
        NAudio.Wave.WaveFileReader[] wav_final_preptime = new NAudio.Wave.WaveFileReader[300];

        NAudio.Wave.WaveFileReader[] wav_competitors_actual = new NAudio.Wave.WaveFileReader[300];
        NAudio.Wave.WaveFileReader[] wav_competitors_next = new NAudio.Wave.WaveFileReader[300];

        NAudio.Wave.WaveFileReader[] wav_roundgroup_actual = new NAudio.Wave.WaveFileReader[300];
        NAudio.Wave.WaveFileReader[] wav_roundgroup_next = new NAudio.Wave.WaveFileReader[300];

        NAudio.Wave.WaveFileReader[] wav_competitors_final_actual = new NAudio.Wave.WaveFileReader[300];
        NAudio.Wave.WaveFileReader[] wav_competitors_final_next = new NAudio.Wave.WaveFileReader[300];

        NAudio.Wave.WaveFileReader[] wav_roundgroup_final_actual = new NAudio.Wave.WaveFileReader[300];
        NAudio.Wave.WaveFileReader[] wav_roundgroup_final_next = new NAudio.Wave.WaveFileReader[300];


        SQLiteConnection DBSORG_Connection;
        SQLiteConnection DBSOUTEZ_Connection;
        SQLiteConnection TMP_Connection;

        System.Windows.Threading.DispatcherTimer MAIN_TIME_TIMER = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer PREP_TIME_TIMER = new System.Windows.Threading.DispatcherTimer();

        System.Windows.Threading.DispatcherTimer MAIN_FINAL_TIME_TIMER = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer PREP_FINAL_TIME_TIMER = new System.Windows.Threading.DispatcherTimer();

        System.Windows.Threading.DispatcherTimer MAIN_DYNAMIC_ROUNDGROUP_ACTUAL_TIMER = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer MAIN_DYNAMIC_COMPETITORS_ACTUAL_TIMER = new System.Windows.Threading.DispatcherTimer();

        System.Windows.Threading.DispatcherTimer MAIN_DYNAMIC_ROUNDGROUP_NEXT_TIMER = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer MAIN_DYNAMIC_COMPETITORS_NEXT_TIMER = new System.Windows.Threading.DispatcherTimer();

        System.Windows.Threading.DispatcherTimer MAIN_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_TIMER = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer MAIN_DYNAMIC_COMPETITORS_FINAL_ACTUAL_TIMER = new System.Windows.Threading.DispatcherTimer();

        System.Windows.Threading.DispatcherTimer MAIN_DYNAMIC_ROUNDGROUP_FINAL_NEXT_TIMER = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer MAIN_DYNAMIC_COMPETITORS_FINAL_NEXT_TIMER = new System.Windows.Threading.DispatcherTimer();


        System.Diagnostics.Stopwatch timer_main = new System.Diagnostics.Stopwatch();
        System.Diagnostics.Stopwatch timer_prep = new System.Diagnostics.Stopwatch();

        System.Diagnostics.Stopwatch timer_final_main = new System.Diagnostics.Stopwatch();
        System.Diagnostics.Stopwatch timer_final_prep = new System.Diagnostics.Stopwatch();

        System.Diagnostics.Stopwatch timer_dynamic_roundgroup_actual = new System.Diagnostics.Stopwatch();
        System.Diagnostics.Stopwatch timer_dynamic_roundgroup_next = new System.Diagnostics.Stopwatch();
        System.Diagnostics.Stopwatch timer_DYNAMIC_COMPETITORS_ACTUAL = new System.Diagnostics.Stopwatch();
        System.Diagnostics.Stopwatch timer_DYNAMIC_COMPETITORS_NEXT = new System.Diagnostics.Stopwatch();

        System.Diagnostics.Stopwatch timer_dynamic_roundgroup_FINAL_actual = new System.Diagnostics.Stopwatch();
        System.Diagnostics.Stopwatch timer_dynamic_roundgroup_FINAL_next = new System.Diagnostics.Stopwatch();
        System.Diagnostics.Stopwatch timer_DYNAMIC_COMPETITORS_FINAL_ACTUAL = new System.Diagnostics.Stopwatch();
        System.Diagnostics.Stopwatch timer_DYNAMIC_COMPETITORS_FINAL_NEXT = new System.Diagnostics.Stopwatch();

        string[] barva = new string[] { "Red", "Green", "Blue", "Purple", "Orange", "Lime", "Emerald", "Teal", "Cyan", "Cobalt", "Indigo", "Violet", "Pink", "Magenta", "Crimson", "Amber", "Yellow", "Brown", "Olive", "Steel", "Mauve", "Taupe", "Sienna" };
        string[] pozadi = new string[] { "Light", "Dark" };
        int pouzitabarva = 1;
        int pouzitepozadi = 1;


        int last_second_prep_time = 0;
        int last_second_main_time = 0;

        int last_second_final_prep_time = 0;
        int last_second_final_main_time = 0;

        bool _BIND_IS_FINAL_FLIGHT_READY = false;

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
        bool BIND_MENU_ENABLED_vysledky_finale_value = false;
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
        public string BIND_VYBRANEKOLOMENU_value = SORGAIR.Properties.Lang.Lang.menu_selectedround  + " : 0/0";
        public string BIND_POCETSOUTEZICICHMENU_value = "0";
        public int BIND_POCETSOUTEZICICH_value = 0;
        public string BIND_CONTESTBEGIN_value;
        public bool BIND_USEAUDIO_value;
        public bool BIND_SQL_SOUTEZ_ENTRYSTYLE_value;
        public bool BIND_SQL_SOUTEZ_ENTRYSTYLEPOINTSORLENGHT_value;
        public bool BIND_SQL_SOUTEZ_ENTRYSTYLENEXT_value;
        public bool BIND_SQL_AUDIO_COMPNUMBERS_value;
        public bool _BIND_AUDIO_PREPTIME_MANUAL_NEXT;
        public bool _BIND_PREP_AUDIO_MAN_AUTO;
        public bool _BIND_AUDIO_PREPTIME_AUTO_NEXT;


        public bool _BIND_AUDIO_FINAL_PREPTIME_MANUAL_NEXT;
        public bool _BIND_PREP_FINAL_AUDIO_MAN_AUTO;
        public bool _BIND_AUDIO_FINAL_PREPTIME_AUTO_NEXT;


        public bool BIND_SQL_AUDIO_COMPNUMBERS_PREP_value;
        public bool BIND_SQL_AUDIO_RNDGRPFLIGHT_value;
        public bool BIND_SQL_AUDIO_RNDGRPPREP_value;
        public string BIND_SORGNEWS_value = "Vítejte v nové verzi SORG AIR pro rok 2020. Aktuální novinky jsou blabla a tak dále a tak dale nějaký text z netu";
        public float BIND_LETOVYCAS_value = 0;
        public float BIND_FINAL_LETOVYCAS_value = 0;

        public float BIND_LETOVYCAS_PREP_value = 0;
        public float BIND_FINAL_LETOVYCAS_PREP_value = 0;

        public int BIND_LETOVYCAS_MAX_value = 600;
        public int BIND_FINAL_LETOVYCAS_MAX_value = 600;


        public int BIND_PRE_LETOVYCAS_MAX_value = 20;
        public int BIND_LETOVYCAS_PREP_MAX_value = 20;
        public int BIND_FINAL_LETOVYCAS_PREP_MAX_value = 20;



        public string BIND_LETOVYCAS_STRING_value = "xxx";
        public string BIND_LETOVYCAS_PREP_STRING_value = "xxx";

        public float BIND_PROGRESS_1_value = 0;
        public float BIND_PROGRESS_PREP_1_value = 0;

        public float BIND_FINAL_PROGRESS_1_value = 0;
        public float BIND_FINAL_PROGRESS_PREP_1_value = 0;

        public int BIND_SELECTED_ROUND_value = 1;
        public int BIND_SELECTED_GROUP_value = 1;
        public int _BIND_SELECTED_STARTPOINT;
        public int _BIND_SELECTED_FINAL_STARTPOINT;
        public int BIND_SELECTED_ROUND_DESC_value;
        public int BIND_SELECTED_GROUP_DECS_value;


        public int BIND_VIEWED_ROUND_value;
        public int BIND_VIEWED_GROUP_value;


        public bool BIND_SQL_AUTO_USEPREPTIME_value;
        public bool _BIND_SQL_AUTO_RUNPREPTIMENEXTROUND;
        public bool _BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME;
        public bool _BIND_SQL_AUTO_RUNPREPTIMENEXTROUND_FINAL;
        public bool _BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME_FINAL;

        public string BIND_SQL_AUTO_PREPTIMELENGHT_value;
        public string _BIND_SQL_AUTO_PREPTIMESTART;
        public string _BIND_SQL_AUTO_MAINTIMESTART;
        public string _BIND_SQL_AUTO_PREPTIMESTART_FINAL;
        public string _BIND_SQL_AUTO_MAINTIMESTART_FINAL;

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



        string _HW_ATI = " ATI ";
        public string HW_ATI
        {
            get { return _HW_ATI; }
            set { _HW_ATI = value; OnPropertyChanged("HW_ATI"); }

        }


        string _HW_ATISN = " SN ";
        public string HW_ATISN
        {
            get { return _HW_ATISN; }
            set { _HW_ATISN = value; OnPropertyChanged("HW_ATISN"); }

        }

        string _HW_ATIMEM = " MEM ";
        public string HW_ATIMEM
        {
            get { return _HW_ATIMEM; }
            set { _HW_ATIMEM = value; OnPropertyChanged("HW_ATIMEM"); }

        }

        string _HW_ATIUPTIME = " UPTIME ";
        public string HW_ATIUPTIME
        {
            get { return _HW_ATIUPTIME; }
            set { _HW_ATIUPTIME = value; OnPropertyChanged("HW_ATIUPTIME"); }

        }


        Boolean _BINDING_HW_MENU_BASE = false;
        public Boolean BINDING_HW_MENU_BASE
        {
            get { return _BINDING_HW_MENU_BASE; }
            set { _BINDING_HW_MENU_BASE = value; OnPropertyChanged("BINDING_HW_MENU_BASE"); }

        }


        public int _BINDING_selectedmenuindex = 0;
        public int BINDING_selectedmenuindex
        {
            get {
                Console.WriteLine("GETBINDING_selectedmenuindex" + _BINDING_selectedmenuindex);
                return _BINDING_selectedmenuindex; 
            }
            set {
                _BINDING_selectedmenuindex = value;
                Console.WriteLine("SETBINDING_selectedmenuindex" + value);
                OnPropertyChanged("BINDING_selectedmenuindex"); 
            }

        }

        


        string _BIND_PREWROUND_TEXT = " --- ";
        public string BIND_PREWROUND_TEXT
        {
            get { return _BIND_PREWROUND_TEXT; }
            set { _BIND_PREWROUND_TEXT = value; OnPropertyChanged("BIND_PREWROUND_TEXT"); }

        }


        
        public bool BIND_IS_FINAL_FLIGHT_READY
        {
            get { return _BIND_IS_FINAL_FLIGHT_READY; }
            set { _BIND_IS_FINAL_FLIGHT_READY = value;
                BIND_MENU_ENABLED_finale = value;
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='isfinalflightready'");
                
                OnPropertyChanged("BIND_IS_FINAL_FLIGHT_READY");
                OnPropertyChanged("BIND_MENU_ENABLED_finale");
            }
        }


        private bool _CONTEST_LOCK = true;
        public bool CONTEST_LOCK
        {
            get { return _CONTEST_LOCK; }
            set { _CONTEST_LOCK = value; OnPropertyChanged("CONTEST_LOCK");
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='islocked'");
            }
        }

        private bool _BIND_CATEGORYEDITOR_ENABLED = false;
        public bool BIND_CATEGORYEDITOR_ENABLED
        {
            get { return _BIND_CATEGORYEDITOR_ENABLED; }
            set { _BIND_CATEGORYEDITOR_ENABLED = value; OnPropertyChanged("BIND_CATEGORYEDITOR_ENABLED"); }
        }



        public bool BIND_MENU_ENABLED_finale
        {
            get { return BIND_MENU_ENABLED_finale_value; }
            set { BIND_MENU_ENABLED_finale_value = value; OnPropertyChanged("BIND_MENU_ENABLED_finale"); }
        }


        int _BIND_AUDIO_SELECTEDBASESOUND_INDEX = -1;
        public int BIND_AUDIO_SELECTEDBASESOUND_INDEX
        {
            get { return _BIND_AUDIO_SELECTEDBASESOUND_INDEX; }
            set { _BIND_AUDIO_SELECTEDBASESOUND_INDEX = value; OnPropertyChanged("BIND_AUDIO_SELECTEDBASESOUND_INDEX");
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='selectedsound1'");

            }
        }


        int _BIND_AUDIO_SELECTEDPREPSOUND_INDEX = -1;
        public int BIND_AUDIO_SELECTEDPREPSOUND_INDEX
        {
            get { return _BIND_AUDIO_SELECTEDPREPSOUND_INDEX; }
            set
            {
                _BIND_AUDIO_SELECTEDPREPSOUND_INDEX = value; OnPropertyChanged("BIND_AUDIO_SELECTEDPREPSOUND_INDEX");
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='selectedsound1prep'");

            }
        }

        int _BIND_AUDIO_SELECTEDPREPFINALSOUND_INDEX = -1;
        public int BIND_AUDIO_SELECTEDPREPFINALSOUND_INDEX
        {
            get { return _BIND_AUDIO_SELECTEDPREPFINALSOUND_INDEX; }
            set
            {
                _BIND_AUDIO_SELECTEDPREPFINALSOUND_INDEX = value; OnPropertyChanged("BIND_AUDIO_SELECTEDPREPFINALSOUND_INDEX");
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='selectedfinalprepsound'");

            }
        }

        int _BIND_AUDIO_SELECTEDFINALSOUND_INDEX = -1;
        public int BIND_AUDIO_SELECTEDFINALSOUND_INDEX
        {
            get { return _BIND_AUDIO_SELECTEDFINALSOUND_INDEX; }
            set
            {
                _BIND_AUDIO_SELECTEDFINALSOUND_INDEX = value; OnPropertyChanged("BIND_AUDIO_SELECTEDFINALSOUND_INDEX");
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='selectedfinalsound'");

            }
        }

        int _BINDING_SoundList_languages_index = -1;
        public int BINDING_SoundList_languages_index
        {
            get { return _BINDING_SoundList_languages_index; }
            set
            {
                _BINDING_SoundList_languages_index = value; OnPropertyChanged("BINDING_SoundList_languages_index");
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='selectedaudiolanguageindex'");
                //FUNCTION_SOUND_LOADSELECTEDSOUND_MAIN(BINDING_SoundList[value].Id);
                //BIND_AUDIO_INFO = BINDING_SoundList[value].SoundName;

            }
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

        private string _BIND_ROZLOSOVANIODPOVIDAPOCTUM = "Visible";
        public string BIND_ROZLOSOVANIODPOVIDAPOCTUM
        {
            get { return _BIND_ROZLOSOVANIODPOVIDAPOCTUM; }
            set { _BIND_ROZLOSOVANIODPOVIDAPOCTUM = value; OnPropertyChanged("BIND_ROZLOSOVANIODPOVIDAPOCTUM"); }
        }

        private int _BIND_JETREBAROZLOSOVAT_SCORE = 0;
        public int BIND_JETREBAROZLOSOVAT_SCORE
        {
            get {
                return _BIND_JETREBAROZLOSOVAT_SCORE; 
            }
            set {

                 SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Matrix_score'");
                _BIND_JETREBAROZLOSOVAT_SCORE = value;
                OnPropertyChanged("BIND_JETREBAROZLOSOVAT_SCORE");
            }
        }


        private int _BIND_JETREBAROZLOSOVAT_SCORE_FINAL = 0;
        public int BIND_JETREBAROZLOSOVAT_SCORE_FINAL
        {
            get
            {
                return _BIND_JETREBAROZLOSOVAT_SCORE_FINAL;
            }
            set
            {

                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Matrix_score_final'");
                _BIND_JETREBAROZLOSOVAT_SCORE_FINAL = value;
                OnPropertyChanged("BIND_JETREBAROZLOSOVAT_SCORE_FINAL");
            }
        }


        private string _BIND_TYPEOFCLOCK = "PRE_MAIN";
        public string BIND_TYPEOFCLOCK
        {
            get { return _BIND_TYPEOFCLOCK; }
            set { _BIND_TYPEOFCLOCK = value; OnPropertyChanged("BIND_TYPEOFCLOCK");Console.WriteLine("BIND_TYPEOFCLOCK" + value); }
        }


        public void FUNCTION_JETREBAROZLOSOVAT_OVER()
        {
            Console.WriteLine("_BIND_JETREBAROZLOSOVAT_SCORE" + _BIND_JETREBAROZLOSOVAT_SCORE);
            Console.WriteLine("SUM"+ (BIND_SQL_SOUTEZ_ROUNDS * BIND_SQL_SOUTEZ_GROUPS * BIND_SQL_SOUTEZ_STARTPOINTS));
            if (_BIND_JETREBAROZLOSOVAT_SCORE != (BIND_SQL_SOUTEZ_ROUNDS * BIND_SQL_SOUTEZ_GROUPS * BIND_SQL_SOUTEZ_STARTPOINTS))
            {
                BIND_ROZLOSOVANIODPOVIDAPOCTUM = "Collapsed";
                Console.WriteLine("BIND_ROZLOSOVANIODPOVIDAPOCTUM" + BIND_ROZLOSOVANIODPOVIDAPOCTUM);
                BIND_MENU_ENABLED_seznamkol = false;
                BIND_MENU_ENABLED_vybranekolo = false;
                BIND_MENU_ENABLED_vysledky = false;
                BIND_MENU_ENABLED_vysledky_finale = false;
                BIND_MENU_ENABLED_detailyastatistiky = false;
            }
            else
            {
                BIND_ROZLOSOVANIODPOVIDAPOCTUM = "Visible";
                Console.WriteLine("BIND_ROZLOSOVANIODPOVIDAPOCTUM" + BIND_ROZLOSOVANIODPOVIDAPOCTUM);
                Console.WriteLine("CBACBA");
                BIND_MENU_ENABLED_seznamkol = true;
                BIND_MENU_ENABLED_vybranekolo = true;
                BIND_MENU_ENABLED_vysledky = true;
                BIND_MENU_ENABLED_vysledky_finale = true;
                BIND_MENU_ENABLED_detailyastatistiky = true;
            }

        }

 public void FUNCTION_JETREBAROZLOSOVAT_OVER_FINAL()
        {
            Console.WriteLine("_BIND_JETREBAROZLOSOVAT_SCORE_FINAL" + _BIND_JETREBAROZLOSOVAT_SCORE_FINAL);
            Console.WriteLine("SUM"+ (BIND_SQL_SOUTEZ_ROUNDSFINALE *  BIND_SQL_SOUTEZ_STARTPOINTSFINALE));
            if (_BIND_JETREBAROZLOSOVAT_SCORE_FINAL != (BIND_SQL_SOUTEZ_ROUNDSFINALE *  BIND_SQL_SOUTEZ_STARTPOINTSFINALE))
            {
                BIND_MENU_ENABLED_finale = false;
                BIND_IS_FINAL_FLIGHT_READY = false;
            }
            else
            {
                if (BIND_IS_FINAL_FLIGHT_READY == true) {
                    BIND_MENU_ENABLED_finale = true;


                    FUNCTION_ROUNDS_LOAD_FINAL_ROUNDS();

                    Console.WriteLine("MODEL_CONTEST_FINAL_ROUNDS.Count:" + MODEL_CONTEST_FINAL_ROUNDS.Count);
                    Console.WriteLine("BIND_SQL_SOUTEZ_ROUNDSFINALE:" + BIND_SQL_SOUTEZ_ROUNDSFINALE);

                    if (MODEL_CONTEST_FINAL_ROUNDS.Count > 0)
                    {
                        FUNCTION_SELECTED_FINAL_ROUND_USERS(1, 1);
                        BIND_SELECTED_FINAL_ROUND = 1;
                        MODEL_CONTEST_FINAL_ROUNDS[BIND_SELECTED_FINAL_ROUND - 1].ISSELECTED = "selected";
                    }


                }
            }

        }
        string _BIND_AUDIO_INFO;
        public string BIND_AUDIO_INFO
        {
            get {


                string casod = "00:00";

                if (MODEL_CONTEST_SOUNDS_MAIN.Count > 0)
                {

                    if (MODEL_CONTEST_SOUNDS_MAIN[0].VALUE < 0)
                    {
                        casod = ("-" + Math.Abs(MODEL_CONTEST_SOUNDS_MAIN[0].VALUE) / 60).ToString() + ":" + (Math.Abs(MODEL_CONTEST_SOUNDS_MAIN[0].VALUE) % 60).ToString("00");
                    }

                }


                string time = (MODEL_CONTEST_RULES[0].BASEROUNDMAXTIME / 60).ToString() + ":" + (MODEL_CONTEST_RULES[0].BASEROUNDMAXTIME % 60).ToString("00");
                last_second_main_time = MODEL_CONTEST_SOUNDS_MAIN[MODEL_CONTEST_SOUNDS_MAIN.Count - 1].VALUE;
                Console.WriteLine("MODEL_CONTEST_SOUNDS_MAIN" + MODEL_CONTEST_SOUNDS_MAIN.Count);
                Console.WriteLine("MODEL_CONTEST_SOUNDS_MAIN.value" + MODEL_CONTEST_SOUNDS_MAIN[MODEL_CONTEST_SOUNDS_MAIN.Count - 1].VALUE);

                return _BIND_AUDIO_INFO + " [" + casod + " - " + time + "]";

            }
            set { _BIND_AUDIO_INFO = value; OnPropertyChanged("BIND_AUDIO_INFO"); }
        }





        string _BIND_AUDIO_PREP_INFO;
        public string BIND_AUDIO_PREP_INFO
        {
            get
            {


                string casod = "00:00";

                if (MODEL_CONTEST_SOUNDS_PREP.Count > 0)
                {

                    if (MODEL_CONTEST_SOUNDS_PREP[0].VALUE < 0)
                    {
                        casod = ("-" + Math.Abs(MODEL_CONTEST_SOUNDS_PREP[0].VALUE) / 60).ToString() + ":" + (Math.Abs(MODEL_CONTEST_SOUNDS_PREP[0].VALUE) % 60).ToString("00");
                    }

                }


                
                string time = (MODEL_CONTEST_SOUNDS_PREP[MODEL_CONTEST_SOUNDS_PREP.Count-1].VALUE / 60).ToString() + ":" + (MODEL_CONTEST_SOUNDS_PREP[MODEL_CONTEST_SOUNDS_PREP.Count-1].VALUE % 60).ToString("00");
                last_second_prep_time = MODEL_CONTEST_SOUNDS_PREP[MODEL_CONTEST_SOUNDS_PREP.Count - 1].VALUE;
                return _BIND_AUDIO_PREP_INFO + " [" + casod + " - " + time + "]";

            }
            set { _BIND_AUDIO_PREP_INFO = value; OnPropertyChanged("BIND_AUDIO_PREP_INFO"); }
        }





        string _BIND_AUDIO_FINAL_INFO;
        public string BIND_AUDIO_FINAL_INFO
        {
            get
            {


                string casod = "00:00";
                if (MODEL_CONTEST_SOUNDS_FINAL_MAIN.Count > 0)
                {

                    if (MODEL_CONTEST_SOUNDS_FINAL_MAIN[0].VALUE < 0)
                    {
                        casod = ("-" + Math.Abs(MODEL_CONTEST_SOUNDS_FINAL_MAIN[0].VALUE) / 60).ToString() + ":" + (Math.Abs(MODEL_CONTEST_SOUNDS_FINAL_MAIN[0].VALUE) % 60).ToString("00");
                    }

                }


                string time = (MODEL_CONTEST_RULES[0].FINALROUNDMAXTIME / 60).ToString() + ":" + (MODEL_CONTEST_RULES[0].FINALROUNDMAXTIME % 60).ToString("00");
                last_second_final_main_time = MODEL_CONTEST_SOUNDS_FINAL_MAIN[MODEL_CONTEST_SOUNDS_FINAL_MAIN.Count - 1].VALUE;

                return _BIND_AUDIO_FINAL_INFO + " [" + casod + " - " + time + "]";

            }
            set { _BIND_AUDIO_FINAL_INFO = value; OnPropertyChanged("BIND_AUDIO_FINAL_INFO"); }
        }





        string _BIND_AUDIO_FINAL_PREP_INFO;
        public string BIND_AUDIO_FINAL_PREP_INFO
        {
            get
            {


                string casod = "00:00";

                if (MODEL_CONTEST_SOUNDS_FINAL_PREP.Count > 0)
                {

                    if (MODEL_CONTEST_SOUNDS_FINAL_PREP[0].VALUE < 0)
                    {
                        casod = ("-" + Math.Abs(MODEL_CONTEST_SOUNDS_FINAL_PREP[0].VALUE) / 60).ToString() + ":" + (Math.Abs(MODEL_CONTEST_SOUNDS_FINAL_PREP[0].VALUE) % 60).ToString("00");
                    }

                }



                string time = (MODEL_CONTEST_SOUNDS_FINAL_PREP[MODEL_CONTEST_SOUNDS_FINAL_PREP.Count - 1].VALUE / 60).ToString() + ":" + (MODEL_CONTEST_SOUNDS_FINAL_PREP[MODEL_CONTEST_SOUNDS_FINAL_PREP.Count - 1].VALUE % 60).ToString("00");
                last_second_final_prep_time = MODEL_CONTEST_SOUNDS_FINAL_PREP[MODEL_CONTEST_SOUNDS_FINAL_PREP.Count - 1].VALUE;
                return _BIND_AUDIO_FINAL_PREP_INFO + " [" + casod + " - " + time + "]";
            }
            set { _BIND_AUDIO_FINAL_PREP_INFO = value; OnPropertyChanged("BIND_AUDIO_FINAL_PREP_INFO"); }
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
        public bool BIND_MENU_ENABLED_vysledky_finale
        {
            get { return BIND_MENU_ENABLED_vysledky_finale_value; }
            set { BIND_MENU_ENABLED_vysledky_finale_value = value; OnPropertyChanged("BIND_MENU_ENABLED_vysledky_finale"); }
        }

        public bool BIND_MENU_ENABLED_seznamkol
        {
            get { return BIND_MENU_ENABLED_seznamkol_value; }
            set { BIND_MENU_ENABLED_seznamkol_value = value; OnPropertyChanged("BIND_MENU_ENABLED_seznamkol"); }
        }


        public string _Function_global_resizemode= "Fill";
        public string Function_global_resizemode
        {
            get { return _Function_global_resizemode; }
            set { 
                _Function_global_resizemode = value;
                OnPropertyChanged("Function_global_resizemode");
                SQL_SAVESORGDATA("update contest set value='" + value + "' where item='resizemode'");

            }
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



            MODEL_Contest_FLAGS.Clear();
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            mArrayOfflags = Directory.GetFiles(directory + "/flags/", "*.*", SearchOption.TopDirectoryOnly);

            int _tmpi = -1;

            foreach (var file in mArrayOfflags)
            {
                _tmpi += 1;
                FileInfo info = new FileInfo(file);
                var players_flags = new MODEL_Player_flags()


                { ID = _tmpi, FILENAME = Path.GetFileNameWithoutExtension(info.Name) };
                MODEL_Contest_FLAGS.Add(players_flags);
            }

            MODEL_Contest_FREQUENCIES.Clear();

            var tmp_frequencies = new MODEL_Player_frequencies()
            { ID = 0, NAME = "2,4 GHz" };
            MODEL_Contest_FREQUENCIES.Add(tmp_frequencies);
            tmp_frequencies = new MODEL_Player_frequencies()
            { ID = 1, NAME = "35 MHz" };
            MODEL_Contest_FREQUENCIES.Add(tmp_frequencies);
            tmp_frequencies = new MODEL_Player_frequencies()
            { ID = 2, NAME = "40 MHz" };
            MODEL_Contest_FREQUENCIES.Add(tmp_frequencies);


            MODEL_Contest_AGECATEGORIES.Clear();
            var tmp_agecategories = new MODEL_Player_agecategories()
            { ID = 0, NAME = "Senior" };
            MODEL_Contest_AGECATEGORIES.Add(tmp_agecategories);
            tmp_agecategories = new MODEL_Player_agecategories()
            { ID = 1, NAME = "Junior" };
            MODEL_Contest_AGECATEGORIES.Add(tmp_agecategories);
            tmp_agecategories = new MODEL_Player_agecategories()
            { ID = 2, NAME = "Žák" };
            MODEL_Contest_AGECATEGORIES.Add(tmp_agecategories);



            check_db_version();





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
            BIND_CONTESTBEGIN = SQL_READSOUTEZDATA("select value from contest where item='contestbegin'", "");
            BIND_USEAUDIO = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='useaudio'", ""));
            BIND_SQL_SOUTEZ_ENTRYSTYLE = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Entrystyle'", ""));
            BIND_SQL_SOUTEZ_ENTRYSTYLEPOINTSORLENGHT = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Entrystylepointsorlenght'", ""));
            BIND_SQL_SOUTEZ_ENTRYSTYLENEXT = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Entrystylenext'", ""));
            BIND_SQL_SOUTEZ_REQUIREFAILICENCE = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='RequireFAILicence'", ""));
            BIND_SQL_AUDIO_COMPNUMBERS = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Audiocumpetitornumber'", ""));
            BIND_AUDIO_PREPTIME_MANUAL_NEXT = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Audio_preptime_man_next'", ""));
            BIND_PREP_AUDIO_MAN_AUTO = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='prep_audio_man_auto'", ""));
            BIND_AUDIO_PREPTIME_AUTO_NEXT = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Audio_preptime_auto_next'", ""));

            BIND_AUDIO_FINAL_PREPTIME_MANUAL_NEXT = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Audio_preptime_final_man_next'", ""));
            BIND_PREP_AUDIO_FINAL_MAN_AUTO = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='prep_audio_final_man_auto'", ""));
            BIND_AUDIO_FINAL_PREPTIME_AUTO_NEXT = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Audio_preptime_final_auto_next'", ""));

            BIND_SQL_AUDIO_COMPNUMBERS_PREP = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Audiocumpetitornumberprep'", ""));
            BIND_SQL_AUDIO_RNDGRPFLIGHT = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Rndgrpflight'", ""));
            BIND_SQL_AUDIO_RNDGRPPREP = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Rndgrpprep'", ""));
            BIND_SQL_AUDIO_FUNKYMOD = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='funkymod'", ""));
            BIND_SQL_AUTO_USEPREPTIME = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Usepreptime'", ""));
            BIND_SQL_AUTO_RUNPREPTIMENEXTROUND = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Runnextroundafterpreptime'", ""));
            BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Runpreptime'", ""));
            BIND_SQL_AUTO_RUNPREPTIMENEXTROUND_FINAL = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Runnextroundafterpreptime_final'", ""));
            BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME_FINAL = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='Runpreptime_final'", ""));
            BIND_SQL_AUTO_PREPTIMELENGHT = SQL_READSOUTEZDATA("select value from contest where item='Preptimelenght'", "");
            BIND_SQL_AUTO_PREPTIMESTART = SQL_READSOUTEZDATA("select value from contest where item='Preptimestart'", "");
            BIND_SQL_AUTO_MAINTIMESTART = SQL_READSOUTEZDATA("select value from contest where item='Maintimestart'", "");
            BIND_SQL_AUTO_PREPTIMESTART_FINAL = SQL_READSOUTEZDATA("select value from contest where item='Preptimestart_final'", "");
            BIND_SQL_AUTO_MAINTIMESTART_FINAL = SQL_READSOUTEZDATA("select value from contest where item='Maintimestart_final'", "");
            BIND_AUDIO_SELECTEDBASESOUND_INDEX = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='selectedsound1'", ""));
            BIND_AUDIO_SELECTEDPREPSOUND_INDEX = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='selectedsound1prep'", ""));
            BIND_AUDIO_SELECTEDPREPFINALSOUND_INDEX = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='selectedfinalprepsound'", ""));
            BIND_AUDIO_SELECTEDFINALSOUND_INDEX = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='selectedfinalsound'", ""));
            BINDING_SoundList_languages_index = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='selectedaudiolanguageindex'", ""));
            CONTEST_LOCK = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest  where item='islocked'",""));


            CONTENT_RANDOM_ID = SQL_READSOUTEZDATA("select value from contest where item='CONTENT_RANDOM_ID'", "");


            BIND_IS_FINAL_FLIGHT_READY = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='isfinalflightready'", ""));

            

            BIND_MENU_ENABLED_nastavenisouteze = true;
            BIND_MENU_ENABLED_vysledky_finale = true;
            BIND_MENU_ENABLED_audioadalsi = true;
            BIND_MENU_ENABLED_hardware = true;
            BIND_MENU_ENABLED_soutezici = true;
            BIND_MENU_ENABLED_rozlosovani = true;
            BIND_MENU_ENABLED_vybranekolo = true;
            BIND_MENU_ENABLED_seznamkol = true;
            BIND_MENU_ENABLED_vysledky = true;
            BIND_MENU_ENABLED_online = true;
            BIND_MENU_ENABLED_detailyastatistiky = true;


            FUNCTION_LOAD_MATRIX_FILES();
            FUNCTION_LOAD_TIMERS_MINUTES();
            FUNCTION_LOAD_TIMERS_SECONDS();
            FUNCTION_LOAD_TIMERS_HEIGHT();
            FUNCTION_LOAD_TIMERS_LANDINGS();
            FUNCTION_LOAD_TIMERS_PENALISATIOGLOBAL();
            FUNCTION_LOAD_TIMERS_PENALISATIONLOCAL();




            Console.WriteLine(SQL_READSOUTEZDATA("select value from contest where item='Matrix_score'", ""));
            BIND_JETREBAROZLOSOVAT_SCORE = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Matrix_score'", ""));
            BIND_JETREBAROZLOSOVAT_SCORE_FINAL = Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Matrix_score_final'", ""));
            FUNCTION_JETREBAROZLOSOVAT_OVER();
            FUNCTION_JETREBAROZLOSOVAT_OVER_FINAL();



        }


        private string _BIND_CAS_DO_MENU = "";
        public string BIND_CAS_DO_MENU
        {
            get
            {
                return _BIND_CAS_DO_MENU;
            }

            set
            {
                _BIND_CAS_DO_MENU = value; OnPropertyChanged("BIND_CAS_DO_MENU");
            }
        }


        public string BIND_LETOVYCAS_STRING
        {
            get
            {
                //                TimeSpan time_elapsed = TimeSpan.FromSeconds(BIND_LETOVYCAS_value);
                var elapsed = timer_main.Elapsed;
                string letovycas = "";

                if (BIND_TYPEOFCLOCK == "PRE_MAIN")
                {
                    TimeSpan time_remaining = TimeSpan.FromSeconds(BIND_LETOVYCAS_MAX);
                    TimeSpan totalsec = TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds);
                    TimeSpan rozdil = time_remaining.Subtract(totalsec);
                    letovycas = "Letový čas začne za: " + rozdil.ToString("mm':'ss':'ff");
                }
                else
                {
                    TimeSpan time_remaining = TimeSpan.FromSeconds(MODEL_CONTEST_RULES[0].BASEROUNDMAXTIME);
                    TimeSpan totalsec = TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds);
                    TimeSpan rozdil2 = time_remaining.Subtract(totalsec);

                    letovycas = "Letový čas : " + elapsed.ToString("mm':'ss':'ff") + " (zbývá : " + rozdil2.ToString("mm':'ss':'ff") + ")";
                }
                //BIND_CAS_DO_MENU = letovycas;
                return letovycas;
            }

        }


        public string BIND_LETOVYCAS_PREP_STRING
        {
            get
            {
                //                TimeSpan time_elapsed = TimeSpan.FromSeconds(BIND_LETOVYCAS_value);
                var elapsed = timer_prep.Elapsed;
                string letovycas = "";

            
                    TimeSpan time_remaining = TimeSpan.FromSeconds(BIND_LETOVYCAS_PREP_MAX);
                    TimeSpan totalsec = TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds);
                    TimeSpan rozdil2 = time_remaining.Subtract(totalsec);

                    letovycas = "Přípravný čas : " + elapsed.ToString("mm':'ss':'ff") + " (zbývá : " + rozdil2.ToString("mm':'ss':'ff") + ")";
              

                //                Console.WriteLine(elapsed.ToString("mm':'ss':'f"));
                return letovycas;
            }

        }









        public string BIND_FINAL_LETOVYCAS_STRING
        {
            get
            {
                //                TimeSpan time_elapsed = TimeSpan.FromSeconds(BIND_LETOVYCAS_value);
                var elapsed = timer_final_main.Elapsed;
                string letovycas = "";

                if (BIND_TYPEOFCLOCK == "PRE_MAIN")
                {
                    TimeSpan time_remaining = TimeSpan.FromSeconds(BIND_FINAL_LETOVYCAS_MAX);
                    TimeSpan totalsec = TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds);
                    TimeSpan rozdil = time_remaining.Subtract(totalsec);
                    letovycas = "Letový čas začne za: " + rozdil.ToString("mm':'ss':'ff");
                }
                else
                {
                    TimeSpan time_remaining = TimeSpan.FromSeconds(MODEL_CONTEST_RULES[0].FINALROUNDMAXTIME);
                    TimeSpan totalsec = TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds);
                    TimeSpan rozdil2 = time_remaining.Subtract(totalsec);

                    letovycas = "Letový čas : " + elapsed.ToString("mm':'ss':'ff") + " (zbývá : " + rozdil2.ToString("mm':'ss':'ff") + ")";
                }

                return letovycas;
            }

        }


        public string BIND_FINAL_LETOVYCAS_PREP_STRING
        {
            get
            {
                //                TimeSpan time_elapsed = TimeSpan.FromSeconds(BIND_LETOVYCAS_value);
                var elapsed = timer_final_prep.Elapsed;
                string letovycas = "";


                TimeSpan time_remaining = TimeSpan.FromSeconds(BIND_FINAL_LETOVYCAS_PREP_MAX);
                TimeSpan totalsec = TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds);
                TimeSpan rozdil2 = time_remaining.Subtract(totalsec);

                letovycas = "Přípravný čas : " + elapsed.ToString("mm':'ss':'ff") + " (zbývá : " + rozdil2.ToString("mm':'ss':'ff") + ")";


                //                Console.WriteLine(elapsed.ToString("mm':'ss':'f"));
                return letovycas;
            }

        }

        private  int lastsecond = -100;
        private int lastsecond_preroundgroup_actual = -100;
        private int lastsecond_precompetitors_actual = -100;


        private int lastsecond_preroundgroup_next = -100;
        private int lastsecond_precompetitors_next = -100;

        private int _lastsecond = -100;

        private int lastsecond_prep = -100;
        private int _lastsecond_prep = -100;

        private string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

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


                if (timer_main.Elapsed.Seconds != lastsecond & BIND_MAINTIME_ISRUNNING == true)
                {
                    //Console.WriteLine("ROZDIL - timer_main.Elapsed.Seconds:" + timer_main.Elapsed.Seconds);
                    //Console.WriteLine("ROZDIL - lastsecond:" + lastsecond);
                    lastsecond = timer_main.Elapsed.Seconds;

                    if (BIND_TYPEOFCLOCK == "PRE_MAIN")
                    {
                        _lastsecond = -(Math.Abs(MODEL_CONTEST_SOUNDS_MAIN[0].VALUE) - timer_main.Elapsed.Seconds);





                        if (lastsecond == BIND_LETOVYCAS_MAX)
                        {
                            BIND_LETOVYCAS_MAX = MODEL_CONTEST_RULES[0].BASEROUNDMAXTIME;
                            BIND_PROGRESS_1 = 0;
                            BIND_TYPEOFCLOCK = "MAIN";
                            timer_main.Restart();

                        }
                        else{
                            playsound_by_time("main", _lastsecond);
                        }
                    }
                    else
                    {
                        _lastsecond = (timer_main.Elapsed.Minutes*60) + timer_main.Elapsed.Seconds;
                        playsound_by_time("main", _lastsecond);

                        if (_lastsecond == _tmp_casspusteniautomatickehopripravnehocasu & BIND_SQL_AUTO_USEPREPTIME == true & BIND_SQL_AUTO_RUNPREPTIMENEXTROUND==true)
                        {
                            BIND_PREP_AUDIO_MAN_AUTO = false;
                            clock_PREP_start();
                        }

                        if (last_second_main_time == _lastsecond)
                        {
                            clock_MAIN_stop();
                        }

                    }
                    Console.WriteLine("last_second_main_time:" + last_second_main_time);

                    Console.WriteLine("cas:" + _lastsecond);
                    DateTime xxx = DateTime.Parse(BIND_SQL_AUTO_PREPTIMESTART);
                    Console.WriteLine("datetime:" + ((xxx.Hour*60)+ xxx.Minute));



                }

                //BIND_LETOVYCAS = Convert.ToSingle(timer_main.Elapsed.TotalSeconds);

            }


        }


        public float BIND_FINAL_LETOVYCAS
        {
            get
            {
                return BIND_FINAL_LETOVYCAS_value;
            }

            set
            {

                BIND_FINAL_PROGRESS_1 = value;
                BIND_FINAL_LETOVYCAS_value = value; OnPropertyChanged("BIND_FINAL_LETOVYCAS"); OnPropertyChanged("BIND_FINAL_LETOVYCAS_STRING");


                if (timer_final_main.Elapsed.Seconds != lastsecond & BIND_FINAL_MAINTIME_ISRUNNING == true)
                {
                    //Console.WriteLine("ROZDIL - timer_main.Elapsed.Seconds:" + timer_main.Elapsed.Seconds);
                    //Console.WriteLine("ROZDIL - lastsecond:" + lastsecond);
                    lastsecond = timer_final_main.Elapsed.Seconds;

                    if (BIND_TYPEOFCLOCK == "PRE_MAIN")
                    {
                        _lastsecond = -(Math.Abs(MODEL_CONTEST_SOUNDS_FINAL_MAIN[0].VALUE) - timer_final_main.Elapsed.Seconds);





                        if (lastsecond == BIND_FINAL_LETOVYCAS_MAX)
                        {
                            BIND_FINAL_LETOVYCAS_MAX = MODEL_CONTEST_RULES[0].FINALROUNDMAXTIME;
                            BIND_FINAL_PROGRESS_1 = 0;
                            BIND_TYPEOFCLOCK = "MAIN";
                            timer_final_main.Restart();

                        }
                        else
                        {
                            playsound_by_time("main_final", _lastsecond);
                        }
                    }
                    else
                    {
                        _lastsecond = (timer_final_main.Elapsed.Minutes * 60) + timer_final_main.Elapsed.Seconds;
                        playsound_by_time("main_final", _lastsecond);

                        if (_lastsecond == _tmp_casspusteniautomatickehopripravnehocasu_final & BIND_SQL_AUTO_USEPREPTIME == true & BIND_SQL_AUTO_RUNPREPTIMENEXTROUND_FINAL == true)
                        {
                            BIND_PREP_AUDIO_FINAL_MAN_AUTO = false;
                            clock_FINAL_PREP_start();
                        }

                        if (last_second_final_main_time == _lastsecond)
                        {
                            clock_FINAL_MAIN_stop();
                        }
                        
                    }
                    Console.WriteLine("cas_final:" + _lastsecond);
                    DateTime xxx = DateTime.Parse(BIND_SQL_AUTO_PREPTIMESTART);
                    Console.WriteLine("datetime_final:" + ((xxx.Hour * 60) + xxx.Minute));



                }

                //BIND_LETOVYCAS = Convert.ToSingle(timer_main.Elapsed.TotalSeconds);

            }


        }




        public float BIND_LETOVYCAS_PREP
        {
            get
            {
                return BIND_LETOVYCAS_PREP_value;
            }

            set
            {

                BIND_PROGRESS_PREP_1 = value;
                Console.WriteLine("BIND_PROGRESS_PREP_1" + BIND_PROGRESS_PREP_1);
                BIND_LETOVYCAS_PREP_value = value; OnPropertyChanged("BIND_LETOVYCAS_PREP"); OnPropertyChanged("BIND_LETOVYCAS_PREP_STRING");


                if (timer_prep.Elapsed.Seconds != lastsecond_prep & BIND_PREPTIME_ISRUNNING == true)
                {
                    lastsecond_prep = timer_prep.Elapsed.Seconds;

             
                   _lastsecond_prep = (timer_prep.Elapsed.Minutes * 60) + timer_prep.Elapsed.Seconds;
                   playsound_by_time("prep", _lastsecond_prep);
                    if (_lastsecond_prep == _tmp_casspusteniautomatickehohlavnihocasu & BIND_SQL_AUTO_USEPREPTIME == true & BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME==true)
                    {


                        if (BIND_PREP_AUDIO_MAN_AUTO == true)
                        {
                            if (BIND_AUDIO_PREPTIME_MANUAL_NEXT == true) { FUNCTION_MOVE_GROUP_UP_DOWN(+1); clock_MAIN_start(); } else { clock_MAIN_start(); }
                        }
                        else
                        {
                            if (BIND_AUDIO_PREPTIME_AUTO_NEXT == true) { FUNCTION_MOVE_GROUP_UP_DOWN(+1); clock_MAIN_start(); } else { clock_MAIN_start(); }
                        }


                    }

                    if (last_second_prep_time == _lastsecond_prep)
                    {
                        clock_PREP_stop();
                    }
                    Console.WriteLine("prep_cas:" + _lastsecond_prep);

                }

            }


        }






        public float BIND_FINAL_LETOVYCAS_PREP
        {
            get
            {
                return BIND_FINAL_LETOVYCAS_PREP_value;
            }

            set
            {

                BIND_FINAL_PROGRESS_PREP_1 = value;
                Console.WriteLine("BIND_FINAL_PROGRESS_PREP_1" + BIND_FINAL_PROGRESS_PREP_1);
                BIND_FINAL_LETOVYCAS_PREP_value = value; OnPropertyChanged("BIND_FINAL_LETOVYCAS_PREP"); OnPropertyChanged("BIND_FINAL_LETOVYCAS_PREP_STRING");


                if (timer_final_prep.Elapsed.Seconds != lastsecond_prep & BIND_FINAL_PREPTIME_ISRUNNING == true)
                {
                    lastsecond_prep = timer_final_prep.Elapsed.Seconds;


                    _lastsecond_prep = (timer_final_prep.Elapsed.Minutes * 60) + timer_final_prep.Elapsed.Seconds;
                    playsound_by_time("prep_final", _lastsecond_prep);
                    if (_lastsecond_prep == _tmp_casspusteniautomatickehohlavnihocasu_final & BIND_SQL_AUTO_USEPREPTIME == true & BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME_FINAL == true)
                    {


                        if (BIND_PREP_AUDIO_FINAL_MAN_AUTO == true)
                        {
                            if (BIND_AUDIO_FINAL_PREPTIME_MANUAL_NEXT == true) { FUNCTION_MOVE_FINAL_ROUND(+1); clock_FINAL_MAIN_start(); } else { clock_FINAL_MAIN_start(); }
                        }
                        else
                        {
                            if (BIND_AUDIO_FINAL_PREPTIME_AUTO_NEXT == true) { FUNCTION_MOVE_FINAL_ROUND(+1); clock_FINAL_MAIN_start(); } else { clock_FINAL_MAIN_start(); }
                        }


                    }

                    if (last_second_final_prep_time == _lastsecond_prep)
                    {
                        clock_FINAL_PREP_stop();
                    }
                    Console.WriteLine("FINALprep_cas:" + _lastsecond_prep);

                }

            }


        }


        void playsound_by_time(string what, int second)
        {

            Console.WriteLine("what_playing:" + what);
            if (BIND_USEAUDIO == true)
            {
                if (what == "main")
                {
                    var listItem = MODEL_CONTEST_SOUNDS_MAIN.SingleOrDefault(i => i.VALUE == second);
                    if (listItem != null)
                    {

                        //listItem.CATEGORY
                        wav_maintime[listItem.CATEGORY].Position = 0;
                        Console.WriteLine("Playing:" + listItem.TEXTVALUE);
                        //maintimewaveout[listItem.CATEGORY].Init(wav_maintime[listItem.CATEGORY]);
                        if (listItem.TEXTVALUE.Contains("---") is true)
                        {
                            if (BIND_SQL_AUDIO_RNDGRPFLIGHT == true & listItem.TEXTVALUE== "---SAY-RND-GRP---" )
                            {
                                clock_DYNAMIC_ROUNDGROUP_ACTUAL_start();
                            }

                            if (BIND_SQL_AUDIO_COMPNUMBERS == true & listItem.TEXTVALUE == "---SAY-COMPETITORS---")
                            {
                                clock_DYNAMIC_COMPETITORS_ACTUAL_start();
                            }

                            if (BIND_SQL_AUDIO_FUNKYMOD == true & listItem.TEXTVALUE == "---FUNKY---")
                            {
                                maintimewaveout[listItem.CATEGORY].Play();
                                maintimewaveout[listItem.CATEGORY].PlaybackStopped += new EventHandler<StoppedEventArgs>(player_PlaybackStopped);
                            }

                        }

                        else
                        {
                            maintimewaveout[listItem.CATEGORY].Play();
                            maintimewaveout[listItem.CATEGORY].PlaybackStopped += new EventHandler<StoppedEventArgs>(player_PlaybackStopped);
                        }


                        //SoundDB[listItem.CATEGORY].MediaEnded += new EventHandler(Media_Ended);
                    }


                }




                if (what == "prep")
                {
                    var listItem = MODEL_CONTEST_SOUNDS_PREP.SingleOrDefault(i => i.VALUE == second);
                    if (listItem != null)
                    {
                        wav_preptime[listItem.CATEGORY].Position = 0;
                        Console.WriteLine("Playing_PREPTIME:" + listItem.CATEGORY);
                        //maintimewaveout[listItem.CATEGORY].Init(wav_maintime[listItem.CATEGORY]);


                        if (listItem.TEXTVALUE.Contains("---") is true)
                        {
                            if (BIND_SQL_AUDIO_RNDGRPPREP == true & listItem.TEXTVALUE == "---SAY-RND-GRP---")
                            {
                                Console.WriteLine("---SAY-RND-GRP---");

                                if (BIND_PREP_AUDIO_MAN_AUTO == true) //pokud je pusteno rucne
                                {
                                    if (BIND_AUDIO_PREPTIME_MANUAL_NEXT == false)
                                    {
                                        clock_DYNAMIC_ROUNDGROUP_ACTUAL_start();
                                    }
                                    else
                                    {
                                       clock_DYNAMIC_ROUNDGROUP_NEXT_start();
                                    }

                                }
                                else //pokud je pusteno automaticky
                                {
                                    if (BIND_AUDIO_PREPTIME_AUTO_NEXT == false)
                                    {
                                        clock_DYNAMIC_ROUNDGROUP_ACTUAL_start();
                                    }
                                    else
                                    {
                                        clock_DYNAMIC_ROUNDGROUP_NEXT_start();
                                    }
                                }

                            }

                            if (BIND_SQL_AUDIO_COMPNUMBERS_PREP == true & listItem.TEXTVALUE == "---SAY-COMPETITORS---")
                            {

                                if (BIND_PREP_AUDIO_MAN_AUTO == true) //pokud je pusteno rucne
                                {
                                    if (BIND_AUDIO_PREPTIME_MANUAL_NEXT == false)
                                    {
                                        clock_DYNAMIC_COMPETITORS_ACTUAL_start();
                                    }
                                    else
                                    {
                                        clock_DYNAMIC_COMPETITORS_NEXT_start();
                                    }

                                }
                                else //pokud je pusteno automaticky
                                {
                                    if (BIND_AUDIO_PREPTIME_AUTO_NEXT == false)
                                    {
                                        clock_DYNAMIC_COMPETITORS_ACTUAL_start();
                                    }
                                    else
                                    {
                                        clock_DYNAMIC_COMPETITORS_NEXT_start();
                                    }
                                }

                            }


                        }

                        else
                        {
                            preptimewaveout[listItem.CATEGORY].Play();
                            preptimewaveout[listItem.CATEGORY].PlaybackStopped += new EventHandler<StoppedEventArgs>(player_PlaybackStopped);
                        }



                        //SoundDB[listItem.CATEGORY].MediaEnded += new EventHandler(Media_Ended);
                    }


                }


                if (what == "roundgroup_actual")
                {
                    roundgroupwav_actual[second].Play();
                    roundgroupwav_actual[second].PlaybackStopped += new EventHandler<StoppedEventArgs>(player_PlaybackStopped);
                }
                
                if (what == "roundgroup_next")
                {
                    roundgroupwav_next[second].Play();
                    roundgroupwav_next[second].PlaybackStopped += new EventHandler<StoppedEventArgs>(player_PlaybackStopped);
                }


                if (what == "competitors_actual")
                {
                    competitorswav_actual[second].Play();
                    competitorswav_actual[second].PlaybackStopped += new EventHandler<StoppedEventArgs>(player_PlaybackStopped);
                }

                if (what == "competitors_next")
                {
                    competitorswav_next[second].Play();
                    competitorswav_next[second].PlaybackStopped += new EventHandler<StoppedEventArgs>(player_PlaybackStopped);
                }


            }


            if (BIND_USEAUDIO == true)
            {
                if (what == "main_final")
                {
                    var listItem = MODEL_CONTEST_SOUNDS_FINAL_MAIN.SingleOrDefault(i => i.VALUE == second);
                    if (listItem != null)
                    {

                        //listItem.CATEGORY
                        wav_final_maintime[listItem.CATEGORY].Position = 0;
                        Console.WriteLine("Playing_final:" + listItem.TEXTVALUE);
                        //maintimewaveout[listItem.CATEGORY].Init(wav_maintime[listItem.CATEGORY]);
                        if (listItem.TEXTVALUE.Contains("---") is true)
                        {
                            if (BIND_SQL_AUDIO_RNDGRPFLIGHT == true & listItem.TEXTVALUE == "---SAY-RND-GRP---")
                            {
                                clock_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_start();
                            }

                            if (BIND_SQL_AUDIO_COMPNUMBERS == true & listItem.TEXTVALUE == "---SAY-COMPETITORS---")
                            {
                                Console.WriteLine("clock_DYNAMIC_COMPETITORS_FINAL_ACTUAL_start");
                                clock_DYNAMIC_COMPETITORS_FINAL_ACTUAL_start();
                            }

                            if (BIND_SQL_AUDIO_FUNKYMOD == true & listItem.TEXTVALUE == "---FUNKY---")
                            {
                                final_maintimewaveout[listItem.CATEGORY].Play();
                                final_maintimewaveout[listItem.CATEGORY].PlaybackStopped += new EventHandler<StoppedEventArgs>(player_PlaybackStopped);
                            }

                        }

                        else
                        {
                            final_maintimewaveout[listItem.CATEGORY].Play();
                            final_maintimewaveout[listItem.CATEGORY].PlaybackStopped += new EventHandler<StoppedEventArgs>(player_PlaybackStopped);
                        }


                        //SoundDB[listItem.CATEGORY].MediaEnded += new EventHandler(Media_Ended);
                    }


                }




                if (what == "prep_final")
                {
                    var listItem = MODEL_CONTEST_SOUNDS_FINAL_PREP.SingleOrDefault(i => i.VALUE == second);
                    if (listItem != null)
                    {
                        wav_final_preptime[listItem.CATEGORY].Position = 0;
                        Console.WriteLine("Playing_PREPTIME:" + listItem.CATEGORY);
                        //maintimewaveout[listItem.CATEGORY].Init(wav_maintime[listItem.CATEGORY]);


                        if (listItem.TEXTVALUE.Contains("---") is true)
                        {
                            if (BIND_SQL_AUDIO_RNDGRPPREP == true & listItem.TEXTVALUE == "---SAY-RND-GRP---")
                            {
                                Console.WriteLine("---SAY-RND-GRP---");

                                if (BIND_PREP_AUDIO_MAN_AUTO == true) //pokud je pusteno rucne
                                {
                                    if (BIND_AUDIO_FINAL_PREPTIME_MANUAL_NEXT == false)
                                    {
                                        clock_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_start();
                                    }
                                    else
                                    {
                                        clock_DYNAMIC_ROUNDGROUP_FINAL_NEXT_start();
                                    }

                                }
                                else //pokud je pusteno automaticky
                                {
                                    if (BIND_AUDIO_FINAL_PREPTIME_AUTO_NEXT == false)
                                    {
                                        clock_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_start();
                                    }
                                    else
                                    {
                                        clock_DYNAMIC_ROUNDGROUP_FINAL_NEXT_start();
                                    }
                                }

                            }

                            if (BIND_SQL_AUDIO_COMPNUMBERS_PREP == true & listItem.TEXTVALUE == "---SAY-COMPETITORS---")
                            {

                                if (BIND_PREP_AUDIO_FINAL_MAN_AUTO == true) //pokud je pusteno rucne
                                {
                                    if (BIND_AUDIO_FINAL_PREPTIME_MANUAL_NEXT == false)
                                    {
                                        clock_DYNAMIC_COMPETITORS_FINAL_ACTUAL_start();
                                    }
                                    else
                                    {
                                        clock_DYNAMIC_COMPETITORS_NEXT_FINAL_start();
                                    }

                                }
                                else //pokud je pusteno automaticky
                                {
                                    if (BIND_AUDIO_FINAL_PREPTIME_AUTO_NEXT == false)
                                    {
                                        clock_DYNAMIC_COMPETITORS_FINAL_ACTUAL_start();
                                    }
                                    else
                                    {
                                        clock_DYNAMIC_COMPETITORS_NEXT_FINAL_start();
                                    }
                                }

                            }


                        }

                        else
                        {
                            final_preptimewaveout[listItem.CATEGORY].Play();
                            final_preptimewaveout[listItem.CATEGORY].PlaybackStopped += new EventHandler<StoppedEventArgs>(player_PlaybackStopped);
                        }



                        //SoundDB[listItem.CATEGORY].MediaEnded += new EventHandler(Media_Ended);
                    }


                }


                if (what == "roundgroup_final_actual")
                {
                    roundgroupwav_final_actual[second].Play();
                    roundgroupwav_final_actual[second].PlaybackStopped += new EventHandler<StoppedEventArgs>(player_PlaybackStopped);
                }

                if (what == "roundgroup_final_next")
                {
                    roundgroupwav_final_next[second].Play();
                    roundgroupwav_final_next[second].PlaybackStopped += new EventHandler<StoppedEventArgs>(player_PlaybackStopped);
                }


                if (what == "competitors_final_actual")
                {
                    competitorswav_final_actual[second].Play();
                    competitorswav_final_actual[second].PlaybackStopped += new EventHandler<StoppedEventArgs>(player_PlaybackStopped);
                }

                if (what == "competitors_final_next")
                {
                    competitorswav_final_next[second].Play();
                    competitorswav_final_next[second].PlaybackStopped += new EventHandler<StoppedEventArgs>(player_PlaybackStopped);
                }


            }
        }

        void player_PlaybackStopped(object sender, StoppedEventArgs e)
        {

            WaveOutEvent button;

            button = sender as WaveOutEvent;
            button.Stop();
            //Console.WriteLine("zvuk zastaven" + button.GetPosition());

        }

        
        private string _BIND_NEWS_COUNT = SORGAIR.Properties.Lang.Lang.home_newscount + " : 4";
        public string BIND_NEWS_COUNT
        {
            get
            {
                return SORGAIR.Properties.Lang.Lang.home_newscount + " : 0";
            }

            set
            {
                _BIND_NEWS_COUNT = value; OnPropertyChanged("BIND_NEWS_COUNT");
            }

        }


        private string _BIND_VERZE_SORGU_LAST = SORGAIR.Properties.Lang.Lang.home_actualversionis + " : 0.0.2.3";
        public string BIND_VERZE_SORGU_LAST
        {
            get
            {
                OnPropertyChanged("BIND_VERZE_SORGU_NEEDUPDATE");
                return SORGAIR.Properties.Lang.Lang.home_actualversionis +  " : " +_BIND_VERZE_SORGU_LAST;
            }

            set
            {
                _BIND_VERZE_SORGU_LAST = value; OnPropertyChanged("BIND_VERZE_SORGU_LAST");
            }

        }


        private string _BIND_VERZE_SORGU_NEEDUPDATE = "0";
        public string BIND_VERZE_SORGU_NEEDUPDATE
        {
            get
            {
                if (_BIND_VERZE_SORGU_LAST == _BIND_VERZE_SORGU)
                {
                    _BIND_VERZE_SORGU_NEEDUPDATE = "0";
                }
                else
                {
                    _BIND_VERZE_SORGU_NEEDUPDATE = "1";
                }

                return _BIND_VERZE_SORGU_NEEDUPDATE;
            }

            set
            {
                _BIND_VERZE_SORGU_NEEDUPDATE = value; OnPropertyChanged("BIND_VERZE_SORGU_NEEDUPDATE");
            }

        }




        private string _BIND_VERZE_SORGU;
        public string BIND_VERZE_SORGU
        {
            get
            {
                return SORGAIR.Properties.Lang.Lang.home_yourversionis + " : " + _BIND_VERZE_SORGU;
            }

            set
            {
                _BIND_VERZE_SORGU = value; OnPropertyChanged("BIND_VERZE_SORGU"); 
            }

        }

        public int BIND_LETOVYCAS_MAX
        {
            get
            {
                return BIND_LETOVYCAS_MAX_value;
            }

            set
            {
                BIND_LETOVYCAS_MAX_value = value; OnPropertyChanged("BIND_LETOVYCAS_MAX"); OnPropertyChanged("BIND_LETOVYCAS_STRING"); Console.WriteLine("BIND_LETOVYCAS_MAX" + BIND_LETOVYCAS_MAX) ;
            }

        }


        public int BIND_FINAL_LETOVYCAS_MAX
        {
            get
            {
                return BIND_FINAL_LETOVYCAS_MAX_value;
            }

            set
            {
                BIND_FINAL_LETOVYCAS_MAX_value = value; OnPropertyChanged("BIND_FINAL_LETOVYCAS_MAX"); OnPropertyChanged("BIND_FINAL_LETOVYCAS_STRING"); Console.WriteLine("BIND_FINAL_LETOVYCAS_MAX" + BIND_FINAL_LETOVYCAS_MAX);
            }

        }

        private int _MAX_WIDTH = 50;
        public int MAX_WIDTH
        {
            get
            {
                Console.WriteLine("_MAX_WIDTH:" + _MAX_WIDTH);
                return _MAX_WIDTH;
            }

            set
            {
                _MAX_WIDTH = value; OnPropertyChanged("MAX_WIDTH");
                Console.WriteLine("_MAX_WIDTH:" + _MAX_WIDTH);

            }

        }


        public int BIND_PRE_LETOVYCAS_MAX
        {
            get
            {
                return BIND_PRE_LETOVYCAS_MAX_value;
            }

            set
            {
                BIND_PRE_LETOVYCAS_MAX_value = value; OnPropertyChanged("BIND_PRE_LETOVYCAS_MAX"); OnPropertyChanged("BIND_PRE_LETOVYCAS_STRING"); 
            }

        }

        public int BIND_LETOVYCAS_PREP_MAX
        {
            get
            {
                return BIND_LETOVYCAS_PREP_MAX_value;
            }

            set
            {
                BIND_LETOVYCAS_PREP_MAX_value = value; OnPropertyChanged("BIND_LETOVYCAS_PREP_MAX"); OnPropertyChanged("BIND_LETOVYCAS_PREP_STRING");
            }

        }

        public int BIND_FINAL_LETOVYCAS_PREP_MAX
        {
            get
            {
                return BIND_FINAL_LETOVYCAS_PREP_MAX_value;
            }

            set
            {
                BIND_FINAL_LETOVYCAS_PREP_MAX_value = value; OnPropertyChanged("BIND_FINAL_LETOVYCAS_PREP_MAX"); OnPropertyChanged("BIND_FINAL_LETOVYCAS_PREP_STRING");
            }

        }

        public float BIND_FINAL_PROGRESS_1
        {
            get { return BIND_FINAL_PROGRESS_1_value; }
            set { BIND_FINAL_PROGRESS_1_value = value; OnPropertyChanged("BIND_FINAL_PROGRESS_1"); }
        }

        public float BIND_FINAL_PROGRESS_PREP_1
        {
            get { return BIND_FINAL_PROGRESS_PREP_1_value; }
            set { BIND_FINAL_PROGRESS_PREP_1_value = value; OnPropertyChanged("BIND_FINAL_PROGRESS_PREP_1"); }
        }


        public float BIND_PROGRESS_1
        {
            get { return BIND_PROGRESS_1_value; }
            set { BIND_PROGRESS_1_value = value; OnPropertyChanged("BIND_PROGRESS_1"); }
        }

        public float BIND_PROGRESS_PREP_1
        {
            get { return BIND_PROGRESS_PREP_1_value; }
            set { BIND_PROGRESS_PREP_1_value = value; OnPropertyChanged("BIND_PROGRESS_PREP_1"); }
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


        bool _BIND_MEN_WOMAN = false;
        public bool BIND_MEN_WOMAN
        {
            get { return _BIND_MEN_WOMAN; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='man_woman'"); _BIND_MEN_WOMAN = value; OnPropertyChanged("BIND_USEAUDIO"); }
        }


        public string BIND_VYBRANEKOLOMENU
        {
            get { return SORGAIR.Properties.Lang.Lang.menu_selectedflight + ":" + BIND_SELECTED_ROUND + "/" + BIND_SELECTED_GROUP; }
        }

        public string BIND_SELECTED_ROUND_DESC
        {
            get { return  SQL_READSOUTEZDATA("select Name from Rounds where id = "+ BIND_SELECTED_ROUND,""); }
        }

        public string BIND_SELECTED_GROUP_DESC
        {
            get { return SQL_READSOUTEZDATA("select Name from Groups where masterround = " + BIND_SELECTED_ROUND + " and groupnumber =  " + BIND_SELECTED_GROUP, ""); }
        }


        public string BIND_SELECTED_FINAL_ROUND_DESC
        {
            get { return SQL_READSOUTEZDATA("select Name from Rounds where id = " + BIND_SELECTED_FINAL_ROUND, ""); }
        }

        public string BIND_SELECTED_FINAL_GROUP_DESC
        {
            get { return SQL_READSOUTEZDATA("select Name from Groups where masterround = " + BIND_SELECTED_FINAL_ROUND + " and groupnumber =  " + BIND_SELECTED_FINAL_GROUP, ""); }
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
            set {
                BIND_POCETSOUTEZICICH_value = value; 
                OnPropertyChanged("BIND_POCETSOUTEZICICH");
                
            }
        }




        private int _BIND_ROUNDS_IN_RESULTS= 1;
        public int BIND_ROUNDS_IN_RESULTS
        {
            get { return _BIND_ROUNDS_IN_RESULTS; }

            set
            {
                _BIND_ROUNDS_IN_RESULTS = value;
                OnPropertyChanged("BIND_ROUNDS_IN_RESULTS");

                if (BIND_ROUNDS_IN_RESULTS > BIND_SQL_SOUTEZ_DELETES)
                {
                    BIND_SKRTEJ_ENABLED = true;
                }
                else
                {
                    BIND_SKRTEJ_ENABLED = false;
                }

            }
        }






        public int BIND_SQL_SOUTEZ_ROUNDS
        {
            get { return BIND_SQL_SOUTEZ_ROUNDS_value; }

            set {
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Rounds'");
                if (value > BIND_SQL_SOUTEZ_ROUNDS_value)
                {
                    SQL_SAVESOUTEZDATA("insert into rounds (id,name,type,lenght,zadano) values (" + value + ",'kolo:a_" + value + "','auto',600,0);");

                    for (int i = 1; i < BIND_SQL_SOUTEZ_GROUPS + 1; i++)
                    {
                        SQL_SAVESOUTEZDATA("insert into groups (id,name,type,lenght,zadano, masterround, groupnumber) values (null, 'autogrp_" + i + "','auto',600,0, " + value + " ," + i + ");");
                    }

                }
                SQL_SAVESOUTEZDATA("delete from rounds where id > " + value + ";");
                SQL_SAVESOUTEZDATA("delete from groups where masterround > " + value + " and type = 'auto' ;");


               


                BIND_SQL_SOUTEZ_ROUNDS_value = value;
                OnPropertyChanged("BIND_SQL_SOUTEZ_ROUNDS");
                FUNCTION_LOAD_MATRIX_FILES();
                FUNCTION_JETREBAROZLOSOVAT_OVER();
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
                    SQL_SAVESOUTEZDATA("delete from groups where groupnumber > " + value + " and type='auto';");
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

                FUNCTION_JETREBAROZLOSOVAT_OVER();
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

        public int BIND_SELECTED_FINAL_STARTPOINT
        {
            get { return _BIND_SELECTED_FINAL_STARTPOINT; }
            set { _BIND_SELECTED_FINAL_STARTPOINT = value; OnPropertyChanged("BIND_SELECTED_FINAL_STARTPOINT"); Console.WriteLine("BIND_SELECTED_FINAL_STARTPOINT:" + BIND_SELECTED_FINAL_STARTPOINT); }
        }


        private int _BIND_VIEWED_STARTPOINT;
        public int BIND_VIEWED_STARTPOINT
        {
            get { return _BIND_VIEWED_STARTPOINT; }
            set { _BIND_VIEWED_STARTPOINT = value; OnPropertyChanged("BIND_VIEWED_STARTPOINT"); Console.WriteLine("BIND_VIEWED_STARTPOINT:" + BIND_VIEWED_STARTPOINT); }
        }


        public int BIND_SELECTED_GROUP
        {
            get { return BIND_SELECTED_GROUP_value; }
            set { BIND_SELECTED_GROUP_value = value; OnPropertyChanged("BIND_SELECTED_GROUP"); OnPropertyChanged("BIND_VYBRANEKOLOMENU"); OnPropertyChanged("BIND_SELECTED_ROUND_DESC"); OnPropertyChanged("BIND_SELECTED_GROUP_DESC"); Console.WriteLine("BIND_SELECTED_GROUP" + BIND_SELECTED_GROUP); }
        }



        public int BIND_VIEWED_ROUND
        {
            get { return BIND_VIEWED_ROUND_value; }
            set { BIND_VIEWED_ROUND_value = value; OnPropertyChanged("BIND_VIEWED_ROUND"); Console.WriteLine("BIND_VIEWED_ROUND:" + BIND_VIEWED_ROUND); FUNCTION_SELECTED_ROUND_USERS(BIND_VIEWED_ROUND, BIND_VIEWED_GROUP); OnPropertyChanged("BIND_START_SELECTED_ROUND_GROUP"); }
        }

        private int _BIND_SELECTED_FINAL_ROUND = 1;
        public int BIND_SELECTED_FINAL_ROUND
        {
            get {
                Console.WriteLine("_BIND_SELECTED_FINAL_ROUND:" + _BIND_SELECTED_FINAL_ROUND);
                return _BIND_SELECTED_FINAL_ROUND; 
            }
            set {
                _BIND_SELECTED_FINAL_ROUND = value; 
                Console.WriteLine("BIND_SELECTED_FINAL_ROUND:" + BIND_SELECTED_FINAL_ROUND);
                OnPropertyChanged("BIND_SELECTED_FINAL_ROUND");
                OnPropertyChanged("BIND_SELECTED_FINAL_ROUND_DESC"); OnPropertyChanged("BIND_SELECTED_FINAL_GROUP_DESC");
            }
        }

        private int _BIND_SELECTED_FINAL_GROUP = 1;
        public int BIND_SELECTED_FINAL_GROUP
        {
            get
            {
                Console.WriteLine("_BIND_SELECTED_FINAL_GROUP:" + _BIND_SELECTED_FINAL_GROUP);
                return _BIND_SELECTED_FINAL_GROUP;
            }
            set
            {
                _BIND_SELECTED_FINAL_GROUP = value;
                Console.WriteLine("BIND_SELECTED_FINAL_GROUP:" + BIND_SELECTED_FINAL_GROUP);
                OnPropertyChanged("BIND_SELECTED_FINAL_GROUP");
                OnPropertyChanged("BIND_SELECTED_FINAL_ROUND_DESC"); OnPropertyChanged("BIND_SELECTED_FINAL_GROUP_DESC");
            }
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
            set { BIND_VIEWED_GROUP_value = value; OnPropertyChanged("BIND_VIEWED_GROUP"); Console.WriteLine("BIND_VIEWED_GROUP" + BIND_VIEWED_GROUP); FUNCTION_SELECTED_ROUND_USERS(BIND_VIEWED_ROUND, BIND_VIEWED_GROUP); OnPropertyChanged("BIND_START_SELECTED_ROUND_GROUP"); }
        }

        public string BIND_START_SELECTED_ROUND_GROUP
        {
            get { return "Odstartovat let: " + BIND_VIEWED_ROUND + "/" + BIND_VIEWED_GROUP; }
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

                FUNCTION_JETREBAROZLOSOVAT_OVER();

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
            set { 


                if (value > BIND_SQL_SOUTEZ_ROUNDSFINALE_value)
                {
                    SQL_SAVESOUTEZDATA("insert into final_rounds (id,name,type,lenght,zadano) values (" + value + ",'finále:_" + value + "','auto',900,0);");
                    //SQL_SAVESOUTEZDATA("insert into final_groups (id,name,type,lenght,zadano, masterround, groupnumber) values (null, 'autofinalgrp_1','auto',900,0, " + value + " ,1);");

                }
                SQL_SAVESOUTEZDATA("delete from final_rounds where id > " + value + ";");
                //SQL_SAVESOUTEZDATA("delete from final_groups where masterround > " + value + ";");

                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Roundsfinale'");
                BIND_SQL_SOUTEZ_ROUNDSFINALE_value = value;
                OnPropertyChanged("BIND_SQL_SOUTEZ_ROUNDSFINALE");

                FUNCTION_JETREBAROZLOSOVAT_OVER_FINAL();


                //BIND_SQL_SOUTEZ_ROUNDS_value = value;
                //OnPropertyChanged("BIND_SQL_SOUTEZ_ROUNDS");
                //FUNCTION_LOAD_MATRIX_FILES();
                //FUNCTION_JETREBAROZLOSOVAT_OVER();




            }
        }

        public int BIND_SQL_SOUTEZ_STARTPOINTSFINALE
        {
            get { return BIND_SQL_SOUTEZ_STARTPOINTSFINALE_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Startpointsfinale'"); BIND_SQL_SOUTEZ_STARTPOINTSFINALE_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_STARTPOINTSFINALE"); }
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
        private string _BIND_SQL_SOUTEZ_DBFILE;
        public string BIND_SQL_SOUTEZ_DBFILE
        {
            get { return _BIND_SQL_SOUTEZ_DBFILE; }
            set { _BIND_SQL_SOUTEZ_DBFILE = value; OnPropertyChanged("BIND_SQL_SOUTEZ_DBFILE"); }
        }
        public string BIND_SQL_SOUTEZ_LOKACE
        {
            get { return "Lokace : " + BIND_SQL_SOUTEZ_LOKACE_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Location'"); BIND_SQL_SOUTEZ_LOKACE_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_LOKACE"); }
        }



        private string _BIND_NEWCONTEST_CATEGORY = "F5J";
        public string BIND_NEWCONTEST_CATEGORY
        {
            get { return _BIND_NEWCONTEST_CATEGORY; }
            set {  _BIND_NEWCONTEST_CATEGORY = value; OnPropertyChanged("BIND_NEWCONTEST_CATEGORY"); }
        }


        private string _BIND_NEWCONTEST_NAME = "Nová soutěž";
        public string BIND_NEWCONTEST_NAME
        {
            get { return _BIND_NEWCONTEST_NAME; }
            set { _BIND_NEWCONTEST_NAME = value; OnPropertyChanged("BIND_NEWCONTEST_NAME"); }
        }

        private string _BIND_NEWCONTEST_LOCATION = "Kde soutěž bude";
        public string BIND_NEWCONTEST_LOCATION
        {
            get { return _BIND_NEWCONTEST_LOCATION; }
            set { _BIND_NEWCONTEST_LOCATION = value; OnPropertyChanged("BIND_NEWCONTEST_LOCATION"); }
        }


        private string _BIND_NEWCONTEST_DATE = DateTime.Now.ToString("dd.MM.yyyy"); // case sensitive;
        public string BIND_NEWCONTEST_DATE
        {
            get { return _BIND_NEWCONTEST_DATE; }
            set { _BIND_NEWCONTEST_DATE = value; OnPropertyChanged("BIND_NEWCONTEST_DATE"); }
        }


        private string _BIND_NEWCONTEST_CATEGORY_ONLINE = "F5J";
        public string BIND_NEWCONTEST_CATEGORY_ONLINE
        {
            get { return _BIND_NEWCONTEST_CATEGORY_ONLINE; }
            set { _BIND_NEWCONTEST_CATEGORY_ONLINE = value; OnPropertyChanged("BIND_NEWCONTEST_CATEGORY_ONLINE"); }
        }




        private string _BIND_NEWCONTEST_NAME_ONLINE = "Nová soutěž";
        public string BIND_NEWCONTEST_NAME_ONLINE
        {
            get { return _BIND_NEWCONTEST_NAME_ONLINE; }
            set { _BIND_NEWCONTEST_NAME_ONLINE = value; OnPropertyChanged("BIND_NEWCONTEST_NAME_ONLINE"); }
        }


        private string _BIND_NEWCONTEST_ID_ONLINE = "0";
        public string BIND_NEWCONTEST_ID_ONLINE
        {
            get { return _BIND_NEWCONTEST_ID_ONLINE; }
            set { _BIND_NEWCONTEST_ID_ONLINE = value; OnPropertyChanged("BIND_NEWCONTEST_ID_ONLINE"); }
        }

        private string _BIND_NEWCONTEST_LOCATION_ONLINE = "Kde soutěž bude";
        public string BIND_NEWCONTEST_LOCATION_ONLINE
        {
            get { return _BIND_NEWCONTEST_LOCATION_ONLINE; }
            set { _BIND_NEWCONTEST_LOCATION_ONLINE = value; OnPropertyChanged("BIND_NEWCONTEST_LOCATION_ONLINE"); }
        }


        private string _BIND_NEWCONTEST_DATE_ONLINE = DateTime.Now.ToString("dd.MM.yyyy"); // case sensitive;
        public string BIND_NEWCONTEST_DATE_ONLINE
        {
            get { return _BIND_NEWCONTEST_DATE_ONLINE; }
            set { _BIND_NEWCONTEST_DATE_ONLINE = value; OnPropertyChanged("BIND_NEWCONTEST_DATE_ONLINE"); }
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


        public bool BIND_SQL_SOUTEZ_ENTRYSTYLEPOINTSORLENGHT
        {
            get { return BIND_SQL_SOUTEZ_ENTRYSTYLEPOINTSORLENGHT_value; }
            set
            {


                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Entrystylepointsorlenght'");
                BIND_SQL_SOUTEZ_ENTRYSTYLEPOINTSORLENGHT_value = value;
                if (value == true)
                {
                    ScoreEntryType2 = "TEXTVALUE";
                }
                else
                {
                    ScoreEntryType2 = "LENGHT";
                }
                OnPropertyChanged("BIND_SQL_SOUTEZ_ENTRYSTYLEPOINTSORLENGHT");
                OnPropertyChanged("ScoreEntryType2");
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

        public bool BIND_AUDIO_PREPTIME_MANUAL_NEXT
        {
            get { return _BIND_AUDIO_PREPTIME_MANUAL_NEXT; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Audio_preptime_man_next'"); _BIND_AUDIO_PREPTIME_MANUAL_NEXT = value; OnPropertyChanged("BIND_AUDIO_PREPTIME_MANUAL_NEXT"); }
        }


        public bool BIND_PREP_AUDIO_MAN_AUTO
        {
            get { return _BIND_PREP_AUDIO_MAN_AUTO; }
            set { _BIND_PREP_AUDIO_MAN_AUTO = value; OnPropertyChanged("BIND_PREP_AUDIO_MAN_AUTO"); }
        }


        public bool BIND_AUDIO_PREPTIME_AUTO_NEXT
        {
            get { return _BIND_AUDIO_PREPTIME_AUTO_NEXT; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Audio_preptime_auto_next'"); _BIND_AUDIO_PREPTIME_AUTO_NEXT = value; OnPropertyChanged("BIND_AUDIO_PREPTIME_AUTO_NEXT"); }
        }




        public bool BIND_AUDIO_FINAL_PREPTIME_MANUAL_NEXT
        {
            get { return _BIND_AUDIO_FINAL_PREPTIME_MANUAL_NEXT; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Audio_preptime_final_man_next'"); _BIND_AUDIO_FINAL_PREPTIME_MANUAL_NEXT = value; OnPropertyChanged("BIND_AUDIO_FINAL_PREPTIME_MANUAL_NEXT"); }
        }


        public bool BIND_PREP_AUDIO_FINAL_MAN_AUTO
        {
            get { return _BIND_PREP_FINAL_AUDIO_MAN_AUTO; }
            set { _BIND_PREP_FINAL_AUDIO_MAN_AUTO = value; OnPropertyChanged("BIND_PREP_AUDIO_FINAL_MAN_AUTO"); }
        }


        public bool BIND_AUDIO_FINAL_PREPTIME_AUTO_NEXT
        {
            get { return _BIND_AUDIO_FINAL_PREPTIME_AUTO_NEXT; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Audio_preptime_final_auto_next'"); _BIND_AUDIO_FINAL_PREPTIME_AUTO_NEXT = value; OnPropertyChanged("BIND_AUDIO_FINAL_PREPTIME_AUTO_NEXT"); }
        }





        public bool BIND_SQL_AUDIO_COMPNUMBERS_PREP
        {
            get { return BIND_SQL_AUDIO_COMPNUMBERS_PREP_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Audiocumpetitornumberprep'"); BIND_SQL_AUDIO_COMPNUMBERS_PREP_value = value; OnPropertyChanged("BIND_SQL_AUDIO_COMPNUMBERS_PREP"); }
        }


        public bool BIND_SQL_AUDIO_RNDGRPFLIGHT
        {
            get { return BIND_SQL_AUDIO_RNDGRPFLIGHT_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Rndgrpflight'"); BIND_SQL_AUDIO_RNDGRPFLIGHT_value = value; OnPropertyChanged("BIND_SQL_AUDIO_RNDGRPFLIGHT"); }
        }
        private bool _BIND_SQL_AUDIO_FUNKYMOD = false;
        public bool BIND_SQL_AUDIO_FUNKYMOD
        {
            get { return _BIND_SQL_AUDIO_FUNKYMOD; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='funkymod'"); _BIND_SQL_AUDIO_FUNKYMOD = value; OnPropertyChanged("BIND_SQL_AUDIO_FUNKYMOD"); }
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
            get { return _BIND_SQL_AUTO_RUNPREPTIMENEXTROUND; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Runnextroundafterpreptime'"); _BIND_SQL_AUTO_RUNPREPTIMENEXTROUND = value; OnPropertyChanged("BIND_SQL_AUTO_RUNPREPTIMENEXTROUND"); }
        }

        public bool BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME
        {
            get { return _BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Runpreptime'"); _BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME = value; OnPropertyChanged("BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME"); }
        }

        public bool BIND_SQL_AUTO_RUNPREPTIMENEXTROUND_FINAL
        {
            get { return _BIND_SQL_AUTO_RUNPREPTIMENEXTROUND_FINAL; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Runnextroundafterpreptime_final'"); _BIND_SQL_AUTO_RUNPREPTIMENEXTROUND_FINAL = value; OnPropertyChanged("BIND_SQL_AUTO_RUNPREPTIMENEXTROUND_FINAL"); }
        }

        public bool BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME_FINAL
        {
            get { return _BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME_FINAL; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Runpreptime_final'"); _BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME_FINAL = value; OnPropertyChanged("BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME_FINAL"); }
        }


        public string BIND_SQL_AUTO_PREPTIMELENGHT
        {
            get { return BIND_SQL_AUTO_PREPTIMELENGHT_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Preptimelenght'"); BIND_SQL_AUTO_PREPTIMELENGHT_value = value; OnPropertyChanged("BIND_SQL_AUTO_PREPTIMELENGHT"); }
        }
        public string BIND_SQL_AUTO_PREPTIMESTART
        {
            get { return _BIND_SQL_AUTO_PREPTIMESTART; }
            set { 
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Preptimestart'"); 
                _BIND_SQL_AUTO_PREPTIMESTART = value;
                DateTime xxx = DateTime.Parse(BIND_SQL_AUTO_PREPTIMESTART);
                _tmp_casspusteniautomatickehopripravnehocasu = ((xxx.Hour * 60) + xxx.Minute);
                OnPropertyChanged("BIND_SQL_AUTO_PREPTIMESTART");
            }
        }

        public string BIND_SQL_AUTO_MAINTIMESTART
        {
            get { return _BIND_SQL_AUTO_MAINTIMESTART; }
            set
            {
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Maintimestart'");
                _BIND_SQL_AUTO_MAINTIMESTART = value;
                DateTime xxx = DateTime.Parse(BIND_SQL_AUTO_MAINTIMESTART);
                _tmp_casspusteniautomatickehohlavnihocasu = ((xxx.Hour * 60) + xxx.Minute);
                OnPropertyChanged("BIND_SQL_AUTO_MAINTIMESTART");
            }
        }

        public string BIND_SQL_AUTO_PREPTIMESTART_FINAL
        {
            get { return _BIND_SQL_AUTO_PREPTIMESTART_FINAL; }
            set
            {
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Preptimestart_final'");
                _BIND_SQL_AUTO_PREPTIMESTART_FINAL = value;
                DateTime xxx = DateTime.Parse(BIND_SQL_AUTO_PREPTIMESTART_FINAL);
                _tmp_casspusteniautomatickehopripravnehocasu_final = ((xxx.Hour * 60) + xxx.Minute);
                OnPropertyChanged("BIND_SQL_AUTO_PREPTIMESTART_FINAL");
            }
        }

        public string BIND_SQL_AUTO_MAINTIMESTART_FINAL
        {
            get { return _BIND_SQL_AUTO_MAINTIMESTART_FINAL; }
            set
            {
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Maintimestart_final'");
                _BIND_SQL_AUTO_MAINTIMESTART_FINAL = value;
                DateTime xxx = DateTime.Parse(BIND_SQL_AUTO_MAINTIMESTART_FINAL);
                _tmp_casspusteniautomatickehohlavnihocasu_final = ((xxx.Hour * 60) + xxx.Minute);
                OnPropertyChanged("BIND_SQL_AUTO_MAINTIMESTART_FINAL");
            }
        }


        public int _tmp_casspusteniautomatickehohlavnihocasu;

        public int _tmp_casspusteniautomatickehopripravnehocasu;

        public int _tmp_casspusteniautomatickehohlavnihocasu_final;

        public int _tmp_casspusteniautomatickehopripravnehocasu_final;






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


            else if (KTERADB == "RULES")
            {
                DBSORG_Connection = new SQLiteConnection("Data Source=" + directory + "/Data/config/rules.db;");
                DBSORG_Connection.Open();

            }

            else
            {
                try
                {
                    DBSOUTEZ_Connection = new SQLiteConnection("Data Source=" + directory + "/Data/"+ KTERADB + ".db;");
                    DBSOUTEZ_Connection.Open();
                    BIND_SQL_SOUTEZ_DBFILE = KTERADB;
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
                    test2.Add(new TodoItem2() { name = tmpname, userid = tmpid, startpoint = sqlite_datareader.GetInt32(2).ToString(), startpoint_data = kolo + "_" + group + "_" + tmpstp + "_" + tmpid });
                    Console.WriteLine("startpoint_data:" + kolo + "_" + group + "_" + tmpstp + "_" + tmpid);

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

            if (KTERADB == "RULES")
            {
                DBSORG_Connection.Close();
            }


            if (KTERADB == "SOUTEZ")
            {
               if (DBSOUTEZ_Connection != null)
                {
                    DBSOUTEZ_Connection.Close();
                }

              
            }

            Console.WriteLine("SQL_OPENCONNECTION [CLOSE] : " + KTERADB);

        }


        public string SQL_READSORGDATA(string sqltext, string kamulozitvysledek)
        {


            string vysledek = "";

            int _results_autoincrement = 0;



            SQLiteCommand command = new SQLiteCommand(sqltext, DBSORG_Connection);

            Console.WriteLine("SQL_READSORGDATA [SQL] : " + sqltext + " >>>> " + kamulozitvysledek);


            SQLiteDataReader sqlite_datareader;
            try
            {
                sqlite_datareader = command.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    _results_autoincrement = _results_autoincrement + 1;







                    if (kamulozitvysledek == "pozadi")
                    {
                        string myreader = sqlite_datareader.GetString(0);
                        Console.WriteLine("SQL_READSORGDATA [READ DATA] : " + myreader + " >>>> " + kamulozitvysledek);
                        vysledek = myreader;

                        pouzitepozadi = Int32.Parse(vysledek);
                        FUNCTION_Changebackgroundcolor();
                    }


                    if (kamulozitvysledek == "copysounds")
                    {
                      
                        SQL_SAVESORGDATA("insert into Sounds(category, id, second, filename, filedesc) select "+ sqlite_datareader.GetInt32(0) + ", " + sqlite_datareader.GetInt32(1) + ", second, filename, filedesc from Sounds where id = 7; ");
                    }




                    if (kamulozitvysledek == "popredi")
                    {
                        string myreader = sqlite_datareader.GetString(0);
                        Console.WriteLine("SQL_READSORGDATA [READ DATA] : " + myreader + " >>>> " + kamulozitvysledek);
                        vysledek = myreader;

                        pouzitabarva = Int32.Parse(vysledek);
                        FUNCTION_Changeforegroundcolor();
                    }

                    if (kamulozitvysledek == "selectedsearch")
                    {
                        string myreader = sqlite_datareader.GetString(0);
                        Console.WriteLine("SQL_READSORGDATA [READ DATA] : " + myreader + " >>>> " + kamulozitvysledek);
                        vysledek = myreader;

                        Console.WriteLine("selectedsearch");
                    }

                    if (kamulozitvysledek == "get_categories")
                    {


                        var _categories = new MODEL_Contests_categories()
                        {

                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
                            CATEGORY = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("category"))
                        };
                        MODEL_CONTESTS_CATEGORIES.Add(_categories);

                    }


                    if (kamulozitvysledek == "get_landing")
                    {





                        var landing = new MODEL_CATEGORY_LANDING()
                        {

                            CATEGORY = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("category")),
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
                            VALUE = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("value")),
                            TEXTVALUE = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("textvalue")),
                            LENGHT = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("lenght")),
                            TODEL = 0

                        };
                        MODEL_CATEGORY_LANDING.Add(landing);
                        vysledek = kamulozitvysledek;
                    }

                    if (kamulozitvysledek == "get_sounds")
                    {





                        var sound = new MODEL_CATEGORY_LANDING()
                        {

                            CATEGORY = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("category")),
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
                            VALUE = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("second")),
                            TEXTVALUE = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")),
                            LENGHT = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filedesc")),
                            TODEL = 0

                        };
                        MODEL_CATEGORY_SOUNDS.Add(sound);
                        vysledek = kamulozitvysledek;
                    }


                    if (kamulozitvysledek == "get_soundlist")
                    {





                        var soundlist = new MODEL_CATEGORY_LANDING()
                        {

                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
                            CATEGORY = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("category")),
                            TEXTVALUE = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("soundname")),
                            LENGHT = "",
                            TODEL = 0

                        };
                        MODEL_CONTESTS_SOUNDLISTS.Add(soundlist);
                        vysledek = kamulozitvysledek;
                    }


                    if (kamulozitvysledek == "get_penalisation")
                    {





                        var landing = new MODEL_CATEGORY_PENALISATIONS()
                        {

                            CATEGORY = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("category")),
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
                            VALUE = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("value")),
                            TEXTVALUE = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("textvalue")),
                            DELETE_LANDING = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_landing")),
                            DELETE_TIME = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_time")),
                            DELETE_ALL = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_all")),
                            TODEL = 0

                        };
                        MODEL_CATEGORY_PENALISATION.Add(landing);
                        vysledek = kamulozitvysledek;
                    }


                    if (kamulozitvysledek == "get_penalisationglobal")
                    {





                        var landing = new MODEL_CATEGORY_PENALISATIONS()
                        {

                            CATEGORY = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("category")),
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
                            VALUE = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("value")),
                            TEXTVALUE = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("textvalue")),
                            DELETE_LANDING = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_landing")),
                            DELETE_TIME = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_time")),
                            DELETE_ALL = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_all")),
                            TODEL = 0

                        };
                        MODEL_CATEGORY_PENALISATIONGLOBAL.Add(landing);
                        vysledek = kamulozitvysledek;
                    }


                    if (kamulozitvysledek == "get_bonuspoints")
                    {





                        var landing = new MODEL_CATEGORY_PENALISATIONS()
                        {

                            CATEGORY = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("category")),
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
                            VALUE = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("value")),
                            TEXTVALUE = "bonus",
                            DELETE_LANDING = "d1",
                            DELETE_TIME = "d2",
                            DELETE_ALL = "d3",
                            TODEL = 0

                        };
                        MODEL_CATEGORY_BONUSPOINTS.Add(landing);
                        vysledek = kamulozitvysledek;
                    }


                    

                    if (kamulozitvysledek == "get_rules")
                    {





                        var rules = new MODEL_category_Rules()
                        {

                            ID = 0,
                            CATEGORY = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("category")),
                            TIME1UNDER = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("points_under_limit1")),
                            TIME1OVER = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("points_over_limit1")),
                            TIME1LIMIT = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("limit1")),
                            TIME2UNDER = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("points_under_limit2")),
                            TIME2OVER = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("points_over_limit2")),
                            TIME2LIMIT = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("limit2")),
                            HEIGHTUNDER = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("heightunder")),
                            HEIGHTOVER = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("heightover")),
                            HEIGHTLIMIT = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("heightlimit")),
                            ENTRYHEIGHT = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("entryheight"))),
                            DELETEALL1 = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_all1"))),
                            DELETEALL2 = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_all2"))),
                            DELETETIME1 = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_time1"))),
                            DELETETIME2 = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_time2"))),
                            DELETELANDING1 = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_landing1"))),
                            DELETELANDING2 = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_landing2"))),
                            SUBFROMLANDING1 = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("sub_from_landing1")),
                            SUBFROMLANDING2 = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("sub_from_landing2")),
                            SUBFROMTIME1 = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("sub_from_time1")),
                            SUBFROMTIME2 = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("sub_from_time2")),
                            BASEROUNDLENGHT = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("BASEROUNDLENGHT")),
                            BASEROUNDMAXTIME = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("BASEROUNDMAXTIME")),
                            FINALROUNDLENGHT = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("FINALROUNDLENGHT")),
                            FINALROUNDMAXTIME = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("FINALROUNDMAXTIME")),
                            BONUSONLYFORFINALIST = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("bonusonlyforfinalist"))),
                            RECTO1000FROMABSMAX = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("RECTO1000FROMABSMAX")))



                        };
                        MODEL_CATEGORY_RULES.Add(rules);
                        vysledek = kamulozitvysledek;
                    }


                    if (kamulozitvysledek == "")
                    {
                        try
                        {
                            Console.WriteLine("SQL RETURN TYPE:" + sqlite_datareader.GetFieldType(0));


                            if (sqlite_datareader.GetFieldType(0) == typeof(Int64))
                            {
                                Int64 myreader = sqlite_datareader.GetInt64(0);
                                Console.WriteLine("SQL_READSORGDATA [READ DATA] : " + myreader + " >>>> " + kamulozitvysledek);
                                vysledek = myreader.ToString();
                            }

                            if (sqlite_datareader.GetFieldType(0) == typeof(Decimal))
                            {
                                Decimal myreader = sqlite_datareader.GetDecimal(0);
                                Console.WriteLine("SQL_READSORGDATA [READ DATA] : " + myreader + " >>>> " + kamulozitvysledek);
                                vysledek = myreader.ToString();
                            }


                            if (sqlite_datareader.GetFieldType(0) == typeof(Double))
                            {
                                double myreader = sqlite_datareader.GetDouble(0);
                                Console.WriteLine("SQL_READSORGDATA [READ DATA] : " + myreader + " >>>> " + kamulozitvysledek);
                                vysledek = myreader.ToString();
                            }



                            if (sqlite_datareader.GetFieldType(0) == typeof(string))
                            {
                                string myreader = sqlite_datareader.GetString(0);
                                Console.WriteLine("SQL_READSORGDATA [READ DATA] : " + myreader + " >>>> " + kamulozitvysledek);
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
                Console.WriteLine("Message: " + myException.Message + "\n");

            }





            return vysledek;





        }





        public string SQL_READSOUTEZDATA_GETALL(string sqltext, string delimiter)
        {
            Console.WriteLine("SQL_READSOUTEZDATA_ALL [SQL] : " + sqltext );
            string vysledek = "";
            SQLiteCommand command = new SQLiteCommand(sqltext, DBSOUTEZ_Connection);

            SQLiteDataReader sqlite_datareader;
            try
            {
                sqlite_datareader = command.ExecuteReader();
                while (sqlite_datareader.Read())
                { 
                vysledek = vysledek + sqlite_datareader.GetString(0) + delimiter;
                }
            }

            catch (SQLiteException myException)
            {
                Console.WriteLine("SQL_READSOUTEZDATA_ALL [ERROR] : " + myException.Message + "\n");
            }



            return vysledek.Remove(vysledek.Length - (delimiter.Length));
        }



        public string SQL_READSOUTEZDATA(string sqltext, string kamulozitvysledek)
        {
            int _results_autoincrement = 0;
            double _results_scoreompare = 1000 * (BIND_SQL_SOUTEZ_ROUNDS- BIND_SQL_SOUTEZ_DELETES);

            double _results_scoreompare_do_kola = 0;

            if (_ZOBRAZIT_ZAKLADNI_VYSLEDKY_S_SKRTACKAMA == true)
            {
                _results_scoreompare_do_kola = 1000 * (BIND_ROUNDS_IN_RESULTS- BIND_SQL_SOUTEZ_DELETES);
            }
            else
            {
                _results_scoreompare_do_kola = 1000 * BIND_ROUNDS_IN_RESULTS;
            }


            double _results_scoreompare_final = 1000 * BIND_SQL_SOUTEZ_ROUNDSFINALE;

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
                            FREQID = int.Parse(sqlite_datareader.GetString(14)),
                            AGECATID = int.Parse(sqlite_datareader.GetString(15))

                        };
                        Players.Add(player);
                        vysledek = "get_players";

                    }

                    if (kamulozitvysledek == "get_finalresults_users")
                    {


                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var directory = System.IO.Path.GetDirectoryName(path);



                        var _Players_results = new MODEL_Player_baseresults()
                        {
                            POSITION = _results_autoincrement.ToString(),
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname")) + "  " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname")),
                            RAWSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalrawscore")),
                            GPEN = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("gpen")),
                            PREPSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore")),
                            PREPSCOREDIFF = Math.Round(sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore")) - _results_scoreompare_final, 2).ToString("0.00"),
                            AGECAT = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("agecatstring")),
                            NATLIC = "asd",
                            FAILIC = "bdd",

                            RND1RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=101  and refly='False'", ""),
                            RND1RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=101  and refly='False'", ""),

                            RND2RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=102  and refly='False'", ""),
                            RND2RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=102  and refly='False'", ""),


                            RND3RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=103  and refly='False'", ""),
                            RND3RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=103  and refly='False'", ""),

                            RND4RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=104  and refly='False'", ""),
                            RND4RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=104  and refly='False'", ""),

                            RND5RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=105  and refly='False'", ""),
                            RND5RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=105  and refly='False'", ""),



                            FLAG = directory + "/flags/" + SQL_READSOUTEZDATA("select country from users where id = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), "") + ".png"

                        };

                        Console.WriteLine("AAAAAAAAAAAAAA" + SQL_READSOUTEZDATA("select prep from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=1", ""));

                        Players_Finalresults.Add(_Players_results);
                        _results_scoreompare = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore"));
                        _results_scoreompare_final = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore"));
                        vysledek = kamulozitvysledek;

                    }





                    if (kamulozitvysledek == "get_statistics_maxheights")
                    {


                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var directory = System.IO.Path.GetDirectoryName(path);



                        var _Players_statistics = new MODEL_Player_statistics()
                        {
                            POSITION = _results_autoincrement.ToString(),
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("lastname")) + " " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("firstname")),
                            FLAG = directory + "/flags/" + SQL_READSOUTEZDATA("select country from users where id = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), "") + ".png",
                            DATA = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("rawmaxheight")),
                            DATA2 = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("summaxheight")),
                            DATA3 = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("averagemaxheight")),
                            DATA4 = SQL_READSOUTEZDATA_GETALL("select cast(height as text) from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), " | "),
                            RECORDS = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("zaznamu")).ToString()
              


                        };




                        Players_statistics.Add(_Players_statistics);
                        vysledek = kamulozitvysledek;

                    }


                    if (kamulozitvysledek == "get_statistics_minheights")
                    {


                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var directory = System.IO.Path.GetDirectoryName(path);



                        var _Players_statistics = new MODEL_Player_statistics()
                        {
                            POSITION = _results_autoincrement.ToString(),
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("lastname")) + " " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("firstname")),
                            FLAG = directory + "/flags/" + SQL_READSOUTEZDATA("select country from users where id = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), "") + ".png",
                            DATA = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("minheight")),
                            DATA2str = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("sumprep")),
                            DATA3 = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("podil")),
                            DATA4 = SQL_READSOUTEZDATA_GETALL("select height || '/' || prep from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), " | "),
                            RECORDS = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("zaznamu")).ToString()



                        };




                        Players_statistics.Add(_Players_statistics);
                        vysledek = kamulozitvysledek;

                    }






                    if (kamulozitvysledek == "get_statistics_timevsheight")
                    {


                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var directory = System.IO.Path.GetDirectoryName(path);



                        var _Players_statistics = new MODEL_Player_statistics()
                        {
                            POSITION = _results_autoincrement.ToString(),
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("lastname")) + " " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("firstname")),
                            FLAG = directory + "/flags/" + SQL_READSOUTEZDATA("select country from users where id = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), "") + ".png",
                            DATAstr = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("prumernycasnakolo")),
                            DATA2 = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("prumernavyskanakolo")),
                            DATA3 = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("na10minutjetreba")),
                            DATA4 = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("ze100metrunalita")),
                            RECORDS = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("zaznamu")).ToString()



                        };




                        Players_statistics.Add(_Players_statistics);
                        vysledek = kamulozitvysledek;

                    }

                    if (kamulozitvysledek == "get_statistics_flighttime")
                    {


                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var directory = System.IO.Path.GetDirectoryName(path);



                        var _Players_statistics = new MODEL_Player_statistics()
                        {
                            POSITION = _results_autoincrement.ToString(),
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("lastname")) + " " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("firstname")),
                            FLAG = directory + "/flags/" + SQL_READSOUTEZDATA("select country from users where id = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), "") + ".png",
                            DATAstr = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("totaltime")),
                            DATA2str = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("averagetime")),
                            DATA4 = SQL_READSOUTEZDATA_GETALL("select strftime('%M:%S',time    ('00:00:00', (minutes*60+seconds) || ' seconds')) from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), " | "),
                            RECORDS = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("zaznamu")).ToString()



                        };




                        Players_statistics.Add(_Players_statistics);
                        vysledek = kamulozitvysledek;

                    }



                    if (kamulozitvysledek == "get_statistics_averageheights")
                    {


                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var directory = System.IO.Path.GetDirectoryName(path);

                      

                        var _Players_statistics = new MODEL_Player_statistics()
                        {
                            POSITION = _results_autoincrement.ToString(),
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("lastname")) + " "  + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("firstname")),
                            FLAG = directory + "/flags/" + SQL_READSOUTEZDATA("select country from users where id = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), "") + ".png",
                            DATA = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("height")),
                            DATA2 =sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("sumheight")),
                            DATA4 = SQL_READSOUTEZDATA_GETALL("select cast(height as text) from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid"))," | "),
                            RECORDS = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("zaznamu")).ToString()

                        };




                            Players_statistics.Add(_Players_statistics);
                        vysledek = kamulozitvysledek;

                    }


                    if (kamulozitvysledek == "get_statistics_averagelandings")
                    {


                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var directory = System.IO.Path.GetDirectoryName(path);



                        var _Players_statistics = new MODEL_Player_statistics()
                        {
                            POSITION = _results_autoincrement.ToString(),
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("lastname")) + " " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("firstname")),
                            FLAG = directory + "/flags/" + SQL_READSOUTEZDATA("select country from users where id = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), "") + ".png",
                            DATA = sqlite_datareader.GetDecimal (sqlite_datareader.GetOrdinal("pristani")),
                            DATA2 = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("sumpristani")),
                            DATA4 = SQL_READSOUTEZDATA_GETALL("select cast(landing as text) from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), " | "),
                            RECORDS = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("zaznamu")).ToString()



                        };




                        Players_statistics.Add(_Players_statistics);
                        vysledek = kamulozitvysledek;

                    }

                    if (kamulozitvysledek == "get_baseresults_users")
                    {


                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var directory = System.IO.Path.GetDirectoryName(path);

                        string tmp_hvezdickafinalisty;

                        if (_results_autoincrement<= BIND_SQL_SOUTEZ_STARTPOINTSFINALE)
                        {
                            tmp_hvezdickafinalisty = "* ";
                        }
                        else
                        {
                            tmp_hvezdickafinalisty = "";
                        }

                    var _Players_Baseresults = new MODEL_Player_baseresults()
                        {
                            POSITION = tmp_hvezdickafinalisty + _results_autoincrement.ToString(),
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")),
                        AGECAT = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("agecatstring")),

                        PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname")) + "  " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname")),
                            RAWSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalrawscore")),
                        GPEN= sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("gpen")),
                        PREPSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore")),
                        PREPSCOREDIFF = Math.Round(sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore")) - _results_scoreompare_do_kola,2).ToString("0.00"),
                        RND1RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=1 and refly='False'", ""),
                        RND1RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=1 and refly='False'", ""),
                        RND1RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=1 and refly='False'", ""),

                        RND2RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=2 and refly='False'", ""),
                        RND2RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=2 and refly='False'", ""),
                        RND2RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=2 and refly='False'", ""),


                        RND3RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=3 and refly='False'", ""),
                        RND3RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=3 and refly='False'", ""),
                        RND3RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=3 and refly='False'", ""),

                        RND4RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=4 and refly='False'", ""),
                        RND4RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=4 and refly='False'", ""),
                        RND4RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=4 and refly='False'", ""),

                        RND5RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=5 and refly='False'", ""),
                        RND5RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=5 and refly='False'", ""),
                        RND5RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=5 and refly='False'", ""),

                        RND6RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=6 and refly='False'", ""),
                        RND6RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=6 and refly='False'", ""),
                        RND6RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=6 and refly='False'", ""),

                        RND7RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=7 and refly='False'", ""),
                        RND7RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=7 and refly='False'", ""),
                        RND7RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=7 and refly='False'", ""),

                        RND8RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=8 and refly='False'", ""),
                        RND8RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=8 and refly='False'", ""),
                        RND8RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=8 and refly='False'", ""),

                        RND9RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=9 and refly='False'", ""),
                        RND9RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=9 and refly='False'", ""),
                        RND9RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=9 and refly='False'", ""),

                        RND10RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=10 and refly='False'", ""),
                        RND10RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=10 and refly='False'", ""),
                        RND10RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=10 and refly='False'", ""),

                        FLAG = directory + "/flags/" + SQL_READSOUTEZDATA("select country from users where id = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), "") + ".png"

                        };

                    


                        Players_Baseresults.Add(_Players_Baseresults);
                        _results_scoreompare_do_kola = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore"));
                        vysledek = kamulozitvysledek;

                    }





                    if (kamulozitvysledek == "get_baseresults_users_complete")
                    {


                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var directory = System.IO.Path.GetDirectoryName(path);
                        string tmpbonus = "0";



                        if (SQL_READSOUTEZDATA("select ifnull(value,0) from bonuspoints where id = " + _results_autoincrement, "") != "")
                        {

                        }



                        if (SQL_READSOUTEZDATA("select bonusonlyforfinalist from rules", "") == "True")
                        {
                            if (_results_autoincrement < BIND_SQL_SOUTEZ_STARTPOINTSFINALE+1 & BIND_SQL_SOUTEZ_ROUNDSFINALE > 0)
                            {
                                if (SQL_READSOUTEZDATA("select ifnull(value,0) from bonuspoints where id = " + _results_autoincrement, "") != "")
                                {
                                    tmpbonus = SQL_READSOUTEZDATA("select ifnull(value,0) from bonuspoints where id = " + _results_autoincrement, "");
                                }
                            }
                        
                        }
                        else
                        {
                            if (SQL_READSOUTEZDATA("select ifnull(value,0) from bonuspoints where id = " + _results_autoincrement, "") != "")
                            {
                                tmpbonus = SQL_READSOUTEZDATA("select ifnull(value,0) from bonuspoints where id = " + _results_autoincrement, "");
                            }


                        }



                        double maxscorefor1000 = 1;
                        if (SQL_READSOUTEZDATA("select RECTO1000FROMABSMAX from rules","") == "True")
                        {
                            maxscorefor1000 = double.Parse(SQL_READSOUTEZDATA("select max(((select sum(prep) from score s2 where s2.userid = s1.userid and rnd < 100 and skrtacka='False' and refly='False' ))) overalscore_base from score s1 left join users U on S1.userid = U.id", ""));
                        }
                        else
                        {
                            maxscorefor1000 = BIND_SQL_SOUTEZ_ROUNDS*1000;
                        }


                        var _Players_Baseresults_complete = new MODEL_Player_baseresults_complete()
                        {
                            POSITION = _results_autoincrement,
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname")) + "  " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname")),
                            AGECAT = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("agecatstring")),
                            NATLIC = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("naclic")),
                            FAILIC = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("failic")),

                            RAWSCORE_BASE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalrawscore_base")),
                            PREPSCORE_BASE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore_base")),
                            RAWSCORE_FINAL = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalrawscore_fin")),
                            PREPSCORE_FINAL = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore_fin")),
                            GPEN = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("gpen")),
                            PREPSCOREDIFF_BASE = Math.Round(sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore_base")) - _results_scoreompare, 2).ToString("0.00"),
                            PREPSCOREDIFF_FINAL = Math.Round(sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore_fin")) - _results_scoreompare_final, 2).ToString("0.00"),


                            TO_1000 = Math.Round((sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore_base")) / maxscorefor1000) *1000, 2).ToString("0.00"),
                            
                            BONUS_POINTS = Double.Parse(tmpbonus),

                        RND1RES_SCORE_F = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=101 and refly='False'", ""),
                            RND1RES_DATA_F = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=101 and refly='False'", ""),

                            RND2RES_SCORE_F = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=102 and refly='False'", ""),
                            RND2RES_DATA_F = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=102 and refly='False'", ""),


                            RND3RES_SCORE_F = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=103 and refly='False'", ""),
                            RND3RES_DATA_F = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=103 and refly='False'", ""),

                            RND4RES_SCORE_F = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=104 and refly='False'", ""),
                            RND4RES_DATA_F = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=104 and refly='False'", ""),

                            RND5RES_SCORE_F = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=105 and refly='False'", ""),
                            RND5RES_DATA_F = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=105 and refly='False'", ""),



                            RND1RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=1 and refly='False'", ""),
                            RND1RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=1 and refly='False'", ""),
                            RND1RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=1 and refly='False'", ""),

                            RND2RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=2 and refly='False'", ""),
                            RND2RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=2 and refly='False'", ""),
                            RND2RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=2 and refly='False'", ""),


                            RND3RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=3 and refly='False'", ""),
                            RND3RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=3 and refly='False'", ""),
                            RND3RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=3 and refly='False'", ""),

                            RND4RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=4 and refly='False'", ""),
                            RND4RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=4 and refly='False'", ""),
                            RND4RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=4 and refly='False'", ""),

                            RND5RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=5 and refly='False'", ""),
                            RND5RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=5 and refly='False'", ""),
                            RND5RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=5 and refly='False'", ""),

                            RND6RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=6 and refly='False'", ""),
                            RND6RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=6 and refly='False'", ""),
                            RND6RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=6 and refly='False'", ""),

                            RND7RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=7 and refly='False'", ""),
                            RND7RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=7 and refly='False'", ""),
                            RND7RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=7 and refly='False'", ""),

                            RND8RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=8 and refly='False'", ""),
                            RND8RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=8 and refly='False'", ""),
                            RND8RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=8 and refly='False'", ""),

                            RND9RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=9 and refly='False'", ""),
                            RND9RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=9 and refly='False'", ""),
                            RND9RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=9 and refly='False'", ""),

                            RND10RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=10 and refly='False'", ""),
                            RND10RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=10 and refly='False'", ""),
                            RND10RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=10 and refly='False'", ""),


                            FLAG = directory + "/flags/" + SQL_READSOUTEZDATA("select country from users where id = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), "") + ".png"

                        };

                        Console.WriteLine("AAAAAAAAAAAAAA" + SQL_READSOUTEZDATA("select prep from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=1", ""));





                        

                        Players_Baseresults_Complete.Add(_Players_Baseresults_complete);
                        _results_scoreompare = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore_base"));
                        Console.WriteLine("_results_scoreompare" + _results_scoreompare);
                        _results_scoreompare_final = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore_fin"));
                        vysledek = kamulozitvysledek;

                    }


                    if (kamulozitvysledek == "get_baseresults_teams")
                    {


                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var directory = System.IO.Path.GetDirectoryName(path);



                        var _Players_Baseresults = new MODEL_Player_baseresults()
                        {
                            POSITION = _results_autoincrement.ToString(),
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("teamid")),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("team")),
                            RAWSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalrawscore")),

                            GPEN = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("gpen")),
                            PREPSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore")),
                            PREPSCOREDIFF = Math.Round(sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore")) - _results_scoreompare, 2).ToString("0.00"),
                            RND1RES_SCORE = SQL_READSOUTEZDATA("select ifnull(sum(prep),0) from score S left JOIN users U ON U.id = S.userid left join teams T on T.id = U.team where  rnd=1 and s.skrtacka = 'False' and s.refly='False' and t.id = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("teamid")), ""),
                            RND1RES_DATA = SQL_READSOUTEZDATA("select ifnull(strftime('%M:%S', ((sum(minutes)*60)+sum(seconds))/86400.0) ||' / '||sum(landing)||' / '||sum(height),0)  from score S left JOIN users U ON U.id = S.userid left join teams T on T.id = U.team where  rnd=1  and s.skrtacka = 'False' and s.refly='False' and t.id = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("teamid")), ""),

                            RND2RES_SCORE = SQL_READSOUTEZDATA("select ifnull(sum(prep),0) from score S left JOIN users U ON U.id = S.userid left join teams T on T.id = U.team where  rnd=2 and s.skrtacka = 'False' and s.refly='False'  and t.id = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("teamid")), ""),
                            RND2RES_DATA = SQL_READSOUTEZDATA("select ifnull(strftime('%M:%S', ((sum(minutes)*60)+sum(seconds))/86400.0) ||' / '||sum(landing)||' / '||sum(height),0)  from score S left JOIN users U ON U.id = S.userid left join teams T on T.id = U.team where  rnd=2 and s.skrtacka = 'False' and s.refly='False'  and t.id= " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("teamid")), ""),

                            RND3RES_SCORE = SQL_READSOUTEZDATA("select ifnull(sum(prep),0) from score S left JOIN users U ON U.id = S.userid left join teams T on T.id = U.team where  rnd=3 and s.skrtacka = 'False' and s.refly='False'  and t.id= " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("teamid")), ""),
                            RND3RES_DATA = SQL_READSOUTEZDATA("select ifnull(strftime('%M:%S', ((sum(minutes)*60)+sum(seconds))/86400.0) ||' / '||sum(landing)||' / '||sum(height),0)  from score S left JOIN users U ON U.id = S.userid left join teams T on T.id = U.team where  rnd=3 and s.skrtacka = 'False' and s.refly='False'  and t.id= " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("teamid")), ""),

                            RND4RES_SCORE = SQL_READSOUTEZDATA("select ifnull(sum(prep),0) from score S left JOIN users U ON U.id = S.userid left join teams T on T.id = U.team where  rnd=4 and s.skrtacka = 'False' and s.refly='False'  and t.id= " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("teamid")), ""),
                            RND4RES_DATA = SQL_READSOUTEZDATA("select ifnull(strftime('%M:%S', ((sum(minutes)*60)+sum(seconds))/86400.0) ||' / '||sum(landing)||' / '||sum(height),0)  from score S left JOIN users U ON U.id = S.userid left join teams T on T.id = U.team where  rnd=4 and s.skrtacka = 'False' and s.refly='False'  and t.id= " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("teamid")), ""),

                            RND5RES_SCORE = SQL_READSOUTEZDATA("select ifnull(sum(prep),0) from score S left JOIN users U ON U.id = S.userid left join teams T on T.id = U.team where  rnd=5 and s.skrtacka = 'False' and s.refly='False'  and t.id= " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("teamid")), ""),
                            RND5RES_DATA = SQL_READSOUTEZDATA("select ifnull(strftime('%M:%S', ((sum(minutes)*60)+sum(seconds))/86400.0) ||' / '||sum(landing)||' / '||sum(height),0)  from score S left JOIN users U ON U.id = S.userid left join teams T on T.id = U.team where  rnd=5 and s.skrtacka = 'False' and s.refly='False'  and t.id= " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("teamid")), ""),

                            RND6RES_SCORE = SQL_READSOUTEZDATA("select ifnull(sum(prep),0) from score S left JOIN users U ON U.id = S.userid left join teams T on T.id = U.team where  rnd=6 and s.skrtacka = 'False' and s.refly='False'  and t.id = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("teamid")), ""),
                            RND6RES_DATA = SQL_READSOUTEZDATA("select ifnull(strftime('%M:%S', ((sum(minutes)*60)+sum(seconds))/86400.0) ||' / '||sum(landing)||' / '||sum(height),0)  from score S left JOIN users U ON U.id = S.userid left join teams T on T.id = U.team where  rnd=6 and s.skrtacka = 'False' and s.refly='False'  and t.id= " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("teamid")), "")

                        };

                        Players_Baseresults.Add(_Players_Baseresults);
                        _results_scoreompare = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore"));
                        vysledek = kamulozitvysledek;

                    }


                    if (kamulozitvysledek == "get_landings")
                    {

                        var _landings = new Timer_landings()
                        {
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
                            VALUE = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("value")),
                            TEXTVALUE = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("textvalue")),
                            LENGHT = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("lenght")),
                        };
                        BINDING_Timer_listoflandings.Add(_landings);

                    }


                    if (kamulozitvysledek == "get_contest_soundlist")
                    {

                        var _sndlst = new SoundList()
                        {
                            Id =  sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
                            SoundName = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("soundname"))
                        };
                        BINDING_SoundList.Add(_sndlst);

                    }

                    if (kamulozitvysledek == "get_penalisationlocal")
                    {
                        Console.WriteLine("PENLOCPENLOC");

                        var _penalisation = new Timer_penalisation()
                        {
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
                            VALUE = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("value")),
                            TEXTVALUE = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("textvalue")) + " ["+ sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("value"))+ "]",
                            DELETE_TIME= sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_time")),
                            DELETE_LANDING = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_landing")),
                            DELETE_ALL = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_all")),

                        };
                        Console.WriteLine("PENLOC" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("textvalue")));
                        BINDING_Timer_listofpenalisationlocal.Add(_penalisation);
                    }


                    if (kamulozitvysledek == "get_contest_sound_main")
                    {
                        Console.WriteLine("get_contest_sound_main");

                        int i = MODEL_CONTEST_SOUNDS_MAIN.Count;
                        Console.WriteLine("ABCDE:" + i);
                        byte[] fileContent;

                        if (sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) == "---FUNKY---")
                        {

                            var rand = new Random();
                            var files = Directory.GetFiles("Audio\\FUNKYMODE\\", "*.wav");
                            var rndname = files[rand.Next(files.Length)];
                            fileContent = File.ReadAllBytes(rndname);
                            Console.WriteLine("Random loaded FUNKY audio is:" + rndname);
                        }
                        else
                        {
                            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) + ".wav");
                        }

                        wav_maintime[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));  

                        maintimewaveout[i] = new WaveOutEvent();
                        maintimewaveout[i].Init(wav_maintime[i]);
                        Console.WriteLine("NANAUDIO _ " + "Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) + ".wav");
                        
                        var _sound = new MODEL_CATEGORY_LANDING()
                        {
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
                            VALUE = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("second")),
                            TEXTVALUE = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")),
                            CATEGORY = i,
                            LENGHT = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filedesc")),
                            TODEL = 0
                        };

                        Console.WriteLine("add to MODEL_CONTEST_SOUNDS_MAIN:" + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("second")));
                        MODEL_CONTEST_SOUNDS_MAIN.Add(_sound);
                    }

                    if (kamulozitvysledek == "get_contest_sound_final_main")
                    {
                        Console.WriteLine("get_contest_sound_final_main");

                        int i = MODEL_CONTEST_SOUNDS_FINAL_MAIN.Count;

                        byte[] fileContent;

                        if (sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) == "---FUNKY---")
                        {

                            var rand = new Random();
                            var files = Directory.GetFiles("Audio\\FUNKYMODE\\", "*.wav");
                            var rndname = files[rand.Next(files.Length)];
                            fileContent = File.ReadAllBytes(rndname);
                            Console.WriteLine("Random loaded FUNKY audio is:" + rndname);
                        }
                        else
                        {
                            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) + ".wav");
                        }

                        wav_final_maintime[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));

                        final_maintimewaveout[i] = new WaveOutEvent();
                        final_maintimewaveout[i].Init(wav_final_maintime[i]);
                        Console.WriteLine("FINALNANAUDIO _ " + "Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) + ".wav");

                        var _sound = new MODEL_CATEGORY_LANDING()
                        {
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
                            VALUE = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("second")),
                            TEXTVALUE = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")),
                            CATEGORY = i,
                            LENGHT = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filedesc")),
                            TODEL = 0
                        };


                        MODEL_CONTEST_SOUNDS_FINAL_MAIN.Add(_sound);
                    }


                    if (kamulozitvysledek == "get_contest_sound_prep")
                    {
                        Console.WriteLine("get_contest_sound_prep");

                        int i = MODEL_CONTEST_SOUNDS_PREP.Count;

                        byte[] fileContent;

                        if (sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) == "---FUNKY---")
                        {
                            fileContent = File.ReadAllBytes("Audio\\FUNKYMODE\\western.wav");
                        }
                        else
                        {
                            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) + ".wav");
                        }


                        wav_preptime[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));

                        preptimewaveout[i] = new WaveOutEvent();
                        preptimewaveout[i].Init(wav_preptime[i]);
                        Console.WriteLine("NANAUDIO _ " + "Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) + ".wav");

                        var _sound = new MODEL_CATEGORY_LANDING()
                        {
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
                            VALUE = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("second")),
                            TEXTVALUE = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")),
                            CATEGORY = i,
                            LENGHT = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filedesc")),
                            TODEL = 0
                        };


                        MODEL_CONTEST_SOUNDS_PREP.Add(_sound);
                    }


                    if (kamulozitvysledek == "get_contest_sound_final_prep")
                    {
                        Console.WriteLine("get_contest_sound_final_prep");

                        int i = MODEL_CONTEST_SOUNDS_FINAL_PREP.Count;

                        byte[] fileContent;

                        if (sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) == "---FUNKY---")
                        {
                            fileContent = File.ReadAllBytes("Audio\\FUNKYMODE\\western.wav");
                        }
                        else
                        {
                            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) + ".wav");
                        }


                        wav_final_preptime[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));

                        final_preptimewaveout[i] = new WaveOutEvent();
                        final_preptimewaveout[i].Init(wav_final_preptime[i]);
                        Console.WriteLine("NANAUDIO _ " + "Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) + ".wav");

                        var _sound = new MODEL_CATEGORY_LANDING()
                        {
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
                            VALUE = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("second")),
                            TEXTVALUE = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")),
                            CATEGORY = i,
                            LENGHT = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filedesc")),
                            TODEL = 0
                        };


                        MODEL_CONTEST_SOUNDS_FINAL_PREP.Add(_sound);
                    }

                    if (kamulozitvysledek == "get_penalisationglobal")
                    {

                        var _penalisationglobal = new Timer_penalisation()
                        {
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
                            VALUE = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("value")),
                            TEXTVALUE = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("textvalue")) + " - [" + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("value")) + "]",
                            DELETE_TIME = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_time")),
                            DELETE_LANDING = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_landing")),
                            DELETE_ALL = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_all")),
                        };
                        BINDING_Timer_listofpenalisationglobal.Add(_penalisationglobal);

                    }


                    if (kamulozitvysledek == "get_Players_Actual_Flying")
                    {


                        bool _ISENABLED = true;
                        if (sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")) == 0)
                        {
                            _ISENABLED = false;
                        }

                        string tmp_refly_data = "----";

                        string tmp_refly_data_from =  SQL_READSOUTEZDATA("select ifnull(r.rnd_from || '-'  || r.grp_from || '-' || r.stp_from || ' <> ' || r.rnd_to || '-' || r.grp_to || '-' || r.stp_to,'Refly') rfly from refly R where  r.rnd_from = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("rnd")) + " and r.grp_from = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("grp")) + " and r.stp_from = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("stp")), "");
                        string tmp_refly_data_to = SQL_READSOUTEZDATA("select ifnull(r.rnd_from || '-'  || r.grp_from || '-' || r.stp_from || ' <> ' || r.rnd_to || '-' || r.grp_to || '-' || r.stp_to,'Refly') rfly from refly R where  r.rnd_to = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("rnd")) + " and r.grp_to = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("grp")) + " and r.stp_to = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("stp")), "");


                        if (tmp_refly_data_from == "" & tmp_refly_data_to == "") {tmp_refly_data = "REFLY";}
                        else
                        {
                            if (tmp_refly_data_from != ""){tmp_refly_data = tmp_refly_data_from;}
                            if (tmp_refly_data_to != "") { tmp_refly_data = tmp_refly_data_to; }
                        }



                        var player_actual = new MODEL_Player_actual()
                        {
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")),
                            STARTPOINT = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("stp")),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname")) 
                            + "  " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname")) 
                            + " [" + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")) + "]" + Environment.NewLine + Environment.NewLine 
                            + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("minutes")) + ":" 
                            + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("seconds")) 
                            + Environment.NewLine + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("landing")) 
                            + Environment.NewLine  +sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("height"))
                            + Environment.NewLine + "l-pen : "+sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("pen1"))
                            +Environment.NewLine + "g-pen : " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("pen2")),
                            RAWSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("raw")),
                            ENTERED = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("entered")),
                            PREPSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("prep")),
                            ISENABLED = _ISENABLED,
                            REFLY_DATA = tmp_refly_data

                        };
                        Players_Actual_Flying.Add(player_actual);
                        vysledek = kamulozitvysledek;

                    }


                    if (kamulozitvysledek == "get_Players_Actual_final_Flying")
                    {


                        bool _ISENABLED = true;
                        if (sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")) == 0)
                        {
                            _ISENABLED = false;
                        }


                        string tmp_refly_data = "----";

                        string tmp_refly_data_from = SQL_READSOUTEZDATA("select ifnull(r.rnd_from || '-'  || r.grp_from || '-' || r.stp_from || ' <> ' || r.rnd_to || '-' || r.grp_to || '-' || r.stp_to,'Refly') rfly from refly R where  r.rnd_from = " + (sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("rnd"))+0) + " and r.grp_from = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("grp")) + " and r.stp_from = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("stp")), "");
                        string tmp_refly_data_to = SQL_READSOUTEZDATA("select ifnull(r.rnd_from || '-'  || r.grp_from || '-' || r.stp_from || ' <> ' || r.rnd_to || '-' || r.grp_to || '-' || r.stp_to,'Refly') rfly from refly R where  r.rnd_to = " + (sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("rnd"))+0) + " and r.grp_to = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("grp")) + " and r.stp_to = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("stp")), "");


                        if (tmp_refly_data_from == "" & tmp_refly_data_to == "") { tmp_refly_data = "REFLY"; }
                        else
                        {
                            if (tmp_refly_data_from != "") { tmp_refly_data = tmp_refly_data_from; }
                            if (tmp_refly_data_to != "") { tmp_refly_data = tmp_refly_data_to; }
                        }


                        var player_actual = new MODEL_Player_actual()
                        {
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")),
                            STARTPOINT = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("stp")),
                            // PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname")) + "  " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname")) + Environment.NewLine  + "[" + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID"))  + "]"+  Environment.NewLine + Environment.NewLine + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("minutes")) + ":" + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("seconds")) + Environment.NewLine + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("landing")) + Environment.NewLine + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("height")),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname"))
                            + "  " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname"))
                            + " [" + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")) + "]" + Environment.NewLine + Environment.NewLine
                            + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("minutes")) + ":"
                            + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("seconds"))
                            + Environment.NewLine + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("landing"))
                            + Environment.NewLine + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("height"))
                            + Environment.NewLine + "l-pen : " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("pen1"))
                            + Environment.NewLine + "g-pen : " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("pen2")),
                            RAWSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("raw")),
                            ENTERED = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("entered")),
                            PREPSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("prep")),
                            ISENABLED = _ISENABLED,
                            REFLY_DATA = tmp_refly_data

                        };
                        Players_Actual_Final_Flying.Add(player_actual);
                        vysledek = kamulozitvysledek;

                    }


                    if (kamulozitvysledek == "get_Players_Actual_Flying_nextforsound")
                    {

                        users_id_for_sound.Add(sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")));
                        Console.WriteLine("users_id_for_sound:" + users_id_for_sound.Count);
                        Console.WriteLine("useridsound:" + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")));
                        vysledek = kamulozitvysledek;

                    }


                    if (kamulozitvysledek == "get_Players_Actual_Flying_nextforsound_final")
                    {

                        users_id_for_sound_final.Add(sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")));
                        Console.WriteLine("users_id_for_sound_final:" + users_id_for_sound_final.Count);
                        vysledek = kamulozitvysledek;

                    }




                    if (kamulozitvysledek == "get_Players_Actual_SelectedRound")
                    {


                        bool _ISENABLED = true;
                        if (sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")) == 0)
                        {
                            _ISENABLED = false;
                        }

                        var player_actual = new MODEL_Player_actual()
                        {
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")),
                            STARTPOINT = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("stp")),
                            //PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname")) + "  " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname")) + Environment.NewLine + "[" + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")) + "]" + Environment.NewLine + Environment.NewLine + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("minutes")) + ":" + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("seconds")) + Environment.NewLine + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("landing")) + Environment.NewLine + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("height")),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname"))
                            + "  " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname"))
                            + " [" + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")) + "]" + Environment.NewLine + Environment.NewLine
                            + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("minutes")) + ":"
                            + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("seconds"))
                            + Environment.NewLine + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("landing"))
                            + Environment.NewLine + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("height"))
                            + Environment.NewLine + "l-pen : " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("pen1"))
                            + Environment.NewLine + "g-pen : " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("pen2")),
                            RAWSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("raw")),
                            ENTERED = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("entered")),
                            PREPSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("prep")),
                            ISENABLED = _ISENABLED,
                            REFLY_DATA = "RFLYDATA3"

                        };
                        Players_Actual_SelectedRound.Add(player_actual);

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


                    if (kamulozitvysledek == "get_Player_Selected_Roundlist")
                    {

                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + sqltext + " >>>> " + kamulozitvysledek);

                        var _player_selected = new MODEL_Player_selected()
                        {
                            ID = sqlite_datareader.GetInt32(0),
                            FIRSTNAME = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname")),
                            LASTNAME = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname")),
                            WHOLENAME = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname")) + " " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname"))
                        };
                        Player_Selected_Roundlist.Add(_player_selected);
                        vysledek = kamulozitvysledek;

                    }

                    if (kamulozitvysledek == "get_rules")
                    {


                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + sqltext + " >>>> " + kamulozitvysledek);



                        var rules = new MODEL_Contest_Rules()
                        {

                            ID = 0,
                            CATEGORY = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("category")),
                            TIME1UNDER= sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("points_under_limit1")),
                            TIME1OVER = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("points_over_limit1")),
                            TIME1LIMIT = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("limit1")),
                            TIME2UNDER = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("points_under_limit2")),
                            TIME2OVER = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("points_over_limit2")),
                            TIME2LIMIT = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("limit2")),
                            HEIGHTUNDER = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("heightunder")),
                            HEIGHTOVER = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("heightover")),
                            HEIGHTLIMIT = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("heightlimit")),
                            ENTRYHEIGHT = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("entryheight"))),
                            DELETEALL1 = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_all1"))),
                            DELETEALL2 = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_all2"))),
                            DELETETIME1 = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_time1"))),
                            DELETETIME2 = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_time2"))),
                            DELETELANDING1 = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_landing1"))),
                            DELETELANDING2 = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_landing2"))),
                            SUBFROMLANDING1 = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("sub_from_landing1")),
                            SUBFROMLANDING2 = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("sub_from_landing2")),
                            SUBFROMTIME1 = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("sub_from_time1")),
                            SUBFROMTIME2 = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("sub_from_time2")),
                            BASEROUNDLENGHT = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("BASEROUNDLENGHT")),
                            BASEROUNDMAXTIME = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("BASEROUNDMAXTIME")),
                            FINALROUNDLENGHT = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("FINALROUNDLENGHT")),
                            FINALROUNDMAXTIME = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("FINALROUNDMAXTIME")),
                            BONUSONLYFORFINALIST = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("bonusonlyforfinalist")))


                        };
                        MODEL_CONTEST_RULES.Add(rules);
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

                    if (kamulozitvysledek == "get_final_rounds")
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
                        MODEL_CONTEST_FINAL_ROUNDS.Add(rounds);
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

                    if (kamulozitvysledek == "get_final_groups")
                    {

                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + sqltext + " AA>>>> " + kamulozitvysledek);

                        if (int.Parse(SQL_READSOUTEZDATA("select count(id) from Groups_final where masterround="+(BIND_SELECTED_FINAL_ROUND+100), "")) > 0)
                        {

                            var groups = new MODEL_Contest_Groups()
                            {
                                ID = sqlite_datareader.GetInt32(8),
                                GROUPNAME = sqlite_datareader.GetString(3),
                                GROUPTYPE = sqlite_datareader.GetString(4),
                                GROUPLENGHT = sqlite_datareader.GetInt32(5),
                                GROUPZADANO = sqlite_datareader.GetInt32(6),

                            };
                            MODEL_CONTEST_FINAL_GROUPS.Add(groups);

                        }

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
                                Int64 myreader = sqlite_datareader.GetInt64(0);
                                Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + myreader + " >>>> " + kamulozitvysledek);
                                vysledek = myreader.ToString();
                            }

                            if (sqlite_datareader.GetFieldType(0) == typeof(Decimal))
                            {
                                Decimal myreader = sqlite_datareader.GetDecimal(0);
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

        public void clock_MAIN_create()
        {
            MAIN_TIME_TIMER.Tick += MAIN_TIME_TIMER_Tick;
            MAIN_TIME_TIMER.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }

        public void clock_FINAL_MAIN_create()
        {
            MAIN_FINAL_TIME_TIMER.Tick += MAIN_FINAL_TIME_TIMER_Tick;
            MAIN_FINAL_TIME_TIMER.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }


        public void clock_MAIN_start()
        {

            if (MODEL_CONTEST_SOUNDS_MAIN.Count > 0)
            {

                if (MODEL_CONTEST_SOUNDS_MAIN[0].VALUE < 0)
                {
                    BIND_TYPEOFCLOCK = "PRE_MAIN";
                    BIND_LETOVYCAS_MAX = Math.Abs(MODEL_CONTEST_SOUNDS_MAIN[0].VALUE);

                }
                else
                {
                    BIND_TYPEOFCLOCK = "MAIN";
                    BIND_LETOVYCAS_MAX = MODEL_CONTEST_RULES[0].BASEROUNDMAXTIME;
                }


            }
            else
            {
                BIND_TYPEOFCLOCK = "MAIN";
                BIND_LETOVYCAS_MAX = MODEL_CONTEST_RULES[0].BASEROUNDMAXTIME;
            }

            clock_DYNAMIC_ROUNDGROUP_ACTUAL_create();
            clock_DYNAMIC_ROUNDGROUP_NEXT_create();
            clock_DYNAMIC_COMPETITORS_ACTUAL_create();
            clock_DYNAMIC_COMPETITORS_NEXT_create();


            BIND_MAINTIME_ISRUNNING = true;
            BIND_MAINTIME_ISSTOPED = false;


            BIND_FINAL_MAINTIME_ISRUNNING = false;
            BIND_FINAL_MAINTIME_ISSTOPED = false;
            BIND_FINAL_PREPTIME_ISRUNNING = false;
            BIND_FINAL_PREPTIME_ISSTOPED = false;



            BIND_LETOVYCAS = 0;
            timer_main.Reset();
            MAIN_TIME_TIMER.Start();
            timer_main.Start();
        }



        public void clock_FINAL_MAIN_start()
        {

            if (MODEL_CONTEST_SOUNDS_FINAL_MAIN.Count > 0)
            {

                if (MODEL_CONTEST_SOUNDS_FINAL_MAIN[0].VALUE < 0)
                {
                    BIND_TYPEOFCLOCK = "PRE_MAIN";
                    BIND_FINAL_LETOVYCAS_MAX = Math.Abs(MODEL_CONTEST_SOUNDS_FINAL_MAIN[0].VALUE);


                }
                else
                {
                    BIND_TYPEOFCLOCK = "MAIN";
                    BIND_FINAL_LETOVYCAS_MAX = MODEL_CONTEST_RULES[0].FINALROUNDMAXTIME;
                }


            }
            else
            {
                BIND_TYPEOFCLOCK = "MAIN";
                BIND_FINAL_LETOVYCAS_MAX = MODEL_CONTEST_RULES[0].FINALROUNDMAXTIME;
            }

            clock_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_create();
            clock_DYNAMIC_COMPETITORS_FINAL_ACTUAL_create();


            if (BIND_SQL_AUDIO_COMPNUMBERS_PREP is true | BIND_SQL_AUDIO_COMPNUMBERS is true)
            {
                if (BIND_SQL_SOUTEZ_ROUNDSFINALE > 1)
                {
                    Console.WriteLine("BIND_SQL_SOUTEZ_ROUNDSFINALE " + BIND_SQL_SOUTEZ_ROUNDSFINALE);
                    clock_DYNAMIC_COMPETITORS_FINAL_NEXT_create();
                }
            }

            if (BIND_SQL_AUDIO_RNDGRPPREP is true | BIND_SQL_AUDIO_RNDGRPFLIGHT is true )
            {
                if (BIND_SQL_SOUTEZ_ROUNDSFINALE > 1)
                {
                    Console.WriteLine("BIND_SQL_SOUTEZ_ROUNDSFINALE " + BIND_SQL_SOUTEZ_ROUNDSFINALE);
                    clock_DYNAMIC_COMPETITORS_FINAL_NEXT_create();
                }
            }


            BIND_FINAL_MAINTIME_ISRUNNING = true;
            BIND_FINAL_MAINTIME_ISSTOPED = false;

            BIND_MAINTIME_ISRUNNING = false;
            BIND_MAINTIME_ISSTOPED = false;
            BIND_PREPTIME_ISRUNNING = false;
            BIND_PREPTIME_ISSTOPED = false;

            BIND_FINAL_LETOVYCAS = 0;
            timer_final_main.Reset();
            MAIN_FINAL_TIME_TIMER.Start();
            timer_final_main.Start();
        }


        public void clock_FINAL_MAIN_stop()
        {
            MAIN_FINAL_TIME_TIMER.Stop();
            timer_final_main.Stop();
            clock_DYNAMIC_ROUNDGROUP_ACTUAL_stop();
            clock_DYNAMIC_COMPETITORS_ACTUAL_stop();
            BIND_FINAL_MAINTIME_ISRUNNING = false;
            BIND_FINAL_MAINTIME_ISSTOPED = true;


            BIND_MAINTIME_ISRUNNING = false;
            BIND_MAINTIME_ISSTOPED = true;
            BIND_PREPTIME_ISRUNNING = false;
            BIND_PREPTIME_ISSTOPED = true;



        }


        public void clock_MAIN_stop()
        {
            MAIN_TIME_TIMER.Stop ();
            timer_main.Stop ();
            clock_DYNAMIC_ROUNDGROUP_ACTUAL_stop();
            clock_DYNAMIC_COMPETITORS_ACTUAL_stop();
            BIND_MAINTIME_ISRUNNING = false;
            BIND_MAINTIME_ISSTOPED = true;

            BIND_FINAL_MAINTIME_ISRUNNING = false;
            BIND_FINAL_MAINTIME_ISSTOPED = true;
            BIND_FINAL_PREPTIME_ISRUNNING = false;
            BIND_FINAL_PREPTIME_ISSTOPED = true;



        }




        public void clock_DYNAMIC_ROUNDGROUP_ACTUAL_create()
        {


            int i = 0;
            byte[] fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\round.wav");
            wav_roundgroup_actual[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_roundgroup_actual[i].Position = 0;
            roundgroupwav_actual[i] = new WaveOutEvent();
            roundgroupwav_actual[i].Init(wav_roundgroup_actual[i]);

            i = 1;
            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\"+ BIND_SELECTED_ROUND +".wav");
            wav_roundgroup_actual[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_roundgroup_actual[i].Position = 0;
            roundgroupwav_actual[i] = new WaveOutEvent();
            roundgroupwav_actual[i].Init(wav_roundgroup_actual[i]);

            i = 2;
            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\group.wav");
            wav_roundgroup_actual[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_roundgroup_actual[i].Position = 0;
            roundgroupwav_actual[i] = new WaveOutEvent();
            roundgroupwav_actual[i].Init(wav_roundgroup_actual[i]);

            i = 3;
            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\"+ BIND_SELECTED_GROUP +".wav");
            wav_roundgroup_actual[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_roundgroup_actual[i].Position = 0;
            roundgroupwav_actual[i] = new WaveOutEvent();
            roundgroupwav_actual[i].Init(wav_roundgroup_actual[i]);


            MAIN_DYNAMIC_ROUNDGROUP_ACTUAL_TIMER.Tick += MAIN_DYNAMIC_ROUNDGROUP_ACTUAL_TIMER_Tick;
            MAIN_DYNAMIC_ROUNDGROUP_ACTUAL_TIMER.Interval = new TimeSpan(0, 0, 0, 0, 10);
           
        }


        public void clock_DYNAMIC_ROUNDGROUP_ACTUAL_start()
        {
            timer_dynamic_roundgroup_actual.Reset();
            MAIN_DYNAMIC_ROUNDGROUP_ACTUAL_TIMER.Start();
            timer_dynamic_roundgroup_actual.Start();
        }

        public void clock_DYNAMIC_ROUNDGROUP_ACTUAL_stop()
        {
            MAIN_DYNAMIC_ROUNDGROUP_ACTUAL_TIMER.Stop();
            timer_dynamic_roundgroup_actual.Stop();
        }




        public void clock_DYNAMIC_ROUNDGROUP_NEXT_create()
        {




            int _tmp_newgroup = BIND_SELECTED_GROUP + 1;
            int _tmp_newround = BIND_SELECTED_ROUND;

            if (_tmp_newgroup > BIND_SQL_SOUTEZ_GROUPS) { _tmp_newgroup = 1; _tmp_newround += 1; }


           



            int i = 0;
            byte[] fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\round.wav");
            wav_roundgroup_next[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_roundgroup_next[i].Position = 0;
            roundgroupwav_next[i] = new WaveOutEvent();
            roundgroupwav_next[i].Init(wav_roundgroup_next[i]);
            i = 1;
            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + _tmp_newround + ".wav");
            wav_roundgroup_next[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_roundgroup_next[i].Position = 0;
            roundgroupwav_next[i] = new WaveOutEvent();
            roundgroupwav_next[i].Init(wav_roundgroup_next[i]);

            i = 2;
            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\group.wav");
            wav_roundgroup_next[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_roundgroup_next[i].Position = 0;
            roundgroupwav_next[i] = new WaveOutEvent();
            roundgroupwav_next[i].Init(wav_roundgroup_next[i]);

            i = 3;
            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + _tmp_newgroup + ".wav");
            wav_roundgroup_next[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_roundgroup_next[i].Position = 0;
            roundgroupwav_next[i] = new WaveOutEvent();
            roundgroupwav_next[i].Init(wav_roundgroup_next[i]);


            MAIN_DYNAMIC_ROUNDGROUP_NEXT_TIMER.Tick += MAIN_DYNAMIC_ROUNDGROUP_NEXT_TIMER_Tick;
            MAIN_DYNAMIC_ROUNDGROUP_NEXT_TIMER.Interval = new TimeSpan(0, 0, 0, 0, 10);

        }


        public void clock_DYNAMIC_ROUNDGROUP_NEXT_start()
        {
            timer_dynamic_roundgroup_next.Reset();
            MAIN_DYNAMIC_ROUNDGROUP_NEXT_TIMER.Start();
            timer_dynamic_roundgroup_next.Start();
        }

        public void clock_DYNAMIC_ROUNDGROUP_NEXT_stop()
        {
            MAIN_DYNAMIC_ROUNDGROUP_NEXT_TIMER.Stop();
            timer_dynamic_roundgroup_next.Stop();
        }


        #region finaldynamicroundgroup


        public void clock_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_create()
        {


            int i = 0;
            byte[] fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\final.wav");
            wav_roundgroup_final_actual[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_roundgroup_final_actual[i].Position = 0;
            roundgroupwav_final_actual[i] = new WaveOutEvent();
            roundgroupwav_final_actual[i].Init(wav_roundgroup_final_actual[i]);

            i = 1;
            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + BIND_SELECTED_FINAL_ROUND + ".wav");
            wav_roundgroup_final_actual[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_roundgroup_final_actual[i].Position = 0;
            roundgroupwav_final_actual[i] = new WaveOutEvent();
            roundgroupwav_final_actual[i].Init(wav_roundgroup_final_actual[i]);

            i = 2;
            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\---NONE---.wav");
            wav_roundgroup_final_actual[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_roundgroup_final_actual[i].Position = 0;
            roundgroupwav_final_actual[i] = new WaveOutEvent();
            roundgroupwav_final_actual[i].Init(wav_roundgroup_final_actual[i]);

            i = 3;
            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\---NONE---.wav");
            wav_roundgroup_final_actual[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_roundgroup_final_actual[i].Position = 0;
            roundgroupwav_final_actual[i] = new WaveOutEvent();
            roundgroupwav_final_actual[i].Init(wav_roundgroup_final_actual[i]);


            MAIN_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_TIMER.Tick += MAIN_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_TIMER_Tick;
            MAIN_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_TIMER.Interval = new TimeSpan(0, 0, 0, 0, 10);

        }


        public void clock_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_start()
        {
            timer_dynamic_roundgroup_FINAL_actual.Reset();
            MAIN_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_TIMER.Start();
            timer_dynamic_roundgroup_FINAL_actual.Start();
        }

        public void clock_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_stop()
        {
            MAIN_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_TIMER.Stop();
            timer_dynamic_roundgroup_FINAL_actual.Stop();
        }




        public void clock_DYNAMIC_ROUNDGROUP_FINAL_NEXT_create()
        {




            int _tmp_newgroup = 1;
            int _tmp_newround = BIND_SELECTED_FINAL_ROUND + 1;







            int i = 0;
            byte[] fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\final.wav");
            wav_roundgroup_final_next[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_roundgroup_final_next[i].Position = 0;
            roundgroupwav_final_next[i] = new WaveOutEvent();
            roundgroupwav_final_next[i].Init(wav_roundgroup_final_next[i]);
            i = 1;
            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + _tmp_newround + ".wav");
            wav_roundgroup_final_next[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_roundgroup_final_next[i].Position = 0;
            roundgroupwav_final_next[i] = new WaveOutEvent();
            roundgroupwav_final_next[i].Init(wav_roundgroup_final_next[i]);

            i = 2;
            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\---NONE---.wav");
            wav_roundgroup_final_next[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_roundgroup_final_next[i].Position = 0;
            roundgroupwav_final_next[i] = new WaveOutEvent();
            roundgroupwav_final_next[i].Init(wav_roundgroup_final_next[i]);

            i = 3;
            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\---NONE---.wav");
            wav_roundgroup_final_next[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_roundgroup_final_next[i].Position = 0;
            roundgroupwav_final_next[i] = new WaveOutEvent();
            roundgroupwav_final_next[i].Init(wav_roundgroup_final_next[i]);


            MAIN_DYNAMIC_ROUNDGROUP_FINAL_NEXT_TIMER.Tick += MAIN_DYNAMIC_ROUNDGROUP_FINAL_NEXT_TIMER_Tick;
            MAIN_DYNAMIC_ROUNDGROUP_FINAL_NEXT_TIMER.Interval = new TimeSpan(0, 0, 0, 0, 10);

        }


        public void clock_DYNAMIC_ROUNDGROUP_FINAL_NEXT_start()
        {
            timer_dynamic_roundgroup_FINAL_next.Reset();
            MAIN_DYNAMIC_ROUNDGROUP_FINAL_NEXT_TIMER.Start();
            timer_dynamic_roundgroup_FINAL_next.Start();
        }

        public void clock_DYNAMIC_ROUNDGROUP_FINAL_NEXT_stop()
        {
            MAIN_DYNAMIC_ROUNDGROUP_FINAL_NEXT_TIMER.Stop();
            timer_dynamic_roundgroup_FINAL_next.Stop();
        }


        #endregion


        public void clock_PREP_create()
        {
            PREP_TIME_TIMER.Tick += PREP_TIME_TIMER_Tick;
            PREP_TIME_TIMER.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }


        public void clock_PREP_start()
        {
            clock_DYNAMIC_COMPETITORS_NEXT_create();
            clock_DYNAMIC_ROUNDGROUP_NEXT_create();
            clock_DYNAMIC_COMPETITORS_ACTUAL_create();
            clock_DYNAMIC_ROUNDGROUP_ACTUAL_create();


            BIND_PREPTIME_ISRUNNING = true;
            BIND_PREPTIME_ISSTOPED = false;
            BIND_LETOVYCAS_PREP = 0;
            timer_prep.Reset();
            PREP_TIME_TIMER.Start();
            timer_prep.Start();
        }

        public void clock_PREP_stop()
        {
            PREP_TIME_TIMER.Stop();
            timer_prep.Stop();
            BIND_PREPTIME_ISRUNNING = false;
            BIND_PREPTIME_ISSTOPED = true;
            clock_DYNAMIC_COMPETITORS_ACTUAL_stop();
            clock_DYNAMIC_COMPETITORS_NEXT_stop();
            clock_DYNAMIC_ROUNDGROUP_ACTUAL_stop();
            clock_DYNAMIC_ROUNDGROUP_NEXT_stop();
        }



        public void clock_PREP_pause()
        {
            PREP_TIME_TIMER.Start();
            timer_prep.Start();
            BIND_PREPTIME_ISRUNNING = true;
            BIND_PREPTIME_ISSTOPED = false;

        }



        public void clock_FINAL_PREP_create()
        {
            PREP_FINAL_TIME_TIMER.Tick += PREP_FINAL_TIME_TIMER_Tick;
            PREP_FINAL_TIME_TIMER.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }


        public void clock_FINAL_PREP_start()
        {
            if (BIND_SQL_SOUTEZ_ROUNDSFINALE > 1)
            {
                clock_DYNAMIC_COMPETITORS_FINAL_NEXT_create();
                clock_DYNAMIC_ROUNDGROUP_FINAL_NEXT_create();
            }

            clock_DYNAMIC_COMPETITORS_FINAL_ACTUAL_create();
            clock_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_create();


            BIND_FINAL_PREPTIME_ISRUNNING = true;
            BIND_FINAL_PREPTIME_ISSTOPED = false;
            BIND_FINAL_LETOVYCAS_PREP = 0;
            timer_final_prep.Reset();
            PREP_FINAL_TIME_TIMER.Start();
            timer_final_prep.Start();
        }

        public void clock_FINAL_PREP_stop()
        {
            PREP_FINAL_TIME_TIMER.Stop();
            timer_final_prep.Stop();
            BIND_FINAL_PREPTIME_ISRUNNING = false;
            BIND_FINAL_PREPTIME_ISSTOPED = true;
            clock_DYNAMIC_COMPETITORS_ACTUAL_stop();
            clock_DYNAMIC_COMPETITORS_NEXT_stop();
            clock_DYNAMIC_ROUNDGROUP_ACTUAL_stop();
            clock_DYNAMIC_ROUNDGROUP_NEXT_stop();
        }




        public void clock_DYNAMIC_COMPETITORS_ACTUAL_create()
        {

            byte[] fileContent;

            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\competitors.wav");
            wav_competitors_actual[0] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_competitors_actual[0].Position = 0;
            competitorswav_actual[0] = new WaveOutEvent();
            competitorswav_actual[0].Init(wav_competitors_actual[0]);


            for (int x = 0; x < Players_Actual_Flying.Count; x++)
            {
                int i = x + 1;
                fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\"+ Players_Actual_Flying[x].ID + ".wav");
                wav_competitors_actual[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
                wav_competitors_actual[i].Position = 0;
                competitorswav_actual[i] = new WaveOutEvent();
                competitorswav_actual[i].Init(wav_competitors_actual[i]);

            }



            MAIN_DYNAMIC_COMPETITORS_ACTUAL_TIMER.Tick += MAIN_DYNAMIC_COMPETITORS_ACTUAL_TIMER_Tick;
            MAIN_DYNAMIC_COMPETITORS_ACTUAL_TIMER.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }


        public void clock_DYNAMIC_COMPETITORS_ACTUAL_start()
        {
            
            timer_DYNAMIC_COMPETITORS_ACTUAL.Reset();
            MAIN_DYNAMIC_COMPETITORS_ACTUAL_TIMER.Start();
            timer_DYNAMIC_COMPETITORS_ACTUAL.Start();
        }

        public void clock_DYNAMIC_COMPETITORS_ACTUAL_stop()
        {
            MAIN_DYNAMIC_COMPETITORS_ACTUAL_TIMER.Stop();
            timer_DYNAMIC_COMPETITORS_ACTUAL.Stop();
        }





        public void clock_DYNAMIC_COMPETITORS_NEXT_create()
        {

            byte[] fileContent;

            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\competitors.wav");
            wav_competitors_next[0] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_competitors_next[0].Position = 0;
            competitorswav_next[0] = new WaveOutEvent();
            competitorswav_next[0].Init(wav_competitors_next[0]);


            for (int x = 0; x < Players_Actual_Flying.Count; x++)
            {
                int i = x + 1;
                fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + users_id_for_sound[x] + ".wav");
                Console.WriteLine("Audio\\" + BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + users_id_for_sound[x] + ".wav");
                wav_competitors_next[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
                wav_competitors_next[i].Position = 0;
                competitorswav_next[i] = new WaveOutEvent();
                competitorswav_next[i].Init(wav_competitors_next[i]);

            }



            MAIN_DYNAMIC_COMPETITORS_NEXT_TIMER.Tick += MAIN_DYNAMIC_COMPETITORS_NEXT_TIMER_Tick;
            MAIN_DYNAMIC_COMPETITORS_NEXT_TIMER.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }


        public void clock_DYNAMIC_COMPETITORS_NEXT_start()
        {

            timer_DYNAMIC_COMPETITORS_NEXT.Reset();
            MAIN_DYNAMIC_COMPETITORS_NEXT_TIMER.Start();
            timer_DYNAMIC_COMPETITORS_NEXT.Start();
        }

        public void clock_DYNAMIC_COMPETITORS_NEXT_stop()
        {
            MAIN_DYNAMIC_COMPETITORS_NEXT_TIMER.Stop();
            timer_DYNAMIC_COMPETITORS_NEXT.Stop();
        }





        #region finaldynamicsounds

        public void clock_DYNAMIC_COMPETITORS_FINAL_ACTUAL_create()
        {

            byte[] fileContent;

            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\competitors.wav");
            wav_competitors_final_actual[0] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_competitors_final_actual[0].Position = 0;
            competitorswav_final_actual[0] = new WaveOutEvent();
            competitorswav_final_actual[0].Init(wav_competitors_final_actual[0]);


            for (int x = 0; x < Players_Actual_Final_Flying.Count; x++)
            {
                int i = x + 1;
                fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + Players_Actual_Final_Flying[x].ID + ".wav");
                wav_competitors_final_actual[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
                wav_competitors_final_actual[i].Position = 0;
                competitorswav_final_actual[i] = new WaveOutEvent();
                competitorswav_final_actual[i].Init(wav_competitors_final_actual[i]);

            }



            MAIN_DYNAMIC_COMPETITORS_FINAL_ACTUAL_TIMER.Tick += MAIN_DYNAMIC_COMPETITORS_FINAL_ACTUAL_TIMER_Tick;
            MAIN_DYNAMIC_COMPETITORS_FINAL_ACTUAL_TIMER.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }


        public void clock_DYNAMIC_COMPETITORS_FINAL_ACTUAL_start()
        {

            timer_DYNAMIC_COMPETITORS_FINAL_ACTUAL.Reset();
            MAIN_DYNAMIC_COMPETITORS_FINAL_ACTUAL_TIMER.Start();
            timer_DYNAMIC_COMPETITORS_FINAL_ACTUAL.Start();
        }

        public void clock_DYNAMIC_COMPETITORS_FINAL_ACTUAL_stop()
        {
            MAIN_DYNAMIC_COMPETITORS_FINAL_ACTUAL_TIMER.Stop();
            timer_DYNAMIC_COMPETITORS_FINAL_ACTUAL.Stop();
        }





        public void clock_DYNAMIC_COMPETITORS_FINAL_NEXT_create()
        {

            Console.WriteLine("clock_DYNAMIC_COMPETITORS_FINAL_NEXT_create");
            byte[] fileContent;

            fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\competitors.wav");
            wav_competitors_final_next[0] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
            wav_competitors_final_next[0].Position = 0;
            competitorswav_final_next[0] = new WaveOutEvent();
            competitorswav_final_next[0].Init(wav_competitors_final_next[0]);


            for (int x = 0; x < Players_Actual_Final_Flying.Count; x++)
            {
                int i = x + 1;
                fileContent = File.ReadAllBytes("Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + users_id_for_sound_final[x] + ".wav");
                wav_competitors_final_next[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));
                wav_competitors_final_next[i].Position = 0;
                competitorswav_final_next[i] = new WaveOutEvent();
                competitorswav_final_next[i].Init(wav_competitors_final_next[i]);

            }



            MAIN_DYNAMIC_COMPETITORS_FINAL_NEXT_TIMER.Tick += MAIN_DYNAMIC_COMPETITORS_FINAL_NEXT_TIMER_Tick;
            MAIN_DYNAMIC_COMPETITORS_FINAL_NEXT_TIMER.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }


        public void clock_DYNAMIC_COMPETITORS_NEXT_FINAL_start()
        {

            timer_DYNAMIC_COMPETITORS_FINAL_NEXT.Reset();
            MAIN_DYNAMIC_COMPETITORS_FINAL_NEXT_TIMER.Start();
            timer_DYNAMIC_COMPETITORS_FINAL_NEXT.Start();
        }

        public void clock_DYNAMIC_COMPETITORS_NEXT_FINAL_stop()
        {
            MAIN_DYNAMIC_COMPETITORS_FINAL_NEXT_TIMER.Stop();
            timer_DYNAMIC_COMPETITORS_FINAL_NEXT.Stop();
        }





        #endregion



        public void MAIN_TIME_TIMER_Tick(object sender, EventArgs e)
        {
                BIND_LETOVYCAS = Convert.ToSingle(timer_main.Elapsed.TotalSeconds);





            string letovycas;
            var elapsed = timer_main.Elapsed;

            if (BIND_TYPEOFCLOCK == "PRE_MAIN")
            {
                TimeSpan time_remaining = TimeSpan.FromSeconds(BIND_LETOVYCAS_MAX);
                TimeSpan totalsec = TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds);
                TimeSpan rozdil = time_remaining.Subtract(totalsec);
                letovycas = "Letový čas začne za: " + rozdil.ToString("mm':'ss':'ff");
            }
            else
            {
                TimeSpan time_remaining = TimeSpan.FromSeconds(MODEL_CONTEST_RULES[0].BASEROUNDMAXTIME);
                TimeSpan totalsec = TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds);
                TimeSpan rozdil2 = time_remaining.Subtract(totalsec);

                letovycas = "Letový čas : " + elapsed.ToString("mm':'ss':'ff") + " (zbývá : " + rozdil2.ToString("mm':'ss':'ff") + ")";
            }



            BIND_CAS_DO_MENU = letovycas;
            OnPropertyChanged("BIND_CAS_DO_MENU");

        }

        public void MAIN_FINAL_TIME_TIMER_Tick(object sender, EventArgs e)
        {
            BIND_FINAL_LETOVYCAS = Convert.ToSingle(timer_final_main.Elapsed.TotalSeconds);
        }

        public void PREP_TIME_TIMER_Tick(object sender, EventArgs e)
        {
            BIND_LETOVYCAS_PREP = Convert.ToSingle(timer_prep.Elapsed.TotalSeconds);
        }


        public void PREP_FINAL_TIME_TIMER_Tick(object sender, EventArgs e)
        {
            BIND_FINAL_LETOVYCAS_PREP = Convert.ToSingle(timer_final_prep.Elapsed.TotalSeconds);
        }





        public void MAIN_DYNAMIC_ROUNDGROUP_ACTUAL_TIMER_Tick(object sender, EventArgs e)
        {

            if (timer_dynamic_roundgroup_actual.Elapsed.Seconds != lastsecond_preroundgroup_actual)
            {
                if (timer_dynamic_roundgroup_actual.Elapsed.Seconds == 4)
                {
                    clock_DYNAMIC_ROUNDGROUP_ACTUAL_stop();
                    //clock_MAIN_start();
                }
                else
                {

                    lastsecond_preroundgroup_actual = timer_dynamic_roundgroup_actual.Elapsed.Seconds;
                    playsound_by_time("roundgroup_actual", timer_dynamic_roundgroup_actual.Elapsed.Seconds);
                }
                Console.WriteLine(timer_dynamic_roundgroup_actual.Elapsed.Seconds);
            }
        }




        public void MAIN_DYNAMIC_ROUNDGROUP_NEXT_TIMER_Tick(object sender, EventArgs e)
        {

            if (timer_dynamic_roundgroup_next.Elapsed.Seconds != lastsecond_preroundgroup_next)
            {
                if (timer_dynamic_roundgroup_next.Elapsed.Seconds == 4)
                {
                    clock_DYNAMIC_ROUNDGROUP_NEXT_stop();
                    //clock_MAIN_start();
                }
                else
                {

                    lastsecond_preroundgroup_next = timer_dynamic_roundgroup_next.Elapsed.Seconds;
                    playsound_by_time("roundgroup_next", timer_dynamic_roundgroup_next.Elapsed.Seconds);
                }
                Console.WriteLine(timer_dynamic_roundgroup_next.Elapsed.Seconds);
            }
        }


        public void MAIN_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_TIMER_Tick(object sender, EventArgs e)
        {

            if (timer_dynamic_roundgroup_FINAL_actual.Elapsed.Seconds != lastsecond_preroundgroup_actual)
            {
                if (timer_dynamic_roundgroup_FINAL_actual.Elapsed.Seconds == 4)
                {
                    clock_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_stop();
                    //clock_MAIN_start();
                }
                else
                {

                    lastsecond_preroundgroup_actual = timer_dynamic_roundgroup_FINAL_actual.Elapsed.Seconds;
                    playsound_by_time("roundgroup_final_actual", timer_dynamic_roundgroup_FINAL_actual.Elapsed.Seconds);
                }
                Console.WriteLine(timer_dynamic_roundgroup_FINAL_actual.Elapsed.Seconds);
            }
        }




        public void MAIN_DYNAMIC_ROUNDGROUP_FINAL_NEXT_TIMER_Tick(object sender, EventArgs e)
        {

            if (timer_dynamic_roundgroup_FINAL_next.Elapsed.Seconds != lastsecond_preroundgroup_next)
            {
                if (timer_dynamic_roundgroup_FINAL_next.Elapsed.Seconds == 4)
                {
                    clock_DYNAMIC_ROUNDGROUP_FINAL_NEXT_stop();
                    //clock_MAIN_start();
                }
                else
                {

                    lastsecond_preroundgroup_next = timer_dynamic_roundgroup_FINAL_next.Elapsed.Seconds;
                    playsound_by_time("roundgroup_final_next", timer_dynamic_roundgroup_FINAL_next.Elapsed.Seconds);
                }
                Console.WriteLine(timer_dynamic_roundgroup_FINAL_next.Elapsed.Seconds);
            }
        }



        public void MAIN_DYNAMIC_COMPETITORS_ACTUAL_TIMER_Tick(object sender, EventArgs e)
        {

            if (timer_DYNAMIC_COMPETITORS_ACTUAL.Elapsed.Seconds != lastsecond_precompetitors_actual)
            {
                if (timer_DYNAMIC_COMPETITORS_ACTUAL.Elapsed.Seconds == Players_Actual_Flying.Count+1)
                {
                    Console.WriteLine("STOPPPP DYNAMIC COMPETITORS ACTUAL CLOCK");
                    clock_DYNAMIC_COMPETITORS_ACTUAL_stop();
                    //clock_MAIN_start();
                }
                else
                {

                    lastsecond_precompetitors_actual = timer_DYNAMIC_COMPETITORS_ACTUAL.Elapsed.Seconds;
                    playsound_by_time("competitors_actual", timer_DYNAMIC_COMPETITORS_ACTUAL.Elapsed.Seconds);
                }
                Console.WriteLine("timer_DYNAMIC_COMPETITORS_ACTUAL:" + timer_DYNAMIC_COMPETITORS_ACTUAL.Elapsed.Seconds);
            }
        }





        public void MAIN_DYNAMIC_COMPETITORS_NEXT_TIMER_Tick(object sender, EventArgs e)
        {

            if (timer_DYNAMIC_COMPETITORS_NEXT.Elapsed.Seconds != lastsecond_precompetitors_next)
            {
                if (timer_DYNAMIC_COMPETITORS_NEXT.Elapsed.Seconds == Players_Actual_Flying.Count + 1)
                {
                    Console.WriteLine("STOPPPP DYNAMIC COMPETITORS NEXT CLOCK");
                    clock_DYNAMIC_COMPETITORS_NEXT_stop();
                    //clock_MAIN_start();
                }
                else
                {

                    lastsecond_precompetitors_next = timer_DYNAMIC_COMPETITORS_NEXT.Elapsed.Seconds;
                    playsound_by_time("competitors_next", timer_DYNAMIC_COMPETITORS_NEXT.Elapsed.Seconds);
                }
                Console.WriteLine("timer_DYNAMIC_COMPETITORS_NEXT:" + timer_DYNAMIC_COMPETITORS_NEXT.Elapsed.Seconds);
            }
        }


        public void MAIN_DYNAMIC_COMPETITORS_FINAL_ACTUAL_TIMER_Tick(object sender, EventArgs e)
        {

            if (timer_DYNAMIC_COMPETITORS_FINAL_ACTUAL.Elapsed.Seconds != lastsecond_precompetitors_actual)
            {
                if (timer_DYNAMIC_COMPETITORS_FINAL_ACTUAL.Elapsed.Seconds == Players_Actual_Final_Flying.Count + 1)
                {
                    Console.WriteLine("STOPPPP DYNAMIC COMPETITORS FINAL ACTUAL CLOCK");
                    clock_DYNAMIC_COMPETITORS_FINAL_ACTUAL_stop();
                    //clock_MAIN_start();
                }
                else
                {

                    lastsecond_precompetitors_actual = timer_DYNAMIC_COMPETITORS_FINAL_ACTUAL.Elapsed.Seconds;
                    playsound_by_time("competitors_final_actual", timer_DYNAMIC_COMPETITORS_FINAL_ACTUAL.Elapsed.Seconds);
                }
                Console.WriteLine("timer_DYNAMIC_COMPETITORS_FINAL_ACTUAL:" + timer_DYNAMIC_COMPETITORS_FINAL_ACTUAL.Elapsed.Seconds);
            }
        }





        public void MAIN_DYNAMIC_COMPETITORS_FINAL_NEXT_TIMER_Tick(object sender, EventArgs e)
        {

            if (timer_DYNAMIC_COMPETITORS_FINAL_NEXT.Elapsed.Seconds != lastsecond_precompetitors_next)
            {
                if (timer_DYNAMIC_COMPETITORS_FINAL_NEXT.Elapsed.Seconds == Players_Actual_Final_Flying.Count + 1)
                {
                    Console.WriteLine("STOPPPP DYNAMIC COMPETITORS NEXT CLOCK");
                    clock_DYNAMIC_COMPETITORS_NEXT_FINAL_stop();
                    //clock_MAIN_start();
                }
                else
                {

                    lastsecond_precompetitors_next = timer_DYNAMIC_COMPETITORS_FINAL_NEXT.Elapsed.Seconds;
                    playsound_by_time("competitors_final_next", timer_DYNAMIC_COMPETITORS_FINAL_NEXT.Elapsed.Seconds);
                }
                Console.WriteLine("timer_DYNAMIC_COMPETITORS_NEXT:" + timer_DYNAMIC_COMPETITORS_FINAL_NEXT.Elapsed.Seconds);
            }
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
            SQL_READSOUTEZDATA("select distinct id, name, (select count(id) from users where team= t.id ) pocet from teams T where id > 0;", "get_teams");
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
            draw_from_file_enabled = false;
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

                string line = File.ReadLines(file).Skip(1).Take(1).First();
                string[] words = line.Split(',');
                tmpinfo = words[2];
                tmpautor = words[1];

                string tmpall = info.Name + " | " + tmpinfo + " | Autor: " + tmpautor;
                Listofmatrixes.Add(new MatrixFiles() { Filename = Path.GetFileNameWithoutExtension(info.Name), Autor = tmpautor, Info = tmpinfo, all = tmpall });
                //filewithmatrix.Items.Add(Path.GetFileNameWithoutExtension(info.Name));
                Console.WriteLine("draw_from_file_enabled" + draw_from_file_enabled);
                draw_from_file_enabled = true;
            }

            if (CONTEST_LOCK == false) { draw_from_file_enabled = false; }

            Console.Write(mArrayOfflags.Length.ToString());
            OnPropertyChanged("Listofmatrixes");
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



        public void FUNCTION_LOAD_CONTESTS_ONLINE(string category)
        {


            MODEL_CONTESTS_ONLINE.Clear();
            Console.WriteLine("Searching online contests for category" + category);

            string[] mArrayOfcontests = new string[300];


            string remoteUrl = "http://api.stoupak.cz/sorgair/2021/api_new_listofcontests.php?cat="+category;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            HttpWebRequest.DefaultCachePolicy = policy;

            httpRequest.CachePolicy = policy;
            WebResponse response = httpRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();


        String[] spearator = {"<br>"};
        String[] strlist = result.Split(spearator,100,StringSplitOptions.None);
            foreach (String soutez in strlist)
            {
                Console.WriteLine(soutez);

                String[] spearator_sub = { "|" };

                if (soutez.Length > 5)
                {
                    var contests = new MODEL_Contests_files()
                    {

                        FILENAME = soutez.Split(spearator_sub, 100, StringSplitOptions.RemoveEmptyEntries)[3],
                        CATEGORY = BIND_NEWCONTEST_CATEGORY,
                        NAME = soutez.Split(spearator_sub, 100, StringSplitOptions.RemoveEmptyEntries)[0],
                        LOCATION = soutez.Split(spearator_sub, 100, StringSplitOptions.RemoveEmptyEntries)[2],
                        DATE = soutez.Split(spearator_sub, 100, StringSplitOptions.RemoveEmptyEntries)[1]
                    };
                    MODEL_CONTESTS_ONLINE.Add(contests);

                }




            }




            OnPropertyChanged("MODEL_CONTESTS_ONLINE");
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





        public ObservableCollection<Int32> users_id_for_sound { get; set; } = new ObservableCollection<Int32>();
        public ObservableCollection<Int32> users_id_for_sound_final { get; set; } = new ObservableCollection<Int32>();


        #region Players
        public ObservableCollection<MODEL_Player> Players { get; set; } = new ObservableCollection<MODEL_Player>();
        public ObservableCollection<MODEL_Player_actual> Players_Actual_Flying { get; set; } = new ObservableCollection<MODEL_Player_actual>();
        public ObservableCollection<MODEL_Player_actual> Players_Actual_Final_Flying { get; set; } = new ObservableCollection<MODEL_Player_actual>();
        public ObservableCollection<MODEL_Player_actual> Players_Actual_SelectedRound { get; set; } = new ObservableCollection<MODEL_Player_actual>();
        public ObservableCollection<MODEL_Player_selected> Player_Selected { get; set; } = new ObservableCollection<MODEL_Player_selected>();
        public ObservableCollection<MODEL_Player_selected> Player_Selected_Roundlist { get; set; } = new ObservableCollection<MODEL_Player_selected>();
        public ObservableCollection<MODEL_Player_baseresults> Players_Baseresults { get; set; } = new ObservableCollection<MODEL_Player_baseresults>();
        public ObservableCollection<MODEL_Player_baseresults_complete> Players_Baseresults_Complete { get; set; } = new ObservableCollection<MODEL_Player_baseresults_complete>();
        public ObservableCollection<MODEL_Player_baseresults> Players_Finalresults { get; set; } = new ObservableCollection<MODEL_Player_baseresults>();

        public ObservableCollection<MODEL_Player_statistics> Players_statistics { get; set; } = new ObservableCollection<MODEL_Player_statistics>();

        public void FUNCTION_USERS_LOAD_ALLCOMPETITORS()
        {
            Players.Clear();
            SQL_READSOUTEZDATA("select ID, Firstname, Lastname, Country,(select name from Agecategories A  where A.id=U.Agecat) Agecat, (select name from Frequencies F  where F.id=U.Freq) Freq, Ch1, Ch2, Failic, Naclic, Club, Paid, Team, Customagecat, U.Freq Freqid, U.Agecat agecatid from users U where id > 0; ", "get_players");
            BIND_POCETSOUTEZICICHMENU = SQL_READSOUTEZDATA("select count(id) pocet from users where id > 0", "");
            BIND_POCETSOUTEZICICH = Int32.Parse(SQL_READSOUTEZDATA("select count(id) pocet from users where id > 0", ""));
            
        }
        

        public bool _draw_from_file_enabled = false;

        public bool draw_from_file_enabled
        {
            get { return _draw_from_file_enabled; }
            set { _draw_from_file_enabled = value; OnPropertyChanged(nameof(draw_from_file_enabled)); }

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


        public int _bind_scoreentry_selected_penalisationlocal=0;
        public int bind_scoreentry_selected_penalisationlocal
        {
            get { return _bind_scoreentry_selected_penalisationlocal; }
            set { _bind_scoreentry_selected_penalisationlocal = value; OnPropertyChanged(nameof(bind_scoreentry_selected_penalisationlocal)); }
        }

        public int _bind_scoreentry_selected_penalisationglobal=0;
        public int bind_scoreentry_selected_penalisationglobal
        {
            get { return _bind_scoreentry_selected_penalisationglobal; }
            set { _bind_scoreentry_selected_penalisationglobal = value; OnPropertyChanged(nameof(bind_scoreentry_selected_penalisationglobal)); }
        }

        public int _bind_scoreentry_selected_height;
        public int bind_scoreentry_selected_height
        {
            get { return _bind_scoreentry_selected_height; }
            set { _bind_scoreentry_selected_height = value; OnPropertyChanged(nameof(bind_scoreentry_selected_height)); }
        }





        public int _bind_scoreentry_fromroundlist_selected_minute;
        public int bind_scoreentry_fromroundlist_selected_minute
        {
            get { return _bind_scoreentry_fromroundlist_selected_minute; }
            set { _bind_scoreentry_fromroundlist_selected_minute = value; OnPropertyChanged(nameof(bind_scoreentry_fromroundlist_selected_minute)); }
        }

        public int _bind_scoreentry_fromroundlist_selected_second;
        public int bind_scoreentry_fromroundlist_selected_second
        {
            get { return _bind_scoreentry_fromroundlist_selected_second; }
            set { _bind_scoreentry_fromroundlist_selected_second = value; OnPropertyChanged(nameof(bind_scoreentry_fromroundlist_selected_second)); }
        }


        public int _bind_scoreentry_fromroundlist_selected_landing;
        public int bind_scoreentry_fromroundlist_selected_landing
        {
            get { return _bind_scoreentry_fromroundlist_selected_landing; }
            set { _bind_scoreentry_fromroundlist_selected_landing = value; OnPropertyChanged(nameof(bind_scoreentry_fromroundlist_selected_landing)); }
        }


        public int _bind_scoreentry_fromroundlist_selected_penalisationlocal;
        public int bind_scoreentry_fromroundlist_selected_penalisationlocal
        {
            get { return _bind_scoreentry_fromroundlist_selected_penalisationlocal; }
            set { _bind_scoreentry_fromroundlist_selected_penalisationlocal = value; OnPropertyChanged(nameof(bind_scoreentry_fromroundlist_selected_penalisationlocal)); }
        }

        public int _bind_scoreentry_fromroundlist_selected_penalisationglobal;
        public int bind_scoreentry_fromroundlist_selected_penalisationglobal
        {
            get { return _bind_scoreentry_fromroundlist_selected_penalisationglobal; }
            set { _bind_scoreentry_fromroundlist_selected_penalisationglobal = value; OnPropertyChanged(nameof(bind_scoreentry_fromroundlist_selected_penalisationglobal)); }
        }

        public int _bind_scoreentry_fromroundlist_selected_height;
        public int bind_scoreentry_fromroundlist_selected_height
        {
            get { return _bind_scoreentry_fromroundlist_selected_height; }
            set { _bind_scoreentry_fromroundlist_selected_height = value; OnPropertyChanged(nameof(bind_scoreentry_fromroundlist_selected_height)); }
        }

        public void FUNCTION_SCOREENTRY_LOAD_USERDATA(int rnd,int grp, int stp)
        {
            Console.WriteLine("FUNCTION_SCOREENTRY_LOAD_USERDATA for rnd grp stp"+ rnd + "_" + grp + "_" + stp);
            Player_Selected.Clear();

            if (rnd == 0) { rnd = BIND_SELECTED_ROUND; }
                if (grp == 0) { grp = BIND_SELECTED_GROUP; }
                if (stp == 0) { stp = BIND_SELECTED_STARTPOINT; }
    
                SQL_READSOUTEZDATA("select U.ID,M.stp,U.Firstname,U.Lastname from matrix M left join users U on M.user = U.id where M.rnd = " + rnd + " and M.grp = " + grp + " and M.stp = " + stp + " order by stp asc;", "get_player_selected");
            bind_scoreentry_selected_minute = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(minutes) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select minutes from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));
            bind_scoreentry_selected_second = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(seconds) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select seconds from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));

            int _tmplandingid = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(landing) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select landing from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));
           
            int _tmppenlocid = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(pen1id) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select pen1id from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));
            int _tmppengloid = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(pen2id) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select pen2id from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));

            Console.WriteLine("bind_scoreentry_selected_second" + bind_scoreentry_selected_second);
            Console.WriteLine("bind_scoreentry_selected_minute" + bind_scoreentry_selected_minute);

            for (int i = 0; i < BINDING_Timer_listoflandings.Count; i++)
            {
                Console.WriteLine(BINDING_Timer_listoflandings[i].VALUE.ToString());
                if (BINDING_Timer_listoflandings[i].VALUE == _tmplandingid)
                {

                    bind_scoreentry_selected_landing = i;
                }
            }


            for (int i = 0; i < BINDING_Timer_listofpenalisationlocal.Count; i++)
            {
                Console.WriteLine(BINDING_Timer_listofpenalisationlocal[i].VALUE.ToString());
                if (BINDING_Timer_listofpenalisationlocal[i].ID == _tmppenlocid)
                {

                    bind_scoreentry_selected_penalisationlocal = i;
                }
            }

            for (int i = 0; i < BINDING_Timer_listofpenalisationglobal.Count; i++)
            {
                Console.WriteLine(BINDING_Timer_listofpenalisationglobal[i].VALUE.ToString());
                if (BINDING_Timer_listofpenalisationglobal[i].ID == _tmppengloid)
                {

                    bind_scoreentry_selected_penalisationglobal = i;
                }
            }


            bind_scoreentry_selected_height = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(height) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select height from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));

        }



        public void FUNCTION_SCOREENTRY_FROMROUNDSLIST_LOAD_USERDATA(int rnd, int grp, int stp)
        {

            Player_Selected_Roundlist.Clear();

            if (rnd == 0) { rnd = BIND_VIEWED_ROUND; }
            if (grp == 0) { grp = BIND_VIEWED_GROUP; }
            if (stp == 0) { stp = BIND_VIEWED_STARTPOINT; }

            SQL_READSOUTEZDATA("select U.ID,M.stp,U.Firstname,U.Lastname from matrix M left join users U on M.user = U.id where M.rnd = " + rnd + " and M.grp = " + grp + " and M.stp = " + stp + " order by stp asc;", "get_Player_Selected_Roundlist");
            bind_scoreentry_fromroundlist_selected_minute = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(minutes) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select minutes from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));
            bind_scoreentry_fromroundlist_selected_second = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(seconds) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select seconds from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));

            int _tmplandingid_fromroundlist = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(landing) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select landing from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));

            int _tmppenlocid_fromroundlist = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(pen1id) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select pen1id from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));
            int _tmppengloid_fromroundlist = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(pen2id) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select pen2id from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));

            for (int i = 0; i < BINDING_Timer_listoflandings.Count; i++)
            {
                Console.WriteLine(BINDING_Timer_listoflandings[i].VALUE.ToString());
                if (BINDING_Timer_listoflandings[i].VALUE == _tmplandingid_fromroundlist)
                {

                    bind_scoreentry_fromroundlist_selected_landing = i;
                }
            }


            for (int i = 0; i < BINDING_Timer_listofpenalisationlocal.Count; i++)
            {
                Console.WriteLine(BINDING_Timer_listofpenalisationlocal[i].VALUE.ToString());
                if (BINDING_Timer_listofpenalisationlocal[i].ID == _tmppenlocid_fromroundlist)
                {

                    bind_scoreentry_fromroundlist_selected_penalisationlocal = i;
                }
            }

            for (int i = 0; i < BINDING_Timer_listofpenalisationglobal.Count; i++)
            {
                Console.WriteLine(BINDING_Timer_listofpenalisationglobal[i].VALUE.ToString());
                if (BINDING_Timer_listofpenalisationglobal[i].ID == _tmppengloid_fromroundlist)
                {

                    bind_scoreentry_fromroundlist_selected_penalisationglobal = i;
                }
            }


            bind_scoreentry_fromroundlist_selected_height = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(height) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select height from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));

        }

        public void FUNCTION_SCOREENTRY_SAVE_SCORE(int rnd, int grp, int stp, int usrid, int minutes, int seconds, int landing, int height, int pen1value, int pen1id, int pen2value, int pen2id, string rawscore, string prepscore)
        {
            Console.WriteLine("saving score");
            SQL_SAVESOUTEZDATA("delete from score where rnd=" + rnd + " and grp=" + grp + " and stp=" + stp + ";");
            SQL_SAVESOUTEZDATA("insert INTO score (rnd, grp, stp, userid, minutes, seconds, landing, height, pen1value, pen1id, pen2value, pen2id, raw, prep, entered) VALUES(" + rnd + "," + grp + "," + stp + "," + usrid + "," + minutes + "," + seconds + "," + landing + "," + height + ", " + pen1value + ", " + pen1id + "," + pen2value + ", " + pen2id + ",'" + rawscore + "','" + prepscore + "', 'True');");

            Decimal _MAXRAW = Decimal.Parse(SQL_READSOUTEZDATA("select max(raw) FROM score s where s.rnd = " + rnd + " and s.grp = " + grp, ""));

            if (rnd >= 100)
            {
                for (int i = 1; i < BIND_SQL_SOUTEZ_STARTPOINTSFINALE + 1; i++)
                {
                    Console.WriteLine(SQL_READSOUTEZDATA("select raw FROM score s where s.rnd = " + rnd + " and s.grp = " + grp + " and s.stp = " + i, ""));

                    decimal _RAW = Decimal.Parse(SQL_READSOUTEZDATA("select raw FROM score s where s.rnd = " + rnd + " and s.grp = " + grp + " and s.stp = " + i, ""));
                    Decimal _PREPSCORE = 0;
                    if (_MAXRAW != 0)
                    {
                        _PREPSCORE = Math.Round((_RAW / _MAXRAW) * 1000, 2);
                    }
                    string _PREPSCORE_STR = _PREPSCORE.ToString(new CultureInfo("en-US"));
                    SQL_SAVESOUTEZDATA("update score set prep = " + _PREPSCORE_STR + " where rnd = " + rnd + " and grp = " + grp + " and stp = " + i);
                }
            }
            else
            {
                for (int i = 1; i < BIND_SQL_SOUTEZ_STARTPOINTS + 1; i++)
                {
                    Console.WriteLine(SQL_READSOUTEZDATA("select raw FROM score s where s.rnd = " + rnd + " and s.grp = " + grp + " and s.stp = " + i, ""));

                    decimal _RAW = Decimal.Parse(SQL_READSOUTEZDATA("select raw FROM score s where s.rnd = " + rnd + " and s.grp = " + grp + " and s.stp = " + i, ""));
                    Decimal _PREPSCORE = 0;
                    if (_MAXRAW != 0)
                    {
                        _PREPSCORE = Math.Round((_RAW / _MAXRAW) * 1000, 2);
                    }
                    string _PREPSCORE_STR = _PREPSCORE.ToString(new CultureInfo("en-US"));
                    SQL_SAVESOUTEZDATA("update score set prep = " + _PREPSCORE_STR + " where rnd = " + rnd + " and grp = " + grp + " and stp = " + i);
                }
            }



            FUNCTION_CHECK_REFLY(rnd, grp);          



        }


        public int FUNCTION_KOLIK_JE_SKUPIN_V_KOLE(int rnd, string type_skupiny, bool pridat_1)
        {

            int vysledek = 0;
                
            if (type_skupiny == "") {
                vysledek = int.Parse(SQL_READSOUTEZDATA("select count(id) from Groups where masterround =" + rnd, ""));
            }
            else {
                vysledek = int.Parse(SQL_READSOUTEZDATA("select count(id) from Groups where masterround =" + rnd + "  and type='" + type_skupiny + "'", ""));
            }


            if (pridat_1 is true) { vysledek = vysledek + 1; }
            return vysledek;
        }
        public int FUNCTION_KOLIK_JE_REFLY_SKUPIN_V_FINALE(int rnd, string type_skupiny, bool pridat_1)
        {

            int vysledek = 0;

            if (type_skupiny == "")
            {
                vysledek = int.Parse(SQL_READSOUTEZDATA("select count(id) from Groups_final where masterround =" + rnd, ""));
            }
            else
            {
                vysledek = int.Parse(SQL_READSOUTEZDATA("select count(id) from Groups_final where masterround =" + rnd + "  and type='" + type_skupiny + "'", ""));
            }


            if (pridat_1 is true) { vysledek = vysledek + 1; }
            return vysledek;
        }


        public void FUNCTION_CHECK_REFLY(int rnd, int grp)
        {
            for (int i = 1; i < BIND_SQL_SOUTEZ_STARTPOINTS + 1; i++)
            {
                FUNCTION_zjisti_jestli_a_ktery_z_refly_je_pocitany_from(rnd, grp, i);
                FUNCTION_zjisti_jestli_a_ktery_z_refly_je_pocitany_to(rnd, grp, i);
            }
        }


        public void FUNCTION_zjisti_jestli_a_ktery_z_refly_je_pocitany_from(int from_rnd, int from_grp, int from_stp)
        {

            string tmp_existujerefly = SQL_READSOUTEZDATA("select userid from refly where rnd_from=" + from_rnd + " and grp_from=" + from_grp + " and stp_from=" + from_stp + " ", "");

            Console.WriteLine("ZJISTUJI REFLY PRO FROM:"+from_rnd+"/"+from_grp+"/"+from_stp);



            if (tmp_existujerefly == "")
            {
                Console.WriteLine("refly neexistuje");
            }
            else
            {
                Console.WriteLine("Refly existuje");

                int to_rnd = int.Parse(SQL_READSOUTEZDATA("select rnd_to from refly where rnd_from=" + from_rnd + " and grp_from=" + from_grp + " and stp_from=" + from_stp + " ", ""));
                int to_grp = int.Parse(SQL_READSOUTEZDATA("select grp_to from refly where rnd_from=" + from_rnd + " and grp_from=" + from_grp + " and stp_from=" + from_stp + " ", ""));
                int to_stp = int.Parse(SQL_READSOUTEZDATA("select stp_to from refly where rnd_from=" + from_rnd + " and grp_from=" + from_grp + " and stp_from=" + from_stp + " ", ""));

                double tmp_score1;
                double tmp_score2;

                tmp_score1 = double.Parse(SQL_READSOUTEZDATA("select prep from score where rnd=" + from_rnd + " and grp=" + from_grp + " and stp=" + from_stp + " ", ""));
                tmp_score2 = double.Parse(SQL_READSOUTEZDATA("select prep from score where rnd=" + to_rnd + " and grp=" + to_grp + " and stp=" + to_stp + " ", ""));

                int tmp_pocitat_jen_lepsi_vysledek = int.Parse(SQL_READSOUTEZDATA("select whatcount1 from refly where rnd_from=" + from_rnd + " and grp_from=" + from_grp + " and stp_from=" + from_stp + " ", ""));

                if (tmp_pocitat_jen_lepsi_vysledek == 0)
                {
                    Console.WriteLine("Tento výsledek má nastaveno že má počítat lepší výsledek");
                    Console.WriteLine("Refly score z obou letů pro porovnání jsou:");
                    Console.WriteLine(tmp_score1);
                    Console.WriteLine(tmp_score2);

                    if (tmp_score1 > tmp_score2)
                    {
                        Console.WriteLine("zapisuji u score1 at se pocita, a score 2 at se nepocita");
                        SQL_SAVESOUTEZDATA("update score set refly='False' where rnd=" + from_rnd + " and grp=" + from_grp + " and stp=" + from_stp);
                        SQL_SAVESOUTEZDATA("update score set refly='True' where rnd=" + to_rnd + " and grp=" + to_grp + " and stp=" + to_stp);
                    }
                    else
                    {
                        Console.WriteLine("zapisuji u score1 at se nepocita, a score 2 at se pocita");
                        SQL_SAVESOUTEZDATA("update score set refly='True' where rnd=" + from_rnd + " and grp=" + from_grp + " and stp=" + from_stp);
                        SQL_SAVESOUTEZDATA("update score set refly='False' where rnd=" + to_rnd + " and grp=" + to_grp + " and stp=" + to_stp);
                    }



                }
                else
                {
                    Console.WriteLine("Neřešit který výsledek je lepší. Počítá se prostě ten druhý");
                    SQL_SAVESOUTEZDATA("update score set refly='True' where rnd=" + from_rnd + " and grp=" + from_grp + " and stp=" + from_stp);
                }
            }


        }

        public void FUNCTION_zjisti_jestli_a_ktery_z_refly_je_pocitany_to(int to_rnd, int to_grp, int to_stp)
        {

            string tmp_existujerefly = SQL_READSOUTEZDATA("select userid from refly where rnd_to=" + to_rnd + " and grp_to=" + to_grp + " and stp_to=" + to_stp + " ", "");

            Console.WriteLine("ZJISTUJI REFLY PRO TO:" + to_rnd + "/" + to_grp + "/" + to_stp);



            if (tmp_existujerefly == "")
            {
                Console.WriteLine("refly neexistuje");
            }
            else
            {
                Console.WriteLine("Refly existuje");

                int from_rnd = int.Parse(SQL_READSOUTEZDATA("select rnd_from from refly where rnd_to=" + to_rnd + " and grp_to=" + to_grp + " and stp_to=" + to_stp + " ", ""));
                int from_grp = int.Parse(SQL_READSOUTEZDATA("select grp_from from refly where rnd_to=" + to_rnd + " and grp_to=" + to_grp + " and stp_to=" + to_stp + " ", ""));
                int from_stp = int.Parse(SQL_READSOUTEZDATA("select stp_from from refly where rnd_to=" + to_rnd + " and grp_to=" + to_grp + " and stp_to=" + to_stp + " ", ""));

                double tmp_score1;
                double tmp_score2;

                tmp_score1 = double.Parse(SQL_READSOUTEZDATA("select prep from score where rnd=" + to_rnd + " and grp=" + to_grp + " and stp=" + to_stp + " ", ""));
                tmp_score2 = double.Parse(SQL_READSOUTEZDATA("select prep from score where rnd=" + from_rnd + " and grp=" + from_grp + " and stp=" + from_stp + " ", ""));

                int tmp_pocitat_jen_lepsi_vysledek = int.Parse(SQL_READSOUTEZDATA("select whatcount1 from refly where rnd_to=" + to_rnd + " and grp_to=" + to_grp + " and stp_to=" + to_stp + " ", ""));

                if (tmp_pocitat_jen_lepsi_vysledek == 0)
                {
                    Console.WriteLine("Tento výsledek má nastaveno že má počítat lepší výsledek");
                    Console.WriteLine("Refly score z obou letů pro porovnání jsou:");
                    Console.WriteLine(tmp_score1);
                    Console.WriteLine(tmp_score2);

                    if (tmp_score1 > tmp_score2)
                    {
                        Console.WriteLine("zapisuji u score1 at se pocita, a score 2 at se nepocita");
                        SQL_SAVESOUTEZDATA("update score set refly='False' where rnd=" + to_rnd + " and grp=" + to_grp + " and stp=" + to_stp);
                        SQL_SAVESOUTEZDATA("update score set refly='True' where rnd=" + from_rnd + " and grp=" + from_grp + " and stp=" + from_stp);
                    }
                    else
                    {
                        Console.WriteLine("zapisuji u score1 at se nepocita, a score 2 at se pocita");
                        SQL_SAVESOUTEZDATA("update score set refly='True' where rnd=" + to_rnd + " and grp=" + to_grp + " and stp=" + to_stp);
                        SQL_SAVESOUTEZDATA("update score set refly='False' where rnd=" + from_rnd + " and grp=" + from_grp + " and stp=" + from_stp);
                    }



                }
                else
                {
                    Console.WriteLine("Neřešit který výsledek je lepší. Počítá se prostě ten druhý");
                    SQL_SAVESOUTEZDATA("update score set refly='True' where rnd=" + from_rnd + " and grp=" + from_grp + " and stp=" + from_stp);
                }
            }


        }


        public void FUNCTION_CHECK_ENTERED_FINAL(int rnd, int grp, bool is_final_rounds)
        {

            if (is_final_rounds == true) 
            {



                int tmp_celkem_vgroupe = BIND_SQL_SOUTEZ_STARTPOINTSFINALE;
                int tmp_zadano_vgroupe = int.Parse(SQL_READSOUTEZDATA("select (select count(entered) from score where rnd=" + (rnd+100) + " and grp = " + grp + " and entered = 'True' and userid>0) zadano", ""));


                if (tmp_zadano_vgroupe == 0) { SQL_SAVESOUTEZDATA("update groups_final set zadano = '0' where masterround=" + (rnd + 100) + " and groupnumber=" + grp); }
                if ((tmp_zadano_vgroupe < tmp_celkem_vgroupe) & (tmp_zadano_vgroupe > 0)) { SQL_SAVESOUTEZDATA("update groups_final set zadano = '1' where masterround=" + (rnd + 100) + " and groupnumber=" + grp); }
                if (tmp_zadano_vgroupe == tmp_celkem_vgroupe) { SQL_SAVESOUTEZDATA("update groups_final set zadano = '2' where masterround=" + (rnd + 100) + " and groupnumber=" + grp); }
                if (tmp_celkem_vgroupe == 0) { SQL_SAVESOUTEZDATA("update groups_final set zadano = '2' where masterround=" + (rnd + 100) + " and groupnumber=" + grp); }

                string tmp_celkem_vkole = SQL_READSOUTEZDATA("select (select count(zadano) from groups_final where masterround=" + (rnd + 100) + ") celkem", "");
                string tmp_zadano_vkole = SQL_READSOUTEZDATA("select (select count(zadano) from groups_final where masterround=" + (rnd + 100) + " and zadano = '2') zadano", "");

                if (Int32.Parse(tmp_zadano_vkole) == 0) { SQL_SAVESOUTEZDATA("update final_rounds set zadano = '0' where id=" + rnd); }
                if (Int32.Parse(tmp_zadano_vkole) < Int32.Parse(tmp_celkem_vkole)) { SQL_SAVESOUTEZDATA("update final_rounds set zadano = '1' where id=" + rnd); }
                if (Int32.Parse(tmp_zadano_vkole) == Int32.Parse(tmp_celkem_vkole)) { SQL_SAVESOUTEZDATA("update final_rounds set zadano = '2' where id=" + rnd); }



            }
            else //toto se už nepouziva, nahrazeno FUNCTION_CHECK_ENTERED_ALL
            {
                string tmp_celkem_vgroupe = SQL_READSOUTEZDATA("select (select count(entered) from score where rnd=" + rnd + " and grp = " + grp + " and userid>0) celkem", "");
                string tmp_zadano_vgroupe = SQL_READSOUTEZDATA("select (select count(entered) from score where rnd=" + rnd + " and grp = " + grp + " and entered = 'True' and userid>0) zadano", "");


                if (Int32.Parse(tmp_zadano_vgroupe) == 0) { SQL_SAVESOUTEZDATA("update groups set zadano = '0' where masterround=" + rnd + " and groupnumber=" + grp); }
                if ((Int32.Parse(tmp_zadano_vgroupe) < Int32.Parse(tmp_celkem_vgroupe)) & (Int32.Parse(tmp_zadano_vgroupe) > 0)) { SQL_SAVESOUTEZDATA("update groups set zadano = '1' where masterround=" + rnd + " and groupnumber=" + grp); }
                if (Int32.Parse(tmp_zadano_vgroupe) == Int32.Parse(tmp_celkem_vgroupe)) { SQL_SAVESOUTEZDATA("update groups set zadano = '2' where masterround=" + rnd + " and groupnumber=" + grp); }
                if (Int32.Parse(tmp_celkem_vgroupe) == 0) { SQL_SAVESOUTEZDATA("update groups set zadano = '2' where masterround=" + rnd + " and groupnumber=" + grp); }



                string tmp_celkem_vkole = SQL_READSOUTEZDATA("select (select count(zadano) from groups where masterround=" + rnd + ") celkem", "");
                string tmp_zadano_vkole = SQL_READSOUTEZDATA("select (select count(zadano) from groups where masterround=" + rnd + " and zadano = '2') zadano", "");

                if (Int32.Parse(tmp_zadano_vkole) == 0) { SQL_SAVESOUTEZDATA("update rounds set zadano = '0' where id=" + rnd); }
                if (Int32.Parse(tmp_zadano_vkole) < Int32.Parse(tmp_celkem_vkole)) { SQL_SAVESOUTEZDATA("update rounds set zadano = '1' where id=" + rnd); }
                if (Int32.Parse(tmp_zadano_vkole) == Int32.Parse(tmp_celkem_vkole)) { SQL_SAVESOUTEZDATA("update rounds set zadano = '2' where id=" + rnd); }
            }


        }




        public void FUNCTION_CHECK_ENTERED_ALL()
        {


            for (int r = 1; r < MODEL_CONTEST_ROUNDS.Count+1; r++)
            {

                for (int g = 1; g < MODEL_CONTEST_GROUPS.Count+1; g++)
                {

                    string tmp_celkem_vgroupe = SQL_READSOUTEZDATA("select (select count(entered) from score where rnd=" + r + " and grp = " + g + " and userid>0) celkem", "");
                    string tmp_zadano_vgroupe = SQL_READSOUTEZDATA("select (select count(entered) from score where rnd=" + r + " and grp = " + g + " and entered = 'True' and userid>0) zadano", "");


                    if (Int32.Parse(tmp_zadano_vgroupe) == 0) { SQL_SAVESOUTEZDATA("update groups set zadano = '0' where masterround=" + r + " and groupnumber=" + g); }
                    if ((Int32.Parse(tmp_zadano_vgroupe) < Int32.Parse(tmp_celkem_vgroupe)) & (Int32.Parse(tmp_zadano_vgroupe) > 0)) { SQL_SAVESOUTEZDATA("update groups set zadano = '1' where masterround=" + r + " and groupnumber=" + g); }
                    if (Int32.Parse(tmp_zadano_vgroupe) == Int32.Parse(tmp_celkem_vgroupe)) { SQL_SAVESOUTEZDATA("update groups set zadano = '2' where masterround=" + r + " and groupnumber=" + g); }
                    if (Int32.Parse(tmp_celkem_vgroupe) == 0) { SQL_SAVESOUTEZDATA("update groups set zadano = '2' where masterround=" + r + " and groupnumber=" + g); }


                }

                string tmp_celkem_vkole = SQL_READSOUTEZDATA("select (select count(zadano) from groups where masterround=" + r + ") celkem", "");
                string tmp_zadano_vkole = SQL_READSOUTEZDATA("select (select count(zadano) from groups where masterround=" + r + " and zadano = '2') zadano", "");

                if (Int32.Parse(tmp_zadano_vkole) == 0) { SQL_SAVESOUTEZDATA("update rounds set zadano = '0' where id=" + r); }
                if ((Int32.Parse(tmp_zadano_vkole) < Int32.Parse(tmp_celkem_vkole)) & (Int32.Parse(tmp_zadano_vkole) > 0)) { SQL_SAVESOUTEZDATA("update rounds set zadano = '1' where id=" + r); }
                if (Int32.Parse(tmp_zadano_vkole) == Int32.Parse(tmp_celkem_vkole)) { SQL_SAVESOUTEZDATA("update rounds set zadano = '2' where id=" + r); }
                if (Int32.Parse(tmp_celkem_vkole) == 0) { SQL_SAVESOUTEZDATA("update rounds set zadano = '2' where id=" + r); }



            }








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


        public ObservableCollection<MODEL_Contests_categories> MODEL_CONTESTS_CATEGORIES { get; set; } = new ObservableCollection<MODEL_Contests_categories>();
        public ObservableCollection<MODEL_CATEGORY_LANDING> MODEL_CONTESTS_SOUNDLISTS { get; set; } = new ObservableCollection<MODEL_CATEGORY_LANDING>();

        public ObservableCollection<MODEL_CATEGORY_LANDING> MODEL_CATEGORY_SOUNDS { get; set; } = new ObservableCollection<MODEL_CATEGORY_LANDING>();
        public ObservableCollection<MODEL_CATEGORY_LANDING> MODEL_CONTEST_SOUNDS_MAIN { get; set; } = new ObservableCollection<MODEL_CATEGORY_LANDING>();
        public ObservableCollection<MODEL_CATEGORY_LANDING> MODEL_CONTEST_SOUNDS_PREP { get; set; } = new ObservableCollection<MODEL_CATEGORY_LANDING>();

        public ObservableCollection<MODEL_CATEGORY_LANDING> MODEL_CONTEST_SOUNDS_FINAL_MAIN { get; set; } = new ObservableCollection<MODEL_CATEGORY_LANDING>();
        public ObservableCollection<MODEL_CATEGORY_LANDING> MODEL_CONTEST_SOUNDS_FINAL_PREP { get; set; } = new ObservableCollection<MODEL_CATEGORY_LANDING>();

        public ObservableCollection<MODEL_CATEGORY_LANDING> MODEL_CATEGORY_LANDING { get; set; } = new ObservableCollection<MODEL_CATEGORY_LANDING>();
        public ObservableCollection<MODEL_CATEGORY_PENALISATIONS> MODEL_CATEGORY_PENALISATION { get; set; } = new ObservableCollection<MODEL_CATEGORY_PENALISATIONS>();
        public ObservableCollection<MODEL_CATEGORY_PENALISATIONS> MODEL_CATEGORY_PENALISATIONGLOBAL { get; set; } = new ObservableCollection<MODEL_CATEGORY_PENALISATIONS>();
        public ObservableCollection<MODEL_CATEGORY_PENALISATIONS> MODEL_CATEGORY_BONUSPOINTS { get; set; } = new ObservableCollection<MODEL_CATEGORY_PENALISATIONS>();

        public ObservableCollection<MODEL_Contests_files> MODEL_CONTESTS_FILES { get; set; } = new ObservableCollection<MODEL_Contests_files>();
        public ObservableCollection<MODEL_Contests_files> MODEL_CONTESTS_ONLINE { get; set; } = new ObservableCollection<MODEL_Contests_files>();

        public ObservableCollection<MODEL_Contest_Rounds> MODEL_CONTEST_ROUNDS { get; set; } = new ObservableCollection<MODEL_Contest_Rounds>();
        public ObservableCollection<MODEL_Contest_Rounds> MODEL_CONTEST_FINAL_ROUNDS { get; set; } = new ObservableCollection<MODEL_Contest_Rounds>();

        public ObservableCollection<MODEL_Contest_Rules> MODEL_CONTEST_RULES { get; set; } = new ObservableCollection<MODEL_Contest_Rules>();
        public ObservableCollection<MODEL_category_Rules> MODEL_CATEGORY_RULES { get; set; } = new ObservableCollection<MODEL_category_Rules>();


        public ObservableCollection<MODEL_Contest_Groups> MODEL_CONTEST_GROUPS { get; set; } = new ObservableCollection<MODEL_Contest_Groups>();
        public ObservableCollection<MODEL_Contest_Groups> MODEL_CONTEST_FINAL_GROUPS { get; set; } = new ObservableCollection<MODEL_Contest_Groups>();
        public ObservableCollection<MODEL_Contest_Groups> MODEL_CONTEST_AVAIABLE_REFLYGROUP { get; set; } = new ObservableCollection<MODEL_Contest_Groups>();

        public ObservableCollection<MODEL_Player_flags> MODEL_Contest_FLAGS { get; set; } = new ObservableCollection<MODEL_Player_flags>();

        public ObservableCollection<MODEL_Player_agecategories> MODEL_Contest_AGECATEGORIES { get; set; } = new ObservableCollection<MODEL_Player_agecategories>();
        public ObservableCollection<MODEL_Player_frequencies> MODEL_Contest_FREQUENCIES { get; set; } = new ObservableCollection<MODEL_Player_frequencies>();

        public void FUNCTION_LOAD_DEFAULT_ROUNDSANDGROUPS()
        {
            BIND_SELECTED_ROUND = 1;
            BIND_SELECTED_GROUP = 1;
            FUNCTION_SELECTED_ROUND_FLYING_USERS(0, 0);

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


        public void FUNCTION_ROUNDS_LOAD_FINAL_ROUNDS()
        {
            MODEL_CONTEST_FINAL_ROUNDS.Clear();
            SQL_READSOUTEZDATA("select * from final_rounds;", "get_final_rounds");
        }


        public void FUNCTION_ROUNDS_CREATE_FINAL_ROUNDS()
        {
            if (BIND_SQL_SOUTEZ_ROUNDSFINALE > 0)
            {
                BIND_MOVE_TO_FINAL_ROUNDS = true;
                BIND_MENU_ENABLED_finale = true; 
                 BIND_IS_FINAL_FLIGHT_READY = true;
                SQL_SAVESOUTEZDATA("delete from score where rnd>=100;");
                SQL_SAVESOUTEZDATA("delete from matrix where rnd>=100;");
                SQL_SAVESOUTEZDATA("delete from groups_final");
                SQL_SAVESOUTEZDATA("delete from refly where rnd_from > 100");
                SQL_SAVESOUTEZDATA("update final_rounds set zadano=0;");

                for (int r = 0; r < BIND_SQL_SOUTEZ_ROUNDSFINALE; r++)
                {
                    SQL_SAVESOUTEZDATA("insert into groups_final (id,name,type,lenght,zadano, masterround, groupnumber) values (null, 'Finále:" + (r+1) + "','final',600,0, " + (r+101) + " ,1);");


                    for (int c = 0; c < BIND_SQL_SOUTEZ_STARTPOINTSFINALE; c++)
                    {

                        int tmpstp = (r + c + 1);
                        if (tmpstp > BIND_SQL_SOUTEZ_STARTPOINTSFINALE) { tmpstp = tmpstp - BIND_SQL_SOUTEZ_STARTPOINTSFINALE; }
                        //FUNCTION_SCOREENTRY_SAVE_SCORE(100 + r, 1, c + 1, Players_Baseresults[c].ID, 0, 0, 0, 0, 0, 0, 0, 0, "0", "0",);
                        SQL_SAVESOUTEZDATA("insert INTO score (rnd, grp, stp, userid, minutes, seconds, landing, height, pen1value, pen1id, pen2value, pen2id, raw, prep, entered) VALUES(" + (r + 101) + "," + 1 + "," + tmpstp + "," + Players_Baseresults[c].ID + ",0,0,0,0,0,0,0,0,'0','0', 'False');");
                        SQL_SAVESOUTEZDATA("insert into matrix (rnd,grp,stp,user) values (" + (r + 101) + ",1," + tmpstp + "," + Players_Baseresults[c].ID + ")");

                    }


                }

                FUNCTION_ROUNDS_LOAD_FINAL_ROUNDS();
                MODEL_CONTEST_FINAL_ROUNDS[0].ISSELECTED = "selected";
                FUNCTION_SELECTED_FINAL_ROUND_USERS(BIND_SELECTED_FINAL_ROUND, 1);

            }
        }

        private bool _BIND_MOVE_TO_FINAL_ROUNDS = false;
        public bool BIND_MOVE_TO_FINAL_ROUNDS
        {
            get { return _BIND_MOVE_TO_FINAL_ROUNDS; }
            set { _BIND_MOVE_TO_FINAL_ROUNDS = value; OnPropertyChanged("BIND_MOVE_TO_FINAL_ROUNDS"); }
        }


        public string _BIND_DATA_OPAKOVACIHO_LETU = "xxx";
        public string  BIND_DATA_OPAKOVACIHO_LETU
        {
            get { return _BIND_DATA_OPAKOVACIHO_LETU; }
            set { _BIND_DATA_OPAKOVACIHO_LETU = value; OnPropertyChanged("BIND_DATA_OPAKOVACIHO_LETU"); }
        }


        private bool _BIND_SKRTEJ_ENABLED = false;
        public bool BIND_SKRTEJ_ENABLED
        {
            get { return _BIND_SKRTEJ_ENABLED; }
            set { _BIND_SKRTEJ_ENABLED = value; OnPropertyChanged("BIND_SKRTEJ_ENABLED"); }
        }

        public bool _ZOBRAZIT_ZAKLADNI_VYSLEDKY_S_SKRTACKAMA = false;

        

        public void FUNCTION_LOAD_RULES()
        {
            MODEL_CONTEST_RULES.Clear();
            SQL_READSOUTEZDATA("select * from rules where category='"+ BIND_SQL_SOUTEZ_KATEGORIE + "';", "get_rules");
        }

        public void FUNCTION_LOAD_CATEGORY_RULES(string categoryname)
        {
            MODEL_CATEGORY_RULES.Clear();
            SQL_READSORGDATA("select * from rules where category='" + categoryname + "';", "get_rules");
        }

        public void FUNCTION_LOAD_CATEGORY_LANDING(int categoryid)
        {
            MODEL_CATEGORY_LANDING.Clear();
            SQL_READSORGDATA("select * from landings where category='" + categoryid + "' order by id asc;", "get_landing");
        }

        public void FUNCTION_LOAD_CATEGORY_SOUNDS(int categoryid, int soundlistid)
        {
            MODEL_CATEGORY_SOUNDS.Clear();
            SQL_READSORGDATA("select * from Sounds where category='" + categoryid + "' and id= '" + soundlistid  + "' order by second asc;", "get_sounds");
        }

        
        public void FUNCTION_LOAD_CATEGORY_SOUNDLISTS(int categoryid)
        {
            MODEL_CONTESTS_SOUNDLISTS.Clear();
            SQL_READSORGDATA("select * from Soundlist where category='" + categoryid + "' order by id asc;", "get_soundlist");
        }


        public void FUNCTION_LOAD_CATEGORY_PENALISATION(int categoryid)
        {
            MODEL_CATEGORY_PENALISATION.Clear();
            SQL_READSORGDATA("select * from penalisations where category='" + categoryid + "' order by id asc;", "get_penalisation");
        }

        public void FUNCTION_LOAD_CATEGORY_PENALISATIONGLOBAL(int categoryid)
        {
            MODEL_CATEGORY_PENALISATIONGLOBAL.Clear();
            SQL_READSORGDATA("select * from penalisationsglobal where category='" + categoryid + "' order by id asc;", "get_penalisationglobal");
        }

        public void FUNCTION_LOAD_CATEGORY_BONUSPOINTS(int categoryid)
        {
            MODEL_CATEGORY_BONUSPOINTS.Clear();
            SQL_READSORGDATA("select * from bonuspoints where category='" + categoryid + "' order by id asc;", "get_bonuspoints");
        }
        
        public void FUNCTION_LOAD_CATEGORIES()
        {
            MODEL_CONTESTS_CATEGORIES.Clear();
            SQL_READSORGDATA("select * from rules;", "get_categories");
        }


        public void FUNCTION_ROUNDS_LOAD_GROUPS(int  kolo)
        {
            MODEL_CONTEST_GROUPS.Clear();
            SQL_READSOUTEZDATA("select rnd,grp,g.* from matrix M left join groups G on M.rnd = G.masterround where M.rnd=" + kolo + " group by groupnumber;", "get_groups");
        }

        public void FUNCTION_ROUNDS_LOAD_FINAL_GROUPS(int kolo)
        {
            MODEL_CONTEST_FINAL_GROUPS.Clear();
            SQL_READSOUTEZDATA("select rnd,grp,g.* from matrix M left join groups_final G on M.rnd = G.masterround where G.groupnumber > 1 and M.rnd=" + (kolo+100) + " group by groupnumber;", "get_final_groups");
        }


        public void FUNCTION_SHOW_AVAIABLE_GROUPS(int kolo)
        {
            MODEL_CONTEST_AVAIABLE_REFLYGROUP.Clear();
            int tmp_pocetstartovistvkole = int.Parse(SQL_READSOUTEZDATA("select count(masterround) from groups where masterround=" + kolo, ""));
            for (int i = 1; i <= tmp_pocetstartovistvkole; i++)
            {
                var pocetpraznychpozic = "0";
                if (i > BIND_SELECTED_GROUP)
                {
                    pocetpraznychpozic = SQL_READSOUTEZDATA("select ifnull(count(user),0) from matrix where rnd=" + kolo + " and grp = " + i + " and user=0", "");

                }

                var groups = new MODEL_Contest_Groups()
                {
                    ID = i,
                    GROUPNAME = "Volných pozic: "+ pocetpraznychpozic,
                    GROUPTYPE = pocetpraznychpozic,
                    GROUPLENGHT = 1,
                    GROUPZADANO = int.Parse(pocetpraznychpozic)
                };
                MODEL_CONTEST_AVAIABLE_REFLYGROUP.Add(groups);


            }


        }



        public void FUNCTION_SELECTED_ROUND_FLYING_USERS(int rnd, int grp)
        {
            Console.WriteLine("YYYYYYYYYYYYYYYYYYYYYYYY");

            if (rnd == 0) { rnd = BIND_SELECTED_ROUND; }
            if (grp == 0) { grp = BIND_SELECTED_GROUP; }
            Players_Actual_Flying.Clear();
            users_id_for_sound.Clear();
            SQL_READSOUTEZDATA("select U.ID,S.rnd, S.grp, S.stp,U.Firstname,U.Lastname, ifnull(s.minutes,0) minutes, ifnull(s.seconds,0) seconds, ifnull(s.landing,0) landing, ifnull(s.height,0) height, ifnull(s.pen1id,0) pen1, ifnull(s.pen2id,0) pen2, ifnull(s.raw,0) raw, ifnull(s.prep,0) prep, ifnull(s.entered,'False') entered  from score S left join users U on S.userid = U.id where  s.rnd = " + rnd + " and s.grp = " + grp + " order by s.stp asc; ", "get_Players_Actual_Flying");


            users_id_for_sound.Clear();
            int _tmp_newgroup = BIND_SELECTED_GROUP + 1;
            int _tmp_newround = BIND_SELECTED_ROUND;

            if (_tmp_newgroup > BIND_SQL_SOUTEZ_GROUPS) { _tmp_newgroup = 1; _tmp_newround += 1; }
            if (_tmp_newround > BIND_SQL_SOUTEZ_ROUNDS) { _tmp_newgroup = BIND_SQL_SOUTEZ_GROUPS; _tmp_newround = BIND_SQL_SOUTEZ_ROUNDS; }


            SQL_READSOUTEZDATA("select U.ID from score S left join users U on S.userid = U.id where  s.rnd = " + _tmp_newround + " and s.grp = " + _tmp_newgroup + " order by s.stp asc; ", "get_Players_Actual_Flying_nextforsound");
            //SQL_READSOUTEZDATA("select round((ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*(select heightunder from rules)) -(heightover*(select heightover from rules)) ),0)) / (select max(ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*(select heightunder from rules)) -(heightover*(select heightover from rules)) ),0)) FROM score s where s.rnd = " + rnd + " and s.grp = " + grp + ")*1000,2) maxrow , ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*0.5) -(heightover*3) ),0) RAWSCORE, U.ID,S.stp,U.Firstname,U.Lastname, ifnull(s.minutes,0) minutes, ifnull(s.seconds,0) seconds, ifnull(s.landing,0) landing, ifnull(s.height,0) height, ifnull(s.pen1,0) pen1, ifnull(s.pen2,0) pen2, ifnull(s.raw,0) war, ifnull(s.prep,0) prep, ifnull(s.entered,'False') entered from score S left join users U on S.userid = U.id where  s.rnd = " + rnd + " and s.grp = " + grp + " order by s.stp asc;", "get_Players_Actual_Flying");
        }

        public void FUNCTION_SELECTED_ROUND_USERS(int rnd, int grp)
        {
            if (rnd == 0) { rnd = BIND_SELECTED_ROUND; }
            if (grp == 0) { grp = BIND_SELECTED_GROUP; }
            Players_Actual_SelectedRound.Clear();
            SQL_READSOUTEZDATA("select U.ID,S.stp,U.Firstname,U.Lastname, ifnull(s.minutes,0) minutes, ifnull(s.seconds,0) seconds, ifnull(s.landing,0) landing, ifnull(s.height,0) height, ifnull(s.pen1id,0) pen1, ifnull(s.pen2id,0) pen2, ifnull(s.raw,0) raw, ifnull(s.prep,0) prep, ifnull(s.entered,'False') entered from score S left join users U on S.userid = U.id where  s.rnd = " + rnd + " and s.grp = " + grp + " order by s.stp asc; ", "get_Players_Actual_SelectedRound");
            //SQL_READSOUTEZDATA("select round((ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*(select heightunder from rules)) -(heightover*(select heightover from rules)) ),0)) / (select max(ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*(select heightunder from rules)) -(heightover*(select heightover from rules)) ),0)) FROM score s where s.rnd = " + rnd + " and s.grp = " + grp + ")*1000,2) maxrow , ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*0.5) -(heightover*3) ),0) RAWSCORE, U.ID,S.stp,U.Firstname,U.Lastname, ifnull(s.minutes,0) minutes, ifnull(s.seconds,0) seconds, ifnull(s.landing,0) landing, ifnull(s.height,0) height, ifnull(s.pen1,0) pen1, ifnull(s.pen2,0) pen2, ifnull(s.raw,0) war, ifnull(s.prep,0) prep, ifnull(s.entered,'False') entered from score S left join users U on S.userid = U.id where  s.rnd = " + rnd + " and s.grp = " + grp + " order by s.stp asc;", "get_Players_Actual_Flying");
        }

        public void FUNCTION_SELECTED_FINAL_ROUND_USERS(int rnd, int grp)
        {
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            if (rnd == 0) { rnd = BIND_SELECTED_FINAL_ROUND; }
            if (grp == 0) { grp = BIND_SELECTED_GROUP; }
            Players_Actual_Final_Flying.Clear();
            SQL_READSOUTEZDATA("select U.ID,S.rnd, S.grp, S.stp,U.Firstname,U.Lastname, ifnull(s.minutes,0) minutes, ifnull(s.seconds,0) seconds, ifnull(s.landing,0) landing, ifnull(s.height,0) height, ifnull(s.pen1id,0) pen1, ifnull(s.pen2id,0) pen2, ifnull(s.raw,0) raw, ifnull(s.prep,0) prep, ifnull(s.entered,'False') entered from score S left join users U on S.userid = U.id where  s.rnd = " + (rnd+100) + " and s.grp = " + grp + " order by s.stp asc; ", "get_Players_Actual_final_Flying");
            //SQL_READSOUTEZDATA("select round((ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*(select heightunder from rules)) -(heightover*(select heightover from rules)) ),0)) / (select max(ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*(select heightunder from rules)) -(heightover*(select heightover from rules)) ),0)) FROM score s where s.rnd = " + rnd + " and s.grp = " + grp + ")*1000,2) maxrow , ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*0.5) -(heightover*3) ),0) RAWSCORE, U.ID,S.stp,U.Firstname,U.Lastname, ifnull(s.minutes,0) minutes, ifnull(s.seconds,0) seconds, ifnull(s.landing,0) landing, ifnull(s.height,0) height, ifnull(s.pen1,0) pen1, ifnull(s.pen2,0) pen2, ifnull(s.raw,0) war, ifnull(s.prep,0) prep, ifnull(s.entered,'False') entered from score S left join users U on S.userid = U.id where  s.rnd = " + rnd + " and s.grp = " + grp + " order by s.stp asc;", "get_Players_Actual_Flying");
            users_id_for_sound_final.Clear();
            int _tmp_newround = BIND_SELECTED_ROUND+1;

            SQL_READSOUTEZDATA("select U.ID from score S left join users U on S.userid = U.id where  s.rnd = " + (_tmp_newround+100) + " and s.grp = 1 order by s.stp asc; ", "get_Players_Actual_Flying_nextforsound_final");

        }




        public void FUNCTION_RESULTS_LOADBASERESULTS(string what)

        {

            if (what == "statistics_flighttime")
            {
                Players_statistics.Clear();
                SQL_READSOUTEZDATA("select time(sum((minutes*60+seconds)), 'unixepoch') totaltime," +
" time(sum(minutes * 60 + seconds) / (select count(rnd) from score where userid = s1.userid), 'unixepoch') averagetime," +
" (select count(rnd) from score where userid = s1.userid) zaznamu," +
" u.Firstname," +
" u.Lastname, u.id userid" +
" from Score s1 left join users U on S1.userid = U.id where s1.userid > 0  group by userid order by totaltime desc", "get_statistics_flighttime");
            }


            if (what == "statistics_maxheights")
            {
                Players_statistics.Clear();
                SQL_READSOUTEZDATA("select s1.userid," +
" (select count(userid) from score where height > 0 and userid = s1.userid) zaznamu," +
" ifnull((select max(s1.height) from Score where height > 0  and userid = s1.userid),0) rawmaxheight," +
" ifnull((select sum(s1.height) from Score where height > 0  and userid = s1.userid),0) summaxheight," +
" ifnull((select sum(s1.height) from Score where height > 0  and userid = s1.userid) / (select count(rnd)from Score where height > 0  and userid = s1.userid),0) averagemaxheight," +
" u.Firstname," +
" u.Lastname" +
" from Score s1 left join users U on S1.userid = U.id where userid > 0  group by s1.userid order by rawmaxheight DESC", "get_statistics_maxheights");
            }


            if (what == "statistics_timevsheight")
            {
                Players_statistics.Clear();
                SQL_READSOUTEZDATA("select s1.userid, time(sum((minutes*60+seconds)), 'unixepoch') totaltime," +
" (select count(userid) from score where height > 0 and userid = s1.userid) zaznamu," +
" (select sum(minutes * 60 + seconds)) totaltimesec," +
" ifnull((select sum(height) from score where height > 0 and userid = s1.userid),0) sumheight," +
" ifnull(time(((select sum(minutes * 60 + seconds) from score where height > 0 and userid = s1.userid) / (select count(rnd)from Score where height > 0  and userid = s1.userid)),  'unixepoch' ),'99:99:99') prumernycasnakolo," +
" ifnull(CAST(((select sum(height) from score where height > 0 and userid = s1.userid) / (select count(rnd)from Score where height > 0  and userid = s1.userid)) as REAL),999.99) prumernavyskanakolo," +
" ifnull(round(CAST((select sum(height) from score where height > 0 and userid = s1.userid) as REAL) / (CAST((select sum(minutes * 60 + seconds) from score where height > 0 and userid = s1.userid) as REAL)) * 600,2),999.99) na10minutjetreba," +
" ifnull(time(round(CAST((select sum(minutes * 60 + seconds) from score where height > 0 and userid = s1.userid) / CAST((select sum(height) from score where height > 0 and userid = s1.userid) as REAL) as REAL) * 100, 2), 'unixepoch'),'00:00:00') ze100metrunalita," +
" u.Firstname," +
" u.Lastname " +
" from Score s1 left join users U on S1.userid = U.id where userid > 0  group by s1.userid order by na10minutjetreba ASC", "get_statistics_timevsheight");
            }




            if (what == "statistics_minheights")
            {
                Players_statistics.Clear();
                SQL_READSOUTEZDATA("select s1.userid,"+
" (select count(userid) from score where height > 0 and userid = s1.userid) zaznamu," +
" ifnull(round(sum(CAST(s1.height as REAL) / (select CAST(count(rnd) as REAL) from Score where height > 0  and userid = s1.userid)), 2), 0) rawminheight," +
" ifnull(round(sum(CAST(s1.height as REAL) / (select CAST(count(rnd) as REAL) from Score where height > 0  and userid = s1.userid)), 2),0) minheight," +
" (ifnull((select sum(height) from score where height > 0 and userid = s1.userid), 0) || ' / ' || ifnull((select sum(prep) from score where height > 0 and userid = s1.userid),0)) sumprep," +
" ifnull(round((select sum(prep) from score where height > 0 and userid = s1.userid) / (select sum(height) from score where height > 0 and userid = s1.userid),2),0) podil," +
" u.Firstname," +
" u.Lastname" +
" from Score s1 left join users U on S1.userid = U.id where userid > 0 group by s1.userid order by podil desc ", "get_statistics_minheights");
            }


            if (what == "statistics_averageheights")
            {
                Players_statistics.Clear();
                SQL_READSOUTEZDATA("select s1.userid,"+
                    " (select count(userid) from score where height > 0 and userid = s1.userid) zaznamu," +
                    " ifnull(round(sum(CAST(s1.height as REAL) / (select CAST(count(rnd) as REAL) from Score where height > 0  and userid = s1.userid)), 2), 9999) rawheight," +
                    " ifnull(round(sum(CAST(s1.height as REAL) / (select CAST(count(rnd) as REAL) from Score where height > 0  and userid = s1.userid)), 2),0) height," +
                    " ifnull((select sum(height) from score where height > 0 and userid = s1.userid),0) sumheight," +
                    " u.Firstname," +
                    " u.Lastname" +
                    " from Score s1 left join users U on S1.userid = U.id where userid > 0  group by s1.userid order by rawheight ASC", "get_statistics_averageheights");
            }

            if (what == "statistics_averagelandings")
            {
                Players_statistics.Clear();

                SQL_READSOUTEZDATA("select s1.userid,"+
"(select count(userid) from score where userid = s1.userid and userid>0) zaznamu," +
" ROUND(cast(sum(s1.landing) as REAL) / (select count(rnd) from Score where userid = s1.userid and userid>0 group by userid), 2) pristani," +
" (select sum(landing) from score where userid = s1.userid and userid>0)  sumpristani," +
" u.Firstname," +
" u.Lastname" +
" from Score s1 left join users U on S1.userid = U.id where userid > 0 and entered is 'True' group by s1.userid order by pristani DESC", "get_statistics_averagelandings");

            }


            if (what == "users")
            {
                Players_Baseresults.Clear();
                SQL_READSOUTEZDATA("select ((select max(prep) from score s2 where s2.userid = s1.userid and rnd <= "+ BIND_ROUNDS_IN_RESULTS + " and skrtacka='True' and refly='False') + (select sum(pen2value) from score s2 where s2.userid = s1.userid and rnd <= " + BIND_ROUNDS_IN_RESULTS + " )) skrtacka," +
                    "((select sum(prep) from score s2 where s2.userid = s1.userid and rnd <= " + BIND_ROUNDS_IN_RESULTS + " and skrtacka='False' and refly='False') + (select sum(pen2value) from score s2 where s2.userid = s1.userid and rnd <= " + BIND_ROUNDS_IN_RESULTS + " )) overalscore," +
                    " (select sum(raw) from score s2 where s2.userid = s1.userid and rnd <= " + BIND_ROUNDS_IN_RESULTS + " and s2.skrtacka='False' and s2.refly='False') overalrawscore ,(select sum(pen2value) from score s2 where s2.userid = s1.userid and rnd <= " + BIND_ROUNDS_IN_RESULTS + " ) gpen,(select name from Agecategories where id=u.Agecat) agecatstring, s1.*,u.* from score s1 left join users U on S1.userid = U.id where userid>0 group by userid order by overalscore desc,skrtacka desc", "get_baseresults_users");
                if (BIND_SQL_SOUTEZ_ROUNDSFINALE_value == 0) { BIND_MENU_ENABLED_finale = false; BIND_MOVE_TO_FINAL_ROUNDS = false; } else { BIND_MOVE_TO_FINAL_ROUNDS = true; }
            }

            if (what == "users_complete")
            {
                
                Players_Baseresults_Complete.Clear();
                SQL_READSOUTEZDATA("select ifnull(" +
                    "((select max(prep) from score s2 where s2.userid = s1.userid and rnd > 100 and skrtacka='True' and refly='False') + (select sum(pen2value) from score s2 where s2.userid = s1.userid and rnd > 100)),0) skrtacka_fin," +
                    "ifnull(((select sum(prep) from score s2 where s2.userid = s1.userid and rnd > 100 and skrtacka='False' and refly='False') + (select sum(pen2value) from score s2 where s2.userid = s1.userid and rnd > 100)),0) overalscore_fin," +
                    " ifnull((select sum(raw) from score s2 where s2.userid = s1.userid and rnd > 100 and skrtacka='False' and refly='False'),0) overalrawscore_fin , " +
                    "((select max(prep) from score s2 where s2.userid = s1.userid and rnd < 100 and skrtacka='True' and refly='False' ) +(select sum(pen2value) from score s2 where s2.userid = s1.userid  and rnd < 100  )) skrtacka_base, " +
                    "((select sum(prep) from score s2 where s2.userid = s1.userid and rnd < 100 and skrtacka='False' and refly='False' ) +(select sum(pen2value) from score s2 where s2.userid = s1.userid  and rnd < 100  )) overalscore_base, " +
                    " (select sum(raw) from score s2 where s2.userid = s1.userid  and rnd < 100 and skrtacka='False' and refly='False' ) overalrawscore_base , (select sum(pen2value) from score s2 where s2.userid = s1.userid  ) gpen, (select name from Agecategories where id=u.Agecat) agecatstring, s1.*,u.* from score s1 left join users U on S1.userid = U.id where userid>0 group by userid order by overalscore_fin desc, skrtacka_fin desc, overalscore_base desc, skrtacka_base desc", "get_baseresults_users_complete");
            }


            if (what == "teams")
            {
                Players_Baseresults.Clear();
                SQL_READSOUTEZDATA("select sum(prep)-sum(pen2value) overalscore,sum(raw) overalrawscore,sum(pen2value) gpen,ifnull(t.id,0) teamid, t.name || ' (' || count( distinct s.userid) ||'x)' team from score S left JOIN users U ON U.id = S.userid left join teams T on T.id = U.team where t.id > 0 and S.rnd < 100 and userid>0 and s.skrtacka = 'False' and s.refly='False'  group by team", "get_baseresults_teams");
                BIND_MOVE_TO_FINAL_ROUNDS = false;
            }

            if (what == "final_users")
            {
                Players_Finalresults.Clear();
                SQL_READSOUTEZDATA(" select ((select sum(prep) from score s2 where s2.userid = s1.userid and rnd > 100 and skrtacka='False' and refly='False') + (select sum(pen2value) from score s2 where s2.userid = s1.userid and rnd>100)) overalscore, (select sum(raw) from score s2 where s2.userid = s1.userid and rnd > 100 and skrtacka='False' and refly='False') overalrawscore , ((select sum(prep) from score s2 where s2.userid = s1.userid and rnd < 100 and skrtacka='False' and refly='False') +(select sum(pen2value) from score s2 where s2.userid = s1.userid  and rnd < 100  )) overalscore_base,  (select sum(raw) from score s2 where s2.userid = s1.userid  and rnd < 100 and skrtacka='False' and refly='False' ) overalrawscore_base, (select sum(pen2value) from score s2 where s2.userid = s1.userid and rnd > 100 ) gpen, (select name from Agecategories where id=u.Agecat) agecatstring, s1.*,u.* from score s1 left join users U on S1.userid = U.id where userid>0 group by userid order by overalscore desc, overalrawscore desc, overalscore_base desc, overalrawscore_base desc  limit " + BIND_SQL_SOUTEZ_STARTPOINTSFINALE, "get_finalresults_users");
            }


        }



        public void FUNCTION_COMPETITOR_UPDATE(string what, string value, int competiroid)
        {
            SQL_SAVESOUTEZDATA("update users set "+ what +" = '"+ value +"' where ID="+competiroid);

        }

        #endregion

        private bool _BIND_MAINTIME_ISRUNNING = false;
        public bool BIND_MAINTIME_ISRUNNING
        {
            get { return _BIND_MAINTIME_ISRUNNING; }
            set { _BIND_MAINTIME_ISRUNNING = value; OnPropertyChanged(nameof(BIND_MAINTIME_ISRUNNING)); }
        }

        private bool _BIND_MAINTIME_ISSTOPED = true;
        public bool BIND_MAINTIME_ISSTOPED
        {
            get { return _BIND_MAINTIME_ISSTOPED; }
            set { _BIND_MAINTIME_ISSTOPED = value; OnPropertyChanged(nameof(BIND_MAINTIME_ISSTOPED)); }
        }


        private bool _BIND_PREPTIME_ISRUNNING = false;
        public bool BIND_PREPTIME_ISRUNNING
        {
            get { return _BIND_PREPTIME_ISRUNNING; }
            set { _BIND_PREPTIME_ISRUNNING = value; OnPropertyChanged(nameof(BIND_PREPTIME_ISRUNNING)); }
        }

        private bool _BIND_PREPTIME_ISSTOPED = true;
        public bool BIND_PREPTIME_ISSTOPED
        {
            get { return _BIND_PREPTIME_ISSTOPED; }
            set { _BIND_PREPTIME_ISSTOPED = value; OnPropertyChanged(nameof(BIND_PREPTIME_ISSTOPED)); }
        }








        private bool _BIND_FINAL_MAINTIME_ISRUNNING = false;
        public bool BIND_FINAL_MAINTIME_ISRUNNING
        {
            get { return _BIND_FINAL_MAINTIME_ISRUNNING; }
            set { _BIND_FINAL_MAINTIME_ISRUNNING = value; OnPropertyChanged(nameof(BIND_FINAL_MAINTIME_ISRUNNING)); }
        }

        private bool _BIND_FINAL_MAINTIME_ISSTOPED = true;
        public bool BIND_FINAL_MAINTIME_ISSTOPED
        {
            get { return _BIND_FINAL_MAINTIME_ISSTOPED; }
            set { _BIND_FINAL_MAINTIME_ISSTOPED = value; OnPropertyChanged(nameof(BIND_FINAL_MAINTIME_ISSTOPED)); }
        }


        private bool _BIND_FINAL_PREPTIME_ISRUNNING = false;
        public bool BIND_FINAL_PREPTIME_ISRUNNING
        {
            get { return _BIND_FINAL_PREPTIME_ISRUNNING; }
            set { _BIND_FINAL_PREPTIME_ISRUNNING = value; OnPropertyChanged(nameof(BIND_FINAL_PREPTIME_ISRUNNING)); }
        }

        private bool _BIND_FINAL_PREPTIME_ISSTOPED = true;
        public bool BIND_FINAL_PREPTIME_ISSTOPED
        {
            get { return _BIND_FINAL_PREPTIME_ISSTOPED; }
            set { _BIND_FINAL_PREPTIME_ISSTOPED = value; OnPropertyChanged(nameof(BIND_FINAL_PREPTIME_ISSTOPED)); }
        }




        private string _scoreentrytype;
        public string ScoreEntryType
        {
            get { return _scoreentrytype; }
            set { _scoreentrytype = value; OnPropertyChanged(nameof(ScoreEntryType)); }
        }

        private string _ScoreEntryType2;
        public string ScoreEntryType2
        {
            get { return _ScoreEntryType2; }
            set { _ScoreEntryType2 = value; OnPropertyChanged(nameof(ScoreEntryType2)); }
        }



        public List<SoundList> BINDING_SoundList { get; } = new List<SoundList>();
        public List<SoundList> BINDING_SoundList_languages { get; } = new List<SoundList>();

        public List<Timer_minutes_seconds> BINDING_Timer_listofminutes { get; } = new List<Timer_minutes_seconds>();
        public List<Timer_minutes_seconds> BINDING_Timer_listofseconds { get; } = new List<Timer_minutes_seconds>();
        public List<Timer_minutes_seconds> BINDING_Timer_listofheights { get; } = new List<Timer_minutes_seconds>();
        public List<Timer_landings> BINDING_Timer_listoflandings { get; } = new List<Timer_landings>();
        public List<Timer_penalisation> BINDING_Timer_listofpenalisationlocal { get; } = new List<Timer_penalisation>();
        public List<Timer_penalisation> BINDING_Timer_listofpenalisationglobal { get; } = new List<Timer_penalisation>();
        public class Timer_minutes_seconds
        {
            public int Value { get; set; }
            public string Text { get; set; }

        }

        public class Timer_landings
        {
            public int ID { get; set; }
            public int VALUE { get; set; }
            public string TEXTVALUE { get; set; }
            public string LENGHT { get; set; }

        }

        public class Timer_penalisation
        {
            public int ID { get; set; }
            public int VALUE { get; set; }
            public string TEXTVALUE { get; set; }
            public string DELETE_TIME { get; set; }
            public string DELETE_LANDING { get; set; }
            public string DELETE_ALL { get; set; }

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

            SQL_READSOUTEZDATA("select * from Landings order by id asc", "get_landings");

        }


        public void FUNCTION_SOUND_LOADSOUNDLIST()
        {
            Console.WriteLine("FUNCTION_SOUND_LOADSOUNDLIST");
            BINDING_SoundList.Clear();
            SQL_READSOUTEZDATA("select * from soundlist order by id asc", "get_contest_soundlist");


            Console.WriteLine("BINDING_SoundList" + BINDING_SoundList.Count);
            if (BIND_AUDIO_SELECTEDBASESOUND_INDEX > (BINDING_SoundList.Count - 1)) { BIND_AUDIO_SELECTEDBASESOUND_INDEX = 0; }
            if (BIND_AUDIO_SELECTEDPREPFINALSOUND_INDEX > (BINDING_SoundList.Count - 1)) { BIND_AUDIO_SELECTEDPREPFINALSOUND_INDEX = 0; }
            FUNCTION_SOUND_LOADSELECTEDSOUND_MAIN(BINDING_SoundList[BIND_AUDIO_SELECTEDBASESOUND_INDEX].Id);
            FUNCTION_SOUND_LOADSELECTEDSOUND_PREP(BINDING_SoundList[BIND_AUDIO_SELECTEDPREPSOUND_INDEX].Id);
            FUNCTION_SOUND_LOADSELECTEDSOUND_FINAL_MAIN(BINDING_SoundList[BIND_AUDIO_SELECTEDFINALSOUND_INDEX].Id);
            FUNCTION_SOUND_LOADSELECTEDSOUND_FINAL_PREP(BINDING_SoundList[BIND_AUDIO_SELECTEDPREPFINALSOUND_INDEX].Id);

        }


        public void FUNCTION_SOUND_LOADAUDIO_LANGUAGE()
        {
            Console.WriteLine("FUNCTION_SOUND_LOADAUDIO_LANGUAGE");
            BINDING_SoundList_languages.Clear();
            //SQL_READSOUTEZDATA("select * from soundlist order by id asc", "get_contest_soundlist");

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);



            try
            {
                string[] dirs = Directory.GetDirectories(directory + "/Audio/", "*", SearchOption.TopDirectoryOnly);
                Console.WriteLine("The number of directories starting with p is {0}.", dirs.Length);
                int i = 0;
                foreach (string dir in dirs)
                {

                    var dirx = new DirectoryInfo(dir);
                    var dirName = dirx.Name;
                    if (dirName != "FUNKYMODE")
                    {
                        i += 1;
                        var _sndlst = new SoundList()
                        {
                            Id = i,
                            SoundName = dirName
                        };
                        BINDING_SoundList_languages.Add(_sndlst);
                        Console.WriteLine(dir);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            if (BINDING_SoundList_languages_index >= BINDING_SoundList_languages.Count())
            {
                BINDING_SoundList_languages_index = 0;
            }

        }



        public void FUNCTION_SOUND_LOADSELECTEDSOUND_MAIN(int soundlistid)
        {
            Console.WriteLine("FUNCTION_SOUND_LOADSELECTEDSOUND_MAIN");
            MODEL_CONTEST_SOUNDS_MAIN.Clear();
            SQL_READSOUTEZDATA("select * from sounds where id = '"+ soundlistid + "' order by second asc", "get_contest_sound_main");
            BIND_AUDIO_INFO = BINDING_SoundList[BIND_AUDIO_SELECTEDBASESOUND_INDEX].SoundName;

        }

        public void FUNCTION_SOUND_LOADSELECTEDSOUND_PREP(int soundlistid)
        {
            Console.WriteLine("FUNCTION_SOUND_LOADSELECTEDSOUND_PREP");
            MODEL_CONTEST_SOUNDS_PREP.Clear();
            SQL_READSOUTEZDATA("select * from sounds where id = '" + soundlistid + "' order by second asc", "get_contest_sound_prep");
            BIND_AUDIO_PREP_INFO = BINDING_SoundList[BIND_AUDIO_SELECTEDPREPSOUND_INDEX].SoundName;
            BIND_LETOVYCAS_PREP_MAX = MODEL_CONTEST_SOUNDS_PREP[MODEL_CONTEST_SOUNDS_PREP.Count - 1].VALUE;

        }

        public void FUNCTION_SOUND_LOADSELECTEDSOUND_FINAL_MAIN(int soundlistid)
        {
            Console.WriteLine("FUNCTION_SOUND_LOADSELECTEDSOUND_FINAL_MAIN");
            MODEL_CONTEST_SOUNDS_FINAL_MAIN.Clear();
            SQL_READSOUTEZDATA("select * from sounds where id = '" + soundlistid + "' order by second asc", "get_contest_sound_final_main");
            BIND_AUDIO_FINAL_INFO = BINDING_SoundList[BIND_AUDIO_SELECTEDFINALSOUND_INDEX].SoundName;

        }

        public void FUNCTION_SOUND_LOADSELECTEDSOUND_FINAL_PREP(int soundlistid)
        {
            Console.WriteLine("FUNCTION_SOUND_LOADSELECTEDSOUND_FINAL_PREP");
            MODEL_CONTEST_SOUNDS_FINAL_PREP.Clear();
            SQL_READSOUTEZDATA("select * from sounds where id = '" + soundlistid + "' order by second asc", "get_contest_sound_final_prep");
            BIND_AUDIO_FINAL_PREP_INFO = BINDING_SoundList[BIND_AUDIO_SELECTEDPREPFINALSOUND_INDEX].SoundName;
            //BIND_LETOVYCAS_PREP_MAX = MODEL_CONTEST_SOUNDS_PREP[MODEL_CONTEST_SOUNDS_PREP.Count - 1].VALUE;

        }


        public void FUNCTION_LOAD_TIMERS_PENALISATIONLOCAL()
        {
            Console.WriteLine("PENALISATIONLOCAL");
            BINDING_Timer_listofpenalisationlocal.Clear();

            SQL_READSOUTEZDATA("select * from Penalisations order by id asc", "get_penalisationlocal");
            Console.WriteLine(BINDING_Timer_listofpenalisationlocal.Count);
        }

        public void FUNCTION_LOAD_TIMERS_PENALISATIOGLOBAL()
        {
            Console.WriteLine("PENALISATIONGLOBAL");
            BINDING_Timer_listofpenalisationglobal.Clear();

            SQL_READSOUTEZDATA("select * from Penalisationsglobal order by id asc", "get_penalisationglobal");
            Console.WriteLine(BINDING_Timer_listofpenalisationglobal.Count);

        }




        public void FUNCTION_MOVE_GROUP_UP_DOWN(int posun)
        {



            int _tmp_newgroup = BIND_SELECTED_GROUP + posun;
            int _tmp_newround = BIND_SELECTED_ROUND;

            if (_tmp_newgroup > FUNCTION_KOLIK_JE_SKUPIN_V_KOLE(BIND_SELECTED_ROUND,"",false) ) { _tmp_newgroup = 1; _tmp_newround += 1; }


            if (_tmp_newgroup <= 0) {
                _tmp_newround -= 1; 
                _tmp_newgroup = FUNCTION_KOLIK_JE_SKUPIN_V_KOLE(_tmp_newround, "", false);  
            }



            int _tmp_selected_group_up = _tmp_newgroup + 1;
            int _tmp_selected_round_up = _tmp_newround;
            int _tmp_selected_group_down = _tmp_newgroup - 1;
            int _tmp_selected_round_down = _tmp_newround;


            if (_tmp_selected_group_up > FUNCTION_KOLIK_JE_SKUPIN_V_KOLE(_tmp_selected_round_up, "", false)) { _tmp_selected_group_up = 1; _tmp_selected_round_up += 1; }
            if (_tmp_selected_group_down < 1) {
                Console.WriteLine("TADY TADY");
                Console.WriteLine("_tmp_newgroup:" + _tmp_newgroup);
                Console.WriteLine("_tmp_newround:" + _tmp_newround);
                _tmp_selected_round_down -= 1;
                 _tmp_selected_group_down = FUNCTION_KOLIK_JE_SKUPIN_V_KOLE(_tmp_selected_round_down, "", false); 
                
            }





            BIND_PREWROUND_TEXT = "yPředchozí let : " + _tmp_selected_round_down + " / " + _tmp_selected_group_down;

            if (_tmp_newround < BIND_SQL_SOUTEZ_ROUNDS + 1 && _tmp_newround > 0)
            {
                BIND_SELECTED_GROUP = _tmp_newgroup;
                BIND_SELECTED_ROUND = _tmp_newround;

                FUNCTION_SELECTED_ROUND_FLYING_USERS(0, 0);
                FUNCTION_ROUNDS_LOAD_GROUPS(BIND_SELECTED_ROUND);
            }



            if (_tmp_selected_round_up > BIND_SQL_SOUTEZ_ROUNDS)
            {
                BIND_NEXTROUND_TEXT = "Žádný další let neexistuje";

                if (_tmp_selected_round_up == BIND_SQL_SOUTEZ_ROUNDS + 1)
                {
                   BIND_PREWROUND_TEXT = "xPředchozí let : " + BIND_SQL_SOUTEZ_ROUNDS + " / " + (BIND_SQL_SOUTEZ_GROUPS - 1);
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




        public void FUNCTION_MOVE_FINAL_ROUND(int posun)
        {

            BIND_SELECTED_FINAL_ROUND = BIND_SELECTED_FINAL_ROUND + posun;
            for (int i = 0; i < MODEL_CONTEST_FINAL_ROUNDS.Count; i++)
            {
                MODEL_CONTEST_FINAL_ROUNDS[i].ISSELECTED = "---";
            }


            MODEL_CONTEST_FINAL_ROUNDS[BIND_SELECTED_FINAL_ROUND - 1].ISSELECTED = "selected";

        }


        public void check_db_version()
        {
            var VERZE_DB_SOUBORU = "0";
            var _VERZE_DB_SOUBORU = SQL_READSOUTEZDATA("select value from contest where item='verze'", "");
            if (_VERZE_DB_SOUBORU != "")
            {
                VERZE_DB_SOUBORU = _VERZE_DB_SOUBORU;
            }
            Console.WriteLine("VERZE_DB_SOUBORU:" + VERZE_DB_SOUBORU);
            Console.WriteLine("BIND_VERZE_SORGU:" + System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());

            if (System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString() != VERZE_DB_SOUBORU)
            {
                Console.WriteLine("Provádím aktualizaci DB");

                

                if (SQL_VERIFY_IF_EXIST("contest", "item", "VERZE_DB_SOUBORU") == false)
                {
                    SQL_SAVESOUTEZDATA("insert into contest (item,value) values ('VERZE_DB_SOUBORU','0');");
                }

                

                if (SQL_VERIFY_IF_EXIST("contest", "item", "Audio_preptime_final_man_next") == false)
                {
                    SQL_SAVESOUTEZDATA("insert into contest (item,value) values ('Audio_preptime_final_man_next','False');");
                }

                if (SQL_VERIFY_IF_EXIST("contest", "item", "islocked") == false)
                {
                    SQL_SAVESOUTEZDATA("insert into contest (item,value) values ('islocked','False');");
                }


                if (SQL_VERIFY_IF_EXIST("contest", "item", "prep_audio_final_man_auto") == false)
                {
                    SQL_SAVESOUTEZDATA("insert into contest (item,value) values ('prep_audio_final_man_auto','False');");
                }
                
                if (SQL_VERIFY_IF_EXIST("contest", "item", "Audio_preptime_final_auto_next") == false)
                {
                    SQL_SAVESOUTEZDATA("insert into contest (item,value) values ('Audio_preptime_final_auto_next','False');");
                }

                if (SQL_VERIFY_IF_EXIST("contest", "item", "Runnextroundafterpreptime_final") == false)
                {
                    SQL_SAVESOUTEZDATA("insert into contest (item,value) values ('Runnextroundafterpreptime_final','False');");
                }
                
                if (SQL_VERIFY_IF_EXIST("contest", "item", "Runpreptime_final") == false)
                {
                    SQL_SAVESOUTEZDATA("insert into contest (item,value) values ('Runpreptime_final','False');");
                }

                if (SQL_VERIFY_IF_EXIST("contest", "item", "Preptimestart_final") == false)
                {
                    SQL_SAVESOUTEZDATA("insert into contest (item,value) values ('Preptimestart_final','01.01.0001 0:20:00');");
                }

                

                if (SQL_VERIFY_IF_EXIST("contest", "item", "Maintimestart_final") == false)
                {
                    SQL_SAVESOUTEZDATA("insert into contest (item,value) values ('Maintimestart_final','01.01.0001 0:20:00');");
                }

                SQL_SAVESOUTEZDATA("update contest set value='" + System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString() + "' where item='VERZE_DB_SOUBORU'");

            }

        }






        public Boolean SQL_VERIFY_IF_EXIST(string table, string item, string value)

        {
            Boolean vysledek = false;

            SQLiteCommand command = new SQLiteCommand("select * from "+table+" where "+item+ " = '"+value+"';", DBSOUTEZ_Connection);
            SQLiteDataReader sqlite_datareader;
            string _tmpvysledek = "";
            try

            {
                sqlite_datareader = command.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    _tmpvysledek = sqlite_datareader.GetString(0);
                }
            }
            catch (SQLiteException myException)
            {
                Console.WriteLine("Message: " + myException.Message + "\n");
            }

            if (_tmpvysledek != "")
            {
                vysledek = true;
            }

            return vysledek;

        }



        private string _CONTENT_RANDOM_ID;

        public string CONTENT_RANDOM_ID
        {
            get { return _CONTENT_RANDOM_ID; }
            set
            {
                _CONTENT_RANDOM_ID = value;
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='CONTENT_RANDOM_ID'");
                OnPropertyChanged("CONTENT_RANDOM_ID");
         
            }
        }


        private bool _CONTENT_ONLINE_ENABLED;

        public bool CONTENT_ONLINE_ENABLED
        {
            get { return _CONTENT_ONLINE_ENABLED; }
            set
            {
                _CONTENT_ONLINE_ENABLED = value;
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='CONTENT_ONLINE_ENABLED'");
                OnPropertyChanged("CONTENT_ONLINE_ENABLED");
                if (_CONTENT_ONLINE_ENABLED is true & CONTENT_RANDOM_ID == "0")
                {
                    FUNCTION_GENERATE_RANDOM_STRING(8);
                }
            }
        }

        private static Random random = new Random();
        public void FUNCTION_GENERATE_RANDOM_STRING(int length)
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            CONTENT_RANDOM_ID = new String(stringChars);

        }





    }
}
