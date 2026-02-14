using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Model;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Media;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using PdfSharp.Pdf.IO;
using System.Data.Entity.Infrastructure;
using SORGAIR.Properties.Lang;

namespace WpfApp6.View
{
    /// <summary>
    /// Interaction logic for PlayersView.xaml
    /// </summary>
    /// 
    public static class ListExtensions
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }



    public partial class Draw : UserControl
    {

        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;

        string html_all;

        string matrix_switch_user1 = "";
        string matrix_switch_user2 = "";


        public Draw()
        {
            InitializeComponent();
        }


        private async void create_matrix_all_rounds(string typlosovani)
        {


            var currentWindow = this.TryFindParent<MetroWindow>();













            if (VM.BIND_POCETSOUTEZICICH > (VM.BIND_SQL_SOUTEZ_GROUPS * VM.BIND_SQL_SOUTEZ_STARTPOINTS))
            {
                MessageDialogResult result = await currentWindow.ShowMessageAsync("Nelze", "Je více soutěžících než startovišť!", MessageDialogStyle.Affirmative);
            }
            else
            {

                VM.POUZITY_TYP_LOSOVANI = typlosovani;
                var controller = await currentWindow.ShowProgressAsync("Please wait...", "Working...");

                    controller.SetTitle("Matrix creator");
                    VM.SQL_SAVESOUTEZDATA("delete from matrix");
                    VM.SQL_SAVESOUTEZDATA("delete from score");
                    VM.SQL_SAVESOUTEZDATA("delete from groups");
                    VM.SQL_SAVESOUTEZDATA("delete from rounds");
                    VM.SQL_SAVESOUTEZDATA("delete from refly");
                    VM.SQL_SAVESOUTEZDATA("update users set Matrixid=0");
                    VM.BIND_JETREBAROZLOSOVAT_SCORE = VM.BIND_SQL_SOUTEZ_ROUNDS * VM.BIND_SQL_SOUTEZ_GROUPS * VM.BIND_SQL_SOUTEZ_STARTPOINTS;
                    VM.FUNCTION_JETREBAROZLOSOVAT_OVER();
                    ////////////////////////// 


                    // Get the Parent MertoWindow here. We could also use a dialogcoordinator here if we want to.




                    for (int r = 1; r < VM.BIND_SQL_SOUTEZ_ROUNDS + 1; r++)
                    {
                        VM.SQL_SAVESOUTEZDATA("insert into rounds (id,name,type,lenght,zadano) values (" + r + ",'kolo:" + r + "','auto',600,0);");


                        for (int g = 1; g < VM.BIND_SQL_SOUTEZ_GROUPS + 1; g++)
                        {
                            VM.SQL_SAVESOUTEZDATA("insert into groups (id,name,type,lenght,zadano, masterround, groupnumber) values (null, 'Skupina:" + g + "','auto',600,0, " + r + " ," + g + ");");

                            for (int s = 1; s < VM.BIND_SQL_SOUTEZ_STARTPOINTS + 1; s++)
                            {

                                double x = (VM.BIND_SQL_SOUTEZ_ROUNDS + 1) * (VM.BIND_SQL_SOUTEZ_GROUPS + 1);
                                double progg = ((((r - 1) * (VM.BIND_SQL_SOUTEZ_GROUPS + 1)) + g) / x);

                                Console.WriteLine(progg);
                                VM.SQL_SAVESOUTEZDATA("insert into matrix (rnd,grp,stp,user) values (" + r + "," + g + "," + s + ",0)");
                                VM.SQL_SAVESOUTEZDATA("insert into score (rnd,grp,stp,userid,entered) values (" + r + "," + g + "," + s + ",0,'True')");
                                controller.SetProgress(progg);
                                controller.SetMessage(string.Format("Generating: {0}%", Math.Round((progg * 100), 2)));
                                await Task.Delay(1);
                            }


                        }

                    }
                    controller.SetProgress(0.90);
                    await Task.Delay(100);





               


                /////////////////////////////

                HashSet<string> uniqueStrings = new HashSet<string>();
                List<string> uniqueStringsid = new List<string>();


                int Round = 1;
                int Group = 0;
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var directory = System.IO.Path.GetDirectoryName(path);





                if (typlosovani != "file")
                {

                    for (var i = 0; i < VM.Players.Count; i++)
                    {
                        VM.SQL_SAVESOUTEZDATA("update users set Matrixid=" + VM.Players[i].ID + " where id=" + VM.Players[i].ID + ";");
                    }



                }
                else
                {
                    string[] lines = File.ReadAllLines(directory + "/Matrix/" + VM.Listofmatrixes[1].Filename + ".txt");


                    for (var i = 0; i < lines.Length; i++)
                    {
                        if (i > 2) //ořezání prvních 4 linek (poznamky, autor atd)



                        {
                            string radekzmatice = lines[i]; //nactu linku
                            string[] poleidnaradku = radekzmatice.Split(','); //rozsekam ji na casti
                            Console.WriteLine("RADEK" + radekzmatice);
                            for (var x = 1; x < poleidnaradku.Length + 1; x++)
                            {
                                if (!uniqueStrings.Contains(poleidnaradku[x - 1]))
                                {
                                    uniqueStrings.Add(poleidnaradku[x - 1]);
                                }
                            }
                        }

                    }

                    foreach (string tmpval in uniqueStrings)
                    {
                        VM.SQL_SAVESOUTEZDATA("update users set Matrixid=" + tmpval.Substring(1) + " where id in (select id from users where matrixid=0 and id > 0 order by random() limit 1 );");
                        System.Diagnostics.Debug.WriteLine(tmpval.Substring(1));
                    }


                }






                if (typlosovani != "file")
                {

                    if (typlosovani == "random")
                    {

                    

                        for (var r = 1; r < VM.BIND_SQL_SOUTEZ_ROUNDS + 1; r++)
                        {

                            for (var i = 0; i < VM.Players.Count; i++)
                            {
                                uniqueStringsid.Add(VM.Players[i].ID.ToString());
                            }

                      

                                //MessageBox.Show("zadávam:" + g);

                            for (var s = 1; s < VM.BIND_SQL_SOUTEZ_STARTPOINTS + 1; s++)
                            {
                                for (var g = 1; g < VM.BIND_SQL_SOUTEZ_GROUPS + 1; g++)
                                {
                                    int _id = 0;
                                    if (uniqueStringsid.Count > 0)
                                    {
                                        var random = new Random();
                                        int index = random.Next(uniqueStringsid.Count);
                                        _id = (int.Parse(uniqueStringsid[index]));
                                        uniqueStringsid.Remove(uniqueStringsid[index]);
                                    }
                                    //MessageBox.Show(uniqueStringsid.Count.ToString());

                                    //string  _id = VM.SQL_READSOUTEZDATA("select userid from score where userid=0 and rnd=" + r + " and grp=" + g + " and stp=" + s + " limit 1 ;", "");

                                    VM.SQL_SAVESOUTEZDATA("update matrix set user=(select id from users where Matrixid=" + _id + ") where rnd=" + r + " and grp=" + g + " and stp=" + s + " ;");
                                    if (_id != 0)
                                    {
                                        VM.SQL_SAVESOUTEZDATA("update score set userid=(select ifnull(id,0) from users where Matrixid=" + _id + "), entered='False' where rnd=" + r + " and grp=" + g + " and stp=" + s + " ;");
                                    }

                                    controller.SetMessage("Placing Round / Group / Startpoint: " + r + " / " + g + " / " + s);
                                    await Task.Delay(1);
                                }


                                //MessageBox.Show("zadáno:" + g);

                            }

                        }  
                    }



                    if (typlosovani == "vertical")
                    {

                        for (var i = 0; i < VM.Players.Count; i++)
                        {
                            uniqueStringsid.Add(VM.Players[i].ID.ToString());
                        }
                        uniqueStringsid.Shuffle();

                        int r = 1;
                      //  for (var r = 1; r < VM.BIND_SQL_SOUTEZ_ROUNDS + 1; r++)
                      //  {
                            List<string> uniqueStringsidcopy = new List<string>(uniqueStringsid);
                            //int polozka = -1;
                            Console.WriteLine("Nové kolo, plním znova uniqueStringsidcopy");
                            Console.WriteLine("uniqueStringsidcopy.Count:" + uniqueStringsidcopy.Count);
                            Console.WriteLine("uniqueStringsid.Count:" + uniqueStringsid.Count);

                            for (var s = 1; s < VM.BIND_SQL_SOUTEZ_STARTPOINTS + 1; s++)
                            {
                                for (var g = 1; g < VM.BIND_SQL_SOUTEZ_GROUPS + 1; g++)
                                {
                                    int _id = 0;
                                    if (uniqueStringsidcopy.Count > 0)
                                    {
                                        Console.WriteLine("uniqueStringsidcopy.Count:" + uniqueStringsidcopy.Count);
                                        //polozka = polozka + 1;
                                        //var random = new Random();
                                        //int index = random.Next(uniqueStringsid.Count);



                                        _id = (int.Parse(uniqueStringsidcopy[0]));
                                        uniqueStringsidcopy.Remove(uniqueStringsidcopy[0]);
                                    }

                                    VM.SQL_SAVESOUTEZDATA("update matrix set user=(select id from users where Matrixid=" + _id + ") where rnd=" + r + " and grp=" + g + " and stp=" + s + " ;");
                                    if (_id != 0)
                                    {
                                        VM.SQL_SAVESOUTEZDATA("update score set userid=(select ifnull(id,0) from users where Matrixid=" + _id + "), entered='False' where rnd=" + r + " and grp=" + g + " and stp=" + s + " ;");
                                    }

                                    controller.SetMessage("Placing Round / Group / Startpoint: " + r + " / " + g + " / " + s);
                                    await Task.Delay(1);
                                }


                           // }

                        }
                    }



                }
                else
                {
                    string[] lines = File.ReadAllLines(directory + "/Matrix/" + VM.Listofmatrixes[1].Filename + ".txt");



                    for (var i = 0; i < lines.Length; i++)


                        if (i > 2) //ořezání prvních 4 linek (poznamky, autor atd)

                        {


                            Group = Group + 1;

                            if (Group == VM.BIND_SQL_SOUTEZ_GROUPS+1)
                            {
                                Round = Round + 1;
                                Group = 1;
                            }

                            string radekzmatice = lines[i]; //nactu linku
                            string[] poleidnaradku = radekzmatice.Split(','); //rozsekam ji na casti
                            System.Diagnostics.Debug.WriteLine("radek" + i + radekzmatice);



                            for (var x = 1; x < poleidnaradku.Length + 1; x++)
                            {
                                //if (!uniqueStrings.Contains("R:" +  Round  + "G:" + Group + "L:" + i + "_" + poleidnaradku[x]))
                                //{
                                //uniqueStrings.Add("L:" + i + "_" + poleidnaradku[x]);
                                //}
                                System.Diagnostics.Debug.WriteLine("R:" + Round + "|G:" + Group + "|L:" + x + "|" + poleidnaradku[x - 1]);

                                VM.SQL_SAVESOUTEZDATA("update matrix set user=(select id from users where Matrixid=" + poleidnaradku[x - 1].Substring(1) + ") where rnd=" + Round + " and grp=" + Group + " and stp=" + x + " ;");
                                VM.SQL_SAVESOUTEZDATA("update score set userid=(select ifnull(id,0) from users where Matrixid=" + poleidnaradku[x - 1].Substring(1) + "), entered='False' where rnd=" + Round + " and grp=" + Group + " and stp=" + x + " ;");
                            }

                            controller.SetMessage("Placing Round / Group:" + Round + "/" + Group);
                            await Task.Delay(1);


                        }


                }


                VM.FUNCTION_LOAD_MATRIX_FILES();

                VM.FUNCTION_CHECK_ENTERED_ALL();

                VM.FUNCTION_ROUNDS_LOAD_ROUNDS();

                VM.BIND_SELECTED_GROUP = 1;
                VM.BIND_SELECTED_ROUND = 1;
                VM.BIND_VIEWED_ROUND = 1;
                VM.BIND_VIEWED_GROUP = 1;

                VM.FUNCTION_SELECTED_ROUND_FLYING_USERS(0, 0);
                VM.FUNCTION_LOAD_DEFAULT_ROUNDSANDGROUPS();
                VM.BIND_IS_FINAL_FLIGHT_READY = false;
                controller.SetProgress(1);
                controller.SetMessage("New matrix created");
                VM.UZ_JE_ROZLOSOVANO = true;
                await Task.Delay(1000);
                await controller.CloseAsync();
            }
        }
        private async void create_matrix_sekvencni_prvni(string typlosovani)
        {


            var currentWindow = this.TryFindParent<MetroWindow>();

            if (VM.BIND_POCETSOUTEZICICH > (VM.BIND_SQL_SOUTEZ_GROUPS * VM.BIND_SQL_SOUTEZ_STARTPOINTS))
            {
                MessageDialogResult result = await currentWindow.ShowMessageAsync("Nelze", "Je více soutěžících než startovišť!", MessageDialogStyle.Affirmative);
            }
            else
            {

                VM.POUZITY_TYP_LOSOVANI = typlosovani;
                var controller = await currentWindow.ShowProgressAsync("Please wait...", "Working...");

                controller.SetTitle("Matrix creator");
                VM.SQL_SAVESOUTEZDATA("delete from matrix");
                VM.SQL_SAVESOUTEZDATA("delete from score");
                VM.SQL_SAVESOUTEZDATA("delete from groups");
                VM.SQL_SAVESOUTEZDATA("delete from rounds");
                VM.SQL_SAVESOUTEZDATA("delete from refly");
                VM.SQL_SAVESOUTEZDATA("update users set Matrixid=0");
                VM.BIND_JETREBAROZLOSOVAT_SCORE = 9999;//VM.BIND_SQL_SOUTEZ_ROUNDS * VM.BIND_SQL_SOUTEZ_GROUPS * VM.BIND_SQL_SOUTEZ_STARTPOINTS;
                VM.FUNCTION_JETREBAROZLOSOVAT_OVER();
                ////////////////////////// 


                // Get the Parent MertoWindow here. We could also use a dialogcoordinator here if we want to.




                for (int r = 1; r < VM.BIND_SQL_SOUTEZ_ROUNDS + 1; r++)
                {
                    VM.SQL_SAVESOUTEZDATA("insert into rounds (id,name,type,lenght,zadano) values (" + r + ",'kolo:" + r + "','auto',600,0);");


                    for (int g = 1; g < VM.BIND_SQL_SOUTEZ_GROUPS + 1; g++)
                    {
                        VM.SQL_SAVESOUTEZDATA("insert into groups (id,name,type,lenght,zadano, masterround, groupnumber) values (null, 'Skupina:" + g + "','auto',600,0, " + r + " ," + g + ");");

                        for (int s = 1; s < VM.BIND_SQL_SOUTEZ_STARTPOINTS + 1; s++)
                        {

                            double x = (VM.BIND_SQL_SOUTEZ_ROUNDS + 1) * (VM.BIND_SQL_SOUTEZ_GROUPS + 1);
                            double progg = ((((r - 1) * (VM.BIND_SQL_SOUTEZ_GROUPS + 1)) + g) / x);

                            Console.WriteLine(progg);
                            VM.SQL_SAVESOUTEZDATA("insert into matrix (rnd,grp,stp,user) values (" + r + "," + g + "," + s + ",0)");
                            VM.SQL_SAVESOUTEZDATA("insert into score (rnd,grp,stp,userid,entered) values (" + r + "," + g + "," + s + ",0,'True')");
                            controller.SetProgress(progg);
                            controller.SetMessage(string.Format("Generating: {0}%", Math.Round((progg * 100), 2)));
                            await Task.Delay(1);
                        }


                    }

                }
                controller.SetProgress(0.90);
                await Task.Delay(100);








                /////////////////////////////

                HashSet<string> uniqueStrings = new HashSet<string>();
                List<string> uniqueStringsid = new List<string>();


                int Round = 1;
                int Group = 0;
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var directory = System.IO.Path.GetDirectoryName(path);






                    for (var i = 0; i < VM.Players.Count; i++)
                    {
                        VM.SQL_SAVESOUTEZDATA("update users set Matrixid=" + VM.Players[i].ID + " where id=" + VM.Players[i].ID + ";");
                    }



                   

                        for (var i = 0; i < VM.Players.Count; i++)
                        {
                            uniqueStringsid.Add(VM.Players[i].ID.ToString());
                        }
                        uniqueStringsid.Shuffle();

                        int rr = 1;
                        //  for (var r = 1; r < VM.BIND_SQL_SOUTEZ_ROUNDS + 1; r++)
                        //  {
                        List<string> uniqueStringsidcopy = new List<string>(uniqueStringsid);
                        //int polozka = -1;
                        Console.WriteLine("Nové kolo, plním znova uniqueStringsidcopy");
                        Console.WriteLine("uniqueStringsidcopy.Count:" + uniqueStringsidcopy.Count);
                        Console.WriteLine("uniqueStringsid.Count:" + uniqueStringsid.Count);

                        for (var s = 1; s < VM.BIND_SQL_SOUTEZ_STARTPOINTS + 1; s++)
                        {
                            for (var g = 1; g < VM.BIND_SQL_SOUTEZ_GROUPS + 1; g++)
                            {
                                int _id = 0;
                                if (uniqueStringsidcopy.Count > 0)
                                {
                                    Console.WriteLine("uniqueStringsidcopy.Count:" + uniqueStringsidcopy.Count);
                                    //polozka = polozka + 1;
                                    //var random = new Random();
                                    //int index = random.Next(uniqueStringsid.Count);



                                    _id = (int.Parse(uniqueStringsidcopy[0]));
                                    uniqueStringsidcopy.Remove(uniqueStringsidcopy[0]);
                                }

                                VM.SQL_SAVESOUTEZDATA("update matrix set user=(select id from users where Matrixid=" + _id + ") where rnd=" + rr + " and grp=" + g + " and stp=" + s + " ;");
                                if (_id != 0)
                                {
                                    VM.SQL_SAVESOUTEZDATA("update score set userid=(select ifnull(id,0) from users where Matrixid=" + _id + "), entered='False' where rnd=" + rr + " and grp=" + g + " and stp=" + s + " ;");
                                }

                                controller.SetMessage("Placing Round / Group / Startpoint: " + rr + " / " + g + " / " + s);
                                await Task.Delay(1);
                            }


                            // }

                       
                    }




                VM.FUNCTION_LOAD_MATRIX_FILES();

                VM.FUNCTION_CHECK_ENTERED_ALL();

                VM.FUNCTION_ROUNDS_LOAD_ROUNDS();
                VM.BIND_JETREBAROZLOSOVAT_SCORE = 9847;
                VM.FUNCTION_JETREBAROZLOSOVAT_OVER();
                VM.BIND_ROZLOSOVANIODPOVIDAPOCTUM = "Visible";
                // VM.BIND_SELECTED_GROUP = 1;
                // VM.BIND_SELECTED_ROUND = 1;
                // VM.BIND_VIEWED_ROUND = 1;
                // VM.BIND_VIEWED_GROUP = 1;

                // VM.FUNCTION_SELECTED_ROUND_FLYING_USERS(0, 0);
                // VM.FUNCTION_LOAD_DEFAULT_ROUNDSANDGROUPS();
                VM.BIND_IS_FINAL_FLIGHT_READY = false;
                controller.SetProgress(1);
                controller.SetMessage("New matrix created");
               // VM.UZ_JE_ROZLOSOVANO = true;
                await Task.Delay(1000);
                await controller.CloseAsync();
            }
        }


        private async void create_matrix_sekvencni_zbytek()
        {


            var currentWindow = this.TryFindParent<MetroWindow>();

            int posunute_s = 1;
            int posunute_g = 1;



            if (VM.POUZITY_TYP_LOSOVANI == "horizontal")
                {
                        for (var r = 2; r < VM.BIND_SQL_SOUTEZ_ROUNDS + 1; r++)
                        {

                            for (var g = 1; g < VM.BIND_SQL_SOUTEZ_GROUPS + 1; g++)
                            {

                                for (var s = 1; s < VM.BIND_SQL_SOUTEZ_STARTPOINTS + 1; s++)
                                {

                                    posunute_s = s + g;
                                    dalsipokuss:
                                    if (posunute_s > VM.BIND_SQL_SOUTEZ_STARTPOINTS)
                                    {
                                        posunute_s = posunute_s - VM.BIND_SQL_SOUTEZ_STARTPOINTS;
                                    goto dalsipokuss;
                                    }

                                    int _id = 0;
                                    _id =  int.Parse(VM.SQL_READSOUTEZDATA("select user from matrix where rnd=" + (r-1) + " and grp=" + g + " and stp=" + s, ""));

                                    VM.SQL_SAVESOUTEZDATA("update matrix set user="+ _id + " where rnd=" + (r) + " and grp=" + g + " and stp=" + posunute_s + " ;");
                                    if (_id != 0)
                                    {
                                        VM.SQL_SAVESOUTEZDATA("update score set userid=" + _id + ", entered='False' where rnd=" + (r) + " and grp=" + g + " and stp=" + posunute_s + " ;");
                                    }

                                    //controller.SetMessage("Placing Round / Group / Startpoint: " + r + " / " + g + " / " + s);
                                    await Task.Delay(1);
                                }


                                //MessageBox.Show("zadáno:" + g);

                            }

                        }
                    }

            if (VM.POUZITY_TYP_LOSOVANI == "vertical")
            {
                for (var r = 2; r < VM.BIND_SQL_SOUTEZ_ROUNDS + 1; r++)
                {

                    for (var g = 1; g < VM.BIND_SQL_SOUTEZ_GROUPS + 1; g++)
                    {

                        for (var s = 1; s < VM.BIND_SQL_SOUTEZ_STARTPOINTS + 1; s++)
                        {

                            posunute_g = g + s;
                        dalsipokusg:
                            if (posunute_g > VM.BIND_SQL_SOUTEZ_GROUPS)
                            {
                                posunute_g = posunute_g - VM.BIND_SQL_SOUTEZ_GROUPS;
                                goto dalsipokusg;
                            }

                            int _id = 0;
                            _id = int.Parse(VM.SQL_READSOUTEZDATA("select user from matrix where rnd=" + (r - 1) + " and grp=" + g + " and stp=" + s, ""));

                            VM.SQL_SAVESOUTEZDATA("update matrix set user=" + _id + " where rnd=" + (r) + " and grp=" + posunute_g + " and stp=" + s + " ;");
                            if (_id != 0)
                            {
                                VM.SQL_SAVESOUTEZDATA("update score set userid=" + _id + ", entered='False' where rnd=" + (r) + " and grp=" + posunute_g + " and stp=" + s + " ;");
                            }

                            //controller.SetMessage("Placing Round / Group / Startpoint: " + r + " / " + g + " / " + s);
                            await Task.Delay(1);
                        }


                        //MessageBox.Show("zadáno:" + g);

                    }

                }
            }

            if (VM.POUZITY_TYP_LOSOVANI == "diagonal")
            {
                for (var r = 2; r < VM.BIND_SQL_SOUTEZ_ROUNDS + 1; r++)
                {

                    for (var g = 1; g < VM.BIND_SQL_SOUTEZ_GROUPS + 1; g++)
                    {


                        posunute_g = g + r;
                    dalsipokusg:
                        if (posunute_g > VM.BIND_SQL_SOUTEZ_GROUPS)
                        {
                            posunute_g = posunute_g - VM.BIND_SQL_SOUTEZ_GROUPS;
                            goto dalsipokusg;
                        }


                        for (var s = 1; s < VM.BIND_SQL_SOUTEZ_STARTPOINTS + 1; s++)
                        {


                            posunute_s = s + g;
                        dalsipokuss:
                            if (posunute_s > VM.BIND_SQL_SOUTEZ_STARTPOINTS)
                            {
                                posunute_s = posunute_s - VM.BIND_SQL_SOUTEZ_STARTPOINTS;


                                goto dalsipokuss;
                            }



                            




                            int _id = 0;
                            _id = int.Parse(VM.SQL_READSOUTEZDATA("select user from matrix where rnd=" + (r - 1) + " and grp=" + g + " and stp=" + s, ""));
                            //MessageBox.Show("update matrix set user=" + _id + " where rnd=" + (r) + " and grp=" + posunute_g + " and stp=" + posunute_s + " ;");
                            Console.Write("select user from matrix where rnd=" + (r - 1) + " and grp=" + g + " and stp=" + s);
                            Console.Write("update matrix set user=" + _id + " where rnd=" + (r) + " and grp=" + posunute_g + " and stp=" + posunute_s + " ;");
                            Console.Write("***********");

                            VM.SQL_SAVESOUTEZDATA("update matrix set user=" + _id + " where rnd=" + (r) + " and grp=" + posunute_g + " and stp=" + posunute_s + " ;");
                            if (_id != 0)
                            {
                                VM.SQL_SAVESOUTEZDATA("update score set userid=" + _id + ", entered='False' where rnd=" + (r) + " and grp=" + posunute_g + " and stp=" + posunute_s + " ;");
                            }

                            //controller.SetMessage("Placing Round / Group / Startpoint: " + r + " / " + g + " / " + s);
                            await Task.Delay(1);
                        }


                        //MessageBox.Show("zadáno:" + g);

                    }

                }
            }

            MessageBox.Show("hotovo");


                VM.FUNCTION_LOAD_MATRIX_FILES();

                VM.FUNCTION_CHECK_ENTERED_ALL();

                VM.FUNCTION_ROUNDS_LOAD_ROUNDS();

                VM.BIND_SELECTED_GROUP = 1;
                VM.BIND_SELECTED_ROUND = 1;
                VM.BIND_VIEWED_ROUND = 1;
                VM.BIND_VIEWED_GROUP = 1;

                VM.FUNCTION_SELECTED_ROUND_FLYING_USERS(0, 0);
                VM.FUNCTION_LOAD_DEFAULT_ROUNDSANDGROUPS();
                VM.BIND_IS_FINAL_FLIGHT_READY = false;
                //controller.SetProgress(1);
                //controller.SetMessage("New matrix created");
                VM.UZ_JE_ROZLOSOVANO = true;
                await Task.Delay(1000);
                //await controller.CloseAsync();
            
        }

        private async void matrix_user_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button selectedbutton = (sender as System.Windows.Controls.Button);
            var currentWindow = this.TryFindParent<MetroWindow>();

            if (matrix_switch_user1 == "")
            {
                matrix_switch_user1 = selectedbutton.Tag.ToString();

                string[] udajetmp = matrix_switch_user1.Split('_');
                string enteredtmp = VM.SQL_READSOUTEZDATA("select entered from score  where rnd=" + udajetmp[0] + " and grp=" + udajetmp[1] + " and stp=" + udajetmp[2] + ";", "");

                if ((udajetmp[3] != "0") & (enteredtmp == "True"))
                {
                    Console.WriteLine("Už má výsledek");
                    await currentWindow.ShowMessageAsync("Nelze přesouvat", "Soutěžící již má zapsán výsledek! Nelze jej již přesunout");
                    matrix_switch_user1 = "";
                    return;

                }
                selectedbutton.Background = new SolidColorBrush(Colors.Red);




            }
            else
            {

                if (matrix_switch_user2 == "")
                {
                    matrix_switch_user2 = selectedbutton.Tag.ToString();



                    string[] udajetmp = matrix_switch_user2.Split('_');
                    string enteredtmp = VM.SQL_READSOUTEZDATA("select entered from score  where rnd=" + udajetmp[0] + " and grp=" + udajetmp[1] + " and stp=" + udajetmp[2] + ";", "");

                    if ((udajetmp[3] != "0") & (enteredtmp == "True"))
                    {
                        Console.WriteLine("Už má výsledek");
                        await currentWindow.ShowMessageAsync("Nelze přesouvat", "Soutěžící již má zapsán výsledek! Nelze jej již přesunout");
                        matrix_switch_user2 = "";
                        return;

                    }

                    selectedbutton.Background = new SolidColorBrush(Colors.Red);



                    string[] udaje = matrix_switch_user1.Split('_');
                    foreach (var udaj in udaje)
                    {
                        System.Console.WriteLine($"<{udaj}>");
                    }


                    string[] udaje2 = matrix_switch_user2.Split('_');
                    foreach (var udaj in udaje2)
                    {
                        System.Console.WriteLine($"<{udaj}>");
                    }

                    if (udaje[0] == udaje2[0])
                    {

                        var controller = await currentWindow.ShowProgressAsync("Přesouvám", "Přesouvám zvolené soutěžící");
                        await Task.Delay(300);
                        controller.SetProgress(0);


                        //VM.SQL_SAVESOUTEZDATA("update score set grp=" + udaje[1] + ",stp=" + udaje[2] + " where userid=" + udaje2[3] + " and rnd=" + udaje[0]);
                        //VM.SQL_SAVESOUTEZDATA("update score set grp=" + udaje2[1] + ",stp=" + udaje2[2] + " where userid=" + udaje[3] + " and rnd=" + udaje2[0]);

                        //VM.SQL_SAVESOUTEZDATA("update matrix set grp=" + udaje[1] + ",stp=" + udaje[2] + " where user=" + udaje2[3] + " and rnd=" + udaje[0] );
                        //VM.SQL_SAVESOUTEZDATA("update matrix set grp=" + udaje2[1] + ",stp=" + udaje2[2] + " where user=" + udaje[3] + " and rnd=" + udaje2[0]);

                        string entered1 = VM.SQL_READSOUTEZDATA("select entered from score  where rnd=" + udaje[0] + " and grp=" + udaje[1] + " and stp=" + udaje[2] + ";", "");
                        string entered2 = VM.SQL_READSOUTEZDATA("select entered from score where rnd=" + udaje2[0] + " and grp=" + udaje2[1] + " and stp=" + udaje2[2] + ";", "");

                        controller.SetProgress(0.1);

                        VM.SQL_SAVESOUTEZDATA("update matrix set user=" + udaje2[3] + " where rnd=" + udaje[0] + " and grp=" + udaje[1] + " and stp=" + udaje[2]);
                        VM.SQL_SAVESOUTEZDATA("update matrix set user=" + udaje[3] + " where rnd=" + udaje2[0] + " and grp=" + udaje2[1] + " and stp=" + udaje2[2]);
                        VM.SQL_SAVESOUTEZDATA("update score set userid=" + udaje2[3] + ", entered= '" + entered2 + "' where rnd=" + udaje[0] + " and grp=" + udaje[1] + " and stp=" + udaje[2]);
                        VM.SQL_SAVESOUTEZDATA("update score set userid=" + udaje[3] + ", entered = '" + entered1 + "' where rnd=" + udaje2[0] + " and grp=" + udaje2[1] + " and stp=" + udaje2[2]);


                        controller.SetProgress(0.6);




                        VM.FUNCTION_ROUNDS_LOAD_ROUNDS();
                        matrix_switch_user2 = "";
                        matrix_switch_user1 = "";

                        controller.SetProgress(0.7);


                        if ((VM.BIND_SELECTED_ROUND == int.Parse(udaje[0].ToString()) & VM.BIND_SELECTED_GROUP == int.Parse(udaje[1].ToString())) | (VM.BIND_SELECTED_ROUND == int.Parse(udaje2[0].ToString()) & VM.BIND_SELECTED_GROUP == int.Parse(udaje2[1].ToString())))
                        {
                            Console.WriteLine("je treba překreslit LETENOU skupinu");
                            VM.FUNCTION_SELECTED_ROUND_FLYING_USERS(0, 0);
                        }
                        else
                        {
                            Console.WriteLine("nepřekreslovat LETENOU");
                        }



                        if ((VM.BIND_VIEWED_ROUND == int.Parse(udaje[0].ToString()) & VM.BIND_VIEWED_GROUP == int.Parse(udaje[1].ToString())) | (VM.BIND_VIEWED_ROUND == int.Parse(udaje2[0].ToString()) & VM.BIND_VIEWED_GROUP == int.Parse(udaje2[1].ToString())))
                        {
                            Console.WriteLine("je treba překreslit zobrazenou skupinu");


                            VM.BIND_VIEWED_GROUP = VM.BIND_VIEWED_GROUP;

                            for (int i = 0; i < VM.MODEL_CONTEST_GROUPS.Count; i++)
                            {
                                VM.MODEL_CONTEST_GROUPS[i].ISSELECTED = "---";
                            }
                            VM.MODEL_CONTEST_GROUPS[VM.BIND_VIEWED_GROUP - 1].ISSELECTED = "selected";


                        }
                        else
                        {
                            Console.WriteLine("nepřekreslovat");
                        }

                        controller.SetProgress(1);
                        controller.CloseAsync();


                    }
                    else
                    {
                        await currentWindow.ShowMessageAsync("Nelze přesouvat", "Nelze přesouvat soutěžící mezi koly!!");
                        VM.FUNCTION_ROUNDS_LOAD_ROUNDS();
                        matrix_switch_user2 = "";
                        matrix_switch_user1 = "";

                    }

                }
            }



            Console.WriteLine("matrix_switch {0}  / {1} ", matrix_switch_user1, matrix_switch_user2);


        }

        private void print_matrix_btn_Click(object sender, RoutedEventArgs e)
        {
            if (draw_print_with_header.IsOn == true)
            {
                VM.print_matrix("frame_small_info", "data_matrix", "print_matrix", "Rozlosování", "html");

            }
            else
            {

                VM.print_matrix("frame_no_info", "data_matrix", "print_matrix", "Rozlosování", "html");

            }
        }



        private async void print_matrix(string template_name, string output_type)
        {



            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Generuji", "Generuji karty soutěžících k tisku");
            await Task.Delay(300);
            controller.SetProgress(0);


            string html_main;
            string html_body;
            string html_body_withrightdata;
            Console.WriteLine("VM.Players.Count" + VM.Players.Count);

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            html_main = File.ReadAllText(directory + "/Print_templates/" + template_name + "_frame.html", Encoding.UTF8);
            html_main = html_main.Replace("@CONTESTNAME", VM.BIND_SQL_SOUTEZ_NAZEV + " - " + VM.BIND_SQL_SOUTEZ_KATEGORIE);

            html_body = File.ReadAllText(directory + "/Print_templates/" + template_name + "_data.html", Encoding.UTF8);
            string html_body_complete = "";
         

                //controller.SetProgress(double.Parse(decimal.Divide(i, VM.Players.Count()).ToString()));
                //Console.WriteLine(decimal.Divide(i, VM.Players.Count()));
                await Task.Delay(100);
                string tabulkaletu = "";

                for (int x = 1; x < VM.BIND_SQL_SOUTEZ_ROUNDS + 1; x++)
                {
                tabulkaletu = tabulkaletu + $@"{Lang.txt_round} : {x}
<table>
    <tbody>
        <tr>
            <th></th>";

                for (int s = 1; s < VM.BIND_SQL_SOUTEZ_STARTPOINTS+1; s++)
                {
                    tabulkaletu = tabulkaletu + "<th>"+Lang.txt_startpoint+":"+s+" </th>";

                }

                tabulkaletu = tabulkaletu + "</tr>";
                

                for (int g = 1; g < VM.BIND_SQL_SOUTEZ_GROUPS + 1; g++)
                {
                    tabulkaletu = tabulkaletu + "<tr><td class='gray'>"+Lang.txt_group+": "+ g +"</td>";
                    for (int stp = 1; stp < VM.BIND_SQL_SOUTEZ_STARTPOINTS + 1; stp++)
                {
                        string kdo = VM.SQL_READSOUTEZDATA("select Lastname || ' ' || Firstname from matrix M left join users U on M.user = U.ID where rnd=" + x.ToString() + " and grp=" + g.ToString() +" and stp="+ stp.ToString() + " ; ", "");
                        tabulkaletu = tabulkaletu + "<td>"+ kdo +"</td>";

                    }
                }


                tabulkaletu = tabulkaletu + "</tr></tbody></table>";
                }
                


             


                html_body_withrightdata = html_body;


                html_body_withrightdata = html_body_withrightdata.Replace("@MATRIX", tabulkaletu);


                html_body_complete = html_body_complete + html_body_withrightdata;


         


            html_all = html_main.Replace("@BODY", html_body_complete);

            if (output_type == "pdf")
            {

                SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
                SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html_all);
                doc.Save(directory + "/Print/" + template_name + ".pdf");
                doc.Close();

                System.Diagnostics.Process.Start(directory + "/Print/" + template_name + ".pdf");
            }


            if (output_type == "html")
            {

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory + "/Print/" + template_name + ".html"))
                {
                    file.WriteLine(html_all);
                }
                System.Diagnostics.Process.Start(directory + "/Print/" + template_name + ".html");
            }
            await controller.CloseAsync();
            await Task.Delay(300);



        }

        private void print_matrixpdf_btn_Click(object sender, RoutedEventArgs e)
        {
            print_matrix("matrix", "pdf");

        }

       

        private async void btn_create_draw_all_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            MessageDialogResult result = await currentWindow.ShowMessageAsync("Rozlosovat?", "Opravdu losovat ? Dojde k vymazání aktuálního rozlosování a případných výsledků", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative){
                object tag = ((FrameworkElement)sender).Tag;
                create_matrix_all_rounds(tag.ToString());
            }
        }



        private async void btn_create_draw_first_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            MessageDialogResult result = await currentWindow.ShowMessageAsync("Rozlosovat?", "Opravdu losovat ? Dojde k vymazání aktuálního rozlosování a případných výsledků", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                object tag = ((FrameworkElement)sender).Tag;
                create_matrix_sekvencni_prvni(tag.ToString());
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            FNC_CHECK_ERROR_MATRIX();

        }



        private async void FNC_CHECK_ERROR_MATRIX()
        {


            var currentWindow = this.TryFindParent<MetroWindow>();

            int kolikrattamje = 0;
            bool bylachyba = false;

            for (int x = 1; x < VM.BIND_SQL_SOUTEZ_ROUNDS + 1; x++)

            {

                for (var i = 0; i < VM.Players.Count; i++)
                {


                    kolikrattamje = int.Parse(VM.SQL_READSOUTEZDATA("select count (rnd),user from matrix where user=" + VM.Players[i].ID + " and rnd=" + x + " and grp<=" + VM.BIND_SQL_SOUTEZ_GROUPS, ""));
                    if (kolikrattamje != 1)
                    {
                        bylachyba = true;
                        var controllerx = await currentWindow.ShowMessageAsync("Kontrola", "ERRR: " + VM.Players[i].LASTNAME + " je v kole " + x + " : " + kolikrattamje + "krát");
                    }



                }



            }

            if (bylachyba is true)
            {
                var controller = await currentWindow.ShowMessageAsync("Kontrola", "Kontrola dokončena. Chyby v rozlosování byly vypsány");
            }
            else
            {
                var controller = await currentWindow.ShowMessageAsync("Kontrola", "Vše v pořádku a bez chyb :)");
            }
        }

        private void btn_draw_dotisk_Click(object sender, RoutedEventArgs e)
        {
            //create_matrix_sekvencni_zbytek();
            //VM.SQL_READSOUTEZDATA("SELECT m1.user as User1, m2.user as User2, COUNT(*) as CommonFlights FROM Matrix m1 JOIN Matrix m2 ON m1.rnd = m2.rnd AND m1.grp = m2.grp AND m1.user != m2.user GROUP BY m1.user, m2.user ORDER BY CommonFlights DESC; ", "");
            //CreateInteractionMatrix();
            string sql = "SELECT \r\n    CASE WHEN m1.user < m2.user THEN m1.user ELSE m2.user END AS User1,\r\n    CASE WHEN m1.user < m2.user THEN m2.user ELSE m1.user END AS User2,\r\n    COUNT(*) AS CommonFlights\r\nFROM \r\n    Matrix m1\r\nJOIN \r\n    Matrix m2 \r\nON \r\n    m1.rnd = m2.rnd \r\n    AND m1.grp = m2.grp \r\n    AND m1.user != m2.user\r\nGROUP BY \r\n    User1, User2\r\nORDER BY \r\n    CommonFlights DESC;";

            List<string> columnNames = new List<string> { "User1", "User2", "CommonFlights" };

            List<string> results = VM.SQL_READSOUTEZDATA_RETURNARR(sql, columnNames);

            // Uložení výsledků do souboru
            string filePath = "matrix.txt";
            SaveResultsToFile(results, filePath);
        }

        static void SaveResultsToFile(List<string> results, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (string result in results)
                {
                    writer.WriteLine(result);
                }
            }
        }


        private void CreateInteractionMatrix()
        {
            string sql = "SELECT m1.user as User1, m2.user as User2, COUNT(*) as CommonFlights FROM Matrix m1 JOIN Matrix m2 ON m1.rnd = m2.rnd AND m1.grp = m2.grp AND m1.user != m2.user GROUP BY m1.user, m2.user ORDER BY CommonFlights DESC;";
            DataTable result = ConvertStringToDataTable(VM.SQL_READSOUTEZDATA(sql,""));

            // Vytvoření seznamu všech unikátních uživatelů
            HashSet<int> users = new HashSet<int>();
            foreach (DataRow row in result.Rows)
            {
                users.Add((int)row["User1"]);
                users.Add((int)row["User2"]);
            }

            // Vytvoření matice uživatelů
            DataTable matrix = new DataTable();
            foreach (int user in users)
            {
                matrix.Columns.Add(user.ToString(), typeof(int));
            }

            // Inicializace matice s nulami
            foreach (int user in users)
            {
                DataRow newRow = matrix.NewRow();
                foreach (DataColumn col in matrix.Columns)
                {
                    newRow[col] = 0;
                }
                matrix.Rows.Add(newRow);
            }

            // Vyplnění matice hodnotami
            foreach (DataRow row in result.Rows)
            {
                int user1 = (int)row["User1"];
                int user2 = (int)row["User2"];
                int commonFlights = (int)row["CommonFlights"];

                matrix.Rows[user1][user2.ToString()] = commonFlights;
                matrix.Rows[user2][user1.ToString()] = commonFlights;
            }

            // Uložení matice do textového souboru
            SaveMatrixToFile(matrix, "matrix.txt");
        }

        private void SaveMatrixToFile(DataTable matrix, string filePath)
        {
            StringBuilder sb = new StringBuilder();

            // Přidání názvů sloupců
            IEnumerable<string> columnNames = matrix.Columns.OfType<DataColumn>().Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            // Přidání dat řádků
            foreach (DataRow row in matrix.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            File.WriteAllText(filePath, sb.ToString());
        }




        private DataTable ConvertStringToDataTable(string data)
        {
            DataTable dt = new DataTable();
            // Předpokládáme, že první řádek obsahuje hlavičky sloupců
            string[] lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string[] headers = lines[0].Split(',');

            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }

            for (int i = 1; i < lines.Length; i++)
            {
                string[] cells = lines[i].Split(',');
                dt.Rows.Add(cells);
            }

            return dt;
        }




    }
}
