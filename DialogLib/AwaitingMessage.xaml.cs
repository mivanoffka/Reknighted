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

namespace DialogLib
{
    /// <summary>
    /// Логика взаимодействия для Message.xaml
    /// </summary>
    public partial class AwaitingMessage : UserControl
    {
        Grid foreignGrid;
        public static Label? foreignLabel;

        private AwaitingMessage()
        {
            InitializeComponent();
        }

        private void SleepThread()
        {
            Thread.Sleep(500);
            Dispatcher.Invoke(Iterate);
            Thread.Sleep(500);
            Dispatcher.Invoke(Iterate);
            Thread.Sleep(500);
            Dispatcher.Invoke(Iterate);
            Thread.Sleep(500);
            Dispatcher.Invoke(Finish);
        }

        private void Start(Grid parent, string message)
        {
            label.Text = message;

            foreignGrid = parent;
            foreignGrid.Children.Add(this);

            Thread thread = new Thread(SleepThread);
            thread.Start();
        }

        private void Finish()
        {
            foreignGrid.Children.Remove(this);
        }

        private void Iterate()
        {
            this.label.Text += ".";
        }

        public static void ShowAwaitingMessage(Grid parent, string message)
        {   
            AwaitingMessage control = new AwaitingMessage();
            control.Start(parent, message);
        }
    }
}
