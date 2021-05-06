using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp6.Model;
using System.Globalization;


namespace WpfApp6.View
{
    /// <summary>
    /// Interaction logic for PlayersView.xaml
    /// </summary>
    public partial class Rounds : UserControl
    {
        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;
        bool _isscoreentryopen = false;

        public Rounds()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            string tag = (sender as MahApps.Metro.Controls.Tile).Tag.ToString();
            VM.BIND_VIEWED_ROUND = int.Parse(tag);
            VM.FUNCTION_CHECK_ENTERED_ALL();
            VM.FUNCTION_ROUNDS_LOAD_GROUPS(VM.BIND_VIEWED_ROUND);
            VM.FUNCTION_ROUNDS_LOAD_ROUNDS();

            for (int i = 0; i < VM.MODEL_CONTEST_GROUPS.Count; i++)
            {
                VM.MODEL_CONTEST_GROUPS[i].ISSELECTED = "---";
            }

            Console.WriteLine(VM.FUNCTION_KOLIK_JE_SKUPIN_V_KOLE(VM.BIND_VIEWED_ROUND, "", false));
            Console.WriteLine("XXXXXXXX");
            if (VM.BIND_VIEWED_GROUP > VM.FUNCTION_KOLIK_JE_SKUPIN_V_KOLE(VM.BIND_VIEWED_ROUND,"",false)) { 
                VM.BIND_VIEWED_GROUP = VM.FUNCTION_KOLIK_JE_SKUPIN_V_KOLE(VM.BIND_VIEWED_ROUND, "", false); 
            }
            VM.MODEL_CONTEST_GROUPS[VM.BIND_VIEWED_GROUP - 1].ISSELECTED = "selected";


            for (int i = 0; i < VM.MODEL_CONTEST_ROUNDS.Count; i++)
            {
                VM.MODEL_CONTEST_ROUNDS[i].ISSELECTED = "---";
            }
            VM.MODEL_CONTEST_ROUNDS[VM.BIND_VIEWED_ROUND - 1].ISSELECTED = "selected";




        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string tag = (sender as Button).Tag.ToString();
            VM.BIND_VIEWED_GROUP = int.Parse(tag);

            for (int i = 0; i < VM.MODEL_CONTEST_GROUPS.Count; i++)
            {
                VM.MODEL_CONTEST_GROUPS[i].ISSELECTED = "---";
            }

            VM.FUNCTION_CHECK_ENTERED_ALL();
            VM.FUNCTION_ROUNDS_LOAD_GROUPS(VM.BIND_VIEWED_ROUND);
            VM.FUNCTION_ROUNDS_LOAD_ROUNDS();
            VM.MODEL_CONTEST_ROUNDS[VM.BIND_VIEWED_ROUND - 1].ISSELECTED = "selected";
            VM.MODEL_CONTEST_GROUPS[VM.BIND_VIEWED_GROUP - 1].ISSELECTED = "selected";

            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            VM.BIND_SELECTED_ROUND = VM.BIND_VIEWED_ROUND;
            VM.BIND_SELECTED_GROUP = VM.BIND_VIEWED_GROUP;
            VM.FUNCTION_SELECTED_ROUND_FLYING_USERS(0, 0);
            int _tmp_selected_group = VM.BIND_SELECTED_GROUP;
            int _tmp_selected_round = VM.BIND_SELECTED_ROUND;
            _tmp_selected_group += 1;
            if (_tmp_selected_group > VM.BIND_SQL_SOUTEZ_GROUPS) { _tmp_selected_group = 1; _tmp_selected_round += 1; }
            if (_tmp_selected_round > VM.MODEL_CONTEST_ROUNDS.Count)
            {
                VM.BIND_NEXTROUND_TEXT = "Žádny další let není k dispozici";
            }
            else
            {
                VM.BIND_NEXTROUND_TEXT = "Vybrat další let : " + _tmp_selected_round + " / " + _tmp_selected_group;
            }

            VM.BINDING_selectedmenuindex = 8;
        }




        private void Button_NEXT_ROUND(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_MOVE_GROUP_UP_DOWN(+1);
        }

        private void Button_PREW_ROUND(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_MOVE_GROUP_UP_DOWN(-1);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Button tagbutton = sender as Button;
            VM.BIND_VIEWED_STARTPOINT = int.Parse(tagbutton.Tag.ToString());
            show_scoreentry_form();
        }


