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
    public partial class Uvod : UserControl
    {

        private ViewModel VM => this.DataContext as ViewModel;
//        ViewModel XX = new ViewModel(DialogCoordinator.Instance);

        public Uvod()
        {

            InitializeComponent();
            DataContext = VM ;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the Parent MertoWindow here. We could also use a dialogcoordinator here if we want to.
            var currentWindow = this.TryFindParent<MetroWindow>();
            var result = await currentWindow.ShowInputAsync("Hi", "What's your name?");
            await currentWindow.ShowMessageAsync("Hi again", $"You are welcome {result}");
        }


      

    }
}
