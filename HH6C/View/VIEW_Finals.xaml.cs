using System;
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
    public partial class Finals : UserControl
    {

        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;
        //        ViewModel XX = new ViewModel(DialogCoordinator.Instance);
        bool _isscoreentryopen = false;
        public Finals()
        {

            InitializeComponent();




        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = (sender as MahApps.Metro.Controls.Tile).Tag.ToString();
            VM.BIND_SELECTED_FINAL_ROUND = int.Parse(tag);
            //VM.FUNCTION_ROUNDS_LOAD_GROUPS(VM.BIND_SELECTED_FINAL_ROUND);
            VM.BIND_SELECTED_FINAL_GROUP = 1;
            VM.FUNCTION_SELECTED_FINAL_ROUND_USERS(VM.BIND_SELECTED_FINAL_ROUND, VM.BIND_SELECTED_FINAL_GROUP);

            for (int i = 0; i < VM.MODEL_CONTEST_FINAL_ROUNDS.Count; i++)
            {
                VM.MODEL_CONTEST_FINAL_ROUNDS[i].ISSELECTED = "---";
            }


            VM.MODEL_CONTEST_FINAL_ROUNDS[VM.BIND_SELECTED_FINAL_ROUND - 1].ISSELECTED = "selected";
            VM.FUNCTION_ROUNDS_LOAD_FINAL_GROUPS(int.Parse(tag));
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
                VM.clock_FINAL_MAIN_stop();

            }





        }

        private void maintimer_play_Click(object sender, RoutedEventArgs e)
        {

            //Console.WriteLine(VM.MODEL_CONTEST_SOUNDS_MAIN.Count);

            //for (int i = 0; i < VM.MODEL_CONTEST_SOUNDS_MAIN.Count; i++)
            //{
            //   Console.WriteLine(VM.MODEL_CONTEST_SOUNDS_MAIN[i].VALUE.ToString() + " --- " + VM.MODEL_CONTEST_SOUNDS_MAIN[i].TEXTVALUE.ToString());
            //}

           
            VM.clock_FINAL_MAIN_start();

        }

        private void HWbasemodul_Copywe2_Click(object sender, RoutedEventArgs e)
        {
            VM.BIND_LETOVYCAS_MAX = 100;
        }


      

        private void show_scoreentry_form(int round, int group, int startpoint)
        {

            if (round == 0) { VM.BIND_SCOREENTRY_SELECTEDFINAL_ROUND = VM.BIND_SELECTED_FINAL_ROUND; } else { VM.BIND_SCOREENTRY_SELECTEDFINAL_ROUND = round; }
            if (group == 0) { VM.BIND_SCOREENTRY_SELECTEDFINAL_GROUP = VM.BIND_SELECTED_FINAL_GROUP; } else { VM.BIND_SCOREENTRY_SELECTEDFINAL_GROUP = group; }
            if (startpoint == 0) { VM.BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT = VM.BIND_SELECTED_FINAL_STARTPOINT; } else { VM.BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT = startpoint; }



            Console.WriteLine(VM.BIND_SCOREENTRY_SELECTEDFINAL_ROUND + "_"+ VM.BIND_SCOREENTRY_SELECTEDFINAL_GROUP+ "_" + VM.BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT);
            VM.FUNCTION_SCOREENTRY_LOAD_USERDATA(VM.BIND_SCOREENTRY_SELECTEDFINAL_ROUND + 100, VM.BIND_SCOREENTRY_SELECTEDFINAL_GROUP, VM.BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT);
            scoreentry.IsOpen = true;
            scoreentry_minutes.Focus();
            _isscoreentryopen = true;
            scoreentry_height.IsEnabled = VM.MODEL_CONTEST_RULES[0].ENTRYHEIGHT;
            savescore_event(true);

        }

        private void show_refly_form()
        {
            Console.WriteLine(VM.BIND_SELECTED_FINAL_ROUND + "_"+ VM.BIND_SELECTED_FINAL_GROUP+"_" + VM.BIND_SELECTED_FINAL_STARTPOINT);
            VM.FUNCTION_SCOREENTRY_LOAD_USERDATA(VM.BIND_SELECTED_FINAL_ROUND + 100, VM.BIND_SELECTED_FINAL_GROUP, VM.BIND_SELECTED_FINAL_STARTPOINT);
            refly.IsOpen = true;
            _isscoreentryopen = true;

        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            Button tagbutton = sender as Button;
            Console.WriteLine("AHAH:" + tagbutton.Tag.ToString());
            VM.BIND_SELECTED_FINAL_STARTPOINT = int.Parse(tagbutton.Tag.ToString());
            Console.WriteLine("BIND_SELECTED_FINAL_STARTPOINT" + VM.BIND_SELECTED_FINAL_STARTPOINT);


            VM.BIND_SELECTED_FINAL_STARTPOINT = int.Parse(tagbutton.Tag.ToString());
            show_scoreentry_form(0, 0, 0);

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
            Console.WriteLine("savescore_event");
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
                if (_time > (VM.MODEL_CONTEST_RULES[0].TIME1LIMIT + (VM.MODEL_CONTEST_RULES[0].FINALROUNDLENGHT - VM.MODEL_CONTEST_RULES[0].BASEROUNDLENGHT)))
                {
                    Decimal _timeunder = (VM.MODEL_CONTEST_RULES[0].TIME1LIMIT + (VM.MODEL_CONTEST_RULES[0].FINALROUNDLENGHT - VM.MODEL_CONTEST_RULES[0].BASEROUNDLENGHT)) * VM.MODEL_CONTEST_RULES[0].TIME1UNDER;
                    Decimal _timeover = (_time - (VM.MODEL_CONTEST_RULES[0].TIME1LIMIT + (VM.MODEL_CONTEST_RULES[0].FINALROUNDLENGHT - VM.MODEL_CONTEST_RULES[0].BASEROUNDLENGHT) )) * VM.MODEL_CONTEST_RULES[0].TIME1OVER;
                    _time_points = _timeunder + _timeover;
                    
                    if (VM.MODEL_CONTEST_RULES[0].DELETETIME1 == true)
                    {
                        _time_points = 0;
                    }

                    if (_time > (VM.MODEL_CONTEST_RULES[0].TIME2LIMIT + (VM.MODEL_CONTEST_RULES[0].FINALROUNDLENGHT - VM.MODEL_CONTEST_RULES[0].BASEROUNDLENGHT)) && VM.MODEL_CONTEST_RULES[0].DELETETIME1 == true)
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

                if (_time > (VM.MODEL_CONTEST_RULES[0].TIME1LIMIT + (VM.MODEL_CONTEST_RULES[0].FINALROUNDLENGHT - VM.MODEL_CONTEST_RULES[0].BASEROUNDLENGHT)) && VM.MODEL_CONTEST_RULES[0].DELETELANDING1 == true)
                {
                    _landings = 0;
                }
                if (_time > (VM.MODEL_CONTEST_RULES[0].TIME2LIMIT + (VM.MODEL_CONTEST_RULES[0].FINALROUNDLENGHT - VM.MODEL_CONTEST_RULES[0].BASEROUNDLENGHT)) && VM.MODEL_CONTEST_RULES[0].DELETELANDING2 == true)
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


                if (_time > (VM.MODEL_CONTEST_RULES[0].TIME1LIMIT + (VM.MODEL_CONTEST_RULES[0].FINALROUNDLENGHT - VM.MODEL_CONTEST_RULES[0].BASEROUNDLENGHT)) && VM.MODEL_CONTEST_RULES[0].DELETEALL1 == true)
                {
                    _RAWSCORE = 0;
                }

                if (_time > (VM.MODEL_CONTEST_RULES[0].TIME2LIMIT + (VM.MODEL_CONTEST_RULES[0].FINALROUNDLENGHT - VM.MODEL_CONTEST_RULES[0].BASEROUNDLENGHT)) && VM.MODEL_CONTEST_RULES[0].DELETEALL2 == true)
                {
                    _RAWSCORE = 0;
                }



                string _RAWSCORE_STR = _RAWSCORE.ToString(new CultureInfo("en-US"));

                VM.SQL_SAVESOUTEZDATA("update score set raw = " + _RAWSCORE_STR + " where rnd = " + (VM.BIND_SCOREENTRY_SELECTEDFINAL_ROUND+100) + " and grp = " + VM.BIND_SCOREENTRY_SELECTEDFINAL_GROUP + " and stp = " + VM.BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT);
                Decimal _MAXRAW = Decimal.Parse(VM.SQL_READSOUTEZDATA("select max(raw) FROM score s where s.rnd = " + (VM.BIND_SCOREENTRY_SELECTEDFINAL_ROUND+100) + " and s.grp = "+ VM.BIND_SCOREENTRY_SELECTEDFINAL_GROUP, ""));
                Decimal _PREPSCORE = 0;

                if (_MAXRAW != 0)
                {
                    _PREPSCORE = Math.Round((_RAWSCORE / _MAXRAW) * 1000, 2);

                }
                string _PREPSCORE_STR = _PREPSCORE.ToString(new CultureInfo("en-US"));


                VM.Player_Selected[0].SCORE_RAW = _RAWSCORE_STR;
                VM.Player_Selected[0].SCORE_PREP = _PREPSCORE_STR;

                VM.FUNCTION_SCOREENTRY_SAVE_SCORE(VM.BIND_SCOREENTRY_SELECTEDFINAL_ROUND+100, VM.BIND_SCOREENTRY_SELECTEDFINAL_GROUP, VM.BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT, VM.Player_Selected[0].ID, VM.BINDING_Timer_listofminutes[scoreentry_minutes.SelectedIndex].Value, VM.BINDING_Timer_listofseconds[scoreentry_seconds.SelectedIndex].Value, VM.BINDING_Timer_listoflandings[scoreentry_landing.SelectedIndex].VALUE , VM.BINDING_Timer_listofheights[scoreentry_height.SelectedIndex].Value, VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].VALUE, VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].ID, VM.BINDING_Timer_listofpenalisationglobal[scoreentry_penglobal.SelectedIndex].VALUE, VM.BINDING_Timer_listofpenalisationglobal[scoreentry_penglobal.SelectedIndex].ID, VM.Player_Selected[0].SCORE_RAW, VM.Player_Selected[0].SCORE_PREP,isnondeletable.IsOn,false);
                VM.Player_Selected[0].SCORE_RAW = _RAWSCORE_STR;
                VM.Player_Selected[0].SCORE_PREP = _PREPSCORE_STR;
                Console.WriteLine(VM.Player_Selected[0].SCORE_RAW);
                Console.WriteLine(VM.Player_Selected[0].SCORE_PREP);
                aktualscore.Content = "ACTSCORE : " + VM.Player_Selected[0].SCORE_PREP + " ==  [ RAW : " + VM.Player_Selected[0].SCORE_RAW + " ]";

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


                    if (_time > (VM.MODEL_CONTEST_RULES[0].TIME1LIMIT+ (VM.MODEL_CONTEST_RULES[0].FINALROUNDLENGHT- VM.MODEL_CONTEST_RULES[0].BASEROUNDLENGHT) ) )
                    {

                            Decimal _timeunder = (VM.MODEL_CONTEST_RULES[0].TIME1LIMIT + (VM.MODEL_CONTEST_RULES[0].FINALROUNDLENGHT - VM.MODEL_CONTEST_RULES[0].BASEROUNDLENGHT) ) * VM.MODEL_CONTEST_RULES[0].TIME1UNDER;
                            Decimal _timeover = (_time - (VM.MODEL_CONTEST_RULES[0].TIME1LIMIT + (VM.MODEL_CONTEST_RULES[0].FINALROUNDLENGHT - VM.MODEL_CONTEST_RULES[0].BASEROUNDLENGHT))) * VM.MODEL_CONTEST_RULES[0].TIME1OVER;
                            _time_points = _timeunder + _timeover;

                        if (VM.MODEL_CONTEST_RULES[0].DELETETIME1 == true)
                        {
                            _time_points = 0;
                        }

                        if (_time > (VM.MODEL_CONTEST_RULES[0].TIME2LIMIT + (VM.MODEL_CONTEST_RULES[0].FINALROUNDLENGHT - VM.MODEL_CONTEST_RULES[0].BASEROUNDLENGHT)) && VM.MODEL_CONTEST_RULES[0].DELETETIME1 == true)
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

                    if (_time > (VM.MODEL_CONTEST_RULES[0].TIME1LIMIT + (VM.MODEL_CONTEST_RULES[0].FINALROUNDLENGHT - VM.MODEL_CONTEST_RULES[0].BASEROUNDLENGHT)) && VM.MODEL_CONTEST_RULES[0].DELETELANDING1 == true)
                    {
                        _landings = 0;
                    }
                    if (_time > (VM.MODEL_CONTEST_RULES[0].TIME2LIMIT + (VM.MODEL_CONTEST_RULES[0].FINALROUNDLENGHT - VM.MODEL_CONTEST_RULES[0].BASEROUNDLENGHT)) && VM.MODEL_CONTEST_RULES[0].DELETELANDING2 == true)
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




                    if (_time > (VM.MODEL_CONTEST_RULES[0].TIME1LIMIT + (VM.MODEL_CONTEST_RULES[0].FINALROUNDLENGHT - VM.MODEL_CONTEST_RULES[0].BASEROUNDLENGHT)) && VM.MODEL_CONTEST_RULES[0].DELETEALL1 == true)
                    {
                        _RAWSCORE = 0;
                    }

                    if (_time > (VM.MODEL_CONTEST_RULES[0].TIME2LIMIT + (VM.MODEL_CONTEST_RULES[0].FINALROUNDLENGHT - VM.MODEL_CONTEST_RULES[0].BASEROUNDLENGHT)) && VM.MODEL_CONTEST_RULES[0].DELETEALL2 == true)
                    {
                        _RAWSCORE = 0;
                    }




                    string _RAWSCORE_STR = _RAWSCORE.ToString(new CultureInfo("en-US"));

                    VM.SQL_SAVESOUTEZDATA("update score set raw = " + _RAWSCORE_STR + " where rnd = " + (VM.BIND_SCOREENTRY_SELECTEDFINAL_ROUND+100) + " and grp = " + VM.BIND_SCOREENTRY_SELECTEDFINAL_GROUP + " and stp = "+ VM.BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT);

                    Decimal _MAXRAW = Decimal.Parse(VM.SQL_READSOUTEZDATA("select max(raw) FROM score s where s.rnd = " + (VM.BIND_SCOREENTRY_SELECTEDFINAL_ROUND+100) + " and s.grp = "+ VM.BIND_SCOREENTRY_SELECTEDFINAL_GROUP, ""));

                    Decimal _PREPSCORE = 0;

                    if (_MAXRAW != 0)
                    {
                        _PREPSCORE = Math.Round((_RAWSCORE / _MAXRAW) * 1000, 2);
                    }


                    

                    string _PREPSCORE_STR = _PREPSCORE.ToString(new CultureInfo("en-US"));


                    VM.Player_Selected[0].SCORE_RAW = _RAWSCORE_STR;
                    VM.Player_Selected[0].SCORE_PREP = _PREPSCORE_STR;

                    VM.FUNCTION_SCOREENTRY_SAVE_SCORE(VM.BIND_SCOREENTRY_SELECTEDFINAL_ROUND+100, VM.BIND_SCOREENTRY_SELECTEDFINAL_GROUP, VM.BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT, VM.Player_Selected[0].ID, VM.BINDING_Timer_listofminutes[scoreentry_minutes.SelectedIndex].Value, VM.BINDING_Timer_listofseconds[scoreentry_seconds.SelectedIndex].Value, VM.BINDING_Timer_listoflandings[scoreentry_landing.SelectedIndex].VALUE, VM.BINDING_Timer_listofheights[scoreentry_height.SelectedIndex].Value, VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].VALUE, VM.BINDING_Timer_listofpenalisationlocal[scoreentry_penlocal.SelectedIndex].ID, VM.BINDING_Timer_listofpenalisationglobal[scoreentry_penglobal.SelectedIndex].VALUE, VM.BINDING_Timer_listofpenalisationglobal[scoreentry_penglobal.SelectedIndex].ID, VM.Player_Selected[0].SCORE_RAW , VM.Player_Selected[0].SCORE_PREP,isnondeletable.IsOn,true);

                    VM.FUNCTION_CHECK_ENTERED_FINAL(VM.BIND_SCOREENTRY_SELECTEDFINAL_ROUND, VM.BIND_SCOREENTRY_SELECTEDFINAL_GROUP, true);
                    //VM.FUNCTION_ROUNDS_LOAD_FINAL_ROUNDS();
                    //VM.FUNCTION_ROUNDS_LOAD_GROUPS(VM.BIND_SCOREENTRY_SELECTEDFINAL_ROUND);
                    VM.FUNCTION_ROUNDS_LOAD_FINAL_ROUNDS();

                    VM.FUNCTION_SELECTED_FINAL_ROUND_USERS(VM.BIND_SELECTED_FINAL_ROUND, VM.BIND_SELECTED_FINAL_GROUP);

                    for (int i = 0; i < VM.MODEL_CONTEST_FINAL_ROUNDS.Count; i++)
                    {
                        VM.MODEL_CONTEST_FINAL_ROUNDS[i].ISSELECTED = "---";
                    }
                    VM.MODEL_CONTEST_FINAL_ROUNDS[VM.BIND_SELECTED_FINAL_ROUND - 1].ISSELECTED = "selected";

                    scoreentry.IsOpen = false;
                    _isscoreentryopen = false;
                    //HWbasemodul_Copy4s.Focus();

                }



                int TMP_BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT;
                TMP_BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT = VM.BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT;

                if (VM.BIND_SQL_SOUTEZ_ENTRYSTYLENEXT == true)
                {
                    Console.WriteLine("VM.BIND_SELECTED_STARTPOINT" + VM.BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT);
                    Console.WriteLine("VM.BIND_SQL_SOUTEZ_STARTPOINTSFINALE" + VM.BIND_SQL_SOUTEZ_STARTPOINTSFINALE);

                    if (TMP_BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT < VM.BIND_SQL_SOUTEZ_STARTPOINTSFINALE)
                    {
                        TMP_BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT += 1;
                        VM.BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT = TMP_BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT;
                        show_scoreentry_form(VM.BIND_SCOREENTRY_SELECTEDFINAL_ROUND, VM.BIND_SCOREENTRY_SELECTEDFINAL_GROUP, VM.BIND_SCOREENTRY_SELECTEDFINAL_STARTPOINT);
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
            VM.MAX_WIDTH = 20;
            VM.BIND_PREP_AUDIO_FINAL_MAN_AUTO = true;
            VM.clock_FINAL_PREP_start();
        }

        private void preptimer_stop_Click(object sender, RoutedEventArgs e)
        {
            VM.clock_FINAL_PREP_stop();
         
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Button tagbutton = sender as Button;
            Console.WriteLine("AHAH:" + tagbutton.Tag.ToString());
            VM.BIND_SELECTED_FINAL_STARTPOINT = int.Parse(tagbutton.Tag.ToString());
            Console.WriteLine("BIND_SELECTED_FINAL_STARTPOINT" + VM.BIND_SELECTED_FINAL_STARTPOINT);
            show_refly_form();
        }

        private void create_refly_group_Click(object sender, RoutedEventArgs e)
        {
            FUNCTION_CREATE_REFLY_GROUP_AND_ADD_COMPETITOR(VM.BIND_SELECTED_FINAL_ROUND+100);
            VM.FUNCTION_CHECK_REFLY(VM.BIND_SELECTED_FINAL_ROUND+100, VM.BIND_SELECTED_FINAL_GROUP);




            for (int i = 0; i < VM.MODEL_CONTEST_FINAL_ROUNDS.Count; i++)
            {
                VM.MODEL_CONTEST_FINAL_ROUNDS[i].ISSELECTED = "---";
            }


            VM.MODEL_CONTEST_FINAL_ROUNDS[VM.BIND_SELECTED_FINAL_ROUND - 1].ISSELECTED = "selected";
            VM.FUNCTION_ROUNDS_LOAD_FINAL_GROUPS(VM.BIND_SELECTED_FINAL_ROUND);

            refly.IsOpen = false;



        }



        private int FUNCTION_CREATE_REFLY_GROUP_AND_ADD_COMPETITOR(int round)
        {

            VM.SQL_SAVESOUTEZDATA("insert into groups_final (id,name,type,lenght,zadano, masterround, groupnumber) values (null, 'refly:" + VM.FUNCTION_KOLIK_JE_REFLY_SKUPIN_V_FINALE(round, "refly", true) + "','refly',600,0, " + round + " ," + VM.FUNCTION_KOLIK_JE_REFLY_SKUPIN_V_FINALE(round, "", true) + ");");
            for (int s = 1; s < VM.BIND_SQL_SOUTEZ_STARTPOINTSFINALE + 1; s++)
            {
                VM.SQL_SAVESOUTEZDATA("insert into matrix (rnd,grp,stp,user) values (" + round + "," + VM.FUNCTION_KOLIK_JE_REFLY_SKUPIN_V_FINALE(round, "", false) + "," + s + ",0)");
                VM.SQL_SAVESOUTEZDATA("insert into score (rnd,grp,stp,userid,entered) values (" + round + "," + VM.FUNCTION_KOLIK_JE_REFLY_SKUPIN_V_FINALE(round, "", false) + "," + s + ",0,'True')");
            }



            int tmp_refly_group_number = VM.FUNCTION_KOLIK_JE_REFLY_SKUPIN_V_FINALE(round, "", false);


            FUNCTION_ADD_USERS_TO_FINAL_REFLY(round, tmp_refly_group_number, VM.Player_Selected[0].ID);


            return tmp_refly_group_number;
        }

        private async void FUNCTION_ADD_USERS_TO_FINAL_REFLY(int round, int refly_group, int userid)
        {


            for (int s = 1; s < VM.BIND_SQL_SOUTEZ_STARTPOINTSFINALE + 1; s++)
            {
                if (VM.Players_Actual_Final_Flying[s-1].ID == userid)
                {
                    VM.SQL_SAVESOUTEZDATA("update score set userid=" + VM.Players_Actual_Final_Flying[s - 1].ID + ", entered='False' where rnd=" + round + " and grp=" + refly_group + " and stp=" + s + "");
                    VM.SQL_SAVESOUTEZDATA("update matrix set user=" + VM.Players_Actual_Final_Flying[s - 1].ID + " where rnd=" + round + " and grp=" + refly_group + " and stp=" + s + "");
                    VM.SQL_SAVESOUTEZDATA("insert into refly (rnd_from,grp_from,stp_from,rnd_to,grp_to,stp_to,userid,whatcount1,whatcount2) values (" + round + "," + VM.BIND_SELECTED_GROUP + "," + s + "," + round + "," + refly_group + "," + s + "," + VM.Players_Actual_Final_Flying[s - 1].ID + ",1,2);");
                }
                else
                {
                    VM.SQL_SAVESOUTEZDATA("update score set userid=" + VM.Players_Actual_Final_Flying[s - 1].ID + ", entered='False' where rnd=" + round + " and grp=" + refly_group + " and stp=" + s + "");
                    VM.SQL_SAVESOUTEZDATA("update matrix set user=" + VM.Players_Actual_Final_Flying[s - 1].ID + " where rnd=" + round + " and grp=" + refly_group + " and stp=" + s + "");
                    VM.SQL_SAVESOUTEZDATA("insert into refly (rnd_from,grp_from,stp_from,rnd_to,grp_to,stp_to,userid,whatcount1,whatcount2) values (" + round + "," + VM.BIND_SELECTED_GROUP + "," + s + "," + round + "," + refly_group + "," + s + "," + VM.Players_Actual_Final_Flying[s - 1].ID + ",0,0);");
                }
            }


            // int tmp_first_empty_stp = int.Parse(VM.SQL_READSOUTEZDATA("select stp from matrix where user=0 and rnd=" + round + " and grp=" + refly_group + " limit 1", ""));


            //if (je_jen_dolosovany == true)
            //{
            //VM.SQL_SAVESOUTEZDATA("insert into refly (rnd_from,grp_from,stp_from,rnd_to,grp_to,stp_to,userid,whatcount1,whatcount2) values (" + round + "," + VM.BIND_SELECTED_GROUP + "," + VM.BIND_SELECTED_STARTPOINT + "," + round + "," + refly_group + "," + tmp_first_empty_stp + "," + userid + ",1,2);");
            // }
            //else
            //{
            //VM.SQL_SAVESOUTEZDATA("insert into refly (rnd_from,grp_from,stp_from,rnd_to,grp_to,stp_to,userid,whatcount1,whatcount2) values (" + round + "," + VM.BIND_SELECTED_GROUP + "," + VM.BIND_SELECTED_STARTPOINT + "," + round + "," + refly_group + "," + tmp_first_empty_stp + "," + userid + ",0,0);");
            //}

            //VM.SQL_SAVESOUTEZDATA("update score set userid=" + userid + ", entered='False' where rnd=" + round + " and grp=" + refly_group + " and stp=" + tmp_first_empty_stp + "");
            //VM.SQL_SAVESOUTEZDATA("update matrix set user=" + userid + " where rnd=" + round + " and grp=" + refly_group + " and stp=" + tmp_first_empty_stp + "");
            var currentWindow = this.TryFindParent<MetroWindow>();
            MessageDialogResult result = await currentWindow.ShowMessageAsync("Přiřazení refly", "Soutěžící byli zařazeni do opravného letu", MessageDialogStyle.Affirmative);


        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {

            Button tagbutton = sender as Tile;
            VM.BIND_SELECTED_FINAL_GROUP = int.Parse(tagbutton.Tag.ToString());
            VM.FUNCTION_SELECTED_FINAL_ROUND_USERS(VM.BIND_SELECTED_FINAL_ROUND, VM.BIND_SELECTED_FINAL_GROUP);


            for (int i = 0; i < VM.MODEL_CONTEST_FINAL_GROUPS.Count; i++)
            {
                VM.MODEL_CONTEST_FINAL_GROUPS[i].ISSELECTED = "---";
            }

            VM.MODEL_CONTEST_FINAL_GROUPS[VM.BIND_SELECTED_FINAL_GROUP - 2].ISSELECTED = "selected";

        }

        private void delete_refly_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
