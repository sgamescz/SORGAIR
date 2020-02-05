using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using MahApps.Metro.Controls.Dialogs;

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

        

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

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




    public string BIND_SQL_SOUTEZ_KATEGORIE {get {return SQL_READSOUTEZDATA("select kategorie from soutez", ""); }}
    public string BIND_SQL_SOUTEZ_NAZEV {get {return "Název soutěže : "+SQL_READSOUTEZDATA("select nazev from soutez", ""); }}
    public string BIND_SQL_SOUTEZ_LOKACE {get {return "Lokace : "+SQL_READSOUTEZDATA("select lokace from soutez", ""); }}
    public string BIND_SQL_SOUTEZ_DATUM {get {return SQL_READSOUTEZDATA("select datum from soutez", ""); }}


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
                Console.WriteLine("OPENOPENSOUTEZ");
            }


        }

        public void SQL_SAVESORGDATA(string sqltext)
        {

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            SQLiteCommand command = new SQLiteCommand(sqltext, DBSORG_Connection);

            Console.Write("savedosorgdata");
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SQLiteException myException)
            {
                Console.WriteLine("Message: " + myException.Message + "\n");
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
        }


        public string SQL_READSORGDATA(string sqltext, string kamulozitvysledek)
        {

         
            string vysledek = "";




            SQLiteCommand command = new SQLiteCommand(sqltext, DBSORG_Connection);



            SQLiteDataReader sqlite_datareader;
            try
            {
                sqlite_datareader = command.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    string myreader = sqlite_datareader.GetString(0);
                    Console.WriteLine(myreader);
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
                //MahApps.Metro.ThemeManager.ChangeThemeBaseColor(System.Windows.Application.Current, pozadi[Int32.Parse(vysledek)]);
                zmenbarvupozadi();
            }
            if (kamulozitvysledek == "popredi")
            {

                pouzitabarva = Int32.Parse(vysledek);
                //                MahApps.Metro.ThemeManager.ChangeThemeColorScheme(System.Windows.Application.Current, barva[Int32.Parse(vysledek)]);
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

            Console.WriteLine("SQL_READSOUTEZDATASQL_READSOUTEZDATASQL_READSOUTEZDATASQL_READSOUTEZDATA");
            string vysledek = "";
            SQLiteCommand command = new SQLiteCommand(sqltext, DBSOUTEZ_Connection);

            SQLiteDataReader sqlite_datareader;
            try
            {
                sqlite_datareader = command.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    string myreader = sqlite_datareader.GetString(0);
                    Console.WriteLine("soutezread:"+myreader);
                    vysledek = myreader;
                }
            }
            catch (SQLiteException myException)
            {
                Console.WriteLine("soutezreadERRMessage: " + myException.Message + "\n");
            }




            return vysledek;

            vysledek = "";




        }



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



    }
}
