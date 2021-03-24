﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp6.Model;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using ControlzEx.Theming;
using System.Printing;

using System.Windows.Documents.Serialization;

using System.Windows.Xps;

using System.Windows.Xps.Packaging;


namespace WpfApp6.View
{
    /// <summary>
    /// Interakční logika pro SecondView.xaml
    /// </summary>
    public partial class Soutezici : UserControl
    {

        
        
        private MODEL_ViewModel VM => this.DataContext as MODEL_ViewModel;





    public Soutezici()
        {
            InitializeComponent();

            

        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {

        }


        private void competitorlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (competitorlist.SelectedIndex>= 0) 
            {
                userdetail_id.Count = VM.Players[competitorlist.SelectedIndex].ID.ToString();
                userdetail_competitorname.Title = VM.Players[competitorlist.SelectedIndex].LASTNAME.ToString() + " " + VM.Players[competitorlist.SelectedIndex].FIRSTNAME.ToString();
                userdetail_agecat.Title   = "Věková kategorie : " + VM.Players[competitorlist.SelectedIndex].AGECAT .ToString();
                userdetail_NATLIC.Title = VM.Players[competitorlist.SelectedIndex].NACLIC  .ToString();
                userdetail_FAILIC.Title = VM.Players[competitorlist.SelectedIndex].FAILIC  .ToString();
                userdetail_club.Title = "Klub : " + VM.Players[competitorlist.SelectedIndex].CLUB .ToString();
                userdetail_freq .Title = "Frekvence : " + VM.Players[competitorlist.SelectedIndex].FREQ.ToString();
                userdetail_ch1 .Title = VM.Players[competitorlist.SelectedIndex].CH1 .ToString();
                userdetail_ch2 .Title = VM.Players[competitorlist.SelectedIndex].CH2.ToString();
                userdetail_country.Title = VM.Players[competitorlist.SelectedIndex].COUNTRY.ToString();
                VM.BIND_FLAG = VM.Players[competitorlist.SelectedIndex].COUNTRY.ToString();
                VM.BIND_PAID = VM.Players[competitorlist.SelectedIndex].PAIDSTR  .ToString();
                ispaid.IsEnabled = true;
                edituser.IsEnabled = true;
                delete_competitor.IsEnabled = true;


            }
            else
            {
                edituser.IsEnabled = false;
                ispaid.IsEnabled = false ;
                delete_competitor.IsEnabled = false ;
            }

        }


        private void l_FUNCTION_clear_all_newuser_fields()
        {
            l_firstname.Text = "";
            L_lastname.Text = "";
            l_country.SelectedIndex = 58;
            l_chanel1.Value = null ;
            l_chanel2.Value = null;
            l_club.Text = "";
            l_failic.Text = "";
            l_naclic.Text = "";
            l_agecat.SelectedIndex = -1;
            l_freq.SelectedIndex = -1;
            l_registered.IsOn  = false;
            l_nextid.Count = VM.SQL_READSOUTEZDATA("SELECT seq+1 FROM SQLITE_SEQUENCE where name='users'", "");



        }

        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            VM.Players[3].FIRSTNAME = "AAXXX";
        }

        private void landingoptions_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ispaid_Click(object sender, RoutedEventArgs e)
        {
            if ( competitorlist.SelectedIndex >= 0) {

                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var directory = System.IO.Path.GetDirectoryName(path);



                string check = (sender as MahApps.Metro.Controls.ToggleSwitch).IsOn.ToString();
                VM.Players[competitorlist.SelectedIndex].PAIDSTR = check;
                VM.Players[competitorlist.SelectedIndex].PAID = directory+ "/flags/" + check + ".png";
                VM.FUNCTION_COMPETITOR_UPDATE("Paid", check, VM.Players[competitorlist.SelectedIndex].ID);
            }
        }

        private void SplitButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string L_flag = (sender as MahApps.Metro.Controls.SplitButton).SelectedIndex.ToString();
            
