using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Model;
using System.Net;
using System.IO;
using System.Net.Cache;
using System.Linq;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Threading.Tasks;



namespace WpfApp6.View
{
    /// <summary>
    /// Interakční logika pro UserControl1.xaml
    /// </summary>
    public partial class Online : UserControl
    {
        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;

        public Online()
        {

        InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            

            string[] mArrayOfcontests = new string[300];


            string remoteUrl = "http://api.stoupak.cz/sorgair/2021/api_results_contestverify.php?id=1;";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            HttpWebRequest.DefaultCachePolicy = policy;

            httpRequest.CachePolicy = policy;
            WebResponse response = httpRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();

            Console.WriteLine(result);

            String[] spearator = { "<br>" };
            String[] strlist = result.Split(spearator, 100, StringSplitOptions.None);
            foreach (String soutez in strlist)
            {
                Console.WriteLine(soutez);

                String[] spearator_sub = { "|" };

                if (soutez.Length > 5)
                {
                    //var contests = new MODEL_Contests_files()
                    //{

  //                      FILENAME = soutez.Split(spearator_sub, 100, StringSplitOptions.RemoveEmptyEntries)[3],
    //                    CATEGORY = BIND_NEWCONTEST_CATEGORY,
      //                  NAME = soutez.Split(spearator_sub, 100, StringSplitOptions.RemoveEmptyEntries)[0],
        //                LOCATION = soutez.Split(spearator_sub, 100, StringSplitOptions.RemoveEmptyEntries)[2],
          //              DATE = soutez.Split(spearator_sub, 100, StringSplitOptions.RemoveEmptyEntries)[1]
            //        };
              //      MODEL_CONTESTS_ONLINE.Add(contests);

                }




            }

        }

      

        private void newcontestid_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_GENERATE_RANDOM_STRING(8);
        }


        public static void UploadFileToFtp(string url, string filePath, string username, string password, string contestid)
        {
            
            var fileName = Path.GetFileName(filePath);
            var request = (FtpWebRequest)WebRequest.Create(url + "/__ID__" + contestid + "__NAME__"+ fileName);

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(username, password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            using (var fileStream = File.OpenRead(filePath))
            {
                using (var requestStream = request.GetRequestStream())
                {
                    fileStream.CopyTo(requestStream);
                    requestStream.Close();
                }
            }

            var response = (FtpWebResponse)request.GetResponse();
            Console.WriteLine("Upload done: {0}", response.StatusDescription);
            response.Close();
        }


   

        private async void uploaddb_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Odesílám", "Odesílá se soubor soutěže na stoupák");
            await Task.Delay(300);
            controller.SetProgress(0.5);
            UploadFileToFtp("ftp://187428.w28.wedos.net", "Upload/"+ VM.BIND_SQL_SOUTEZ_DBFILE + ".db", "w187428_sorgairupload", "WU37pfeN", VM.CONTENT_RANDOM_ID);
            controller.SetProgress(0.9);
            await Task.Delay(300);
            await controller.CloseAsync();
            await currentWindow.ShowMessageAsync("Odeslání soutěže", "Data soutěže byly odeslány na stoupak.cz, díky!", MessageDialogStyle.Affirmative, new MetroDialogSettings() { AnimateShow = true, AnimateHide = true });

        }
    }
}
