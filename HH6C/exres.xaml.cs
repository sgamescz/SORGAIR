
using System.Windows;
using System;
using System.Threading;

using MahApps.Metro.Controls;

using WpfApp6.Model;
using System.IO;
using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Controls;
using System.Threading.Tasks;


namespace SORGAIR
{
    /// <summary>
    /// Interakční logika pro exres.xaml
    /// </summary>
    public partial class exres : MetroWindow
    {

        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;
        private DispatcherTimer scrollTimer;
        private bool scrollDown = true; // Směr scrollování
        private ScrollViewer dataGridScrollViewer; private int rowsPerPage;
        private int countdownValue = 10;  // Nastavení délky intervalu na 10 sekund
        private const double smoothScrollDuration = 5; // Doba trvání posunu v sekundách
        private const int timerInterval = 1000; // Interval časovače v milisekundách
        private double smoothScrollIncrement; // Inkrement pro plynulý posun

        public exres(MODEL_ViewModel viewModel)
        {
            InitializeComponent();

            // Nastavení DataContext, VM se automaticky odvodí z tohoto DataContext
            this.DataContext = viewModel;
            rowsPerPage = (int)(dataGrid_clasic_results.ActualHeight / dataGrid_clasic_results.RowHeight);

            var langcode = SORGAIR.Properties.Settings.Default.Languagecode;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langcode);

            // Protože VM je nyní odvozeno z DataContext, můžeme volat metody přímo na VM
            InitializeDatabaseAsync();
        }

        private async void InitializeDatabaseAsync()
        {
            await VM.SQL_OPENCONNECTION("SORG");
            VM.SQL_READSORGDATA("select hodnota from nastaveni where polozka='pozadi'", "pozadi");
            VM.typpozadi = VM.SQL_READSORGDATA("select hodnota from nastaveni where polozka='pozadi'", "");
            Console.WriteLine(VM.typpozadi);
            Console.WriteLine("xx");

            VM.SQL_READSORGDATA("select hodnota from nastaveni where polozka='popredi' ", "popredi");
            VM.SQL_CLOSECONNECTION("SORG");
            Loaded += (s, e) =>
            {
                dataGridScrollViewer = FindVisualChild<ScrollViewer>(dataGrid_clasic_results);
                InitializeScrolling();
                countdownLabel.Content = $"{countdownValue}s do dalšího posunu";
            };

            upravitdatagrid();
        }

        public void upravitdatagrid()
        {

            // V konstruktoru nebo při otevírání okna
            for (int i = 0; i < dataGrid_clasic_results.Columns.Count; i++)
            {
                dataGrid_clasic_results.Columns[i].Visibility = VM.GetColumnVisibility(i);
            }



            dataGrid_clasic_results.Visibility = Visibility.Visible;
            R1VISIBILITY.Visibility = Visibility.Hidden;
            R2VISIBILITY.Visibility = Visibility.Hidden;
            R3VISIBILITY.Visibility = Visibility.Hidden;
            R4VISIBILITY.Visibility = Visibility.Hidden;
            R5VISIBILITY.Visibility = Visibility.Hidden;
            R6VISIBILITY.Visibility = Visibility.Hidden;
            R7VISIBILITY.Visibility = Visibility.Hidden;
            R8VISIBILITY.Visibility = Visibility.Hidden;
            R9VISIBILITY.Visibility = Visibility.Hidden;
            R10VISIBILITY.Visibility = Visibility.Hidden;

            R11VISIBILITY.Visibility = Visibility.Hidden;
            R12VISIBILITY.Visibility = Visibility.Hidden;
            R13VISIBILITY.Visibility = Visibility.Hidden;
            R14VISIBILITY.Visibility = Visibility.Hidden;
            R15VISIBILITY.Visibility = Visibility.Hidden;
            R16VISIBILITY.Visibility = Visibility.Hidden;
            R17VISIBILITY.Visibility = Visibility.Hidden;
            R18VISIBILITY.Visibility = Visibility.Hidden;
            R19VISIBILITY.Visibility = Visibility.Hidden;
            R20VISIBILITY.Visibility = Visibility.Hidden;



            for (int i = 1; i < VM.BIND_ROUNDS_IN_RESULTS + 1; i++)
            {
                if (i == 1) { R1VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 2) { R2VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 3) { R3VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 4) { R4VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 5) { R5VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 6) { R6VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 7) { R7VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 8) { R8VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 9) { R9VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 10) { R10VISIBILITY.Visibility = Visibility.Visible; }

                if (i == 11) { R11VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 12) { R12VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 13) { R13VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 14) { R14VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 15) { R15VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 16) { R16VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 17) { R17VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 18) { R18VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 19) { R19VISIBILITY.Visibility = Visibility.Visible; }
                if (i == 20) { R20VISIBILITY.Visibility = Visibility.Visible; }

            }




        }



