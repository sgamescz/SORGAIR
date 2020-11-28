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
    /// Interakční logika pro SecondView.xaml
    /// </summary>
    public partial class Teams : UserControl
    {
        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;


        public Teams()
        {
            InitializeComponent();

        }


        private void teamlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (teamlist.SelectedIndex>= 0) 
            {
                teams_teamname.Title = VM.Teams[teamlist.SelectedIndex].TEAMNAME.ToString() ;
                teams_usersinteamcount.Count = VM.BIND_kolikjelidivteamu(VM.Teams[teamlist.SelectedIndex].ID);
                VM.FUNCTION_TEAM_SHOW_USERS_IN_TEAMS(VM.Teams[teamlist.SelectedIndex].ID);
                teams_delete.IsEnabled = true ;
                teams_removefromteam.IsEnabled = true;
                teams_addtoteam.IsEnabled = true;
                teams_teamname.IsEnabled = true;
            }
            else
            {
                teams_teamname.IsEnabled = false ;
                teams_removefromteam.IsEnabled = false ;
                teams_addtoteam.IsEnabled = false ;
                teams_delete.IsEnabled = false;
            }

        }



        private void competitorlist2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (usersINteam.SelectedIndex >= 0)
            {
                //MessageBox.Show(VM.Usersinteams[competitorlist2.SelectedIndex].ID.ToString());
            }

        }

        private void teams_usersinteamcount_Click(object sender, RoutedEventArgs e)
        {
            teams_usersinteamcount.Count = VM.BIND_kolikjelidivteamu(VM.Teams[teamlist.SelectedIndex].ID);

        }

       

        private async void teams_teamname_Click(object sender, RoutedEventArgs e)
        {

            int tmp_what_is_selected = teamlist.SelectedIndex;

            var currentWindow = this.TryFindParent<MetroWindow>();
            var result = await currentWindow.ShowInputAsync("Název týmu", "Zadejte název týmu", new MetroDialogSettings() { DefaultText = teams_teamname.Title});
            if (result == null)
                return;
            VM.FUNCTION_team_rename(result, VM.Teams[teamlist.SelectedIndex].ID);

                //teams_teamname.Title = VM.Teams[teamlist.SelectedIndex].TEAMNAME.ToString();
            teamlist.SelectedIndex = tmp_what_is_selected;
            //if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_NAZEV") { VM.BIND_SQL_SOUTEZ_NAZEV = result; }




        }

        private async  void teams_add_Click(object sender, RoutedEventArgs e)
        {

         


            var currentWindow = this.TryFindParent<MetroWindow>();
            var result = await currentWindow.ShowInputAsync("Název nového týmu", "Zadejte název nového týmu", new MetroDialogSettings() { DefaultText = "Nový tým"});
            if (result == null)
                return;
            VM.FUNCTION_team_create(result);

            //teams_teamname.Title = VM.Teams[teamlist.SelectedIndex].TEAMNAME.ToString();
            teamlist.SelectedIndex = teamlist.Items.Count-1 ;
            //if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_NAZEV") { VM.BIND_SQL_SOUTEZ_NAZEV = result; }
        }

        private void move_to_team(object sender, RoutedEventArgs e)
        {

            if (usersNOTinteam.SelectedIndex >= 0 & teamlist.SelectedIndex >= 0)
            {
                int tmp_what_is_selected = teamlist.SelectedIndex;
                VM.FUNCTION_team_move_user_into_team(VM.UsersNOTinteams[usersNOTinteam.SelectedIndex].ID, VM.Teams[teamlist.SelectedIndex].ID, VM.Teams[teamlist.SelectedIndex].ID);
                //teams_usersinteamcount.Count = VM.BIND_kolikjelidivteamu(VM.Teams[teamlist.SelectedIndex].ID);
                VM.FUNCTION_TEAM_LOAD_TEAMS();
                teamlist.SelectedIndex = tmp_what_is_selected;
            }
        }

        private void move_from_team(object sender, RoutedEventArgs e)
        {
            if (usersINteam.SelectedIndex >= 0 & teamlist.SelectedIndex >= 0)
            {

                int tmp_what_is_selected = teamlist.SelectedIndex;
                VM.FUNCTION_team_move_user_into_team(VM.Usersinteams[usersINteam.SelectedIndex].ID, 0, VM.Teams[teamlist.SelectedIndex].ID);
                //teams_usersinteamcount.Count = VM.BIND_kolikjelidivteamu(VM.Teams[teamlist.SelectedIndex].ID);
                VM.FUNCTION_TEAM_LOAD_TEAMS();
                teamlist.SelectedIndex = tmp_what_is_selected;
            }
        }


        private async void teams_delete_Click(object sender, RoutedEventArgs e)
        {


            var currentWindow = this.TryFindParent<MetroWindow>();
            var result = await currentWindow.ShowMessageAsync("Smazání týmu", "Opravdu smazat zvolený tým", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { AnimateShow  = false, AnimateHide=false  });
            if (result == null)
                return;
            if (result == MessageDialogResult.Affirmative)
                VM.FUNCTION_team_delete(VM.Teams[teamlist.SelectedIndex].ID);
                VM.FUNCTION_TEAM_LOAD_TEAMS();

            //VM.FUNCTION_team_move_user_into_team(VM.Usersinteams[usersINteam.SelectedIndex].ID, 0, VM.Teams[teamlist.SelectedIndex].ID);
            //teams_usersinteamcount.Count = VM.BIND_kolikjelidivteamu(VM.Teams[teamlist.SelectedIndex].ID);
        }


    }


}
