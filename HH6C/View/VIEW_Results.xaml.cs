﻿using System;
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
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;


namespace WpfApp6.View
{
    /// <summary>
    /// Interakční logika pro RozvrhView.xaml
    /// </summary>
    public partial class Results : UserControl
    {
        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;
        public Results()
        {
            InitializeComponent();
        }

       

        private void printresults_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printdialog = new PrintDialog();
            if (printdialog.ShowDialog() == true)
            {
                printdialog.PrintVisual(dataGrid1, "test");

            }
        }

        private void results_users_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_RESULTS_LOADBASERESULTS("users");
        }

        private void results_teams_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_RESULTS_LOADBASERESULTS("teams");
            //VM.FUNCTION_ROUNDS_LOAD_FINAL_ROUNDS();

        }

        private async void results_move_to_final_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            var result = await currentWindow.ShowMessageAsync("Postoupit do finále?", "Opravdu postoupit zobrazené soutěžící do finálových kol?", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { AnimateShow = true, AnimateHide = true});
            if (result == null)
                return;
            if (result == MessageDialogResult.Affirmative)
            {
                VM.FUNCTION_ROUNDS_CREATE_FINAL_ROUNDS();
                VM.BINDING_selectedmenuindex = 10;
            }



        }
    }
}
