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




    }




}
