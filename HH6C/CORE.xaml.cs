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
using MahApps.Metro.Controls;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
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


namespace WpfApp6
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        string[] barva = new string[] { "Red", "Green", "Blue", "Purple", "Orange", "Lime", "Emerald", "Teal", "Cyan", "Cobalt", "Indigo", "Violet", "Pink", "Magenta", "Crimson", "Amber", "Yellow", "Brown", "Olive", "Steel", "Mauve", "Taupe", "Sienna" };
        string[] pozadi = new string[] { "Light", "Dark" };
        int pouzitabarva = 1;
        int pouzitepozadi = 1;



        public MainWindow()
        {

            InitializeComponent();

            this.DataContext = new ViewModel.MainViewModel();
            MahApps.Metro.ThemeManager.ChangeTheme(Application.Current, pozadi[pouzitepozadi], barva[pouzitabarva]);
        }

        private void zmenbarvupopredi(object sender, RoutedEventArgs e)
        {
            pouzitabarva = pouzitabarva + 1;
            if (pouzitabarva == 23)
            {
                pouzitabarva = 0;
            }

            MahApps.Metro.ThemeManager.ChangeTheme(Application.Current, pozadi[pouzitepozadi], barva[pouzitabarva]);
        }

        private void zmenbarvupozadi(object sender, RoutedEventArgs e)
        {

            pouzitepozadi = pouzitepozadi + 1;


            if (pouzitepozadi == 2)
            {
                pouzitepozadi = 0;
            }

            MahApps.Metro.ThemeManager.ChangeTheme(Application.Current, pozadi[pouzitepozadi].ToString(), barva[pouzitabarva].ToString());

        }


    }
}
