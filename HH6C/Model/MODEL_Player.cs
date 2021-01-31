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

        private int _AGECATID;
        public int AGECATID
        {
            get { return _AGECATID; }
            set { _AGECATID = value; RaisePropertyChanged(nameof(AGECATID)); }
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


        private int _FREQID;
        public int FREQID
        {
            get { return _FREQID; }
            set { _FREQID = value; RaisePropertyChanged(nameof(FREQID)); }
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


        private Double  _RAWSCORE;
        public Double RAWSCORE
        {
            get { return _RAWSCORE; }
            set { _RAWSCORE = value; RaisePropertyChanged(nameof(RAWSCORE)); }
        }

        private Double _PREPSCORE;
        public Double PREPSCORE
        {
            get { return _PREPSCORE; }
            set { _PREPSCORE = value; RaisePropertyChanged(nameof(PREPSCORE)); }
        }


        private string   _ENTERED;
        public string  ENTERED
        {
            get { return _ENTERED; }
            set { _ENTERED = value; RaisePropertyChanged(nameof(ENTERED)); }
        }


        private bool _REALPLAYER;
        public bool REALPLAYER
        {
            get { return _REALPLAYER; }
            set { _REALPLAYER = value; RaisePropertyChanged(nameof(REALPLAYER)); }
        }


    }





    public class MODEL_Player_baseresults : MODEL_BaseClass
    {
        // Default constructor for Player
        public MODEL_Player_baseresults()
        {
        }


        private int _POSITION;
        public int POSITION
        {
            get { return _POSITION; }
            set { _POSITION = value; RaisePropertyChanged(nameof(POSITION)); }
        }


        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
        }



        private string _PLAYERDATA;
        public string PLAYERDATA
        {
            get { return _PLAYERDATA; }
            set { _PLAYERDATA = value; RaisePropertyChanged(nameof(PLAYERDATA)); }
        }


        private Double _RAWSCORE;
        public Double RAWSCORE
        {
            get { return _RAWSCORE; }
            set { _RAWSCORE = value; RaisePropertyChanged(nameof(RAWSCORE)); }
        }

        private Double _PREPSCORE;
        public Double PREPSCORE
        {
            get { return _PREPSCORE; }
            set { _PREPSCORE = value; RaisePropertyChanged(nameof(PREPSCORE)); }
        }


        private Double _GPEN;
        public Double GPEN
        {
            get { return _GPEN; }
            set { _GPEN = value; RaisePropertyChanged(nameof(GPEN)); }
        }


        private string  _PREPSCOREDIFF;
        public string  PREPSCOREDIFF
        {
            get { return _PREPSCOREDIFF; }
            set { _PREPSCOREDIFF = value; RaisePropertyChanged(nameof(PREPSCOREDIFF)); }
        }


        private string _RND1RES_SCORE;
        public string RND1RES_SCORE
        {
            get { return _RND1RES_SCORE; }
            set { _RND1RES_SCORE = value; RaisePropertyChanged(nameof(RND1RES_SCORE)); }
        }

        private string _RND1RES_DATA;
        public string RND1RES_DATA
        {
            get { return _RND1RES_DATA; }
            set { _RND1RES_DATA = value; RaisePropertyChanged(nameof(RND1RES_DATA)); }
        }



        private string _RND2RES_SCORE;
        public string RND2RES_SCORE
        {
            get { return _RND2RES_SCORE; }
            set { _RND2RES_SCORE = value; RaisePropertyChanged(nameof(RND2RES_SCORE)); }
        }


        private string _RND3RES_SCORE;
        public string RND3RES_SCORE
        {
            get { return _RND3RES_SCORE; }
            set { _RND3RES_SCORE = value; RaisePropertyChanged(nameof(RND3RES_SCORE)); }
        }


        private string _RND4RES_SCORE;
        public string RND4RES_SCORE
        {
            get { return _RND4RES_SCORE; }
            set { _RND4RES_SCORE = value; RaisePropertyChanged(nameof(RND4RES_SCORE)); }
        }

        private string _RND5RES_SCORE;
        public string RND5RES_SCORE
        {
            get { return _RND5RES_SCORE; }
            set { _RND5RES_SCORE = value; RaisePropertyChanged(nameof(RND5RES_SCORE)); }
        }

        private string _RND6RES_SCORE;
        public string RND6RES_SCORE
        {
            get { return _RND6RES_SCORE; }
            set { _RND6RES_SCORE = value; RaisePropertyChanged(nameof(RND6RES_SCORE)); }
        }




        private string _RND2RES_DATA;
        public string RND2RES_DATA
        {
            get { return _RND2RES_DATA; }
            set { _RND2RES_DATA = value; RaisePropertyChanged(nameof(RND2RES_DATA)); }
        }


        private string _RND3RES_DATA;
        public string RND3RES_DATA
        {
            get { return _RND3RES_DATA; }
            set { _RND3RES_DATA = value; RaisePropertyChanged(nameof(RND3RES_DATA)); }
        }


        private string _RND4RES_DATA;
        public string RND4RES_DATA
        {
            get { return _RND4RES_DATA; }
            set { _RND4RES_DATA = value; RaisePropertyChanged(nameof(RND4RES_DATA)); }
        }

        private string _RND5RES_DATA;
        public string RND5RES_DATA
        {
            get { return _RND5RES_DATA; }
            set { _RND5RES_DATA = value; RaisePropertyChanged(nameof(RND5RES_DATA)); }
        }

        private string _RND6RES_DATA;
        public string RND6RES_DATA
        {
            get { return _RND6RES_DATA; }
            set { _RND6RES_DATA = value; RaisePropertyChanged(nameof(RND6RES_DATA)); }
        }


        private string _FLAG;
        public string FLAG
        {
            get { return _FLAG; }
            set { _FLAG = value; RaisePropertyChanged(nameof(FLAG)); }
        }




    }



    public class MODEL_Player_selected : MODEL_BaseClass


    {


        // Default constructor for Player
        public MODEL_Player_selected()
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

        private string _WHOLENAME;
        public string WHOLENAME
        {
            get { return _WHOLENAME; }
            set { _WHOLENAME = value; RaisePropertyChanged(nameof(WHOLENAME)); }
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
        public int CH1
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


        private int _SCORE_MINUTES;
        public int SCORE_MINUTES
        {
            get { return _SCORE_MINUTES; }
            set { _SCORE_MINUTES = value; RaisePropertyChanged(nameof(SCORE_MINUTES)); }
        }

        private int _SCORE_SECONDS;
        public int SCORE_SECONDS
        {
            get { return _SCORE_SECONDS; }
            set { _SCORE_SECONDS = value; RaisePropertyChanged(nameof(SCORE_SECONDS)); }
        }


        private int _SCORE_LANDING;
        public int SCORE_LANDING
        {
            get { return _SCORE_LANDING; }
            set { _SCORE_LANDING = value; RaisePropertyChanged(nameof(SCORE_LANDING)); }
        }


        private int _SCORE_HEIGHT;
        public int SCORE_HEIGHT
        {
            get { return _SCORE_HEIGHT; }
            set { _SCORE_HEIGHT = value; RaisePropertyChanged(nameof(SCORE_HEIGHT)); }
        }

        private string _SCORE_RAW;
        public string SCORE_RAW
        {
            get { return _SCORE_RAW; }
            set { _SCORE_RAW = value; RaisePropertyChanged(nameof(SCORE_RAW)); }
        }



        private string  _SCORE_PREP;
        public string SCORE_PREP
        {
            get { return _SCORE_PREP; }
            set { _SCORE_PREP = value; RaisePropertyChanged(nameof(SCORE_PREP)); }
        }


    }



    public class MODEL_Player_flags : MODEL_BaseClass
    {
        // Default constructor for Player
        public MODEL_Player_flags()
        {

        }

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
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

    public class DataObject
    {
  
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
