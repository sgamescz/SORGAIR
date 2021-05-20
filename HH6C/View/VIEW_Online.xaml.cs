using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Model;
using System.Net;
using System.IO;
using System.Net.Cache;


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
    }
}
