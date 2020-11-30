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
using System.Windows.Interop;
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
    public partial class Soutezici : UserControl
    {

        
        
        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;





    public Soutezici()
        {
            InitializeComponent();

            

        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {

        }


        private void competitorlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (competitorlist.SelectedIndex>= 0) 
            {
                userdetail_id.Count = VM.Players[competitorlist.SelectedIndex].ID.ToString();
                userdetail_competitorname.Title = VM.Players[competitorlist.SelectedIndex].LASTNAME.ToString() + " " + VM.Players[competitorlist.SelectedIndex].FIRSTNAME.ToString();
                userdetail_agecat.Title   = "Věková kategorie : " + VM.Players[competitorlist.SelectedIndex].AGECAT .ToString();
                userdetail_NATLIC.Title = VM.Players[competitorlist.SelectedIndex].NACLIC  .ToString();
                userdetail_FAILIC.Title = VM.Players[competitorlist.SelectedIndex].FAILIC  .ToString();
                userdetail_club.Title = "Klub : " + VM.Players[competitorlist.SelectedIndex].CLUB .ToString();
                userdetail_freq .Title = "Frekvence : " + VM.Players[competitorlist.SelectedIndex].FREQ.ToString();
                userdetail_ch1 .Title = VM.Players[competitorlist.SelectedIndex].CH1 .ToString();
                userdetail_ch2 .Title = VM.Players[competitorlist.SelectedIndex].CH2.ToString();
                userdetail_country.Title = VM.Players[competitorlist.SelectedIndex].COUNTRY.ToString();
                VM.BIND_FLAG = VM.Players[competitorlist.SelectedIndex].COUNTRY.ToString();
                VM.BIND_PAID = VM.Players[competitorlist.SelectedIndex].PAIDSTR  .ToString();
                ispaid.IsEnabled = true;
                delete_competitor.IsEnabled = true;


            }
            else
            {
                ispaid.IsEnabled = false ;
                delete_competitor.IsEnabled = false ;
            }

        }


        private void l_FUNCTION_clear_all_newuser_fields()
        {
            l_firstname.Text = "";
            L_lastname.Text = "";
            l_country.SelectedIndex = 58;
            l_chanel1.Value = null ;
            l_chanel2.Value = null;
            l_club.Text = "";
            l_failic.Text = "";
            l_naclic.Text = "";
            l_agecat.SelectedIndex = -1;
            l_freq.SelectedIndex = -1;
            l_registered.IsOn  = false;
            l_nextid.Count = VM.SQL_READSOUTEZDATA("SELECT seq+1 FROM SQLITE_SEQUENCE where name='users'", "");



        }

        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            VM.Players[3].FIRSTNAME = "AAXXX";
        }

        private void landingoptions_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ispaid_Click(object sender, RoutedEventArgs e)
        {
            string check = (sender as MahApps.Metro.Controls.ToggleSwitch).IsOn  .ToString();
            VM.Players[competitorlist.SelectedIndex].PAIDSTR = check;
            VM.Players[competitorlist.SelectedIndex].PAID = @"E:\SORGAIR\SORGAIR\HH6C\bin\Debug\flags\" + check + ".png";
            VM.FUNCTION_COMPETITOR_UPDATE("Paid", check, VM.Players[competitorlist.SelectedIndex].ID);
        }

        private void SplitButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string L_flag = (sender as MahApps.Metro.Controls.SplitButton).SelectedIndex.ToString();
            
            if (Int32.Parse(L_flag) >= 0 )
            {
                Console.WriteLine(L_flag);
                L_newuser_flag.Source = new BitmapImage(new Uri("E:/SORGAIR/SORGAIR/HH6C/bin/Debug/flags/" + VM.MODEL_Contest_FLAGS[Int32.Parse(L_flag)].FILENAME + ".png"));
            }

        }

        private void L_savenewuser_Click(object sender, RoutedEventArgs e)
        {
            if (__SAVE_NEW_USER() == true)
            {

                l_FUNCTION_clear_all_newuser_fields();
                firstFlyout.IsOpen = false;
                competitorlist.SelectedIndex = competitorlist.Items.Count - 1;
                competitorlist.ScrollIntoView(competitorlist.Items[competitorlist.SelectedIndex]);
            }

        }

        private void Tile_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private async  void delete_competitor_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            if (competitorlist.SelectedIndex > 0)
            {
                MessageDialogResult result = await currentWindow.ShowMessageAsync("Smazání soutěžícího", "Opravdu smazat soutěžícího s ID: " + VM.Players[competitorlist.SelectedIndex].ID + " : " + VM.Players[competitorlist.SelectedIndex].FIRSTNAME + " " + VM.Players[competitorlist.SelectedIndex].LASTNAME + "?", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Negative)
                {
                    Console.WriteLine("No");
                }
                else
                {
                    Console.WriteLine("yes");
                    VM.FUNCTION_USERS_DELETE_COMPETITOR(VM.Players[competitorlist.SelectedIndex].ID);

                }
            }
            else
            {
                await currentWindow.ShowMessageAsync("Nikdo není vybrán", "Vyber prosím, koho chceš smazat");
            }
    
        }

        private void addnewuser_Click(object sender, RoutedEventArgs e)
        {
            //VM.save_new_competitor("firstname", "lastname", "FIN");
            firstFlyout.IsOpen = true;
            l_nextid.Count  = VM.SQL_READSOUTEZDATA("SELECT seq+1 FROM SQLITE_SEQUENCE where name='users'", "");
        }


        private bool  __SAVE_NEW_USER()
        {
            bool results = true ;

            if (l_firstname.Text == "" || L_lastname.Text == "" || l_agecat.SelectedIndex ==-1 || l_chanel1.Value is null || l_chanel2.Value is null || l_club.Text == "" || l_country.SelectedIndex == -1 || l_failic.Text == "" || l_freq.SelectedIndex == -1 || l_naclic.Text ==""         )
            {
                results = false;
                var currentWindow = this.TryFindParent<MetroWindow>();
                currentWindow.ShowMessageAsync("Nelze uložit", "Nejsou vyplněné všechny údaje soutěžícího. Nelze uložit"   );

            }
            else
            {
                VM.FUNCTION_USERS_CREATE_NEW(l_firstname.Text, L_lastname.Text, VM.MODEL_Contest_FLAGS[l_country.SelectedIndex].FILENAME, VM.MODEL_Contest_AGECATEGORIES[l_agecat.SelectedIndex].ID, VM.MODEL_Contest_FREQUENCIES[l_freq.SelectedIndex].ID, Convert.ToInt32(l_chanel1.Value), Convert.ToInt32(l_chanel2.Value), l_failic.Text, l_naclic.Text, l_club.Text, Convert.ToBoolean(l_registered.IsOn), 1, 1);
            }
            return results;
        }
        private void L_savenewuserandagain_Click(object sender, RoutedEventArgs e)
        {

            if (__SAVE_NEW_USER() == true)
            {
                l_FUNCTION_clear_all_newuser_fields();
                firstFlyout.IsOpen = true;
                competitorlist.SelectedIndex = competitorlist.Items.Count - 1;
                competitorlist.ScrollIntoView(competitorlist.Items[competitorlist.SelectedIndex]);
            }
        }

        private void L_savenewuser_clearall_Click(object sender, RoutedEventArgs e)
        {
            l_FUNCTION_clear_all_newuser_fields();
        }

        private void L_close_Click(object sender, RoutedEventArgs e)
        {
            l_FUNCTION_clear_all_newuser_fields();
            firstFlyout.IsOpen = false;
        }
    }


}
