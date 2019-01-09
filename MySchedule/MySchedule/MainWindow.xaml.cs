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
using System.Windows.Threading;

namespace MySchedule
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>

    public class MyButton : Button
    {
        public int Index { get; set; }
    }

    public class MyCheckBox : CheckBox
    {
        public DateTime Time { get; set; }
        public int Index { get; set; }
    }

    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer;
        private int index = 1;
        public MainWindow()
        {
            InitializeComponent();
            AddTask();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i <= index; i++)
            {
                MyCheckBox cb = ground.FindName("cb" + index) as MyCheckBox;
                if (cb!=null&&cb.IsChecked == true)
                {
                    Label label = ground.FindName("label" + index) as Label;
                    label.Content = (DateTime.Now - cb.Time).ToString().Split('.')[0];
                }
            }
        }

        private void AddTask()
        {
            AddTextBox();
            AddManageButton();
            AddStartButton();
            addCounter();
            index++;
        }

        private void AddTextBox()
        {
            TextBox tb = new TextBox();
            Grid.SetRow(tb, index);
            Grid.SetColumn(tb, 0);
            Grid.SetColumnSpan(tb, 3);
            tb.Margin = new Thickness(20, 0, 0, 0);
            tb.Height = 20;
            tb.Focus();
            tb.Text = "";
            ground.RegisterName("tb" + index, tb);
            ground.Children.Add(tb);
        }

        private void AddStartButton()
        {
            MyCheckBox cb = new MyCheckBox();
            Grid.SetRow(cb, index);
            Grid.SetColumn(cb, 4);
            cb.Index = index;
            cb.VerticalAlignment = VerticalAlignment.Center;
            cb.Content = "开始";
            cb.Click += Cb_Click;
            ground.RegisterName("cb" + index,cb);
            ground.Children.Add(cb);
        }

        private void Cb_Click(object sender, RoutedEventArgs e)
        {
            MyCheckBox cb = (MyCheckBox)sender;
            if (cb.IsChecked==true)
            {
                cb.Time = DateTime.Now;
                cb.Content = "暂停";
            }
            else
            {
                Label label = ground.FindName("label" + cb.Index) as Label;
                label.Content = (DateTime.Now - cb.Time).ToString().Split('.')[0];
                cb.Content = "开始";
            }
        }
        private void addCounter()
        {
            Label label = new Label();
            label.Content = 0;
            label.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(label, index);
            Grid.SetColumn(label, 5);
            ground.RegisterName("label" + index, label);
            ground.Children.Add(label);
        }

        private void AddManageButton()
        {
            StackPanel spManage = new StackPanel();
            Grid.SetRow(spManage, index);
            Grid.SetColumn(spManage, 6);
            spManage.Orientation = Orientation.Horizontal;

            MyButton btnDelete = new MyButton();
            btnDelete.Index = index;
            btnDelete.Height = 20;
            btnDelete.Content = "删除";
            btnDelete.Click += btnDelete_Click;
            spManage.Children.Add(btnDelete);

            

            MyButton btnAdd = new MyButton();
            btnAdd.Height = 20;
            btnAdd.Content = "新建任务";
            btnAdd.Margin = new Thickness(5, 0, 5, 0);
            btnAdd.Click += addTask_Click;
            spManage.Children.Add(btnAdd);

            ground.RegisterName("spManage" + index, spManage);
            ground.Children.Add(spManage);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MyButton mb = (MyButton)sender;
            TextBox tb = ground.FindName("tb" + mb.Index) as TextBox;
            MyCheckBox cb = ground.FindName("cb" + mb.Index) as MyCheckBox;
            Label label = ground.FindName("label" + mb.Index) as Label;
            StackPanel spManage = ground.FindName("spManage" + mb.Index) as StackPanel;
            ground.Children.Remove(tb);
            ground.Children.Remove(cb);
            ground.Children.Remove(label);
            ground.Children.Remove(spManage);
        }

        private void addTask_Click(object sender, RoutedEventArgs e)
        {
            AddTask();
        }

   
    }
}
