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



        private int _ROUNDZADANO;
        public int ROUNDZADANO
        {
            get { return _ROUNDZADANO; }
            set { _ROUNDZADANO = value; RaisePropertyChanged(nameof(ROUNDZADANO)); }
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





    }
}
