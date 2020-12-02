using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Model;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System;


namespace WpfApp6.View
{
    /// <summary>
    /// Interaction logic for PlayersView.xaml
    /// </summary>
    public partial class Draw : UserControl
    {

        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;

        public Draw()
        {
            InitializeComponent();

    }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btn_create_new_matrix_Click(object sender, RoutedEventArgs e)
        {


            // Get the Parent MertoWindow here. We could also use a dialogcoordinator here if we want to.
            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Please wait...", "Progress message");

            controller.SetTitle("Matrix creator");

            


            for (int r = 1; r < VM.BIND_SQL_SOUTEZ_ROUNDS+1; r++)
            {


                for (int g = 1; g < VM.BIND_SQL_SOUTEZ_GROUPS+1; g++)
                {

                    for (int s = 1; s < VM.BIND_SQL_SOUTEZ_STARTPOINTS + 1; s++)
                    {

                        double x = (VM.BIND_SQL_SOUTEZ_ROUNDS + 1)* (VM.BIND_SQL_SOUTEZ_GROUPS + 1);
                        double progg = ((((r-1)* (VM.BIND_SQL_SOUTEZ_GROUPS + 1)) +g) / x);

                        Console.WriteLine(progg);
                        VM.SQL_SAVESOUTEZDATA("insert into matrix (rnd,grp,stp,user) values (" + r + "," + g + "," + s + ",0)");
                        controller.SetProgress(progg);
                        controller.SetMessage(string.Format("Creating new matrix: {0}%", (progg*100)));
                        await Task.Delay(1);
                    }


                }

            }
            controller.SetProgress(0.95);
            await Task.Delay(100);
            controller.SetProgress(1);
            controller.SetMessage("Creating new matrix: 100% - Done");
            await Task.Delay(1000);
            VM.FUNCTION_ROUNDS_LOAD_ROUNDS();
            await controller.CloseAsync();


        }

        private void btn_delete_matrix_Click(object sender, RoutedEventArgs e)
        {
            VM.SQL_SAVESOUTEZDATA("delete from matrix");
            VM.FUNCTION_ROUNDS_LOAD_ROUNDS();

        }
    }
}
