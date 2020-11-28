using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp6.Model
{
    public class MODEL_Player : MODEL_BaseClass
    {
        // Default constructor for Player
        public MODEL_Player()
        {
          

        }


      
        private int _ID;
        public int  ID
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

        private string _FLAG;
        public string FLAG
        {
            get { return _FLAG; }
            set { _FLAG = value; RaisePropertyChanged(nameof(FLAG)); }
        }


        private string _COUNTRY;
        public string COUNTRY
        {
            get { return _COUNTRY; }
            set { _COUNTRY = value; RaisePropertyChanged(nameof(COUNTRY)); }
        }

        private string _AGECAT;
        public string AGECAT
        {
            get { return _AGECAT; }
            set { _AGECAT = value; RaisePropertyChanged(nameof(AGECAT)); }
        }

        private string _FAILIC;
        public string FAILIC
        {
            get { return _FAILIC; }
            set { _FAILIC = value; RaisePropertyChanged(nameof(FAILIC)); }
        }


        private string _NACLIC;
        public string NACLIC
        {
            get { return _NACLIC; }
            set { _NACLIC = value; RaisePropertyChanged(nameof(NACLIC)); }
        }


        private string _FREQ;
        public string FREQ
        {
            get { return _FREQ; }
            set { _FREQ = value; RaisePropertyChanged(nameof(FREQ)); }
        }


        private int _CH2;
        public int CH2
        {
            get { return _CH2; }
            set { _CH2 = value; RaisePropertyChanged(nameof(CH2)); }
        }


        private int _CH1;
        public int  CH1
        {
            get { return _CH1; }
            set { _CH1 = value; RaisePropertyChanged(nameof(CH1)); }
        }


        private string _CLUB;
        public string CLUB
        {
            get { return _CLUB; }
            set { _CLUB = value; RaisePropertyChanged(nameof(CLUB)); }
        }


        private int _CUSTOMAGECAT;
        public int CUSTOMAGECAT
        {
            get { return _CUSTOMAGECAT; }
            set { _CUSTOMAGECAT = value; RaisePropertyChanged(nameof(CUSTOMAGECAT)); }
        }


        private int _TEAM;
        public int TEAM
        {
            get { return _TEAM; }
            set { _TEAM = value; RaisePropertyChanged(nameof(TEAM)); }
        }




        private string _PAID;
        public string PAID
        {
            get { return _PAID; }
            set { _PAID = value; RaisePropertyChanged(nameof(PAID)); }
        }

        private string _PAIDSTR;
        public string PAIDSTR
        {
            get { return _PAIDSTR; }
            set { _PAIDSTR = value; RaisePropertyChanged(nameof(PAIDSTR)); }
        }

    }











    public class MODEL_Player_actual : MODEL_BaseClass
    {
        // Default constructor for Player
        public MODEL_Player_actual()
        {

        }

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
        }



        private int _STARTPOINT;
        public int STARTPOINT
        {
            get { return _STARTPOINT; }
            set { _STARTPOINT = value; RaisePropertyChanged(nameof(STARTPOINT)); }
        }


        private string _PLAYERDATA;
        public string PLAYERDATA
        {
            get { return _PLAYERDATA; }
            set { _PLAYERDATA = value; RaisePropertyChanged(nameof(PLAYERDATA)); }
        }


        private int _RAWSCORE;
        public int RAWSCORE
        {
            get { return _RAWSCORE; }
            set { _RAWSCORE = value; RaisePropertyChanged(nameof(RAWSCORE)); }
        }

        private int _PREPSCORE;
        public int PREPSCORE
        {
            get { return _PREPSCORE; }
            set { _PREPSCORE = value; RaisePropertyChanged(nameof(PREPSCORE)); }
        }

    }





    public class MODEL_Player_flags : MODEL_BaseClass
    {
        // Default constructor for Player
        public MODEL_Player_flags()
        {

        }

        private string  _FILENAME;
        public string FILENAME
        {
            get { return _FILENAME; }
            set { _FILENAME = value; RaisePropertyChanged(nameof(FILENAME)); }
        }

    }



    public class MODEL_Player_agecategories : MODEL_BaseClass
    {
        // Default constructor for Player
        public MODEL_Player_agecategories()
        {

        }

        private Int32  _ID;
        private string _NAME;
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; RaisePropertyChanged(nameof(NAME)); }
        }


        public Int32  ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
        }


    }



    public class MODEL_Player_frequencies : MODEL_BaseClass
    {
        // Default constructor for Player
        public MODEL_Player_frequencies()
        {

        }

        private Int32 _ID;
        private string _NAME;
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; RaisePropertyChanged(nameof(NAME)); }
        }


        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
        }


    }


}
