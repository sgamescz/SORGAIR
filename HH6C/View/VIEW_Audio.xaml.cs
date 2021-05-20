using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Model;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;


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

    

        private void soundfile_basicround2_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void soundfile_basicroundprep2_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void soundfile_finalround_prep2_Click(object sender, RoutedEventArgs e)
        {
                    }

        private async void soundfile_save_and_load_all(object sender, RoutedEventArgs e)
        {



            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync(SORGAIR.Properties.Lang.Lang.home_load_caption, SORGAIR.Properties.Lang.Lang.home_load_caption);
            controller.SetProgress(0.2);
            controller.SetMessage(string.Format(SORGAIR.Properties.Lang.Lang.home_load_sounds));
            await Task.Delay(1000);



            if (soundfile_finalround_prep.SelectedIndex >= 0) {

                Console.WriteLine("LOADZVUKU_FINAL_PREP");
                VM.FUNCTION_SOUND_LOADSELECTEDSOUND_FINAL_MAIN(VM.BINDING_SoundList[VM.BIND_AUDIO_SELECTEDFINALSOUND_INDEX].Id);
                VM.BIND_AUDIO_FINAL_INFO = VM.BINDING_SoundList[VM.BIND_AUDIO_SELECTEDFINALSOUND_INDEX].SoundName;
            }

            controller.SetProgress(0.4);
            await Task.Delay(100);

            if (soundfile_finalround.SelectedIndex >= 0)
            {
                Console.WriteLine("LOADFINALZVUKU");
                VM.FUNCTION_SOUND_LOADSELECTEDSOUND_FINAL_PREP(VM.BINDING_SoundList[VM.BIND_AUDIO_SELECTEDPREPFINALSOUND_INDEX].Id);
                VM.BIND_AUDIO_FINAL_PREP_INFO = VM.BINDING_SoundList[VM.BIND_AUDIO_SELECTEDPREPFINALSOUND_INDEX].SoundName;
            }

            controller.SetProgress(0.6);
            await Task.Delay(100);

            if (soundfile_basicround.SelectedIndex >= 0)
            {
                Console.WriteLine("LOADZVUKU");
                VM.FUNCTION_SOUND_LOADSELECTEDSOUND_MAIN(VM.BINDING_SoundList[VM.BIND_AUDIO_SELECTEDBASESOUND_INDEX].Id);
                VM.BIND_AUDIO_INFO = VM.BINDING_SoundList[VM.BIND_AUDIO_SELECTEDBASESOUND_INDEX].SoundName;
            }

            controller.SetProgress(0.8);
            await Task.Delay(100);

            if (soundfile_basicround_prep.SelectedIndex >= 0)
            {
                Console.WriteLine("LOADZVUKU_PREP");
                VM.FUNCTION_SOUND_LOADSELECTEDSOUND_PREP(VM.BINDING_SoundList[VM.BIND_AUDIO_SELECTEDPREPSOUND_INDEX].Id);
                VM.BIND_AUDIO_PREP_INFO = VM.BINDING_SoundList[VM.BIND_AUDIO_SELECTEDPREPSOUND_INDEX].SoundName;
            }

            controller.SetProgress(1);
            controller.SetMessage(string.Format(SORGAIR.Properties.Lang.Lang.home_load_complete));
            await Task.Delay(100);
            await controller.CloseAsync();



        }
    }
}
