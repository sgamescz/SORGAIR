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
    /// Interakční logika pro HledaniView.xaml
    /// </summary>
    public partial class Audio : UserControl
    {
        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;

        public Audio()
        {
            InitializeComponent();
        }

        private void test(object sender, RoutedEventArgs e)
        {
           // MainWindow.hledejvsql("blabla");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (soundfile_basicround.SelectedIndex >= 0)
            {
                VM.FUNCTION_SOUND_LOADSELECTEDSOUND(VM.BINDING_SoundList[soundfile_basicround.SelectedIndex].Id);
            }
        }
    }
}
