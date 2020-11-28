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
    /// Interakční logika pro nastavenisouteze.xaml
    /// </summary>
    public partial class nastavenisouteze : UserControl
    {
        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;
        public nastavenisouteze()
        {
            InitializeComponent();

        }



        private async void nastaveni_soutez_Click(object sender, RoutedEventArgs e)
        {

            string content = (sender as Tile).Tag.ToString();
            string[] TAGY = content.Split('|');
            Console.WriteLine(TAGY.Length);
            int a = TAGY.Length / 3;

            for (int i = 0; i < a; i++)
            {
                string vyplnenyinput = "";

                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_NAZEV")
                {
                    String nazev = VM.BIND_SQL_SOUTEZ_NAZEV;
                    char[] spearator1 = { ':' };
                    String[] strlist1 = nazev.Split(spearator1);
                    vyplnenyinput = strlist1[1];
                }
                    
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_LOKACE") {
                    String lokace = VM.BIND_SQL_SOUTEZ_LOKACE;
                    char[] spearator2 = { ':'};
                    String[] strlist2 = lokace.Split(spearator2);
                    vyplnenyinput = strlist2[1];
                }


                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_DATUM") { vyplnenyinput = VM.BIND_SQL_SOUTEZ_DATUM ;}
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_KATEGORIE") { vyplnenyinput = VM.BIND_SQL_SOUTEZ_KATEGORIE ;}
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_TEPLOTA") { vyplnenyinput = VM.BIND_SQL_SOUTEZ_TEPLOTA.Substring(0, VM.BIND_SQL_SOUTEZ_TEPLOTA.Length-2) ;}
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_POCASI") { vyplnenyinput = VM.BIND_SQL_SOUTEZ_POCASI ;}
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_CLUB") { vyplnenyinput = VM.BIND_SQL_SOUTEZ_CLUB ;}
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_SMCRID") { vyplnenyinput = VM.BIND_SQL_SOUTEZ_SMCRID ;}
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_DIRECTOR") { vyplnenyinput = VM.BIND_SQL_SOUTEZ_DIRECTOR ;}
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_HEADJURY") { vyplnenyinput = VM.BIND_SQL_SOUTEZ_HEADJURY ;}
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_JURY1") { vyplnenyinput = VM.BIND_SQL_SOUTEZ_JURY1 ;}
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_JURY2") { vyplnenyinput = VM.BIND_SQL_SOUTEZ_JURY2 ;}
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_JURY3") { vyplnenyinput = VM.BIND_SQL_SOUTEZ_JURY3 ;}

                var currentWindow = this.TryFindParent<MetroWindow>();
                var result = await currentWindow.ShowInputAsync(TAGY[(i*3)], TAGY[(i * 3)+1], new MetroDialogSettings() { DefaultText = vyplnenyinput });
                if (result == null)
                    return;

                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_NAZEV") { VM.BIND_SQL_SOUTEZ_NAZEV = result; }
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_LOKACE") { VM.BIND_SQL_SOUTEZ_LOKACE = result; }
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_DATUM") { VM.BIND_SQL_SOUTEZ_DATUM = result; }
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_KATEGORIE") { VM.BIND_SQL_SOUTEZ_KATEGORIE = result; }
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_TEPLOTA") { VM.BIND_SQL_SOUTEZ_TEPLOTA = result; }
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_POCASI") { VM.BIND_SQL_SOUTEZ_POCASI = result; }
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_CLUB") { VM.BIND_SQL_SOUTEZ_CLUB = result; }
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_SMCRID") { VM.BIND_SQL_SOUTEZ_SMCRID = result; }
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_DIRECTOR") { VM.BIND_SQL_SOUTEZ_DIRECTOR = result; }
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_HEADJURY") { VM.BIND_SQL_SOUTEZ_HEADJURY = result; }
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_JURY1") { VM.BIND_SQL_SOUTEZ_JURY1 = result; }
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_JURY2") { VM.BIND_SQL_SOUTEZ_JURY2 = result; }
                if (TAGY[(i * 3) + 2] == "BIND_SQL_SOUTEZ_JURY3") { VM.BIND_SQL_SOUTEZ_JURY3 = result; }
            }




        }

        private void Roundsetingupdown(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            int xxx = Convert.ToInt32((sender as NumericUpDown).Value);
            string tagy = (sender as NumericUpDown).Tag.ToString();

            try
            {
                if (tagy == "startpoints") { VM.BIND_SQL_SOUTEZ_STARTPOINTS = xxx; }
                if (tagy == "deletes") { VM.BIND_SQL_SOUTEZ_DELETES = xxx; }
                if (tagy == "rounds") { VM.BIND_SQL_SOUTEZ_ROUNDS = xxx; }
                if (tagy == "startpointsfinale") { VM.BIND_SQL_SOUTEZ_STARTPOINTSFINALE = xxx; }
                if (tagy == "deletesfinale") { VM.BIND_SQL_SOUTEZ_DELETESFINALE = xxx; }
                if (tagy == "roundsfinale") { VM.BIND_SQL_SOUTEZ_ROUNDSFINALE = xxx; }

            }
            catch
            {
                Console.WriteLine("EER numericupdown - mohu ignorovat");
                //MessageBox.Show("err");
            }
        }

        private void landingoptions_IsCheckedChanged(object sender, EventArgs e)
        {
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
          //'  VM.FUNCTION_LOADCONTEST();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_LOADCONTEST();

        }
    }


}
