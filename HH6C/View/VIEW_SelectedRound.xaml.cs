﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        private async void maintimer_stop_Click(object sender, RoutedEventArgs e)
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
                VM.clock_MAIN_stop();

            }





        }

        private void maintimer_play_Click(object sender, RoutedEventArgs e)
        {

            //Console.WriteLine(VM.MODEL_CONTEST_SOUNDS_MAIN.Count);

            //for (int i = 0; i < VM.MODEL_CONTEST_SOUNDS_MAIN.Count; i++)
            //{
            //   Console.WriteLine(VM.MODEL_CONTEST_SOUNDS_MAIN[i].VALUE.ToString() + " --- " + VM.MODEL_CONTEST_SOUNDS_MAIN[i].TEXTVALUE.ToString());
            //}

           
            VM.clock_MAIN_start();

        }

        private void HWbasemodul_Copywe2_Click(object sender, RoutedEventArgs e)
        {
            VM.BIND_LETOVYCAS_MAX = 100;
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
            _isscoreentryopen = true;
            scoreentry_height.IsEnabled = VM.MODEL_CONTEST_RULES[0].ENTRYHEIGHT;
            savescore_event(true);

        }


        private void show_refly_form()
        {
            VM.FUNCTION_SCOREENTRY_LOAD_USERDATA(0, 0, 0);
            
            refly.IsOpen = true;
            VM.FUNCTION_SHOW_AVAIABLE_GROUPS(VM.BIND_SELECTED_ROUND);
            string tmp_existujerefly = VM.SQL_READSOUTEZDATA("select userid from refly where rnd_from=" + VM.BIND_SELECTED_ROUND + " and grp_from=" + VM.BIND_SELECTED_GROUP + " and stp_from=" + VM.BIND_SELECTED_STARTPOINT + " ", "");
            if (tmp_existujerefly == "")
            {
                refly_muzebyt.Visibility = Visibility.Visible;
                refly_nemuzebyt.Visibility = Visibility.Collapsed;
            }
            else
            {
                refly_muzebyt.Visibility = Visibility.Collapsed;
                refly_nemuzebyt.Visibility = Visibility.Visible;

                string tmp_cosepocita = VM.SQL_READSOUTEZDATA("select whatcount1 from refly where rnd_from=" + VM.BIND_SELECTED_ROUND + " and grp_from=" + VM.BIND_SELECTED_GROUP + " and stp_from=" + VM.BIND_SELECTED_STARTPOINT + " ", "");

                if (tmp_cosepocita == "1")
                {
                    VM.BIND_DATA_OPAKOVACIHO_LETU = "Počítá se výsledek z opakovacího letu";
                }
                else
                {
                    VM.BIND_DATA_OPAKOVACIHO_LETU = "Počítá se lepší výsledek";
                }
            }
            //scoreentry_minutes.Focus();
            //_isscoreentryopen = true;
            //scoreentry_height.IsEnabled = VM.MODEL_CONTEST_RULES[0].ENTRYHEIGHT;
            //savescore_event(true);

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
            scoreentry.IsOpen = false;
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

                aktualscore.Content = "XXXSCORE : " + VM.Player_Selected[0].SCORE_PREP + " ==  [ RAW : " + VM.Player_Selected[0].SCORE_RAW + " ]";

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


                    //VM.FUNCTION_zjisti_jestli_a_ktery_z_refly_je_pocitany(VM.BIND_SELECTED_ROUND, VM.BIND_SELECTED_GROUP, VM.BIND_SELECTED_STARTPOINT);

                    VM.FUNCTION_CHECK_ENTERED_ALL();
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
                    //HWbasemodul_Copy4s.Focus();

                }




                if (VM.BIND_SQL_SOUTEZ_ENTRYSTYLENEXT == true)
                {
                    Console.WriteLine("VM.BIND_SELECTED_STARTPOINT" + VM.BIND_SELECTED_STARTPOINT);
                    Console.WriteLine("VM.BIND_SQL_SOUTEZ_STARTPOINTS" + VM.BIND_SQL_SOUTEZ_STARTPOINTS);


                    znova:
                    if (VM.BIND_SELECTED_STARTPOINT < VM.BIND_SQL_SOUTEZ_STARTPOINTS)
                    {
                        VM.BIND_SELECTED_STARTPOINT += 1;
                        if (int.Parse(VM.SQL_READSOUTEZDATA("select userid from score where rnd=" + VM.BIND_SELECTED_ROUND + " and grp=" + VM.BIND_SELECTED_GROUP + " and stp=" + VM.BIND_SELECTED_STARTPOINT, "")) > 0)
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
                savescore_event(false ); 

            }
        }


        private void preptimer_play_Click(object sender, RoutedEventArgs e)
        {

            VM.BIND_PREP_AUDIO_MAN_AUTO = true;
            VM.clock_PREP_start();
        }

        private void preptimer_stop_Click(object sender, RoutedEventArgs e)
        {
            VM.clock_PREP_stop();
         
        }

        private void scoreentry_minutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            aktualscore.Content = "--XX--";
            Console.Write("scoreentry.IsOpen" + _isscoreentryopen);
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Button tagbutton = sender as Button;
            VM.BIND_SELECTED_STARTPOINT = int.Parse(tagbutton.Tag.ToString());
            show_refly_form();

        }

        private void save_refly_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void userdetail_id_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Tile_Click(object sender, RoutedEventArgs e)
        {
            Tile tagbutton = sender as Tile;

            int tmp_first_empty_stp = int.Parse(VM.SQL_READSOUTEZDATA("select stp from matrix where user=0 and rnd=" + VM.BIND_SELECTED_ROUND + " and grp=" + int.Parse(tagbutton.Tag.ToString()) + " limit 1", ""));

            if (refly_what_count.IsOn == true)
            {
                VM.SQL_SAVESOUTEZDATA("insert into refly (rnd_from,grp_from,stp_from,rnd_to,grp_to,stp_to,userid,whatcount1,whatcount2) values (" + VM.BIND_SELECTED_ROUND + "," + VM.BIND_SELECTED_GROUP + "," + VM.BIND_SELECTED_STARTPOINT + "," + VM.BIND_SELECTED_ROUND + "," + int.Parse(tagbutton.Tag.ToString()) + ","+ tmp_first_empty_stp+"," + VM.Player_Selected[0].ID + ",1,2);");
            }
            else
            {
                VM.SQL_SAVESOUTEZDATA("insert into refly (rnd_from,grp_from,stp_from,rnd_to,grp_to,stp_to,userid,whatcount1,whatcount2) values (" + VM.BIND_SELECTED_ROUND + "," + VM.BIND_SELECTED_GROUP + "," + VM.BIND_SELECTED_STARTPOINT + "," + VM.BIND_SELECTED_ROUND + "," + int.Parse(tagbutton.Tag.ToString()) + "," + tmp_first_empty_stp + "," + VM.Player_Selected[0].ID + ",0,0);");
            }

            VM.SQL_SAVESOUTEZDATA("update score set userid=" + VM.Player_Selected[0].ID + ", entered='False' where rnd=" + VM.BIND_SELECTED_ROUND + " and grp=" + int.Parse(tagbutton.Tag.ToString()) + " and stp=" + tmp_first_empty_stp + "");
            VM.SQL_SAVESOUTEZDATA("update matrix set user=" + VM.Player_Selected[0].ID + " where rnd=" + VM.BIND_SELECTED_ROUND + " and grp="+ int.Parse(tagbutton.Tag.ToString()) + " and stp=" + tmp_first_empty_stp + "");
           
            var currentWindow = this.TryFindParent<MetroWindow>();
            MessageDialogResult result = await currentWindow.ShowMessageAsync("Přiřazení refly", "Soutěžící byl zařazen k opravnému letu", MessageDialogStyle.Affirmative);


            VM.FUNCTION_CHECK_ENTERED_ALL();
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
            refly.IsOpen = false;

            VM.FUNCTION_CHECK_REFLY(VM.BIND_SELECTED_ROUND, VM.BIND_SELECTED_GROUP);


        }



        private async void delete_refly_Click(object sender, RoutedEventArgs e)
        {


            var currentWindow = this.TryFindParent<MetroWindow>();
            MessageDialogResult result = await currentWindow.ShowMessageAsync("Odstranění refly", "Opravdu chceš odebrat opravný let a jeho výsledky?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Negative)
            {
                Console.WriteLine("No");
            }
            else
            {



                int to_rnd = int.Parse(VM.SQL_READSOUTEZDATA("select rnd_to from refly where rnd_from=" + VM.BIND_SELECTED_ROUND + " and grp_from=" + VM.BIND_SELECTED_GROUP + " and stp_from=" + VM.BIND_SELECTED_STARTPOINT + " ", ""));
                int to_grp = int.Parse(VM.SQL_READSOUTEZDATA("select grp_to from refly where rnd_from=" + VM.BIND_SELECTED_ROUND + " and grp_from=" + VM.BIND_SELECTED_GROUP + " and stp_from=" + VM.BIND_SELECTED_STARTPOINT + " ", ""));
                int to_stp = int.Parse(VM.SQL_READSOUTEZDATA("select stp_to from refly where rnd_from=" + VM.BIND_SELECTED_ROUND + " and grp_from=" + VM.BIND_SELECTED_GROUP + " and stp_from=" + VM.BIND_SELECTED_STARTPOINT + " ", ""));
                VM.SQL_SAVESOUTEZDATA("update score set refly='False' where rnd=" + VM.BIND_SELECTED_ROUND + " and grp=" + VM.BIND_SELECTED_GROUP + " and stp=" + VM.BIND_SELECTED_STARTPOINT + "");
                VM.SQL_SAVESOUTEZDATA("update matrix set user=0 where rnd=" + to_rnd + " and grp=" + to_grp + " and stp=" + to_stp + "");
                VM.SQL_SAVESOUTEZDATA("delete from refly where rnd_from=" + VM.BIND_SELECTED_ROUND + " and grp_from = " + VM.BIND_SELECTED_GROUP + " and stp_from = " + VM.BIND_SELECTED_STARTPOINT);
                VM.SQL_SAVESOUTEZDATA("delete from score where rnd=" + to_rnd + " and grp=" + to_grp + " and stp=" + to_stp + "");
                VM.SQL_SAVESOUTEZDATA("insert into score (rnd,grp,stp,userid,entered) values (" + to_rnd + "," + to_grp + "," + to_stp + ",0,'True')");

                int tmp_pozustalych_v_refly_skupine = int.Parse(VM.SQL_READSOUTEZDATA("select count(userid) from refly where rnd_to=" + to_rnd + " and grp_to=" + to_grp , ""));


                if (VM.SQL_READSOUTEZDATA("select type from groups where masterround="+ to_rnd + " and groupnumber="+ to_grp, "") == "refly") { 
                   
                    Console.WriteLine("v refly groupe zustalo lidi:" + tmp_pozustalych_v_refly_skupine);
                    if (tmp_pozustalych_v_refly_skupine == 0)
    
                    {
                        Console.WriteLine("mažu vše po prázné refly skupině");
                        VM.SQL_SAVESOUTEZDATA("delete from groups where masterround=" + to_rnd + " and groupnumber = " + to_grp);
                        VM.SQL_SAVESOUTEZDATA("delete from score where rnd=" + to_rnd + " and grp = " + to_grp);
                        VM.SQL_SAVESOUTEZDATA("delete from matrix where rnd=" + to_rnd + " and grp = " + to_grp);
                        VM.FUNCTION_CHECK_ENTERED_ALL();
                        VM.FUNCTION_ROUNDS_LOAD_ROUNDS();
                        VM.FUNCTION_ROUNDS_LOAD_GROUPS(VM.BIND_SELECTED_ROUND);

                        VM.FUNCTION_SELECTED_ROUND_FLYING_USERS(0, 0);
                        for (int i = 0; i < VM.MODEL_CONTEST_GROUPS.Count; i++)
                        {
                            VM.MODEL_CONTEST_GROUPS[i].ISSELECTED = "---";
                        }
                        VM.MODEL_CONTEST_GROUPS[VM.BIND_SELECTED_GROUP - 1].ISSELECTED = "selected";
                    }
                    else
                    {
                        VM.FUNCTION_CHECK_ENTERED_ALL();
                        VM.FUNCTION_ROUNDS_LOAD_ROUNDS();
                        VM.FUNCTION_ROUNDS_LOAD_GROUPS(VM.BIND_SELECTED_ROUND);

                        VM.FUNCTION_SELECTED_ROUND_FLYING_USERS(0, 0);
                        for (int i = 0; i < VM.MODEL_CONTEST_GROUPS.Count; i++)
                        {
                            VM.MODEL_CONTEST_GROUPS[i].ISSELECTED = "---";
                        }
                        VM.MODEL_CONTEST_GROUPS[VM.BIND_SELECTED_GROUP - 1].ISSELECTED = "selected";
                    }

                }
                else
                {
                    VM.FUNCTION_CHECK_ENTERED_ALL();
                    VM.FUNCTION_ROUNDS_LOAD_ROUNDS();
                    VM.FUNCTION_ROUNDS_LOAD_GROUPS(VM.BIND_SELECTED_ROUND);
                    
                    VM.FUNCTION_SELECTED_ROUND_FLYING_USERS(0, 0);
                    for (int i = 0; i < VM.MODEL_CONTEST_GROUPS.Count; i++)
                    {
                        VM.MODEL_CONTEST_GROUPS[i].ISSELECTED = "---";
                    }
                    VM.MODEL_CONTEST_GROUPS[VM.BIND_SELECTED_GROUP - 1].ISSELECTED = "selected";

                }

              


               


                for (int i = 0; i < VM.MODEL_CONTEST_ROUNDS.Count; i++)
                {
                    VM.MODEL_CONTEST_ROUNDS[i].ISSELECTED = "---";
                }
                VM.MODEL_CONTEST_ROUNDS[VM.BIND_SELECTED_ROUND - 1].ISSELECTED = "selected";
                refly.IsOpen = false;
                VM.FUNCTION_CHECK_REFLY(VM.BIND_SELECTED_ROUND, VM.BIND_SELECTED_GROUP);

            }


        }


        private int FUNCTION_CREATE_REFLY_GROUP_AND_ADD_COMPETITOR(int round)
        {

            VM.SQL_SAVESOUTEZDATA("insert into groups (id,name,type,lenght,zadano, masterround, groupnumber) values (null, 'refly:" + VM.FUNCTION_KOLIK_JE_SKUPIN_V_KOLE(round, "refly", true) + "','refly',600,0, " + round + " ," + VM.FUNCTION_KOLIK_JE_SKUPIN_V_KOLE(round, "", true) + ");");
            for (int s = 1; s < VM.BIND_SQL_SOUTEZ_STARTPOINTS + 1; s++)
            {
                VM.SQL_SAVESOUTEZDATA("insert into matrix (rnd,grp,stp,user) values (" + round + "," + VM.FUNCTION_KOLIK_JE_SKUPIN_V_KOLE(round, "", false) + "," + s + ",0)");
                VM.SQL_SAVESOUTEZDATA("insert into score (rnd,grp,stp,userid,entered) values (" + round + "," + VM.FUNCTION_KOLIK_JE_SKUPIN_V_KOLE(round, "", false) + "," + s + ",0,'True')");
            }



            int tmp_refly_group_number = VM.FUNCTION_KOLIK_JE_SKUPIN_V_KOLE(round, "", false);


            FUNCTION_ADD_USER_TO_REFLY(tmp_refly_group_number, VM.Player_Selected[0].ID, refly_what_count.IsOn);


            return tmp_refly_group_number;
        }
     
        private void FUNCTION_ADD_USER_TO_REFLY(int refly_group, int userid, bool je_jen_dolosovany)
        {

            int tmp_first_empty_stp = int.Parse(VM.SQL_READSOUTEZDATA("select stp from matrix where user=0 and rnd=" + VM.BIND_SELECTED_ROUND + " and grp=" + refly_group + " limit 1", ""));


            if (je_jen_dolosovany== true)
            {
                VM.SQL_SAVESOUTEZDATA("insert into refly (rnd_from,grp_from,stp_from,rnd_to,grp_to,stp_to,userid,whatcount1,whatcount2) values (" + VM.BIND_SELECTED_ROUND + "," + VM.BIND_SELECTED_GROUP + "," + VM.BIND_SELECTED_STARTPOINT + "," + VM.BIND_SELECTED_ROUND + "," + refly_group + "," + tmp_first_empty_stp + "," + userid + ",1,2);");
            }
            else
            {
                VM.SQL_SAVESOUTEZDATA("insert into refly (rnd_from,grp_from,stp_from,rnd_to,grp_to,stp_to,userid,whatcount1,whatcount2) values (" + VM.BIND_SELECTED_ROUND + "," + VM.BIND_SELECTED_GROUP + "," + VM.BIND_SELECTED_STARTPOINT + "," + VM.BIND_SELECTED_ROUND + "," + refly_group + "," + tmp_first_empty_stp + "," + userid + ",0,0);");
            }

            VM.SQL_SAVESOUTEZDATA("update score set userid=" + userid + ", entered='False' where rnd=" + VM.BIND_SELECTED_ROUND + " and grp=" + refly_group + " and stp=" + tmp_first_empty_stp + "");
            VM.SQL_SAVESOUTEZDATA("update matrix set user=" + userid + " where rnd=" + VM.BIND_SELECTED_ROUND + " and grp=" + refly_group + " and stp=" + tmp_first_empty_stp + "");

        }

        private async void create_refly_group_Click(object sender, RoutedEventArgs e)
        {

            FUNCTION_CREATE_REFLY_GROUP_AND_ADD_COMPETITOR(VM.BIND_SELECTED_ROUND);

            var currentWindow = this.TryFindParent<MetroWindow>();
            MessageDialogResult result = await currentWindow.ShowMessageAsync("Přiřazení refly", "Soutěžící byl zařazen k opravnému letu", MessageDialogStyle.Affirmative);

            VM.FUNCTION_CHECK_ENTERED_ALL();
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
            refly.IsOpen = false;

            VM.FUNCTION_CHECK_REFLY(VM.BIND_SELECTED_ROUND, VM.BIND_SELECTED_GROUP);


        }

        private async void create_refly_and_add_random_Click(object sender, RoutedEventArgs e)
        {

            int _novareflyskupina = FUNCTION_CREATE_REFLY_GROUP_AND_ADD_COMPETITOR(VM.BIND_SELECTED_ROUND);

            var currentWindow = this.TryFindParent<MetroWindow>();
            MessageDialogResult uvodnimsgbox = await currentWindow.ShowMessageAsync("Přiřazení refly", "Soutěžící byl zařazen k opravnému letu", MessageDialogStyle.Affirmative);


            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Přidat vylosovaného",
                NegativeButtonText = "Vylosovat jiného",
                FirstAuxiliaryButtonText = "Konec", 
                ColorScheme = MetroDialogColorScheme.Theme
            };

znovalosovat:

            int _tmp_vylosovany_soutezici_id = int.Parse(VM.SQL_READSOUTEZDATA("select s.userid from score S where s.rnd=1 and s.userid not in (select userid from refly where rnd_from = 1)  and s.userid>0 order by random() limit 1", ""));
            string _tmp_vylosovany_soutezici_jmeno = VM.SQL_READSOUTEZDATA("select lastname || ' ' || firstname from users where id = "+_tmp_vylosovany_soutezici_id, "");

            int _pocetvolnychstartovist = int.Parse(VM.SQL_READSOUTEZDATA("select count(userid) from refly where rnd_to = " + VM.BIND_SELECTED_ROUND + " and grp_to=" + _novareflyskupina, ""))+1;


            MessageDialogResult result = await currentWindow.ShowMessageAsync("Přidání dalšího soutěžícího do refly", "Pro refly byl na pozici " + _pocetvolnychstartovist + "/" + VM.BIND_SQL_SOUTEZ_STARTPOINTS + " vylosován : " + _tmp_vylosovany_soutezici_jmeno +". Chcete jej přidat ?", MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary,mySettings);
            if (result == MessageDialogResult.Negative) { goto znovalosovat; }
            if (result == MessageDialogResult.Affirmative)
            {



                int tmp_first_empty_stp = int.Parse(VM.SQL_READSOUTEZDATA("select stp from matrix where user=0 and rnd=" + VM.BIND_SELECTED_ROUND + " and grp=" + _novareflyskupina + " limit 1", ""));

                int tmp_grp = int.Parse(VM.SQL_READSOUTEZDATA("select grp from matrix where rnd = " + VM.BIND_SELECTED_ROUND + " and user = " + _tmp_vylosovany_soutezici_id + " order by grp asc limit 1", ""));
                int tmp_stp = int.Parse(VM.SQL_READSOUTEZDATA("select stp from matrix where rnd = " + VM.BIND_SELECTED_ROUND + " and user = "+ _tmp_vylosovany_soutezici_id + " order by grp asc limit 1", ""));

                VM.SQL_SAVESOUTEZDATA("insert into refly (rnd_from,grp_from,stp_from,rnd_to,grp_to,stp_to,userid,whatcount1,whatcount2) values (" + VM.BIND_SELECTED_ROUND + "," + tmp_grp + "," + tmp_stp+ "," + VM.BIND_SELECTED_ROUND + "," + _novareflyskupina + "," + tmp_first_empty_stp + "," + _tmp_vylosovany_soutezici_id + ",1,2);");
                VM.SQL_SAVESOUTEZDATA("update score set userid=" + _tmp_vylosovany_soutezici_id + ", entered='False' where rnd=" + VM.BIND_SELECTED_ROUND + " and grp=" + _novareflyskupina + " and stp=" + tmp_first_empty_stp + "");
                VM.SQL_SAVESOUTEZDATA("update matrix set user=" + _tmp_vylosovany_soutezici_id + " where rnd=" + VM.BIND_SELECTED_ROUND + " and grp=" + _novareflyskupina + " and stp=" + tmp_first_empty_stp + "");
                _pocetvolnychstartovist = int.Parse(VM.SQL_READSOUTEZDATA("select count(userid) from refly where rnd_to = " + VM.BIND_SELECTED_ROUND + " and grp_to=" + _novareflyskupina, ""));
                MessageDialogResult resultxx = await currentWindow.ShowMessageAsync("Přiřazení refly", "Soutěžící byl zařazen k opravnému letu", MessageDialogStyle.Affirmative);
                MessageDialogResult result2 = await currentWindow.ShowMessageAsync("Přidání dalšího soutěžícího do refly", "Obsazenost refly je : "+ _pocetvolnychstartovist + "/" + VM.BIND_SQL_SOUTEZ_STARTPOINTS + " Chcete přidat dalšího?", MessageDialogStyle.AffirmativeAndNegative);
                if (result2 == MessageDialogResult.Affirmative) { goto znovalosovat; }
            }

            if (result == MessageDialogResult.FirstAuxiliary) {

                VM.FUNCTION_CHECK_ENTERED_ALL();
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
                refly.IsOpen = false;

                VM.FUNCTION_CHECK_REFLY(VM.BIND_SELECTED_ROUND, VM.BIND_SELECTED_GROUP);

            }
        }
    }
}
