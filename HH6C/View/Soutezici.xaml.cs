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
using WpfApp6.Model;


namespace WpfApp6.View
{
    /// <summary>
    /// Interakční logika pro SecondView.xaml
    /// </summary>
    public partial class Soutezici : UserControl
    {
        private ViewModel VM => this.DataContext as ViewModel;
        public Soutezici()
        {
            InitializeComponent();

            List<TodoItem> items = new List<TodoItem>();
//            MessageBox.Show(VM.SQL_READSOUTEZDATA("select count(ID) from users", ""));
            for (int i = 0; i < Convert.ToInt32(20); i++)
            {
                items.Add(new TodoItem() { Title = "Complete this WPF tutorial", Completion = 45 });
            }



            lbTodoList.ItemsSource = items;
        }

        public class TodoItem
        {
            public string Title { get; set; }
            public int Completion { get; set; }
        }


    }
}
