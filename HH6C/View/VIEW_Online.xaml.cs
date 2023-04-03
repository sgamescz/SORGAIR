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

using System.Net;
using System.Net.Mail;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Model;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Threading;
using System.Net;
using System.IO;
using System.Net.Cache;
using System.Globalization;

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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            var currentWindow = this.TryFindParent<MetroWindow>();



            string[] mArrayOfcontests = new string[300];

            //toto overuje, zda soutez s takovym XYASDASD id existuje
            string remoteUrl = "http://api.sorgair.com/api_online_results.php?action=verifyifexist&id="+VM.CONTENT_RANDOM_ID;
            Console.WriteLine(remoteUrl);
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            HttpWebRequest.DefaultCachePolicy = policy;

            httpRequest.CachePolicy = policy;
            WebResponse response = httpRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();

            Console.WriteLine(result);
            if (result == "0")
            {
                Console.WriteLine("soutez neexistuje");


                var controller = await currentWindow.ShowProgressAsync("Online systém", "Vytvářím online záznam soutěže");
                controller.SetProgress(0);


                //toto vytvori soutez jako takovou
                remoteUrl = "http://api.sorgair.com/api_online_results.php?action=createcontest&contesID=" + VM.BIND_SQL_SOUTEZ_SMCRID 
                    + "&category="+VM.BIND_SQL_SOUTEZ_KATEGORIE
                    + "&name=" + VM.BIND_SQL_SOUTEZ_NAZEV
                    + "&place=" + VM.BIND_SQL_SOUTEZ_LOKACE
                    + "&organisator=" + VM.BIND_SQL_SOUTEZ_CLUB
                    + "&date=" + VM.BIND_SQL_SOUTEZ_DATUM
                    + "&contestdirector=" + VM.BIND_SQL_SOUTEZ_DIRECTOR
                    + "&wheater=" + VM.BIND_SQL_SOUTEZ_POCASI
                    + "&headjury=" + VM.BIND_SQL_SOUTEZ_HEADJURY
                    + "&jurymember1=" + VM.BIND_SQL_SOUTEZ_JURY1
                    + "&jurymember2=" + VM.BIND_SQL_SOUTEZ_JURY2
                    + "&jurymember3=" + VM.BIND_SQL_SOUTEZ_JURY3
                    + "&stat=" + VM.BIND_SQL_SOUTEZ_STAT
                    + "&isjuniors=False"
                    + "&sorgairidentifikator=" + VM.CONTENT_RANDOM_ID
                    ;
                Console.WriteLine(remoteUrl);
                httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
                policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                HttpWebRequest.DefaultCachePolicy = policy;


                await Task.Delay(300);
                controller.SetProgress(0.2);
                
                httpRequest.CachePolicy = policy;
                response = httpRequest.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                result = reader.ReadToEnd();
                int noveonlineidsouteze=0;
                int.TryParse(result,out noveonlineidsouteze);
                Console.WriteLine(noveonlineidsouteze);



                //toto vytvari vsechny soutezici
                for (int i = 0; i < VM.Players.Count; i++)
                {



                    remoteUrl = "http://api.sorgair.com/api_online_results.php?action=createcompetitor&noveonlineidsouteze=" + noveonlineidsouteze
                        + "&insorgid=" + VM.Players[i].ID
                        + "&firstname=" + VM.Players[i].FIRSTNAME
                        + "&lastname=" + VM.Players[i].LASTNAME
                        + "&country=" + VM.Players[i].COUNTRY
                        + "&agecat=" + VM.Players[i].AGECATID
                        + "&failic=" + VM.Players[i].FAILIC
                        + "&naclic=" + VM.Players[i].NACLIC
                        + "&freq=" + VM.Players[i].FREQID
                        + "&ch1=" + VM.Players[i].CH1
                        + "&ch2=" + VM.Players[i].CH2
                        + "&club=" + VM.Players[i].CLUB
                        + "&flag=" + VM.Players[i].FLAG
                        + "&paid=" + VM.Players[i].PAIDSTR
                        + "&team=" + VM.Players[i].TEAM
                        + "&customagecat=" + VM.Players[i].CUSTOMAGECAT
                        ;
                    Console.WriteLine(remoteUrl);
                    httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
                    policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                    HttpWebRequest.DefaultCachePolicy = policy;

                    httpRequest.CachePolicy = policy;
                    response = httpRequest.GetResponse();
                    reader = new StreamReader(response.GetResponseStream());
                    result = reader.ReadToEnd();


                }



                await Task.Delay(300);
                controller.SetProgress(0.4);


                // vytvori to kola
                for (int i = 0; i < VM.BIND_SQL_SOUTEZ_ROUNDS; i++)
                {



                    remoteUrl = "http://api.sorgair.com/api_online_results.php?action=createround&noveonlineidsouteze=" + noveonlineidsouteze
                        + "&desc=" +(i+1)
                        + "&desc2=" + (i+1)
                        ;
                    Console.WriteLine(remoteUrl);
                    httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
                    policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                    HttpWebRequest.DefaultCachePolicy = policy;

                    httpRequest.CachePolicy = policy;
                    response = httpRequest.GetResponse();
                    reader = new StreamReader(response.GetResponseStream());
                    result = reader.ReadToEnd();


                    // toto vytvari v klech jeste skupiny
                    for (int y = 0; y < VM.BIND_SQL_SOUTEZ_GROUPS; y++)
                    {



                        remoteUrl = "http://api.sorgair.com/api_online_results.php?action=creategroup&noveonlineidsouteze=" + noveonlineidsouteze
                            + "&round=" + (i + 1)
                            + "&desc=" + (y + 1)
                            + "&desc2=" + (y + 1)
                            ;
                        Console.WriteLine(remoteUrl);
                        httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
                        policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                        HttpWebRequest.DefaultCachePolicy = policy;

                        httpRequest.CachePolicy = policy;
                        response = httpRequest.GetResponse();
                        reader = new StreamReader(response.GetResponseStream());
                        result = reader.ReadToEnd();


                    }



                }



                await Task.Delay(300);
                controller.SetProgress(0.7);

                for (int x = 1; x < VM.BIND_SQL_SOUTEZ_ROUNDS + 1; x++)
                {
                   
                    for (int g = 1; g < VM.BIND_SQL_SOUTEZ_GROUPS + 1; g++)
                    {
                    
                        for (int stp = 1; stp < VM.BIND_SQL_SOUTEZ_STARTPOINTS + 1; stp++)
                        {
                            string kdo = VM.SQL_READSOUTEZDATA("select user from matrix where rnd=" + x.ToString() + " and grp=" + g.ToString() + " and stp=" + stp.ToString() + " ; ", "");


                              remoteUrl = "http://api.sorgair.com/api_online_results.php?action=createdraw&noveonlineidsouteze=" + noveonlineidsouteze
                            + "&round=" + x
                            + "&group=" + g
                            + "&stp=" + stp
                            + "&insorguserid=" + kdo
                            ;
                            Console.WriteLine(remoteUrl);
                        httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
                        policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                        HttpWebRequest.DefaultCachePolicy = policy;

                        httpRequest.CachePolicy = policy;
                        response = httpRequest.GetResponse();
                        reader = new StreamReader(response.GetResponseStream());
                        result = reader.ReadToEnd();


                        }
                    }


                }

                controller.SetProgress(0.9);
                await Task.Delay(300);
                await controller.CloseAsync();







                var msgresult = await currentWindow.ShowMessageAsync("Online systém", "Soutěž vytvořena a aktivována. Nyní se budou výsledky realtime přenášet do SAEMu."
  , MessageDialogStyle.Affirmative, new MetroDialogSettings() { AnimateShow = true, AnimateHide = true });




            }
            else
            {


                var msgresult = await currentWindow.ShowMessageAsync("Online systém", "Nelze vytvořit online záznam s tímto ID. Prosím vygenerujte nové  ikonou vlevo."
    , MessageDialogStyle.Affirmative, new MetroDialogSettings() { AnimateShow = true, AnimateHide = true });





                Console.WriteLine("soutez bohuzel existuje");

            }



        }

      

        private void newcontestid_Click(object sender, RoutedEventArgs e)
        {
            VM.FUNCTION_GENERATE_RANDOM_STRING(8);
            generateQRcode(sender ,e);
        }


        public static void UploadFileToFtp(string url, string filePath, string username, string password, string contestid)
        {
            
            var fileName = Path.GetFileName(filePath);
            var request = (FtpWebRequest)WebRequest.Create(url + "/" + contestid  );

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(username, password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            using (var fileStream = File.OpenRead("Print/" + filePath))
            {
                using (var requestStream = request.GetRequestStream())
                {
                    fileStream.CopyTo(requestStream);
                    requestStream.Close();
                }
            }

            var response = (FtpWebResponse)request.GetResponse();
            Console.WriteLine("Upload done: {0}", response.StatusDescription);
            Console.WriteLine(contestid);
            response.Close();
        }


   

        private async void uploaddb_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            MessageDialogResult result = await currentWindow.ShowMessageAsync("Odeslat?", "Odeslat výsledovku na stoupak.cz ?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {

                vytvor_megavysledovku_pro_odeslani_a_odesli("stoupak.cz", "html");
            }



        }

        private void generateQRcode(object sender, RoutedEventArgs e)
        {

            QRCoder.QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
            QRCoder.QRCodeData qrCodeData = qrGenerator.CreateQrCode(VM.CONTENT_ONLINE_URL, QRCoder.QRCodeGenerator.ECCLevel.Q);
            QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);
            System.Drawing.Bitmap  qrCodeImage = qrCode.GetGraphic(20);

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);
            


            qrCodeImage.Save(directory + "/qr/" + VM.CONTENT_RANDOM_ID + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            QRIMG.Source = ConvertBitmap(qrCodeImage);
            var SigBase64 = "";
            using (var ms = new MemoryStream())
            {
                using (var bitmap = new System.Drawing.Bitmap(qrCodeImage))
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    SigBase64  = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                }
            }


        }


        private async void vytvor_megavysledovku_pro_odeslani_a_odesli(string kamposlat, string output_type)
        {



            string[] visibility = {
                "Visible"
            };

            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Vytvářím velmi zajmavou statistiku");
            controller.SetProgress(0);
            await Task.Delay(300);
            controller.SetProgress(0.5);



            VM.print_userslist("frame_empty", "data_userlist", "print_userlist", "Seznam soutěžících", "memory");


            //VM.print_userstatistics("frame_empty", "data_userstatistics", "print_userstatistics", "Statistiky uživatelů", "memory");


            VM.print_matrix("frame_empty", "data_matrix", "print_basic_resuls", "Rozlosování", "memory");


            #region Základní výsledky


            VM.FUNCTION_RESULTS_LOAD_RESULTS("users", VM.BIND_SQL_SOUTEZ_ROUNDS, 99);

            visibility = new string[] {
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True"
            };



            if (1 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[5] = "True"; } else { visibility[5] = "False"; }
            if (2 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[6] = "True"; } else { visibility[6] = "False"; }
            if (3 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[7] = "True"; } else { visibility[7] = "False"; }
            if (4 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[8] = "True"; } else { visibility[8] = "False"; }
            if (5 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[9] = "True"; } else { visibility[9] = "False"; }
            if (6 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[10] = "True"; } else { visibility[10] = "False"; }
            if (7 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[11] = "True"; } else { visibility[11] = "False"; }
            if (8 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[12] = "True"; } else { visibility[12] = "False"; }
            if (9 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[13] = "True"; } else { visibility[13] = "False"; }
            if (10 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[14] = "True"; } else { visibility[14] = "False"; }

            if (11 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[15] = "True"; } else { visibility[15] = "False"; }
            if (12 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[16] = "True"; } else { visibility[16] = "False"; }
            if (13 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[17] = "True"; } else { visibility[17] = "False"; }
            if (14 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[18] = "True"; } else { visibility[18] = "False"; }
            if (15 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[19] = "True"; } else { visibility[19] = "False"; }
            if (16 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[20] = "True"; } else { visibility[20] = "False"; }
            if (17 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[21] = "True"; } else { visibility[21] = "False"; }
            if (18 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[22] = "True"; } else { visibility[22] = "False"; }
            if (19 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[23] = "True"; } else { visibility[23] = "False"; }
            if (20 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[24] = "True"; } else { visibility[24] = "False"; }



            VM.print_basicresults("frame_empty", "data_empty", "print_basic_resuls", "Základní výsledky", "memory", visibility);

            #endregion



            #region celkové výsledky





            VM._ZOBRAZIT_ZAKLADNI_VYSLEDKY_S_SKRTACKAMA = true;

//            VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'False'");

            for (int s = 0; s < VM.BIND_SQL_SOUTEZ_DELETES; s++)
            {


                string tmp_kolo_pro_skracku;
                for (int i = 0; i < VM.Players.Count(); i++)
                {

                    tmp_kolo_pro_skracku = VM.SQL_READSOUTEZDATA("select rnd,min(prep) from score where userid=" + VM.Players[i].ID + " and skrtacka='False' and nondeletable = 'False' and rnd < 100 ", "");
  //                  VM.SQL_SAVESOUTEZDATA("update score set skrtacka = 'True' where rnd='" + tmp_kolo_pro_skracku + "' and userid=" + VM.Players[i].ID);

                }

            }





            visibility = new string[] {
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True"
            };




            if (VM.BIND_SQL_SOUTEZ_ROUNDSFINALE == 0) {
                visibility[6] = "False";
                visibility[7] = "False";
            }


            if (1 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[8] = "True"; } else { visibility[8] = "False"; }
            if (2 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[9] = "True"; } else { visibility[9] = "False"; }
            if (3 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[10] = "True"; } else { visibility[10] = "False"; }
            if (4 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[11] = "True"; } else { visibility[11] = "False"; }
            if (5 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[12] = "True"; } else { visibility[12] = "False"; }






            if (1 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[17] = "True"; } else { visibility[17] = "False"; }
            if (2 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[18] = "True"; } else { visibility[18] = "False"; }
            if (3 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[19] = "True"; } else { visibility[19] = "False"; }
            if (4 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[20] = "True"; } else { visibility[20] = "False"; }
            if (5 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[21] = "True"; } else { visibility[21] = "False"; }
            if (6 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[22] = "True"; } else { visibility[22] = "False"; }
            if (7 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[23] = "True"; } else { visibility[23] = "False"; }
            if (8 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[24] = "True"; } else { visibility[24] = "False"; }
            if (9 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[25] = "True"; } else { visibility[25] = "False"; }
            if (10 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[26] = "True"; } else { visibility[26] = "False"; }


            if (11 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[27] = "True"; } else { visibility[27] = "False"; }
            if (12 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[28] = "True"; } else { visibility[28] = "False"; }
            if (13 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[29] = "True"; } else { visibility[29] = "False"; }
            if (14 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[30] = "True"; } else { visibility[30] = "False"; }
            if (15 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[31] = "True"; } else { visibility[31] = "False"; }
            if (16 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[32] = "True"; } else { visibility[32] = "False"; }
            if (17 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[33] = "True"; } else { visibility[33] = "False"; }
            if (18 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[34] = "True"; } else { visibility[34] = "False"; }
            if (19 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[35] = "True"; } else { visibility[35] = "False"; }
            if (20 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[36] = "True"; } else { visibility[36] = "False"; }




            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, 99);
            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", "Celkové výsledky - " + VM.agecatitems[0], "memory", visibility);

            System.Threading.Thread.Sleep(50);

            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, 0);
            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", "Celkové výsledky - " + VM.agecatitems[1], "memory", visibility);

            System.Threading.Thread.Sleep(50);

            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, 1);
            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", "Celkové výsledky - " + VM.agecatitems[2], "memory", visibility);

            System.Threading.Thread.Sleep(50);

            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, 2);
            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", "Celkové výsledky - " + VM.agecatitems[3], "memory", visibility);

            System.Threading.Thread.Sleep(50);



            #endregion

            #region prumerne pristani
            //////////////////////////////////////////////
            ///

            string[] headers = {
            "Pořadí",
            "Soutěžící",
            "Stát",
            "ID",
            "Záznamů",
            "ø Průměr",
            "---",
            "Σ Suma",
            "---",
            "---",
            "---",
            "Hodnoty"
            };

            visibility = new string[] {
                "Visible",
               "Hidden",
                "Visible",
                "Hidden",
                "Hidden",
                "Hidden",
                "Visible"
            };

            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_averagelandings", VM.BIND_SQL_SOUTEZ_ROUNDS, 99);
            //VM.print_statistics("statistics_c_landing", "statistics_landing", "memory", headers, visibility);
            VM.print_statistics("frame_empty", "data_empty", "print_complete_resuls", "statistics_landing", "Přistání", "memory", headers, visibility, VM.BIND_SQL_SOUTEZ_ROUNDS);

            ///////////////////////////////////////////////////////////
            #endregion


            #region letovy cas

            headers = new string[] {
            "Pořadí",
            "Soutěžící",
            "Stát",
            "ID",
            "Záznamů",
            "---",
            "Σ Celková doba",
            "---",
            "ø Průměr kola",
            "---",
            "---",
            "Hodnoty"
            };

            visibility = new string[]{
               "Hidden",
                "Visible",
                "Hidden",
                "Visible",
                "Hidden",
                "Hidden",
                "Visible"
            };


            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_flighttime", VM.BIND_SQL_SOUTEZ_ROUNDS, 99);
            //VM.print_statistics("statistics_c_flighttime", "statistics_flighttime", "memory", headers, visibility);
            VM.print_statistics("frame_empty", "data_empty", "print_complete_resuls", "statistics_flighttime", "Letový čas", "memory", headers, visibility, VM.BIND_SQL_SOUTEZ_ROUNDS);

            #endregion

            #region prumerna vyska


            headers = new string[] {
            "Pořadí",
            "Soutěžící",
            "Stát",
            "ID",
            "Záznamů",
            "ø Průměr",
            "---",
            "Σ Suma",
            "---",
            "---",
            "---",
            "Hodnoty"
            };

            visibility = new string[]{
               "Visible",
                "Hidden",
                "Visible",
                "Hidden",
                "Hidden",
                "Hidden",
                "Visible"
            };



            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_averageheights", VM.BIND_SQL_SOUTEZ_ROUNDS, 99);
            //VM.print_statistics("statistics_c_averageheights", "statistics_averageheights", "memory", headers, visibility);

            VM.print_statistics("frame_empty", "data_empty", "print_complete_resuls", "statistics_averageheights", "Průměrná výška", "memory", headers, visibility, VM.BIND_SQL_SOUTEZ_ROUNDS);
            #endregion



            #region max vyska




            headers = new string[] {
            "Pořadí",
            "Soutěžící",
            "Stát",
            "ID",
            "Záznamů",
            "↑ Max výška",
            "---",
            "Σ Celková výška",
            "---",
            "ø Průměr",
            "---",
            "Hodnoty"
            };

            visibility = new string[]{
               "Visible",
                "Hidden",
                "Visible",
                "Hidden",
                "Visible",
                "Hidden",
                "Visible"
            };



            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_maxheights", VM.BIND_SQL_SOUTEZ_ROUNDS, 99);
            //VM.print_statistics("statistics_c_averageheights", "statistics_averageheights", "memory", headers, visibility);

            VM.print_statistics("frame_empty", "data_empty", "print_statistics_maxheights", "statistics_maxheights", "Gagarin (max.výška)", "memory", headers, visibility, VM.BIND_SQL_SOUTEZ_ROUNDS);
            #endregion


            #region min vyska





            headers = new string[] {
            "Pořadí",
            "Soutěžící",
            "Stát",
            "ID",
            "Záznamů",
            "Minimální výška",
            "Σ Celková doba",
            "---",
            "Σ výšky / Σ bodů",
            "Bodů za metr",
            "---",
            "Hodnoty"
            };

            visibility = new string[]{
                "Visible",
                "Hidden",
                "Hidden",
                "Visible",
                "Visible",
                "Hidden",
                "Visible"
            };



            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_minheights", VM.BIND_SQL_SOUTEZ_ROUNDS, 99);
            //VM.print_statistics("statistics_c_averageheights", "statistics_averageheights", "memory", headers, visibility);

            VM.print_statistics("frame_empty", "data_empty", "print_statistics_minheights", "statistics_minheights", "Krtek (min.výška)", "memory", headers, visibility, VM.BIND_SQL_SOUTEZ_ROUNDS);
            #endregion


            #region čas vs výška



            headers = new string[] {
            "Pořadí",
            "Soutěžící",
            "Stát",
            "ID",
            "Záznamů",
            "Minimální výška",
            "ø čas v kole",
            "ø výška v kole",
            "Σ výšky / Σ bodů",
            "na 10 minut\nje třeba metrů",
            "---",
            "Ze 100 metrů"
            };

            visibility = new string[]{
                "Hidden",
                "Visible",
                "Visible",
                "Hidden",
                "Visible",
                "Hidden",
                "Visible"
            };



            VM.FUNCTION_RESULTS_LOAD_RESULTS("statistics_timevsheight", VM.BIND_SQL_SOUTEZ_ROUNDS, 99);
            //VM.print_statistics("statistics_c_averageheights", "statistics_averageheights", "memory", headers, visibility);

            VM.print_statistics("frame_empty", "data_empty", "print_statistics_timevsheights", "statistics_timevsheights", "Čas vs. výška", "memory", headers, visibility, VM.BIND_SQL_SOUTEZ_ROUNDS);
            #endregion


            if (output_type == "html")
            {
                VM.print_memory_to_file("frame_with_contest_info", "data_empty", "print_complete_overview_for_sending", "SORG AIR Megavýsledovka", output_type, false);
            }


            if (output_type == "mpdf")
            {
                VM.print_memory_to_file("frame_with_contest_info", "data_empty", "print_complete_overview_for_sending", "SORG AIR Megavýsledovka", "mpdf", false);
            }




            controller.SetProgress(0.9);
            await Task.Delay(300);

            var parsedDate = DateTime.Parse(VM.BIND_SQL_SOUTEZ_DATUM);

            string newfilename = (VM.BIND_SQL_SOUTEZ_KATEGORIE + "_" + RemoveDiacritics(VM.BIND_SQL_SOUTEZ_NAZEV) + "_" + parsedDate.ToString("yyyy_MM_dd")).ToLower();


            if (kamposlat == "sorgair.com") {
                UploadFileToFtp("ftp://187428.w28.wedos.net", "print_complete_overview_for_sending.html", "w187428_kalendarvysledky", "xGjprNNg", VM.BIND_SQL_SACALENDAR_NUMBER + "_" + VM.BIND_SQL_SOUTEZ_SMCRID + "_" + newfilename +".html");
            }
            if (kamposlat == "stoupak.cz")
            {
                UploadFileToFtp("ftp://48324.w24.wedos.net", "print_complete_overview_for_sending.html", "w48324_vysledkyupload", "a956nUS4", newfilename+ ".html");
            }

            if (kamposlat.Contains("@"))
            {
                email_send(kamposlat, "MEGAVýsledovka ze soutěže: " + VM.BIND_SQL_SOUTEZ_SMCRID + " - " + VM.BIND_SQL_SOUTEZ_NAZEV, "mv" + VM.BIND_SQL_SOUTEZ_SMCRID + ".pdf");

            }


            await controller.CloseAsync();
            await currentWindow.ShowMessageAsync("Odeslání soutěže", "Data soutěže byly odeslány, díky!", MessageDialogStyle.Affirmative, new MetroDialogSettings() { AnimateShow = true, AnimateHide = true });

            //var url = "mailto:emailnameu@domain.com&attachment=c:/asw/tisk.txt";
            //System.Diagnostics.Process.Start(url);


        }


        private async void vytvor_smcrvysledovku_pro_odeslani_a_odesli(string kamposlat, string output_type)
        {


            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Vytvářím oficiální výsledovku");
            controller.SetProgress(0);
            await Task.Delay(300);
            controller.SetProgress(0.5);



            string[] visibility = {
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "True",
                "kolo11",
                "kolo12",
                "kolo13",
                "kolo14",
                "kolo15",
                "kolo16",
                "k17",
                "k18",
                "k19",
                "k20"
            };





            if (VM.BIND_SQL_SOUTEZ_ROUNDSFINALE == 0)
            {
                visibility[6] = "False";
                visibility[7] = "False";
            }

            if (1 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[8] = "True"; } else { visibility[8] = "False"; }
            if (2 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[9] = "True"; } else { visibility[9] = "False"; }
            if (3 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[10] = "True"; } else { visibility[10] = "False"; }
            if (4 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[11] = "True"; } else { visibility[11] = "False"; }
            if (5 <= VM.BIND_SQL_SOUTEZ_ROUNDSFINALE) { visibility[12] = "True"; } else { visibility[12] = "False"; }





            if (1 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[17] = "True"; } else { visibility[17] = "False"; }
            if (2 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[18] = "True"; } else { visibility[18] = "False"; }
            if (3 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[19] = "True"; } else { visibility[19] = "False"; }
            if (4 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[20] = "True"; } else { visibility[20] = "False"; }
            if (5 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[21] = "True"; } else { visibility[21] = "False"; }
            if (6 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[22] = "True"; } else { visibility[22] = "False"; }
            if (7 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[23] = "True"; } else { visibility[23] = "False"; }
            if (8 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[24] = "True"; } else { visibility[24] = "False"; }
            if (9 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[25] = "True"; } else { visibility[25] = "False"; }
            if (10 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[26] = "True"; } else { visibility[26] = "False"; }

            if (11 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[27] = "True"; } else { visibility[27] = "False"; }
            if (12 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[28] = "True"; } else { visibility[28] = "False"; }
            if (13 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[29] = "True"; } else { visibility[29] = "False"; }
            if (14 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[30] = "True"; } else { visibility[30] = "False"; }
            if (15 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[31] = "True"; } else { visibility[31] = "False"; }
            if (16 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[32] = "True"; } else { visibility[32] = "False"; }
            if (17 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[33] = "True"; } else { visibility[33] = "False"; }
            if (18 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[34] = "True"; } else { visibility[34] = "False"; }
            if (19 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[35] = "True"; } else { visibility[35] = "False"; }
            if (20 <= VM.BIND_SQL_SOUTEZ_ROUNDS) { visibility[36] = "True"; } else { visibility[36] = "False"; }

            //VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, Convert.ToInt32(VM.BINDING_SELECTED_AGECAT_ID));
            //VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", "bind", "memory", visibility);
            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, 99);
            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", VM.agecatitems[0], "memory", visibility);

            System.Threading.Thread.Sleep(50);

            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, 0);
            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", VM.agecatitems[1], "memory", visibility);

            System.Threading.Thread.Sleep(50);

            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, 1);
            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", VM.agecatitems[2], "memory", visibility);

            System.Threading.Thread.Sleep(50);

            VM.FUNCTION_RESULTS_LOAD_RESULTS("users_complete", 99, 2);
            VM.print_completeresults("frame_empty", "data_empty", "print_complete_resuls", VM.agecatitems[3], "memory", visibility);

            System.Threading.Thread.Sleep(50);





            VM.print_memory_to_file("frame_with_contest_info", "data_empty", "print_complete_overview_for_sending", "SORG AIR oficiální výsledovka", output_type, false);


            controller.SetProgress(0.9);
            await Task.Delay(300);

            var parsedDate = DateTime.Parse(VM.BIND_SQL_SOUTEZ_DATUM);

            string newfilename = (VM.BIND_SQL_SOUTEZ_KATEGORIE + "_" + RemoveDiacritics(VM.BIND_SQL_SOUTEZ_NAZEV) + "_" + parsedDate.ToString("yyyy_MM_dd")).ToLower();



            if (kamposlat == "smcr")
            {
                email_send("kalendar@klem.cz", "Výsledovka pro SMČR ze soutěže: " + VM.BIND_SQL_SOUTEZ_SMCRID + " - " + VM.BIND_SQL_SOUTEZ_NAZEV, "v"+ VM.BIND_SQL_SOUTEZ_SMCRID + ".pdf");
            }

            await controller.CloseAsync();
            await currentWindow.ShowMessageAsync("Odeslání soutěže", "Data soutěže byly odeslány, díky!", MessageDialogStyle.Affirmative, new MetroDialogSettings() { AnimateShow = true, AnimateHide = true });

            //var url = "mailto:emailnameu@domain.com&attachment=c:/asw/tisk.txt";
            //System.Diagnostics.Process.Start(url);


        }

        static string RemoveDiacritics(string text)
        {




            char[] arr = text.Where(c => (char.IsLetterOrDigit(c) || c == '-' || c == '_')).ToArray();

            text = new string(arr);




            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {

                    stringBuilder.Append(c);
                }
            }

            string fixcontestname = stringBuilder.ToString().Normalize(NormalizationForm.FormC);
            fixcontestname = fixcontestname.Replace("Nazev souteze : ", string.Empty);
            fixcontestname = fixcontestname.Replace(" ", string.Empty);
            return fixcontestname;
        }




        public void email_send(string prijemce, string subject, string att)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("wes1-smtp.wedos.net");
            mail.From = new MailAddress("robot@sorgair.com");
            mail.To.Add(prijemce);
            mail.Subject = subject;
            mail.Body = "Níže je výsledovka se soutěže SMČRID:" + VM.BIND_SQL_SOUTEZ_SMCRID + " - " + VM.BIND_SQL_SOUTEZ_NAZEV;

            System.Net.Mail.Attachment attachment;
            
            attachment = new System.Net.Mail.Attachment ("pdf/"+att);
            attachment.Name = att;
            mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("robot@sorgair.com", "gD7jyZ2OB@()z24");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }


        public System.Windows.Media.Imaging.BitmapImage ConvertBitmap(System.Drawing.Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            System.Windows.Media.Imaging.BitmapImage image = new System.Windows.Media.Imaging.BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();

            return image;
        }

        private async void uploaddb_sorgair_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            MessageDialogResult result = await currentWindow.ShowMessageAsync("Odeslat?", "Odeslat výsledovku na sorgair.com (kalendář) ?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative) { 

                vytvor_megavysledovku_pro_odeslani_a_odesli("sorgair.com", "html");
            }

        }

        private async void sendtosmcr_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            MessageDialogResult result = await currentWindow.ShowMessageAsync("Odeslat?", "Odeslat výsledovku na SMČR?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                var result2 = await currentWindow.ShowInputAsync("SMŘ", "Zadej číslo soutěže na SMČR", new MetroDialogSettings() { DefaultText = VM.BIND_SQL_SOUTEZ_SMCRID });
                if (result2 == null)
                {
                    return;
                }
                else
                {
                    vytvor_smcrvysledovku_pro_odeslani_a_odesli("smcr","pdf");

                }

            }


        }

        private async void sendviaemail_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            MessageDialogResult result = await currentWindow.ShowMessageAsync("Odeslat?", "Odeslat výsledovku emailem?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                var result2 = await currentWindow.ShowInputAsync("Adresát", "Zadej email, kam výsledovka dojde", new MetroDialogSettings() { DefaultText = "info@sorgair.com" });
                if (result2 == null)
                {
                    return;
                }
                else
                {
                    vytvor_megavysledovku_pro_odeslani_a_odesli(result2, "mpdf");

                }

            }
        }
    }
}
