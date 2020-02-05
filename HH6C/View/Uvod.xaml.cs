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
            
            
            
        }


        public async Task DoSomethingAsync()
        {
            Console.WriteLine("start");

            // In the Real World, we would actually do something...
            // For this example, we're just going to (asynchronously) wait 100ms.
            //await this.ShowMessageAsync("This is the title", "Some message", MessageDialogStyle.AffirmativeAndNegative);

            Console.WriteLine("konec");
        }

    }
}
