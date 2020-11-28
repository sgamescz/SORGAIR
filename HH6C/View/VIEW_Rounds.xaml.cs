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

            string tag = (sender as MahApps.Metro.Controls.Tile).Tag.ToString();
            VM.BIND_VIEWED_ROUND = int.Parse(tag);
            VM.FUNCTION_ROUNDS_LOAD_GROUPS(VM.BIND_VIEWED_ROUND);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string tag = (sender as Button).Tag.ToString();
            VM.BIND_VIEWED_GROUP  = int.Parse(tag);
            //VM.FUNCTION_ROUNDS_LOAD_GROUPS(VM.BIND_SELECTED_GROUP);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            VM.BIND_SELECTED_ROUND = VM.BIND_VIEWED_ROUND;
            VM.BIND_SELECTED_GROUP = VM.BIND_VIEWED_GROUP;
            VM.FUNCTION_TEAM_ACTIVEMEMBERS(0, 0);
            
        }
    }
}
