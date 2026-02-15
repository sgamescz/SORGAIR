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
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading.Tasks;
using SORGAIR.Properties.Lang;
using System.Windows.Interop;
using System.Threading.Tasks;
using System.Threading;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;



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

    public class MyParameters
    {
        public int rnd { get; set; }
        public int grp { get; set; }
        public int stp { get; set; }
        public int refly { get; set; }
    }


    public class MyParameters2
    {
        public int rnd { get; set; }
        public int grp { get; set; }
        public int stp { get; set; }
        public decimal prepscore { get; set; }
    }

    public class MyParameters3
    {
        public int idsouteze { get; set; }
        public int skrtaci { get; set; }

    }
    public class MyParameters4
    {
        public int rnd { get; set; }
        public int grp { get; set; }
        public int stp { get; set; }
        public int usrid { get; set; }
        public int minutes { get; set; }
        public int seconds { get; set; }
        public int landing { get; set; }
        public int height { get; set; }
        public int pen1value { get; set; }
        public int pen1id { get; set; }
        public int pen2value { get; set; }

        public int pen2id { get; set; }
        public string rawscore { get; set; }
        public string prepscore { get; set; }
        public bool nondeletable { get; set; }
    }


    public class MODEL_ViewModel : INotifyPropertyChanged
    {
        public SerialPort _serialPort;
        public SerialPort _RPI_serialPort;




        string memoryprint = "";

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

        public string[] barva = new string[] { "Red", "Green", "Blue", "Purple", "Orange", "Lime", "Emerald", "Teal", "Cyan", "Cobalt", "Indigo", "Violet", "Pink", "Magenta", "Crimson", "Amber", "Yellow", "Brown", "Olive", "Steel", "Mauve", "Taupe", "Sienna" };
        public string[] pozadi = new string[] { "Light", "Dark" };

        public string typpozadi = "";
        public int pouzitabarva = 1;
        public int pouzitepozadi = 1;


        int last_second_prep_time = 0;
        int last_second_main_time = 0;

        bool serialport_blocked = false;


        int last_second_final_prep_time = 0;
        int last_second_final_main_time = 0;

        bool _BIND_IS_FINAL_FLIGHT_READY = false;
        public bool _bind_isnondeletable = false;

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

        public Double maxscoreproprocenta = 0;


        public string BIND_SQL_SOUTEZ_KATEGORIE_value;
        public string BIND_SQL_SOUTEZ_NAZEV_value;
        public string BIND_SQL_SOUTEZ_LOKACE_value;
        public string BIND_SQL_SOUTEZ_DATUM_value;
        public string BIND_SQL_SOUTEZ_TEPLOTA_value;
        public string BIND_SQL_SOUTEZ_POCASI_value;
        public string BIND_SQL_SOUTEZ_CLUB_value = "CZE";
        public string BIND_SQL_SOUTEZ_STAT_value;
        public string BIND_SQL_SOUTEZ_SMCRID_value;
        public string BIND_SQL_SACALENDAR_NUMBER_value;
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
        public string BIND_VYBRANEKOLOMENU_value = SORGAIR.Properties.Lang.Lang.menu_selectedround + " : 0/0";
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

        public int preptime_clock_id = 1;

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

        public int VYBRANYSTAT = 58;

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



        Boolean _UZ_JE_ROZLOSOVANO = false;
        public Boolean UZ_JE_ROZLOSOVANO
        {
            get { return _UZ_JE_ROZLOSOVANO; }
            set { _UZ_JE_ROZLOSOVANO = value; OnPropertyChanged("UZ_JE_ROZLOSOVANO"); }

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


        string _POUZITY_TYP_LOSOVANI = "unknown";
        public string POUZITY_TYP_LOSOVANI
        {
            get { return _POUZITY_TYP_LOSOVANI; }
            set
            {
                _POUZITY_TYP_LOSOVANI = value; OnPropertyChanged("POUZITY_TYP_LOSOVANI");
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='POUZITY_TYP_LOSOVANI'");

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
            set { _BIND_TYPEOFCLOCK = value; OnPropertyChanged("BIND_TYPEOFCLOCK"); Console.WriteLine("BIND_TYPEOFCLOCK" + value); }
        }


        public async void pripojserialport(string serialport)
        {
            try
            {
                // Zavření předchozího portu, pokud existuje
                if (_RPI_serialPort != null && _RPI_serialPort.IsOpen)
                {
                    _RPI_serialPort.Close();
                    _RPI_serialPort.Dispose();
                }

                _RPI_serialPort = new SerialPort(serialport, 9600, Parity.None, 8, StopBits.One);
                _RPI_serialPort.ReadTimeout = 500;
                _RPI_serialPort.WriteTimeout = 500;
                _RPI_serialPort.Handshake = Handshake.None;
                _RPI_serialPort.PortName = serialport;
                _RPI_serialPort.BaudRate = 9600;
                _RPI_serialPort.DataBits = 8;
                _RPI_serialPort.StopBits = StopBits.One;
                _RPI_serialPort.Parity = Parity.None;
                _RPI_serialPort.DataReceived += DataReceivedHandler;

                _RPI_serialPort.Open();

                if (!_RPI_serialPort.IsOpen)
                {
                    Console.WriteLine($"Nepodařilo se otevřít sériový port {serialport}");
                    MessageBox.Show($"Nepodařilo se otevřít sériový port {serialport}", 
                        "Chyba připojení", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Console.WriteLine($"Sériový port {serialport} byl úspěšně otevřen");

                // Testovací komunikace s HW
                await Task.Delay(100);
                _serialPortWrite("ATI", true);
                await Task.Delay(100);
                _serialPortWrite("AT+SN", true);
                await Task.Delay(100);
                _serialPortWrite("AT+MEM", true);
                await Task.Delay(100);
                _serialPortWrite("AT+UPTIME", true);
                await Task.Delay(100);
            }
            catch (UnauthorizedAccessException uaEx)
            {
                Console.WriteLine($"Přístup k portu {serialport} byl odepřen: {uaEx.Message}");
                MessageBox.Show($"Port {serialport} je již používán jinou aplikací nebo nemáte oprávnění k přístupu.", 
                    "Chyba přístupu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine($"Neplatný název portu {serialport}: {argEx.Message}");
                MessageBox.Show($"Neplatný název sériového portu: {serialport}", 
                    "Chyba portu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Chyba I/O při otevírání portu {serialport}: {ioEx.Message}");
                MessageBox.Show($"Port {serialport} nebyl nalezen nebo je nedostupný.", 
                    "Chyba I/O", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při připojování k sériovému portu {serialport}: {ex.Message}");
                MessageBox.Show($"Nepodařilo se připojit k portu {serialport}:\n{ex.Message}", 
                    "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void DataReceivedHandler(object sender,SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            Console.WriteLine(sp.BytesToRead + "bytes to read");
            while (sp.BytesToRead > 0)
            {
                Console.WriteLine("reading by line if bytestoread is greather than zero");

                string indata = sp.ReadLine();
                if (indata.Contains("Sorg HW"))
                {
                    HW_ATI = indata;
                    BINDING_HW_MENU_BASE = true;
                }
                if (indata.Contains("+MEM")) { HW_ATIMEM = indata; }
                if (indata.Contains("+UPTIME")) { HW_ATIUPTIME = indata; }
                if (indata.Contains("+SN")) { HW_ATISN = indata; }
                Console.WriteLine("Data Received:" + indata);
            }

            Console.Write("---------UNBLOCKING SERIAL PORT--------\n");
            serialport_blocked = false;
            Console.Write("---------SERIAL READ DONE--------\n");
        }


        public async void _serialPortWrite(string cozapsat, bool ignoreconnecteddevice )
        {

            try
            {
                if (ignoreconnecteddevice is true)
                {
                    // Kontrola, zda je port otevřený
                    if (_RPI_serialPort == null || !_RPI_serialPort.IsOpen)
                    {
                        Console.WriteLine("Seriový port není otevřený. Nelze zapsat: " + cozapsat);
                        return;
                    }

                    Console.WriteLine("Zapisuji na seriový port i s ignoraci pripojeneho zarizeni:" + cozapsat);
                    _RPI_serialPort.Write(cozapsat + "\r");
                }
                else
                {
                    if (BINDING_HW_MENU_BASE is true)
                    {
                        // Kontrola, zda je port otevřený
                        if (_RPI_serialPort == null || !_RPI_serialPort.IsOpen)
                        {
                            Console.WriteLine("Seriový port není otevřený. Nelze zapsat: " + cozapsat);
                            return;
                        }

                        Console.WriteLine("Zapisuji na seriový port pouze pokud je pripojeno:" + cozapsat);

                    znova:

                        Console.WriteLine("serialport_cozapsat:" + cozapsat);
                        Console.WriteLine("serialport_blocked:" + serialport_blocked);
                        if (serialport_blocked is true)
                        {
                            // CODE
                            Console.WriteLine("BLOCKED BLoCKED BLOCKED");
                            await Task.Delay(100);
                            Console.WriteLine("LOOP znova");
                            goto znova;

                        }


                        Console.WriteLine("NONBLOCKED NONBLoCKED NONBLOCKED");
                        Console.Write("---------BLOCKING SERIAL PORT--------\n");

                        serialport_blocked = true;


                        _RPI_serialPort.Write(cozapsat + "\r");

                    }

                }
            }
            catch (TimeoutException timeoutEx)
            {
                Console.WriteLine($"Timeout při zápisu na seriový port: {timeoutEx.Message}");
                Console.WriteLine("Hardware pravděpodobně není připojen nebo neodpovídá.");
                serialport_blocked = false; // Odblokování portu při chybě

                // Volitelně: můžete nastavit flag, že HW není připojen
                // BINDING_HW_MENU_BASE = false;
            }
            catch (InvalidOperationException ioEx)
            {
                Console.WriteLine($"Port není otevřený nebo je v neplatném stavu: {ioEx.Message}");
                serialport_blocked = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při zápisu na seriový port: {ex.Message}");
                serialport_blocked = false;
            }

        }

        public void FUNCTION_JETREBAROZLOSOVAT_OVER()
        {

            Console.WriteLine("_BIND_JETREBAROZLOSOVAT_SCORE" + _BIND_JETREBAROZLOSOVAT_SCORE);
            Console.WriteLine("SUM" + (BIND_SQL_SOUTEZ_ROUNDS * BIND_SQL_SOUTEZ_GROUPS * BIND_SQL_SOUTEZ_STARTPOINTS));
            if (_BIND_JETREBAROZLOSOVAT_SCORE != (BIND_SQL_SOUTEZ_ROUNDS * BIND_SQL_SOUTEZ_GROUPS * BIND_SQL_SOUTEZ_STARTPOINTS))
            {
                BIND_ROZLOSOVANIODPOVIDAPOCTUM = "Collapsed";
                Console.WriteLine("BIND_ROZLOSOVANIODPOVIDAPOCTUM" + BIND_ROZLOSOVANIODPOVIDAPOCTUM);
                BIND_MENU_ENABLED_seznamkol = false;
                BIND_MENU_ENABLED_vybranekolo = false;
                BIND_MENU_ENABLED_vysledky = false;
                BIND_MENU_ENABLED_vysledky_finale = false;
                BIND_MENU_ENABLED_detailyastatistiky = false;
                UZ_JE_ROZLOSOVANO = false;
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
                UZ_JE_ROZLOSOVANO = true;
            }

        }

        public void FUNCTION_JETREBAROZLOSOVAT_OVER_FINAL()
        {
            Console.WriteLine("_BIND_JETREBAROZLOSOVAT_SCORE_FINAL" + _BIND_JETREBAROZLOSOVAT_SCORE_FINAL);
            Console.WriteLine("SUM" + (BIND_SQL_SOUTEZ_ROUNDSFINALE * BIND_SQL_SOUTEZ_STARTPOINTSFINALE));
            if (_BIND_JETREBAROZLOSOVAT_SCORE_FINAL != (BIND_SQL_SOUTEZ_ROUNDSFINALE * BIND_SQL_SOUTEZ_STARTPOINTSFINALE))
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



                string time = (MODEL_CONTEST_SOUNDS_PREP[MODEL_CONTEST_SOUNDS_PREP.Count - 1].VALUE / 60).ToString() + ":" + (MODEL_CONTEST_SOUNDS_PREP[MODEL_CONTEST_SOUNDS_PREP.Count - 1].VALUE % 60).ToString("00");
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




        bool HARDWARE_CLOCK_OLD_ISCONNECTED_ = false;

        public bool HARDWARE_CLOCK_OLD_ISCONNECTED
        {
            get { return HARDWARE_CLOCK_OLD_ISCONNECTED_; }
            set { HARDWARE_CLOCK_OLD_ISCONNECTED_ = value; OnPropertyChanged("HARDWARE_CLOCK_OLD_ISCONNECTED"); }
        }


        public bool BIND_MENU_ENABLED_seznamkol
        {
            get { return BIND_MENU_ENABLED_seznamkol_value; }
            set { BIND_MENU_ENABLED_seznamkol_value = value; OnPropertyChanged("BIND_MENU_ENABLED_seznamkol"); }
        }


        public string _Function_global_resizemode = "None";
        public string Function_global_resizemode
        {
            get { 
                return _Function_global_resizemode; }
            set {
                _Function_global_resizemode = value;
                OnPropertyChanged("Function_global_resizemode");
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


        public List<int> customagecatidList = new List<int>(); // Tento list by byl naplněn IDčky z databáze




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




            check_db_version();

            MODEL_Contest_AGECATEGORIES.Clear();
            MODEL_Contest_CUSTOMAGECATEGORIES.Clear();


            SQL_READSOUTEZDATA("select distinct id, name from Agecategories where CUSTOM = 0 order by id asc;", "get_agecategories");
            SQL_READSOUTEZDATA("select distinct id, name from Agecategories where CUSTOM = 1 order by id desc;", "get_customagecategories");




            BIND_SQL_SOUTEZ_NAZEV = SQL_READSOUTEZDATA("select value from contest where item='Name'", "");
            BIND_SQL_SOUTEZ_KATEGORIE = SQL_READSOUTEZDATA("select value from contest where item='Category'", "");
            BIND_SQL_SOUTEZ_LOKACE = SQL_READSOUTEZDATA("select value from contest where item='Location'", "");
            BIND_SQL_SOUTEZ_DATUM = SQL_READSOUTEZDATA("select value from contest where item='Date'", "");
            BIND_SQL_SOUTEZ_TEPLOTA = SQL_READSOUTEZDATA("select value from contest where item='Temperature'", "");
            BIND_SQL_SOUTEZ_POCASI = SQL_READSOUTEZDATA("select value from contest where item='Weather'", "");
            BIND_SQL_SOUTEZ_CLUB = SQL_READSOUTEZDATA("select value from contest where item='Club'", "");
            BIND_SQL_SOUTEZ_STAT = SQL_READSOUTEZDATA("select value from contest where item='country'", "");
            BIND_SQL_SOUTEZ_SMCRID = SQL_READSOUTEZDATA("select value from contest where item='SMCRID'", "");
            BIND_SQL_SACALENDAR_NUMBER = SQL_READSOUTEZDATA("select value from contest where item='sorgaircalendarcontestid'", "");
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
            CONTEST_LOCK = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest  where item='islocked'", ""));


            CONTENT_RANDOM_ID = SQL_READSOUTEZDATA("select value from contest where item='CONTENT_RANDOM_ID'", "");
            POUZITY_TYP_LOSOVANI = SQL_READSOUTEZDATA("select value from contest where item='POUZITY_TYP_LOSOVANI'", "");

            CONTENT_ONLINE_ENABLED = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='CONTENT_ONLINE_ENABLED'", ""));
            CONTENT_ONLINE_PUBLIC = Convert.ToBoolean(SQL_READSOUTEZDATA("select value from contest where item='CONTENT_ONLINE_PUBLIC'", ""));

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

            if (BINDING_IS_INTERNET is true) { BIND_MENU_ENABLED_online = true; } else { BIND_MENU_ENABLED_online = false; }
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
                    letovycas = Lang.flight_time_will_start_in + ": " + rozdil.ToString("mm':'ss':'ff");
                }
                else
                {
                    TimeSpan time_remaining = TimeSpan.FromSeconds(MODEL_CONTEST_RULES[0].BASEROUNDMAXTIME);
                    TimeSpan totalsec = TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds);
                    TimeSpan rozdil2 = time_remaining.Subtract(totalsec);

                    letovycas = Lang.flight_time+ ": " + elapsed.ToString("mm':'ss':'ff") + " (" + Lang.remaining + ": " + rozdil2.ToString("mm':'ss':'ff") + ")";
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

                letovycas = Lang.preparation_time + ": " + elapsed.ToString("mm':'ss':'ff") + " (" + Lang.remaining + ": " + rozdil2.ToString("mm':'ss':'ff") + ")";


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
                    letovycas = Lang.flight_time_will_start_in + ": " + rozdil.ToString("mm':'ss':'ff");
                }
                else
                {
                    TimeSpan time_remaining = TimeSpan.FromSeconds(MODEL_CONTEST_RULES[0].FINALROUNDMAXTIME);
                    TimeSpan totalsec = TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds);
                    TimeSpan rozdil2 = time_remaining.Subtract(totalsec);

                    letovycas = Lang.flight_time + ": " + elapsed.ToString("mm':'ss':'ff") + " ("+ Lang.remaining +" : " + rozdil2.ToString("mm':'ss':'ff") + ")";
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

                letovycas = Lang.preparation_time+ ": " + elapsed.ToString("mm':'ss':'ff") + " (" + Lang.remaining + ": " + rozdil2.ToString("mm':'ss':'ff") + ")";


                //                Console.WriteLine(elapsed.ToString("mm':'ss':'f"));
                return letovycas;
            }

        }

        private int lastsecond = -100;
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

                          
                                FUNCTION_CLOCK_SET_STOPWATCH_TIME(0, 0);
                                FUNCTION_CLOCK_SET_DIRECTION(1);
                            //FUNCTION_SACLOCK_SETTIME(0, 0);

                            FUNCTION_SACLOCK_SETTIMETO_CLOCK(0, 0, false);
                            FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(42, 0, false);

                            //FUNCTION_SACLOCK_SETTIMETO_CLOCK(0, 600, false);
                            //FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(50, 0, false);




                            timer_main.Restart();

                        }
                        else {
                            playsound_by_time("main", _lastsecond);
                        }
                    }
                    else
                    {
                        _lastsecond = (timer_main.Elapsed.Minutes * 60) + timer_main.Elapsed.Seconds;
                        playsound_by_time("main", _lastsecond);

                        if (_lastsecond == _tmp_casspusteniautomatickehopripravnehocasu & BIND_SQL_AUTO_USEPREPTIME == true & BIND_SQL_AUTO_RUNPREPTIMENEXTROUND == true)
                        {
                            BIND_PREP_AUDIO_MAN_AUTO = false;
                            Console.WriteLine(BIND_SELECTED_ROUND);
                            Console.WriteLine(BIND_SQL_SOUTEZ_ROUNDS);
                            Console.WriteLine(BIND_SELECTED_GROUP);
                            Console.WriteLine(BIND_SQL_SOUTEZ_GROUPS);

                            int _tmp_newgroup = BIND_SELECTED_GROUP + 1;
                            int _tmp_newround = BIND_SELECTED_ROUND;

                            if (_tmp_newgroup > BIND_SQL_SOUTEZ_GROUPS) { _tmp_newgroup = 1; _tmp_newround += 1; }


                            if (_tmp_newround <= BIND_SQL_SOUTEZ_ROUNDS)
                            {
                                Console.WriteLine("je dalsi pripravny cas");
                                clock_PREP_start();
                            }
                            else
                            {
                                Console.WriteLine("neni dalsi pripravny cas");

                            }
                        }

                        if (last_second_main_time == _lastsecond)
                        {
                            clock_MAIN_stop();
                        }

                    }
                    Console.WriteLine("last_second_main_time:" + last_second_main_time);

                    Console.WriteLine("cas:" + _lastsecond);
                    DateTime xxx = DateTime.Parse(BIND_SQL_AUTO_PREPTIMESTART);
                    Console.WriteLine("datetime:" + ((xxx.Hour * 60) + xxx.Minute));



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
                           
                                FUNCTION_CLOCK_SET_STOPWATCH_TIME(0, 0);
                                FUNCTION_CLOCK_SET_DIRECTION(1);
                            //FUNCTION_SACLOCK_SETTIME(0, 0);
                            FUNCTION_SACLOCK_SETTIMETO_CLOCK(0, 0, false);
                            FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(42, 0, false);

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
                            int _tmp_newround;
                            _tmp_newround = BIND_SELECTED_FINAL_ROUND + 1;

                            if (_tmp_newround <= BIND_SQL_SOUTEZ_ROUNDSFINALE)
                            {


                                Console.WriteLine("je dalsi pripravny cas finale");
                                clock_FINAL_PREP_start();
                            }
                            else
                            {
                                Console.WriteLine("neni dalsi pripravny cas finale");

                            }



                            
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
                    if (_lastsecond_prep == _tmp_casspusteniautomatickehohlavnihocasu & BIND_SQL_AUTO_USEPREPTIME == true & BIND_SQL_AUTO_NEXTFLIGHTAFTERPREPTIME == true)
                    {


                        if (BIND_PREP_AUDIO_MAN_AUTO == true)
                        {
                            if (BIND_AUDIO_PREPTIME_MANUAL_NEXT == true) { FUNCTION_MOVE_GROUP_UP_DOWN(+1,false); clock_MAIN_start(); } else { clock_MAIN_start(); }
                        }
                        else
                        {
                            if (BIND_AUDIO_PREPTIME_AUTO_NEXT == true) { FUNCTION_MOVE_GROUP_UP_DOWN(+1,false); clock_MAIN_start(); } else { clock_MAIN_start(); }
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
                            if (BIND_SQL_AUDIO_RNDGRPFLIGHT == true & listItem.TEXTVALUE == "---SAY-RND-GRP---")
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

                                if (BIND_PREP_AUDIO_FINAL_MAN_AUTO == true) //pokud je pusteno rucne
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
                                    Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" + BIND_PREP_AUDIO_MAN_AUTO  + BIND_AUDIO_FINAL_PREPTIME_AUTO_NEXT);

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

        public string _BIND_NEWS_COUNT_NEXT_NEEDUPDATE = "-";

        public string BIND_NEWS_COUNT_NEXT_NEEDUPDATE
        {
            get
            {
                return _BIND_NEWS_COUNT_NEXT_NEEDUPDATE;
            }

            set
            {
                _BIND_NEWS_COUNT_NEXT_NEEDUPDATE = value; OnPropertyChanged("BIND_NEWS_COUNT_NEXT_NEEDUPDATE");
            }

        }


        public string _BIND_NEWS_COUNT_ACTUAL_NEEDUPDATE = "-";

        public string BIND_NEWS_COUNT_ACTUAL_NEEDUPDATE
        {
            get
            {
                return _BIND_NEWS_COUNT_ACTUAL_NEEDUPDATE;
            }

            set
            {
                _BIND_NEWS_COUNT_ACTUAL_NEEDUPDATE = value; OnPropertyChanged("BIND_NEWS_COUNT_ACTUAL_NEEDUPDATE");
            }

        }



        private string _BIND_NEWS_COUNT_ACTUAL = Lang.checking;
        public string BIND_NEWS_COUNT_ACTUAL
        {
            get
            {
                return SORGAIR.Properties.Lang.Lang.home_newscount + " : " + _BIND_NEWS_COUNT_ACTUAL;
            }

            set
            {
                _BIND_NEWS_COUNT_ACTUAL = value; OnPropertyChanged("BIND_NEWS_COUNT_ACTUAL");
                if (_BIND_NEWS_COUNT_ACTUAL == "0")
                {
                    BIND_NEWS_COUNT_ACTUAL_NEEDUPDATE = "0";
                }
                else
                {
                    BIND_NEWS_COUNT_ACTUAL_NEEDUPDATE = "1";
                }
            }

        }

        private string _BIND_NEWS_COUNT_NEXT = Lang.checking;
        public string BIND_NEWS_COUNT_NEXT
        {
            get
            {
                return SORGAIR.Properties.Lang.Lang.home_newscount + " : " + _BIND_NEWS_COUNT_NEXT;
            }

            set
            {
                _BIND_NEWS_COUNT_NEXT = value; OnPropertyChanged("BIND_NEWS_COUNT_NEXT");
                if (_BIND_NEWS_COUNT_NEXT == "0")
                {
                    BIND_NEWS_COUNT_NEXT_NEEDUPDATE = "0";
                }
                else
                {
                    BIND_NEWS_COUNT_NEXT_NEEDUPDATE = "1";
                }
            }

        }


        private string _BIND_VERZE_SORGU_LAST = Lang.checking;
        public string BIND_VERZE_SORGU_LAST
        {
            get
            {
                OnPropertyChanged("BIND_VERZE_SORGU_NEEDUPDATE");
                return Lang.home_actualversionis + " : " + _BIND_VERZE_SORGU_LAST;
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
                _BIND_VERZE_SORGU_NEEDUPDATE = "1";

                if (_BIND_VERZE_SORGU_LAST == _BIND_VERZE_SORGU)
                {
                    _BIND_VERZE_SORGU_NEEDUPDATE = "0";
                }
                if (_BIND_VERZE_SORGU_LAST == Lang.checking)
                {
                    _BIND_VERZE_SORGU_NEEDUPDATE = "-";
                }


                return _BIND_VERZE_SORGU_NEEDUPDATE;
            }

            set
            {
                _BIND_VERZE_SORGU_NEEDUPDATE = value; OnPropertyChanged("BIND_VERZE_SORGU_NEEDUPDATE");
            }

        }




        private string _BIND_VERZE_SORGU = Lang.checking;
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
                BIND_LETOVYCAS_MAX_value = value; OnPropertyChanged("BIND_LETOVYCAS_MAX"); OnPropertyChanged("BIND_LETOVYCAS_STRING"); Console.WriteLine("BIND_LETOVYCAS_MAX" + BIND_LETOVYCAS_MAX);
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
            get { return SQL_READSOUTEZDATA("select Name from Rounds where id = " + BIND_SELECTED_ROUND, ""); }
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

                if (int.Parse(BIND_POCETSOUTEZICICHMENU_value) >= (BIND_SQL_SOUTEZ_GROUPS * BIND_SQL_SOUTEZ_STARTPOINTS)) {
                    BIND_SOUTEZ_JEPLNO = false;
                }
                else
                {
                    BIND_SOUTEZ_JEPLNO = true;
                }
                return SORGAIR.Properties.Lang.Lang.menu_competitors + " [" + BIND_POCETSOUTEZICICHMENU_value + "/" + (BIND_SQL_SOUTEZ_GROUPS * BIND_SQL_SOUTEZ_STARTPOINTS) + "]";

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


        



        private int _BIND_ROUNDS_IN_RESULTS = 1;
        public int BIND_ROUNDS_IN_RESULTS
        {
            get { return _BIND_ROUNDS_IN_RESULTS; }

            set
            {
                _BIND_ROUNDS_IN_RESULTS = value;
                OnPropertyChanged("BIND_ROUNDS_IN_RESULTS");
                BIND_ROUNDS_IN_RESULTS_FOR_PRINT = "Výsledky " + value + " kola po skupinách";
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



        private string _BIND_ROUNDS_IN_RESULTS_FOR_PRINT = "Výsledky 1 kola po skupinách";
        public string BIND_ROUNDS_IN_RESULTS_FOR_PRINT
        {
            get { return _BIND_ROUNDS_IN_RESULTS_FOR_PRINT; }

            set
            {
                _BIND_ROUNDS_IN_RESULTS_FOR_PRINT = value;
                OnPropertyChanged("BIND_ROUNDS_IN_RESULTS_FOR_PRINT");

            }
        }



        private int _BIND_ROUNDS_IN_STATISTICS = 1;
        public int BIND_ROUNDS_IN_STATISTICS
        {
            get { return _BIND_ROUNDS_IN_STATISTICS; }

            set
            {
                _BIND_ROUNDS_IN_STATISTICS = value;
                OnPropertyChanged("BIND_ROUNDS_IN_STATISTICS");


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
                    BIND_SQL_SOUTEZ_GROUPS_value = value + 1;
                    OnPropertyChanged("BIND_SQL_SOUTEZ_GROUPS");

                }

                FUNCTION_JETREBAROZLOSOVAT_OVER();
                //




            }
        }



        public int BIND_SELECTED_ROUND
        {
            get { return BIND_SELECTED_ROUND_value; }
            set { 
                BIND_SELECTED_ROUND_value = value; 
                OnPropertyChanged("BIND_SELECTED_ROUND"); 
                OnPropertyChanged("BIND_VYBRANEKOLOMENU"); 
                OnPropertyChanged("BIND_SELECTED_ROUND_DESC"); 
                OnPropertyChanged("BIND_SELECTED_GROUP_DESC"); 
                Console.WriteLine("BIND_SELECTED_ROUND:" + BIND_SELECTED_ROUND);
                FUNCTION_SACLOCK_SETTEXT(0,"R/G : " + BIND_SELECTED_ROUND + "/" + BIND_SELECTED_GROUP);
            }
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





        private int _BIND_SCOREENTRY_ROUND_ROUND;
        public int BIND_SCOREENTRY_ROUND_ROUND
        {
            get { return _BIND_SCOREENTRY_ROUND_ROUND; }
            set { _BIND_SCOREENTRY_ROUND_ROUND = value; OnPropertyChanged("BIND_SCOREENTRY_ROUND_ROUND"); Console.WriteLine("BIND_SCOREENTRY_ROUND_ROUND:" + BIND_SCOREENTRY_ROUND_ROUND); }
        }

        private int _BIND_SCOREENTRY_ROUND_GROUP;
        public int BIND_SCOREENTRY_ROUND_GROUP
        {
            get { return _BIND_SCOREENTRY_ROUND_GROUP; }
            set { _BIND_SCOREENTRY_ROUND_GROUP = value; OnPropertyChanged("BIND_SCOREENTRY_ROUND_GROUP"); Console.WriteLine("BIND_SCOREENTRY_ROUND_GROUP:" + BIND_SCOREENTRY_ROUND_GROUP); }
        }


        private int _BIND_SCOREENTRY_ROUND_STARTPOINT;
        public int BIND_SCOREENTRY_ROUND_STARTPOINT
        {
            get { return _BIND_SCOREENTRY_ROUND_STARTPOINT; }
            set { _BIND_SCOREENTRY_ROUND_STARTPOINT = value; OnPropertyChanged("BIND_SCOREENTRY_ROUND_STARTPOINT"); Console.WriteLine("BIND_SCOREENTRY_ROUND_STARTPOINT:" + BIND_SCOREENTRY_ROUND_STARTPOINT); }
        }







        private int _BIND_SCOREENTRY_SELECTEDFINAL_ROUND;
        public int BIND_SCOREENTRY_SELECTEDFINAL_ROUND
        {
            get { return _BIND_SCOREENTRY_SELECTEDFINAL_ROUND; }
            set { _BIND_SCOREENTRY_SELECTEDFINAL_ROUND = value; OnPropertyChanged("BIND_SCOREENTRY_SELECTEDFINAL_ROUND"); Console.WriteLine("BIND_SCOREENTRY_SELECTEDFINAL_ROUND:" + BIND_SCOREENTRY_SELECTEDFINAL_ROUND); }
        }

        private int _BIND_SCOREENTRY_SELECTEDFINAL_GROUP;
        public int BIND_SCOREENTRY_SELECTEDFINAL_GROUP
        {
            get { return _BIND_SCOREENTRY_SELECTEDFINAL_GROUP; }
            set { _BIND_SCOREENTRY_SELECTEDFINAL_GROUP = value; OnPropertyChanged("BIND_SCOREENTRY_SELECTEDFINAL_GROUP"); Console.WriteLine("BIND_SCOREENTRY_SELECTEDFINAL_GROUP:" + BIND_SCOREENTRY_SELECTEDFINAL_GROUP); }
        }


        private int _BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT;
        public int BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT
        {
            get { return _BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT; }
            set { _BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT = value; OnPropertyChanged("BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT"); Console.WriteLine("BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT:" + BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT); }
        }




        private int _BIND_SCOREENTRY_SELECTEDROUND_ROUND = 1;
        public int BIND_SCOREENTRY_SELECTEDROUND_ROUND
        {
            get { return _BIND_SCOREENTRY_SELECTEDROUND_ROUND; }
            set { _BIND_SCOREENTRY_SELECTEDROUND_ROUND = value; OnPropertyChanged("BIND_SCOREENTRY_SELECTEDROUND_ROUND"); Console.WriteLine("BIND_SCOREENTRY_SELECTEDROUND_ROUND:" + BIND_SCOREENTRY_SELECTEDROUND_ROUND); }
        }

        private int _BIND_SCOREENTRY_SELECTEDROUND_GROUP = 1;
        public int BIND_SCOREENTRY_SELECTEDROUND_GROUP
        {
            get { return _BIND_SCOREENTRY_SELECTEDROUND_GROUP; }
            set { _BIND_SCOREENTRY_SELECTEDROUND_GROUP = value; OnPropertyChanged("BIND_SCOREENTRY_SELECTEDROUND_GROUP"); Console.WriteLine("BIND_SCOREENTRY_SELECTEDROUND_GROUP:" + BIND_SCOREENTRY_SELECTEDROUND_GROUP); }
        }


        private int _BIND_SCOREENTRY_SELECTEDROUND_STARTPOINT = 1;
        public int BIND_SCOREENTRY_SELECTEDROUND_STARTPOINT
        {
            get { return _BIND_SCOREENTRY_SELECTEDROUND_STARTPOINT; }
            set { _BIND_SCOREENTRY_SELECTEDROUND_STARTPOINT = value; OnPropertyChanged("BIND_SCOREENTRY_SELECTEDROUND_STARTPOINT"); Console.WriteLine("BIND_SCOREENTRY_SELECTEDROUND_STARTPOINT:" + BIND_SCOREENTRY_SELECTEDROUND_STARTPOINT); }
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
            set { 
                BIND_SELECTED_GROUP_value = value; 
                OnPropertyChanged("BIND_SELECTED_GROUP"); 
                OnPropertyChanged("BIND_VYBRANEKOLOMENU"); 
                OnPropertyChanged("BIND_SELECTED_ROUND_DESC"); 
                OnPropertyChanged("BIND_SELECTED_GROUP_DESC"); 
                Console.WriteLine("BIND_SELECTED_GROUP" + BIND_SELECTED_GROUP);
                //_serialPortWrite("AT1+TEXTT=1x,R/G:" + BIND_SELECTED_ROUND + "/" + BIND_SELECTED_GROUP, false);
                FUNCTION_SACLOCK_SETTEXT(0, "R/G : " + BIND_SELECTED_ROUND + "/" + BIND_SELECTED_GROUP);
            }
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

        public string BIND_SQL_SACALENDAR_NUMBER
        {
            get { return BIND_SQL_SACALENDAR_NUMBER_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='sorgaircalendarcontestid'"); BIND_SQL_SACALENDAR_NUMBER_value = value; OnPropertyChanged("BIND_SQL_SACALENDAR_NUMBER"); }
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
            get { return Lang.contest_name+ " : " + BIND_SQL_SOUTEZ_NAZEV_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Name'"); BIND_SQL_SOUTEZ_NAZEV_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_NAZEV"); }
        }


        public string BIND_SQL_SOUTEZ_NAZEV_CLEAN
        {
            get { return BIND_SQL_SOUTEZ_NAZEV_value; }
        }




        private string _BIND_SQL_SOUTEZ_DBFILE;
        public string BIND_SQL_SOUTEZ_DBFILE
        {
            get { return _BIND_SQL_SOUTEZ_DBFILE; }
            set { _BIND_SQL_SOUTEZ_DBFILE = value; OnPropertyChanged("BIND_SQL_SOUTEZ_DBFILE"); }
        }
        public string BIND_SQL_SOUTEZ_LOKACE
        {
            get { return Lang.contest_location+ " : " + BIND_SQL_SOUTEZ_LOKACE_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Location'"); BIND_SQL_SOUTEZ_LOKACE_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_LOKACE"); }
        }



        private string _BIND_NEWCONTEST_CATEGORY = "F5J";
        public string BIND_NEWCONTEST_CATEGORY
        {
            get { return _BIND_NEWCONTEST_CATEGORY; }
            set { _BIND_NEWCONTEST_CATEGORY = value; OnPropertyChanged("BIND_NEWCONTEST_CATEGORY"); }
        }


        private string _BIND_NEWCONTEST_NAME = Lang.new_contest;
        public string BIND_NEWCONTEST_NAME
        {
            get { return _BIND_NEWCONTEST_NAME; }
            set { _BIND_NEWCONTEST_NAME = value; OnPropertyChanged("BIND_NEWCONTEST_NAME"); }
        }

        private string _BIND_NEWCONTEST_LOCATION = Lang.where_contest_will_be;
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




        private string _BIND_NEWCONTEST_COUNTRY = "CZE";
        public string BIND_NEWCONTEST_COUNTRY
        {
            get { Console.WriteLine("BIND_NEWCONTEST_COUNTRY"); return _BIND_NEWCONTEST_COUNTRY; }
            set { _BIND_NEWCONTEST_COUNTRY = value; OnPropertyChanged("BIND_NEWCONTEST_COUNTRY"); Console.WriteLine("BIND_NEWCONTEST_COUNTRY"); }
        }



        private string _BIND_NEWCONTEST_CATEGORY_ONLINE = "F5J";
        public string BIND_NEWCONTEST_CATEGORY_ONLINE
        {
            get { return _BIND_NEWCONTEST_CATEGORY_ONLINE; }
            set { _BIND_NEWCONTEST_CATEGORY_ONLINE = value; OnPropertyChanged("BIND_NEWCONTEST_CATEGORY_ONLINE"); }
        }




        private string _BIND_NEWCONTEST_CALENDAR_SOURCE = "http://api.sorgair.com/";
        public string BIND_NEWCONTEST_CALENDAR_SOURCE
        {
            get { return _BIND_NEWCONTEST_CALENDAR_SOURCE; }
            set { _BIND_NEWCONTEST_CALENDAR_SOURCE = value; OnPropertyChanged("BIND_NEWCONTEST_CALENDAR_SOURCE"); }
        }




        private string _BIND_NEWCONTEST_NAME_ONLINE = Lang.new_contest;
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

        private string _BIND_NEWCONTEST_SMCR_ONLINE = "0";
        public string BIND_NEWCONTEST_SMCR_ONLINE
        {
            get { return _BIND_NEWCONTEST_SMCR_ONLINE; }
            set { _BIND_NEWCONTEST_SMCR_ONLINE = value; OnPropertyChanged("BIND_NEWCONTEST_SMCR_ONLINE"); }
        }


        private string _BIND_NEWCONTEST_LOCATION_ONLINE = Lang.where_contest_will_be;
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

        private string _BIND_NEWCONTEST_COUNTRY_ONLINE = "CZE";
        public string BIND_NEWCONTEST_COUNTRY_ONLINE
        {
            get { return _BIND_NEWCONTEST_COUNTRY_ONLINE; }
            set { _BIND_NEWCONTEST_COUNTRY_ONLINE = value; OnPropertyChanged("BIND_NEWCONTEST_COUNTRY_ONLINE"); }
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

        public string BIND_SQL_SOUTEZ_STAT
        {
            get { return BIND_SQL_SOUTEZ_STAT_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='country'"); BIND_SQL_SOUTEZ_STAT_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_STAT"); }
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




        public void StopAllTimers()
        {
            try
            {
                // Stop all timers
                if (MAIN_TIME_TIMER != null && MAIN_TIME_TIMER.IsEnabled)
                {
                    MAIN_TIME_TIMER.Stop();
                }

                if (PREP_TIME_TIMER != null && PREP_TIME_TIMER.IsEnabled)
                {
                    PREP_TIME_TIMER.Stop();
                }

                if (MAIN_FINAL_TIME_TIMER != null && MAIN_FINAL_TIME_TIMER.IsEnabled)
                {
                    MAIN_FINAL_TIME_TIMER.Stop();
                }

                if (PREP_FINAL_TIME_TIMER != null && PREP_FINAL_TIME_TIMER.IsEnabled)
                {
                    PREP_FINAL_TIME_TIMER.Stop();
                }

                if (MAIN_DYNAMIC_ROUNDGROUP_ACTUAL_TIMER != null && MAIN_DYNAMIC_ROUNDGROUP_ACTUAL_TIMER.IsEnabled)
                {
                    MAIN_DYNAMIC_ROUNDGROUP_ACTUAL_TIMER.Stop();
                }

                if (MAIN_DYNAMIC_COMPETITORS_ACTUAL_TIMER != null && MAIN_DYNAMIC_COMPETITORS_ACTUAL_TIMER.IsEnabled)
                {
                    MAIN_DYNAMIC_COMPETITORS_ACTUAL_TIMER.Stop();
                }

                if (MAIN_DYNAMIC_ROUNDGROUP_NEXT_TIMER != null && MAIN_DYNAMIC_ROUNDGROUP_NEXT_TIMER.IsEnabled)
                {
                    MAIN_DYNAMIC_ROUNDGROUP_NEXT_TIMER.Stop();
                }

                if (MAIN_DYNAMIC_COMPETITORS_NEXT_TIMER != null && MAIN_DYNAMIC_COMPETITORS_NEXT_TIMER.IsEnabled)
                {
                    MAIN_DYNAMIC_COMPETITORS_NEXT_TIMER.Stop();
                }

                if (MAIN_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_TIMER != null && MAIN_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_TIMER.IsEnabled)
                {
                    MAIN_DYNAMIC_ROUNDGROUP_FINAL_ACTUAL_TIMER.Stop();
                }

                if (MAIN_DYNAMIC_COMPETITORS_FINAL_ACTUAL_TIMER != null && MAIN_DYNAMIC_COMPETITORS_FINAL_ACTUAL_TIMER.IsEnabled)
                {
                    MAIN_DYNAMIC_COMPETITORS_FINAL_ACTUAL_TIMER.Stop();
                }

                if (MAIN_DYNAMIC_ROUNDGROUP_FINAL_NEXT_TIMER != null && MAIN_DYNAMIC_ROUNDGROUP_FINAL_NEXT_TIMER.IsEnabled)
                {
                    MAIN_DYNAMIC_ROUNDGROUP_FINAL_NEXT_TIMER.Stop();
                }

                if (MAIN_DYNAMIC_COMPETITORS_FINAL_NEXT_TIMER != null && MAIN_DYNAMIC_COMPETITORS_FINAL_NEXT_TIMER.IsEnabled)
                {
                    MAIN_DYNAMIC_COMPETITORS_FINAL_NEXT_TIMER.Stop();
                }

                // Stop all audio players
                if (maintimewaveout != null)
                {
                    foreach (var waveOut in maintimewaveout)
                    {
                        if (waveOut != null)
                        {
                            waveOut.Stop();
                            waveOut.Dispose();
                        }
                    }
                }

                if (preptimewaveout != null)
                {
                    foreach (var waveOut in preptimewaveout)
                    {
                        if (waveOut != null)
                        {
                            waveOut.Stop();
                            waveOut.Dispose();
                        }
                    }
                }

                if (final_maintimewaveout != null)
                {
                    foreach (var waveOut in final_maintimewaveout)
                    {
                        if (waveOut != null)
                        {
                            waveOut.Stop();
                            waveOut.Dispose();
                        }
                    }
                }

                if (final_preptimewaveout != null)
                {
                    foreach (var waveOut in final_preptimewaveout)
                    {
                        if (waveOut != null)
                        {
                            waveOut.Stop();
                            waveOut.Dispose();
                        }
                    }
                }

                // Close serial ports
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    _serialPort.Close();
                    _serialPort.Dispose();
                }

                if (_RPI_serialPort != null && _RPI_serialPort.IsOpen)
                {
                    _RPI_serialPort.Close();
                    _RPI_serialPort.Dispose();
                }

                Console.WriteLine("All timers, audio players, and serial ports stopped successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping timers: {ex.Message}");
            }
        }


        #endregion

        #region SQL_funkce
        public async Task SQL_OPENCONNECTION(string KTERADB)
        {
            try
            {
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var directory = System.IO.Path.GetDirectoryName(path);

                if (KTERADB == "SORG")
                {
                    var dbPath = Path.Combine(directory, "Data", "config", "sorgair.db");

                    if (!File.Exists(dbPath))
                    {
                        MessageBox.Show($"Databázový soubor nebyl nalezen:\n{dbPath}", 
                            "Chyba - Soubor nenalezen", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var connectionString = new SQLiteConnectionStringBuilder
                    {
                        DataSource = dbPath,
                        Version = 3,
                        FailIfMissing = false
                    }.ToString();

                    DBSORG_Connection = new SQLiteConnection(connectionString);
                    await Task.Run(() => DBSORG_Connection.Open());
                }
                else if (KTERADB == "RULES")
                {
                    var dbPath = Path.Combine(directory, "Data", "config", "rules.db");

                    if (!File.Exists(dbPath))
                    {
                        MessageBox.Show($"Databázový soubor nebyl nalezen:\n{dbPath}", 
                            "Chyba - Soubor nenalezen", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var connectionString = new SQLiteConnectionStringBuilder
                    {
                        DataSource = dbPath,
                        Version = 3,
                        FailIfMissing = false
                    }.ToString();

                    DBSORG_Connection = new SQLiteConnection(connectionString);
                    await Task.Run(() => DBSORG_Connection.Open());
                }
                else
                {
                    var dbPath = Path.Combine(directory, "Data", KTERADB + ".db");

                    if (!File.Exists(dbPath))
                    {
                        MessageBox.Show($"Databázový soubor nebyl nalezen:\n{dbPath}", 
                            "Chyba - Soubor nenalezen", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var connectionString = new SQLiteConnectionStringBuilder
                    {
                        DataSource = dbPath,
                        Version = 3,
                        FailIfMissing = false
                    }.ToString();

                    DBSOUTEZ_Connection = new SQLiteConnection(connectionString);
                    await Task.Run(() => DBSOUTEZ_Connection.Open());
                    BIND_SQL_SOUTEZ_DBFILE = KTERADB;
                }

                Console.WriteLine("SQL_OPENCONNECTION [OPEN] : " + KTERADB);
            }
            catch (DllNotFoundException dllEx)
            {
                MessageBox.Show(
                    "Chybí SQLite nativní knihovny (SQLite.Interop.dll).\n\n" +
                    "ŘEŠENÍ:\n" +
                    "1. Přeinstalujte balíček: System.Data.SQLite.Core\n" +
                    "2. Zkontrolujte, že existují složky x86 a x64 s SQLite.Interop.dll\n" +
                    "3. Ujistěte se, že Platform Target je nastaven na x64 nebo x86\n\n" +
                    $"Technická chyba: {dllEx.Message}",
                    "SQLite - Chyba knihoven",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (SQLiteException sqlEx)
            {
                MessageBox.Show(
                    $"Chyba SQLite databáze:\n{sqlEx.Message}\n\n" +
                    $"Databáze: {KTERADB}",
                    "SQLite - Chyba databáze",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Console.WriteLine($"SQLite Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Neočekávaná chyba při otevírání databáze:\n{ex.Message}\n\n" +
                    $"Databáze: {KTERADB}",
                    "Chyba",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Console.WriteLine($"Error in SQL_OPENCONNECTION: {ex.Message}\n{ex.StackTrace}");
            }
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
                Console.WriteLine("SQL_SAVESORGDATA [ERROR] : " + myException.Message + "\n");
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



        private string _BINDING_SELECTED_AGECAT = Lang.age_cat_all;
        public string BINDING_SELECTED_AGECAT
        {
            get { return _BINDING_SELECTED_AGECAT; }

            set
            {
                _BINDING_SELECTED_AGECAT = value;
                if (value == Lang.age_cat_all){ BINDING_SELECTED_AGECAT_ID = 99; }
                if (value == Lang.age_cat_seniors) { BINDING_SELECTED_AGECAT_ID = 0; }
                if (value == Lang.age_cat_juniors) { BINDING_SELECTED_AGECAT_ID = 1; }
                if (value == Lang.age_cat_pup) { BINDING_SELECTED_AGECAT_ID = 2; }
                if (value == Lang.age_cat_65) { BINDING_SELECTED_AGECAT_ID = 3; }
                if (value == Lang.age_cat_woman) { BINDING_SELECTED_AGECAT_ID = 4; }

                OnPropertyChanged("BINDING_SELECTED_AGECAT");
                OnPropertyChanged("BINDING_SELECTED_AGECAT_ID");

            }
        }

        private int _BINDING_SELECTED_AGECAT_ID = 99;
        public int BINDING_SELECTED_AGECAT_ID
        {
            get { return _BINDING_SELECTED_AGECAT_ID; }

            set
            {
                _BINDING_SELECTED_AGECAT_ID = value;
                OnPropertyChanged("BINDING_SELECTED_AGECAT_ID");

            }
        }


        private bool _BINDING_IS_INTERNET= false ;
        public bool BINDING_IS_INTERNET
        {
            get { return _BINDING_IS_INTERNET; }

            set
            {
                _BINDING_IS_INTERNET = value;
                OnPropertyChanged("BINDING_IS_INTERNET");

            }
        }


        public List<String> agecatitems
        {
            get { return new List<String> { Lang.age_cat_all, Lang.age_cat_seniors, Lang.age_cat_juniors, Lang.age_cat_pup, Lang.age_cat_65, Lang.age_cat_woman }; }
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
            try
            {
                if (KTERADB == "SORG")
                {
                    if (DBSORG_Connection != null && DBSORG_Connection.State != System.Data.ConnectionState.Closed)
                    {
                        DBSORG_Connection.Close();
                    }
                }

                if (KTERADB == "RULES")
                {
                    if (DBSORG_Connection != null && DBSORG_Connection.State != System.Data.ConnectionState.Closed)
                    {
                        DBSORG_Connection.Close();
                    }
                }


                if (KTERADB == "SOUTEZ")
                {
                    if (DBSOUTEZ_Connection != null && DBSOUTEZ_Connection.State != System.Data.ConnectionState.Closed)
                    {
                        DBSOUTEZ_Connection.Close();
                    }


                }

                Console.WriteLine("SQL_CLOSECONNECTION [CLOSE] : " + KTERADB);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SQL_CLOSECONNECTION [ERROR] : {KTERADB} - {ex.Message}");
            }

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
                            RECTO1000FROMABSMAX = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("RECTO1000FROMABSMAX"))),
                            RECOUNTSCORETO1000 = bool.Parse(sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("RECOUNTSCORETO1000")))



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





        public string SQL_READSOUTEZDATA_GETALL(string sqltext, string delimiter,string arraydelimiter, int pocetsloupcu, string starttag, string endtag)
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
                if (pocetsloupcu == 1) { 
                vysledek = vysledek + sqlite_datareader.GetString(0) + delimiter;
                    }
                    else
                    {
                        string multivysledek = "";
                        for (int x = 0; x < pocetsloupcu; x++)
                        {
                            string obalovac = "";
                            if (x == 0) { obalovac = "'"; } 
                            multivysledek = multivysledek + obalovac + sqlite_datareader.GetString(x) + obalovac + arraydelimiter;
                            
                        }
                        vysledek = vysledek + starttag + (multivysledek.Remove(multivysledek.Length - (arraydelimiter.Length))) + endtag + delimiter;
                    }
                }
            }

            catch (SQLiteException myException)
            {
                Console.WriteLine("SQL_READSOUTEZDATA_ALL [ERROR] : " + myException.Message + "\n");
            }


            if (delimiter.Length > 0)
            {
                if ( vysledek.Length >= delimiter.Length){
                    return vysledek.Remove(vysledek.Length - (delimiter.Length));
                }
                else
                {
                    return vysledek;
                }



            }
            else
            {
                return vysledek;

            }
        }



        public string SQL_READSOUTEZDATA(string sqltext, string kamulozitvysledek, int xto_round = 0)
        {
            int _results_autoincrement = 0;
            double _results_scoreompare = 1000 * (BIND_SQL_SOUTEZ_ROUNDS - BIND_SQL_SOUTEZ_DELETES);

            double _results_scoreompare_do_kola = 0;

            if (_ZOBRAZIT_ZAKLADNI_VYSLEDKY_S_SKRTACKAMA == true)
            {
                _results_scoreompare_do_kola = 1000 * (BIND_ROUNDS_IN_RESULTS - BIND_SQL_SOUTEZ_DELETES);
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



                        string rowValues = "";

                        // Projdeme všechny sloupce ve výsledku
                        for (int i = 0; i < sqlite_datareader.FieldCount; i++)
                        {
                            // Získáme hodnotu aktuálního sloupce jako řetězec
                            // Použijeme ternární operátor pro zjednodušení kódu - pokud je hodnota null, vložíme "null"
                            string value = sqlite_datareader.IsDBNull(i) ? "null" : sqlite_datareader.GetValue(i).ToString();

                            // Přidáme hodnotu do řetězce s hodnotami řádku, oddělené například čárkou
                            rowValues += value + (i < sqlite_datareader.FieldCount - 1 ? ", " : "");
                        }

                        // Vypíšeme hodnoty z aktuálního řádku
                        Console.WriteLine(rowValues);


                        string jmeno = sqlite_datareader.GetString(1);
                        string prijmeni = sqlite_datareader.GetString(2);
                        string country = sqlite_datareader.GetString(3);
                        string agecat = sqlite_datareader.GetString(4);
                        string freq = sqlite_datareader.GetString(5);
                        int ch1 = sqlite_datareader.GetInt32(6);
                        int ch2 = sqlite_datareader.GetInt32(7);
                        string failic = sqlite_datareader.GetString(8);
                        string naclic = sqlite_datareader.GetString(9);
                        string club = sqlite_datareader.GetString(10);
                        string paid = sqlite_datareader.GetString(11);
                        int team = sqlite_datareader.GetInt32(12);


                        string customagecat = Convert.ToString(sqlite_datareader.GetValue(13));

                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + jmeno + " >>>> " + kamulozitvysledek);
                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + prijmeni + " >>>> " + kamulozitvysledek);
                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + country + " >>>> " + kamulozitvysledek);

                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var directory = System.IO.Path.GetDirectoryName(path);
                        string dataType = sqlite_datareader.GetDataTypeName(16);
                        Console.WriteLine("Datový typ sloupce 17 je: " + dataType);
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
                            FREQID = Convert.ToInt32(sqlite_datareader.GetValue(14)),
                            AGECATID = Convert.ToInt32(sqlite_datareader.GetValue(15)),
                            CUSTOMAGECATID = Convert.ToInt32(sqlite_datareader.GetValue(16))

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
                            DATA4 = SQL_READSOUTEZDATA_GETALL("select cast(height as text) from score where prep > 0 and rnd <= " + xto_round + " and userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), " | ", "", 1, "", ""),
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
                            DATA4 = SQL_READSOUTEZDATA_GETALL("select height || '/' || prep from score where prep > 0 and rnd <= " + xto_round + " and userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), " | ", "", 1, "", ""),
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
                    if (kamulozitvysledek == "get_statistics_enemykiled")
                    {


                        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var directory = System.IO.Path.GetDirectoryName(path);

                        int score = 0;

                        for (int x = 1; x < BIND_SQL_SOUTEZ_ROUNDS; x++)
                        {
                            for (int y = 1; y < BIND_SQL_SOUTEZ_GROUPS; y++)
                            {
                                score = score + int.Parse(SQL_READSOUTEZDATA_GETALL("select cast(ifnull(count(userid),0) as text) from score where rnd=" + x + " and grp=" + y + " and prep <= (select prep from score where rnd=" + x + " and grp=" + y + " and userid=" + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + ")", "|", "", 1, "", ""));
                            }
                        }


                        var _Players_statistics = new MODEL_Player_statistics()
                        {
                            POSITION = _results_autoincrement.ToString(),
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("lastname")) + " " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("firstname")),
                            FLAG = directory + "/flags/" + SQL_READSOUTEZDATA("select country from users where id = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), "") + ".png",
                            DATA = score

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
                            DATA4 = SQL_READSOUTEZDATA_GETALL("select strftime('%M:%S',time    ('00:00:00', (minutes*60+seconds) || ' seconds')) from score where prep > 0 and rnd <= " + xto_round + " and userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), " | ", "", 1, "", ""),
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
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("lastname")) + " " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("firstname")),
                            FLAG = directory + "/flags/" + SQL_READSOUTEZDATA("select country from users where id = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), "") + ".png",
                            DATA = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("height")),
                            DATA2 = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("sumheight")),
                            DATA4 = SQL_READSOUTEZDATA_GETALL("select cast(height as text) from score where prep > 0 and rnd <= " + xto_round + " and userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), " | ", "", 1, "", ""),
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
                            DATA = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("pristani")),
                            DATA2 = sqlite_datareader.GetDecimal(sqlite_datareader.GetOrdinal("sumpristani")),
                            DATA4 = SQL_READSOUTEZDATA_GETALL("select cast(landing as text) from score where prep > 0  and rnd <= " + xto_round + " and userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")), " | ", "", 1, "", ""),
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

                        if (_results_autoincrement <= BIND_SQL_SOUTEZ_STARTPOINTSFINALE)
                        {
                            tmp_hvezdickafinalisty = "* ";
                        }
                        else
                        {
                            tmp_hvezdickafinalisty = "";
                        }


                        if (maxscoreproprocenta < sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore")))
                        {
                            maxscoreproprocenta = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore"));
                        }
                        var _Players_Baseresults = new MODEL_Player_baseresults()
                        {
                            POSITION = tmp_hvezdickafinalisty + _results_autoincrement.ToString(),
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")),
                            AGECAT = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("agecatstring")),

                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname")) + "  " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname")),
                            RAWSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalrawscore")),
                            GPEN = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("gpen")),
                            PREPSCORE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore")),
                            PREPSCOREDIFF = Math.Round(sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore")) - _results_scoreompare_do_kola, 2).ToString("0.00"),

                            PROCENTASCORE = Math.Round((sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore")) / maxscoreproprocenta) * 100,2),

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




                            RND11RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=11 and refly='False'", ""),
                            RND11RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=11 and refly='False'", ""),
                            RND11RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=11 and refly='False'", ""),

                            RND12RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=12 and refly='False'", ""),
                            RND12RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=12 and refly='False'", ""),
                            RND12RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=12 and refly='False'", ""),

                            RND13RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=13 and refly='False'", ""),
                            RND13RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=13 and refly='False'", ""),
                            RND13RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=13 and refly='False'", ""),

                            RND14RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=14 and refly='False'", ""),
                            RND14RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=14 and refly='False'", ""),
                            RND14RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=14 and refly='False'", ""),

                            RND15RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=15 and refly='False'", ""),
                            RND15RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=15 and refly='False'", ""),
                            RND15RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=15 and refly='False'", ""),

                            RND16RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=16 and refly='False'", ""),
                            RND16RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=16 and refly='False'", ""),
                            RND16RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=16 and refly='False'", ""),

                            RND17RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=17 and refly='False'", ""),
                            RND17RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=17 and refly='False'", ""),
                            RND17RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=17 and refly='False'", ""),

                            RND18RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=18 and refly='False'", ""),
                            RND18RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=18 and refly='False'", ""),
                            RND18RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=18 and refly='False'", ""),

                            RND19RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=19 and refly='False'", ""),
                            RND19RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=19 and refly='False'", ""),
                            RND19RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=19 and refly='False'", ""),

                            RND20RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' | G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=20 and refly='False'", ""),
                            RND20RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' | '||landing||' | '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=20 and refly='False'", ""),
                            RND20RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=20 and refly='False'", ""),


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
                            if (_results_autoincrement < BIND_SQL_SOUTEZ_STARTPOINTSFINALE + 1 & BIND_SQL_SOUTEZ_ROUNDSFINALE > 0)
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
                        if (SQL_READSOUTEZDATA("select RECTO1000FROMABSMAX from rules", "") == "True")
                        {
                            maxscorefor1000 = double.Parse(SQL_READSOUTEZDATA("select max(((select sum(prep) from score s2 where s2.userid = s1.userid and rnd < 100 and skrtacka='False' and refly='False' ))) overalscore_base from score s1 left join users U on S1.userid = U.id", ""));
                        }
                        else
                        {
                            maxscorefor1000 = BIND_SQL_SOUTEZ_ROUNDS * 1000;
                        }


                        if (maxscoreproprocenta < sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore_base")))
                        {
                            maxscoreproprocenta = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore_base"));
                        }


                        var _Players_Baseresults_complete = new MODEL_Player_baseresults_complete()
                        {
                            POSITION = _results_autoincrement,
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")),
                            PLAYERDATA = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname")) + "  " + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname")),
                            AGECAT = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("agecatstring")) + "\n" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("customagecatstring")),
                            NATLIC = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("naclic")),
                            FAILIC = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("failic")),

                            RAWSCORE_BASE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalrawscore_base")),
                            PREPSCORE_BASE = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore_base")),
                            RAWSCORE_FINAL = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalrawscore_fin")),
                            PREPSCORE_FINAL = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore_fin")),
                            GPEN = sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("gpen")),
                            PREPSCOREDIFF_BASE = Math.Round(sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore_base")) - _results_scoreompare, 2).ToString("0.00"),
                            PREPSCOREDIFF_FINAL = Math.Round(sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore_fin")) - _results_scoreompare_final, 2).ToString("0.00"),
                            PROCENTASCORE = Math.Round((sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore_base")) / maxscoreproprocenta) * 100,2),


                            TO_1000 = Math.Round((sqlite_datareader.GetDouble(sqlite_datareader.GetOrdinal("overalscore_base")) / maxscorefor1000) * 1000, 2).ToString("0.00"),

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

                            RND11RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=11 and refly='False'", ""),
                            RND11RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=11 and refly='False'", ""),
                            RND11RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=11 and refly='False'", ""),

                            RND12RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=12 and refly='False'", ""),
                            RND12RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=12 and refly='False'", ""),
                            RND12RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=12 and refly='False'", ""),

                            RND13RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=13 and refly='False'", ""),
                            RND13RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=13 and refly='False'", ""),
                            RND13RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=13 and refly='False'", ""),

                            RND14RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=14 and refly='False'", ""),
                            RND14RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=14 and refly='False'", ""),
                            RND14RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=14 and refly='False'", ""),

                            RND15RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=15 and refly='False'", ""),
                            RND15RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=15 and refly='False'", ""),
                            RND15RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=15 and refly='False'", ""),

                            RND16RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=16 and refly='False'", ""),
                            RND16RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=16 and refly='False'", ""),
                            RND16RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=16 and refly='False'", ""),

                            RND17RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=17 and refly='False'", ""),
                            RND17RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=17 and refly='False'", ""),
                            RND17RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=17 and refly='False'", ""),

                            RND18RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=18 and refly='False'", ""),
                            RND18RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=18 and refly='False'", ""),
                            RND18RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=18 and refly='False'", ""),

                            RND19RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=19 and refly='False'", ""),
                            RND19RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=19 and refly='False'", ""),
                            RND19RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=19 and refly='False'", ""),

                            RND20RES_SCORE = SQL_READSOUTEZDATA("select cast(prep as text) || ' / G' || grp from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=20 and refly='False'", ""),
                            RND20RES_DATA = SQL_READSOUTEZDATA("select minutes ||':'|| seconds ||' / '||landing||' / '||height  from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=20 and refly='False'", ""),
                            RND20RES_SKRTACKA = SQL_READSOUTEZDATA("select skrtacka from score where userid = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("userid")) + " and rnd=20 and refly='False'", ""),



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
                            Id = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
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
                            TEXTVALUE = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("textvalue")) + " [" + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("value")) + "]",
                            DELETE_TIME = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_time")),
                            DELETE_LANDING = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_landing")),
                            DELETE_ALL = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("delete_all")),

                        };
                        Console.WriteLine("PENLOC" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("textvalue")));
                        BINDING_Timer_listofpenalisationlocal.Add(_penalisation);
                    }
                    if (kamulozitvysledek == "get_contest_sound_main")
                    {
                        //Console.WriteLine("get_contest_sound_main");

                        int i = MODEL_CONTEST_SOUNDS_MAIN.Count;
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
                            fileContent = File.ReadAllBytes("Audio\\" + BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) + ".wav");
                        }

                        wav_maintime[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));

                        maintimewaveout[i] = new WaveOutEvent();
                        maintimewaveout[i].Init(wav_maintime[i]);
                        //Console.WriteLine("NANAUDIO _ " + "Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) + ".wav");

                        var _sound = new MODEL_CATEGORY_LANDING()
                        {
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("id")),
                            VALUE = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("second")),
                            TEXTVALUE = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")),
                            CATEGORY = i,
                            LENGHT = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filedesc")),
                            TODEL = 0
                        };

                        //Console.WriteLine("add to MODEL_CONTEST_SOUNDS_MAIN:" + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("second")));
                        MODEL_CONTEST_SOUNDS_MAIN.Add(_sound);
                    }
                    if (kamulozitvysledek == "get_contest_sound_final_main")
                    {
                        //Console.WriteLine("get_contest_sound_final_main");

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
                            fileContent = File.ReadAllBytes("Audio\\" + BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) + ".wav");
                        }

                        wav_final_maintime[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));

                        final_maintimewaveout[i] = new WaveOutEvent();
                        final_maintimewaveout[i].Init(wav_final_maintime[i]);
                        //Console.WriteLine("FINALNANAUDIO _ " + "Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) + ".wav");

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
                        //Console.WriteLine("get_contest_sound_prep");

                        int i = MODEL_CONTEST_SOUNDS_PREP.Count;

                        byte[] fileContent;

                        if (sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) == "---FUNKY---")
                        {
                            fileContent = File.ReadAllBytes("Audio\\FUNKYMODE\\western.wav");
                        }
                        else
                        {
                            fileContent = File.ReadAllBytes("Audio\\" + BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) + ".wav");
                        }


                        wav_preptime[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));

                        preptimewaveout[i] = new WaveOutEvent();
                        preptimewaveout[i].Init(wav_preptime[i]);
                        //Console.WriteLine("NANAUDIO _ " + "Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) + ".wav");

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
                        //Console.WriteLine("get_contest_sound_final_prep");

                        int i = MODEL_CONTEST_SOUNDS_FINAL_PREP.Count;

                        byte[] fileContent;

                        if (sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) == "---FUNKY---")
                        {
                            fileContent = File.ReadAllBytes("Audio\\FUNKYMODE\\western.wav");
                        }
                        else
                        {
                            fileContent = File.ReadAllBytes("Audio\\" + BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) + ".wav");
                        }


                        wav_final_preptime[i] = new NAudio.Wave.WaveFileReader(new MemoryStream(fileContent));

                        final_preptimewaveout[i] = new WaveOutEvent();
                        final_preptimewaveout[i].Init(wav_final_preptime[i]);
                        //Console.WriteLine("NANAUDIO _ " + "Audio\\"+ BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("filename")) + ".wav");

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

                        string tmp_refly_data_from = SQL_READSOUTEZDATA("select ifnull(r.rnd_from || '-'  || r.grp_from || '-' || r.stp_from || ' <> ' || r.rnd_to || '-' || r.grp_to || '-' || r.stp_to,'Refly') rfly from refly R where  r.rnd_from = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("rnd")) + " and r.grp_from = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("grp")) + " and r.stp_from = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("stp")), "");
                        string tmp_refly_data_to = SQL_READSOUTEZDATA("select ifnull(r.rnd_from || '-'  || r.grp_from || '-' || r.stp_from || ' <> ' || r.rnd_to || '-' || r.grp_to || '-' || r.stp_to,'Refly') rfly from refly R where  r.rnd_to = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("rnd")) + " and r.grp_to = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("grp")) + " and r.stp_to = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("stp")), "");


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

                        string tmp_refly_data_from = SQL_READSOUTEZDATA("select ifnull(r.rnd_from || '-'  || r.grp_from || '-' || r.stp_from || ' <> ' || r.rnd_to || '-' || r.grp_to || '-' || r.stp_to,'Refly') rfly from refly R where  r.rnd_from = " + (sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("rnd")) + 0) + " and r.grp_from = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("grp")) + " and r.stp_from = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("stp")), "");
                        string tmp_refly_data_to = SQL_READSOUTEZDATA("select ifnull(r.rnd_from || '-'  || r.grp_from || '-' || r.stp_from || ' <> ' || r.rnd_to || '-' || r.grp_to || '-' || r.stp_to,'Refly') rfly from refly R where  r.rnd_to = " + (sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("rnd")) + 0) + " and r.grp_to = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("grp")) + " and r.stp_to = " + sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("stp")), "");


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
                    if (kamulozitvysledek == "get_Players_Actual_SelectedRoundandgroup")
                    {


                        bool _ISENABLED = true;
                        if (sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("ID")) == 0)
                        {
                            _ISENABLED = false;
                        }

                        var player_actualgrp = new MODEL_Player_selected()
                        {
                            ID = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("startpoint")),
                            LASTNAME = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Lastname")),
                            FIRSTNAME = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Firstname")),
                            SCORE_MINUTES = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("minutes")),
                            SCORE_SECONDS = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("seconds")),
                            SCORE_LANDING = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("landing")),
                            SCORE_HEIGHT = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("height")),
                            SCORE_RAW = sqlite_datareader.GetValue(sqlite_datareader.GetOrdinal("raw")).ToString(),
                            SCORE_PREP = sqlite_datareader.GetValue(sqlite_datareader.GetOrdinal("prep")).ToString()

                        };
                        Players_Actual_SelectedRoundandGroup.Add(player_actualgrp);

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
                            GROUPTYPE = sqlite_datareader.GetString(4),
                            GROUPLENGHT = sqlite_datareader.GetInt32(5),
                            GROUPZADANO = sqlite_datareader.GetInt32(6),

                        };
                        MODEL_CONTEST_GROUPS.Add(groups);
                        vysledek = kamulozitvysledek;
                    }
                    if (kamulozitvysledek == "get_final_groups")
                    {

                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + sqltext + " AA>>>> " + kamulozitvysledek);

                        if (int.Parse(SQL_READSOUTEZDATA("select count(id) from Groups_final where masterround=" + (BIND_SELECTED_FINAL_ROUND + 100), "")) > 0)
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
                            FIRSTNAME = sqlite_datareader.GetString(1),
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
                        UsersNOTinteams.Add(usrsnotintm);
                        vysledek = "get_usersnotinteam";
                    }
                    if (kamulozitvysledek == "get_agecategories")
                    {
                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + sqlite_datareader.GetString(1) + " >>>> " + kamulozitvysledek);
                        var tmp_agecategories = new MODEL_Player_agecategories()
                        {
                            ID = sqlite_datareader.GetInt32(0),
                            NAME = sqlite_datareader.GetString(1)
                        };
                        MODEL_Contest_AGECATEGORIES.Add(tmp_agecategories);
                        vysledek = kamulozitvysledek;
                    }
                    if (kamulozitvysledek == "get_customagecategories")
                    {
                        Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + sqlite_datareader.GetString(1) + " >>>> " + kamulozitvysledek);
                        var tmp_agecategories = new MODEL_Player_agecategories()
                        {
                            ID = sqlite_datareader.GetInt32(0),
                            NAME = sqlite_datareader.GetString(1)
                        };
                        MODEL_Contest_CUSTOMAGECATEGORIES.Add(tmp_agecategories);
                        vysledek = kamulozitvysledek;



                        // Při načítání dat z databáze byste naplnili idList IDčky
                        customagecatidList.Add(sqlite_datareader.GetInt32(0)); // Příklad přidání ID, které začínají 5 a jdou výše

                    }

                    if (kamulozitvysledek == "")
                    {
                        try
                        {
                            Console.WriteLine("SQL RETURN TYPE:" + sqlite_datareader.GetFieldType(0));


                            if (sqlite_datareader.GetFieldType(0) == typeof(Int64))
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



                            if (sqlite_datareader.GetFieldType(0) == typeof(string))
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



        public List<string> SQL_READSOUTEZDATA_RETURNARR(string sqltext, List<string> columnNames)
        {
            SQLiteCommand command = new SQLiteCommand(sqltext, DBSOUTEZ_Connection);
            List<string> vysledky = new List<string>();
            SQLiteDataReader sqlite_datareader;
            try
            {
                sqlite_datareader = command.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    try
                    {
                        List<string> rowValues = new List<string>();

                        // Načteme hodnoty jednotlivých sloupců podle zadaných názvů
                        foreach (string columnName in columnNames)
                        {
                            rowValues.Add(sqlite_datareader[columnName].ToString());
                        }

                        // Spojíme hodnoty do jednoho řetězce odděleného čárkami
                        string row = string.Join(";", rowValues);
                        vysledky.Add(row);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid data type: " + ex.Message);
                    }
                }
            }
            catch (SQLiteException myException)
            {
                Console.WriteLine("SQL_READSOUTEZDATA [ERROR] : " + myException.Message + "\n");
            }

            return vysledky;
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
          
                FUNCTION_CLOCK_SET_STOPWATCH_MODE();




            if (MODEL_CONTEST_SOUNDS_MAIN.Count > 0)
            {

                if (MODEL_CONTEST_SOUNDS_MAIN[0].VALUE < 0)
                {
                    BIND_TYPEOFCLOCK = "PRE_MAIN";
                    BIND_LETOVYCAS_MAX = Math.Abs(MODEL_CONTEST_SOUNDS_MAIN[0].VALUE);

                    //FUNCTION_SACLOCK_COUNTDOWN(0);


                    FUNCTION_CLOCK_SET_STOPWATCH_TIME(0, Math.Abs(MODEL_CONTEST_SOUNDS_MAIN[0].VALUE));
                    FUNCTION_CLOCK_SET_DIRECTION(2);
                  


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




            BIND_LETOVYCAS = 0;
            timer_main.Reset();
            MAIN_TIME_TIMER.Start();
            timer_main.Start();

            if (BIND_TYPEOFCLOCK == "PRE_MAIN")
            {

                //FUNCTION_SACLOCK_DELETE_CLOCK(0,false);
                //FUNCTION_SACLOCK_CREATE_CLOCK(Math.Abs(MODEL_CONTEST_SOUNDS_MAIN[0].VALUE), 50, true);
                //FUNCTION_SACLOCK_STOPCOUNT(0);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(0, Math.Abs(MODEL_CONTEST_SOUNDS_MAIN[0].VALUE), false);
                FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(18, 0, false);
                   
                    if (BIND_PREPTIME_ISRUNNING is true)
                    {
                    //FUNCTION_SACLOCK_DELETE_CLOCK(1,true);
                    //FUNCTION_SACLOCK_CREATE_SMALLCLOCK(BIND_LETOVYCAS_PREP, 50, true);
                    //preptime_clock_id = 1;
                    //FUNCTION_SACLOCK_STOPCOUNT(1);
                    FUNCTION_SACLOCK_SETTIMETO_CLOCK(1, BIND_LETOVYCAS_PREP_MAX - BIND_LETOVYCAS_PREP, false);
                    FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(50, 1, false);


                }
                else
                    {
                        //FUNCTION_SACLOCK_DELETE_CLOCK(1,true);
                        //FUNCTION_SACLOCK_CREATE_SMALLCLOCK_RTC(true);

                    }

              


            }

            BIND_MAINTIME_ISRUNNING = true;
            BIND_MAINTIME_ISSTOPED = false;


            BIND_FINAL_MAINTIME_ISRUNNING = false;
            BIND_FINAL_MAINTIME_ISSTOPED = false;
            BIND_FINAL_PREPTIME_ISRUNNING = false;
            BIND_FINAL_PREPTIME_ISSTOPED = false;

          
            

        }



        public void clock_FINAL_MAIN_start()
        {
           
                FUNCTION_CLOCK_SET_STOPWATCH_MODE();
          

            if (MODEL_CONTEST_SOUNDS_FINAL_MAIN.Count > 0)
            {

                if (MODEL_CONTEST_SOUNDS_FINAL_MAIN[0].VALUE < 0)
                {
                    BIND_TYPEOFCLOCK = "PRE_MAIN";
                    BIND_FINAL_LETOVYCAS_MAX = Math.Abs(MODEL_CONTEST_SOUNDS_FINAL_MAIN[0].VALUE);
                   
                  
                        FUNCTION_CLOCK_SET_STOPWATCH_TIME(0, Math.Abs(MODEL_CONTEST_SOUNDS_FINAL_MAIN[0].VALUE));
                        FUNCTION_CLOCK_SET_DIRECTION(2);
                   

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




            if (BIND_TYPEOFCLOCK == "PRE_MAIN")
            {

                //FUNCTION_SACLOCK_DELETE_CLOCK(0,false);
                //FUNCTION_SACLOCK_CREATE_CLOCK(Math.Abs(MODEL_CONTEST_SOUNDS_MAIN[0].VALUE), 50, true);
                //FUNCTION_SACLOCK_STOPCOUNT(0);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(0, Math.Abs(MODEL_CONTEST_SOUNDS_FINAL_MAIN[0].VALUE), false);
                FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(18, 0, false);

                if (BIND_FINAL_PREPTIME_ISRUNNING is true)
                {
                    //FUNCTION_SACLOCK_DELETE_CLOCK(1,true);
                    //FUNCTION_SACLOCK_CREATE_SMALLCLOCK(BIND_LETOVYCAS_PREP, 50, true);
                    //preptime_clock_id = 1;
                    //FUNCTION_SACLOCK_STOPCOUNT(1);
                    FUNCTION_SACLOCK_SETTIMETO_CLOCK(1, BIND_FINAL_LETOVYCAS_PREP_MAX - BIND_FINAL_LETOVYCAS_PREP, false);
                    FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(50, 1, false);


                }
                else
                {
                    //FUNCTION_SACLOCK_DELETE_CLOCK(1,true);
                    //FUNCTION_SACLOCK_CREATE_SMALLCLOCK_RTC(true);

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

            FUNCTION_CLOCK_SET_DIRECTION(0);


            MAIN_FINAL_TIME_TIMER.Stop();
            timer_final_main.Stop();
            clock_DYNAMIC_ROUNDGROUP_ACTUAL_stop();
            clock_DYNAMIC_COMPETITORS_ACTUAL_stop();





            if (BIND_FINAL_PREPTIME_ISRUNNING is true)
            {
                //VM.FUNCTION_SACLOCK_DELETE_CLOCK(0,true);
                //VM.FUNCTION_SACLOCK_CREATE_CLOCK(VM.BIND_LETOVYCAS_PREP_MAX - VM.BIND_LETOVYCAS_PREP, 50, true);
                //VM.FUNCTION_SACLOCK_DELETE_CLOCK(1,true);
                //VM.FUNCTION_SACLOCK_CREATE_SMALLCLOCK_RTC(true);
                //VM.preptime_clock_id = 0;
                //FUNCTION_SACLOCK_STOPCOUNT(0);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(0, BIND_FINAL_LETOVYCAS_PREP_MAX - BIND_FINAL_LETOVYCAS_PREP, false);
                FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(18, 0, false);

                //FUNCTION_SACLOCK_STOPCOUNT(1);
                var datet = DateTime.Now;
                int hodiny = datet.Hour * 60 * 60;
                int minuty = datet.Minute * 60;
                int sum = hodiny + minuty;
                FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(136, 1, false);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(1, sum, false);



            }
            else
            {
                FUNCTION_SACLOCK_STOPCOUNT(0);
            }



            BIND_FINAL_MAINTIME_ISRUNNING = false;
            BIND_FINAL_MAINTIME_ISSTOPED = true;


            BIND_MAINTIME_ISRUNNING = false;
            BIND_MAINTIME_ISSTOPED = true;
            BIND_PREPTIME_ISRUNNING = false;
            BIND_PREPTIME_ISSTOPED = true;

            FUNCTION_CLOCK_SET_CLOCK_MODE();

        }


        public void clock_MAIN_stop()
        {

           
            FUNCTION_CLOCK_SET_DIRECTION(0);
          

            MAIN_TIME_TIMER.Stop ();
            timer_main.Stop ();
            clock_DYNAMIC_ROUNDGROUP_ACTUAL_stop();
            clock_DYNAMIC_COMPETITORS_ACTUAL_stop();


            

            if (BIND_PREPTIME_ISRUNNING is true)
            {
                //VM.FUNCTION_SACLOCK_DELETE_CLOCK(0,true);
                //VM.FUNCTION_SACLOCK_CREATE_CLOCK(VM.BIND_LETOVYCAS_PREP_MAX - VM.BIND_LETOVYCAS_PREP, 50, true);
                //VM.FUNCTION_SACLOCK_DELETE_CLOCK(1,true);
                //VM.FUNCTION_SACLOCK_CREATE_SMALLCLOCK_RTC(true);
                //VM.preptime_clock_id = 0;
                //FUNCTION_SACLOCK_STOPCOUNT(0);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(0, BIND_LETOVYCAS_PREP_MAX - BIND_LETOVYCAS_PREP, false);
                FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(18, 0, false);

                //FUNCTION_SACLOCK_STOPCOUNT(1);
                var datet = DateTime.Now;
                int hodiny = datet.Hour * 60 * 60;
                int minuty = datet.Minute * 60;
                int sum = hodiny + minuty;
                FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(136, 1, false);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(1, sum, false);



            }
            else
            {
                FUNCTION_SACLOCK_STOPCOUNT(0);
            }


            BIND_MAINTIME_ISRUNNING = false;
            BIND_MAINTIME_ISSTOPED = true;

            BIND_FINAL_MAINTIME_ISRUNNING = false;
            BIND_FINAL_MAINTIME_ISSTOPED = true;
            BIND_FINAL_PREPTIME_ISRUNNING = false;
            BIND_FINAL_PREPTIME_ISSTOPED = true;

            FUNCTION_CLOCK_SET_CLOCK_MODE();

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
            
            if (BIND_MAINTIME_ISRUNNING is true)
            {
                //smazu male RTC
                //FUNCTION_SACLOCK_DELETE_CLOCK(1,false);
                // a vytvorim maly pripravny cas 
                //FUNCTION_SACLOCK_CREATE_SMALLCLOCK(BIND_LETOVYCAS_PREP_MAX, 50,false);
                FUNCTION_SACLOCK_STOPCOUNT(1);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(1, BIND_LETOVYCAS_PREP_MAX, false);
                FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(50, 1, false);
                preptime_clock_id = 1;
            }

            if (BIND_MAINTIME_ISRUNNING is false)
            {
                //smazu velke hodiny 
                //FUNCTION_SACLOCK_DELETE_CLOCK(0,false);
                // smazu male RTC
                //FUNCTION_SACLOCK_DELETE_CLOCK(1, false);
                FUNCTION_SACLOCK_STOPCOUNT(0);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(0, BIND_LETOVYCAS_PREP_MAX, false);
                FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(18, 0, false);





                var datet = DateTime.Now;
                int hodiny = datet.Hour * 60 * 60;
                int minuty = datet.Minute * 60;
                int sum = hodiny + minuty;
                FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(136, 1, false);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(1, sum, false);



                //vytvorim velke hodiny s preptime
                //FUNCTION_SACLOCK_CREATE_CLOCK(BIND_LETOVYCAS_PREP_MAX, 50,false);
                //vytvorim znova male RTC
                //FUNCTION_SACLOCK_CREATE_SMALLCLOCK_RTC(false);
                // a reknu, ze velke hodiny bezi na ID0
                //preptime_clock_id = 0;
            }



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
            //zastavim preptime clock at uz bezi kdekoliv

            if (BIND_MAINTIME_ISRUNNING is true)
            {
                //FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(50, 0, false);
                //FUNCTION_SACLOCK_SETTIMETO_CLOCK(0, BIND_LETOVYCAS_PREP_MAX, false);

                FUNCTION_SACLOCK_STOPCOUNT(1);
                var datet = DateTime.Now;
                int hodiny = datet.Hour * 60 * 60;
                int minuty = datet.Minute * 60;
                int sum = hodiny + minuty;
                FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(136, 1, false); ;
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(1, sum, false);

            }

            if (BIND_MAINTIME_ISRUNNING is false)
            {
                //FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(50, 0, false);
                //FUNCTION_SACLOCK_SETTIMETO_CLOCK(0, BIND_LETOVYCAS_PREP_MAX, false);

                FUNCTION_SACLOCK_STOPCOUNT(0);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(0, 0, false);
                var datet = DateTime.Now;
                int hodiny = datet.Hour * 60 * 60;
                int minuty = datet.Minute * 60;
                int sum = hodiny + minuty;
                FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(136, 1, false);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(1, sum, false);

            }




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

            if (BIND_FINAL_MAINTIME_ISRUNNING is true)
            {
                //smazu male RTC
                //FUNCTION_SACLOCK_DELETE_CLOCK(1,false);
                // a vytvorim maly pripravny cas 
                //FUNCTION_SACLOCK_CREATE_SMALLCLOCK(BIND_LETOVYCAS_PREP_MAX, 50,false);
                FUNCTION_SACLOCK_STOPCOUNT(1);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(1, BIND_FINAL_LETOVYCAS_PREP_MAX, false);
                FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(50, 1, false);
                preptime_clock_id = 1;
            }

            if (BIND_FINAL_MAINTIME_ISRUNNING is false)
            {
                //smazu velke hodiny 
                //FUNCTION_SACLOCK_DELETE_CLOCK(0,false);
                // smazu male RTC
                //FUNCTION_SACLOCK_DELETE_CLOCK(1, false);
                FUNCTION_SACLOCK_STOPCOUNT(0);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(0, BIND_FINAL_LETOVYCAS_PREP_MAX, false);
                FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(18, 0, false);


                var datet = DateTime.Now;
                int hodiny = datet.Hour * 60 * 60;
                int minuty = datet.Minute * 60;
                int sum = hodiny + minuty;
                FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(136, 1, false);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(1, sum, false);

                //vytvorim velke hodiny s preptime
                //FUNCTION_SACLOCK_CREATE_CLOCK(BIND_LETOVYCAS_PREP_MAX, 50,false);
                //vytvorim znova male RTC
                //FUNCTION_SACLOCK_CREATE_SMALLCLOCK_RTC(false);
                // a reknu, ze velke hodiny bezi na ID0
                //preptime_clock_id = 0;
            }
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

            if (BIND_FINAL_MAINTIME_ISRUNNING is true)
            {
                //FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(50, 0, false);
                //FUNCTION_SACLOCK_SETTIMETO_CLOCK(0, BIND_LETOVYCAS_PREP_MAX, false);

                FUNCTION_SACLOCK_STOPCOUNT(1);
                var datet = DateTime.Now;
                int hodiny = datet.Hour * 60 * 60;
                int minuty = datet.Minute * 60;
                int sum = hodiny + minuty;
                FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(136, 1, false);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(1, sum, false);

            }

            if (BIND_FINAL_MAINTIME_ISRUNNING is false)
            {
                //FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(50, 0, false);
                //FUNCTION_SACLOCK_SETTIMETO_CLOCK(0, BIND_LETOVYCAS_PREP_MAX, false);

                FUNCTION_SACLOCK_STOPCOUNT(0);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(0, 0, false);
                var datet = DateTime.Now;
                int hodiny = datet.Hour * 60 * 60;
                int minuty = datet.Minute * 60;
                int sum = hodiny + minuty;
                FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(136, 1, false);
                FUNCTION_SACLOCK_SETTIMETO_CLOCK(1, sum, false);

            }
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
                if (File.Exists("Audio\\NAMES\\" + Players_Actual_Flying[x].ID + ".wav"))
                {
                    fileContent = File.ReadAllBytes("Audio\\NAMES\\" + Players_Actual_Flying[x].ID + ".wav");
                }
                else
                {
                    fileContent = File.ReadAllBytes("Audio\\" + BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + Players_Actual_Flying[x].ID + ".wav");

                }
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
                Console.WriteLine(Players_Actual_Flying[x].ID);
                    int i = x+1;
                    if (File.Exists("Audio\\NAMES\\" + users_id_for_sound[x] + ".wav"))
                    {
                        fileContent = File.ReadAllBytes("Audio\\NAMES\\" + users_id_for_sound[x] + ".wav");
                    }
                    else
                    {
                        fileContent = File.ReadAllBytes("Audio\\" + BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + users_id_for_sound[x] + ".wav");

                    }

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
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            Console.WriteLine(BIND_SELECTED_ROUND);
            Console.WriteLine(BIND_SQL_SOUTEZ_ROUNDS);

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
                if (File.Exists("Audio\\NAMES\\" + Players_Actual_Final_Flying[x].ID + ".wav"))
                {
                    fileContent = File.ReadAllBytes("Audio\\NAMES\\" + Players_Actual_Final_Flying[x].ID + ".wav");
                }
                else
                {
                    fileContent = File.ReadAllBytes("Audio\\" + BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + Players_Actual_Final_Flying[x].ID + ".wav");

                }


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

                if (File.Exists("Audio\\NAMES\\" + users_id_for_sound_final[x] + ".wav"))
                {
                    fileContent = File.ReadAllBytes("Audio\\NAMES\\" + users_id_for_sound_final[x] + ".wav");
                }
                else
                {
                    fileContent = File.ReadAllBytes("Audio\\" + BINDING_SoundList_languages[BINDING_SoundList_languages_index].SoundName + "\\" + users_id_for_sound_final[x] + ".wav");

                }


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
                letovycas = Lang.flight_time_will_start_in+ ": " + rozdil.ToString("mm':'ss':'ff");
            }
            else
            {
                TimeSpan time_remaining = TimeSpan.FromSeconds(MODEL_CONTEST_RULES[0].BASEROUNDMAXTIME);
                TimeSpan totalsec = TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds);
                TimeSpan rozdil2 = time_remaining.Subtract(totalsec);

                letovycas = Lang.flight_time +": " + elapsed.ToString("mm':'ss':'ff") + " ("+ Lang.remaining+" : " + rozdil2.ToString("mm':'ss':'ff") + ")";
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



        public void FUNCTION_LOAD_CONTESTS_ONLINE(string category, string language)
        {


            MODEL_CONTESTS_ONLINE.Clear();
            Console.WriteLine("Searching online contests for category" + category);

            string[] mArrayOfcontests = new string[300];


            string remoteUrl = "http://api.sorgair.com/api_listofcontests.php?cat="+category+"&lang="+language;
            Console.WriteLine(remoteUrl);
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
                        DATE = soutez.Split(spearator_sub, 100, StringSplitOptions.RemoveEmptyEntries)[1],
                        COMPETITORS = soutez.Split(spearator_sub, 100, StringSplitOptions.RemoveEmptyEntries)[4],
                        SMCRID = soutez.Split(spearator_sub, 100, StringSplitOptions.RemoveEmptyEntries)[5],
                        COUNTRY = soutez.Split(spearator_sub, 100, StringSplitOptions.RemoveEmptyEntries)[6],

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
        public ObservableCollection<MODEL_Player_selected> Players_Actual_SelectedRoundandGroup { get; set; } = new ObservableCollection<MODEL_Player_selected>();
        public ObservableCollection<MODEL_Player_selected> Player_Selected { get; set; } = new ObservableCollection<MODEL_Player_selected>();
        public ObservableCollection<MODEL_Player_selected> Player_Selected_Roundlist { get; set; } = new ObservableCollection<MODEL_Player_selected>();
        public ObservableCollection<MODEL_Player_baseresults> Players_Baseresults { get; set; } = new ObservableCollection<MODEL_Player_baseresults>();
        public ObservableCollection<MODEL_Player_baseresults> PRINT_Players_Baseresults { get; set; } = new ObservableCollection<MODEL_Player_baseresults>();
        public ObservableCollection<MODEL_Player_baseresults_complete> Players_Baseresults_Complete { get; set; } = new ObservableCollection<MODEL_Player_baseresults_complete>();
        public ObservableCollection<MODEL_Player_baseresults> Players_Finalresults { get; set; } = new ObservableCollection<MODEL_Player_baseresults>();

        public ObservableCollection<MODEL_Player_statistics> Players_statistics { get; set; } = new ObservableCollection<MODEL_Player_statistics>();

        public void FUNCTION_USERS_LOAD_ALLCOMPETITORS()
        {
            Players.Clear();
            SQL_READSOUTEZDATA("select ID, Firstname, Lastname, Country,(select name from Agecategories A  where A.id=U.Agecat) Agecat, (select name from Frequencies F  where F.id=U.Freq) Freq, Ch1, Ch2, Failic, Naclic, Club, Paid, Team, (select name from Agecategories A  where A.id=U.Customagecat) Customagecat, U.Freq Freqid, U.Agecat agecatid, U.customagecat customagecatid  from users U where id > 0; ", "get_players");
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




        public bool bind_isnondeletable
        {
            get { return _bind_isnondeletable; }
            set { _bind_isnondeletable = value; OnPropertyChanged(nameof(bind_isnondeletable)); }
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

            //if (rnd == 0) { rnd = BIND_SCOREENTRY_SELECTEDROUND_ROUND; }
            //if (grp == 0) { grp = BIND_SCOREENTRY_SELECTEDROUND_GROUP; }
            //if (stp == 0) { stp = BIND_SCOREENTRY_SELECTEDROUND_STARTPOINT; }


            bind_isnondeletable = bool.Parse(SQL_READSOUTEZDATA("SELECT nondeletable from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));
            System.Threading.Thread.Sleep(100);

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

            if (rnd == 0) { rnd = BIND_SCOREENTRY_ROUND_ROUND; }
            if (grp == 0) { grp = BIND_SCOREENTRY_ROUND_GROUP; }
            if (stp == 0) { stp = BIND_SCOREENTRY_ROUND_STARTPOINT; }

            SQL_READSOUTEZDATA("select U.ID,M.stp,U.Firstname,U.Lastname from matrix M left join users U on M.user = U.id where M.rnd = " + rnd + " and M.grp = " + grp + " and M.stp = " + stp + " order by stp asc;", "get_Player_Selected_Roundlist");
            bind_scoreentry_fromroundlist_selected_minute = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(minutes) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select minutes from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));
            bind_scoreentry_fromroundlist_selected_second = int.Parse(SQL_READSOUTEZDATA("SELECT CASE WHEN (select count(seconds) from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") =0  THEN -1 ELSE (select seconds from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp + ") END FROM score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));
            bind_isnondeletable = bool.Parse(SQL_READSOUTEZDATA("SELECT nondeletable from score where rnd = " + rnd + " and grp = " + grp + " and stp = " + stp, ""));
            Console.WriteLine("bind_isnondeletable" + bind_isnondeletable);

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

        public void FUNCTION_SCOREENTRY_SAVE_SCORE(int rnd, int grp, int stp, int usrid, int minutes, int seconds, int landing, int height, int pen1value, int pen1id, int pen2value, int pen2id, string rawscore, string prepscore, bool nondeletable, bool insertoninternet)
        {
            Console.WriteLine("saving score");
            SQL_SAVESOUTEZDATA("delete from score where rnd=" + rnd + " and grp=" + grp + " and stp=" + stp + ";");
            SQL_SAVESOUTEZDATA("insert INTO score (rnd, grp, stp, userid, minutes, seconds, landing, height, pen1value, pen1id, pen2value, pen2id, raw, prep, entered, nondeletable) VALUES(" + rnd + "," + grp + "," + stp + "," + usrid + "," + minutes + "," + seconds + "," + landing + "," + height + ", " + pen1value + ", " + pen1id + "," + pen2value + ", " + pen2id + ",'" + rawscore + "','" + prepscore + "', 'True','" + nondeletable + "');");
            if (insertoninternet == true)
            {
                online_savescore(rnd, grp, stp, usrid, minutes, seconds, landing, height, pen1value, pen1id, pen2value, pen2id, rawscore, prepscore, nondeletable);
            }
            Decimal _MAXRAW = Decimal.Parse(SQL_READSOUTEZDATA("select max(raw) FROM score s where s.rnd = " + rnd + " and s.grp = " + grp, ""));
            Decimal _PREPSCORE = 0;


            if (rnd >= 100)
            {
                for (int i = 1; i < BIND_SQL_SOUTEZ_STARTPOINTSFINALE + 1; i++)
                {
                    Console.WriteLine(SQL_READSOUTEZDATA("select raw FROM score s where s.rnd = " + rnd + " and s.grp = " + grp + " and s.stp = " + i, ""));

                    decimal _RAW = Decimal.Parse(SQL_READSOUTEZDATA("select raw FROM score s where s.rnd = " + rnd + " and s.grp = " + grp + " and s.stp = " + i, ""));
                    if (SQL_READSOUTEZDATA("select RECOUNTSCORETO1000 from rules", "") == "False")
                    {
                        _PREPSCORE = Math.Round(_RAW, 2);

                    }
                    else
                    {
                        if (_MAXRAW != 0)
                        {
                            _PREPSCORE = Math.Round((_RAW / _MAXRAW) * 1000, 2);
                        }

                    }
                    Console.WriteLine("_PREPSCORE = " + _PREPSCORE);
                    string _PREPSCORE_STR = _PREPSCORE.ToString(new CultureInfo("en-US"));
                    SQL_SAVESOUTEZDATA("update score set prep = " + _PREPSCORE_STR + " where rnd = " + rnd + " and grp = " + grp + " and stp = " + i);
                    if (insertoninternet == true) 
                    { 
                        online_updateprepscore(rnd, grp, i, _PREPSCORE); 
                    }

                }
            }
            else
            {
                for (int i = 1; i < BIND_SQL_SOUTEZ_STARTPOINTS + 1; i++)
                {
                    Console.WriteLine(SQL_READSOUTEZDATA("select raw FROM score s where s.rnd = " + rnd + " and s.grp = " + grp + " and s.stp = " + i, ""));

                    decimal _RAW = Decimal.Parse(SQL_READSOUTEZDATA("select raw FROM score s where s.rnd = " + rnd + " and s.grp = " + grp + " and s.stp = " + i, ""));
                    if (SQL_READSOUTEZDATA("select RECOUNTSCORETO1000 from rules", "") == "False")
                    {
                        _PREPSCORE = Math.Round(_RAW, 2);

                    }
                    else
                    {
                        if (_MAXRAW != 0)
                        {
                            _PREPSCORE = Math.Round((_RAW / _MAXRAW) * 1000, 2);
                        }

                    }
                    Console.WriteLine("_PREPSCORE = " + _PREPSCORE);
                    string _PREPSCORE_STR = _PREPSCORE.ToString(new CultureInfo("en-US"));
                    SQL_SAVESOUTEZDATA("update score set prep = " + _PREPSCORE_STR + " where rnd = " + rnd + " and grp = " + grp + " and stp = " + i);
                    if (insertoninternet == true)
                    {
                        online_updateprepscore(rnd, grp, i, _PREPSCORE); 
                    }

                }
            }







            FUNCTION_CHECK_REFLY(rnd, grp);
            Console.WriteLine("_PREPSCORE = " + _PREPSCORE);
            Console.WriteLine("_PREPSCORE_ONLINE = " + _PREPSCORE);

        }




        public void online_savescore(int rnd, int grp, int stp, int usrid, int minutes, int seconds, int landing, int height, int pen1value, int pen1id, int pen2value, int pen2id, string rawscore, string prepscore, bool nondeletable)
        {

            if (CONTENT_ONLINE_ENABLED is true & BINDING_IS_INTERNET is true)
            {
                //SQL_SAVESOUTEZDATA("delete from score where rnd=" + rnd + " and grp=" + grp + " and stp=" + stp + ";");
                //SQL_SAVESOUTEZDATA("insert INTO score (rnd, grp, stp, userid, minutes, seconds, landing, height, pen1value, pen1id, pen2value, pen2id, raw, prep, entered, nondeletable) VALUES(" + rnd + "," + grp + "," + stp + "," + usrid + "," + minutes + "," + seconds + "," + landing + "," + height + ", " + pen1value + ", " + pen1id + "," + pen2value + ", " + pen2id + ",'" + rawscore + "','" + prepscore + "', 'True','" + nondeletable + "');");

                MyParameters4 parameters = new MyParameters4
                {
                    rnd = rnd,
                    grp = grp,
                    stp = stp,
                    usrid = usrid,
                    minutes = minutes,
                    seconds = seconds,
                    landing = landing,
                    height = height,
                    pen1value = pen1value,
                    pen1id = pen1id,
                    pen2value = pen2value,
                    pen2id = pen2id,
                    rawscore = rawscore,
                    prepscore = prepscore,
                    nondeletable = nondeletable
                };
                Thread t_savescore = new Thread(new ParameterizedThreadStart(thread_savescore));
                t_savescore.Start(parameters);

            }

        }


        public async void thread_savescore(object parameter)
        {

            if (parameter is MyParameters4)
            {
                MyParameters4 parameters = (MyParameters4)parameter;

                int rnd = parameters.rnd;
                int grp = parameters.grp;
                int stp = parameters.stp;
                int usrid = parameters.usrid;
                int minutes = parameters.minutes;
                int seconds = parameters.seconds;
                int landing = parameters.landing;
                int height = parameters.height;
                int pen1value = parameters.pen1value;
                int pen1id = parameters.pen1id;
                int pen2value = parameters.pen2value;
                int pen2id = parameters.pen2id;
                string rawscore = parameters.rawscore;
                string prepscore = parameters.prepscore;
                bool nondeletable = parameters.nondeletable;

                string remoteUrl = "http://api.sorgair.com/api_online_results.php?action=insertscore&noveonlineidsouteze=" + CONTENT_MASTER_ID +
               "&master_contest_sorgairidentifikator=" + CONTENT_RANDOM_ID +
               "&rnd=" + rnd +
               "&grp=" + grp +
               "&stp=" + stp +
                "&insorgid=" + usrid +
               "&min=" + minutes +
               "&sec=" + seconds +
               "&landing=" + landing +
               "&height=" + height +
               "&pen1value=" + pen1value +
               "&pen1id=" + pen1id +
               "&pen2value=" + pen2value +
               "&pen2id=" + pen2id +
               "&raw=" + rawscore +
               "&prep=" + prepscore +
               "&entered=1" +
               "&nondeletable=" + nondeletable +
               "&skrtaci=0"
               ;
                Console.WriteLine(remoteUrl);

                int maxRetries = 3;
                int retryDelayMs = 1000;

                for (int attempt = 1; attempt <= maxRetries; attempt++)
                {
                    try
                    {
                        using (var httpClient = new System.Net.Http.HttpClient())
                        {
                            httpClient.Timeout = TimeSpan.FromSeconds(30);
                            var response = await httpClient.GetStringAsync(remoteUrl);
                            Console.WriteLine("Score sent successfully: " + response);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending score (attempt {attempt}/{maxRetries}): " + ex.Message);

                        if (attempt < maxRetries)
                        {
                            await Task.Delay(retryDelayMs);
                            retryDelayMs *= 2;
                        }
                        else
                        {
                            Console.WriteLine("Failed to send score after " + maxRetries + " attempts. Data: " + remoteUrl);
                        }
                    }
                }
            }







        }



        public void online_updateprepscore(int rnd, int grp, int stp, decimal prepscore)
        {

            if (CONTENT_ONLINE_ENABLED is true & BINDING_IS_INTERNET is true)
            {


                //SQL_SAVESOUTEZDATA("delete from score where rnd=" + rnd + " and grp=" + grp + " and stp=" + stp + ";");
                //SQL_SAVESOUTEZDATA("insert INTO score (rnd, grp, stp, userid, minutes, seconds, landing, height, pen1value, pen1id, pen2value, pen2id, raw, prep, entered, nondeletable) VALUES(" + rnd + "," + grp + "," + stp + "," + usrid + "," + minutes + "," + seconds + "," + landing + "," + height + ", " + pen1value + ", " + pen1id + "," + pen2value + ", " + pen2id + ",'" + rawscore + "','" + prepscore + "', 'True','" + nondeletable + "');");

                MyParameters2 parameters = new MyParameters2
                {
                    rnd = rnd,
                    grp = grp,
                    stp = stp,
                    prepscore = prepscore
                };
                Thread t_updateprepscore = new Thread(new ParameterizedThreadStart(thread_updateprepscore));
                t_updateprepscore.Start(parameters);

            }


        }




        public async void thread_updateprepscore(object parameter)
        {

            if (parameter is MyParameters2)
            {
                MyParameters2 parameters = (MyParameters2)parameter;
                int rnd = parameters.rnd;
                int grp = parameters.grp;
                int stp = parameters.stp;
                decimal prepscore = parameters.prepscore;

                string remoteUrl = "http://api.sorgair.com/api_online_results.php?action=updateprepscore&noveonlineidsouteze=" + CONTENT_MASTER_ID +
               "&master_contest_sorgairidentifikator=" + CONTENT_RANDOM_ID +
               "&rnd=" + rnd +
               "&grp=" + grp +
               "&stp=" + stp +
               "&prep=" + prepscore
               ;
                Console.WriteLine(remoteUrl);

                int maxRetries = 3;
                int retryDelayMs = 1000;

                for (int attempt = 1; attempt <= maxRetries; attempt++)
                {
                    try
                    {
                        using (var httpClient = new System.Net.Http.HttpClient())
                        {
                            httpClient.Timeout = TimeSpan.FromSeconds(30);
                            var response = await httpClient.GetStringAsync(remoteUrl);
                            Console.WriteLine("Prepscore updated successfully: " + response);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating prepscore (attempt {attempt}/{maxRetries}): " + ex.Message);

                        if (attempt < maxRetries)
                        {
                            await Task.Delay(retryDelayMs);
                            retryDelayMs *= 2;
                        }
                        else
                        {
                            Console.WriteLine("Failed to update prepscore after " + maxRetries + " attempts.");
                        }
                    }
                }
            }







        }




        public void online_updateskrtaci(int rnd, int grp, int userid, int skrtaci)
        {

            if (CONTENT_ONLINE_ENABLED is true & BINDING_IS_INTERNET is true)
            {

                MyParameters parameters = new MyParameters
                {
                    rnd = rnd,
                    grp = grp,
                    stp = userid,
                    refly = skrtaci
                };
                Thread t_updateskrtaci = new Thread(new ParameterizedThreadStart(thread_updateskrtaci));
                t_updateskrtaci.Start(parameters);

                //SQL_SAVESOUTEZDATA("delete from score where rnd=" + rnd + " and grp=" + grp + " and stp=" + stp + ";");
                //SQL_SAVESOUTEZDATA("insert INTO score (rnd, grp, stp, userid, minutes, seconds, landing, height, pen1value, pen1id, pen2value, pen2id, raw, prep, entered, nondeletable) VALUES(" + rnd + "," + grp + "," + stp + "," + usrid + "," + minutes + "," + seconds + "," + landing + "," + height + ", " + pen1value + ", " + pen1id + "," + pen2value + ", " + pen2id + ",'" + rawscore + "','" + prepscore + "', 'True','" + nondeletable + "');");



            }


        }
       


        public async void thread_updateskrtaci(object parameter)
        {

            if (parameter is MyParameters)
            {
                MyParameters parameters = (MyParameters)parameter;
                int rnd = parameters.rnd;
                int grp = parameters.grp;
                int userid = parameters.stp;
                int skrtaci = parameters.refly;

                string remoteUrl = "http://api.sorgair.com/api_online_results.php?action=updateskrtaci&noveonlineidsouteze=" + CONTENT_MASTER_ID +
                   "&master_contest_sorgairidentifikator=" + CONTENT_RANDOM_ID +
                   "&rnd=" + rnd +
                   "&grp=" + grp +
                   "&insorgid=" + userid +
                   "&skrtaci=" + skrtaci
                   ;
                Console.WriteLine(remoteUrl);

                int maxRetries = 3;
                int retryDelayMs = 1000;

                for (int attempt = 1; attempt <= maxRetries; attempt++)
                {
                    try
                    {
                        using (var httpClient = new System.Net.Http.HttpClient())
                        {
                            httpClient.Timeout = TimeSpan.FromSeconds(20);
                            var response = await httpClient.GetStringAsync(remoteUrl);
                            Console.WriteLine("Skrtaci updated successfully: " + response);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating skrtaci (attempt {attempt}/{maxRetries}): " + ex.Message);

                        if (attempt < maxRetries)
                        {
                            await Task.Delay(retryDelayMs);
                            retryDelayMs *= 2;
                        }
                        else
                        {
                            Console.WriteLine("Failed to update skrtaci after " + maxRetries + " attempts.");
                        }
                    }
                }
            }




        }


        public async void thread_updateskrtaciall(object parameter)
        {

            if (parameter is MyParameters3)
            {
                MyParameters3 parameters = (MyParameters3)parameter;
                int idsouteze = parameters.idsouteze;
                int skrtaci = parameters.skrtaci;

                string remoteUrl = "http://api.sorgair.com/api_online_results.php?action=updateskrtaci_all&noveonlineidsouteze=" + idsouteze +
                   "&master_contest_sorgairidentifikator=" + idsouteze +
                   "&skrtaci=" + skrtaci
                   ;
                Console.WriteLine(remoteUrl);

                int maxRetries = 3;
                int retryDelayMs = 1000;

                for (int attempt = 1; attempt <= maxRetries; attempt++)
                {
                    try
                    {
                        using (var httpClient = new System.Net.Http.HttpClient())
                        {
                            httpClient.Timeout = TimeSpan.FromSeconds(20);
                            var response = await httpClient.GetStringAsync(remoteUrl);
                            Console.WriteLine("Skrtaci all updated successfully: " + response);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating skrtaci all (attempt {attempt}/{maxRetries}): " + ex.Message);

                        if (attempt < maxRetries)
                        {
                            await Task.Delay(retryDelayMs);
                            retryDelayMs *= 2;
                        }
                        else
                        {
                            Console.WriteLine("Failed to update skrtaci all after " + maxRetries + " attempts.");
                        }
                    }
                }
            }




        }



        public async void thread_updaterefly(object parameter)
        {

            if (parameter is MyParameters)
            {
                MyParameters parameters = (MyParameters)parameter;
                int rnd = parameters.rnd;
                int grp = parameters.grp;
                int stp = parameters.stp;
                int refly = parameters.refly;

                string remoteUrl = "http://api.sorgair.com/api_online_results.php?action=updaterefly&noveonlineidsouteze=" + CONTENT_MASTER_ID +
                   "&master_contest_sorgairidentifikator=" + CONTENT_RANDOM_ID +
                   "&rnd=" + rnd +
                   "&grp=" + grp +
                   "&stp=" + stp +
                   "&refly=" + refly
                   ;
                Console.WriteLine(remoteUrl);

                int maxRetries = 3;
                int retryDelayMs = 1000;

                for (int attempt = 1; attempt <= maxRetries; attempt++)
                {
                    try
                    {
                        using (var httpClient = new System.Net.Http.HttpClient())
                        {
                            httpClient.Timeout = TimeSpan.FromSeconds(20);
                            var response = await httpClient.GetStringAsync(remoteUrl);
                            Console.WriteLine("Refly updated successfully: " + response);

                            Console.WriteLine($"Content Master ID: {CONTENT_MASTER_ID}");
                            Console.WriteLine($"Content Random ID: {CONTENT_RANDOM_ID}");
                            Console.WriteLine($"Rnd: {rnd}");
                            Console.WriteLine($"Grp: {grp}");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating refly (attempt {attempt}/{maxRetries}): " + ex.Message);

                        if (attempt < maxRetries)
                        {
                            await Task.Delay(retryDelayMs);
                            retryDelayMs *= 2;
                        }
                        else
                        {
                            Console.WriteLine("Failed to update refly after " + maxRetries + " attempts.");
                        }
                    }
                }
            }







        }



        public void online_updaterefly(int rnd, int grp, int stp, int refly)
        {

            if (CONTENT_ONLINE_ENABLED is true & BINDING_IS_INTERNET is true)
            {

                MyParameters parameters = new MyParameters
                {
                    rnd = rnd,
                    grp = grp,
                    stp = stp,
                    refly = refly
                };
                Thread t_updaterefly = new Thread(new ParameterizedThreadStart(thread_updaterefly));
                t_updaterefly.Start(parameters);



                //SQL_SAVESOUTEZDATA("delete from score where rnd=" + rnd + " and grp=" + grp + " and stp=" + stp + ";");
                //SQL_SAVESOUTEZDATA("insert INTO score (rnd, grp, stp, userid, minutes, seconds, landing, height, pen1value, pen1id, pen2value, pen2id, raw, prep, entered, nondeletable) VALUES(" + rnd + "," + grp + "," + stp + "," + usrid + "," + minutes + "," + seconds + "," + landing + "," + height + ", " + pen1value + ", " + pen1id + "," + pen2value + ", " + pen2id + ",'" + rawscore + "','" + prepscore + "', 'True','" + nondeletable + "');");



            }


        }



        public void online_updateskrtaci_all(int skrtaci)
        {

            if (CONTENT_ONLINE_ENABLED is true & BINDING_IS_INTERNET is true)
            {





                //SQL_SAVESOUTEZDATA("delete from score where rnd=" + rnd + " and grp=" + grp + " and stp=" + stp + ";");
                //SQL_SAVESOUTEZDATA("insert INTO score (rnd, grp, stp, userid, minutes, seconds, landing, height, pen1value, pen1id, pen2value, pen2id, raw, prep, entered, nondeletable) VALUES(" + rnd + "," + grp + "," + stp + "," + usrid + "," + minutes + "," + seconds + "," + landing + "," + height + ", " + pen1value + ", " + pen1id + "," + pen2value + ", " + pen2id + ",'" + rawscore + "','" + prepscore + "', 'True','" + nondeletable + "');");

                MyParameters3 parameters = new MyParameters3
                {
                    idsouteze = CONTENT_MASTER_ID,
                    skrtaci = skrtaci
                };

                Thread t_updateskrtaciall = new Thread(new ParameterizedThreadStart(thread_updateskrtaciall));
                t_updateskrtaciall.Start(parameters);



            }


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
//            MessageBox.Show(rnd.ToString()  + " - " + vysledek.ToString());
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
                        online_updaterefly(from_rnd, from_grp, from_stp, 0);
                        online_updaterefly(to_rnd, to_grp, to_stp, 1);

                    }
                    else
                    {
                        Console.WriteLine("zapisuji u score1 at se nepocita, a score 2 at se pocita");
                        SQL_SAVESOUTEZDATA("update score set refly='True' where rnd=" + from_rnd + " and grp=" + from_grp + " and stp=" + from_stp);
                        SQL_SAVESOUTEZDATA("update score set refly='False' where rnd=" + to_rnd + " and grp=" + to_grp + " and stp=" + to_stp);
                        online_updaterefly(from_rnd, from_grp, from_stp, 1);
                        online_updaterefly(to_rnd, to_grp, to_stp, 0);

                    }



                }
                else
                {
                    Console.WriteLine("Neřešit který výsledek je lepší. Počítá se prostě ten druhý");
                    SQL_SAVESOUTEZDATA("update score set refly='True' where rnd=" + from_rnd + " and grp=" + from_grp + " and stp=" + from_stp);
                    SQL_SAVESOUTEZDATA("update score set refly='False' where rnd=" + to_rnd + " and grp=" + to_grp + " and stp=" + to_stp);
                    online_updaterefly(from_rnd, from_grp, from_stp, 1);
                    online_updaterefly(to_rnd, to_grp, to_stp, 0);

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
                    SQL_SAVESOUTEZDATA("update score set refly='False' where rnd=" + to_rnd + " and grp=" + to_grp + " and stp=" + to_stp);
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





        public void FUNCTION_USERS_CREATE_NEW(string firstname, string lastname, string country, int agecat, int freq, int chanel1, int chanel2, string failic, string naclic , string club, bool registered, int team, int customagecat )
        {

            SQL_SAVESOUTEZDATA("insert into users values (null,'"+ firstname + "', '" + lastname  + "', '" + country  + "', '" + agecat  + "', '" + freq  + "', '" + chanel1  + "', '" + chanel2 + "' , '" + failic + "', '" + naclic + "', '" + club + "' , '" + registered + "', '" + team + "', '" + customagecat + "' , 0 );");
            Players.Clear();
            //SQL_READSOUTEZDATA("select ID, Firstname, Lastname, Country,(select name from Agecategories A  where A.id=U.Agecat) Agecat, (select name from Frequencies F  where F.id=U.Freq) Freq, Ch1, Ch2, Failic, Naclic, Club, Paid, Team, Customagecat, U.Freq Freqid, U.Agecat agecatid from users U where id > 0; ", "get_players");
            SQL_READSOUTEZDATA("select ID, Firstname, Lastname, Country,(select name from Agecategories A  where A.id=U.Agecat) Agecat, (select name from Frequencies F  where F.id=U.Freq) Freq, Ch1, Ch2, Failic, Naclic, Club, Paid, Team, (select name from Agecategories A  where A.id=U.Customagecat) Customagecat, U.Freq Freqid, U.Agecat agecatid, U.customagecat customagecatid  from users U where id > 0; ", "get_players");
            BIND_POCETSOUTEZICICHMENU = SQL_READSOUTEZDATA("select count(id) pocet from users where id > 0", "");
            BIND_POCETSOUTEZICICH = Int32.Parse(SQL_READSOUTEZDATA("select count(id) pocet from users where id > 0", ""));

        }

        public void FUNCTION_USERS_CREATE_EDIT(int ID, string firstname, string lastname, string country, int agecat, int freq, int chanel1, int chanel2, string failic, string naclic, string club, bool paid, int customagecat)
        {

            SQL_SAVESOUTEZDATA("update users set Firstname='" + firstname + "', Lastname='" + lastname + "', Country='" + country + "', Agecat='" + agecat + "', Freq='" + freq + "', Ch1='" + chanel1 + "', Ch2='" + chanel2 + "' , Failic='" + failic + "', Naclic='" + naclic + "', Club='" + club + "' , Customagecat='" + customagecat + "' , paid='"+paid+"' where ID="+ID+" ;");
            Players.Clear();
            //SQL_READSOUTEZDATA("select ID, Firstname, Lastname, Country,(select name from Agecategories A  where A.id=U.Agecat) Agecat, (select name from Frequencies F  where F.id=U.Freq) Freq, Ch1, Ch2, Failic, Naclic, Club, Paid, Team, Customagecat, U.Freq Freqid, U.Agecat agecatid from users U where id > 0; ", "get_players");
            SQL_READSOUTEZDATA("select ID, Firstname, Lastname, Country,(select name from Agecategories A  where A.id=U.Agecat) Agecat, (select name from Frequencies F  where F.id=U.Freq) Freq, Ch1, Ch2, Failic, Naclic, Club, Paid, Team, (select name from Agecategories A  where A.id=U.Customagecat) Customagecat, U.Freq Freqid, U.Agecat agecatid, U.customagecat customagecatid  from users U where id > 0; ", "get_players");
            BIND_POCETSOUTEZICICHMENU = SQL_READSOUTEZDATA("select count(id) pocet from users where id > 0", "");
            BIND_POCETSOUTEZICICH = Int32.Parse(SQL_READSOUTEZDATA("select count(id) pocet from users where id > 0", ""));

        }


        public void FUNCTION_USERS_DELETE_COMPETITOR(int idsouteziciho)
        {
            SQL_READSOUTEZDATA("delete from users where id="+idsouteziciho +"", "");
            SQL_READSOUTEZDATA("update matrix set user=0 where user=" + idsouteziciho + "", "");
            SQL_READSOUTEZDATA("delete from score where userid=" + idsouteziciho + "", "");
            Players.Clear();
            //SQL_READSOUTEZDATA("select ID, Firstname, Lastname, Country,(select name from Agecategories A  where A.id=U.Agecat) Agecat, (select name from Frequencies F  where F.id=U.Freq) Freq, Ch1, Ch2, Failic, Naclic, Club, Paid, Team, Customagecat, U.Freq Freqid, U.Agecat agecatid from users U where id > 0; ", "get_players");
            SQL_READSOUTEZDATA("select ID, Firstname, Lastname, Country,(select name from Agecategories A  where A.id=U.Agecat) Agecat, (select name from Frequencies F  where F.id=U.Freq) Freq, Ch1, Ch2, Failic, Naclic, Club, Paid, Team, (select name from Agecategories A  where A.id=U.Customagecat) Customagecat, U.Freq Freqid, U.Agecat agecatid, U.customagecat customagecatid  from users U where id > 0; ", "get_players");
            BIND_POCETSOUTEZICICHMENU = SQL_READSOUTEZDATA("select count(id) pocet from users where id > 0", "");
            BIND_POCETSOUTEZICICH = Int32.Parse(SQL_READSOUTEZDATA("select count(id) pocet from users where id > 0", ""));

        }


        public ObservableCollection<MODEL_Contests_categories> MODEL_CONTESTS_CALENDAR_COUNTRY_SOURCES { get; set; } = new ObservableCollection<MODEL_Contests_categories>();

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
        public ObservableCollection<MODEL_Contest_Groups_refly> MODEL_CONTEST_AVAIABLE_REFLYGROUP { get; set; } = new ObservableCollection<MODEL_Contest_Groups_refly>();

        public ObservableCollection<MODEL_Player_flags> MODEL_Contest_FLAGS { get; set; } = new ObservableCollection<MODEL_Player_flags>();

        public ObservableCollection<MODEL_Player_agecategories> MODEL_Contest_AGECATEGORIES { get; set; } = new ObservableCollection<MODEL_Player_agecategories>();
        public ObservableCollection<MODEL_Player_agecategories> MODEL_Contest_CUSTOMAGECATEGORIES { get; set; } = new ObservableCollection<MODEL_Player_agecategories>();
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

        public void FUNCTION_LOAD_CALENDAR_COUNTRY_SOURCES()
        {
            MODEL_CONTESTS_CALENDAR_COUNTRY_SOURCES.Clear();

            var all = new MODEL_Contests_categories()
            {

                ID = 1,
                CATEGORY = "Všechny staty",
                ADRESS = "ALL"
            };


            var cz = new MODEL_Contests_categories()
            {

                ID = 2,
                CATEGORY = "Česko",
                ADRESS = "CZE"
            };


            var sk = new MODEL_Contests_categories()
            {

                ID = 3,
                CATEGORY = "Slovensko",
                ADRESS = "SVK"
            };

            MODEL_CONTESTS_CALENDAR_COUNTRY_SOURCES.Add(all);
            MODEL_CONTESTS_CALENDAR_COUNTRY_SOURCES.Add(cz);
            MODEL_CONTESTS_CALENDAR_COUNTRY_SOURCES.Add(sk);


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

                var groups = new MODEL_Contest_Groups_refly()
                {
                    ID = i,
                    GROUPNAME = Lang.available_positions + ": " + pocetpraznychpozic,
                    GROUPNAME_SRC = "G:" + i,
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




        public void FUNCTION_RESULTS_LOAD_RESULTS(string what ,int to_round, int age_cat)

        {
            string age_cat_str;

            if (age_cat == 99)
            {
                age_cat_str = "0,1,2";
            }
            else
            {
                if (age_cat >= 3)
                {
                    age_cat_str = (age_cat+1).ToString();
                }
                else
                {
                    age_cat_str = (age_cat).ToString();
                }
            }

            if (what == "statistics_enemykiled")
            {
                Players_statistics.Clear();
                SQL_READSOUTEZDATA("select Firstname, Lastname, id userid from users where id > 0 ", "get_statistics_enemykiled");
            }

            if (what == "statistics_flighttime")
            {
                Players_statistics.Clear();
                SQL_READSOUTEZDATA("select time(sum((minutes*60+seconds)), 'unixepoch') totaltime," +
" time(sum(minutes * 60 + seconds) / (select count(rnd) from score where userid = s1.userid  and prep > 0 and rnd <= " + to_round + "), 'unixepoch') averagetime," +
" (select count(rnd) from score where userid = s1.userid  and prep > 0 and rnd <= " + to_round + ") zaznamu," +
" u.Firstname," +
" u.Lastname, u.id userid" +
" from Score s1 left join users U on S1.userid = U.id where s1.userid > 0  and prep > 0 and rnd <= "+ to_round + " group by userid order by totaltime desc", "get_statistics_flighttime",to_round);
            }


            if (what == "statistics_maxheights")
            {
                Players_statistics.Clear();
                SQL_READSOUTEZDATA("select s1.userid," +
" (select count(userid) from score where height > 0  and prep > 0 and userid = s1.userid and rnd <= " + to_round + ") zaznamu," +
" ifnull((select max(s1.height) from Score where height > 0  and prep > 0  and userid = s1.userid and rnd <= " + to_round + "),0) rawmaxheight," +
" ifnull((select sum(s1.height) from Score where height > 0  and prep > 0  and userid = s1.userid and rnd <= " + to_round + "),0) summaxheight," +
" ifnull((select sum(s1.height) from Score where height > 0  and prep > 0  and userid = s1.userid and rnd <= " + to_round + ") / (select count(rnd)from Score where height > 0  and prep > 0  and userid = s1.userid and rnd <= " + to_round + "),0) averagemaxheight," +
" u.Firstname," +
" u.Lastname" +
" from Score s1 left join users U on S1.userid = U.id where userid > 0  and prep > 0 and rnd <= " + to_round + " group by s1.userid order by rawmaxheight DESC", "get_statistics_maxheights", to_round);
            }


            if (what == "statistics_timevsheight")
            {
                Players_statistics.Clear();
                SQL_READSOUTEZDATA("select s1.userid, time(sum((minutes*60+seconds)), 'unixepoch') totaltime," +
" (select count(userid) from score where height > 0  and prep > 0 and userid = s1.userid and rnd <= " + to_round + ") zaznamu," +
" (select sum(minutes * 60 + seconds)) totaltimesec," +
" ifnull((select sum(height) from score where height > 0  and prep > 0  and userid = s1.userid and rnd <= " + to_round + "),0) sumheight," +
" ifnull(time(((select sum(minutes * 60 + seconds) from score where height > 0  and prep > 0 and userid = s1.userid and rnd <= " + to_round + ") / (select count(rnd)from Score where height > 0  and prep > 0  and userid = s1.userid and rnd <= " + to_round + ")),  'unixepoch' ),'99:99:99') prumernycasnakolo," +
" ifnull(CAST(((select sum(height) from score where height > 0  and prep > 0 and userid = s1.userid and rnd <= " + to_round + ") / (select count(rnd)from Score where height > 0  and prep > 0  and userid = s1.userid and rnd <= " + to_round + ")) as REAL),999.99) prumernavyskanakolo," +
" ifnull(round(CAST((select sum(height) from score where height > 0  and prep > 0 and userid = s1.userid and rnd <= " + to_round + ") as REAL) / (CAST((select sum(minutes * 60 + seconds) from score where height > 0  and prep > 0 and userid = s1.userid and rnd <= " + to_round + ") as REAL)) * 600,2),999.99) na10minutjetreba," +
" ifnull(time(round(CAST((select sum(minutes * 60 + seconds) from score where height > 0  and prep > 0 and userid = s1.userid and rnd <= " + to_round + ") / CAST((select sum(height) from score where height > 0  and prep > 0 and userid = s1.userid and rnd <= " + to_round + ") as REAL) as REAL) * 100, 2), 'unixepoch'),'00:00:00') ze100metrunalita," +
" u.Firstname," +
" u.Lastname " +
" from Score s1 left join users U on S1.userid = U.id where userid > 0  and prep > 0 and rnd <= " + to_round + "  group by s1.userid order by na10minutjetreba ASC", "get_statistics_timevsheight");
            }




            if (what == "statistics_minheights")
            {
                Players_statistics.Clear();
                SQL_READSOUTEZDATA("select s1.userid,"+
" (select count(userid) from score where height > 0  and prep > 0 and userid = s1.userid and rnd <= " + to_round + ") zaznamu," +
" ifnull(round(sum(CAST(s1.height as REAL) / (select CAST(count(rnd) as REAL) from Score where height > 0  and prep > 0  and userid = s1.userid and rnd <= " + to_round + ")), 2), 0) rawminheight," +
" ifnull(round(sum(CAST(s1.height as REAL) / (select CAST(count(rnd) as REAL) from Score where height > 0  and prep > 0  and userid = s1.userid and rnd <= " + to_round + ")), 2),0) minheight," +
" (ifnull((select sum(height) from score where height > 0  and prep > 0 and userid = s1.userid and rnd <= " + to_round + "), 0) || ' / ' || ifnull((select sum(prep) from score where height > 0  and prep > 0 and userid = s1.userid and rnd <= " + to_round + "),0)) sumprep," +
" ifnull(round((select sum(prep) from score where height > 0  and prep > 0 and userid = s1.userid and rnd <= " + to_round + ") / (select sum(height) from score where height > 0  and prep > 0 and userid = s1.userid and rnd <= " + to_round + "),2),0) podil," +
" u.Firstname," +
" u.Lastname" +
" from Score s1 left join users U on S1.userid = U.id where userid > 0  and prep > 0  and rnd <= " + to_round + " group by s1.userid order by podil desc ", "get_statistics_minheights",to_round);
            }


            if (what == "statistics_averageheights")
            {
                Players_statistics.Clear();
                SQL_READSOUTEZDATA("select s1.userid,"+
                    " (select count(userid) from score where height > 0 and userid = s1.userid  and prep > 0 and rnd <= " + to_round + ") zaznamu," +
                    " ifnull(round(sum(CAST(s1.height as REAL) / (select CAST(count(rnd) as REAL) from Score where height > 0  and prep > 0  and userid = s1.userid and rnd <= " + to_round + ")), 2), 9999) rawheight," +
                    " ifnull(round(sum(CAST(s1.height as REAL) / (select CAST(count(rnd) as REAL) from Score where height > 0  and prep > 0  and userid = s1.userid and rnd <= " + to_round + ")), 2),0) height," +
                    " ifnull((select sum(height) from score where height > 0  and prep > 0 and userid = s1.userid and rnd <= " + to_round + "),0) sumheight," +
                    " u.Firstname," +
                    " u.Lastname" +
                    " from Score s1 left join users U on S1.userid = U.id where userid > 0  and prep > 0 and rnd <= " + to_round + " group by s1.userid order by rawheight ASC", "get_statistics_averageheights",to_round);
            }

            if (what == "statistics_averagelandings")
            {
                Players_statistics.Clear();


        SQL_READSOUTEZDATA("select s1.userid,"+
"(select count(userid) from score where userid = s1.userid and userid>0 and prep>0 and rnd <= " + to_round + ") zaznamu," +
" ROUND(cast(sum(s1.landing) as REAL) / (select count(rnd) from Score where userid = s1.userid and userid>0 and prep > 0 and rnd <= " + to_round + " group by userid ), 2) pristani," +
" (select sum(landing) from score where userid = s1.userid and userid>0  and prep > 0 and rnd <= " + to_round + ")  sumpristani," +
" u.Firstname," +
" u.Lastname" +
" from Score s1 left join users U on S1.userid = U.id where userid > 0 and entered is 'True'  and prep > 0  and rnd <= " + to_round + " group by s1.userid order by pristani DESC", "get_statistics_averagelandings",to_round);

            }


            if (what == "users")
            {
                Players_Baseresults.Clear();
                SQL_READSOUTEZDATA("select ((select max(prep) from score s2 where s2.userid = s1.userid and rnd <= "+ to_round + " and skrtacka='True' and refly='False') + (select sum(pen2value) from score s2 where s2.userid = s1.userid and rnd <= " + to_round + " )) skrtacka," +
                    "((select sum(prep) from score s2 where s2.userid = s1.userid and rnd <= " + to_round + " and skrtacka='False' and refly='False') + (select sum(pen2value) from score s2 where s2.userid = s1.userid and rnd <= " + to_round + " )) overalscore," +
                    " (select sum(raw) from score s2 where s2.userid = s1.userid and rnd <= " + to_round + " and s2.skrtacka='False' and s2.refly='False') overalrawscore ,(select sum(pen2value) from score s2 where s2.userid = s1.userid and rnd <= " + to_round + " ) gpen,(select name from Agecategories where id=u.Agecat) agecatstring, s1.*,u.* from score s1 left join users U on S1.userid = U.id where userid>0 group by userid order by overalscore desc,skrtacka desc", "get_baseresults_users");
                if (BIND_SQL_SOUTEZ_ROUNDSFINALE_value == 0) { BIND_MENU_ENABLED_finale = false; BIND_MOVE_TO_FINAL_ROUNDS = false; } else { BIND_MOVE_TO_FINAL_ROUNDS = true; }
            }

            if (what == "users_complete")
            {
                maxscoreproprocenta = 0;
                
                Players_Baseresults_Complete.Clear();
                SQL_READSOUTEZDATA("select ifnull(" +
                    "((select max(prep) from score s2 where s2.userid = s1.userid and rnd > 100 and skrtacka='True' and refly='False') + (select sum(pen2value) from score s2 where s2.userid = s1.userid and rnd > 100)),0) skrtacka_fin," +
                    "ifnull(((select sum(prep) from score s2 where s2.userid = s1.userid and rnd > 100 and skrtacka='False' and refly='False') + (select sum(pen2value) from score s2 where s2.userid = s1.userid and rnd > 100)),0) overalscore_fin," +
                    " ifnull((select sum(raw) from score s2 where s2.userid = s1.userid and rnd > 100 and skrtacka='False' and refly='False'),0) overalrawscore_fin , " +
                    "((select max(prep) from score s2 where s2.userid = s1.userid and rnd < 100 and skrtacka='True' and refly='False' ) +(select sum(pen2value) from score s2 where s2.userid = s1.userid  and rnd < 100  )) skrtacka_base, " +
                    "((select sum(prep) from score s2 where s2.userid = s1.userid and rnd < 100 and skrtacka='False' and refly='False' ) +(select sum(pen2value) from score s2 where s2.userid = s1.userid  and rnd < 100  )) overalscore_base, " +
                    " (select sum(raw) from score s2 where s2.userid = s1.userid  and rnd < 100 and skrtacka='False' and refly='False' ) overalrawscore_base , (select sum(pen2value) from score s2 where s2.userid = s1.userid  ) gpen, (select name from Agecategories where id=u.Agecat) agecatstring,(select name from Agecategories where id=u.customagecat) customagecatstring, s1.*,u.* from score s1 left join users U on S1.userid = U.id where userid>0 and (agecat in (" + age_cat_str + ") or customagecat in ("+ age_cat_str + ")) group by userid order by overalscore_fin desc, skrtacka_fin desc, overalscore_base desc, skrtacka_base desc", "get_baseresults_users_complete");
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


        public Dictionary<int, Visibility> ColumnVisibility { get; set; } = new Dictionary<int, Visibility>();

        public void SetColumnVisibility(int columnIndex, Visibility visibility)
        {
            if (ColumnVisibility.ContainsKey(columnIndex))
            {
                ColumnVisibility[columnIndex] = visibility;
            }
            else
            {
                ColumnVisibility.Add(columnIndex, visibility);
            }
        }

        public Visibility GetColumnVisibility(int columnIndex)
        {
            if (ColumnVisibility.ContainsKey(columnIndex))
            {
                return ColumnVisibility[columnIndex];
            }
            return Visibility.Visible; // Default visibility
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
                BINDING_Timer_listofminutes.Add(new Timer_minutes_seconds() { Value = i, Text = i+" " + Lang.minutes });
            }

        }


        public void FUNCTION_LOAD_TIMERS_SECONDS()
        {
            Console.WriteLine("SECONDS");
            BINDING_Timer_listofseconds.Clear();

            for (int i = 0; i < 60; i++)
            {
                BINDING_Timer_listofseconds.Add(new Timer_minutes_seconds() { Value = i, Text = i+" "+Lang.seconds });
            }

        }


        public void FUNCTION_LOAD_TIMERS_HEIGHT()
        {
            Console.WriteLine("HEIGHT");
            BINDING_Timer_listofheights.Clear();

            for (int i = 0; i < 1000; i++)
            {
                BINDING_Timer_listofheights.Add(new Timer_minutes_seconds() { Value = i, Text = i + " "+Lang.meters });
            }

        }


        public string RelativePath(string absPath, string relTo)
        {
            string[] absDirs = absPath.Split('\\');
            string[] relDirs = relTo.Split('\\');
            // Get the shortest of the two paths 
            int len = absDirs.Length < relDirs.Length ? absDirs.Length : relDirs.Length;
            // Use to determine where in the loop we exited 
            int lastCommonRoot = -1; int index;
            // Find common root 
            for (index = 0; index < len; index++)
            {
                if (absDirs[index] == relDirs[index])
                    lastCommonRoot = index;
                else break;
            }
            // If we didn't find a common prefix then throw 
            if (lastCommonRoot == -1)
            {
                throw new ArgumentException("Paths do not have a common base");
            }
            // Build up the relative path 
            StringBuilder relativePath = new StringBuilder();
            // Add on the .. 
            for (index = lastCommonRoot + 1; index < absDirs.Length; index++)
            {
                if (absDirs[index].Length > 0) relativePath.Append("..\\");
            }
            // Add on the folders 
            for (index = lastCommonRoot + 1; index < relDirs.Length - 1; index++)
            {
                relativePath.Append(relDirs[index] + "\\");
            }
            relativePath.Append(relDirs[relDirs.Length - 1]);
            return relativePath.ToString();
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
                    Console.WriteLine("dirName");
                    Console.WriteLine(dirx.FullName);
                    Console.WriteLine(dirx.Name);

                    if (dirName != "FUNKYMODE" && dirName != "NAMES")
                    {
                        i += 1;
                        var _sndlst = new SoundList()
                        {
                            Id = i,
                            SoundName = dirName
                        };
                        BINDING_SoundList_languages.Add(_sndlst);
                        Console.WriteLine("adding BINDING_SoundList_languages:"+ dirName);
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
            BIND_FINAL_LETOVYCAS_PREP_MAX = MODEL_CONTEST_SOUNDS_FINAL_PREP[MODEL_CONTEST_SOUNDS_FINAL_PREP.Count - 1].VALUE;

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




        public void FUNCTION_MOVE_GROUP_UP_DOWN(int posun, bool zmenit_i_v_seznamu_kol)
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





            BIND_PREWROUND_TEXT = Lang.prew_flight+ " : " + _tmp_selected_round_down + " / " + _tmp_selected_group_down;

            if (_tmp_newround < BIND_SQL_SOUTEZ_ROUNDS + 1 && _tmp_newround > 0)
            {
                BIND_SELECTED_GROUP = _tmp_newgroup;
                BIND_SELECTED_ROUND = _tmp_newround;

                FUNCTION_SELECTED_ROUND_FLYING_USERS(0, 0);
                if (zmenit_i_v_seznamu_kol == true) { FUNCTION_ROUNDS_LOAD_GROUPS(BIND_SELECTED_ROUND); }
            }



            if (_tmp_selected_round_up > BIND_SQL_SOUTEZ_ROUNDS)
            {
                BIND_NEXTROUND_TEXT = Lang.no_next_flight;

                if (_tmp_selected_round_up == BIND_SQL_SOUTEZ_ROUNDS + 1)
                {
                   BIND_PREWROUND_TEXT = Lang.prew_flight+ " : " + BIND_SQL_SOUTEZ_ROUNDS + " / " + (BIND_SQL_SOUTEZ_GROUPS - 1);
                }

            }
            else
            {
                BIND_NEXTROUND_TEXT = Lang.next_flight+ " : " + _tmp_selected_round_up + " / " + _tmp_selected_group_up;
            }

            Console.WriteLine("_tmp_selected_round_down" + _tmp_selected_round_down);
            Console.WriteLine("_tmp_newgroup" + _tmp_newgroup);
            Console.WriteLine("_tmp_selected_group_down" + _tmp_selected_group_down);




            if (_tmp_selected_round_down < 1)
            {
                BIND_PREWROUND_TEXT = Lang.no_prew_flight;

                if (_tmp_selected_round_down == 0)
                {
                    BIND_NEXTROUND_TEXT = Lang.next_flight+ " : 1 / 2";
                }
            }






            if (zmenit_i_v_seznamu_kol == true) {


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


                if (SQL_VERIFY_IF_EXIST("contest", "item", "CONTENT_ONLINE_ENABLED") == false)
                {
                    SQL_SAVESOUTEZDATA("insert into contest (item,value) values ('CONTENT_ONLINE_ENABLED','False');");
                }

                if (SQL_VERIFY_IF_EXIST("contest", "item", "CONTENT_ONLINE_PUBLIC") == false)
                {
                    SQL_SAVESOUTEZDATA("insert into contest (item,value) values ('CONTENT_ONLINE_PUBLIC','False');");
                }

                if (SQL_VERIFY_IF_EXIST("contest", "item", "CONTENT_RANDOM_ID") == false)
                {
                    SQL_SAVESOUTEZDATA("insert into contest (item,value) values ('CONTENT_RANDOM_ID','123');");
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


                if (SQL_VERIFY_IF_EXIST("contest", "item", "POUZITY_TYP_LOSOVANI") == false)
                {
                    SQL_SAVESOUTEZDATA("insert into contest (item,value) values ('POUZITY_TYP_LOSOVANI','unknown');");
                }


                int jecustomagecatoznaceni = 0;

                jecustomagecatoznaceni = int.Parse(SQL_READSOUTEZDATA("SELECT COUNT(*) AS CNTREC FROM pragma_table_info('Agecategories') WHERE name='CUSTOM'", ""));

                if (jecustomagecatoznaceni == 0)
                {
                    SQL_SAVESOUTEZDATA("ALTER TABLE `Agecategories` ADD `CUSTOM` INTEGER DEFAULT 0");
                    SQL_SAVESOUTEZDATA("UPDATE Agecategories SET CUSTOM = 1 WHERE ID >= 4;");
                }


                if (SQL_VERIFY_IF_EXIST("Agecategories", "NAME", "---") == false)
                {
                    SQL_SAVESOUTEZDATA("insert into Agecategories (ID,NAME,CUSTOM) values ('4','65+','1');");
                    SQL_SAVESOUTEZDATA("insert into Agecategories (ID,NAME,CUSTOM) values ('5','ŽENY','1');");
                    SQL_SAVESOUTEZDATA("insert into Agecategories (ID,NAME,CUSTOM) values ('100','---','1');");
                }


                int jenondeletable = 0;
                jenondeletable = int.Parse(SQL_READSOUTEZDATA("SELECT COUNT(*) AS CNTREC FROM pragma_table_info('score') WHERE name='nondeletable'", ""));
                   
                if (jenondeletable == 0)
                {
                    SQL_SAVESOUTEZDATA("ALTER TABLE `score` ADD `nondeletable` TEXT DEFAULT `False`");
                }

                int jecustomagecat = 0;

                jecustomagecat = int.Parse(SQL_READSOUTEZDATA("SELECT COUNT(*) AS CNTREC FROM pragma_table_info('users') WHERE name='Customagecat'", ""));

                if (jecustomagecat == 0)
                {
                    SQL_SAVESOUTEZDATA("ALTER TABLE `users` ADD `Customagecat` TEXT DEFAULT `0`");
                }


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
                    _tmpvysledek = sqlite_datareader.GetString(1);
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
                OnPropertyChanged("CONTENT_ONLINE_URL");

            }
        }

        private int _CONTENT_MASTER_ID;

        public int CONTENT_MASTER_ID
        {
            get { return _CONTENT_MASTER_ID; }
            set
            {
                _CONTENT_MASTER_ID = value;
                OnPropertyChanged("CONTENT_MASTER_ID");

            }
        }



        private string _CONTENT_ONLINE_URL;

        public string CONTENT_ONLINE_URL
        {
            get { return "http://sorgair.com/contest/" + CONTENT_RANDOM_ID; }
            set
            {
                _CONTENT_ONLINE_URL = value;
                OnPropertyChanged("CONTENT_ONLINE_URL");

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


        private bool _CONTENT_ONLINE_PUBLIC;

        public bool CONTENT_ONLINE_PUBLIC
        {
            get { return _CONTENT_ONLINE_PUBLIC; }
            set
            {
                _CONTENT_ONLINE_PUBLIC = value;
                SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='CONTENT_ONLINE_PUBLIC'");
                OnPropertyChanged("CONTENT_ONLINE_PUBLIC");
                if (_CONTENT_ONLINE_PUBLIC is true & CONTENT_RANDOM_ID == "0")
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





        public void FUNCTION_CLOCK_SET_CLOCK_MODE()
        {

            if (HARDWARE_CLOCK_OLD_ISCONNECTED == true)
            {

                byte[] bytes = { 0x83, 0x0, 0x0, 0x0, 0x83, 0x81 };
                _serialPort.Write(bytes, 0, bytes.Length);

            }
        }



        public void FUNCTION_CLOCK_SET_STOPWATCH_MODE()
        {
            if (HARDWARE_CLOCK_OLD_ISCONNECTED == true)
            {


                byte[] bytes = { 0x83, 0x1, 0x0, 0x0, 0x84, 0x81 };
                _serialPort.Write(bytes, 0, bytes.Length);

            }

        }




        public async void FUNCTION_SACLOCK_CREATE_CLOCK(float seconds, int clockparams,bool timeout)
        {
            if (timeout is true) { await Task.Delay(110); }

            _serialPortWrite("AT1+CNT=5,1,2,3,0,"+ clockparams + ","+seconds+",999", false);
           

        }

        public async void FUNCTION_SACLOCK_SETTIMETO_CLOCK(int clockid, float seconds, bool timeout)
        {
            if (timeout is true) { await Task.Delay(110); }
            Console.WriteLine("AT1+CNTSET=" + clockid + "," + seconds);
            _serialPortWrite("AT1+CNTSET=" + clockid + "," + seconds, false);


        }

        public async void FUNCTION_SACLOCK_SETPARAMSTO_CLOCK(float parametry, int clockid, bool timeout)
        {
            if (timeout is true) { await Task.Delay(110); }

            _serialPortWrite("AT1+CNTO2=" + clockid + "," + parametry, false);


        }

        public async void FUNCTION_SACLOCK_CREATE_SMALLCLOCK_RTC(bool timeout)
        {
            if (timeout is true) { await Task.Delay(110); }

            var datet = DateTime.Now;
            int hodiny = datet.Hour * 60 * 60;
            int minuty = datet.Minute * 60;
            int sum = hodiny + minuty;
            _serialPortWrite("AT1+CNT=65,25,2,1,0,136," + sum + ",0", false);


        }


        public async void FUNCTION_SACLOCK_CREATE_SMALLCLOCK(float seconds, int clockparams, bool timeout)
        {
            if (timeout is true) { await Task.Delay(110); }
            _serialPortWrite("AT1+CNT=65,25,2,1,0," + clockparams + "," + seconds + ",0", false);



        }


        public async void FUNCTION_SACLOCK_DELETE_CLOCK(int clockid, bool timeout)
        {
            if (timeout is true) { await Task.Delay(110); }
            _serialPortWrite("AT1+CNTDEL="+clockid, false);
            
        }

        public async void FUNCTION_SACLOCK_COUNTDOWN(int clockid)
        {
        


                _serialPortWrite("AT1+CNTDN=" + clockid, false);
        

        }

        public async void FUNCTION_SACLOCK_COUNTUP(int clockid)
        {
//            await Task.Delay(101);

            _serialPortWrite("AT1+CNTUP="+clockid, false);

        }



        public async void FUNCTION_SACLOCK_SETTEXT(int textid, string text)
        {

            _serialPortWrite("AT1+TEXTT=" + textid+ "," + text, false);
        }


        public async void FUNCTION_SACLOCK_STOPCOUNT(int clockid)
        {



            _serialPortWrite("AT1+CNTSTOP=" + clockid, false);


        }

        public void FUNCTION_CLOCK_SET_STOPWATCH_TIME(int minuty, int vteriny)
        {
            if (HARDWARE_CLOCK_OLD_ISCONNECTED == true)
            {

                int funkce = 0x85;
                int ctvrte = 0;

                int crc = funkce + minuty + vteriny + ctvrte;


                byte[] bytes = { ((byte)funkce), ((byte)minuty), ((byte)vteriny), ((byte)ctvrte), ((byte)crc), 0x81 };
                _serialPort.Write(bytes, 0, bytes.Length);
            }
        }


        public void FUNCTION_CLOCK_SET_DIRECTION(int direction)
        {
            if (HARDWARE_CLOCK_OLD_ISCONNECTED == true)
            {


                int funkce = 0x86;
                int smer = direction;
                int minuty = 0;
                int vteriny = 0;

                int crc = funkce + minuty + vteriny + smer;


                byte[] bytes = { ((byte)funkce), ((byte)smer), ((byte)minuty), ((byte)vteriny), ((byte)crc), 0x81 };
                _serialPort.Write(bytes, 0, bytes.Length);
            }
        }































        #region printing




        private static readonly Random getrandom = new Random();

        public void print_userslist(string frame_template_name, string data_emplate_name, string file_name, string what_string, string output_type)
        {



            string html_main;
            string html_body = "";
            string html_body_withrightdata;
            string html_all;


            Console.WriteLine("Players.Count" + Players.Count);

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);


            html_main = File.ReadAllText(directory + "/Print_templates/" + frame_template_name + ".html", Encoding.UTF8);


            string tmp_style = File.ReadAllText(directory + "/Print_templates/_style.dat", Encoding.UTF8);
            html_main = html_main.Replace("@STYLE", tmp_style);
            string tmp_zahlavi = File.ReadAllText(directory + "/Print_templates/_zahlavi.dat", Encoding.UTF8);
            html_main = html_main.Replace("@ZAHLAVI", tmp_zahlavi);
            string tmp_hlavicka = File.ReadAllText(directory + "/Print_templates/_hlavicka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@HLAVICKA", tmp_hlavicka);
            string tmp_paticka = File.ReadAllText(directory + "/Print_templates/_paticka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@PATICKA", tmp_paticka);
            string tmp_logo = File.ReadAllText(directory + "/Print_templates/_logo.dat", Encoding.UTF8);
            html_main = html_main.Replace("@LOGO", tmp_logo);

            html_main = html_main.Replace("@CONTESTNAME", BIND_SQL_SOUTEZ_NAZEV + " - " + BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@ORGANISATOR", BIND_SQL_SOUTEZ_CLUB);
            html_main = html_main.Replace("@PLACE", BIND_SQL_SOUTEZ_LOKACE);
            html_main = html_main.Replace("@DATE", BIND_SQL_SOUTEZ_DATUM);
            html_main = html_main.Replace("@CONTESTNUMBER", BIND_SQL_SOUTEZ_SMCRID);
            html_main = html_main.Replace("@WHAT", what_string);
            html_main = html_main.Replace("@CATEGORY", BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@DIRECTOR", BIND_SQL_SOUTEZ_DIRECTOR);
            html_main = html_main.Replace("@HEADJURY", BIND_SQL_SOUTEZ_HEADJURY);
            html_main = html_main.Replace("@SUBJURY", BIND_SQL_SOUTEZ_JURY1 + " | " + BIND_SQL_SOUTEZ_JURY2 + " | " + BIND_SQL_SOUTEZ_JURY3);
            html_main = html_main.Replace("@WEATHER", BIND_SQL_SOUTEZ_POCASI);



            html_main = html_main.Replace("@BODY", "<table>@BODY</table>");
            

            html_body = File.ReadAllText(directory + "/Print_templates/" + data_emplate_name + ".html", Encoding.UTF8); ;
            string html_body_complete = "";

            for (int i = 0; i < Players.Count(); i++)
            {


                html_body_withrightdata = html_body;
                Console.WriteLine(html_body_withrightdata);

                html_body_withrightdata = html_body_withrightdata.Replace("@USERNAME", Players[i].LASTNAME + " " + Players[i].FIRSTNAME);
                html_body_withrightdata = html_body_withrightdata.Replace("@CONTESTNAME", BIND_SQL_SOUTEZ_NAZEV + " - " + BIND_SQL_SOUTEZ_KATEGORIE);
                html_body_withrightdata = html_body_withrightdata.Replace("@COUNTRY", Players[i].COUNTRY);
                html_body_withrightdata = html_body_withrightdata.Replace("@ID", Players[i].ID.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@NATLIC", Players[i].NACLIC);
                html_body_withrightdata = html_body_withrightdata.Replace("@NACLIC", Players[i].NACLIC);
                html_body_withrightdata = html_body_withrightdata.Replace("@FAILIC", Players[i].FAILIC);
                html_body_withrightdata = html_body_withrightdata.Replace("@AGECAT", Players[i].AGECAT);
                html_body_withrightdata = html_body_withrightdata.Replace("@CLUB", Players[i].CLUB);
                html_body_withrightdata = html_body_withrightdata.Replace("@PAID", Players[i].PAIDSTR);
                html_body_withrightdata = html_body_withrightdata.Replace("@TEAM", "tym");
                html_body_withrightdata = html_body_withrightdata.Replace("@FREQUENCY", Players[i].FREQ);




                byte[] imageArray = System.IO.File.ReadAllBytes(directory + "/flags/" + Players[i].COUNTRY + ".png");
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                Console.WriteLine(base64ImageRepresentation);
                html_body_withrightdata = html_body_withrightdata.Replace("@FLAG", "data:image/png;base64," + base64ImageRepresentation);


                html_body_complete = html_body_complete + html_body_withrightdata;

            }

            html_body_complete = html_body_complete + "</table>";

            html_all = html_main.Replace("@BODY", html_body_complete);



            if (output_type == "html")
            {

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory + "/Print/" + file_name + ".html"))
                {
                    file.WriteLine(html_all);
                }
                System.Diagnostics.Process.Start(directory + "/Print/" + file_name + ".html");
            }


            if (output_type == "memory")
            {

                memoryprint = memoryprint + html_all;
            }

        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


        public void print_userstatistics(string frame_template_name, string data_emplate_name, string file_name, string what_string, string output_type)
        {



            string html_main;
            string html_body = "";
            string html_body_withrightdata;
            string html_all;


            Console.WriteLine("Players.Count" + Players.Count);

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);


            html_main = File.ReadAllText(directory + "/Print_templates/" + frame_template_name + ".html", Encoding.UTF8);


            string tmp_style = File.ReadAllText(directory + "/Print_templates/_style.dat", Encoding.UTF8);
            html_main = html_main.Replace("@STYLE", tmp_style);
            string tmp_zahlavi = File.ReadAllText(directory + "/Print_templates/_zahlavi.dat", Encoding.UTF8);
            html_main = html_main.Replace("@ZAHLAVI", tmp_zahlavi);
            string tmp_hlavicka = File.ReadAllText(directory + "/Print_templates/_hlavicka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@HLAVICKA", tmp_hlavicka);
            string tmp_paticka = File.ReadAllText(directory + "/Print_templates/_paticka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@PATICKA", tmp_paticka);
            string tmp_logo = File.ReadAllText(directory + "/Print_templates/_logo.dat", Encoding.UTF8);
            html_main = html_main.Replace("@LOGO", tmp_logo);

            html_main = html_main.Replace("@CONTESTNAME", BIND_SQL_SOUTEZ_NAZEV + " - " + BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@ORGANISATOR", BIND_SQL_SOUTEZ_CLUB);
            html_main = html_main.Replace("@PLACE", BIND_SQL_SOUTEZ_LOKACE);
            html_main = html_main.Replace("@DATE", BIND_SQL_SOUTEZ_DATUM);
            html_main = html_main.Replace("@CONTESTNUMBER", BIND_SQL_SOUTEZ_SMCRID);
            html_main = html_main.Replace("@WHAT", what_string);
            html_main = html_main.Replace("@CATEGORY", BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@DIRECTOR", BIND_SQL_SOUTEZ_DIRECTOR);
            html_main = html_main.Replace("@HEADJURY", BIND_SQL_SOUTEZ_HEADJURY);
            html_main = html_main.Replace("@SUBJURY", BIND_SQL_SOUTEZ_JURY1 + " | " + BIND_SQL_SOUTEZ_JURY2 + " | " + BIND_SQL_SOUTEZ_JURY3);
            html_main = html_main.Replace("@WEATHER", BIND_SQL_SOUTEZ_POCASI);



            html_main = html_main.Replace("@BODY", "@BODY");


            html_body = File.ReadAllText(directory + "/Print_templates/" + data_emplate_name + ".html", Encoding.UTF8); ;
            string html_body_complete = "";


            for (int i = 0; i < Players.Count(); i++)
            {


                html_body_withrightdata = html_body;
                Console.WriteLine(html_body_withrightdata);

                html_body_withrightdata = html_body_withrightdata.Replace("@USERNAME", Players[i].LASTNAME + " " + Players[i].FIRSTNAME);
                html_body_withrightdata = html_body_withrightdata.Replace("@CONTESTNAME", BIND_SQL_SOUTEZ_NAZEV + " - " + BIND_SQL_SOUTEZ_KATEGORIE);
                html_body_withrightdata = html_body_withrightdata.Replace("@COUNTRY", Players[i].COUNTRY);
                html_body_withrightdata = html_body_withrightdata.Replace("@ID", Players[i].ID.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@NATLIC", Players[i].NACLIC);
                html_body_withrightdata = html_body_withrightdata.Replace("@NACLIC", Players[i].NACLIC);
                html_body_withrightdata = html_body_withrightdata.Replace("@FAILIC", Players[i].FAILIC);
                html_body_withrightdata = html_body_withrightdata.Replace("@AGECAT", Players[i].AGECAT);
                html_body_withrightdata = html_body_withrightdata.Replace("@CLUB", Players[i].CLUB);
                html_body_withrightdata = html_body_withrightdata.Replace("@PAID", Players[i].PAIDSTR);
                html_body_withrightdata = html_body_withrightdata.Replace("@TEAM", "tym");
                html_body_withrightdata = html_body_withrightdata.Replace("@FREQUENCY", Players[i].FREQ);




                byte[] imageArray = System.IO.File.ReadAllBytes(directory + "/flags/" + Players[i].COUNTRY + ".png");
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                Console.WriteLine(base64ImageRepresentation);
                html_body_withrightdata = html_body_withrightdata.Replace("@FLAG", "data:image/png;base64," + base64ImageRepresentation);


                html_body_complete = html_body_complete + html_body_withrightdata;

            }

            html_body_complete = html_body_complete + "</table>";

            html_all = html_main.Replace("@BODY", html_body_complete);



            if (output_type == "html")
            {

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory + "/Print/" + file_name + ".html"))
                {
                    file.WriteLine(html_all);
                }
                System.Diagnostics.Process.Start(directory + "/Print/" + file_name + ".html");
            }


            if (output_type == "memory")
            {

                memoryprint = memoryprint + html_all;
            }

        }

      



        public void print_basicresults(string frame_template_name, string data_emplate_name, string file_name, string what_string, string output_type, string[] visibility)
        {



            string html_main;
            string html_body;
            string html_body_withrightdata;
            string html_all;


            Console.WriteLine("Players.Count" + Players.Count);

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);


            html_main = File.ReadAllText(directory + "/Print_templates/" + frame_template_name + ".html", Encoding.UTF8);


            string tmp_style = File.ReadAllText(directory + "/Print_templates/_style.dat", Encoding.UTF8);
            html_main = html_main.Replace("@STYLE", tmp_style);
            string tmp_zahlavi = File.ReadAllText(directory + "/Print_templates/_zahlavi.dat", Encoding.UTF8);
            html_main = html_main.Replace("@ZAHLAVI", tmp_zahlavi);
            string tmp_hlavicka = File.ReadAllText(directory + "/Print_templates/_hlavicka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@HLAVICKA", tmp_hlavicka);
            string tmp_paticka = File.ReadAllText(directory + "/Print_templates/_paticka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@PATICKA", tmp_paticka);
            string tmp_logo = File.ReadAllText(directory + "/Print_templates/_logo.dat", Encoding.UTF8);
            html_main = html_main.Replace("@LOGO", tmp_logo);

            html_main = html_main.Replace("@CONTESTNAME", BIND_SQL_SOUTEZ_NAZEV + " - " + BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@ORGANISATOR", BIND_SQL_SOUTEZ_CLUB);
            html_main = html_main.Replace("@PLACE", BIND_SQL_SOUTEZ_LOKACE);
            html_main = html_main.Replace("@DATE", BIND_SQL_SOUTEZ_DATUM);
            html_main = html_main.Replace("@CONTESTNUMBER", BIND_SQL_SOUTEZ_SMCRID);
            html_main = html_main.Replace("@WHAT", what_string);
            html_main = html_main.Replace("@CATEGORY", BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@DIRECTOR", BIND_SQL_SOUTEZ_DIRECTOR);
            html_main = html_main.Replace("@HEADJURY", BIND_SQL_SOUTEZ_HEADJURY);
            html_main = html_main.Replace("@SUBJURY", BIND_SQL_SOUTEZ_JURY1 + " | " + BIND_SQL_SOUTEZ_JURY2 + " | " + BIND_SQL_SOUTEZ_JURY3);
            html_main = html_main.Replace("@WEATHER", BIND_SQL_SOUTEZ_POCASI);

            html_body = File.ReadAllText(directory + "/Print_templates/" + data_emplate_name + ".html", Encoding.UTF8);
            string html_body_complete = "";



            html_body_complete = $@"<table>
                <th>Pozice</th>
                <th>Soutěžící</th>
                <th class='visibility_{visibility[0]}'>Stát</th>
                <th class='visibility_{visibility[1]}'>ID</th>
                <th class='visibility_{visibility[2]}'>AGECAT</th>
                <th>Celkové scóre</th>
                <th class='visibility_{visibility[3]}'>G.Pen</th>
                <th class='visibility_{visibility[4]}'>Ztráta</th>
                <th class='visibility_{visibility[5]}'>%</th>
                <th class='visibility_{visibility[6]}'>Kolo 1</th>
                <th class='visibility_{visibility[7]}'>Kolo 2</th>
                <th class='visibility_{visibility[8]}'>Kolo 3</th>
                <th class='visibility_{visibility[9]}'>Kolo 4</th>
                <th class='visibility_{visibility[10]}'>Kolo 5</th>
                <th class='visibility_{visibility[11]}'>Kolo 6</th>
                <th class='visibility_{visibility[12]}'>Kolo 7</th>
                <th class='visibility_{visibility[13]}'>Kolo 8</th>
                <th class='visibility_{visibility[14]}'>Kolo 9</th>
                <th class='visibility_{visibility[15]}'>Kolo 10</th>

                <th class='visibility_{visibility[16]}'>Kolo 11</th>
                <th class='visibility_{visibility[17]}'>Kolo 12</th>
                <th class='visibility_{visibility[18]}'>Kolo 13</th>
                <th class='visibility_{visibility[19]}'>Kolo 14</th>
                <th class='visibility_{visibility[20]}'>Kolo 15</th>
                <th class='visibility_{visibility[21]}'>Kolo 16</th>
                <th class='visibility_{visibility[22]}'>Kolo 17</th>
                <th class='visibility_{visibility[23]}'>Kolo 18</th>
                <th class='visibility_{visibility[24]}'>Kolo 19</th>
                <th class='visibility_{visibility[25]}'>Kolo 20</th>



                @BODY
          </table>";

            html_body_withrightdata = "";
            Console.WriteLine(Players_Baseresults.Count());
            for (int i = 0; i < Players_Baseresults.Count(); i++)
            {

                html_body = $@"<tr>
    <td>@POSITION</td>
    <td>@USERNAME</td>
    <td class='visibility_{visibility[0]}'><img class='vlajka' src='@FLAG' /></td>
    <td class='visibility_{visibility[1]}'>@ID</td>
    <td class='visibility_{visibility[2]}'>@AGECAT</td>
    <td>@SCORE</td>
    <td class='visibility_{visibility[3]}'>@GPEN</td>
    <td class='visibility_{visibility[4]}'>@LOST</td>
    <td class='visibility_{visibility[5]}'>@PERC</td>
<td class='visibility_{visibility[6]} skrtacka{Players_Baseresults[i].RND1RES_SKRTACKA}'>@R1X</td>
<td class='visibility_{visibility[7]} skrtacka{Players_Baseresults[i].RND2RES_SKRTACKA}'>@R2X</td>
<td class='visibility_{visibility[8]} skrtacka{Players_Baseresults[i].RND3RES_SKRTACKA}'>@R3X</td>
<td class='visibility_{visibility[9]} skrtacka{Players_Baseresults[i].RND4RES_SKRTACKA}'>@R4X</td>
<td class='visibility_{visibility[10]} skrtacka{Players_Baseresults[i].RND5RES_SKRTACKA}'>@R5X</td>
<td class='visibility_{visibility[11]} skrtacka{Players_Baseresults[i].RND6RES_SKRTACKA}'>@R6X</td>
<td class='visibility_{visibility[12]} skrtacka{Players_Baseresults[i].RND7RES_SKRTACKA}'>@R7X</td>
<td class='visibility_{visibility[13]} skrtacka{Players_Baseresults[i].RND8RES_SKRTACKA}'>@R8X</td>
<td class='visibility_{visibility[14]} skrtacka{Players_Baseresults[i].RND9RES_SKRTACKA}'>@R9X</td>
<td class='visibility_{visibility[15]} skrtacka{Players_Baseresults[i].RND10RES_SKRTACKA}'>@R10X</td>
<td class='visibility_{visibility[16]} skrtacka{Players_Baseresults[i].RND11RES_SKRTACKA}'>@R11X</td>
<td class='visibility_{visibility[17]} skrtacka{Players_Baseresults[i].RND12RES_SKRTACKA}'>@R12X</td>
<td class='visibility_{visibility[18]} skrtacka{Players_Baseresults[i].RND13RES_SKRTACKA}'>@R13X</td>
<td class='visibility_{visibility[19]} skrtacka{Players_Baseresults[i].RND14RES_SKRTACKA}'>@R14X</td>
<td class='visibility_{visibility[20]} skrtacka{Players_Baseresults[i].RND15RES_SKRTACKA}'>@R15X</td>
<td class='visibility_{visibility[21]} skrtacka{Players_Baseresults[i].RND16RES_SKRTACKA}'>@R16X</td>
<td class='visibility_{visibility[22]} skrtacka{Players_Baseresults[i].RND17RES_SKRTACKA}'>@R17X</td>
<td class='visibility_{visibility[23]} skrtacka{Players_Baseresults[i].RND18RES_SKRTACKA}'>@R18X</td>
<td class='visibility_{visibility[24]} skrtacka{Players_Baseresults[i].RND19RES_SKRTACKA}'>@R19X</td>
<td class='visibility_{visibility[25]} skrtacka{Players_Baseresults[i].RND20RES_SKRTACKA}'>@R20X</td>
</tr>";
                string tabulkaletu = "";




                html_body_withrightdata = html_body_withrightdata + html_body;

                html_body_withrightdata = html_body_withrightdata.Replace("@USERNAME", Players_Baseresults[i].PLAYERDATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@POSITION", Players_Baseresults[i].POSITION.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@ID", Players_Baseresults[i].ID.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@AGECAT", Players_Baseresults[i].AGECAT.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@SCORE", Players_Baseresults[i].PREPSCORE.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@GPEN", Players_Baseresults[i].GPEN.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@LOST", Players_Baseresults[i].PREPSCOREDIFF.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@PERC", Players_Baseresults[i].PROCENTASCORE.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@R1X", Players_Baseresults[i].RND1RES_SCORE + "<br>" + Players_Baseresults[i].RND1RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R2X", Players_Baseresults[i].RND2RES_SCORE + "<br>" + Players_Baseresults[i].RND2RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R3X", Players_Baseresults[i].RND3RES_SCORE + "<br>" + Players_Baseresults[i].RND3RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R4X", Players_Baseresults[i].RND4RES_SCORE + "<br>" + Players_Baseresults[i].RND4RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R5X", Players_Baseresults[i].RND5RES_SCORE + "<br>" + Players_Baseresults[i].RND5RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R6X", Players_Baseresults[i].RND6RES_SCORE + "<br>" + Players_Baseresults[i].RND6RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R7X", Players_Baseresults[i].RND7RES_SCORE + "<br>" + Players_Baseresults[i].RND7RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R8X", Players_Baseresults[i].RND8RES_SCORE + "<br>" + Players_Baseresults[i].RND8RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R9X", Players_Baseresults[i].RND9RES_SCORE + "<br>" + Players_Baseresults[i].RND9RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R10", Players_Baseresults[i].RND10RES_SCORE + "<br>" + Players_Baseresults[i].RND10RES_DATA);


                html_body_withrightdata = html_body_withrightdata.Replace("@R11", Players_Baseresults[i].RND11RES_SCORE + "<br>" + Players_Baseresults[i].RND11RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R12", Players_Baseresults[i].RND12RES_SCORE + "<br>" + Players_Baseresults[i].RND12RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R13", Players_Baseresults[i].RND13RES_SCORE + "<br>" + Players_Baseresults[i].RND13RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R14", Players_Baseresults[i].RND14RES_SCORE + "<br>" + Players_Baseresults[i].RND14RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R15", Players_Baseresults[i].RND15RES_SCORE + "<br>" + Players_Baseresults[i].RND15RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R16", Players_Baseresults[i].RND16RES_SCORE + "<br>" + Players_Baseresults[i].RND16RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R17", Players_Baseresults[i].RND17RES_SCORE + "<br>" + Players_Baseresults[i].RND17RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R18", Players_Baseresults[i].RND18RES_SCORE + "<br>" + Players_Baseresults[i].RND18RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R19", Players_Baseresults[i].RND19RES_SCORE + "<br>" + Players_Baseresults[i].RND19RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R20", Players_Baseresults[i].RND20RES_SCORE + "<br>" + Players_Baseresults[i].RND20RES_DATA);




                byte[] imageArray = System.IO.File.ReadAllBytes(Players_Baseresults[i].FLAG);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                Console.WriteLine(base64ImageRepresentation);
                html_body_withrightdata = html_body_withrightdata.Replace("@FLAG", "data:image/png;base64," + base64ImageRepresentation);
            }

            html_body_complete = html_body_complete.Replace("@BODY", html_body_withrightdata);



            html_all = html_main.Replace("@BODY", html_body_complete);



            if (output_type == "html")
            {

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory + "/Print/" + file_name + ".html"))
                {
                    file.WriteLine(html_all);
                }
                System.Diagnostics.Process.Start(directory + "/Print/" + file_name + ".html");
            }


            if (output_type == "memory")
            {

                memoryprint = memoryprint + html_all;
            }

        }


        public void print_basicresultsbygrp(string frame_template_name, string data_emplate_name, string file_name, string what_string, string output_type, string[] visibility)
        {



            string html_main;
            string html_body;
            string html_all;


            Console.WriteLine("Players.Count" + Players.Count);

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);


            html_main = File.ReadAllText(directory + "/Print_templates/" + frame_template_name + ".html", Encoding.UTF8);


            string tmp_style = File.ReadAllText(directory + "/Print_templates/_style.dat", Encoding.UTF8);
            html_main = html_main.Replace("@STYLE", tmp_style);
            string tmp_zahlavi = File.ReadAllText(directory + "/Print_templates/_zahlavi.dat", Encoding.UTF8);
            html_main = html_main.Replace("@ZAHLAVI", tmp_zahlavi);
            string tmp_hlavicka = File.ReadAllText(directory + "/Print_templates/_hlavicka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@HLAVICKA", tmp_hlavicka);
            string tmp_paticka = File.ReadAllText(directory + "/Print_templates/_paticka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@PATICKA", tmp_paticka);
            string tmp_logo = File.ReadAllText(directory + "/Print_templates/_logo.dat", Encoding.UTF8);
            html_main = html_main.Replace("@LOGO", tmp_logo);

            html_main = html_main.Replace("@CONTESTNAME", BIND_SQL_SOUTEZ_NAZEV + " - " + BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@ORGANISATOR", BIND_SQL_SOUTEZ_CLUB);
            html_main = html_main.Replace("@PLACE", BIND_SQL_SOUTEZ_LOKACE);
            html_main = html_main.Replace("@DATE", BIND_SQL_SOUTEZ_DATUM);
            html_main = html_main.Replace("@CONTESTNUMBER", BIND_SQL_SOUTEZ_SMCRID);
            html_main = html_main.Replace("@WHAT", what_string);
            html_main = html_main.Replace("@CATEGORY", BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@DIRECTOR", BIND_SQL_SOUTEZ_DIRECTOR);
            html_main = html_main.Replace("@HEADJURY", BIND_SQL_SOUTEZ_HEADJURY);
            html_main = html_main.Replace("@SUBJURY", BIND_SQL_SOUTEZ_JURY1 + " | " + BIND_SQL_SOUTEZ_JURY2 + " | " + BIND_SQL_SOUTEZ_JURY3);
            html_main = html_main.Replace("@WEATHER", BIND_SQL_SOUTEZ_POCASI);

            html_body = File.ReadAllText(directory + "/Print_templates/" + data_emplate_name + ".html", Encoding.UTF8);
            string html_body_complete = "";

            html_body_complete = $@"
                <table>
                @BODY
                  </table>";



            string html_obsah = "";
            string html_body_withrightdata = "";
            for (int i = 0; i < FUNCTION_KOLIK_JE_SKUPIN_V_KOLE(BIND_ROUNDS_IN_RESULTS, "", false); i++)
            {





                html_body_withrightdata = $@"KOLO  {BIND_ROUNDS_IN_RESULTS} SKUPINA {i + 1}<br>
                <table>
                <th>Startoviště</th>
                <th>Soutěžící</th>
                <th>Minuty</th>
                <th>Vteřiny</th>
                <th>Výška</th>
                <th>Přistání</th>
                <th>Surové skóre</th>
                <th>Přepočtené skóre</th>



             
                  ";


                Players_Actual_SelectedRoundandGroup.Clear();
                SQL_READSOUTEZDATA("select U.ID,S.stp startpoint,U.Firstname,U.Lastname, ifnull(s.minutes,0) minutes, ifnull(s.seconds,0) seconds, ifnull(s.landing,0) landing, ifnull(s.height,0) height, ifnull(s.pen1id,0) pen1, ifnull(s.pen2id,0) pen2, ifnull(s.raw,0) raw, ifnull(s.prep,0) prep, ifnull(s.entered,'False') entered from score S left join users U on S.userid = U.id where  s.rnd = " + BIND_ROUNDS_IN_RESULTS + " and s.grp = " + (i+1)+ " order by s.prep desc; ", "get_Players_Actual_SelectedRoundandgroup");




                Console.WriteLine(Players_Actual_SelectedRoundandGroup.Count());
                for (int u = 0; u < Players_Actual_SelectedRoundandGroup.Count(); u++)
                {

                    html_body_withrightdata = html_body_withrightdata + $@"<tr>
                        <td>@STARTPOINT</td>
                        <td>@USERNAME</td>
                        <td>@MINUTES</td>
                        <td>@SECONDS</td>
                        <td>@HEIGHT</td>
                        <td>@LANDING</td>
                        <td>@RAW</td>
                        <td>@PREP</td>
                        </tr>";




                    //html_body_withrightdata = html_body_withrightdata + html_body_content;

                    //html_body_withrightdata = html_body_withrightdata.Replace("@STARTPOINT", (u+1).ToString());
                    html_body_withrightdata = html_body_withrightdata.Replace("@STARTPOINT", Players_Actual_SelectedRoundandGroup[u].ID.ToString());
                    html_body_withrightdata = html_body_withrightdata.Replace("@USERNAME", Players_Actual_SelectedRoundandGroup[u].LASTNAME + " " + Players_Actual_SelectedRoundandGroup[u].FIRSTNAME);
                    html_body_withrightdata = html_body_withrightdata.Replace("@MINUTES", Players_Actual_SelectedRoundandGroup[u].SCORE_MINUTES.ToString());
                    html_body_withrightdata = html_body_withrightdata.Replace("@SECONDS", Players_Actual_SelectedRoundandGroup[u].SCORE_SECONDS.ToString());
                    html_body_withrightdata = html_body_withrightdata.Replace("@HEIGHT", Players_Actual_SelectedRoundandGroup[u].SCORE_HEIGHT.ToString());
                    html_body_withrightdata = html_body_withrightdata.Replace("@LANDING", Players_Actual_SelectedRoundandGroup[u].SCORE_LANDING.ToString());
                    html_body_withrightdata = html_body_withrightdata.Replace("@RAW", Players_Actual_SelectedRoundandGroup[u].SCORE_RAW.ToString());
                    html_body_withrightdata = html_body_withrightdata.Replace("@PREP", Players_Actual_SelectedRoundandGroup[u].SCORE_PREP.ToString());




                    //byte[] imageArray = System.IO.File.ReadAllBytes(Players_Actual_SelectedRoundandGroup[u].PLAYERDATA);
                    //string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                    //Console.WriteLine(base64ImageRepresentation);
                    //html_body_withrightdata = html_body_withrightdata.Replace("@FLAG", "data:image/png;base64," + base64ImageRepresentation);
                }
                html_body_withrightdata = html_body_withrightdata + "</table>";

                //html_body_content = html_body_content + html_body_withrightdata + $@"</table><br>";
                html_obsah = html_obsah + html_body_withrightdata + $@"<br>";

            }
            html_body_complete = html_body_complete.Replace("@BODY", html_obsah);









            html_all = html_main.Replace("@BODY", html_body_complete);



            if (output_type == "html")
            {

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory + "/Print/" + file_name + ".html"))
                {
                    file.WriteLine(html_all);
                }
                System.Diagnostics.Process.Start(directory + "/Print/" + file_name + ".html");
            }


            if (output_type == "memory")
            {

                memoryprint = memoryprint + html_all;
            }

        }


        public async void print_completeresults(string frame_template_name, string data_emplate_name, string file_name, string what_string, string output_type, string[] visibility)
        {



            string html_main;
            string html_body;
            string html_body_withrightdata;
            string html_all;


            Console.WriteLine("Players.Count" + Players.Count);

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);


            html_main = File.ReadAllText(directory + "/Print_templates/" + frame_template_name + ".html", Encoding.UTF8);


            string tmp_style = File.ReadAllText(directory + "/Print_templates/_style.dat", Encoding.UTF8);
            html_main = html_main.Replace("@STYLE", tmp_style);
            string tmp_zahlavi = File.ReadAllText(directory + "/Print_templates/_zahlavi.dat", Encoding.UTF8);
            html_main = html_main.Replace("@ZAHLAVI", tmp_zahlavi);
            string tmp_hlavicka = File.ReadAllText(directory + "/Print_templates/_hlavicka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@HLAVICKA", tmp_hlavicka);
            string tmp_paticka = File.ReadAllText(directory + "/Print_templates/_paticka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@PATICKA", tmp_paticka);
            string tmp_logo = File.ReadAllText(directory + "/Print_templates/_logo.dat", Encoding.UTF8);
            html_main = html_main.Replace("@LOGO", tmp_logo);

            html_main = html_main.Replace("@CONTESTNAME", BIND_SQL_SOUTEZ_NAZEV + " - " + BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@ORGANISATOR", BIND_SQL_SOUTEZ_CLUB);
            html_main = html_main.Replace("@PLACE", BIND_SQL_SOUTEZ_LOKACE);
            html_main = html_main.Replace("@DATE", BIND_SQL_SOUTEZ_DATUM);
            html_main = html_main.Replace("@WHAT", what_string);
            html_main = html_main.Replace("@CONTESTNUMBER", BIND_SQL_SOUTEZ_SMCRID);
            html_main = html_main.Replace("@CATEGORY", BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@DIRECTOR", BIND_SQL_SOUTEZ_DIRECTOR);
            html_main = html_main.Replace("@HEADJURY", BIND_SQL_SOUTEZ_HEADJURY);
            html_main = html_main.Replace("@SUBJURY", BIND_SQL_SOUTEZ_JURY1 + " | " + BIND_SQL_SOUTEZ_JURY2 + " | " + BIND_SQL_SOUTEZ_JURY3);
            html_main = html_main.Replace("@WEATHER", BIND_SQL_SOUTEZ_POCASI);

            html_body = File.ReadAllText(directory + "/Print_templates/" + data_emplate_name + ".html", Encoding.UTF8);
            string html_body_complete = "";


            html_body_complete = $@"<table>
                <th>Pozice</th>
                <th>Soutěžící</th>
                <th class='visibility_{visibility[0]}'>Stát</th>
                <th class='visibility_{visibility[1]}'>ID</th>
                <th class='visibility_{visibility[2]}'>NAT lic.</th>
                <th class='visibility_{visibility[3]}'>FAI lic.</th>
                <th class='visibility_{visibility[4]}'>AGECAT</th>
                <th class='visibility_{visibility[5]}'>G.Pen</th>
                <th class='visibility_{visibility[6]}'>F.scóre</th>
                <th class='visibility_{visibility[7]}'>F.Ztráta</th>
                <th class='visibility_{visibility[8]}'>F1</th>
                <th class='visibility_{visibility[9]}'>F2</th>
                <th class='visibility_{visibility[10]}'>F3</th>
                <th class='visibility_{visibility[11]}'>F4</th>
                <th class='visibility_{visibility[12]}'>F5</th>
                <th class='visibility_{visibility[13]}'>Bonus</th>
                <th class='visibility_{visibility[14]}'>1000</th>
                <th class='visibility_{visibility[15]}'>Z.scóre</th>
                <th class='visibility_{visibility[16]}'>Z.Ztráta</th>
                <th class='visibility_{visibility[17]}'>Z.%</th>
      <th class='visibility_{visibility[18]}'>Kolo 1</th>
<th class='visibility_{visibility[19]}'>Kolo 2</th>
<th class='visibility_{visibility[20]}'>Kolo 3</th>
<th class='visibility_{visibility[21]}'>Kolo 4</th>
<th class='visibility_{visibility[22]}'>Kolo 5</th>
<th class='visibility_{visibility[23]}'>Kolo 6</th>
<th class='visibility_{visibility[24]}'>Kolo 7</th>
<th class='visibility_{visibility[25]}'>Kolo 8</th>
<th class='visibility_{visibility[26]}'>Kolo 9</th>
<th class='visibility_{visibility[27]}'>Kolo 10</th>
<th class='visibility_{visibility[28]}'>Kolo 11</th>
<th class='visibility_{visibility[29]}'>Kolo 12</th>
<th class='visibility_{visibility[30]}'>Kolo 13</th>
<th class='visibility_{visibility[31]}'>Kolo 14</th>
<th class='visibility_{visibility[32]}'>Kolo 15</th>
<th class='visibility_{visibility[33]}'>Kolo 16</th>
<th class='visibility_{visibility[34]}'>Kolo 17</th>
<th class='visibility_{visibility[35]}'>Kolo 18</th>
<th class='visibility_{visibility[36]}'>Kolo 19</th>
                @BODY
          </table>";

            html_body_withrightdata = "";

            for (int i = 0; i < Players_Baseresults_Complete.Count(); i++)
            {

                html_body = $@"<tr>
    <td>@POSITION</td>
    <td><a href='#USER_@ID'>@USERNAME</a></td>
    <td class='visibility_{visibility[0]}'><img class='vlajka' src='@FLAG' /></td>
    <td class='visibility_{visibility[1]}'>@ID</td>
    <td class='visibility_{visibility[2]}'>@NATLIC</td>
    <td class='visibility_{visibility[3]}'>@FAILIC</td>
    <td class='visibility_{visibility[4]}'>@AGECAT</td>
    <td class='visibility_{visibility[5]}'>@GPEN</td>
    <td class='visibility_{visibility[6]}'>@FINSCO</td>
    <td class='visibility_{visibility[7]}'>@FINLST</td>
    <td class='visibility_{visibility[8]} skrtacka{Players_Baseresults_Complete[i].RND1RES_SKRTACKA_F}'>@F1</td>
    <td class='visibility_{visibility[9]} skrtacka{Players_Baseresults_Complete[i].RND2RES_SKRTACKA_F}'>@F2</td>
    <td class='visibility_{visibility[10]} skrtacka{Players_Baseresults_Complete[i].RND3RES_SKRTACKA_F}'>@F3</td>
    <td class='visibility_{visibility[11]} skrtacka{Players_Baseresults_Complete[i].RND4RES_SKRTACKA_F}'>@F4</td>
    <td class='visibility_{visibility[12]} skrtacka{Players_Baseresults_Complete[i].RND5RES_SKRTACKA_F}'>@F5</td>
    <td class='visibility_{visibility[13]}'>@BONUS</td>
    <td class='visibility_{visibility[14]}'>@1000</td>
    <td class='visibility_{visibility[15]}'>@SCORE</td>
    <td class='visibility_{visibility[16]}'>@LOST</td>
    <td class='visibility_{visibility[17]}'>@PERC</td>
<td class='visibility_{visibility[18]} skrtacka{Players_Baseresults_Complete[i].RND1RES_SKRTACKA}'>@R1X</td>
<td class='visibility_{visibility[19]} skrtacka{Players_Baseresults_Complete[i].RND2RES_SKRTACKA}'>@R2X</td>
<td class='visibility_{visibility[20]} skrtacka{Players_Baseresults_Complete[i].RND3RES_SKRTACKA}'>@R3X</td>
<td class='visibility_{visibility[21]} skrtacka{Players_Baseresults_Complete[i].RND4RES_SKRTACKA}'>@R4X</td>
<td class='visibility_{visibility[22]} skrtacka{Players_Baseresults_Complete[i].RND5RES_SKRTACKA}'>@R5X</td>
<td class='visibility_{visibility[23]} skrtacka{Players_Baseresults_Complete[i].RND6RES_SKRTACKA}'>@R6X</td>
<td class='visibility_{visibility[24]} skrtacka{Players_Baseresults_Complete[i].RND7RES_SKRTACKA}'>@R7X</td>
<td class='visibility_{visibility[25]} skrtacka{Players_Baseresults_Complete[i].RND8RES_SKRTACKA}'>@R8X</td>
<td class='visibility_{visibility[26]} skrtacka{Players_Baseresults_Complete[i].RND9RES_SKRTACKA}'>@R9X</td>
<td class='visibility_{visibility[27]} skrtacka{Players_Baseresults_Complete[i].RND10RES_SKRTACKA}'>@R10</td>
<td class='visibility_{visibility[28]} skrtacka{Players_Baseresults_Complete[i].RND11RES_SKRTACKA}'>@R11</td>
<td class='visibility_{visibility[29]} skrtacka{Players_Baseresults_Complete[i].RND12RES_SKRTACKA}'>@R12</td>
<td class='visibility_{visibility[30]} skrtacka{Players_Baseresults_Complete[i].RND13RES_SKRTACKA}'>@R13</td>
<td class='visibility_{visibility[31]} skrtacka{Players_Baseresults_Complete[i].RND14RES_SKRTACKA}'>@R14</td>
<td class='visibility_{visibility[32]} skrtacka{Players_Baseresults_Complete[i].RND15RES_SKRTACKA}'>@R15</td>
<td class='visibility_{visibility[33]} skrtacka{Players_Baseresults_Complete[i].RND16RES_SKRTACKA}'>@R16</td>
<td class='visibility_{visibility[34]} skrtacka{Players_Baseresults_Complete[i].RND17RES_SKRTACKA}'>@R17</td>
<td class='visibility_{visibility[35]} skrtacka{Players_Baseresults_Complete[i].RND18RES_SKRTACKA}'>@R18</td>
<td class='visibility_{visibility[36]} skrtacka{Players_Baseresults_Complete[i].RND19RES_SKRTACKA}'>@R19</td>
</tr>";

                string tabulkaletu = "";




                html_body_withrightdata = html_body_withrightdata + html_body;

                html_body_withrightdata = html_body_withrightdata.Replace("@USERNAME", Players_Baseresults_Complete[i].PLAYERDATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@POSITION", Players_Baseresults_Complete[i].POSITION.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@ID", Players_Baseresults_Complete[i].ID.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@NATLIC", Players_Baseresults_Complete[i].NATLIC.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@FAILIC", Players_Baseresults_Complete[i].FAILIC.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@AGECAT", Players_Baseresults_Complete[i].AGECAT.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@GPEN", Players_Baseresults_Complete[i].GPEN.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@SCORE", Players_Baseresults_Complete[i].PREPSCORE_BASE.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@LOST", Players_Baseresults_Complete[i].PREPSCOREDIFF_BASE.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@PERC", Players_Baseresults_Complete[i].PROCENTASCORE.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@R1X", Players_Baseresults_Complete[i].RND1RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND1RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R2X", Players_Baseresults_Complete[i].RND2RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND2RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R3X", Players_Baseresults_Complete[i].RND3RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND3RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R4X", Players_Baseresults_Complete[i].RND4RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND4RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R5X", Players_Baseresults_Complete[i].RND5RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND5RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R6X", Players_Baseresults_Complete[i].RND6RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND6RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R7X", Players_Baseresults_Complete[i].RND7RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND7RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R8X", Players_Baseresults_Complete[i].RND8RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND8RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R9X", Players_Baseresults_Complete[i].RND9RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND9RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R10", Players_Baseresults_Complete[i].RND10RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND10RES_DATA);

                html_body_withrightdata = html_body_withrightdata.Replace("@R11", Players_Baseresults_Complete[i].RND11RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND11RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R12", Players_Baseresults_Complete[i].RND12RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND12RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R13", Players_Baseresults_Complete[i].RND13RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND13RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R14", Players_Baseresults_Complete[i].RND14RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND14RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R15", Players_Baseresults_Complete[i].RND15RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND15RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R16", Players_Baseresults_Complete[i].RND16RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND16RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R17", Players_Baseresults_Complete[i].RND17RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND17RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R18", Players_Baseresults_Complete[i].RND18RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND18RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R19", Players_Baseresults_Complete[i].RND19RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND19RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@R20", Players_Baseresults_Complete[i].RND20RES_SCORE + "<br>" + Players_Baseresults_Complete[i].RND20RES_DATA);


                html_body_withrightdata = html_body_withrightdata.Replace("@F1", Players_Baseresults_Complete[i].RND1RES_SCORE_F + "<br>" + Players_Baseresults_Complete[i].RND1RES_DATA_F);
                html_body_withrightdata = html_body_withrightdata.Replace("@F2", Players_Baseresults_Complete[i].RND2RES_SCORE_F + "<br>" + Players_Baseresults_Complete[i].RND2RES_DATA_F);
                html_body_withrightdata = html_body_withrightdata.Replace("@F3", Players_Baseresults_Complete[i].RND3RES_SCORE_F + "<br>" + Players_Baseresults_Complete[i].RND3RES_DATA_F);
                html_body_withrightdata = html_body_withrightdata.Replace("@F4", Players_Baseresults_Complete[i].RND4RES_SCORE_F + "<br>" + Players_Baseresults_Complete[i].RND4RES_DATA_F);
                html_body_withrightdata = html_body_withrightdata.Replace("@F5", Players_Baseresults_Complete[i].RND5RES_SCORE_F + "<br>" + Players_Baseresults_Complete[i].RND5RES_DATA_F);
                html_body_withrightdata = html_body_withrightdata.Replace("@FINSCO", Players_Baseresults_Complete[i].PREPSCORE_FINAL.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@FINLST", Players_Baseresults_Complete[i].PREPSCOREDIFF_FINAL.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@BONUS", Players_Baseresults_Complete[i].BONUS_POINTS.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@1000", Players_Baseresults_Complete[i].TO_1000.ToString());




                byte[] imageArray = System.IO.File.ReadAllBytes(Players_Baseresults_Complete[i].FLAG);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                Console.WriteLine(base64ImageRepresentation);
                html_body_withrightdata = html_body_withrightdata.Replace("@FLAG", "data:image/png;base64," + base64ImageRepresentation);
            }
            html_body_complete = html_body_complete.Replace("@BODY", html_body_withrightdata);



            html_all = html_main.Replace("@BODY", html_body_complete);



            if (output_type == "html")
            {

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory + "/Print/" + file_name + ".html"))
                {
                    file.WriteLine(html_all);
                }
                System.Diagnostics.Process.Start(directory + "/Print/" + file_name + ".html");
            }


            if (output_type == "memory")
            {

                memoryprint = memoryprint + html_all;
            }

        }

        public async void print_final_results(string frame_template_name, string data_emplate_name, string file_name, string what_string, string output_type, string[] visibility)
        {



            string html_main;
            string html_body;
            string html_body_withrightdata;
            string html_all;


            Console.WriteLine("Players_Finalresults.Count" + Players_Finalresults.Count);

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);


            html_main = File.ReadAllText(directory + "/Print_templates/" + frame_template_name + ".html", Encoding.UTF8);


            string tmp_style = File.ReadAllText(directory + "/Print_templates/_style.dat", Encoding.UTF8);
            html_main = html_main.Replace("@STYLE", tmp_style);
            string tmp_zahlavi = File.ReadAllText(directory + "/Print_templates/_zahlavi.dat", Encoding.UTF8);
            html_main = html_main.Replace("@ZAHLAVI", tmp_zahlavi);
            string tmp_hlavicka = File.ReadAllText(directory + "/Print_templates/_hlavicka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@HLAVICKA", tmp_hlavicka);
            string tmp_paticka = File.ReadAllText(directory + "/Print_templates/_paticka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@PATICKA", tmp_paticka);
            string tmp_logo = File.ReadAllText(directory + "/Print_templates/_logo.dat", Encoding.UTF8);
            html_main = html_main.Replace("@LOGO", tmp_logo);

            html_main = html_main.Replace("@CONTESTNAME", BIND_SQL_SOUTEZ_NAZEV + " - " + BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@ORGANISATOR", BIND_SQL_SOUTEZ_CLUB);
            html_main = html_main.Replace("@PLACE", BIND_SQL_SOUTEZ_LOKACE);
            html_main = html_main.Replace("@DATE", BIND_SQL_SOUTEZ_DATUM);
            html_main = html_main.Replace("@WHAT", what_string);
            html_main = html_main.Replace("@CONTESTNUMBER", BIND_SQL_SOUTEZ_SMCRID);
            html_main = html_main.Replace("@CATEGORY", BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@DIRECTOR", BIND_SQL_SOUTEZ_DIRECTOR);
            html_main = html_main.Replace("@HEADJURY", BIND_SQL_SOUTEZ_HEADJURY);
            html_main = html_main.Replace("@SUBJURY", BIND_SQL_SOUTEZ_JURY1 + " | " + BIND_SQL_SOUTEZ_JURY2 + " | " + BIND_SQL_SOUTEZ_JURY3);
            html_main = html_main.Replace("@WEATHER", BIND_SQL_SOUTEZ_POCASI);

            html_body = File.ReadAllText(directory + "/Print_templates/" + data_emplate_name + ".html", Encoding.UTF8);
            string html_body_complete = "";


            html_body_complete = $@"<table>
                <th>Pozice</th>
                <th>Soutěžící</th>
                <th class='visibility_{visibility[0]}'>Stát</th>
                <th class='visibility_{visibility[1]}'>ID</th>
                <th class='visibility_{visibility[2]}'>NAT lic.</th>
                <th class='visibility_{visibility[3]}'>FAI lic.</th>
                <th class='visibility_{visibility[4]}'>AGECAT</th>
                <th class='visibility_{visibility[5]}'>G.Pen</th>
                <th>Celkové score</th>
                <th class='visibility_{visibility[6]}'>Ztráta</th>
                <th class='visibility_{visibility[7]}'>F1</th>
                <th class='visibility_{visibility[8]}'>F2</th>
                <th class='visibility_{visibility[9]}'>F3</th>
                <th class='visibility_{visibility[10]}'>F4</th>
                <th class='visibility_{visibility[11]}'>F5</th>

                @BODY
          </table>";

            html_body_withrightdata = "";

            for (int i = 0; i < Players_Finalresults.Count(); i++)
            {

                html_body = $@"<tr>
    <td>@POSITION</td>
    <td><a href='#USER_@ID'>@USERNAME</a></td>
    <td class='visibility_{visibility[0]}'><img class='vlajka' src='@FLAG' /></td>
    <td class='visibility_{visibility[1]}'>@ID</td>
    <td class='visibility_{visibility[2]}'>@NATLIC</td>
    <td class='visibility_{visibility[3]}'>@FAILIC</td>
    <td class='visibility_{visibility[4]}'>@AGECAT</td>
    <td class='visibility_{visibility[5]}'>@GPEN</td>
    <td>@FINSCO</td>
    <td class='visibility_{visibility[6]}'>@FINLST</td>
    <td class='visibility_{visibility[7]} skrtacka{Players_Finalresults[i].RND1RES_SKRTACKA}'>@F1</td>
    <td class='visibility_{visibility[8]} skrtacka{Players_Finalresults[i].RND2RES_SKRTACKA}'>@F2</td>
    <td class='visibility_{visibility[9]} skrtacka{Players_Finalresults[i].RND3RES_SKRTACKA}'>@F3</td>
    <td class='visibility_{visibility[10]} skrtacka{Players_Finalresults[i].RND4RES_SKRTACKA}'>@F4</td>
    <td class='visibility_{visibility[11]} skrtacka{Players_Finalresults[i].RND5RES_SKRTACKA}'>@F5</td>

</tr>";

                string tabulkaletu = "";




                html_body_withrightdata = html_body_withrightdata + html_body;

                html_body_withrightdata = html_body_withrightdata.Replace("@USERNAME", Players_Finalresults[i].PLAYERDATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@POSITION", Players_Finalresults[i].POSITION.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@ID", Players_Finalresults[i].ID.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@NATLIC", Players_Finalresults[i].NATLIC.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@FAILIC", Players_Finalresults[i].FAILIC.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@AGECAT", Players_Finalresults[i].AGECAT.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@GPEN", Players_Finalresults[i].GPEN.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@F1", Players_Finalresults[i].RND1RES_SCORE + "<br>" + Players_Finalresults[i].RND1RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@F2", Players_Finalresults[i].RND2RES_SCORE + "<br>" + Players_Finalresults[i].RND2RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@F3", Players_Finalresults[i].RND3RES_SCORE + "<br>" + Players_Finalresults[i].RND3RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@F4", Players_Finalresults[i].RND4RES_SCORE + "<br>" + Players_Finalresults[i].RND4RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@F5", Players_Finalresults[i].RND5RES_SCORE + "<br>" + Players_Finalresults[i].RND5RES_DATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@FINSCO", Players_Finalresults[i].PREPSCORE.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@FINLST", Players_Finalresults[i].PREPSCOREDIFF.ToString());




                byte[] imageArray = System.IO.File.ReadAllBytes(Players_Finalresults[i].FLAG);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                Console.WriteLine(base64ImageRepresentation);
                html_body_withrightdata = html_body_withrightdata.Replace("@FLAG", "data:image/png;base64," + base64ImageRepresentation);
            }
            html_body_complete = html_body_complete.Replace("@BODY", html_body_withrightdata);



            html_all = html_main.Replace("@BODY", html_body_complete);



            if (output_type == "html")
            {

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory + "/Print/" + file_name + ".html"))
                {
                    file.WriteLine(html_all);
                }
                System.Diagnostics.Process.Start(directory + "/Print/" + file_name + ".html");
            }


            if (output_type == "memory")
            {

                memoryprint = memoryprint + html_all;
            }

        }

        public async void print_statistics(string frame_template_name, string data_emplate_name, string file_name, string graph_name, string what_string, string output_type, string[] headers, string[] visibility, int to_round)
        {



            string html_all;
            string html_main;
            string html_body;
            string html_body_withrightdata;
            Console.WriteLine(graph_name);
            

            Console.WriteLine("Players.Count" + Players.Count);

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);


            html_main = File.ReadAllText(directory + "/Print_templates/" + frame_template_name + ".html", Encoding.UTF8);


            string tmp_style = File.ReadAllText(directory + "/Print_templates/_style.dat", Encoding.UTF8);
            html_main = html_main.Replace("@STYLE", tmp_style);
            string tmp_zahlavi = File.ReadAllText(directory + "/Print_templates/_zahlavi.dat", Encoding.UTF8);
            html_main = html_main.Replace("@ZAHLAVI", tmp_zahlavi);
            string tmp_hlavicka = File.ReadAllText(directory + "/Print_templates/_hlavicka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@HLAVICKA", tmp_hlavicka);
            string tmp_paticka = File.ReadAllText(directory + "/Print_templates/_paticka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@PATICKA", tmp_paticka);
            string tmp_logo = File.ReadAllText(directory + "/Print_templates/_logo.dat", Encoding.UTF8);
            html_main = html_main.Replace("@LOGO", tmp_logo);



            // html_main = File.ReadAllText(directory + "/Print_templates/" + template_name + "_frame.html", Encoding.UTF8);
            html_main = html_main.Replace("@CONTESTNAME", BIND_SQL_SOUTEZ_NAZEV + " - " + BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@WHAT", what_string);
            html_main = html_main.Replace("@ORGANISATOR", BIND_SQL_SOUTEZ_CLUB);
            html_main = html_main.Replace("@PLACE", BIND_SQL_SOUTEZ_LOKACE);
            html_main = html_main.Replace("@DATE", BIND_SQL_SOUTEZ_DATUM);
            html_main = html_main.Replace("@CONTESTNUMBER", BIND_SQL_SOUTEZ_SMCRID);
            html_main = html_main.Replace("@CATEGORY", BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@DIRECTOR", BIND_SQL_SOUTEZ_DIRECTOR);
            html_main = html_main.Replace("@HEADJURY", BIND_SQL_SOUTEZ_HEADJURY);
            html_main = html_main.Replace("@SUBJURY", BIND_SQL_SOUTEZ_JURY1 + " | " + BIND_SQL_SOUTEZ_JURY2 + " | " + BIND_SQL_SOUTEZ_JURY3);
            html_main = html_main.Replace("@WEATHER", BIND_SQL_SOUTEZ_POCASI);
            //html_body = File.ReadAllText(directory + "/Print_templates/" + template_name + "_data.html", Encoding.UTF8);
            string html_body_complete = "";


            html_body_complete = $@"<table>
                <th>{headers[0]}</th>
                <th>{headers[1]}</th>
                <th>{headers[2]}</th>
                <th>{headers[3]}</th>
                <th>{headers[4]}</th>
    <th class='visibility_{visibility[0]}'>{headers[5]}</td>
    <th class='visibility_{visibility[1]}'>{headers[6]}</td>
    <th class='visibility_{visibility[2]}'>{headers[7]}</td>
    <th class='visibility_{visibility[3]}'>{headers[8]}</td>
    <th class='visibility_{visibility[4]}'>{headers[9]}</td>
    <th class='visibility_{visibility[5]}'>{headers[10]}</td>
    <th class='visibility_{visibility[6]}'>{headers[11]}</td>
    @BODY
          </table>";

            html_body_withrightdata = "";
            Console.WriteLine(Players_statistics.Count());
            for (int i = 0; i < Players_statistics.Count(); i++)
            {




                html_body = $@"<tr>
    <td>@POSITION</td>
    <td>@USERNAME</td>
    <td><img class='vlajka' src='@FLAG' /></td>
    <td>@ID</td>
    <td>@RECORDS</td>
    <td class='visibility_{visibility[0]}'>@DATA_</td>
    <td class='visibility_{visibility[1]}'>@DATASTR_</td>
    <td class='visibility_{visibility[2]}'>@DATA2_</td>
    <td class='visibility_{visibility[3]}'>@DATA2STR_</td>
    <td class='visibility_{visibility[4]}'>@DATA3_</td>
    <td class='visibility_{visibility[5]}'>@DATA3STR_</td>
    <td class='visibility_{visibility[6]}'>@DATA4_</td>
</tr>";


           




                html_body_withrightdata = html_body_withrightdata + html_body;

                html_body_withrightdata = html_body_withrightdata.Replace("@POSITION", Players_statistics[i].POSITION.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@USERNAME", Players_statistics[i].PLAYERDATA);
                html_body_withrightdata = html_body_withrightdata.Replace("@ID", Players_statistics[i].ID.ToString());
                html_body_withrightdata = html_body_withrightdata.Replace("@RECORDS", Players_statistics[i].RECORDS.ToString());


                if (Players_statistics[i].DATA != null)
                { html_body_withrightdata = html_body_withrightdata.Replace("@DATA_", Players_statistics[i].DATA.ToString()); }

                if (Players_statistics[i].DATAstr != null)
                { html_body_withrightdata = html_body_withrightdata.Replace("@DATASTR_", Players_statistics[i].DATAstr.ToString()); }

                if (Players_statistics[i].DATA2 != null)
                { html_body_withrightdata = html_body_withrightdata.Replace("@DATA2_", Players_statistics[i].DATA2.ToString()); }

                if (Players_statistics[i].DATA2str != null)
                { html_body_withrightdata = html_body_withrightdata.Replace("@DATA2STR_", Players_statistics[i].DATA2str.ToString()); }

                if (Players_statistics[i].DATA3 != null)
                { html_body_withrightdata = html_body_withrightdata.Replace("@DATA3_", Players_statistics[i].DATA3.ToString()); }

                if (Players_statistics[i].DATA3str != null)
                { html_body_withrightdata = html_body_withrightdata.Replace("@DATA3STR_", Players_statistics[i].DATA3str.ToString()); }


                if (Players_statistics[i].DATA4 != null)
                { html_body_withrightdata = html_body_withrightdata.Replace("@DATA4_", Players_statistics[i].DATA4.ToString()); }


                byte[] imageArray = System.IO.File.ReadAllBytes(Players_statistics[i].FLAG);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                Console.WriteLine(base64ImageRepresentation);
                html_body_withrightdata = html_body_withrightdata.Replace("@FLAG", "data:image/png;base64," + base64ImageRepresentation);
            }
            html_body_complete = html_body_complete.Replace("@BODY", html_body_withrightdata);


            if (graph_name == "statistics_landing")
            {
                string datadografu = SQL_READSOUTEZDATA_GETALL("select cast(landing as text) || \" bodů \", cast(count(landing) as text) from score where userid > 0 and prep > 0 and rnd <= " + to_round  + " group by landing", ",", ",", 2, "[", "]");
                Console.WriteLine(datadografu);
                html_body_complete = html_body_complete +
                    vytvor_graf_google("Poměr přistání v soutěží", "xnone", datadografu, "ColumnChart", "['xx', 'Počet']");
            }


            if (graph_name == "statistics_flighttime")
            {
                string datadografu = SQL_READSOUTEZDATA_GETALL("select cast('kolo:' || rnd as text), cast (max((minutes*60)+seconds) as text), cast(min((minutes*60)+seconds) as text), cast((sum(((minutes*60)+seconds)) )/(count(rnd))as text) from Score where rnd <= " + to_round  + " and minutes >0 and seconds >0 group by rnd ", ",", ",", 4, "[", "]");
                Console.WriteLine(datadografu);
                html_body_complete = html_body_complete +
                    vytvor_graf_google("Max / min / avg. letové časy", "xnone", datadografu, "AreaChart", "['kolo', 'max', 'min', 'average']") +
                    vytvor_graf_google("Max / min / avg. letové časy", "xnone", datadografu, "ColumnChart", "['kolo', 'max', 'min', 'average']");
            }

            if (graph_name == "statistics_averageheights")
            {
                string datadografu = SQL_READSOUTEZDATA_GETALL("select cast('kolo:' || rnd as text), cast (max(height) as text), cast(min(height) as text), cast((sum(height))/(count(rnd))as text) as prumer from Score where rnd <= " + to_round + " and minutes >0 and seconds >0 group by rnd ", ",", ",", 4, "[", "]");
                Console.WriteLine(datadografu);
                html_body_complete = html_body_complete +
                    vytvor_graf_google("Max / min / avg. výšky", "xnone", datadografu, "AreaChart", "['kolo', 'max', 'min', 'average']") +
                    vytvor_graf_google("Max / min / avg. výšky", "xnone", datadografu, "ColumnChart", "['kolo', 'max', 'min', 'average']");
            }


            if (graph_name == "statistics_maxheightsXXXX")
            {

                html_body_complete = html_body_complete + "<img src='data: image / png; base64,iVBORw0KGgoAAAANSUhEUgAAAgAAAAIACAYAAAD0eNT6AAAACXBIWXMAAAsTAAALEwEAmpwYAAAAIGNIUk0AAHolAACAgwAA + f8AAIDpAAB1MAAA6mAAADqYAAAXb5JfxUYAAB9iSURBVHja7N15kFyFfeDxX19zSjMadB9ICF2AkLBAEohT4jRgE8AugwETp5JNVbyVeDebqiR2nKydw7mP8tqbTW2t1wYTB2J8httgc8lICIzuAyGh + 54Zaa6evvYPA2sDBoHU3dM9n8 +/ NP26f69H79vvvX4vUSqVAgAYXpJGAAACAAAQAACAAAAABAAAIAAAAAEAAAgAAEAAAAACAAAQAACAAAAABAAAIAAAAAEAAAgAAEAAAAACAAAEAAAgAAAAAQAACAAAQAAAAAIAABAAAIAAAAAEAAAgAAAAAQAACAAAQAAAAAIAABAAAIAAAAAEAAAIAABAAAAAAgAAEAAAQC1LGwG1ZMGdt95oCgxFL379m98xBQQAlMGUxedOuf6f / vIvIuJM02CI2TD2zNmPHNywuc8oqBUOAVBrbPzxuQQBAAAIAABAAAAAAgAAEAAAIAAAAAEAAAgAAEAAAAACAAAQAACAAAAABAAAIAAAAAEAAAgAAEAAAAACAAAQAACAAAAABAAAIAAAQAAAAAIAABAAAIAAAAAEAAAgAAAAAQAACAAAQAAAAAIAoFZtMAJqTdoIqBXZ7mOHdj//4v843sd3zJmxINnSdJHJcSJK2dyKI+s3r3i3xx3csLnPtKgliVKpZArUpWVf/uKXW2ZP+5RJcCLyew7e8/Cvfvp2k6DeOAQAAAIAABAAAIAAAAAEAAAgAAAAAQAACAAAQAAAAAIAABAAAEBZuRkQdSt/uGv/yIYzDYITcvBw935TQABADRncf/jV9oYWg+CE7D3YudMUqEcOAQCAAAAABAAAIAAAAAEAAAgAAEAAAAACAAAQAACAAAAABAAAIAAAAAEAAAgAAEAAAAACAAAQAACAAAAAAQAACAAAQAAAAAIAABAAAIAAAAAEAAAgAAAAAQAACAAAQAAAAAIAABAAAIAAAAAEAAAgAAAAAQAAAgAAEAAAgAAAAAQAACAAAAABAAAMcWkjgKFtwSnj4qaJU2rudT90YF88e3CPFQgCAHg/RmQaYnzbmJp73W1dnVYeDGEOAQCAAAAABAAAIAAAAAEAAAgAAEAAAAACAAAQAACAAAAABAAAIAAAAAEAAAgAAOC9Sv/x5//4FWNgKNr54j2/99XvvHy/SQCUIQCuHXnDdGNgKPre6PunmQJAeTgEAAACAAAQAABAXUobAQxtO3uPxcN7tp/cP/xEMq6YONVwQQAAQ9X2nu7Y3tN9Up+zJZ0RADDMOQQAAAIAABAAAIAAAAAEAAAgAAAAAQAACAAAQAAAAAIAABAAAIAAAAAEAAAgAAAAAQAACAAAQAAAAAIAAAQAACAAAAABAAAIAABAAAAAAgAAEAAAgAAAAAQAACAAAIByS//f5bfdfjwPbJn3iU/dkLz6IiPjROQSg/Gtn/76cX3mnltz6NEvGhlAeQLgf923+Z7jeeAf/sbFCwYnXSEAOLEAKBbieD9zAJSPQwAAIAAAAAEAAAgAAEAAAAACAAAQAACAAAAABAAAIAAAAAEAAJRV+ngfuPfA3uXNc1ImxgnZ1L1uf8RSgwColQDoyxZ6MikBwIk5lNnfZwoA1ecQAAAIAABAAAAAAgAAEAAAgAAAAAQAACAAAAABAAAIAABAAAAAAgAAEAAAgAAAAN6rtBEAlMcdc2d8eOJNt/+ZSSAAAIaR8a1Np+694Mr5JsFQ5BAAAAgAAEAAAAACAAAQAACAAAAABAAAIAAAAAEAAAgAAEAAAAACAAAQAACAAAAABAAAIAAAAAEAAAgAABAAAIAAAAAEAAAgAAAAAQAACAAAQAAAAENK2ghOjlwqF4XmXEREpPozkSlkDAUAAVDLG/bO8QfjWEdndDSNjjPaz4pRbR3RPKIp0i3pyDSnI5FKvOX/KxVLURwsxmBvLno7+2L3kV2xsWddJDvTMX73lMjkBQIAAmBI2TVtW4wcOyIWTbkgRk1ui1Rj6j0/RyKZiFRTKpqbUtE8uinGxClxTsyPiIhirhjH9vbGD7c+HC072qP9cIehAyAAqmHH1K0x89RZcfaZZ8eStoVlXVYyk4z2qSPj5qkfjVKpFH37+uO76++PqRtmRmrQKgFAAJRVLpWLbWdujJvnfiyWTF5YldeQSCSidWJL3DbxjshfmI+VL66Mrk3HYlTnKT6dAAiAcmz4P3bux+PSMUuGzspoTseSC5dEcVExnlzxZBTXJKK5r9WnFAABcKIOTNoTSy9aFpeeumTIvsZkJhlLL1oauXPzcd+z34wpa2f4VQEn1UAhH/+4flV5/9YG+gwaBMAQ+NafzkXu3L64YcmH3/as/aEo05yO2664IzpP747nnlzhZEFOmmKpFBu6DxsECID6tn/S7rjqsquidWJLTb7+juntceWky+OeJ++KWWvO9qmFGvHo9r3fXPiZXz94PI898sdfubeUShkaJ+TWu//ykUe3bPvfAiAiNp+1Ju5Yduf7+infkCq1xlTcedUnY/OYLbHvqUOuIwA1YPWBI0ci4r7jeex19/wgCo1NhsYJ2b5z14r/s3rLcX3m6vZSwLlULuKCXNx5zSdrfuP/82YvmBXn3Hh2DLQ6vgrA+1eXAZDP5GPW1dNjyYVLIpFI1N37a586Ms6/aVFkR/b7BAMgACIiBkb0xYKPzIuJZ06o6xXXMq45Ft+4MLo6jvgUAzC8A2BgRF8svmFhjJg0PH473zK2OS67/pIoNBZ8kgEYngGQHdkf59+0KFontAyrFdgyrjlmXD7tZ+c8AMBwCoC+lt5Y9OHzomVs87BciRPPnBB9C7p8mgEYPgGQS+fiA1fPH3bf/N/smos/GLumbfOJBmB4BMCES8bE6NNdIS+RTMR1F17vUAAA9R8AOz6wJWYvmGUtvqZ1Ykvsn7/DIACo3wDYNnNzfGzprdbgm/zKopsjn8kbBAD1FwADrX3xkaUfjUQyYQ2+ScOITOyZ61wAAN5ZTd4L4KylZ0RjW0PVX0cxV4zuXcfimR1PxuHc4UhkE5EupKOQLkRDUyaWjL04Js2cGOnmyo75xvM+Es+tWeUWwlBDZmxd8/KB+UtmmgQnYt2x/hV1GwDbztkYS+YsrOpr6N3fF99afW+cvumMSA2mY3RMjNEx8S2P2x3749Uf7Y5tZ26KWxZ/vGLR0tjeENtnbo5Zm+b6a4Aa0ZTt78ukkgbBCekrFI/VZQB0j+6MWy7+eNWWP9CZjfuW/1vM3HhWzIrjuy1vOpeOWavnxspNL0bq3GJccP4FFTl0ccGkC+Pwpm5/DQC8rZrKzQUXnFOVO/uVSqXY/OKWeP6en8bMjWe9r+dIZVMRyzNx90N3RT5b/kv3Tj9jeuTSfhIIQI0HQPfozmgf317x5Rayhbj7wbvi8BPdP9uIn6CZG8+K7/zH/VHMF8v6ujPN6dh12is+4QDUdgC0H+6IFV97Ib72yFdjoDNbkWUO9uTi/h/c/76/9f8yk7dPjx8uf6zsr7+lrcUnHIDaDoCIiEwhE7PXzosVd70Qd//w6zHQVb4QGOjMxg/veyKmvDq9LM/f9NO2sofMkokX+YQDUPsB8EYI5DMx46WzYsXXyxMC2aOD8aPvPRmjOk8p23tI59Jx76pvlnVOHZNdIhmAOgqAcoZArjcXDz7wYLQfLv/G87T1syN7dLB882lNR6HBVQEBqLMAeEsInOA5AqViKe554hsxfs/kir3uLZu2lO35E4lEHB5zwKccgPoMgDc2qK+fI/D19xcC//rEN2L25rMr+ppXda4o6/MfaT/oUw5AfQfAW0LgPZwsuHblupj+0hkVf60jD44q6/M3NjT6lAMwPALgjRA4zkMD/YcHouu5Y1V5je3HTinvDJLuBwDAMAuAt+wReJtDA6VCKb7/xPciNVidqyK39LVGqVAq2/M3J10LAIC3Sg+nN/tGCGx8IXbM3RIfPe+WeOinD8SpO06v6usqFUuRSJXn/gDNKQEAwDAPgDdC4PVDA+teiFNKE6r6WgoN+UhmyrcjJhXuLgbAWw3rrUMmn4lMobrHyHtGlvfcg6N5dwQEQAAMOQfG7Cnr8/cX+w0ZAAEw1EwdNa2sz58tZA0ZAAEw1Jx32qLyLmAwYcgACIChZN/EXTFiYnnP0h93aJJBAyAAhpK5Z8yNRLJ839Dz2UK0dI0waAAEwFDRfUpnzJw3o6zLyB7JVv1XDgAIAH7OBy6YH8l0ece/b/8+gwZAAAwVW+atjQlnjC/7cp459JRhAyAAhoKujiPx8YvvqMiyxu12AiAAAqDqculcXHjFksg0l/8KzL37+qL9cIehAyAAqm30hW3RPnVkRZb16MaHDRwAAVBtW89ZH3MXzq3IskqFUjRuazV0AARANW2Zsy5uX/qJii1v5/pdMarzFIMHQABUy86pr8RtV94RiVRlLslbKpVi+aZnDR4AAVAt3aM741euvjHSjamKLfPVNTti6o4Zhg+AAKiGgRF9cdmHLonGtoaKLTOfLcTGFzcbPgACoBoKDflYeP150Ty6qaLLvffpf/XTPwAEQDXkUrmYeeX0GDm5smfhd23vjilr7foHQABURfuS1opc5vfn5bOF+MmPV7jxDwACoBoOn7c3zlk8v+LLtesfAAFQJVvmrY3rL/1QxZe7Y+2umP7SGVYAAAKg4hv/OeviE5f/aiQSiYout//QQOz40S4rAAABUPFv4FO3xu0VvNDP6wqDhXjksUciNZi2EgAQAJXUPbozbrrm5khV8EI/r7v7ybti3B63+wVAAFTU6xf6aRhZ+TPvX3juhZi9+mwrAYD3zf7j96HQkI9FVbjQT0TE4Vc6o+cn2ciEn/wBlZFJJKI1mTCIN+kqFAXAcJLP5OOsa+fEiMmVv93uwJFsrH50TTQVWqwIoGJmtjTEZ5edbxBvcscPnqzp1+8QwHuQS+dizjUzY/SMyv/mPt+fj4ceeSiaem38ARAAldv4p3Ix44ppMW72mIovu1QsxT2PfyPG75lsRQAgACq58T/tilNj8tzqnHV/74++GbM2zbUiABAAldz4j724I6aePaUqy1++fHlM/eksKwIAAVDJjf8pF7bFmedV51K7h14+ErkV1gMAAqCiMosizl5UnV3vvfv6YuPDW9zhDwABUEnZ83tiyYVLqrPso4Px5INPRyqbsiIAEACV0rPoSCy9aGlVlp3PFuI7j3w7RnWeYkUAIAAq5eC5u+KqS66uyrKL+WLc89jdMXXHDCsCAAFQKXsWbIsblt5YlWWXiqW46/Gv+bkfAAKgkradszFuXvrR6mz8S6W464dfi9lr51kRAAiAStkyf118/PLbI5Go/M0uSqVS/Ovj34hZa9zdDwABUDGbz14Tn7j8zqps/CMivvvj78T0l87wSQRAAFTK1rnr4s4rPxmJKt3m8qEnH4zxL5zqUwiAAKiUl89YH7dfeWfVNv4vPPdCtD8/1icQAAFQKVvmrIvbr74jEqnqbPzXrlwX2WeKPn0ACIBK2TZzc9xx9Scima7OW9+xZmccefaoTx4AAqByG/9NccsHb4lkpjpve/+mA7H9iV2u7w9Q4555arkAqBW7pm2LW66+NVIN1bm+fte27tj8yCuRydv4A9T0l8nNB+Ofu3ICoBbsmLo1brrupkg1VWfjf2x3b6x9YGOkc2l/OQA17MDuo/H5TRuilKj991L3AXBg0p64+dqbI91cnY1v796+WPH9593ZD6DGdR3uj8++tDryifp4P3UdAPsn7Y5rP3RtZFqrs9u972B/rPj+89HS1+ovB6hZCSOIgb5cfG7Fqugv1s8vuOo2ALpHd8a1110bDSOqs/Ef6MzGj3/wVDT1tPjLAahh+Vwx/uSZ56OzUF8/367Lg9Ldoztj2Y2XRWNbQ1WWnz06GA888EBM7HSVP6D27RnIxT88tXLIvJ4rm0fFvIWzKrKsUrEUf/70ytidzdXdeq27AOjqOBJLP3RpNLZXZ+Of683FQw88FBP32/gDdfLvaqEYq7r7h8RrGZFKxm+eM61iy/vSMytjS2+2LtdrXR0CyI7sj6U3XBrNo5uqs/Hvz8e3HvhWjNszyb8YAGX4xvqF+fOjtUJ7d+974uFY0T1Qt/OsmwAYaO2Lxb+ysGob/3y2EN998NsxbedMf6UAJ1sp4jPTZsa4yW0VWdzmNXviuz3NdT3SugiA1zf+LeOqs7KKuWLc9/C/xeTt0/2RApTB9e2FmD2vMntX9+7ojr949eW6//lDzQdAf0tvLL5hYbROqM7Z9qVCKb722FfjtJdn+wsFKIM5rY1x6yVLK7KszoN98UdrVkd+GMy1pk8CLDQWYtGHF0brxOr91O7x5Y/HyP622H3atpqYWWaw0TkKQM0Yn0nFH1y0MBKJ8n8d7z02GJ9d+UJkS6VhMduaDYBCQz7mfXhujJxc3YvsXHHxFTU1t959fbH6nvX+VQGGvBGpZHxhyaLIVOAeLoPZfHzu2ZVxtDh8btVek4cA8pl8nHntnGifOtJfCECdfjut1Bn/xWIp/uzplXEgVxhWM665AMilczHnmpkxekaHvxCAelTBM/5LpVL87ZMr45X+3LAbc80FwIEpu2Lc7DH+QADqVCXP+L/72eWxumdgWM7Z/WkBGDJ+dsb/4oosa82qrfFwZ37Yzjrp4wbAUFDJM/5ffflw/M3e3cN63gIAgKqr5Bn/h/b1xn/fuD6Kw3zmAgCAqqrkGf9Huwbisy++GLkoDfu5CwAAqqeCZ/wP9Ofjcz9ZFb2ForkLAACqqVJn/BfyxfjCMyvjcL5g6K/xKwAAqqJSZ/yXSqX4y6dWxo6BnKELAACqqZJn/P/LU8tjQ2/e0AUAANVUyTP+H3/i8Xiq16bu7TgHAICKfuus2Bn/nQPx/WyjDZ09AABUVSniM6dV5oz/iIi2jqb4hw9eFMVCKboO98dL65+LHxebY3vvYOQTVocAAKAirm3PV+wa/z8vmUrEKeNaYtm4ZbEsfvaLgK0b9sXdB16NV/pyEcM0BgQAAGV3Rmtj3HbJJUPitaTSyZg9b1J8ISbF0a6B+Orq1fHC0YEYbj8QdGgEgLKa0JCK36/QGf/vVduopvj0pYvjK5deEOe2NcdwukCgAACgbEakkvH5Cypzxv+JaG1riN+9dFH846LzYlLD8Ng5LgAAKJs5Ixorcsb/yTJmQmv81ZVL4vq2Qt3vDRAAAPBzEslEfPzSZfHX8+bHiFT9biYFAAC8jUmnjYq/WbIwRmdSdfn+au5Ax4Q9p8a/33+fT+b7lBlsjHExySAAjsPIUU3xV5eeH3/y9MrYna2vewnUXACkBtMxeft0n0oAKqKpOR1/esmi+MMfPxf7c/XzY0GHAADgXTQ0pePPL1kco9P1czhAAADA8ewJaMnEn55/XjQn62PTKQAA4Di1dTTF5886OxJ18BNBAQAA78Gk00bFB9vzAgAAhpvbLl4WE2r854ECAADeo0QyEZ9ZsCCSNXwoQAAAwPtwyriWWDiqSQAAwHDzGws+EOka3QsgAADgfWoZ0RAXd2QEAACcLNmBfBSLQ//r9a3zzq3JOwemfcQAqJaBvlzs3dkVq3eujPWp0XFwMB89hVL0FYoRidceVIpoSiYik0hEQzIR7ZlkdDSk48rmUTF56uToGNsciUSiau9hRHtjzGxtiJf7BgUAAPyyDf6OrQfjwc5dsb0/FwffuLb+iIjI/v8H/vz2PBExUCrFQKkUUYw4nC9E9OdiVXd/xL69MTKZjHltmbjtjPkxakxzVd7X7eOnxee3bREAAPC6o50D8fxPn43HCs2xsz8XpZP8Zf1YsRjPdmXj2eUrY3ZrY3z6nPnRPrqyITBtxthIvrIlionaWS8CAICyeam7Pz71zIrXNje5X/xmf7IlIjb3ZeO3l6+Ma9vyccvFyyKZrMwWuaEpHVOaMrGjhm4Z7CRAAMqmGhfMLUbEfxxNxx88tjx6urMVW+68xoGaWjcCAIC6tGcwH//t2ZXRdai/Iss7f9zZAgAAhoLeQjE+s3JVDPSXf1/E2AkjBQAADBVHC8X4u5+sKvtyRrQ11tRtggUAAHVvQ2829mzvKusyEslEjEjVzmZVAAAwLPzz1g1lX8bItAAAgCHllb5c9Bwt79X6MsnauRCAAABgeEhEbN6wWQAIAACGm8f6+8r6/OmEAACAIefIG/ceKI9CDc1CAAAwbAwUi2V9/myhWDOzEAAADBvl/p1+TgAAwNDTUuaf6Q26EBAA9erM1sb4k+mzojlZe5uQyU3lfc099gAAUI8SpYj/PG9+zJo7Mf7p0sUxu6Wxpl7/VR1Ty/bcA325yJZqZxeAAADguJ3T1hyjxjRHRETLiIb43LLF8fsTJ0djDfz8LV2KmDZzbNme/2hnf02tSwEAwHFJRcRvnjPvF/cIJBIx77wZ8ZXLl8RFHU0RQ/gL8PxRzdHQmC7b8x85dEwAAFB/LuhoirZRTW/73xqb0/FbFy2Ov/3AOTE2nRp63/4j4jfOnlfWZTx1bIcAAKC+ZCIRv7Zgwbs+bsKp7fH3V18YfzjptBg5hE4SvK61P9o6msq6jM29xZpapwIAgHd1dVsumloyx/XYRDIRc8+dGl+66sL4WFNP1c8POL05Ex+97OqyLmNwIB/7Bgs1tU4FAADvqDmZjI8svvQ9/3/pTDJuuPK6+J9XLIlfa8lVZY/AaY2Z+NwliyNZ5pv07NnZFaVEba3XtI82AO/k1qZsNDS9/81FQ1M6rrj8ilhWKMWG1dviqwf2xr4yX5M/HRFXdaTitiUXRKICd+i7f9+2mluvAgCAX2pkMhmXXbLspDxXMpWIuQtOj7+N06PzYF/8+6aXYtWxwkm9eE4yIua3NcdvfeCcaG1rqMiMCvlirDs6IAAAqB+fGjc+0pmTv+u+Y2xL/KexS+LXi6U4tPdY/Gjz8ngh3xZ7srl4rzmQLEVMbc7ELaPGxKyzTjvucxVOllc27a+pCwAJAADeeSOdSsbcc2eWdRnJZCLGTW6Lj02+Jj722rfprkP9cehAd7zQtSkORGv0FkqRLZYinUxEYyIRTamIsaXeOK9jTowZPypGjW6OZKp6B+C/sf/Vmly/AgCAt/XbU04v+8lzb5ZKJ2P0hNYYPaE15sSkIT+jw/t74+XewYhE7a1fvwIA4C3GplMx6+yJBvEuvrJ+bU1u/AUAAG/r06fPjkQiYRDv4MiBvtjUk63Z1y8AAPgFkxrSMW3WGIN4B6VSKf7+pZdq9tu/AADgLf7rnLN8+38Xa1e9HNuzuZp+DwIAgDfMaG6IidNGGcQ76OsZjH/cv6/m34cAAOBnShG/PXeeObyDYrEUf7Z8VU3+7l8AAPC2zhzRGGMmtBrEO/jS0ytjR43v+hcAALxlgzA4kDeIX+KlFZtjZQ1e8lcAAPCO1vVm43ee+ElsWr3HMN5k85o98XcH9tXVexIAALyhp1CMP93xcnz2seXRfbjfQCJi9fMvxxe2vxzFOntfAgCAt3h1IBeffnZlfPPJJyI3WBiWMygWS3HfEw/HX+/dU9O/9/9l3AsAgLeVT0T84GgqfvjY8vhww9G4fukHI5UeHt8bj3UNxBeffzF2DDTX5cbfHgAA3lV/sRj3DoyI//Los7Fl3d4o1cFP4H6ZUqkUa57fEr/z9MrYMZCr6/VqDwAAx6WzUIzPb9sSHTu2xm0j07Ho/MWRztTP98iDe3vi79auiV3Z3LBYnwIAgPccAl/uGowRjz4b1zUei2suuioam2p3c3K0cyC+8tLqWHtsoG539wsAAE6anmIx7u1vjW8/tjzOHNEYd54+J8af2lYz9xE4vL83/mX92ljfk41SIobVxl8AAHDCclGK1T0D8XurX4r2tcm4rLk3rl1wWYwc1TTkXms+V4xXNu6Luw+8Gq/0v7arf5je90gAAHDSdBeL8b3e5vje0ytibDoV80am4topZ8T4U9sjmazOljafK8benV3xb7u2xvpjAzFYxycxCgAAqu5gvhCPdxbi8c7V0bg2EdOaG2J+4VCcN2NJjJvUVrbzBgaz+eg82BfPbXomVhRHxa5sLlzgWAAAUAXZUik292Vjc4yMf1+/NmJdxCmZVIxtTMe4VCHmJRMxcezMaO9ojYbGVGQaU5HJpCKZ+sW9BsViKXKDhcjnitF3LBs9PQNxtKsz1uYOxiuD6dg/kIuufPG13fqtEZEzfAEAwJCRiDiSL8SRfCE2RcRTERFdG976uFJEUzIRhYjIveuu+8Ibz40AAKDGQ2HAMfuycCVAABAAAIAAAAAEAAAgAAAAAQAACAAAQAAAAAIAABAAAIAAAAAEAAAgAAAAAQAACAAAQAAAAAIAABAAACAAAAABAAAIAABAAAAAAgAAEAAAgAAAAAQAACAAAAABAAAIAABAAAAAAgAAEAAAgAAAAAQAACAAAEAAAAACAAAQAACAAAAABAAAIAAAAAEAAAgAAEAAAAACAAAQAACAAAAABAAAIAAAAAEAAAgAAEAAAMBwlj7eB67e3Pn0d6d88neNjBOx71D/qxFfqMiydi5f+dDgsZ6bTJ0TcXjL1hcrsZy93/7GH417+Funmjgn4geHun5yvI9NlEolEwOAYcYhAAAQAACAAAAABAAAIAAAAAEAAAgAAEAAAAACAAAQAACAAAAABAAAIAAAAAEAAAgAAEAAAAACAAAQAAAgAAAAAQAACAAAQAAAAAIAABAAAIAAAAAEAAAgAAAAAQAACAAAQAAAAAIAABAAAIAAAAAEAAAgAABAAAAAAgAAEAAAgAAAAGra/xsAjvcFtVIJ/tcAAAAASUVORK5CYII=' />";

            }





            // vytvor_graf_chart("scatter") +
            //                vytvor_graf_google_line() +





            html_all = html_main.Replace("@BODY", html_body_complete);



            if (output_type == "html")
            {

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory + "/Print/" + file_name + ".html"))
                {
                    file.WriteLine(html_all);
                }
                System.Diagnostics.Process.Start(directory + "/Print/" + file_name + ".html");
            }


            if (output_type == "memory")
            {
                
                memoryprint = memoryprint + html_all;
            }



        }
        public async void print_matrix(string frame_template_name, string data_emplate_name, string file_name, string what_string, string output_type)
        {



            string html_all;
            string html_main;
            string html_body;
            string html_body_withrightdata;


            Console.WriteLine("Players.Count" + Players.Count);

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);


            html_main = File.ReadAllText(directory + "/Print_templates/" + frame_template_name + ".html", Encoding.UTF8);


            string tmp_style = File.ReadAllText(directory + "/Print_templates/_style.dat", Encoding.UTF8);
            html_main = html_main.Replace("@STYLE", tmp_style);
            string tmp_zahlavi = File.ReadAllText(directory + "/Print_templates/_zahlavi.dat", Encoding.UTF8);
            html_main = html_main.Replace("@ZAHLAVI", tmp_zahlavi);
            string tmp_hlavicka = File.ReadAllText(directory + "/Print_templates/_hlavicka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@HLAVICKA", tmp_hlavicka);
            string tmp_paticka = File.ReadAllText(directory + "/Print_templates/_paticka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@PATICKA", tmp_paticka);
            string tmp_logo = File.ReadAllText(directory + "/Print_templates/_logo.dat", Encoding.UTF8);
            html_main = html_main.Replace("@LOGO", tmp_logo);



            // html_main = File.ReadAllText(directory + "/Print_templates/" + template_name + "_frame.html", Encoding.UTF8);
            html_main = html_main.Replace("@CONTESTNAME", BIND_SQL_SOUTEZ_NAZEV + " - " + BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@WHAT", what_string);
            html_main = html_main.Replace("@ORGANISATOR", BIND_SQL_SOUTEZ_CLUB);
            html_main = html_main.Replace("@PLACE", BIND_SQL_SOUTEZ_LOKACE);
            html_main = html_main.Replace("@DATE", BIND_SQL_SOUTEZ_DATUM);
            html_main = html_main.Replace("@CONTESTNUMBER", BIND_SQL_SOUTEZ_SMCRID);
            html_main = html_main.Replace("@CATEGORY", BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@DIRECTOR", BIND_SQL_SOUTEZ_DIRECTOR);
            html_main = html_main.Replace("@HEADJURY", BIND_SQL_SOUTEZ_HEADJURY);
            html_main = html_main.Replace("@SUBJURY", BIND_SQL_SOUTEZ_JURY1 + " | " + BIND_SQL_SOUTEZ_JURY2 + " | " + BIND_SQL_SOUTEZ_JURY3);
            html_main = html_main.Replace("@WEATHER", BIND_SQL_SOUTEZ_POCASI);
            //html_body = File.ReadAllText(directory + "/Print_templates/" + template_name + "_data.html", Encoding.UTF8);
            string html_body_complete = "";
            html_body_complete = File.ReadAllText(directory + "/Print_templates/" + data_emplate_name + ".html", Encoding.UTF8);

            string tabulkaletu = "";

            for (int x = 1; x < BIND_SQL_SOUTEZ_ROUNDS + 1; x++)
            {
                tabulkaletu = tabulkaletu + $@"
                    {Lang.txt_round} : {x}
                    <table>
                        <tbody>
                            <tr>
                                <th></th>";

                for (int s = 1; s < BIND_SQL_SOUTEZ_STARTPOINTS + 1; s++)
                {
                    tabulkaletu = tabulkaletu + "<th>" + Lang.txt_startpoint + ":" + s + " </th>";

                }

                tabulkaletu = tabulkaletu + "</tr>";

                for (int g = 1; g < BIND_SQL_SOUTEZ_GROUPS + 1; g++)
                {
                    
                    tabulkaletu = tabulkaletu + "<tr><td class='gray'>" + Lang.txt_group+ ": " + g + "</td>";
                    for (int stp = 1; stp < BIND_SQL_SOUTEZ_STARTPOINTS + 1; stp++)
                    {
                        string kdo = SQL_READSOUTEZDATA("select Lastname || ' ' || Firstname from matrix M left join users U on M.user = U.ID where rnd=" + x.ToString() + " and grp=" + g.ToString() + " and stp=" + stp.ToString() + " ; ", "");
                        tabulkaletu = tabulkaletu + "<td>" + kdo + "</td>";

                    }
                }


                tabulkaletu = tabulkaletu + "</tr></tbody></table>";
            }






            html_body_complete = html_body_complete.Replace("@DATA", tabulkaletu);


            html_all = html_main.Replace("@BODY", html_body_complete);



            if (output_type == "html")
            {

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory + "/Print/" + file_name + ".html"))
                {
                    file.WriteLine(html_all);
                }
                System.Diagnostics.Process.Start(directory + "/Print/" + file_name + ".html");
            }


            if (output_type == "memory")
            {

                memoryprint = memoryprint + html_all;
            }



        }

        public void print_memory_to_file(string frame_template_name, string data_emplate_name, string file_name, string what_string, string output_type, Boolean openfile)
        {





            string html_all;
            string html_main;
            string html_body;
            string html_body_withrightdata;


            Console.WriteLine("Players.Count" + Players.Count);

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);



            html_main = File.ReadAllText(directory + "/Print_templates/" + frame_template_name + ".html", Encoding.UTF8);


            string tmp_style = File.ReadAllText(directory + "/Print_templates/_style.dat", Encoding.UTF8);
            html_main = html_main.Replace("@STYLE", tmp_style);
            string tmp_zahlavi = File.ReadAllText(directory + "/Print_templates/_zahlavi.dat", Encoding.UTF8);
            html_main = html_main.Replace("@ZAHLAVI", tmp_zahlavi);
            string tmp_hlavicka = File.ReadAllText(directory + "/Print_templates/_hlavicka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@HLAVICKA", tmp_hlavicka);
            string tmp_paticka = File.ReadAllText(directory + "/Print_templates/_paticka.dat", Encoding.UTF8);
            html_main = html_main.Replace("@PATICKA", tmp_paticka);
            string tmp_logo = File.ReadAllText(directory + "/Print_templates/_logo.dat", Encoding.UTF8);
            html_main = html_main.Replace("@LOGO", tmp_logo);
            html_main = html_main.Replace("@WHAT", what_string);



            // html_main = File.ReadAllText(directory + "/Print_templates/" + template_name + "_frame.html", Encoding.UTF8);
            html_main = html_main.Replace("@CONTESTNAME", BIND_SQL_SOUTEZ_NAZEV + " - " + BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@ORGANISATOR", BIND_SQL_SOUTEZ_CLUB);
            html_main = html_main.Replace("@PLACE", BIND_SQL_SOUTEZ_LOKACE);
            html_main = html_main.Replace("@DATE", BIND_SQL_SOUTEZ_DATUM);
            html_main = html_main.Replace("@CONTESTNUMBER", BIND_SQL_SOUTEZ_SMCRID);
            html_main = html_main.Replace("@CATEGORY", BIND_SQL_SOUTEZ_KATEGORIE);
            html_main = html_main.Replace("@DIRECTOR", BIND_SQL_SOUTEZ_DIRECTOR);
            html_main = html_main.Replace("@HEADJURY", BIND_SQL_SOUTEZ_HEADJURY);
            html_main = html_main.Replace("@SUBJURY", BIND_SQL_SOUTEZ_JURY1 + " | " + BIND_SQL_SOUTEZ_JURY2 + " | " + BIND_SQL_SOUTEZ_JURY3);
            html_main = html_main.Replace("@WEATHER", BIND_SQL_SOUTEZ_POCASI);

            html_body = File.ReadAllText(directory + "/Print_templates/" + data_emplate_name + ".html", Encoding.UTF8);






            html_all = html_main.Replace("@BODY", memoryprint);



            if (output_type == "html") {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory + "/Print/" + file_name + "." + output_type))
                {
                    file.WriteLine(html_all);
                }
            }

            if (output_type == "pdf")
            {

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory + "/pdf/" + file_name + ".html"))
                {
                    file.WriteLine(html_all);
                }


            System.Diagnostics.Process installProcess = new System.Diagnostics.Process();
            //settings up parameters for the install process
            installProcess.StartInfo.FileName = "pdf\\wkhtmltopdf.exe";
            installProcess.StartInfo.Arguments = " -O Landscape -L 0 -R 0 -T 0 -B 0 " + directory + "/pdf/" + file_name + ".html " + directory + "/pdf/v" + BIND_SQL_SOUTEZ_SMCRID + ".pdf";

            installProcess.Start();

            installProcess.WaitForExit();
                // Check for sucessful completion
            Console.WriteLine("PDF vytvořeno ");

            }


            if (output_type == "mpdf")
            {

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory + "/pdf/" + file_name + ".html"))
                {
                    file.WriteLine(html_all);
                }


                System.Diagnostics.Process installProcess = new System.Diagnostics.Process();
                //settings up parameters for the install process
                installProcess.StartInfo.FileName = "pdf\\wkhtmltopdf.exe";
                installProcess.StartInfo.Arguments = " -O Landscape -L 0 -R 0 -T 0 -B 0 " + directory + "/pdf/" + file_name + ".html " + directory + "/pdf/mv" + BIND_SQL_SOUTEZ_SMCRID + ".pdf";

                installProcess.Start();

                installProcess.WaitForExit();
                // Check for sucessful completion
                Console.WriteLine("PDF vytvořeno ");

            }





            if (openfile == true)
            {

                System.Diagnostics.Process.Start(directory + "/Print/" + file_name + "."+ output_type);
            }

            memoryprint = "";

        }

        private string vytvor_graf_chart(string typgrafu)
        {
            string graf = $@"
<canvas id='myChart1' style='width:100%;max-width:700px'></canvas>


<script>
    var xValues = [50,60,70,80,90,100,110,120,130,140,150];
    var yValues = [7,8,8,9,9,9,10,11,14,14,15];
    
    new Chart('myChart1', {{
      type: 'line',
      data: {{
        labels: xValues,
        datasets: [{{
          fill: false,
          lineTension: 0,
          backgroundColor: 'rgba(0,0,255,1.0)',
          borderColor: 'rgba(0,0,255,0.1)',
          data: yValues
        }}]
      }},
      options: {{
        legend: {{display: false}},
        scales: {{
          yAxes: [{{ticks: {{min: 6, max:16}}}}],
        }}
      }}
    }});
    </script>
<br><br><hr><br><br>
";






            return graf;
        }





        private string vytvor_graf_google_line()
        {
            string graf = $@"
<div id='myChart2' style='width:100%; height:500px;'></div>

<script>
google.charts.load('current',{{packages:['corechart']}});
google.charts.setOnLoadCallback(drawChart);

function drawChart() {{
// Set Data
var data = google.visualization.arrayToDataTable([
 ['Category', 'Amount', 'prumer', '200', 'max'],
  [1,20,30,200,358],
  [2,14,18,200,358],
  [3,15,80,200,358]
],false);



// Set Options
var options = {{
  title: 'House Prices vs. Size',
  hAxis: {{title: 'kolo'}},
  vAxis: {{title: 'výška v metrech'}},
  legend: 'none'
}};
// Draw
var chart = new google.visualization.LineChart(document.getElementById('myChart2'));
chart.draw(data, options);
}}
</script>
<br><br><hr><br><br>

";






            return graf;
        }


        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }



        private string vytvor_graf_google(string nazev_grafu, string zobrazit_legendu, string datadografu, string typgrafu, string sloupce)
        {

            int divrandom = GetRandomNumber(1, 100000);

            string graf = $@"
<div id='{divrandom}' style='width:100%; height:500px;'></div>

<script>
google.charts.load('current',{{packages:['corechart']}});
google.charts.setOnLoadCallback(drawChart);

function drawChart() {{
// Set Data
 var data = google.visualization.arrayToDataTable([
          {sloupce},
        {datadografu}
        ]);

 var options = {{
        legend: '{zobrazit_legendu}',
        pieSliceText: 'label',
pieHole: 0.5,
        title: '{nazev_grafu}',
        pieStartAngle: 100,
      }};


// Draw

 var chart = new google.visualization.{typgrafu}(document.getElementById('{divrandom}'));
chart.draw(data, options);
}}
</script>
<br><br><hr><br><br>

";






            return graf;
        }


  #endregion  
    
    }





}
