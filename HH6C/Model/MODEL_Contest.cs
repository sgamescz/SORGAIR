using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp6.Model
{
    public class MODEL_Contest_Rounds : MODEL_BaseClass
    {






        // Default constructor for Player
        public MODEL_Contest_Rounds()
        {

        }

        private int _ID;
        public int  ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
        }

        private string _ROUNDNAME;
        public string ROUNDNAME
        {
            get { return _ROUNDNAME; }
            set { _ROUNDNAME = value; RaisePropertyChanged(nameof(ROUNDNAME)); }
        }

        private string _ROUNDTYPE;
        public string ROUNDTYPE
        {
            get { return _ROUNDTYPE; }
            set { _ROUNDTYPE = value; RaisePropertyChanged(nameof(ROUNDTYPE)); }
        }


        private int _ROUNDLENGHT;
        public int ROUNDLENGHT
        {
            get { return _ROUNDLENGHT; }
            set { _ROUNDLENGHT = value; RaisePropertyChanged(nameof(ROUNDLENGHT)); }
        }



        private int  _ROUNDZADANO;
        public int  ROUNDZADANO
        {
            get { return _ROUNDZADANO; }
            set { _ROUNDZADANO = value; RaisePropertyChanged(nameof(ROUNDZADANO)); }
        }


        public List<TodoItem> items { get; set; } = new List<TodoItem>();

        private string _ISSELECTED;
        public string ISSELECTED
        {
            get { return _ISSELECTED; }
            set { _ISSELECTED = value; RaisePropertyChanged(nameof(ISSELECTED)); }
        }




    }









    public class MODEL_Contest_Groups_refly : MODEL_BaseClass
    {

        // Default constructor for Player
        public MODEL_Contest_Groups_refly()
        {

        }

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
        }

        private string _GROUPNAME;
        public string GROUPNAME
        {
            get { return _GROUPNAME; }
            set { _GROUPNAME = value; RaisePropertyChanged(nameof(GROUPNAME)); }
        }

        private string _GROUPNAME_SRC;
        public string GROUPNAME_SRC
        {
            get { return _GROUPNAME_SRC; }
            set { _GROUPNAME_SRC = value; RaisePropertyChanged(nameof(GROUPNAME_SRC)); }
        }


        private string _GROUPTYPE;
        public string GROUPTYPE
        {
            get { return _GROUPTYPE; }
            set { _GROUPTYPE = value; RaisePropertyChanged(nameof(GROUPTYPE)); }
        }


        private int _GROUPLENGHT;
        public int GROUPLENGHT
        {
            get { return _GROUPLENGHT; }
            set { _GROUPLENGHT = value; RaisePropertyChanged(nameof(GROUPLENGHT)); }
        }



        private int _GROUPZADANO;
        public int GROUPZADANO
        {
            get { return _GROUPZADANO; }
            set { _GROUPZADANO = value; RaisePropertyChanged(nameof(GROUPZADANO)); }
        }


        private string _ISSELECTED;
        public string ISSELECTED
        {
            get { return _ISSELECTED; }
            set { _ISSELECTED = value; RaisePropertyChanged(nameof(ISSELECTED)); }
        }






    }










    public class MODEL_Contest_Groups : MODEL_BaseClass
    {

        // Default constructor for Player
        public MODEL_Contest_Groups()
        {

        }

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
        }

        private string _GROUPNAME;
        public string GROUPNAME
        {
            get { return _GROUPNAME; }
            set { _GROUPNAME = value; RaisePropertyChanged(nameof(GROUPNAME)); }
        }

        private string _GROUPTYPE;
        public string GROUPTYPE
        {
            get { return _GROUPTYPE; }
            set { _GROUPTYPE = value; RaisePropertyChanged(nameof(GROUPTYPE)); }
        }


        private int _GROUPLENGHT;
        public int GROUPLENGHT
        {
            get { return _GROUPLENGHT; }
            set { _GROUPLENGHT = value; RaisePropertyChanged(nameof(GROUPLENGHT)); }
        }



        private int _GROUPZADANO;
        public int GROUPZADANO
        {
            get { return _GROUPZADANO; }
            set { _GROUPZADANO = value; RaisePropertyChanged(nameof(GROUPZADANO)); }
        }


        private string _ISSELECTED;
        public string ISSELECTED
        {
            get { return _ISSELECTED; }
            set { _ISSELECTED = value; RaisePropertyChanged(nameof(ISSELECTED)); }
        }






    }








    public class MODEL_Contest_Rules : MODEL_BaseClass
    {


        // Default constructor for Player
        public MODEL_Contest_Rules()
        {
            

        }

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
        }

        private string _CATEGORY;
        public string CATEGORY
        {
            get { return _CATEGORY; }
            set { _CATEGORY = value; RaisePropertyChanged(nameof(CATEGORY)); }
        }

        private int _HEIGHTLIMIT;
        public int HEIGHTLIMIT
        {
            get { return _HEIGHTLIMIT; }
            set { _HEIGHTLIMIT = value;   RaisePropertyChanged(nameof(HEIGHTLIMIT)); }
        }

        private Decimal _HEIGHTOVER;
        public Decimal HEIGHTOVER
        {
            get { return _HEIGHTOVER; }
            set { _HEIGHTOVER = value; RaisePropertyChanged(nameof(HEIGHTOVER)); }
        }


        private Decimal _HEIGHTUNDER;
        public Decimal HEIGHTUNDER
        {
            get { return _HEIGHTUNDER; }
            set { _HEIGHTUNDER = value; RaisePropertyChanged(nameof(HEIGHTUNDER)); }
        }




        private int _TIME1LIMIT;
        public int TIME1LIMIT
        {
            get { return _TIME1LIMIT; }
            set { _TIME1LIMIT = value; RaisePropertyChanged(nameof(TIME1LIMIT)); }
        }

        private Decimal _TIME1OVER;
        public Decimal TIME1OVER
        {
            get { return _TIME1OVER; }
            set { _TIME1OVER = value; RaisePropertyChanged(nameof(TIME1OVER)); }
        }


        private Decimal _TIME1UNDER;
        public Decimal TIME1UNDER
        {
            get { return _TIME1UNDER; }
            set { _TIME1UNDER = value; RaisePropertyChanged(nameof(TIME1UNDER)); }
        }



        private int _TIME2LIMIT;
        public int TIME2LIMIT
        {
            get { return _TIME2LIMIT; }
            set { _TIME2LIMIT = value; RaisePropertyChanged(nameof(TIME2LIMIT)); }
        }

        private Decimal _TIME2OVER;
        public Decimal TIME2OVER
        {
            get { return _TIME2OVER; }
            set { _TIME2OVER = value; RaisePropertyChanged(nameof(TIME2OVER)); }
        }


        private Decimal _TIME2UNDER;
        public Decimal TIME2UNDER
        {
            get { return _TIME2UNDER; }
            set { _TIME2UNDER = value; RaisePropertyChanged(nameof(TIME2UNDER)); }
        }




        private bool _ENTRYHEIGHT;
        public bool ENTRYHEIGHT
        {
            get { return _ENTRYHEIGHT; }
            set { _ENTRYHEIGHT = value; RaisePropertyChanged(nameof(ENTRYHEIGHT)); }
        }



        private Decimal _SUBFROMLANDING1;
        public Decimal SUBFROMLANDING1
        {
            get { return _SUBFROMLANDING1; }
            set { _SUBFROMLANDING1 = value; RaisePropertyChanged(nameof(SUBFROMLANDING1)); }
        }


        private Decimal _SUBFROMLANDING2;
        public Decimal SUBFROMLANDING2
        {
            get { return _SUBFROMLANDING2; }
            set { _SUBFROMLANDING2 = value; RaisePropertyChanged(nameof(SUBFROMLANDING2)); }
        }



        private Decimal _SUBFROMTIME1;
        public Decimal SUBFROMTIME1
        {
            get { return _SUBFROMTIME1; }
            set { _SUBFROMTIME1 = value; RaisePropertyChanged(nameof(SUBFROMTIME1)); }
        }


        private Decimal _SUBFROMTIME2;
        public Decimal SUBFROMTIME2
        {
            get { return _SUBFROMTIME2; }
            set { _SUBFROMTIME2 = value; RaisePropertyChanged(nameof(SUBFROMTIME2)); }
        }


        private bool _DELETELANDING1;
        public bool DELETELANDING1
        {
            get { return _DELETELANDING1; }
            set { _DELETELANDING1 = value; RaisePropertyChanged(nameof(DELETELANDING1)); }
        }


        private bool _DELETELANDING2;
        public bool DELETELANDING2
        {
            get { return _DELETELANDING2; }
            set { _DELETELANDING2 = value; RaisePropertyChanged(nameof(DELETELANDING2)); }
        }


        private bool _DELETETIME1;
        public bool DELETETIME1
        {
            get { return _DELETETIME1; }
            set { _DELETETIME1 = value; RaisePropertyChanged(nameof(DELETETIME1)); }
        }


        private bool _DELETETIME2;
        public bool DELETETIME2
        {
            get { return _DELETETIME2; }
            set { _DELETETIME2 = value; RaisePropertyChanged(nameof(DELETETIME2)); }
        }


        private bool _DELETEALL1;
        public bool DELETEALL1
        {
            get { return _DELETEALL1; }
            set { _DELETEALL1 = value; RaisePropertyChanged(nameof(DELETEALL1)); }
        }


        private bool _DELETEALL2;
        public bool DELETEALL2
        {
            get { return _DELETEALL2; }
            set { _DELETEALL2 = value; RaisePropertyChanged(nameof(DELETEALL2)); }
        }




        private int _BASEROUNDLENGHT;
        public int BASEROUNDLENGHT
        {
            get { return _BASEROUNDLENGHT; }
            set { _BASEROUNDLENGHT = value; RaisePropertyChanged(nameof(BASEROUNDLENGHT)); }
        }

        private int _BASEROUNDMAXTIME;
        public int BASEROUNDMAXTIME
        {
            get { return _BASEROUNDMAXTIME; }
            set { _BASEROUNDMAXTIME = value; RaisePropertyChanged(nameof(BASEROUNDMAXTIME)); }
        }


        private int _FINALROUNDLENGHT;
        public int FINALROUNDLENGHT
        {
            get { return _FINALROUNDLENGHT; }
            set { _FINALROUNDLENGHT = value; RaisePropertyChanged(nameof(FINALROUNDLENGHT)); }
        }

        private int _FINALROUNDMAXTIME;
        public int FINALROUNDMAXTIME
        {
            get { return _FINALROUNDMAXTIME; }
            set { _FINALROUNDMAXTIME = value; RaisePropertyChanged(nameof(FINALROUNDMAXTIME)); }
        }

        private bool _BONUSONLYFORFINALIST;
        public bool BONUSONLYFORFINALIST
        {
            get { return _BONUSONLYFORFINALIST; }
            set { _BONUSONLYFORFINALIST = value; RaisePropertyChanged(nameof(BONUSONLYFORFINALIST)); }
        }


    }



    public class MODEL_category_Rules : MODEL_BaseClass
    {


        // Default constructor for Player
        public MODEL_category_Rules()
        {


        }

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
        }

        private string _CATEGORY;
        public string CATEGORY
        {
            get { return _CATEGORY; }
            set { _CATEGORY = value; RaisePropertyChanged(nameof(CATEGORY)); }
        }

        private int _HEIGHTLIMIT;
        public int HEIGHTLIMIT
        {
            get { return _HEIGHTLIMIT; }
            set { _HEIGHTLIMIT = value; RaisePropertyChanged(nameof(HEIGHTLIMIT)); }
        }

        private Decimal _HEIGHTOVER;
        public Decimal HEIGHTOVER
        {
            get { return _HEIGHTOVER; }
            set { _HEIGHTOVER = value; RaisePropertyChanged(nameof(HEIGHTOVER)); }
        }


        private Decimal _HEIGHTUNDER;
        public Decimal HEIGHTUNDER
        {
            get { return _HEIGHTUNDER; }
            set { _HEIGHTUNDER = value; RaisePropertyChanged(nameof(HEIGHTUNDER)); }
        }




        private int _TIME1LIMIT;
        public int TIME1LIMIT
        {
            get { return _TIME1LIMIT; }
            set { _TIME1LIMIT = value; RaisePropertyChanged(nameof(TIME1LIMIT)); }
        }

        private Decimal _TIME1OVER;
        public Decimal TIME1OVER
        {
            get { return _TIME1OVER; }
            set { _TIME1OVER = value; RaisePropertyChanged(nameof(TIME1OVER)); }
        }


        private Decimal _TIME1UNDER;
        public Decimal TIME1UNDER
        {
            get { return _TIME1UNDER; }
            set { _TIME1UNDER = value; RaisePropertyChanged(nameof(TIME1UNDER)); }
        }



        private int _TIME2LIMIT;
        public int TIME2LIMIT
        {
            get { return _TIME2LIMIT; }
            set { _TIME2LIMIT = value; RaisePropertyChanged(nameof(TIME2LIMIT)); }
        }

        private Decimal _TIME2OVER;
        public Decimal TIME2OVER
        {
            get { return _TIME2OVER; }
            set { _TIME2OVER = value; RaisePropertyChanged(nameof(TIME2OVER)); }
        }


        private Decimal _TIME2UNDER;
        public Decimal TIME2UNDER
        {
            get { return _TIME2UNDER; }
            set { _TIME2UNDER = value; RaisePropertyChanged(nameof(TIME2UNDER)); }
        }




        private bool _ENTRYHEIGHT;
        public bool ENTRYHEIGHT
        {
            get { return _ENTRYHEIGHT; }
            set { _ENTRYHEIGHT = value; RaisePropertyChanged(nameof(ENTRYHEIGHT)); }
        }



        private Decimal _SUBFROMLANDING1;
        public Decimal SUBFROMLANDING1
        {
            get { return _SUBFROMLANDING1; }
            set { _SUBFROMLANDING1 = value; RaisePropertyChanged(nameof(SUBFROMLANDING1)); }
        }


        private Decimal _SUBFROMLANDING2;
        public Decimal SUBFROMLANDING2
        {
            get { return _SUBFROMLANDING2; }
            set { _SUBFROMLANDING2 = value; RaisePropertyChanged(nameof(SUBFROMLANDING2)); }
        }



        private Decimal _SUBFROMTIME1;
        public Decimal SUBFROMTIME1
        {
            get { return _SUBFROMTIME1; }
            set { _SUBFROMTIME1 = value; RaisePropertyChanged(nameof(SUBFROMTIME1)); }
        }


        private Decimal _SUBFROMTIME2;
        public Decimal SUBFROMTIME2
        {
            get { return _SUBFROMTIME2; }
            set { _SUBFROMTIME2 = value; RaisePropertyChanged(nameof(SUBFROMTIME2)); }
        }


        private bool _DELETELANDING1;
        public bool DELETELANDING1
        {
            get { return _DELETELANDING1; }
            set { _DELETELANDING1 = value; RaisePropertyChanged(nameof(DELETELANDING1)); }
        }


        private bool _DELETELANDING2;
        public bool DELETELANDING2
        {
            get { return _DELETELANDING2; }
            set { _DELETELANDING2 = value; RaisePropertyChanged(nameof(DELETELANDING2)); }
        }


        private bool _DELETETIME1;
        public bool DELETETIME1
        {
            get { return _DELETETIME1; }
            set { _DELETETIME1 = value; RaisePropertyChanged(nameof(DELETETIME1)); }
        }


        private bool _DELETETIME2;
        public bool DELETETIME2
        {
            get { return _DELETETIME2; }
            set { _DELETETIME2 = value; RaisePropertyChanged(nameof(DELETETIME2)); }
        }


        private bool _DELETEALL1;
        public bool DELETEALL1
        {
            get { return _DELETEALL1; }
            set { _DELETEALL1 = value; RaisePropertyChanged(nameof(DELETEALL1)); }
        }


        private bool _DELETEALL2;
        public bool DELETEALL2
        {
            get { return _DELETEALL2; }
            set { _DELETEALL2 = value; RaisePropertyChanged(nameof(DELETEALL2)); }
        }



        private int _BASEROUNDLENGHT;
        public int BASEROUNDLENGHT
        {
            get { return _BASEROUNDLENGHT; }
            set { _BASEROUNDLENGHT = value; RaisePropertyChanged(nameof(BASEROUNDLENGHT)); }
        }

        private int _BASEROUNDMAXTIME;
        public int BASEROUNDMAXTIME
        {
            get { return _BASEROUNDMAXTIME; }
            set { _BASEROUNDMAXTIME = value; RaisePropertyChanged(nameof(BASEROUNDMAXTIME)); }
        }


        private int _FINALROUNDLENGHT;
        public int FINALROUNDLENGHT
        {
            get { return _FINALROUNDLENGHT; }
            set { _FINALROUNDLENGHT = value; RaisePropertyChanged(nameof(FINALROUNDLENGHT)); }
        }

        private int _FINALROUNDMAXTIME;
        public int FINALROUNDMAXTIME
        {
            get { return _FINALROUNDMAXTIME; }
            set { _FINALROUNDMAXTIME = value; RaisePropertyChanged(nameof(FINALROUNDMAXTIME)); }
        }



        private bool _BONUSONLYFORFINALIST;
        public bool BONUSONLYFORFINALIST
        {
            get { return _BONUSONLYFORFINALIST; }
            set { _BONUSONLYFORFINALIST = value; RaisePropertyChanged(nameof(BONUSONLYFORFINALIST)); }
        }


        private bool _RECTO1000FROMABSMAX;
        public bool RECTO1000FROMABSMAX
        {
            get { return _RECTO1000FROMABSMAX; }
            set { _RECTO1000FROMABSMAX = value; RaisePropertyChanged(nameof(RECTO1000FROMABSMAX)); }
        }



        private bool _RECOUNTSCORETO1000;
        public bool RECOUNTSCORETO1000
        {
            get { return _RECOUNTSCORETO1000; }
            set { _RECOUNTSCORETO1000 = value; RaisePropertyChanged(nameof(RECOUNTSCORETO1000)); }
        }



    }


    public class MODEL_Contests_files : MODEL_BaseClass
    {






        // Default constructor for Player
        public MODEL_Contests_files()
        {

        }

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
        }

        private string _FILENAME;
        public string FILENAME
        {
            get { return _FILENAME; }
            set { _FILENAME = value; RaisePropertyChanged(nameof(FILENAME)); }
        }


        private string _CATEGORY;
        public string CATEGORY
        {
            get { return _CATEGORY; }
            set { _CATEGORY = value; RaisePropertyChanged(nameof(CATEGORY)); }
        }


        private string _NAME;
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; RaisePropertyChanged(nameof(NAME)); }
        }

        private string _LOCATION;
        public string LOCATION
        {
            get { return _LOCATION; }
            set { _LOCATION = value; RaisePropertyChanged(nameof(LOCATION)); }
        }

        private string _DATE;
        public string DATE
        {
            get { return _DATE; }
            set { _DATE = value; RaisePropertyChanged(nameof(DATE)); }
        }

        private string _COMPETITORS;
        public string COMPETITORS
        {
            get { return _COMPETITORS; }
            set { _COMPETITORS = value; RaisePropertyChanged(nameof(COMPETITORS)); }
        }

        private string _SMCRID;
        public string SMCRID
        {
            get { return _SMCRID; }
            set { _SMCRID  = value; RaisePropertyChanged(nameof(SMCRID)); }
        }


        private string _COUNTRY;
        public string COUNTRY
        {
            get { return _COUNTRY; }
            set { _COUNTRY = value; RaisePropertyChanged(nameof(COUNTRY)); }
        }



    }







    public class MODEL_Contests_categories : MODEL_BaseClass
    {






        // Default constructor for Player
        public MODEL_Contests_categories()
        {

        }

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
        }

        private string _CATEGORY;
        public string CATEGORY
        {
            get { return _CATEGORY; }
            set { _CATEGORY = value; RaisePropertyChanged(nameof(CATEGORY)); }
        }

        private string _ADRESS;
        public string ADRESS
        {
            get { return _ADRESS; }
            set { _ADRESS = value; RaisePropertyChanged(nameof(ADRESS)); }
        }


    }





    public class MODEL_CATEGORY_LANDING : MODEL_BaseClass
    {






        // Default constructor for Player
        public MODEL_CATEGORY_LANDING()
        {

        }

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
        }

        private int _CATEGORY;
        public int CATEGORY
        {
            get { return _CATEGORY; }
            set { _CATEGORY = value; RaisePropertyChanged(nameof(CATEGORY)); }
        }



        private int _VALUE;
        public int VALUE
        {
            get { return _VALUE; }
            set { _VALUE = value; RaisePropertyChanged(nameof(VALUE)); }
        }



        private string _TEXTVALUE;
        public string TEXTVALUE
        {
            get { return _TEXTVALUE; }
            set { _TEXTVALUE = value; RaisePropertyChanged(nameof(TEXTVALUE)); }
        }


        private string _LENGHT;
        public string LENGHT
        {
            get { return _LENGHT; }
            set { _LENGHT = value; RaisePropertyChanged(nameof(LENGHT)); }
        }


        private int _TODEL;
        public int TODEL
        {
            get { return _TODEL; }
            set { _TODEL = value; RaisePropertyChanged(nameof(TODEL)); }
        }

    }





    public class MODEL_CATEGORY_PENALISATIONS : MODEL_BaseClass
    {






        // Default constructor for Player
        public MODEL_CATEGORY_PENALISATIONS()
        {

        }

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; RaisePropertyChanged(nameof(ID)); }
        }

        private int _CATEGORY;
        public int CATEGORY
        {
            get { return _CATEGORY; }
            set { _CATEGORY = value; RaisePropertyChanged(nameof(CATEGORY)); }
        }



        private int _VALUE;
        public int VALUE
        {
            get { return _VALUE; }
            set { _VALUE = value; RaisePropertyChanged(nameof(VALUE)); }
        }



        private string _TEXTVALUE;
        public string TEXTVALUE
        {
            get { return _TEXTVALUE; }
            set { _TEXTVALUE = value; RaisePropertyChanged(nameof(TEXTVALUE)); }
        }


        private string _DELETE_LANDING;
        public string DELETE_LANDING
        {
            get { return _DELETE_LANDING; }
            set { _DELETE_LANDING = value; RaisePropertyChanged(nameof(DELETE_LANDING)); }
        }

        private string _DELETE_TIME;
        public string DELETE_TIME
        {
            get { return _DELETE_TIME; }
            set { _DELETE_TIME = value; RaisePropertyChanged(nameof(DELETE_TIME)); }
        }

        private string _DELETE_ALL;
        public string DELETE_ALL
        {
            get { return _DELETE_ALL; }
            set { _DELETE_ALL = value; RaisePropertyChanged(nameof(DELETE_ALL)); }
        }


        private int _TODEL;
        public int TODEL
        {
            get { return _TODEL; }
            set { _TODEL = value; RaisePropertyChanged(nameof(TODEL)); }
        }

    }




}


