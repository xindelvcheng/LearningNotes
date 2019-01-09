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

namespace Demo2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TaskItem> taskList;
        private int index = 0;

        public MainWindow()
        {
            InitializeComponent();
            for(int i = 0; i < 16; i++) { ground.RowDefinitions.Add(new RowDefinition()); }
            taskList = new List<TaskItem>();
            TaskItem ti = new TaskItem();
            ti.AddHandler(TaskItem.DeleteEvent,new RoutedEventHandler(Ti_deleteTask));
            ti.Index = index++;
            taskList.Add(ti);
            Grid.SetRow(ti,1);
            ground.Children.Add(ti);
        }

        private void NewTask_Click(object sender, RoutedEventArgs e)
        {
            TaskItem ti = new TaskItem();
            ti.Index = index++;
            ti.AddHandler(TaskItem.DeleteEvent,new RoutedEventHandler(Ti_deleteTask));
            Grid.SetRow(ti, taskList.Count+1);
            taskList.Add(ti);
            ground.Children.Add(ti);
            ReOrder();
        }

        private void Ti_deleteTask(object sender, EventArgs e)
        {
           
            DeleteTaskEventArgs dte = (DeleteTaskEventArgs)e;
            for (int i = 0; i < taskList.Count; i++)
            {
                ground.Children.Remove(taskList[i]);
            }
                taskList.Remove(taskList[dte.TaskIndex]);
            for (int i = 0; i < taskList.Count; i++)
            {
                taskList[i].Index = i;
                Grid.SetRow(taskList[i], i + 1);
                ground.Children.Add(taskList[i]);
            }
        }

        private void ReOrder()
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                taskList[i].Index = i;
            }
        }

    }

    class TaskItem : Grid
    {
        private TextBox tb;
        private CheckBox cb;
        private DateTime time;
        private Label counter;
        private Button btnDelete;
        private Button btnAdd;
        public int Index { get; set; }
        public TaskItem()
        {
            for (int i = 0; i < 7; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                this.ColumnDefinitions.Add(cd);
            }
            AddTextBox();
            AddStartButton();
            AddCounter();
            AddDeleteButton();
            AddCreateButton();
        }

        private void AddTextBox()
        {
            tb = new TextBox();
            Grid.SetColumn(tb, 0);
            Grid.SetColumnSpan(tb, 3);
            tb.Margin = new Thickness(20, 0, 20, 0);
            tb.Height = 20;
            tb.Text = "";
            this.Children.Add(tb);
        }

        private void AddStartButton()
        {
            cb = new CheckBox();
            Grid.SetColumn(cb, 3);
            cb.VerticalAlignment = VerticalAlignment.Center;
            cb.Margin = new Thickness(10, 0, 10, 0);
            cb.Content = "开始";
            cb.Click += Cb_Click;
            this.Children.Add(cb);
        }

        private void Cb_Click(object sender, RoutedEventArgs e)
        {
            if (cb.IsChecked == true)
            {
                this.time = DateTime.Now;
                cb.Content = "暂停";
            }
            else
            {
                counter.Content = (DateTime.Now - this.time).ToString().Split('.')[0];
                cb.Content = "开始";
            }
        }

        private void AddCounter()
        {
            counter = new Label();
            counter.Content = "00:00:00";
            counter.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(counter, 4);
            //this.RegisterName("counter", label);
            this.Children.Add(counter);
        }

        private void AddDeleteButton()
        {
            btnDelete = new Button();
            Grid.SetColumn(btnDelete, 5);
            btnDelete.Height = 20;
            btnDelete.Content = "删除";
            btnDelete.Click += BtnDelete_Click;
            this.Children.Add(btnDelete);
        }

        //第一步：声明并注册路由事件，与消息 EMessages 关联。
        //EventManager.RegisterRoutedEvent(CLR事件包装器名称,路由事件冒泡策略,事件处理程序的类型,路由事件的所有者类类型)
        public static readonly RoutedEvent DeleteEvent = EventManager.RegisterRoutedEvent
 ("TaskItem", RoutingStrategy.Bubble, typeof(EventHandler<DeleteTaskEventArgs>), typeof(TaskItem));


        //第二步：为路由事件添加CLR事件包装器
        public event RoutedEventHandler Delete
        {
            add { this.AddHandler(DeleteEvent, value); }
            remove { this.RemoveHandler(DeleteEvent, value); }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteTaskEventArgs args = new DeleteTaskEventArgs(DeleteEvent, this);
            args.TaskIndex = this.Index;
            //调用元素的RaiseEvent方法（继承自UIElement类），将事件发出去
            this.RaiseEvent(args);
        }

        private void AddCreateButton()
        {
            btnAdd = new Button();
            Grid.SetColumn(btnAdd, 6);
            btnAdd.Height = 20;
            btnAdd.Content = "新建任务";
            btnAdd.Margin = new Thickness(5, 0, 5, 0);
            this.Children.Add(btnAdd);
        }

    }

    class DeleteTaskEventArgs : RoutedEventArgs
    {
        public DeleteTaskEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source) { }
        public int TaskIndex { get; set; }
    }
}
