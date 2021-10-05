using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


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


        private bool _ISENABLED;
        public bool ISENABLED
        {
            get { return _ISENABLED; }
            set { _ISENABLED = value; RaisePropertyChanged(nameof(ISENABLED)); }
        }

        private bool _ISFOCUSED;
        public bool ISFOCUSED
        {
            get { return _ISFOCUSED; }
            set { _ISFOCUSED = value; RaisePropertyChanged(nameof(ISFOCUSED)); }
        }


        private string _REFLY_DATA;
        public string REFLY_DATA
        {
            get { return _REFLY_DATA; }
            set { _REFLY_DATA = value; RaisePropertyChanged(nameof(REFLY_DATA)); }
        }


    }


    public class MODEL_Player_baseresults : MODEL_BaseClass
    {
        // Default constructor for Player
        public MODEL_Player_baseresults()
        {
        }


        private string _POSITION;
        public string POSITION
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



        private string _NATLIC;
        public string NATLIC
        {
            get { return _NATLIC; }
            set { _NATLIC = value; RaisePropertyChanged(nameof(NATLIC)); }
        }


        private string _FAILIC;
        public string FAILIC
        {
            get { return _FAILIC; }
            set { _FAILIC = value; RaisePropertyChanged(nameof(FAILIC)); }
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


        private string _PREPSCOREDIFF;
        public string PREPSCOREDIFF
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

        private string _RND1RES_SKRTACKA = "False";
        public string RND1RES_SKRTACKA
        {
            get { return _RND1RES_SKRTACKA; }
            set { _RND1RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND1RES_SKRTACKA)); }
        }


        private string _RND2RES_SCORE;
        public string RND2RES_SCORE
        {
            get { return _RND2RES_SCORE; }
            set { _RND2RES_SCORE = value; RaisePropertyChanged(nameof(RND2RES_SCORE)); }
        }

        private string _RND2RES_SKRTACKA = "False";
        public string RND2RES_SKRTACKA
        {
            get { return _RND2RES_SKRTACKA; }
            set { _RND2RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND2RES_SKRTACKA)); }
        }


        private string _RND3RES_SCORE;
        public string RND3RES_SCORE
        {
            get { return _RND3RES_SCORE; }
            set { _RND3RES_SCORE = value; RaisePropertyChanged(nameof(RND3RES_SCORE)); }
        }

        private string _RND3RES_SKRTACKA = "False";
        public string RND3RES_SKRTACKA
        {
            get { return _RND3RES_SKRTACKA; }
            set { _RND3RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND3RES_SKRTACKA)); }
        }


        private string _RND4RES_SCORE;
        public string RND4RES_SCORE
        {
            get { return _RND4RES_SCORE; }
            set { _RND4RES_SCORE = value; RaisePropertyChanged(nameof(RND4RES_SCORE)); }
        }

        private string _RND4RES_SKRTACKA = "False";
        public string RND4RES_SKRTACKA
        {
            get { return _RND4RES_SKRTACKA; }
            set { _RND4RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND4RES_SKRTACKA)); }
        }


        private string _RND5RES_SCORE;
        public string RND5RES_SCORE
        {
            get { return _RND5RES_SCORE; }
            set { _RND5RES_SCORE = value; RaisePropertyChanged(nameof(RND5RES_SCORE)); }
        }

        private string _RND5RES_SKRTACKA = "False";
        public string RND5RES_SKRTACKA
        {
            get { return _RND5RES_SKRTACKA; }
            set { _RND5RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND5RES_SKRTACKA)); }
        }


        private string _RND6RES_SCORE;
        public string RND6RES_SCORE
        {
            get { return _RND6RES_SCORE; }
            set { _RND6RES_SCORE = value; RaisePropertyChanged(nameof(RND6RES_SCORE)); }
        }
        private string _RND6RES_SKRTACKA = "False";
        public string RND6RES_SKRTACKA
        {
            get { return _RND6RES_SKRTACKA; }
            set { _RND6RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND6RES_SKRTACKA)); }
        }

        private string _RND7RES_SCORE;
        public string RND7RES_SCORE
        {
            get { return _RND7RES_SCORE; }
            set { _RND7RES_SCORE = value; RaisePropertyChanged(nameof(RND7RES_SCORE)); }
        }
        private string _RND7RES_SKRTACKA = "False";
        public string RND7RES_SKRTACKA
        {
            get { return _RND7RES_SKRTACKA; }
            set { _RND7RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND7RES_SKRTACKA)); }
        }

        private string _RND8RES_SCORE;
        public string RND8RES_SCORE
        {
            get { return _RND8RES_SCORE; }
            set { _RND8RES_SCORE = value; RaisePropertyChanged(nameof(RND8RES_SCORE)); }
        }
        private string _RND8RES_SKRTACKA = "False";
        public string RND8RES_SKRTACKA
        {
            get { return _RND8RES_SKRTACKA; }
            set { _RND8RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND8RES_SKRTACKA)); }
        }


        private string _RND9RES_SCORE;
        public string RND9RES_SCORE
        {
            get { return _RND9RES_SCORE; }
            set { _RND9RES_SCORE = value; RaisePropertyChanged(nameof(RND9RES_SCORE)); }
        }
        private string _RND9RES_SKRTACKA = "False";
        public string RND9RES_SKRTACKA
        {
            get { return _RND9RES_SKRTACKA; }
            set { _RND9RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND9RES_SKRTACKA)); }
        }

        private string _RND10RES_SCORE;
        public string RND10RES_SCORE
        {
            get { return _RND10RES_SCORE; }
            set { _RND10RES_SCORE = value; RaisePropertyChanged(nameof(RND10RES_SCORE)); }
        }
        private string _RND10RES_SKRTACKA = "False";
        public string RND10RES_SKRTACKA
        {
            get { return _RND10RES_SKRTACKA; }
            set { _RND10RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND10RES_SKRTACKA)); }
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

        private string _RND7RES_DATA;
        public string RND7RES_DATA
        {
            get { return _RND7RES_DATA; }
            set { _RND7RES_DATA = value; RaisePropertyChanged(nameof(RND7RES_DATA)); }
        }
        private string _RND8RES_DATA;
        public string RND8RES_DATA
        {
            get { return _RND8RES_DATA; }
            set { _RND8RES_DATA = value; RaisePropertyChanged(nameof(RND8RES_DATA)); }
        }
        private string _RND9RES_DATA;
        public string RND9RES_DATA
        {
            get { return _RND9RES_DATA; }
            set { _RND9RES_DATA = value; RaisePropertyChanged(nameof(RND9RES_DATA)); }
        }
        private string _RND10RES_DATA;
        public string RND10RES_DATA
        {
            get { return _RND10RES_DATA; }
            set { _RND10RES_DATA = value; RaisePropertyChanged(nameof(RND10RES_DATA)); }
        }






        public Visibility _R1VISIBILITY;
        public Visibility R1VISIBILITY
        {
            get { return _R1VISIBILITY; }
            set { _R1VISIBILITY = value; RaisePropertyChanged(nameof(R1VISIBILITY)); }
        }

        public Visibility _R2VISIBILITY;
        public Visibility R2VISIBILITY
        {
            get { return _R2VISIBILITY; }
            set { _R2VISIBILITY = value; RaisePropertyChanged(nameof(R2VISIBILITY)); }
        }

        public Visibility _R3VISIBILITY;
        public Visibility R3VISIBILITY
        {
            get { return _R3VISIBILITY; }
            set { _R3VISIBILITY = value; RaisePropertyChanged(nameof(R3VISIBILITY)); }
        }

        public Visibility _R4VISIBILITY;
        public Visibility R4VISIBILITY
        {
            get { return _R4VISIBILITY; }
            set { _R4VISIBILITY = value; RaisePropertyChanged(nameof(R4VISIBILITY)); }
        }

        public Visibility _R5VISIBILITY;
        public Visibility R5VISIBILITY
        {
            get { return _R5VISIBILITY; }
            set { _R5VISIBILITY = value; RaisePropertyChanged(nameof(R5VISIBILITY)); }
        }

        public Visibility _R6VISIBILITY;
        public Visibility R6VISIBILITY
        {
            get { return _R6VISIBILITY; }
            set { _R6VISIBILITY = value; RaisePropertyChanged(nameof(R6VISIBILITY)); }
        }

        public Visibility _R7VISIBILITY;
        public Visibility R7VISIBILITY
        {
            get { return _R7VISIBILITY; }
            set { _R7VISIBILITY = value; RaisePropertyChanged(nameof(R7VISIBILITY)); }
        }


        public Visibility _R8VISIBILITY;
        public Visibility R8VISIBILITY
        {
            get { return _R8VISIBILITY; }
            set { _R8VISIBILITY = value; RaisePropertyChanged(nameof(R8VISIBILITY)); }
        }

        public Visibility _R9VISIBILITY;
        public Visibility R9VISIBILITY
        {
            get { return _R9VISIBILITY; }
            set { _R9VISIBILITY = value; RaisePropertyChanged(nameof(R9VISIBILITY)); }
        }

        public Visibility _R10VISIBILITY;
        public Visibility R10VISIBILITY
        {
            get { return _R10VISIBILITY; }
            set { _R10VISIBILITY = value; RaisePropertyChanged(nameof(R10VISIBILITY)); }
        }





        private string _FLAG;
        public string FLAG
        {
            get { return _FLAG; }
            set { _FLAG = value; RaisePropertyChanged(nameof(FLAG)); }
        }
        private string _AGECAT;
        public string AGECAT
        {
            get { return _AGECAT; }
            set { _AGECAT = value; RaisePropertyChanged(nameof(AGECAT)); }
        }




    }



    public class MODEL_Player_baseresults_complete : MODEL_BaseClass
    {
        // Default constructor for Player
        public MODEL_Player_baseresults_complete()
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


        private string _NATLIC;
        public string NATLIC
        {
            get { return _NATLIC; }
            set { _NATLIC = value; RaisePropertyChanged(nameof(NATLIC)); }
        }


        private string _FAILIC;
        public string FAILIC
        {
            get { return _FAILIC; }
            set { _FAILIC = value; RaisePropertyChanged(nameof(FAILIC)); }
        }




        private string _PLAYERDATA;
        public string PLAYERDATA
        {
            get { return _PLAYERDATA; }
            set { _PLAYERDATA = value; RaisePropertyChanged(nameof(PLAYERDATA)); }
        }


        private Double _RAWSCORE_BASE;
        public Double RAWSCORE_BASE
        {
            get { return _RAWSCORE_BASE; }
            set { _RAWSCORE_BASE = value; RaisePropertyChanged(nameof(RAWSCORE_BASE)); }
        }

        private Double _PREPSCORE_BASE;
        public Double PREPSCORE_BASE
        {
            get { return _PREPSCORE_BASE; }
            set { _PREPSCORE_BASE = value; RaisePropertyChanged(nameof(PREPSCORE_BASE)); }
        }


        private Double _RAWSCORE_FINAL;
        public Double RAWSCORE_FINAL
        {
            get { return _RAWSCORE_FINAL; }
            set { _RAWSCORE_FINAL = value; RaisePropertyChanged(nameof(RAWSCORE_FINAL)); }
        }

        private Double _PREPSCORE_FINAL;
        public Double PREPSCORE_FINAL
        {
            get { return _PREPSCORE_FINAL; }
            set { _PREPSCORE_FINAL = value; RaisePropertyChanged(nameof(PREPSCORE_FINAL)); }
        }



        private Double _GPEN;
        public Double GPEN
        {
            get { return _GPEN; }
            set { _GPEN = value; RaisePropertyChanged(nameof(GPEN)); }
        }


        private string  _PREPSCOREDIFF_BASE;
        public string PREPSCOREDIFF_BASE
        {
            get { return _PREPSCOREDIFF_BASE; }
            set { _PREPSCOREDIFF_BASE = value; RaisePropertyChanged(nameof(PREPSCOREDIFF_BASE)); }
        }

        private string _PREPSCOREDIFF_FINAL;
        public string PREPSCOREDIFF_FINAL
        {
            get { return _PREPSCOREDIFF_FINAL; }
            set { _PREPSCOREDIFF_FINAL = value; RaisePropertyChanged(nameof(PREPSCOREDIFF_FINAL)); }
        }


        private string _RND1RES_SCORE_F;
        public string RND1RES_SCORE_F
        {
            get { return _RND1RES_SCORE_F; }
            set { _RND1RES_SCORE_F = value; RaisePropertyChanged(nameof(RND1RES_SCORE_F)); }
        }

        private string _RND1RES_DATA_F;
        public string RND1RES_DATA_F
        {
            get { return _RND1RES_DATA_F; }
            set { _RND1RES_DATA_F = value; RaisePropertyChanged(nameof(RND1RES_DATA_F)); }
        }

        private string _RND1RES_SKRTACKA_F = "False";
        public string RND1RES_SKRTACKA_F
        {
            get { return _RND1RES_SKRTACKA_F; }
            set { _RND1RES_SKRTACKA_F = value; RaisePropertyChanged(nameof(RND1RES_SKRTACKA_F)); }
        }


        private string _RND2RES_SCORE_F;
        public string RND2RES_SCORE_F
        {
            get { return _RND2RES_SCORE_F; }
            set { _RND2RES_SCORE_F = value; RaisePropertyChanged(nameof(RND2RES_SCORE_F)); }
        }

        private string _RND2RES_DATA_F;
        public string RND2RES_DATA_F
        {
            get { return _RND2RES_DATA_F; }
            set { _RND2RES_DATA_F = value; RaisePropertyChanged(nameof(RND2RES_DATA_F)); }
        }

        private string _RND2RES_SKRTACKA_F = "False";
        public string RND2RES_SKRTACKA_F
        {
            get { return _RND2RES_SKRTACKA_F; }
            set { _RND2RES_SKRTACKA_F = value; RaisePropertyChanged(nameof(RND2RES_SKRTACKA_F)); }
        }

        private string _RND3RES_SCORE_F;
        public string RND3RES_SCORE_F
        {
            get { return _RND3RES_SCORE_F; }
            set { _RND3RES_SCORE_F = value; RaisePropertyChanged(nameof(RND3RES_SCORE_F)); }
        }

        private string _RND3RES_DATA_F;
        public string RND3RES_DATA_F
        {
            get { return _RND3RES_DATA_F; }
            set { _RND3RES_DATA_F = value; RaisePropertyChanged(nameof(RND3RES_DATA_F)); }
        }

        private string _RND3RES_SKRTACKA_F = "False";
        public string RND3RES_SKRTACKA_F
        {
            get { return _RND3RES_SKRTACKA_F; }
            set { _RND3RES_SKRTACKA_F = value; RaisePropertyChanged(nameof(RND3RES_SKRTACKA_F)); }
        }

        private string _RND4RES_SCORE_F;
        public string RND4RES_SCORE_F
        {
            get { return _RND4RES_SCORE_F; }
            set { _RND4RES_SCORE_F = value; RaisePropertyChanged(nameof(RND4RES_SCORE_F)); }
        }

        private string _RND4RES_DATA_F;
        public string RND4RES_DATA_F
        {
            get { return _RND4RES_DATA_F; }
            set { _RND4RES_DATA_F = value; RaisePropertyChanged(nameof(RND4RES_DATA_F)); }
        }
        private string _RND4RES_SKRTACKA_F = "False";
        public string RND4RES_SKRTACKA_F
        {
            get { return _RND4RES_SKRTACKA_F; }
            set { _RND4RES_SKRTACKA_F = value; RaisePropertyChanged(nameof(RND4RES_SKRTACKA_F)); }
        }


        private string _RND5RES_SCORE_F;
        public string RND5RES_SCORE_F
        {
            get { return _RND5RES_SCORE_F; }
            set { _RND5RES_SCORE_F = value; RaisePropertyChanged(nameof(RND5RES_SCORE_F)); }
        }


        private string _RND5RES_DATA_F;
        public string RND5RES_DATA_F
        {
            get { return _RND5RES_DATA_F; }
            set { _RND5RES_DATA_F = value; RaisePropertyChanged(nameof(RND5RES_DATA_F)); }
        }

        private string _RND5RES_SKRTACKA_F = "False";
        public string RND5RES_SKRTACKA_F
        {
            get { return _RND5RES_SKRTACKA_F; }
            set { _RND5RES_SKRTACKA_F = value; RaisePropertyChanged(nameof(RND5RES_SKRTACKA_F)); }
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

        private string _RND1RES_SKRTACKA = "False";
        public string RND1RES_SKRTACKA
        {
            get { return _RND1RES_SKRTACKA; }
            set { _RND1RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND1RES_SKRTACKA)); }
        }


        private string _RND2RES_SCORE;
        public string RND2RES_SCORE
        {
            get { return _RND2RES_SCORE; }
            set { _RND2RES_SCORE = value; RaisePropertyChanged(nameof(RND2RES_SCORE)); }
        }

        private string _RND2RES_SKRTACKA = "False";
        public string RND2RES_SKRTACKA
        {
            get { return _RND2RES_SKRTACKA; }
            set { _RND2RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND2RES_SKRTACKA)); }
        }


        private string _RND3RES_SCORE;
        public string RND3RES_SCORE
        {
            get { return _RND3RES_SCORE; }
            set { _RND3RES_SCORE = value; RaisePropertyChanged(nameof(RND3RES_SCORE)); }
        }

        private string _RND3RES_SKRTACKA = "False";
        public string RND3RES_SKRTACKA
        {
            get { return _RND3RES_SKRTACKA; }
            set { _RND3RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND3RES_SKRTACKA)); }
        }


        private string _RND4RES_SCORE;
        public string RND4RES_SCORE
        {
            get { return _RND4RES_SCORE; }
            set { _RND4RES_SCORE = value; RaisePropertyChanged(nameof(RND4RES_SCORE)); }
        }

        private string _RND4RES_SKRTACKA = "False";
        public string RND4RES_SKRTACKA
        {
            get { return _RND4RES_SKRTACKA; }
            set { _RND4RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND4RES_SKRTACKA)); }
        }


        private string _RND5RES_SCORE;
        public string RND5RES_SCORE
        {
            get { return _RND5RES_SCORE; }
            set { _RND5RES_SCORE = value; RaisePropertyChanged(nameof(RND5RES_SCORE)); }
        }

        private string _RND5RES_SKRTACKA = "False";
        public string RND5RES_SKRTACKA
        {
            get { return _RND5RES_SKRTACKA; }
            set { _RND5RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND5RES_SKRTACKA)); }
        }


        private string _RND6RES_SCORE;
        public string RND6RES_SCORE
        {
            get { return _RND6RES_SCORE; }
            set { _RND6RES_SCORE = value; RaisePropertyChanged(nameof(RND6RES_SCORE)); }
        }
        private string _RND6RES_SKRTACKA = "False";
        public string RND6RES_SKRTACKA
        {
            get { return _RND6RES_SKRTACKA; }
            set { _RND6RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND6RES_SKRTACKA)); }
        }

        private string _RND7RES_SCORE;
        public string RND7RES_SCORE
        {
            get { return _RND7RES_SCORE; }
            set { _RND7RES_SCORE = value; RaisePropertyChanged(nameof(RND7RES_SCORE)); }
        }
        private string _RND7RES_SKRTACKA = "False";
        public string RND7RES_SKRTACKA
        {
            get { return _RND7RES_SKRTACKA; }
            set { _RND7RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND7RES_SKRTACKA)); }
        }

        private string _RND8RES_SCORE;
        public string RND8RES_SCORE
        {
            get { return _RND8RES_SCORE; }
            set { _RND8RES_SCORE = value; RaisePropertyChanged(nameof(RND8RES_SCORE)); }
        }
        private string _RND8RES_SKRTACKA = "False";
        public string RND8RES_SKRTACKA
        {
            get { return _RND8RES_SKRTACKA; }
            set { _RND8RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND8RES_SKRTACKA)); }
        }


        private string _RND9RES_SCORE;
        public string RND9RES_SCORE
        {
            get { return _RND9RES_SCORE; }
            set { _RND9RES_SCORE = value; RaisePropertyChanged(nameof(RND9RES_SCORE)); }
        }
        private string _RND9RES_SKRTACKA = "False";
        public string RND9RES_SKRTACKA
        {
            get { return _RND9RES_SKRTACKA; }
            set { _RND9RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND9RES_SKRTACKA)); }
        }

        private string _RND10RES_SCORE;
        public string RND10RES_SCORE
        {
            get { return _RND10RES_SCORE; }
            set { _RND10RES_SCORE = value; RaisePropertyChanged(nameof(RND10RES_SCORE)); }
        }
        private string _RND10RES_SKRTACKA = "False";
        public string RND10RES_SKRTACKA
        {
            get { return _RND10RES_SKRTACKA; }
            set { _RND10RES_SKRTACKA = value; RaisePropertyChanged(nameof(RND10RES_SKRTACKA)); }
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

        private string _RND7RES_DATA;
        public string RND7RES_DATA
        {
            get { return _RND7RES_DATA; }
            set { _RND7RES_DATA = value; RaisePropertyChanged(nameof(RND7RES_DATA)); }
        }
        private string _RND8RES_DATA;
        public string RND8RES_DATA
        {
            get { return _RND8RES_DATA; }
            set { _RND8RES_DATA = value; RaisePropertyChanged(nameof(RND8RES_DATA)); }
        }
        private string _RND9RES_DATA;
        public string RND9RES_DATA
        {
            get { return _RND9RES_DATA; }
            set { _RND9RES_DATA = value; RaisePropertyChanged(nameof(RND9RES_DATA)); }
        }
        private string _RND10RES_DATA;
        public string RND10RES_DATA
        {
            get { return _RND10RES_DATA; }
            set { _RND10RES_DATA = value; RaisePropertyChanged(nameof(RND10RES_DATA)); }
        }






        public Visibility _R1VISIBILITY;
        public Visibility R1VISIBILITY
        {
            get { return _R1VISIBILITY; }
            set { _R1VISIBILITY = value; RaisePropertyChanged(nameof(R1VISIBILITY)); }
        }

        public Visibility _R2VISIBILITY;
        public Visibility R2VISIBILITY
        {
            get { return _R2VISIBILITY; }
            set { _R2VISIBILITY = value; RaisePropertyChanged(nameof(R2VISIBILITY)); }
        }

        public Visibility _R3VISIBILITY;
        public Visibility R3VISIBILITY
        {
            get { return _R3VISIBILITY; }
            set { _R3VISIBILITY = value; RaisePropertyChanged(nameof(R3VISIBILITY)); }
        }

        public Visibility _R4VISIBILITY;
        public Visibility R4VISIBILITY
        {
            get { return _R4VISIBILITY; }
            set { _R4VISIBILITY = value; RaisePropertyChanged(nameof(R4VISIBILITY)); }
        }

        public Visibility _R5VISIBILITY;
        public Visibility R5VISIBILITY
        {
            get { return _R5VISIBILITY; }
            set { _R5VISIBILITY = value; RaisePropertyChanged(nameof(R5VISIBILITY)); }
        }

        public Visibility _R6VISIBILITY;
        public Visibility R6VISIBILITY
        {
            get { return _R6VISIBILITY; }
            set { _R6VISIBILITY = value; RaisePropertyChanged(nameof(R6VISIBILITY)); }
        }

        public Visibility _R7VISIBILITY;
        public Visibility R7VISIBILITY
        {
            get { return _R7VISIBILITY; }
            set { _R7VISIBILITY = value; RaisePropertyChanged(nameof(R7VISIBILITY)); }
        }


        public Visibility _R8VISIBILITY;
        public Visibility R8VISIBILITY
        {
            get { return _R8VISIBILITY; }
            set { _R8VISIBILITY = value; RaisePropertyChanged(nameof(R8VISIBILITY)); }
        }

        public Visibility _R9VISIBILITY;
        public Visibility R9VISIBILITY
        {
            get { return _R9VISIBILITY; }
            set { _R9VISIBILITY = value; RaisePropertyChanged(nameof(R9VISIBILITY)); }
        }

        public Visibility _R10VISIBILITY;
        public Visibility R10VISIBILITY
        {
            get { return _R10VISIBILITY; }
            set { _R10VISIBILITY = value; RaisePropertyChanged(nameof(R10VISIBILITY)); }
        }





        private string _FLAG;
        public string FLAG
        {
            get { return _FLAG; }
            set { _FLAG = value; RaisePropertyChanged(nameof(FLAG)); }
        }



        private Double _BONUS_POINTS;
        public Double BONUS_POINTS
        {
            get { return _BONUS_POINTS; }
            set { _BONUS_POINTS  = value; RaisePropertyChanged(nameof(BONUS_POINTS)); }
        }

        private string _TO_1000;
        public string TO_1000
        {
            get { return _TO_1000; }
            set { _TO_1000  = value; RaisePropertyChanged(nameof(TO_1000)); }
        }


        private string _AGECAT;
        public string AGECAT
        {
            get { return _AGECAT; }
            set { _AGECAT = value; RaisePropertyChanged(nameof(AGECAT)); }
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
