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
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Globalization;

namespace WpfApp6.View
{
    /// <summary>
    /// Interakční logika pro FirstView.xaml
    /// </summary>
    public partial class selectedround : UserControl
    {

        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;
        //        ViewModel XX = new ViewModel(DialogCoordinator.Instance);
        bool _isscoreentryopen = false;
        public selectedround()
        {

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);


            System.Media.SoundPlayer player = new System.Media.SoundPlayer(directory + "/Audio/petiminutoveho.wav");
            player.PlayLooping();
            //'player.

        }

        private async void HWbasemodul_Copy1_Click(object sender, RoutedEventArgs e)
        {

            var currentWindow = this.TryFindParent<MetroWindow>();
            MessageDialogResult result = await currentWindow.ShowMessageAsync("Zastavení odpočtu", "Opravdu chceš ukončit aktuální odpočet času ??", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Negative)
            {
                Console.WriteLine("No");
            }
            else
            {
                Console.WriteLine("yes");
                VM.clock_stop();
                maintimer_play.IsEnabled = true;
                maintimer_stop.IsEnabled = false;
            }





        }

        private void HWbasemodul_Copy2_Click(object sender, RoutedEventArgs e)
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            System.Media.SoundPlayer player = new System.Media.SoundPlayer(directory + "/Audio/petiminutoveho.wav");
            player.Play();
            VM.BIND_LETOVYCAS = 0;
            VM.BIND_LETOVYCAS_MAX = 600;
            maintimer_play.IsEnabled = false;
            maintimer_stop.IsEnabled = true;
            VM.clock_start();



        }

        private void HWbasemodul_Copywe2_Click(object sender, RoutedEventArgs e)
        {
            VM.BIND_LETOVYCAS_MAX = 600;
        }


        private void Button_NEXT_ROUND(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_MOVE_GROUP_UP_DOWN(+1);
        }

        private void Button_PREW_ROUND(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_MOVE_GROUP_UP_DOWN(-1);
        }

        private void show_scoreentry_form()
        {
            Console.WriteLine(VM.BIND_SELECTED_ROUND + "_" + VM.BIND_SELECTED_GROUP + "_" + VM.BIND_SELECTED_STARTPOINT);
            VM.FUNCTION_SCOREENTRY_LOAD_USERDATA(0, 0, 0);
            scoreentry.IsOpen = true;
            scoreentry_minutes.Focus();
            savescore_event(true);
            _isscoreentryopen = true;
            scoreentry_height.IsEnabled = VM.MODEL_CONTEST_RULES[0].ENTRYHEIGHT;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            Button tagbutton = sender as Button;
            VM.BIND_SELECTED_STARTPOINT = int.Parse(tagbutton.Tag.ToString());
            show_scoreentry_form();

        }


        private void scoreentry_save_Click(object sender, RoutedEventArgs e)
        {

            

                savescore_event(false);

           

        }

        private void scoreentry_back_Click(object sender, RoutedEventArgs e)
        {
            //scoreentry_landing.SelectedIndex = 5;
            //scoreentry.IsOpen = false;
            _isscoreentryopen = false;

        }


        public void savescore_event(bool quick_partial_save)
        {

            Console.WriteLine(quick_partial_save);

            if (quick_partial_save == true)
            {

                if (VM.bind_scoreentry_selected_minute > 0 & VM.bind_scoreentry_selected_second > 0 & VM.bind_scoreentry_selected_landing > 0 & VM.bind_scoreentry_selected_height > 0)
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

                if (scoreentry_penlocal.SelectedIndex >= 0 )
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

                VM.SQL_SAVESOUTEZDATA("update score set raw = " + _RAWSCORE_STR + " where rnd = " + VM.BIND_SELECTED_ROUND + " and grp = " + VM.BIND_SELECTED_GROUP + " and stp = " + VM.BIND_SELECTED_STARTPOINT);
                Decimal _MAXRAW = Decimal.Parse(VM.SQL_READSOUTEZDATA("select max(raw) FROM score s where s.rnd = " + VM.BIND_SELECTED_ROUND + " and s.grp = " + VM.BIND_SELECTED_GROUP, ""));
                Decimal _PREPSCORE = 0;

                if (_MAXRAW != 0)
                {
                    _PREPSCORE = Math.Round((_RAWSCORE / _MAXRAW) * 1000, 2);

                }
                string _PREPSCORE_STR = _PREPSCORE.ToString(new CultureInfo("en-US"));


                VM.Player_Selected[0].SCORE_RAW = _RAWSCORE_STR;
                VM.Player_Selected[0].SCORE_PREP = _PREPSCORE_STR;

                VM.FUNCTION_SCOREENTRY_SAVE_SCORE(VM.BIND_SELECTED_ROUND, VM.BIND_SELECTED_GROUP, VM.BIND_SELECTED_STARTPOINT, VM.Player_Selected[0].ID, VM.BINDING_Timer_listofminutes[scoreentry_minutes.SelectedIndex].Value, VM.BINDING_Timer_listofseconds[scoreentry_seconds.SelectedIndex].Value, VM.BINDING_Timer_listoflandings[scoreentry_landing.SelectedIndex].VALUE , VM.BINDING_Timer_listofheights[scoreentry_height.SelectedIndex].Value, VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].VALUE, VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].ID, VM.BINDING_Timer_listofpenalisationglobal[scoreentry_penglobal.SelectedIndex].VALUE, VM.BINDING_Timer_listofpenalisationglobal[scoreentry_penglobal.SelectedIndex].ID, VM.Player_Selected[0].SCORE_RAW, VM.Player_Selected[0].SCORE_PREP);
                VM.Player_Selected[0].SCORE_RAW = _RAWSCORE_STR;
                VM.Player_Selected[0].SCORE_PREP = _PREPSCORE_STR;
                Console.WriteLine(VM.Player_Selected[0].SCORE_RAW);
                Console.WriteLine(VM.Player_Selected[0].SCORE_PREP);
                aktualscore.Content = "SCORE : " + VM.Player_Selected[0].SCORE_PREP + " ==  [ RAW : " + VM.Player_Selected[0].SCORE_RAW + " ]";

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

                    VM.SQL_SAVESOUTEZDATA("update score set raw = " + _RAWSCORE_STR + " where rnd = " + VM.BIND_SELECTED_ROUND + " and grp = " + VM.BIND_SELECTED_GROUP + " and stp = "+ VM.BIND_SELECTED_STARTPOINT);

                    Decimal _MAXRAW = Decimal.Parse(VM.SQL_READSOUTEZDATA("select max(raw) FROM score s where s.rnd = " + VM.BIND_SELECTED_ROUND + " and s.grp = " + VM.BIND_SELECTED_GROUP, ""));

                    Decimal _PREPSCORE = 0;

                    if (_MAXRAW != 0)
                    {
                        _PREPSCORE = Math.Round((_RAWSCORE / _MAXRAW) * 1000, 2);
                    }


                    

                    string _PREPSCORE_STR = _PREPSCORE.ToString(new CultureInfo("en-US"));


                    VM.Player_Selected[0].SCORE_RAW = _RAWSCORE_STR;
                    VM.Player_Selected[0].SCORE_PREP = _PREPSCORE_STR;

                    VM.FUNCTION_SCOREENTRY_SAVE_SCORE(VM.BIND_SELECTED_ROUND, VM.BIND_SELECTED_GROUP, VM.BIND_SELECTED_STARTPOINT, VM.Player_Selected[0].ID, VM.BINDING_Timer_listofminutes[scoreentry_minutes.SelectedIndex].Value, VM.BINDING_Timer_listofseconds[scoreentry_seconds.SelectedIndex].Value, VM.BINDING_Timer_listoflandings[scoreentry_landing.SelectedIndex].VALUE, VM.BINDING_Timer_listofheights[scoreentry_height.SelectedIndex].Value, VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].VALUE, VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].ID, VM.BINDING_Timer_listofpenalisationglobal[scoreentry_penglobal.SelectedIndex].VALUE, VM.BINDING_Timer_listofpenalisationglobal[scoreentry_penglobal.SelectedIndex].ID, VM.Player_Selected[0].SCORE_RAW , VM.Player_Selected[0].SCORE_PREP);

                    VM.FUNCTION_CHECK_ENTERED(VM.BIND_SELECTED_ROUND, VM.BIND_SELECTED_GROUP);
                    VM.FUNCTION_ROUNDS_LOAD_ROUNDS();
                    VM.FUNCTION_ROUNDS_LOAD_GROUPS(VM.BIND_SELECTED_ROUND);


                    VM.FUNCTION_SELECTED_ROUND_FLYING_USERS(0, 0);
                    for (int i = 0; i < VM.MODEL_CONTEST_GROUPS.Count; i++)
                    {
                        VM.MODEL_CONTEST_GROUPS[i].ISSELECTED = "---";
                    }
                    VM.MODEL_CONTEST_GROUPS[VM.BIND_SELECTED_GROUP - 1].ISSELECTED = "selected";


                    for (int i = 0; i < VM.MODEL_CONTEST_ROUNDS.Count; i++)
                    {
                        VM.MODEL_CONTEST_ROUNDS[i].ISSELECTED = "---";
                    }
                    VM.MODEL_CONTEST_ROUNDS[VM.BIND_SELECTED_ROUND - 1].ISSELECTED = "selected";
                    scoreentry.IsOpen = false;
                    _isscoreentryopen = false;
                    HWbasemodul_Copy4s.Focus();

                }


                if (VM.BIND_SQL_SOUTEZ_ENTRYSTYLENEXT == true)
                {
                    Console.WriteLine("VM.BIND_SELECTED_STARTPOINT" + VM.BIND_SELECTED_STARTPOINT);
                    Console.WriteLine("VM.BIND_SQL_SOUTEZ_STARTPOINTS" + VM.BIND_SQL_SOUTEZ_STARTPOINTS);

                    if (VM.BIND_SELECTED_STARTPOINT < VM.BIND_SQL_SOUTEZ_STARTPOINTS)
                    {
                        VM.BIND_SELECTED_STARTPOINT += 1;
                        show_scoreentry_form();
                    }
                }



            }





        }
        private void scoreentry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                savescore_event(false ); 

            }
        }


        private void scoreentry_minutes_SelectionChanged(object sender, RoutedEventArgs e)
        {

            Console.Write("_isscoreentryopen" + _isscoreentryopen);
            try
            {


                if (VM != null)
                {
                    if (scoreentry.IsOpen == true)
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

        private void scoreentry_minutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