        private void InitializeScrolling()
        {
            // Vypočítat inkrement pro plynulý posun, inkrement musí být větší, například alespoň 5 pixelů na posun
            int minIncrement = 5; // minimální pixelový posun pro viditelný efekt
            int totalSteps = (int)(smoothScrollDuration * 10000 / timerInterval);
            smoothScrollIncrement = Math.Max(minIncrement, dataGridScrollViewer.ViewportHeight / totalSteps);
            Console.WriteLine("Smooth Scroll Increment: " + smoothScrollIncrement);  // Ladění: Vypsat inkrement

            scrollTimer = new DispatcherTimer();
            scrollTimer.Interval = TimeSpan.FromMilliseconds(timerInterval);
            scrollTimer.Tick += ScrollTimer_Tick;
            scrollTimer.Start();
        }

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            countdownValue--;
            Console.WriteLine("Countdown Value: " + countdownValue);  // Ladění: Vypsat zbývající čas
            if (countdownValue <= 0)
            {
                PerformSmoothScroll();
                countdownValue = 10;  // Reset countdown after a full scroll
            }
            countdownLabel.Content = $"{countdownValue}s do dalšího posunu v kole {VM.BIND_ROUNDS_IN_RESULTS}";
        }
        private void PerformSmoothScroll()
        {
            if (dataGridScrollViewer == null) return;

            double currentOffset = dataGridScrollViewer.VerticalOffset;
            double maxOffset = dataGridScrollViewer.ExtentHeight - dataGridScrollViewer.ViewportHeight;
            Console.WriteLine("Current Offset: " + currentOffset);  // Ladění: Vypsat aktuální offset

            double newOffset;
            if (scrollDown)
            {
                newOffset = Math.Min(currentOffset + smoothScrollIncrement, maxOffset);
                dataGridScrollViewer.ScrollToVerticalOffset(newOffset);
                if (newOffset >= maxOffset)
                {
                    scrollDown = false;  // Change direction
                    Console.WriteLine("Reached max offset. Changing direction to up.");  // Ladění
                }
            }
            else
            {
                newOffset = Math.Max(currentOffset - smoothScrollIncrement, 0);
                dataGridScrollViewer.ScrollToVerticalOffset(newOffset);
                if (newOffset <= 0)
                {
                    scrollDown = true;  // Change direction
                    Console.WriteLine("Reached min offset. Changing direction to down.");  // Ladění
                }
            }
        }

            private void ScrollDataGrid()
        {
            if (dataGridScrollViewer == null) return;

            var maxOffset = dataGridScrollViewer.ExtentHeight - dataGridScrollViewer.ViewportHeight;
            var offsetStep = dataGridScrollViewer.ViewportHeight;

            if (scrollDown)
            {
                if (dataGridScrollViewer.VerticalOffset >= maxOffset)
                {
                    scrollDown = false;  // Změnit směr na scrollování nahoru
                    dataGridScrollViewer.ScrollToVerticalOffset(dataGridScrollViewer.VerticalOffset - offsetStep);
                }
                else
                {
                    dataGridScrollViewer.ScrollToVerticalOffset(dataGridScrollViewer.VerticalOffset + offsetStep);
                }
            }
            else
            {
                if (dataGridScrollViewer.VerticalOffset <= 0)
                {
                    scrollDown = true;  // Změnit směr na scrollování dolů
                    dataGridScrollViewer.ScrollToVerticalOffset(dataGridScrollViewer.VerticalOffset + offsetStep);
                }
                else
                {
                    dataGridScrollViewer.ScrollToVerticalOffset(dataGridScrollViewer.VerticalOffset - offsetStep);
                }
            }

            // Reset odpočtu po každém posunu
            countdownValue = 10;
            countdownLabel.Content = $"{countdownValue}s do dalšího posunu"; // Okamžitá aktualizace Labelu
        }
        private static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is T children)
                    return children;
                else
                {
                    var childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }

}
