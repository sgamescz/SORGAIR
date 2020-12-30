using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Model;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System;
using System.IO;
using System.Collections.Generic;


namespace WpfApp6.View
{
    /// <summary>
    /// Interaction logic for PlayersView.xaml
    /// </summary>
    public partial class Draw : UserControl
    {

        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;


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

            ////////////////////////// 


            // Get the Parent MertoWindow here. We could also use a dialogcoordinator here if we want to.




            for (int r = 1; r < VM.BIND_SQL_SOUTEZ_ROUNDS + 1; r++)
            {


                for (int g = 1; g < VM.BIND_SQL_SOUTEZ_GROUPS + 1; g++)
                {

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


            int Round = 1;
            int Group = 0;
            int Startpoint = 0;


            string[] lines = File.ReadAllLines(@"E:\SORGAIR\SORGAIR\HH6C\bin\Debug\Matrix\" + VM.Listofmatrixes[filewithmatrix.SelectedIndex].Filename + ".txt");
            for (var i = 0; i < lines.Length; i++)
            {
                if (i > 2) //ořezání prvních 4 linek (poznamky, autor atd)



                {
                    string radekzmatice = lines[i]; //nactu linku
                    string[] poleidnaradku = radekzmatice.Split(','); //rozsekam ji na casti
                    for (var x = 1; x < poleidnaradku.Length+1; x++)
                    {
                        if (!uniqueStrings.Contains(poleidnaradku[x-1]))
                        {
                            uniqueStrings.Add(poleidnaradku[x-1]);
                        }
                    }
                }

            }

            foreach (string tmpval in uniqueStrings)
            {
                VM.SQL_SAVESOUTEZDATA("update users set Matrixid="+ tmpval.Substring(1)  + " where id in (select id from users where matrixid=0 and id > 0 limit 1);");
                System.Diagnostics.Debug.WriteLine(tmpval.Substring(1));
            }



            for (var i = 0; i < lines.Length; i++)
                if (i > 2) //ořezání prvních 4 linek (poznamky, autor atd)



                {

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



                        for (var x = 1; x < poleidnaradku.Length+1; x++)
                        {
                            //if (!uniqueStrings.Contains("R:" +  Round  + "G:" + Group + "L:" + i + "_" + poleidnaradku[x]))
                            //{
                            //uniqueStrings.Add("L:" + i + "_" + poleidnaradku[x]);
                            //}
                            System.Diagnostics.Debug.WriteLine("R:" + Round + "|G:" + Group + "|L:" + x + "|" + poleidnaradku[x-1]);

                            VM.SQL_SAVESOUTEZDATA("update matrix set user=(select id from users where Matrixid=" + poleidnaradku[x-1].Substring(1) + ") where rnd=" + Round + " and grp=" + Group + " and stp=" + x + " ;");
                            VM.SQL_SAVESOUTEZDATA("update score set userid=(select ifnull(id,0) from users where Matrixid=" + poleidnaradku[x - 1].Substring(1) + ") where rnd=" + Round + " and grp=" + Group + " and stp=" + x + " ;");
                        }

                        controller.SetMessage("Placing Round / Group:" + Round + "/" + Group );
                        await Task.Delay(1);

                    }
                }


         VM.FUNCTION_ROUNDS_LOAD_ROUNDS();

            controller.SetProgress(1);
            controller.SetMessage("New matrix created");
            await Task.Delay(1000);
            await controller.CloseAsync();


        }

    }
}