        private void show_scoreentry_form()
        {
            Console.WriteLine(VM.BIND_VIEWED_ROUND + "_" + VM.BIND_VIEWED_GROUP + "_" + VM.BIND_VIEWED_STARTPOINT);
            VM.FUNCTION_SCOREENTRY_FROMROUNDSLIST_LOAD_USERDATA(0, 0, 0);
            scoreentry.IsOpen = true;
            scoreentry_minutes.Focus();
            savescore_event(true);
            _isscoreentryopen = true;
            scoreentry_height.IsEnabled = VM.MODEL_CONTEST_RULES[0].ENTRYHEIGHT;

        }


        private void scoreentry_back_Click(object sender, RoutedEventArgs e)
        {
               scoreentry.IsOpen = false;
            _isscoreentryopen = false;
        }

      
        private void scoreentry_minutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            aktualscore.Content = "--XX--";

            Console.Write("_isscoreentryopen" + _isscoreentryopen);
            try
            {


                if (VM != null)
                {
                    if (_isscoreentryopen == true)
                    {
                        savescore_event(true);
                    }
                }


            }
            catch (Exception aaa)
            {
                Console.WriteLine(aaa.Message);
            }


        }

        private void scoreentry_save_Click(object sender, RoutedEventArgs e)
        {
            savescore_event(false);
        }




        public void savescore_event(bool quick_partial_save)
        {

            Console.WriteLine(quick_partial_save);

            if (quick_partial_save == true)
            {

                if (VM.bind_scoreentry_fromroundlist_selected_minute > 0 & VM.bind_scoreentry_fromroundlist_selected_second > 0 & VM.bind_scoreentry_fromroundlist_selected_landing > 0 & VM.bind_scoreentry_fromroundlist_selected_height > 0)
                {


                int _tmp_height = scoreentry_height.SelectedIndex;
                if (_tmp_height < 0) { _tmp_height = 0; }




                int tmpheight = VM.BINDING_Timer_listofheights[_tmp_height].Value;
                int _tmp_min = scoreentry_minutes.SelectedIndex;
                if (_tmp_min < 0) { _tmp_min = 0; }
                int _tmp_sec = scoreentry_seconds.SelectedIndex;
                if (_tmp_sec < 0) { _tmp_sec = 0; }
                int _tmp_land = VM.BINDING_Timer_listoflandings[scoreentry_landing.SelectedIndex].VALUE;
                if (_tmp_land < 0) { _tmp_land = 0; }

                Decimal _RAWSCORE = 0;

                // time section
                Decimal _time_points = 0;
                int _time = (_tmp_min * 60) + _tmp_sec;
                if (_time > VM.MODEL_CONTEST_RULES[0].TIME1LIMIT)
                {
                    Decimal _timeunder = VM.MODEL_CONTEST_RULES[0].TIME1LIMIT * VM.MODEL_CONTEST_RULES[0].TIME1UNDER;
                    Decimal _timeover = (_time - VM.MODEL_CONTEST_RULES[0].TIME1LIMIT) * VM.MODEL_CONTEST_RULES[0].TIME1OVER;
                    _time_points = _timeunder + _timeover;

                    if (VM.MODEL_CONTEST_RULES[0].DELETETIME1 == true)
                    {
                        _time_points = 0;
                    }

                    if (_time > VM.MODEL_CONTEST_RULES[0].TIME2LIMIT && VM.MODEL_CONTEST_RULES[0].DELETETIME1 == true)
                    {
                        _time_points = 0;
                    }


                }
                else
                {
                    _time_points = _time * VM.MODEL_CONTEST_RULES[0].TIME1UNDER;
                }


                //height section


                Decimal _height_points = 0;
                int _height = _tmp_height;
                if (_height > VM.MODEL_CONTEST_RULES[0].HEIGHTLIMIT)
                {

                    Decimal _heightunder = VM.MODEL_CONTEST_RULES[0].HEIGHTLIMIT * VM.MODEL_CONTEST_RULES[0].HEIGHTUNDER;
                    Decimal _heightover = (_height - VM.MODEL_CONTEST_RULES[0].HEIGHTLIMIT) * VM.MODEL_CONTEST_RULES[0].HEIGHTOVER;

                    _height_points = _heightunder + _heightover;


                }
                else
                {
                    _height_points = _height * VM.MODEL_CONTEST_RULES[0].HEIGHTUNDER;
                }


                int _landings = _tmp_land;

                if (_time > VM.MODEL_CONTEST_RULES[0].TIME1LIMIT && VM.MODEL_CONTEST_RULES[0].DELETELANDING1 == true)
                {
                    _landings = 0;
                }
                if (_time > VM.MODEL_CONTEST_RULES[0].TIME2LIMIT && VM.MODEL_CONTEST_RULES[0].DELETELANDING2 == true)
                {
                    _landings = 0;
                }

                _RAWSCORE = _time_points + _height_points + _landings;


                Console.WriteLine("_RAWSCORE is " + _RAWSCORE);

                if (scoreentry_penlocal.SelectedIndex >= 0)
                {
                    if (VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].DELETE_LANDING == "True")
                    {
                        _RAWSCORE = _RAWSCORE - _landings;
                    }

                    if (VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].DELETE_TIME == "True")
                    {
                        _RAWSCORE = _RAWSCORE - _time_points;
                    }

                    if (VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].DELETE_ALL == "True")
                    {
                        _RAWSCORE = 0;
                    }

                    _RAWSCORE = _RAWSCORE + (VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].VALUE);
                }


                Console.WriteLine("pen is " + VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].VALUE);
                Console.WriteLine("fixed rawscore is " + _RAWSCORE);


                if (_time > VM.MODEL_CONTEST_RULES[0].TIME1LIMIT && VM.MODEL_CONTEST_RULES[0].DELETEALL1 == true)
                {
                    _RAWSCORE = 0;
                }

                if (_time > VM.MODEL_CONTEST_RULES[0].TIME2LIMIT && VM.MODEL_CONTEST_RULES[0].DELETEALL2 == true)
                {
                    _RAWSCORE = 0;
                }



                string _RAWSCORE_STR = _RAWSCORE.ToString(new CultureInfo("en-US"));

                VM.SQL_SAVESOUTEZDATA("update score set raw = " + _RAWSCORE_STR + " where rnd = " + VM.BIND_VIEWED_ROUND + " and grp = " + VM.BIND_VIEWED_GROUP + " and stp = " + VM.BIND_VIEWED_STARTPOINT);
                Decimal _MAXRAW = Decimal.Parse(VM.SQL_READSOUTEZDATA("select max(raw) FROM score s where s.rnd = " + VM.BIND_VIEWED_ROUND + " and s.grp = " + VM.BIND_VIEWED_GROUP, ""));
                Decimal _PREPSCORE = 0;

                if (_MAXRAW != 0)
                {
                    _PREPSCORE = Math.Round((_RAWSCORE / _MAXRAW) * 1000, 2);

                }
                string _PREPSCORE_STR = _PREPSCORE.ToString(new CultureInfo("en-US"));


                VM.Player_Selected_Roundlist[0].SCORE_RAW = _RAWSCORE_STR;
                VM.Player_Selected_Roundlist[0].SCORE_PREP = _PREPSCORE_STR;

                VM.FUNCTION_SCOREENTRY_SAVE_SCORE(VM.BIND_VIEWED_ROUND, VM.BIND_VIEWED_GROUP, VM.BIND_VIEWED_STARTPOINT, VM.Player_Selected_Roundlist[0].ID, VM.BINDING_Timer_listofminutes[scoreentry_minutes.SelectedIndex].Value, VM.BINDING_Timer_listofseconds[scoreentry_seconds.SelectedIndex].Value, VM.BINDING_Timer_listoflandings[scoreentry_landing.SelectedIndex].VALUE, VM.BINDING_Timer_listofheights[scoreentry_height.SelectedIndex].Value, VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].VALUE, VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].ID, VM.BINDING_Timer_listofpenalisationglobal[scoreentry_penglobal.SelectedIndex].VALUE, VM.BINDING_Timer_listofpenalisationglobal[scoreentry_penglobal.SelectedIndex].ID, VM.Player_Selected_Roundlist[0].SCORE_RAW, VM.Player_Selected_Roundlist[0].SCORE_PREP);
                VM.Player_Selected_Roundlist[0].SCORE_RAW = _RAWSCORE_STR;
                VM.Player_Selected_Roundlist[0].SCORE_PREP = _PREPSCORE_STR;
                Console.WriteLine(VM.Player_Selected_Roundlist[0].SCORE_RAW);
                Console.WriteLine(VM.Player_Selected_Roundlist[0].SCORE_PREP);
                aktualscore.Content = "SCORE : " + VM.Player_Selected_Roundlist[0].SCORE_PREP + " ==  [ RAW : " + VM.Player_Selected_Roundlist[0].SCORE_RAW + " ]";


            }


        }
            else
            {
                if (scoreentry_minutes.SelectedIndex >= 0 & scoreentry_seconds.SelectedIndex >= 0 & scoreentry_height.SelectedIndex >= 0 & scoreentry_landing.SelectedIndex >= 0)

                {





                    Decimal _RAWSCORE = 0;

                    // time section
                    Decimal _time_points = 0;
                    int _time = (VM.BINDING_Timer_listofminutes[scoreentry_minutes.SelectedIndex].Value * 60) + VM.BINDING_Timer_listofseconds[scoreentry_seconds.SelectedIndex].Value;


                    if (_time > VM.MODEL_CONTEST_RULES[0].TIME1LIMIT)
                    {

                        Decimal _timeunder = VM.MODEL_CONTEST_RULES[0].TIME1LIMIT * VM.MODEL_CONTEST_RULES[0].TIME1UNDER;
                        Decimal _timeover = (_time - VM.MODEL_CONTEST_RULES[0].TIME1LIMIT) * VM.MODEL_CONTEST_RULES[0].TIME1OVER;
                        _time_points = _timeunder + _timeover;

                        if (VM.MODEL_CONTEST_RULES[0].DELETETIME1 == true)
                        {
                            _time_points = 0;
                        }

                        if (_time > VM.MODEL_CONTEST_RULES[0].TIME2LIMIT && VM.MODEL_CONTEST_RULES[0].DELETETIME1 == true)
                        {
                            _time_points = 0;
                        }




                    }

                    else
                    {
                        _time_points = _time * VM.MODEL_CONTEST_RULES[0].TIME1UNDER;
                    }





                    //height section


                    Decimal _height_points = 0;
                    int _height = VM.BINDING_Timer_listofheights[scoreentry_height.SelectedIndex].Value;
                    if (_height > VM.MODEL_CONTEST_RULES[0].HEIGHTLIMIT)
                    {

                        Decimal _heightunder = VM.MODEL_CONTEST_RULES[0].HEIGHTLIMIT * VM.MODEL_CONTEST_RULES[0].HEIGHTUNDER;
                        Decimal _heightover = (_height - VM.MODEL_CONTEST_RULES[0].HEIGHTLIMIT) * VM.MODEL_CONTEST_RULES[0].HEIGHTOVER;

                        _height_points = _heightunder + _heightover;


                    }
                    else
                    {
                        _height_points = _height * VM.MODEL_CONTEST_RULES[0].HEIGHTUNDER;
                    }


                    int _landings = VM.BINDING_Timer_listoflandings[scoreentry_landing.SelectedIndex].VALUE;

                    if (_time > VM.MODEL_CONTEST_RULES[0].TIME1LIMIT && VM.MODEL_CONTEST_RULES[0].DELETELANDING1 == true)
                    {
                        _landings = 0;
                    }
                    if (_time > VM.MODEL_CONTEST_RULES[0].TIME2LIMIT && VM.MODEL_CONTEST_RULES[0].DELETELANDING2 == true)
                    {
                        _landings = 0;
                    }


                    _RAWSCORE = _time_points + _height_points + _landings;






                    if (scoreentry_penlocal.SelectedIndex >= 0)
                    {

                        if (VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].DELETE_LANDING == "True")
                        {
                            _RAWSCORE = _RAWSCORE - _landings;
                        }

                        if (VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].DELETE_TIME == "True")
                        {
                            _RAWSCORE = _RAWSCORE - _time_points;
                        }

                        if (VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].DELETE_ALL == "True")
                        {
                            _RAWSCORE = 0;
                        }

                        _RAWSCORE = _RAWSCORE + (VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].VALUE);
                    }




                    if (_time > VM.MODEL_CONTEST_RULES[0].TIME1LIMIT && VM.MODEL_CONTEST_RULES[0].DELETEALL1 == true)
                    {
                        _RAWSCORE = 0;
                    }

                    if (_time > VM.MODEL_CONTEST_RULES[0].TIME2LIMIT && VM.MODEL_CONTEST_RULES[0].DELETEALL2 == true)
                    {
                        _RAWSCORE = 0;
                    }




                    string _RAWSCORE_STR = _RAWSCORE.ToString(new CultureInfo("en-US"));

                    VM.SQL_SAVESOUTEZDATA("update score set raw = " + _RAWSCORE_STR + " where rnd = " + VM.BIND_VIEWED_ROUND + " and grp = " + VM.BIND_VIEWED_GROUP + " and stp = " + VM.BIND_VIEWED_STARTPOINT);

                    Decimal _MAXRAW = Decimal.Parse(VM.SQL_READSOUTEZDATA("select max(raw) FROM score s where s.rnd = " + VM.BIND_VIEWED_ROUND + " and s.grp = " + VM.BIND_VIEWED_GROUP, ""));

                    Decimal _PREPSCORE = 0;

                    if (_MAXRAW != 0)
                    {
                        _PREPSCORE = Math.Round((_RAWSCORE / _MAXRAW) * 1000, 2);
                    }




                    string _PREPSCORE_STR = _PREPSCORE.ToString(new CultureInfo("en-US"));


                    VM.Player_Selected_Roundlist[0].SCORE_RAW = _RAWSCORE_STR;
                    VM.Player_Selected_Roundlist[0].SCORE_PREP = _PREPSCORE_STR;

                    VM.FUNCTION_SCOREENTRY_SAVE_SCORE(VM.BIND_VIEWED_ROUND, VM.BIND_VIEWED_GROUP, VM.BIND_VIEWED_STARTPOINT, VM.Player_Selected_Roundlist[0].ID, VM.BINDING_Timer_listofminutes[scoreentry_minutes.SelectedIndex].Value, VM.BINDING_Timer_listofseconds[scoreentry_seconds.SelectedIndex].Value, VM.BINDING_Timer_listoflandings[scoreentry_landing.SelectedIndex].VALUE, VM.BINDING_Timer_listofheights[scoreentry_height.SelectedIndex].Value, VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].VALUE, VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].ID, VM.BINDING_Timer_listofpenalisationglobal[scoreentry_penglobal.SelectedIndex].VALUE, VM.BINDING_Timer_listofpenalisationglobal[scoreentry_penglobal.SelectedIndex].ID, VM.Player_Selected_Roundlist[0].SCORE_RAW, VM.Player_Selected_Roundlist[0].SCORE_PREP);


                    
                    scoreentry.IsOpen = false;
                    _isscoreentryopen = false;
                    //HWbasemodul_Copy4s.Focus();
                    VM.FUNCTION_CHECK_ENTERED_ALL();
                    VM.FUNCTION_SELECTED_ROUND_USERS(VM.BIND_VIEWED_ROUND, VM.BIND_VIEWED_GROUP);
                   
                    VM.FUNCTION_ROUNDS_LOAD_ROUNDS();
                    VM.FUNCTION_ROUNDS_LOAD_GROUPS(VM.BIND_VIEWED_ROUND);

                    for (int i = 0; i < VM.MODEL_CONTEST_GROUPS.Count; i++)
                    {
                        VM.MODEL_CONTEST_GROUPS[i].ISSELECTED = "---";
                    }
                    VM.MODEL_CONTEST_GROUPS[VM.BIND_VIEWED_GROUP - 1].ISSELECTED = "selected";


                    for (int i = 0; i < VM.MODEL_CONTEST_ROUNDS.Count; i++)
                    {
                        VM.MODEL_CONTEST_ROUNDS[i].ISSELECTED = "---";
                    }
                    VM.MODEL_CONTEST_ROUNDS[VM.BIND_VIEWED_ROUND - 1].ISSELECTED = "selected";




                }


                if (VM.BIND_SQL_SOUTEZ_ENTRYSTYLENEXT == true)
                {
                    Console.WriteLine("VM.BIND_VIEWED_STARTPOINT" + VM.BIND_VIEWED_STARTPOINT);
                    Console.WriteLine("VM.BIND_SQL_SOUTEZ_STARTPOINTS" + VM.BIND_SQL_SOUTEZ_STARTPOINTS);
                    znova:
                    if (VM.BIND_VIEWED_STARTPOINT < VM.BIND_SQL_SOUTEZ_STARTPOINTS)
                    {
                        VM.BIND_VIEWED_STARTPOINT += 1;
                        if ( int.Parse(VM.SQL_READSOUTEZDATA("select userid from score where rnd="+ VM.BIND_VIEWED_ROUND + " and grp=" + VM.BIND_VIEWED_GROUP + " and stp=" + VM.BIND_VIEWED_STARTPOINT, ""))>0)
                        {
                            show_scoreentry_form();
                        }
                        else
                        {
                            goto znova;
                        }
                    }
                }



            }





        }




        private void scoreentry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                savescore_event(false);

            }
        }

       
    }
}
