using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;

namespace WpfApp6.Model
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    /// 
    public class ViewModel : INotifyPropertyChanged
    {
        SQLiteConnection DBSORG_Connection;
        SQLiteConnection DBSOUTEZ_Connection;
        string[] barva = new string[] { "Red", "Green", "Blue", "Purple", "Orange", "Lime", "Emerald", "Teal", "Cyan", "Cobalt", "Indigo", "Violet", "Pink", "Magenta", "Crimson", "Amber", "Yellow", "Brown", "Olive", "Steel", "Mauve", "Taupe", "Sienna" };
        string[] pozadi = new string[] { "Light", "Dark" };
        int pouzitabarva = 1;
        int pouzitepozadi = 1;
        bool bindingMENU_online_value = true;
        bool bindingMENU_finale_value = true;
        bool bindingMENU_detailyastatistiky_value = true;


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
        public int BIND_SQL_SOUTEZ_STARTPOINTS_value;
        public int BIND_SQL_SOUTEZ_DELETES_value;
        public int BIND_SQL_SOUTEZ_ROUNDSFINALE_value;
        public int BIND_SQL_SOUTEZ_STARTPOINTSFINALE_value;
        public int BIND_SQL_SOUTEZ_DELETESFINALE_value;


        public ViewModel()
        {
            CreateTestDataForPlayers();
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

        public bool bindingMENU_finale
        {
            get { return bindingMENU_finale_value; }
            set { bindingMENU_finale_value = value; OnPropertyChanged("bindingMENU_finale"); }
        }

        public bool bindingMENU_detailyastatistiky
        {
            get { return bindingMENU_detailyastatistiky_value; }
            set { bindingMENU_detailyastatistiky_value = value; OnPropertyChanged("bindingMENU_detailyastatistiky"); }
        }

        public bool bindingMENU_online {
            get { return bindingMENU_online_value; }
            set { bindingMENU_online_value = value; OnPropertyChanged("bindingMENU_online"); }
        }




        public int Function_global_changeforeground
        {
            get { return pouzitabarva; }
            set {pouzitabarva = value;zmenbarvupopredi();}
        }
        public int Function_global_changebackground
        {
            get { return pouzitepozadi; }
            set { pouzitepozadi = value; zmenbarvupozadi(); }
        }

        #endregion

        #region BINDING_Nastavení

        public void nactisoutez()
        {
            Console.WriteLine("asdasd");
            BIND_SQL_SOUTEZ_NAZEV= SQL_READSOUTEZDATA("select value from contest where item='Name'", "");
            BIND_SQL_SOUTEZ_KATEGORIE= SQL_READSOUTEZDATA("select value from contest where item='Category'", ""); 
            BIND_SQL_SOUTEZ_LOKACE=  SQL_READSOUTEZDATA("select value from contest where item='Location'", "");
            BIND_SQL_SOUTEZ_DATUM = SQL_READSOUTEZDATA("select value from contest where item='Date'", "");
            BIND_SQL_SOUTEZ_TEPLOTA=SQL_READSOUTEZDATA("select value from contest where item='Temperature'", "") ;
            BIND_SQL_SOUTEZ_POCASI= SQL_READSOUTEZDATA("select value from contest where item='Weather'", ""); 
            BIND_SQL_SOUTEZ_CLUB= SQL_READSOUTEZDATA("select value from contest where item='Club'", "");
            BIND_SQL_SOUTEZ_SMCRID= SQL_READSOUTEZDATA("select value from contest where item='SMCRID'", "");
            BIND_SQL_SOUTEZ_DIRECTOR= SQL_READSOUTEZDATA("select value from contest where item='Director'", "");
            BIND_SQL_SOUTEZ_HEADJURY= SQL_READSOUTEZDATA("select value from contest where item='Headjury'", "");
            BIND_SQL_SOUTEZ_JURY1= SQL_READSOUTEZDATA("select value from contest where item='Jury1'", "");
            BIND_SQL_SOUTEZ_JURY2= SQL_READSOUTEZDATA("select value from contest where item='Jury2'", "");
            BIND_SQL_SOUTEZ_ROUNDS= Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Rounds'", ""));
            BIND_SQL_SOUTEZ_STARTPOINTS= Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Startpoints'", ""));
            BIND_SQL_SOUTEZ_DELETES= Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Deletes'", ""));
            BIND_SQL_SOUTEZ_ROUNDSFINALE= Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Roundsfinale'", ""));
            BIND_SQL_SOUTEZ_STARTPOINTSFINALE= Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Startpointsfinale'", ""));
            BIND_SQL_SOUTEZ_DELETESFINALE= Convert.ToInt32(SQL_READSOUTEZDATA("select value from contest where item='Deletesfinale'", ""));
        }


   


        public int BIND_SQL_SOUTEZ_ROUNDS
        {
            get { return BIND_SQL_SOUTEZ_ROUNDS_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Rounds'"); BIND_SQL_SOUTEZ_ROUNDS_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_ROUNDS"); }
        }

        public int BIND_SQL_SOUTEZ_STARTPOINTS
        {
            get { return BIND_SQL_SOUTEZ_STARTPOINTS_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Startpoints'"); BIND_SQL_SOUTEZ_STARTPOINTS_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_STARTPOINTS"); }
        }

        public int BIND_SQL_SOUTEZ_DELETES
        {
            get { return BIND_SQL_SOUTEZ_DELETES_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Deletes'");BIND_SQL_SOUTEZ_DELETES_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_DELETES"); }
        }

        public int BIND_SQL_SOUTEZ_ROUNDSFINALE
        {
            get { return BIND_SQL_SOUTEZ_ROUNDSFINALE_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Roundsfinale'");BIND_SQL_SOUTEZ_ROUNDSFINALE_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_ROUNDSFINALE"); }
        }

        public int BIND_SQL_SOUTEZ_STARTPOINTSFINALE
        {
            get { return BIND_SQL_SOUTEZ_STARTPOINTSFINALE_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Startpointsfinale'");BIND_SQL_SOUTEZ_STARTPOINTSFINALE_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_STARTPOINTSFINALE"); }
        }

        public int BIND_SQL_SOUTEZ_DELETESFINALE
        {
            get { return BIND_SQL_SOUTEZ_DELETESFINALE_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Deletesfinale'");BIND_SQL_SOUTEZ_DELETESFINALE_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_DELETESFINALE"); }
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
        public string BIND_SQL_SOUTEZ_KATEGORIE {
            get { return BIND_SQL_SOUTEZ_KATEGORIE_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Category'"); BIND_SQL_SOUTEZ_KATEGORIE_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_KATEGORIE"); }
        }
        public string BIND_SQL_SOUTEZ_NAZEV {
            get { return "Název soutěže : " + BIND_SQL_SOUTEZ_NAZEV_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Name'"); BIND_SQL_SOUTEZ_NAZEV_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_NAZEV"); }
        }
        public string BIND_SQL_SOUTEZ_LOKACE {
            get { return "Lokace : " + BIND_SQL_SOUTEZ_LOKACE_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Location'"); BIND_SQL_SOUTEZ_LOKACE_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_LOKACE"); }
        }

        public string BIND_SQL_SOUTEZ_DATUM {
            get { return BIND_SQL_SOUTEZ_DATUM_value; }
            set { SQL_SAVESOUTEZDATA("update contest set value='" + value + "' where item='Date'"); BIND_SQL_SOUTEZ_DATUM_value = value; OnPropertyChanged("BIND_SQL_SOUTEZ_DATUM"); }
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
                zmenbarvupozadi();
            }
            if (kamulozitvysledek == "popredi")
            {

                pouzitabarva = Int32.Parse(vysledek);
                zmenbarvupopredi();
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
                    string myreader = sqlite_datareader.GetString(0);
                    Console.WriteLine("SQL_READSOUTEZDATA [READ DATA] : " + myreader + " >>>> " + kamulozitvysledek);
                    vysledek = myreader;
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
        public void zmenbarvupopredi()
        {




            if (pouzitabarva == 23)
            {
                pouzitabarva = 0;
            }


            MahApps.Metro.ThemeManager.ChangeTheme(System.Windows.Application.Current, pozadi[pouzitepozadi], barva[pouzitabarva]);
            SQL_SAVESORGDATA("update nastaveni set hodnota = " + pouzitabarva + " where polozka='popredi'");

        }


        public void zmenbarvupozadi()
        {



            if (pouzitepozadi == 2)
            {
                pouzitepozadi = 0;
            }

            SQL_SAVESORGDATA("update nastaveni set hodnota = " + pouzitepozadi + " where polozka='pozadi'");
            MahApps.Metro.ThemeManager.ChangeTheme(System.Windows.Application.Current, pozadi[pouzitepozadi].ToString(), barva[pouzitabarva].ToString());

        }

        #endregion





        #region Players
        public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();

        void CreateTestDataForPlayers()
        {

            for (int i = 0; i < 25; i++)
            {
                var player = new Player() { ID = i, Username = "Player " + i.ToString("0") };
                
                if (i%2 == 0) player.Pets.Add("Cat"); // Only add this to every second item
                player.Pets.Add("Dog");
                player.Pets.Add("Bird");

                Players.Add(player);
            }
        }

        #endregion

    }
}
