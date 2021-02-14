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


namespace WpfApp6.View
{
    /// <summary>
    /// Interaction logic for PlayersView.xaml
    /// </summary>
    public partial class Draw : UserControl
    {

        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;

        string matrix_switch_user1 = "";
        string matrix_switch_user2 = "";


        public Draw()
        {
            InitializeComponent();
        }


        private async void btn_importusersbymatrixfile_Click(object sender, RoutedEventArgs e)
        {

            var currentWindow = this.TryFindParent<MetroWindow>();
            var controller = await currentWindow.ShowProgressAsync("Please wait...", "Deleting old matrix");
            controller.SetTitle("Matrix creator");
            VM.FUNCTION_LOAD_MATRIX_FILES();
            VM.SQL_SAVESOUTEZDATA("delete from matrix");
            VM.SQL_SAVESOUTEZDATA("delete from score");
            VM.SQL_SAVESOUTEZDATA("delete from groups");
            VM.SQL_SAVESOUTEZDATA("delete from rounds");
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
                        VM.SQL_SAVESOUTEZDATA("insert into score (rnd,grp,stp,userid) values (" + r + "," + g + "," + s + ",0)");
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
            int Startpoint = 0;
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);

            if (filewithmatrix.SelectedIndex == 0)
            {

                for (var i = 0; i < VM.Players.Count; i++)
                {
                    VM.SQL_SAVESOUTEZDATA("update users set Matrixid=" + VM.Players[i].ID  + " where id="+VM.Players[i].ID + ";");
                }



            }
            else
            {
                string[] lines = File.ReadAllLines(directory + "/Matrix/" + VM.Listofmatrixes[filewithmatrix.SelectedIndex].Filename + ".txt");


                for (var i = 0; i < lines.Length; i++)
                {
                    if (i > 2) //ořezání prvních 4 linek (poznamky, autor atd)



                    {
                        string radekzmatice = lines[i]; //nactu linku
                        string[] poleidnaradku = radekzmatice.Split(','); //rozsekam ji na casti
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






            if (filewithmatrix.SelectedIndex == 0)
            {


                for (var r = 1; r < VM.BIND_SQL_SOUTEZ_ROUNDS+1; r++)
                {

                    for (var i = 0; i < VM.Players.Count; i++)
                    {
                        uniqueStringsid.Add(VM.Players[i].ID.ToString());
                    }

                    for (var g = 1; g < VM.BIND_SQL_SOUTEZ_GROUPS+1; g++)
                    {

                        //MessageBox.Show("zadávam:" + g);

                        for (var s = 1; s < VM.BIND_SQL_SOUTEZ_STARTPOINTS+1; s++)
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
                            VM.SQL_SAVESOUTEZDATA("update score set userid=(select ifnull(id,0) from users where Matrixid=" + _id  + ") where rnd=" + r + " and grp=" + g + " and stp=" + s + " ;");

                        }

                        controller.SetMessage("Placing Round / Group:" + r + "/" + g);
                        await Task.Delay(1);

                        //MessageBox.Show("zadáno:" + g);

                    }

                }
            }
            else
            {
                string[] lines = File.ReadAllLines(directory + "/Matrix/" + VM.Listofmatrixes[filewithmatrix.SelectedIndex].Filename + ".txt");



                for (var i = 0; i < lines.Length; i++)


                    if (i > 2) //ořezání prvních 4 linek (poznamky, autor atd)

                    {
               
                    
                        Group = Group + 1;

                        if (Group == 4)
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
                            VM.SQL_SAVESOUTEZDATA("update score set userid=(select ifnull(id,0) from users where Matrixid=" + poleidnaradku[x - 1].Substring(1) + ") where rnd=" + Round + " and grp=" + Group + " and stp=" + x + " ;");
                        }

                        controller.SetMessage("Placing Round / Group:" + Round + "/" + Group);
                        await Task.Delay(1);

                   
                    }


            }

            


         VM.FUNCTION_ROUNDS_LOAD_ROUNDS();
            VM.BIND_SELECTED_GROUP = 1;
            VM.BIND_SELECTED_ROUND = 1;
            VM.BIND_VIEWED_ROUND = 1;
            VM.BIND_VIEWED_GROUP = 1;

            VM.FUNCTION_SELECTED_ROUND_FLYING_USERS(0, 0);
            VM.FUNCTION_LOAD_DEFAULT_ROUNDSANDGROUPS();
            controller.SetProgress(1);
            controller.SetMessage("New matrix created");
            await Task.Delay(1000);
            await controller.CloseAsync();


        }

        private void matrix_user_Click(object sender, RoutedEventArgs e)
        {
            Button selectedbutton = (sender as Button);

            if (matrix_switch_user1 == "")
            {
                matrix_switch_user1 = selectedbutton.Tag.ToString();
                selectedbutton.Background = new SolidColorBrush(Colors.Red);
            }
            else{

                if (matrix_switch_user2 == "")
                {
                    matrix_switch_user2 = selectedbutton.Tag.ToString();
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


                    VM.SQL_SAVESOUTEZDATA("update matrix set user=" + udaje2[3] + " where rnd=" + udaje[0] + " and grp=" + udaje[1] + " and stp=" + udaje[2]);
                    VM.SQL_SAVESOUTEZDATA("update matrix set user=" + udaje[3] + " where rnd=" + udaje2[0] + " and grp=" + udaje2[1] + " and stp=" + udaje2[2]);
                    VM.SQL_SAVESOUTEZDATA("update score set userid=" + udaje2[3] + " where rnd=" + udaje[0] + " and grp=" + udaje[1] + " and stp=" + udaje[2]);
                    VM.SQL_SAVESOUTEZDATA("update score set userid=" + udaje[3] + " where rnd=" + udaje2[0] + " and grp=" + udaje2[1] + " and stp=" + udaje2[2]);
                    VM.FUNCTION_ROUNDS_LOAD_ROUNDS();
                    matrix_switch_user2 = "";
                    matrix_switch_user1 = "";


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
                }
            }


            Console.WriteLine("matrix_switch {0}  / {1} ", matrix_switch_user1, matrix_switch_user2);


        }
    }
}
