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

namespace finance_pal.Views.Components
{

    public partial class TitleBar : UserControl
    {
        private bool isExpanded = false;
        public TitleBar()
        {
            InitializeComponent();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        private void Expand_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (isExpanded)
            {
                window.Width = 800; 
                window.Height = 450; 
                isExpanded = false;
            }
            else
            {
                window.Width = 1200; 
                window.Height = 800; 
                isExpanded = true;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                Expand_Click(sender, e);
            }
            else
            {
                Window.GetWindow(this).DragMove();
            }
        }
    }
}
