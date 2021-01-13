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


namespace WpfApp6.View
{
    /// <summary>
    /// Interaction logic for PlayersView.xaml
    /// </summary>
    public partial class Rounds : UserControl
    {
        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;

        public Rounds()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            
            


            //StackPanel stp = ((sender as MahApps.Metro.Controls.Tile).Parent as StackPanel);
            //stp.Tag = "selected";


            string tag = (sender as MahApps.Metro.Controls.Tile).Tag.ToString();
            VM.BIND_VIEWED_ROUND = int.Parse(tag);
            VM.FUNCTION_ROUNDS_LOAD_GROUPS(VM.BIND_VIEWED_ROUND);

            for (int i = 0; i < VM.MODEL_CONTEST_ROUNDS.Count; i++)
            {
                VM.MODEL_CONTEST_ROUNDS[i].ISSELECTED = "---";
            }
            VM.MODEL_CONTEST_ROUNDS[VM.BIND_VIEWED_ROUND - 1].ISSELECTED = "selected";



        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            string tag = (sender as Button).Tag.ToString();
            VM.BIND_VIEWED_GROUP  = int.Parse(tag);

            for (int i = 0; i < VM.MODEL_CONTEST_GROUPS.Count; i++)
            {
                VM.MODEL_CONTEST_GROUPS[i].ISSELECTED = "---";
            }
            VM.MODEL_CONTEST_GROUPS[VM.BIND_VIEWED_GROUP - 1].ISSELECTED = "selected";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            VM.BIND_SELECTED_ROUND = VM.BIND_VIEWED_ROUND;
            VM.BIND_SELECTED_GROUP = VM.BIND_VIEWED_GROUP;
            VM.FUNCTION_SELECTED_ROUND_USERS(0, 0);
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



    }
}
