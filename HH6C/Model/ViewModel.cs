using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace WpfApp6.Model
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    /// 
    public class ViewModel : INotifyPropertyChanged
    {
        SQLiteConnection m_dbConnection;
        string[] barva = new string[] { "Red", "Green", "Blue", "Purple", "Orange", "Lime", "Emerald", "Teal", "Cyan", "Cobalt", "Indigo", "Violet", "Pink", "Magenta", "Crimson", "Amber", "Yellow", "Brown", "Olive", "Steel", "Mauve", "Taupe", "Sienna" };
        string[] pozadi = new string[] { "Light", "Dark" };
        int pouzitabarva = 1;
        int pouzitepozadi = 1;


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }








        private string someText;
        public string SomeText
        {
            get { return someText; }
            set
            {
                someText = value;
  //              OnPropertyChanged("SomeText");
//                OnPropertyChanged(nameof(FancyMessage));
            }
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





        public void SQL_OPENCONNECTION()
        {

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            m_dbConnection = new SQLiteConnection("Data Source=" + directory + "/db/data.db;");
            m_dbConnection.Open();

        }

        public void SQL_SAVEDATA(string sqltext)
        {

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            SQLiteCommand command = new SQLiteCommand(sqltext, m_dbConnection);

            Console.Write("asdasdasdasd");
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SQLiteException myException)
            {
                Console.WriteLine("Message: " + myException.Message + "\n");
            }



        }







        public void SQL_CLOSECONNECTION()
        {

            m_dbConnection.Close();

        }


        public void SQL_READDATA(string sqltext, string kamulozitvysledek)
        {

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);
            string vysledek = "";




            SQLiteCommand command = new SQLiteCommand(sqltext, m_dbConnection);



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




            vysledek = "";




        }


        public void zmenbarvupopredi()
        {




            if (pouzitabarva == 23)
            {
                pouzitabarva = 0;
            }


            MahApps.Metro.ThemeManager.ChangeTheme(System.Windows.Application.Current, pozadi[pouzitepozadi], barva[pouzitabarva]);
            SQL_SAVEDATA("update nastaveni set hodnota = " + pouzitabarva + " where polozka='popredi'");

        }


        public void zmenbarvupozadi()
        {



            if (pouzitepozadi == 2)
            {
                pouzitepozadi = 0;
            }

            SQL_SAVEDATA("update nastaveni set hodnota = " + pouzitepozadi + " where polozka='pozadi'");
            MahApps.Metro.ThemeManager.ChangeTheme(System.Windows.Application.Current, pozadi[pouzitepozadi].ToString(), barva[pouzitabarva].ToString());

        }



    }
}
