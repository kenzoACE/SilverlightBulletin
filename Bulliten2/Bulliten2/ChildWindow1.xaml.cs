using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Bulliten2
{
    public partial class ChildWindow1 : ChildWindow
    {
        public ChildWindow1()
        {
            InitializeComponent();
        }

        private void ScrollViewer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            childWindowScrollViewer.ScrollToVerticalOffset(childWindowScrollViewer.VerticalOffset - e.Delta);
        }
    }
}

