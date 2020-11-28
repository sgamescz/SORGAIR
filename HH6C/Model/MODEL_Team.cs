using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp6.Model
{
    public class MODEL_Team : MODEL_BaseClass
    {
        // Default constructor for Player
        public MODEL_Team()
        {

        }

        private int _ID;
        public int  ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
        }

        private string _TEAMNAME;
        public string TEAMNAME
        {
            get { return _TEAMNAME; }
            set { _TEAMNAME = value; RaisePropertyChanged(nameof(TEAMNAME)); }
        }

        private string _POCETCLENU;
        public string POCETCLENU
        {
            get { return "Členů: " + _POCETCLENU; }
            set { _POCETCLENU = value; RaisePropertyChanged(nameof(POCETCLENU)); }
        }



    }


    public class MODEL_usersinteam : MODEL_BaseClass
    {
        // Default constructor for Player
        public MODEL_usersinteam()
        {

        }

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
        }

        private string _FIRSTNAME;
        public string FIRSTNAME
        {
            get { return _FIRSTNAME; }
            set { _FIRSTNAME = value; RaisePropertyChanged(nameof(FIRSTNAME)); }
        }


        private string _LASTNAME;
        public string LASTNAME
        {
            get { return _LASTNAME; }
            set { _LASTNAME = value; RaisePropertyChanged(nameof(LASTNAME)); }
        }


    }



    public class MODEL_usersnotinteam : MODEL_BaseClass
    {
        // Default constructor for Player
        public MODEL_usersnotinteam()
        {

        }

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
        }

        private string _FIRSTNAME;
        public string FIRSTNAME
        {
            get { return _FIRSTNAME; }
            set { _FIRSTNAME = value; RaisePropertyChanged(nameof(FIRSTNAME)); }
        }


        private string _LASTNAME;
        public string LASTNAME
        {
            get { return _LASTNAME; }
            set { _LASTNAME = value; RaisePropertyChanged(nameof(LASTNAME)); }
        }


    }
}
