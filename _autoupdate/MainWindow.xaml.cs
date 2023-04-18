using System;
using System.Linq;
using System.Windows;
using System.IO;
using System.Threading;
using System.ComponentModel;
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
using System.IO.Compression;
using System.IO;
using MahApps.Metro.Controls;
using ControlzEx.Theming;




namespace _autoupdate
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        BackgroundWorker bg = new BackgroundWorker();
        string new_random_dir;
        String ZipPath;
        string kopirovanysoubor;

        public MainWindow()
        {
            InitializeComponent();

            ThemeManager.Current.ChangeTheme(this, "Dark.Green");

            bg.DoWork += Bg_DoWork;
            bg.ProgressChanged += Bg_ProgressChanged;
            bg.RunWorkerCompleted += Bg_RunWorkerCompleted;
            bg.WorkerReportsProgress = true;


        }
        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            MessageBox.Show("Aktualizace hotova.");
            File.Delete(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ZipPath));
            var dir = new DirectoryInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, new_random_dir));
            dir.Delete(true);
            var pathdst = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\SORGAIR.exe");
            System.Diagnostics.Process.Start(pathdst);
            Application.Current.Shutdown();
        }

        private void Bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            copyprogress.Value += 1;
            copytext.Content = kopirovanysoubor;
        }

        private void Bg_DoWork(object sender, DoWorkEventArgs e)
        {
            var pathsrc = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, new_random_dir);
            var pathdst = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\.");


            try
            {
                foreach (string dirPath in Directory.GetDirectories(pathsrc, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(pathsrc, pathdst));
                }

                int zpracovanychsouboru = 0;

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(pathsrc, "*.*", SearchOption.AllDirectories))
                {
                    zpracovanychsouboru++;
                    File.Copy(newPath, newPath.Replace(pathsrc, pathdst), true);
                    bg.ReportProgress(5);
                    kopirovanysoubor = newPath.Replace(pathsrc, pathdst);
                }

            }

            catch (Exception exp)
            {
               // MessageBox.Show("File trouble " + exp.Message);
            }



        }


        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            copytext.Content = "Pracuji...";




            string path;
            path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\sorgair.zip");

            if (System.IO.File.Exists(path))
            {


                ZipPath = path;
                new_random_dir = RandomString(12);
                Console.WriteLine(new_random_dir);
                path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, new_random_dir);
                ZipFile.ExtractToDirectory(ZipPath, path);
                copyprogress.Value = 0;
                int fileCount = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Length;
                copyprogress.Maximum = fileCount;
                bg.RunWorkerAsync();

            }
            else
            {
                path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\SORGAIR.exe");
                System.Diagnostics.Process.Start(path);
                Application.Current.Shutdown();

            }



        }



    }
}