            if (Int32.Parse(L_flag) >= 0 )
            {
                Console.WriteLine(L_flag);
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var directory = System.IO.Path.GetDirectoryName(path);

                L_newuser_flag.Source = new BitmapImage(new Uri(directory+ "/flags/" + VM.MODEL_Contest_FLAGS[Int32.Parse(L_flag)].FILENAME + ".png"));
            }

        }

        private void L_savenewuser_Click(object sender, RoutedEventArgs e)
        {
            if (__SAVE_NEW_USER() == true)
            {

                l_FUNCTION_clear_all_newuser_fields();
                firstFlyout.IsOpen = false;
                competitorlist.SelectedIndex = competitorlist.Items.Count - 1;
                competitorlist.ScrollIntoView(competitorlist.Items[competitorlist.SelectedIndex]);
            }

        }

        private void Tile_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private async  void delete_competitor_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this.TryFindParent<MetroWindow>();
            if (competitorlist.SelectedIndex >= 0)
            {
                MessageDialogResult result = await currentWindow.ShowMessageAsync("Smazání soutěžícího", "Opravdu smazat soutěžícího s ID: " + VM.Players[competitorlist.SelectedIndex].ID + " : " + VM.Players[competitorlist.SelectedIndex].FIRSTNAME + " " + VM.Players[competitorlist.SelectedIndex].LASTNAME + "?", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Negative)
                {
                    Console.WriteLine("No");
                }
                else
                {
                    Console.WriteLine("yes");
                    VM.FUNCTION_USERS_DELETE_COMPETITOR(VM.Players[competitorlist.SelectedIndex].ID);
                    competitorlist.SelectedIndex = competitorlist.Items.Count - 1;
                    competitorlist.ScrollIntoView(competitorlist.Items[competitorlist.SelectedIndex]);

                }
            }
            else
            {
                await currentWindow.ShowMessageAsync("Nikdo není vybrán", "Vyber prosím, koho chceš smazat");
            }
    
        }

        private void addnewuser_Click(object sender, RoutedEventArgs e)
        {
            //VM.save_new_competitor("firstname", "lastname", "FIN");
            firstFlyout.IsOpen = true;
            l_nextid.Count  = VM.SQL_READSOUTEZDATA("SELECT seq+1 FROM SQLITE_SEQUENCE where name='users'", "");
        }


        private bool  __SAVE_NEW_USER()
        {
            bool results = true ;
            string _failictmp = "";


            if (VM.BIND_SQL_SOUTEZ_REQUIREFAILICENCE == false & l_failic.Text == "")
            {
                _failictmp = "---";
            }
            else
            {
                _failictmp = l_failic.Text;
            }



            if (l_firstname.Text == "" || L_lastname.Text == "" || l_agecat.SelectedIndex == -1 || l_club.Text == "" || l_country.SelectedIndex == -1 || _failictmp == "" || l_freq.SelectedIndex == -1 || l_naclic.Text == "")
            {
                results = false;
                var currentWindow = this.TryFindParent<MetroWindow>();
                currentWindow.ShowMessageAsync("Nelze uložit", "Nejsou vyplněné všechny údaje soutěžícího. Nelze uložit");

            }
            else
            {
                if (l_freq.SelectedIndex == 0) 
                {
                    l_chanel1.Value = 0;
                    l_chanel2.Value = 0;
                }

                Console.WriteLine("l_freq.SelectedIndex" + l_freq.SelectedIndex);
                Console.WriteLine("l_chanel1.Value" + l_chanel1.Value);
                Console.WriteLine("l_chanel2.Value" + l_chanel2.Value);
                if (l_freq.SelectedIndex != 0 && (l_chanel1.Value is null || l_chanel2.Value is null))
                {
                    results = false;
                    var currentWindow = this.TryFindParent<MetroWindow>();
                    currentWindow.ShowMessageAsync("Nelze uložit", "Nejsou vyplněné kanály u  zvolené frekvence");
                }
                else
                {
                    VM.FUNCTION_USERS_CREATE_NEW(l_firstname.Text, L_lastname.Text, VM.MODEL_Contest_FLAGS[l_country.SelectedIndex].FILENAME, VM.MODEL_Contest_AGECATEGORIES[l_agecat.SelectedIndex].ID, VM.MODEL_Contest_FREQUENCIES[l_freq.SelectedIndex].ID, Convert.ToInt32(l_chanel1.Value), Convert.ToInt32(l_chanel2.Value), _failictmp, l_naclic.Text, l_club.Text, Convert.ToBoolean(l_registered.IsOn), 0, 1);
                }



            }
            return results;
        }

        private bool __EDIT_NEW_USER()
        {
            bool results = true;
            string _failictmp = "";

            if (VM.BIND_SQL_SOUTEZ_REQUIREFAILICENCE == false)
            {
                if (l_failic_edit.Text == "")
                {
                    _failictmp = "---";
                }
                else
                {
                    _failictmp = l_failic_edit.Text;
                }

            }
            else
            {
                _failictmp = l_failic_edit.Text;
            }

            if (l_firstname_edit.Text == "" || L_lastname_edit.Text == "" || l_agecat_edit.SelectedIndex == -1 || l_club_edit.Text == "" || l_country_edit.SelectedIndex == -1 || _failictmp == "" || l_freq_edit.SelectedIndex == -1 || l_naclic_edit.Text == "")
            {
                results = false;
                var currentWindow = this.TryFindParent<MetroWindow>();
                currentWindow.ShowMessageAsync("Nelze uložit", "Nejsou vyplněné všechny údaje soutěžícího. Nelze uložit");

            }
            else
            {
                if (l_freq_edit.SelectedIndex == 0)
                {
                    l_chanel1_edit.Value = 0;
                    l_chanel2_edit.Value = 0;
                }



                if (l_freq_edit.SelectedIndex != 0)
                {
                    if (l_chanel1_edit.Value is null || l_chanel2_edit.Value is null)
                    {
                        results = false;
                        var currentWindow = this.TryFindParent<MetroWindow>();
                        currentWindow.ShowMessageAsync("Nelze uložit", "EDIT Nejsou vyplněné kanály u  zvolené frekvence");

                    }
                }

                if (results != false)
                {
                VM.FUNCTION_USERS_CREATE_EDIT(int.Parse(l_nextid_edit.Count), l_firstname_edit.Text, L_lastname_edit.Text, VM.MODEL_Contest_FLAGS[l_country_edit.SelectedIndex].FILENAME, VM.MODEL_Contest_AGECATEGORIES[l_agecat_edit.SelectedIndex].ID, VM.MODEL_Contest_FREQUENCIES[l_freq_edit.SelectedIndex].ID, Convert.ToInt32(l_chanel1_edit.Value), Convert.ToInt32(l_chanel2_edit.Value), _failictmp, l_naclic_edit.Text, l_club_edit.Text, Convert.ToBoolean(l_registered_edit.IsOn), 1);
                }



            }
            return results;
        }


        private void L_savenewuserandagain_Click(object sender, RoutedEventArgs e)
        {

            if (__SAVE_NEW_USER() == true)
            {
                l_FUNCTION_clear_all_newuser_fields();
                firstFlyout.IsOpen = true;
                competitorlist.SelectedIndex = competitorlist.Items.Count - 1;
                competitorlist.ScrollIntoView(competitorlist.Items[competitorlist.SelectedIndex]);
            }
        }

        private void L_savenewuser_clearall_Click(object sender, RoutedEventArgs e)
        {
            l_FUNCTION_clear_all_newuser_fields();
        }

        private void L_close_Click(object sender, RoutedEventArgs e)
        {
            l_FUNCTION_clear_all_newuser_fields();
            firstFlyout.IsOpen = false;
        }

        private void edituser_Click(object sender, RoutedEventArgs e)
        {

            l_nextid_edit.Count = VM.Players[competitorlist.SelectedIndex].ID.ToString();
            L_lastname_edit.Text = VM.Players[competitorlist.SelectedIndex].LASTNAME.ToString();
            l_firstname_edit.Text = VM.Players[competitorlist.SelectedIndex].FIRSTNAME.ToString();
            l_naclic_edit.Text = VM.Players[competitorlist.SelectedIndex].NACLIC.ToString();
            l_failic_edit.Text = VM.Players[competitorlist.SelectedIndex].FAILIC.ToString();
            l_club_edit.Text = VM.Players[competitorlist.SelectedIndex].CLUB.ToString();
            l_chanel1_edit.Value = VM.Players[competitorlist.SelectedIndex].CH1;
            l_chanel2_edit.Value = VM.Players[competitorlist.SelectedIndex].CH2;
            l_freq_edit.SelectedIndex = VM.Players[competitorlist.SelectedIndex].FREQID;
            foreach (var stat in VM.MODEL_Contest_FLAGS)
            {
                if (stat.FILENAME == VM.Players[competitorlist.SelectedIndex].COUNTRY.ToString()) { l_country_edit.SelectedIndex = stat.ID; }
            }

            l_agecat_edit.SelectedIndex = VM.Players[competitorlist.SelectedIndex].AGECATID;
            l_registered_edit.IsOn = bool.Parse(VM.Players[competitorlist.SelectedIndex].PAIDSTR);

            firstFlyout_edit.IsOpen = true;

            //VM.BIND_FLAG = VM.Players[competitorlist.SelectedIndex].COUNTRY.ToString();
            //VM.BIND_PAID = VM.Players[competitorlist.SelectedIndex].PAIDSTR.ToString();
            //ispaid.IsEnabled = true;



        }

        private void L_back_edit_Click(object sender, RoutedEventArgs e)
        {
            firstFlyout_edit.IsOpen = false;
        }

        private void L_savenewuser_edit_Click(object sender, RoutedEventArgs e)
        {

            int _selectedusr = competitorlist.SelectedIndex;
            if (__EDIT_NEW_USER() == true)
            {
                firstFlyout_edit.IsOpen = false;
                competitorlist.SelectedIndex = _selectedusr;
                competitorlist.ScrollIntoView(competitorlist.Items[competitorlist.SelectedIndex]);
            }

        }

        private void l_country_edit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string L_flag = (sender as MahApps.Metro.Controls.SplitButton).SelectedIndex.ToString();

            if (Int32.Parse(L_flag) >= 0)
            {
                Console.WriteLine(L_flag);
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var directory = System.IO.Path.GetDirectoryName(path);

                L_newuser_flag_edit.Source = new BitmapImage(new Uri(directory + "/flags/" + VM.MODEL_Contest_FLAGS[Int32.Parse(L_flag)].FILENAME + ".png"));
            }

        }

        private void L_savenewuser_edxxit_Click(object sender, RoutedEventArgs e)
        {
        }

        private void printuserlist_Click(object sender, RoutedEventArgs e)
        {
            firstFlyout_print_userlist.IsOpen = true;
        }



        public void Print_WPF_Preview(FrameworkElement wpf_Element)

        {

            //------------< WPF_Print_current_Window >------------

            //--< create xps document >--

            if (System.IO.File.Exists("print_preview.xps")) { System.IO.File.Delete("print_preview.xps"); }
            XpsDocument doc = new XpsDocument("print_preview.xps", System.IO.FileAccess.ReadWrite);

            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);

            SerializerWriterCollator preview_Document = writer.CreateVisualsCollator();

            preview_Document.BeginBatchWrite();

            preview_Document.Write(wpf_Element);  //*this or wpf xaml control

            preview_Document.EndBatchWrite();

            //--</ create xps document >--



            //var doc2 = new XpsDocument("Druckausgabe.xps", FileAccess.Read);



            FixedDocumentSequence preview = doc.GetFixedDocumentSequence();



            var window = new Window();

            window.Content = new DocumentViewer { Document = preview };

            window.ShowDialog();



            doc.Close();

            //------------</ WPF_Print_current_Window >------------





        }


        public static class PrintHelper
        {
            public static FixedDocument GetFixedDocument(FrameworkElement toPrint, PrintDialog printDialog)
            {
                PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);
                Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
                Size visibleSize = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);
                FixedDocument fixedDoc = new FixedDocument();

                // If the toPrint visual is not displayed on screen we neeed to measure and arrange it.
                toPrint.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                toPrint.Arrange(new Rect(new Point(0, 0), toPrint.DesiredSize));

                Size size = toPrint.DesiredSize;

                // Will assume for simplicity the control fits horizontally on the page.
                double yOffset = 0;
                while (yOffset < size.Height)
                {
                    VisualBrush vb = new VisualBrush(toPrint);
                    vb.Stretch = Stretch.None;
                    vb.AlignmentX = AlignmentX.Left;
                    vb.AlignmentY = AlignmentY.Top;
                    vb.ViewboxUnits = BrushMappingMode.Absolute;
                    vb.TileMode = TileMode.None;
                    vb.Viewbox = new Rect(0, yOffset, visibleSize.Width, visibleSize.Height);

                    PageContent pageContent = new PageContent();
                    FixedPage page = new FixedPage();
                    ((System.Windows.Markup.IAddChild)pageContent).AddChild(page);
                    fixedDoc.Pages.Add(pageContent);
                    page.Width = pageSize.Width;
                    page.Height = pageSize.Height;

                    Canvas canvas = new Canvas();
                    FixedPage.SetLeft(canvas, capabilities.PageImageableArea.OriginWidth);
                    FixedPage.SetTop(canvas, capabilities.PageImageableArea.OriginHeight);
                    canvas.Width = visibleSize.Width;
                    canvas.Height = visibleSize.Height;
                    canvas.Background = vb;
                    page.Children.Add(canvas);

                    yOffset += visibleSize.Height;
                }
                return fixedDoc;
            }

            public static void ShowPrintPreview(FixedDocument fixedDoc)
            {
                Window wnd = new Window();
                DocumentViewer viewer = new DocumentViewer();
                viewer.Document = fixedDoc;
                wnd.Content = viewer;
                wnd.ShowDialog();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog xxx = new PrintDialog();

            IDocumentPaginatorSource idocument = myFlowDocReader as IDocumentPaginatorSource;
            xxx.PrintDocument(idocument.DocumentPaginator, "Printing Machine : ");

            //ThemeManager.Current.ChangeTheme(idocument, "Light.Blue");


            //PrintHelper.ShowPrintPreview(PrintHelper.GetFixedDocument(testprint, xxx));
            //Print_WPF_Preview(competitorcards_print);
           // Print();
        }

        private void printusercards_Click(object sender, RoutedEventArgs e)
        {
            firstFlyout_print_usercards.IsOpen = true;
        }

        internal void Print()
        {
          //  var paginator = new ProgramPaginator(testprint);
            var dlg = new PrintDialog();


           // paginator.PageSize = new Size(dlg.PrintableAreaWidth, dlg.PrintableAreaHeight);

            Window wnd = new Window();
            DocumentViewer viewer = new DocumentViewer();
          //  viewer.Document = PrintHelper.GetFixedDocument(competitorcards_print, dlg);
            wnd.Content = viewer;
            wnd.ShowDialog();




            if ((bool)dlg.ShowDialog())
            {
              //  dlg.PrintDocument(paginator, "Program");
            }
        }

        private void createFlowDoc(object sender, RoutedEventArgs e)
        {



            BlockUIContainer bc = new BlockUIContainer();

            Image myImg = new Image();
            myImg.Source = new BitmapImage(new Uri(@"C:\asw\test.bmp"));
            myImg.Width = 50;
            myImg.Height = 50;
            myImg.Stretch = Stretch.Fill;
            myImg.Margin = new Thickness(0, 0, 0, 0);

            Grid grid = new Grid();
            grid.Width = 400;
            grid.Height = 50;
            grid.Background = new SolidColorBrush(Colors.Green);
            grid.Children.Add(myImg);

            StackPanel sp = new StackPanel();

            sp.Width = 400;
            sp.Height = 50;
            sp.Orientation = Orientation.Vertical;
            sp.HorizontalAlignment = HorizontalAlignment.Left;
            sp.Children.Add(grid);
            bc.Child = sp;


            BlockUIContainer document1 = new BlockUIContainer();

            this.myFlowDoc.Blocks.Add(bc);
        }
    }



    class ProgramPaginator : DocumentPaginator
    {
        private FrameworkElement Element;
        private ProgramPaginator()
        {
        }

        public ProgramPaginator(FrameworkElement element)
        {
            Element = element;
        }

        public override DocumentPage GetPage(int pageNumber)
        {

            Element.RenderTransform = new TranslateTransform(-PageSize.Width * (pageNumber % Columns), -PageSize.Height * (pageNumber / Columns));

            Size elementSize = new Size(
                Element.ActualWidth,
                Element.ActualHeight);
            Element.Measure(elementSize);
            Element.Arrange(new Rect(new Point(0, 0), elementSize));

            var page = new DocumentPage(Element);
            Element.RenderTransform = null;

            return page;
        }

        public override bool IsPageCountValid
        {
            get { return true; }
        }

        public int Columns
        {
            get
            {
                return (int)Math.Ceiling(Element.ActualWidth / PageSize.Width);
            }
        }
        public int Rows
        {
            get
            {
                return (int)Math.Ceiling(Element.ActualHeight / PageSize.Height);
            }
        }

        public override int PageCount
        {
            get
            {
                return Columns * Rows;
            }
        }

        public override Size PageSize
        {
            set; get;
        }

        public override IDocumentPaginatorSource Source
        {
            get { return null; }
        }
    }


}
