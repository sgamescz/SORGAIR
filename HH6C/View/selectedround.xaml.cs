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

        private ViewModel VM => this.DataContext as ViewModel;
//        ViewModel XX = new ViewModel(DialogCoordinator.Instance);

        public selectedround()
        {

            InitializeComponent();
            DataContext = VM ;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"Audio\petiminutoveho.wav");
            player.PlayLooping();
            //'player.

        }




    }
}
