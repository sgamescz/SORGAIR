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
using System.Threading;


namespace WpfApp6.View
{
    /// <summary>
    /// Interakční logika pro FirstView.xaml
    /// </summary>
    public partial class Uvod : UserControl
    {

        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;
//        ViewModel XX = new ViewModel(DialogCoordinator.Instance);

        public Uvod()
        {

            InitializeComponent();
            //DataContext = VM ;
        }



        private async void Button_Click(object sender, RoutedEventArgs e)
        {


            // Get the Parent MertoWindow here. We could also use a dialogcoordinator here if we want to.
            var currentWindow = this.TryFindParent<MetroWindow>();
            var result = await currentWindow.ShowInputAsync("Hi", "What's your name?");
            await currentWindow.ShowMessageAsync("Hi again", $"You are welcome {result}", MessageDialogStyle.AffirmativeAndNegativeAndDoubleAuxiliary );
            var controller = await currentWindow.ShowProgressAsync("Please wait...", "Progress message");

            await Task.Delay(3000);
            controller.SetTitle("Magnetometer Calibration");
            for (int i = 0; i < 101; i++)
            {
                controller.SetProgress(i / 100.0);
                controller.SetMessage(string.Format("Rotate the controller in all directions: {0}%", i));

                if (controller.IsCanceled) break;
                await Task.Delay(100);
            }
            await controller.CloseAsync();

            if (controller.IsCanceled)
            {
                await currentWindow.ShowMessageAsync("Magnetometer Calibration", "Calibration has been cancelled.");
            }
            else
            {
                await currentWindow.ShowMessageAsync("Magnetometer Calibration", "Calibration finished successfully.");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_LOADCONTEST();
            VM.FUNCTION_USERS_LOAD_ALLCOMPETITORS();
            VM.FUNCTION_TEAM_LOAD_TEAMS();
            VM.FUNCTION_ROUNDS_LOAD_ROUNDS();
            //VM.BIND_VYBRANEKOLOMENU = "Vybrané kolo: 1/4";
            VM.clock_create ();
            VM.BIND_MENU_ENABLED_finale = false;
            VM.BIND_MENU_ENABLED_detailyastatistiky = false;
            VM.BIND_MENU_ENABLED_online = false;
            VM.BIND_MENU_ENABLED_nastavenisouteze = true;
            VM.BIND_MENU_ENABLED_audioadalsi = true;
            VM.BIND_MENU_ENABLED_hardware = true;
            VM.BIND_MENU_ENABLED_soutezici = true;
            VM.BIND_MENU_ENABLED_rozlosovani = true;
            VM.BIND_MENU_ENABLED_vybranekolo = true;
            VM.BIND_MENU_ENABLED_vysledky = true;
            VM.BIND_MENU_ENABLED_seznamkol = true;
            VM.BIND_MENU_ENABLED_online = true;
            VM.BIND_MENU_ENABLED_detailyastatistiky = true;
            VM.BIND_MENU_ENABLED_finale  = true;


        }



        public void thread2()
        {
            var result = string.Empty;
            using (var webClient = new System.Net.WebClient())
            {
                webClient.Encoding = System.Text.Encoding.UTF8;
                result = webClient.DownloadString("https://stoupak.cz");
            }

            this.Invoke(() => VM.BIND_SORGNEWS = result);

        }
        public void download_news(object sender, RoutedEventArgs e)
        {

            VM.FUNCTION_LOAD_MATRIX_FILES();
            //Thread test = new Thread(new ThreadStart(thread2));
            //test.Start();


        }
    }
}
