using System;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Documents;
//using System.Windows.Ink;
using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Shapes;
using System.ComponentModel;

namespace Bulliten2
{
    public class BullitenBinding : INotifyPropertyChanged
    {
        private string heading1 = "";
        private string heading2 = "";
        private string heading3 = "";
        private string heading4 = "";
        private string heading5 = "";
        private string heading6 = "";
        private string heading7 = "";
        private string heading8 = "";
        private string heading9 = "";
        private string heading10 = "";
        private string heading11 = "";
        private string heading12 = "";
        private string heading13 = "";
        private string heading14 = "";
        private string heading15 = "";
        private string heading16 = "";
        private string heading17 = "";
//        private string content = "";

        private string tooltip1 = "";
        private string tooltip2 = "";
        private string tooltip3 = "";
        private string tooltip4 = "";
        private string tooltip5 = "";
        private string tooltip6 = "";
        private string tooltip7 = "";
        private string tooltip8 = "";
        private string tooltip9 = "";
        private string tooltip10 = "";
        private string tooltip11 = "";
        private string tooltip12 = "";
        private string tooltip13 = "";
        private string tooltip14 = "";
        private string tooltip15 = "";
        private string tooltip16 = "";
        private string tooltip17 = "";

        private string viewToolTip = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public string Heading1
        {
            get { return heading1; }
            set
            {
                heading1 = value;
                NotifyPropertyChanged("Heading1");
            }
        }

        public string Heading2
        {
            get { return heading2; }
            set
            {
                heading2 = value;
                NotifyPropertyChanged("Heading2");
            }
        }

        public string Heading3
        {
            get { return heading3; }
            set
            {
                heading3 = value;
                NotifyPropertyChanged("Heading3");
            }
        }

        public string Heading4
        {
            get { return heading4; }
            set
            {
                heading4 = value;
                NotifyPropertyChanged("Heading4");
            }
        }

        public string Heading5
        {
            get { return heading5; }
            set
            {
                heading5 = value;
                NotifyPropertyChanged("Heading5");
            }
        }

        public string Heading6
        {
            get { return heading6; }
            set
            {
                heading6 = value;
                NotifyPropertyChanged("Heading6");
            }
        }

        public string Heading7
        {
            get { return heading7; }
            set
            {
                heading7 = value;
                NotifyPropertyChanged("Heading7");
            }
        }

        public string Heading8
        {
            get { return heading8; }
            set
            {
                heading8 = value;
                NotifyPropertyChanged("Heading8");
            }
        }

        public string Heading9
        {
            get { return heading9; }
            set
            {
                heading9 = value;
                NotifyPropertyChanged("Heading9");
            }
        }

        public string Heading10
        {
            get { return heading10; }
            set
            {
                heading10 = value;
                NotifyPropertyChanged("Heading10");
            }
        }

        public string Heading11
        {
            get { return heading11; }
            set
            {
                heading11 = value;
                NotifyPropertyChanged("Heading11");
            }
        }

        public string Heading12
        {
            get { return heading12; }
            set
            {
                heading12 = value;
                NotifyPropertyChanged("Heading12");
            }
        }

        public string Heading13
        {
            get { return heading13; }
            set
            {
                heading13 = value;
                NotifyPropertyChanged("Heading13");
            }
        }

        public string Heading14
        {
            get { return heading14; }
            set
            {
                heading14 = value;
                NotifyPropertyChanged("Heading14");
            }
        }

        public string Heading15
        {
            get { return heading15; }
            set
            {
                heading15 = value;
                NotifyPropertyChanged("Heading15");
            }
        }

        public string Heading16
        {
            get { return heading16; }
            set
            {
                heading16 = value;
                NotifyPropertyChanged("Heading16");
            }
        }

        public string Heading17
        {
            get { return heading17; }
            set
            {
                heading17 = value;
                NotifyPropertyChanged("Heading17");
            }
        }
        public string Tooltip1
        {
            get { return tooltip1; }
            set
            {
                tooltip1 = value;
                NotifyPropertyChanged("Tooltip1");
            }
        }

        public string Tooltip2
        {
            get { return tooltip2; }
            set
            {
                tooltip2 = value;
                NotifyPropertyChanged("Tooltip2");
            }
        }

        public string Tooltip3
        {
            get { return tooltip3; }
            set
            {
                tooltip3 = value;
                NotifyPropertyChanged("Tooltip3");
            }
        }

        public string Tooltip4
        {
            get { return tooltip4; }
            set
            {
                tooltip4 = value;
                NotifyPropertyChanged("Tooltip4");
            }
        }

        public string Tooltip5
        {
            get { return tooltip5; }
            set
            {
                tooltip5 = value;
                NotifyPropertyChanged("Tooltip5");
            }
        }

        public string Tooltip6
        {
            get { return tooltip6; }
            set
            {
                tooltip6 = value;
                NotifyPropertyChanged("Tooltip6");
            }
        }

        public string Tooltip7
        {
            get { return tooltip7; }
            set
            {
                tooltip7 = value;
                NotifyPropertyChanged("Tooltip7");
            }
        }

        public string Tooltip8
        {
            get { return tooltip8; }
            set
            {
                tooltip8 = value;
                NotifyPropertyChanged("Tooltip8");
            }
        }

        public string Tooltip9
        {
            get { return tooltip9; }
            set
            {
                tooltip9 = value;
                NotifyPropertyChanged("Tooltip9");
            }
        }

        public string Tooltip10
        {
            get { return tooltip10; }
            set
            {
                tooltip10 = value;
                NotifyPropertyChanged("Tooltip10");
            }
        }

        public string Tooltip11
        {
            get { return tooltip11; }
            set
            {
                tooltip11 = value;
                NotifyPropertyChanged("Tooltip11");
            }
        }

        public string Tooltip12
        {
            get { return tooltip12; }
            set
            {
                tooltip12 = value;
                NotifyPropertyChanged("Tooltip12");
            }
        }

        public string Tooltip13
        {
            get { return tooltip13; }
            set
            {
                tooltip13 = value;
                NotifyPropertyChanged("Tooltip13");
            }
        }

        public string Tooltip14
        {
            get { return tooltip14; }
            set
            {
                tooltip14 = value;
                NotifyPropertyChanged("Tooltip14");
            }
        }

        public string Tooltip15
        {
            get { return tooltip15; }
            set
            {
                tooltip15 = value;
                NotifyPropertyChanged("Tooltip15");
            }
        }

        public string Tooltip16
        {
            get { return tooltip16; }
            set
            {
                tooltip16 = value;
                NotifyPropertyChanged("Tooltip16");
            }
        }

        public string Tooltip17
        {
            get { return tooltip17; }
            set
            {
                tooltip17 = value;
                NotifyPropertyChanged("Tooltip17");
            }
        }

        public string ViewToolTip
        {
            get { return viewToolTip; }
            set
            {
                viewToolTip = value;
                NotifyPropertyChanged("ViewToolTip");
            }
        }

        //factoring out the call to the event
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
