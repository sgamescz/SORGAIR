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

namespace WpfApp6.View
{
    /// <summary>
    /// Interakční logika pro FirstView.xaml
    /// </summary>
    public partial class selectedround : UserControl
    {

        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;
//        ViewModel XX = new ViewModel(DialogCoordinator.Instance);

        public selectedround()
        {

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"Audio\petiminutoveho.wav");
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
                maintimer_pause.IsEnabled = false;
                maintimer_stop.IsEnabled = false;
            }





        }

        private void HWbasemodul_Copy2_Click(object sender, RoutedEventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"Audio\petiminutoveho.wav");
            player.Play();
            VM.BIND_LETOVYCAS = 0;
            VM.BIND_LETOVYCAS_MAX = 600;
            maintimer_play .IsEnabled = false;
            maintimer_pause.IsEnabled = true;
            maintimer_stop.IsEnabled = true ;
            VM.clock_start();



        }

        private void HWbasemodul_Copywe2_Click(object sender, RoutedEventArgs e)
        {
            VM.BIND_LETOVYCAS_MAX = 600;
        }

        private void maintimer_pause_Click(object sender, RoutedEventArgs e)
        {
            VM.clock_pause ();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            VM.BIND_SELECTED_GROUP += 1;
            if (VM.BIND_SELECTED_GROUP > VM.BIND_SQL_SOUTEZ_GROUPS) { VM.BIND_SELECTED_GROUP = 1;VM.BIND_SELECTED_ROUND += 1; }
            VM.FUNCTION_SELECTED_ROUND_USERS(0, 0);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            Button tagbutton = sender as Button;
            VM.BIND_SELECTED_STARTPOINT = int.Parse(tagbutton.Tag.ToString());
            Console.WriteLine(VM.BIND_SELECTED_ROUND + "_" + VM.BIND_SELECTED_GROUP + "_" + VM.BIND_SELECTED_STARTPOINT);
            VM.FUNCTION_SCOREENTRY_LOAD_USERDATA(0,0,0);
            scoreentry.IsOpen = true;
            scoreentry_minutes.Focus();
        }

        private void scoreentry_save_Click(object sender, RoutedEventArgs e)
        {

            

                savescore_event();

           

        }

        private void scoreentry_back_Click(object sender, RoutedEventArgs e)
        {
            scoreentry_landing.SelectedIndex = 5;
            //scoreentry.IsOpen = false;

        }


        public void savescore_event()
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

                VM.FUNCTION_SCOREENTRY_SAVE_SCORE(VM.BIND_SELECTED_ROUND, VM.BIND_SELECTED_GROUP, VM.BIND_SELECTED_STARTPOINT, VM.Player_Selected[0].ID, VM.BINDING_Timer_listofminutes[scoreentry_minutes.SelectedIndex].Value, VM.BINDING_Timer_listofseconds[scoreentry_seconds.SelectedIndex].Value, VM.BINDING_Timer_listoflandings[scoreentry_landing.SelectedIndex].Value, tmpheight, tmpunder, tmpover, 1, 1);
                VM.FUNCTION_CHECK_ENTERED(VM.BIND_SELECTED_ROUND, VM.BIND_SELECTED_GROUP);
                VM.FUNCTION_SELECTED_ROUND_USERS(0, 0);
                scoreentry.IsOpen = false;
                HWbasemodul_Copy4s.Focus(); 

            }


          

        }
        private void scoreentry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                savescore_event(); 

            }
        }
    }
}
