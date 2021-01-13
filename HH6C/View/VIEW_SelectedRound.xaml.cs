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

                
                    int _tmp_height = scoreentry_height.SelectedIndex;
                    if (_tmp_height < 0) { _tmp_height = 0; }


                    int tmpheight = VM.BINDING_Timer_listofheights[_tmp_height].Value;
                    int tmpunder = 0;
                    int tmpover = 0;

                    if (tmpheight < 200)
                    {
                        tmpunder = tmpheight;
                        tmpover = 0;

                    }
                    else
                    {
                        tmpunder = 200;
                        tmpover = (tmpheight - 200);

                    }

                    int _tmp_min = scoreentry_minutes.SelectedIndex;
                    if (_tmp_min < 0) { _tmp_min = 0; }
                    int _tmp_sec = scoreentry_seconds.SelectedIndex;
                    if (_tmp_sec < 0) { _tmp_sec = 0; }
                    int _tmp_land = scoreentry_landing.SelectedIndex;
                    if (_tmp_land < 0) { _tmp_land = 0; }


                    VM.FUNCTION_SCOREENTRY_SAVE_SCORE(VM.BIND_SELECTED_ROUND, VM.BIND_SELECTED_GROUP, VM.BIND_SELECTED_STARTPOINT, VM.Player_Selected[0].ID, VM.BINDING_Timer_listofminutes[_tmp_min].Value, VM.BINDING_Timer_listofseconds[_tmp_sec].Value, VM.BINDING_Timer_listoflandings[_tmp_land].Value, tmpheight, tmpunder, tmpover, 1, 1,"0","0");


                float _prep =  float.Parse(VM.SQL_READSOUTEZDATA("select ifnull(round((ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*(select heightunder from rules)) " +
               "-(heightover*(select heightover from rules)) ),0)) / (select max(ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*(select heightunder from rules)) " +
               "-(heightover*(select heightover from rules)) ),0)) FROM score s where s.rnd = " + VM.BIND_SELECTED_ROUND + " and s.grp = " + VM.BIND_SELECTED_GROUP + ")*1000,2),round(0,2)) maxrow , ifnull(((((minutes*60)+seconds)*" +
               "(select persecond from rules))+landing-(heightunder*0.5) -(heightover*3) ),0) RAWSCORE from score S left join users U on S.userid = U.id where  s.rnd = " + VM.BIND_SELECTED_ROUND + " and s.grp = " + VM.BIND_SELECTED_GROUP + " and id=" + VM.Player_Selected[0].ID + " order by s.stp asc; ", ""));

                float _raw = float.Parse(VM.SQL_READSOUTEZDATA("select ifnull(((((minutes*60)+seconds)*(select persecond from rules))+landing-(heightunder*0.5) -(heightover*3) ),0) RAWSCORE from score S left join users U on S.userid = U.id where  s.rnd = " + VM.BIND_SELECTED_ROUND + " and s.grp = " + VM.BIND_SELECTED_GROUP + " and id =" + VM.Player_Selected[0].ID + " order by s.stp asc; ", ""));

                VM.Player_Selected[0].SCORE_RAW = _raw.ToString(CultureInfo.InvariantCulture);
                VM.Player_Selected[0].SCORE_PREP = _prep.ToString(new CultureInfo("en-US"));
                Console.WriteLine(VM.Player_Selected[0].SCORE_RAW);
                Console.WriteLine(VM.Player_Selected[0].SCORE_PREP);

                aktualscore.Content = "SCORE : " + VM.Player_Selected[0].SCORE_PREP + " ==  [ RAW : " + VM.Player_Selected[0].SCORE_RAW + " ]";




            }
            else
            {
                if (scoreentry_minutes.SelectedIndex >= 0 & scoreentry_seconds.SelectedIndex >= 0 & scoreentry_height.SelectedIndex >= 0 & scoreentry_landing.SelectedIndex >= 0)

                {

                    int tmpheight = VM.BINDING_Timer_listofheights[scoreentry_height.SelectedIndex].Value;
                    int tmpunder = 0;
                    int tmpover = 0;

                    if (tmpheight < 200)
                    {
                        tmpunder = tmpheight;
                        tmpover = 0;

                    }
                    else
                    {
                        tmpunder = 200;
                        tmpover = (tmpheight - 200);

                    }



                    VM.FUNCTION_SCOREENTRY_SAVE_SCORE(VM.BIND_SELECTED_ROUND, VM.BIND_SELECTED_GROUP, VM.BIND_SELECTED_STARTPOINT, VM.Player_Selected[0].ID, VM.BINDING_Timer_listofminutes[scoreentry_minutes.SelectedIndex].Value, VM.BINDING_Timer_listofseconds[scoreentry_seconds.SelectedIndex].Value, VM.BINDING_Timer_listoflandings[scoreentry_landing.SelectedIndex].Value, tmpheight, tmpunder, tmpover, 1, 1,VM.Player_Selected[0].SCORE_RAW , VM.Player_Selected[0].SCORE_PREP);

                    VM.FUNCTION_CHECK_ENTERED(VM.BIND_SELECTED_ROUND, VM.BIND_SELECTED_GROUP);
                    VM.FUNCTION_SELECTED_ROUND_USERS(0, 0);
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


                if (VM.BIND_SQL_SOUTEZ_ENTRYSTYLENEXT = true)
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


                    if (_isscoreentryopen == true)
                    {
                        savescore_event(true);
                    }


            }
            catch (Exception aaa)
            {
                Console.WriteLine(aaa.Message);
            }




        }

    }
}
