using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.IO.IsolatedStorage;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Automation;

namespace Bulliten2
{
    public partial class Page : UserControl
    {
        private BullitenBinding bulliten = null;
        string file = "";  //current file string which contains the whole bulliten
        //        string title = "";
        //        string name = "";
        //        string content = "";
        //        string time = "";
        ushort numberOfPosts = 0;
        int currentPost = 0;
        int currentTopPosition = 0;
        DispatcherTimer scrollTimer = new DispatcherTimer();
        int mouseHeadingNum = 0;
        int headingNum = 0;
        double tempDouble = 0;
        bool IS_LIVE_LOGIN = false;
        string ID = "";
        //        string name = "";
        private IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;  //data to confirm the user who posted comments and bulliten



        public Page()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Page_Loaded);
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            bulliten = new BullitenBinding();
            LayoutRoot.DataContext = bulliten;

            scrollTimer.Interval = TimeSpan.FromMilliseconds(300);
            scrollTimer.Tick += new EventHandler(ScrollTimer_Tick);

            //bulliten treeview
            displayTreeView.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(displayTreeView_SelectedItemChanged);
            displayTreeView.MouseWheel += new MouseWheelEventHandler(displayTreeView_MouseWheel);

            //populate the comboboxes
            settingComboBox.Items.Add("新規");
            for (int x = 1; x < 64; x++)
            {
                settingComboBox.Items.Add(x);
            }

            adminComboBox.Items.Add("掲示内容");

            for (int x = 1; x <= 64; x++)
            {
                adminComboBox.Items.Add(x);
            }

            adminComboBox.SelectedIndex = -1;  //always show blank item

            //add user name key to isolated storage so user does not have to retype name everytime
            //use stored user name if logged in
            ServiceReference1.WebServiceLiveSoapClient webserviceMgrSoapClient = new ServiceReference1.WebServiceLiveSoapClient();
            webserviceMgrSoapClient.HelloWorldCompleted += new EventHandler<ServiceReference1.HelloWorldCompletedEventArgs>(webserviceMgrSoapClient_HelloWorldCompleted);
            webserviceMgrSoapClient.HelloWorldAsync();

            if (!appSettings.Contains("UserName"))
            {
                appSettings.Add("UserName", "");
            }

            //initialize the scrol bar
            mainScrollBar.Maximum = 0;

            //initialize main view
            UriBuilder ub = new UriBuilder("http://fushigispace.com/faq/Bulliten/BullitenHandler.ashx");
            WebClient c = new WebClient();

            c.UploadStringCompleted += (sender2, e2) =>
            {
                file = String.Copy(e2.Result);
                bulliten.Heading1 = BullitenFile.CreateHeading((int)(1), file).Replace(":@", "");
                bulliten.Heading2 = BullitenFile.CreateHeading((int)(2), file).Replace(":@", "");
                bulliten.Heading3 = BullitenFile.CreateHeading((int)(3), file).Replace(":@", "");
                bulliten.Heading4 = BullitenFile.CreateHeading((int)(4), file).Replace(":@", "");
                bulliten.Heading5 = BullitenFile.CreateHeading((int)(5), file).Replace(":@", "");
                bulliten.Heading6 = BullitenFile.CreateHeading((int)(6), file).Replace(":@", "");
                bulliten.Heading7 = BullitenFile.CreateHeading((int)(7), file).Replace(":@", "");
                bulliten.Heading8 = BullitenFile.CreateHeading((int)(8), file).Replace(":@", "");
                bulliten.Heading9 = BullitenFile.CreateHeading((int)(9), file).Replace(":@", "");
                bulliten.Heading10 = BullitenFile.CreateHeading((int)(10), file).Replace(":@", "");
                bulliten.Heading11 = BullitenFile.CreateHeading((int)(11), file).Replace(":@", "");
                bulliten.Heading12 = BullitenFile.CreateHeading((int)(12), file).Replace(":@", "");
                bulliten.Heading13 = BullitenFile.CreateHeading((int)(13), file).Replace(":@", "");
                bulliten.Heading14 = BullitenFile.CreateHeading((int)(14), file).Replace(":@", "");
                bulliten.Heading15 = BullitenFile.CreateHeading((int)(15), file).Replace(":@", "");
                bulliten.Heading16 = BullitenFile.CreateHeading((int)(16), file).Replace(":@", "");
                bulliten.Heading17 = BullitenFile.CreateHeading((int)(17), file).Replace(":@", "");
                /*
                                bulliten.Tooltip1 = "名前：\r\n" + BullitenFile.GetName(1, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(1, file);
                                bulliten.Tooltip2 = "名前：\r\n" + BullitenFile.GetName(2, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(2, file);
                                bulliten.Tooltip3 = "名前：\r\n" + BullitenFile.GetName(3, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(3, file);
                                bulliten.Tooltip4 = "名前：\r\n" + BullitenFile.GetName(4, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(4, file);
                                bulliten.Tooltip5 = "名前：\r\n" + BullitenFile.GetName(5, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(5, file);
                                bulliten.Tooltip6 = "名前：\r\n" + BullitenFile.GetName(6, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(6, file);
                                bulliten.Tooltip7 = "名前：\r\n" + BullitenFile.GetName(7, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(7, file);
                                bulliten.Tooltip8 = "名前：\r\n" + BullitenFile.GetName(8, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(8, file);
                                bulliten.Tooltip9 = "名前：\r\n" + BullitenFile.GetName(9, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(9, file);
                                bulliten.Tooltip10 = "名前：\r\n" + BullitenFile.GetName(10, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(10, file);
                                bulliten.Tooltip11 = "名前：\r\n" + BullitenFile.GetName(11, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(11, file);
                                bulliten.Tooltip12 = "名前：\r\n" + BullitenFile.GetName(12, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(12, file);
                                bulliten.Tooltip13 = "名前：\r\n" + BullitenFile.GetName(13, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(13, file);
                                bulliten.Tooltip14 = "名前：\r\n" + BullitenFile.GetName(14, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(14, file);
                                bulliten.Tooltip15 = "名前：\r\n" + BullitenFile.GetName(15, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(15, file);
                                bulliten.Tooltip16 = "名前：\r\n" + BullitenFile.GetName(16, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(16, file);
                                bulliten.Tooltip17 = "名前：\r\n" + BullitenFile.GetName(17, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(17, file);
                 */
            };
            c.UploadStringAsync(ub.Uri, "");

            //initialize scroll bar
            UriBuilder ub2 = new UriBuilder("http://fushigispace.com/faq/Bulliten/BullitenLengthHandler.ashx");
            WebClient c2 = new WebClient();

            c2.UploadStringCompleted += (sender2, e2) =>
            {
                numberOfPosts = Convert.ToUInt16(String.Copy(e2.Result));

                if (numberOfPosts <= 17)
                {
                    mainScrollBar.Maximum = 0;
                }
                else
                {
                    mainScrollBar.Maximum = 1;
                    tempDouble = ((double)17 / (double)numberOfPosts) * (255 / numberOfPosts);
                    mainScrollBar.ViewportSize = tempDouble;
                }
            };
            c2.UploadStringAsync(ub2.Uri, "");
        }

        void webserviceMgrSoapClient_HelloWorldCompleted(object sender, ServiceReference1.HelloWorldCompletedEventArgs e)
        {
            if (e.Error == null && e.Result != "" && e.Result != null)
            {

                UriBuilder ub = new UriBuilder("http://fushigispace.com/faq/Bulliten/LiveGetName.ashx");
                WebClient wc = new WebClient();
                UriBuilder ub2 = new UriBuilder("http://fushigispace.com/faq/Bulliten/LiveGetID.ashx");
                WebClient wc2 = new WebClient();

                wc2.UploadStringCompleted += (sender3, e3) =>
                {
                    if (e3.Result != null && e3.Error == null && e3.Result != "")
                    {
                        ID = String.Copy(e3.Result);

                        wc.UploadStringCompleted += (sender2, e2) =>
                        {
                            if (e2.Result != null && e2.Error == null && e2.Result != "")
                            {

                                if (!appSettings.Contains("UserName"))
                                {
                                    appSettings.Add("UserName", "");
                                }

                                appSettings["UserName"] = String.Copy(e2.Result);
                                replyNameTextBox.Text = "!" + String.Copy(e2.Result) + "(" + ID.Substring(28, 4) + ")";     //last four chars of live id for this site
                                newNameTextBox.Text = "!" + String.Copy(e2.Result) + "(" + ID.Substring(28, 4) + ")";

                                IS_LIVE_LOGIN = true;
                            }
                        };
                        wc.UploadStringAsync(ub.Uri, e.Result);
                    }
                };
                wc2.UploadStringAsync(ub2.Uri, e.Result);
            }
            else
            {
                //                name = "";
            }
        }

        void displayTreeView_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e != null)
            {
                TreeViewAutomationPeer trvAutomation = (TreeViewAutomationPeer)TreeViewAutomationPeer.CreatePeerForElement(displayTreeView);
                IScrollProvider scrollingAutomationProvider = (IScrollProvider)trvAutomation.GetPattern(PatternInterface.Scroll);

                if (e.Delta < 0 && scrollingAutomationProvider.HorizontallyScrollable && scrollingAutomationProvider.VerticallyScrollable)
                {
                    scrollingAutomationProvider.SetScrollPercent(0, -1);
                    scrollingAutomationProvider.Scroll(ScrollAmount.NoAmount, ScrollAmount.SmallIncrement);
                }
                else if (e.Delta > 0 && scrollingAutomationProvider.HorizontallyScrollable && scrollingAutomationProvider.VerticallyScrollable)
                {
                    scrollingAutomationProvider.SetScrollPercent(0, -1);
                    scrollingAutomationProvider.Scroll(ScrollAmount.NoAmount, ScrollAmount.SmallDecrement);
                }
                else if (e.Delta < 0 && !scrollingAutomationProvider.HorizontallyScrollable && scrollingAutomationProvider.VerticallyScrollable)
                {
                    scrollingAutomationProvider.Scroll(ScrollAmount.NoAmount, ScrollAmount.SmallIncrement);
                }
                else if (e.Delta > 0 && !scrollingAutomationProvider.HorizontallyScrollable && scrollingAutomationProvider.VerticallyScrollable)
                {
                    scrollingAutomationProvider.Scroll(ScrollAmount.NoAmount, ScrollAmount.SmallDecrement);
                }
                else if (scrollingAutomationProvider.HorizontallyScrollable)
                {
                    scrollingAutomationProvider.SetScrollPercent(0, -1);
                }
            }
        }

        void displayTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            string temp = "";
            TreeViewItem sourceItem = new TreeViewItem();

            //            TreeViewAutomationPeer trvAutomation = (TreeViewAutomationPeer)TreeViewAutomationPeer.CreatePeerForElement(displayTreeView);
            //            IScrollProvider scrollingAutomationProvider = (IScrollProvider)trvAutomation.GetPattern(PatternInterface.Scroll);


            try
            {
                sourceItem = (TreeViewItem)e.NewValue;
                if (sourceItem != null)
                {
                    temp = string.Copy((string)(sourceItem.Header));
                    temp = temp.Substring(0, 2).Replace(":", "");
                    settingComboBox.SelectedItem = Convert.ToInt32(temp);
                    settingTextBlock.Text = string.Copy(temp);
                }
            }
            catch
            {
                settingComboBox.SelectedIndex = 0;
                settingTextBlock.Text = "新規";
            }
            /*
                        if (scrollingAutomationProvider.HorizontallyScrollable)
                        {
                            scrollingAutomationProvider.SetScrollPercent(0, -1);
                        }
            */
        }

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            switch (mouseHeadingNum)
            {
                case 1:
                    if (bulliten.Heading1.Length != 0) { bulliten.Heading1 = bulliten.Heading1.Substring(1, bulliten.Heading1.Length - 1); }
                    else { bulliten.Heading1 = BullitenFile.CreateHeading((int)(1 + currentTopPosition), file).Replace(":@", ""); }
                    break;
                case 2:
                    if (bulliten.Heading2.Length != 0) { bulliten.Heading2 = bulliten.Heading2.Substring(1, bulliten.Heading2.Length - 1); }
                    else { bulliten.Heading2 = BullitenFile.CreateHeading((int)(2 + currentTopPosition), file).Replace(":@", ""); }
                    break;
                case 3:
                    if (bulliten.Heading3.Length != 0) { bulliten.Heading3 = bulliten.Heading3.Substring(1, bulliten.Heading3.Length - 1); }
                    else { bulliten.Heading3 = BullitenFile.CreateHeading((int)(3 + currentTopPosition), file).Replace(":@", ""); }
                    break;
                case 4:
                    if (bulliten.Heading4.Length != 0) { bulliten.Heading4 = bulliten.Heading4.Substring(1, bulliten.Heading4.Length - 1); }
                    else { bulliten.Heading4 = BullitenFile.CreateHeading((int)(4 + currentTopPosition), file).Replace(":@", ""); }
                    break;
                case 5:
                    if (bulliten.Heading5.Length != 0) { bulliten.Heading5 = bulliten.Heading5.Substring(1, bulliten.Heading5.Length - 1); }
                    else { bulliten.Heading5 = BullitenFile.CreateHeading((int)(5 + currentTopPosition), file).Replace(":@", ""); }
                    break;
                case 6:
                    if (bulliten.Heading6.Length != 0) { bulliten.Heading6 = bulliten.Heading6.Substring(1, bulliten.Heading6.Length - 1); }
                    else { bulliten.Heading6 = BullitenFile.CreateHeading((int)(6 + currentTopPosition), file).Replace(":@", ""); }
                    break;
                case 7:
                    if (bulliten.Heading7.Length != 0) { bulliten.Heading7 = bulliten.Heading7.Substring(1, bulliten.Heading7.Length - 1); }
                    else { bulliten.Heading7 = BullitenFile.CreateHeading((int)(7 + currentTopPosition), file).Replace(":@", ""); }
                    break;
                case 8:
                    if (bulliten.Heading8.Length != 0) { bulliten.Heading8 = bulliten.Heading8.Substring(1, bulliten.Heading8.Length - 1); }
                    else { bulliten.Heading8 = BullitenFile.CreateHeading((int)(8 + currentTopPosition), file).Replace(":@", ""); }
                    break;
                case 9:
                    if (bulliten.Heading9.Length != 0) { bulliten.Heading9 = bulliten.Heading9.Substring(1, bulliten.Heading9.Length - 1); }
                    else { bulliten.Heading9 = BullitenFile.CreateHeading((int)(9 + currentTopPosition), file).Replace(":@", ""); }
                    break;
                case 10:
                    if (bulliten.Heading10.Length != 0) { bulliten.Heading10 = bulliten.Heading10.Substring(1, bulliten.Heading10.Length - 1); }
                    else { bulliten.Heading10 = BullitenFile.CreateHeading((int)(10 + currentTopPosition), file).Replace(":@", ""); }
                    break;
                case 11:
                    if (bulliten.Heading11.Length != 0) { bulliten.Heading11 = bulliten.Heading11.Substring(1, bulliten.Heading11.Length - 1); }
                    else { bulliten.Heading11 = BullitenFile.CreateHeading((int)(11 + currentTopPosition), file).Replace(":@", ""); }
                    break;
                case 12:
                    if (bulliten.Heading12.Length != 0) { bulliten.Heading12 = bulliten.Heading12.Substring(1, bulliten.Heading12.Length - 1); }
                    else { bulliten.Heading12 = BullitenFile.CreateHeading((int)(12 + currentTopPosition), file).Replace(":@", ""); }
                    break;
                case 13:
                    if (bulliten.Heading13.Length != 0) { bulliten.Heading13 = bulliten.Heading13.Substring(1, bulliten.Heading13.Length - 1); }
                    else { bulliten.Heading13 = BullitenFile.CreateHeading((int)(13 + currentTopPosition), file).Replace(":@", ""); }
                    break;
                case 14:
                    if (bulliten.Heading14.Length != 0) { bulliten.Heading14 = bulliten.Heading14.Substring(1, bulliten.Heading14.Length - 1); }
                    else { bulliten.Heading14 = BullitenFile.CreateHeading((int)(14 + currentTopPosition), file).Replace(":@", ""); }
                    break;
                case 15:
                    if (bulliten.Heading15.Length != 0) { bulliten.Heading15 = bulliten.Heading15.Substring(1, bulliten.Heading15.Length - 1); }
                    else { bulliten.Heading15 = BullitenFile.CreateHeading((int)(15 + currentTopPosition), file).Replace(":@", ""); }
                    break;
                case 16:
                    if (bulliten.Heading16.Length != 0) { bulliten.Heading16 = bulliten.Heading16.Substring(1, bulliten.Heading16.Length - 1); }
                    else { bulliten.Heading16 = BullitenFile.CreateHeading((int)(16 + currentTopPosition), file).Replace(":@", ""); }
                    break;
                case 17:
                    if (bulliten.Heading17.Length != 0) { bulliten.Heading17 = bulliten.Heading17.Substring(1, bulliten.Heading17.Length - 1); }
                    else { bulliten.Heading17 = BullitenFile.CreateHeading((int)(17 + currentTopPosition), file).Replace(":@", ""); }
                    break;
            };
        }
        /*
                private void text1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
                {
                    newCanvas.Visibility = Visibility.Visible;
                    newCanvasLayer.Visibility = Visibility.Visible;
                }
        */
        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            string time = "";
            string saveText = "";

            if (!String.IsNullOrEmpty(inputTextBox.Text) && !String.IsNullOrEmpty(file))
            {
                UriBuilder ub = new UriBuilder("http://fushigispace.com/faq/Bulliten/BullitenCommentHandler.ashx");
                WebClient c = new WebClient();

                c.UploadStringCompleted += (sender3, e3) =>
                {
                    /*                //re-initialize main view
                                    UriBuilder ub2 = new UriBuilder("http://fushigispace.com/faq/Bulliten/BullitenHandler.ashx");
                                    WebClient c2 = new WebClient();

                                    c2.UploadStringCompleted += (sender2, e2) =>
                                    {
                                        file = String.Copy(e2.Result);

                                        bulliten.Heading1 = BullitenFile.CreateHeading(1, file).Replace(":@", "");
                                        bulliten.Heading2 = BullitenFile.CreateHeading(2, file).Replace(":@", "");
                                        bulliten.Heading3 = BullitenFile.CreateHeading(3, file).Replace(":@", "");
                                        bulliten.Heading4 = BullitenFile.CreateHeading(4, file).Replace(":@", "");
                                        bulliten.Heading5 = BullitenFile.CreateHeading(5, file).Replace(":@", "");
                                        bulliten.Heading6 = BullitenFile.CreateHeading(6, file).Replace(":@", "");
                                        bulliten.Heading7 = BullitenFile.CreateHeading(7, file).Replace(":@", "");
                                        bulliten.Heading8 = BullitenFile.CreateHeading(8, file).Replace(":@", "");
                                        bulliten.Heading9 = BullitenFile.CreateHeading(9, file).Replace(":@", "");
                                        bulliten.Heading10 = BullitenFile.CreateHeading(10, file).Replace(":@", "");
                                        bulliten.Heading11 = BullitenFile.CreateHeading(11, file).Replace(":@", "");
                                        bulliten.Heading12 = BullitenFile.CreateHeading(12, file).Replace(":@", "");
                                        bulliten.Heading13 = BullitenFile.CreateHeading(13, file).Replace(":@", "");
                                        bulliten.Heading14 = BullitenFile.CreateHeading(14, file).Replace(":@", "");
                                        bulliten.Heading15 = BullitenFile.CreateHeading(15, file).Replace(":@", "");
                                        bulliten.Heading16 = BullitenFile.CreateHeading(16, file).Replace(":@", "");
                                        bulliten.Heading17 = BullitenFile.CreateHeading(17, file).Replace(":@", "");

                                        mainScrollBar.Value = 0;
                                        currentTopPosition = 0;

                                        if (numberOfPosts <= 17)
                                        {
                                            mainScrollBar.Maximum = 0;
                                        }
                                        else
                                        {
                                            mainScrollBar.Maximum = 1;
                                            tempDouble = ((double)17 / (double)numberOfPosts) * (255 / numberOfPosts);
                                            mainScrollBar.ViewportSize = tempDouble;
                                        }
                                    };
                                    c2.UploadStringAsync(ub2.Uri, "");
                    */
                    if (e3.Error == null)
                    {
                        inputTextBox.Text = "";
                        displayCanvas.Visibility = Visibility.Collapsed;
                        mainBoard.Visibility = Visibility.Visible;
                        errorTextBlock.Visibility = Visibility.Collapsed;
                        Button_Click_1(null, null);
                    }
                    else
                    {
                        errorTextBlock.Visibility = Visibility.Visible;
                    }

                    sendButton.IsEnabled = true;
                };

                if (!IS_LIVE_LOGIN)
                {
                    replyNameTextBox.Text = replyNameTextBox.Text.Replace("!", "");
                }

                //remove all delimiters and newline chars
                replyNameTextBox.Text = replyNameTextBox.Text.Replace(":@", "");
                replyNameTextBox.Text = replyNameTextBox.Text.Replace(";@", "");
                replyNameTextBox.Text = replyNameTextBox.Text.Replace(";@;", "");
                replyNameTextBox.Text = replyNameTextBox.Text.Replace("\r\n", "");
                replyNameTextBox.Text = replyNameTextBox.Text.Replace(" ", "_");
                replyNameTextBox.Text = replyNameTextBox.Text.Replace("　", "_");
                inputTextBox.Text = inputTextBox.Text.Replace(":@", "");
                inputTextBox.Text = inputTextBox.Text.Replace(";@", "");
                inputTextBox.Text = inputTextBox.Text.Replace(";@;", "");
                inputTextBox.Text = inputTextBox.Text.Replace("-@-", "");
                inputTextBox.Text = inputTextBox.Text.Replace("\r\n", "");
                inputTextBox.Text = inputTextBox.Text.Replace("\r", "-@-");  //newline delimiter
                inputTextBox.Text = inputTextBox.Text.Replace(" ---- ", "");

                if (appSettings.Contains("UserName") && !IS_LIVE_LOGIN)
                {
                    appSettings["UserName"] = replyNameTextBox.Text;
                }

                time = DateTime.Now.ToString();

                if (inputTextBox.Text.Length < 10)
                {
                    saveText = inputTextBox.Text;
                }
                else
                {
                    saveText = inputTextBox.Text.Substring(0, 9);
                }

                if (BullitenFile.IsCommentHack(time, saveText, file))  //figure out if saving timestamp and text is safe
                {
                    errorTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    appSettings.Add(time, saveText);

                    c.UploadStringAsync(ub.Uri, Convert.ToString(currentPost) + ":@" + settingComboBox.SelectedIndex.ToString() + ";@" + "名前：" + replyNameTextBox.Text + " " + time + " ---- " + inputTextBox.Text);
                    sendButton.IsEnabled = false;

                    //displayCanvas.Visibility = Visibility.Collapsed;
                    //mainBoard.Visibility = Visibility.Visible;
                    //errorTextBlock.Visibility = Visibility.Collapsed;
                }
            }
            else if (String.IsNullOrEmpty(file))
            {
                Button_Click_1(null, null);
                errorTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {
            //            string content = BullitenFile.GetContent(currentPost, file);
            //            string comments = "";

            /*
                        settingComboBox.Items.Clear();  //rebuild each time
            
                        //skip the main content and delimit
                        content = content.Remove(0, 2);
                        content = content.Remove(0, content.IndexOf("\r\n"));
                        comments = content + ":@;@";

                        //go through the comments and find out the number of comments
                        for (int x = 0; comments.IndexOf(";@") != 0 && x < 255; x++)
                        {
                            comments = comments.Remove(0, comments.IndexOf(":@") + 2);

                            settingComboBox.Items.Add(Convert.ToString(x));
                        }
            */
            //settingComboBox.SelectedIndex = 0;

            errorTextBlock.Visibility = Visibility.Collapsed;
            adminCanvas.Visibility = Visibility.Collapsed;
            noDeleteTextBlock.Visibility = Visibility.Collapsed;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (BullitenFile.GetContent(headingNum, file).Contains(":@" + Convert.ToString(settingComboBox.SelectedItem)) || settingComboBox.SelectedIndex == 0)
            {
                //do nothing
            }
            else
            {
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            displayCanvas.Visibility = Visibility.Collapsed;
            adminCanvas.Visibility = Visibility.Collapsed;
            noDeleteTextBlock.Visibility = Visibility.Collapsed;
            errorTextBlock.Visibility = Visibility.Collapsed;

            mainBoard.Visibility = Visibility.Visible;
        }

        private void newCancelButton_Click(object sender, RoutedEventArgs e)
        {
            newCanvas.Visibility = Visibility.Collapsed;
            newCanvasLayer.Visibility = Visibility.Collapsed;
            newErrorTextBlock.Visibility = Visibility.Collapsed;
        }

        private void newSendButton_Click(object sender, RoutedEventArgs e)
        {
            string time = "";
            string saveText = "";
            bool IS_ERROR = false;

            if (!String.IsNullOrEmpty(newPostTextBox.Text) && !String.IsNullOrEmpty(file))
            {
                //            BullitenFile.AddPost(newTitleTextBox.Text, newNameTextBox.Text, newPostTextBox.Text, bulliten, file);


                if (!IS_LIVE_LOGIN)
                {
                    newNameTextBox.Text = newNameTextBox.Text.Replace("!", "");
                }

                //replace all delimiters and create a newline delimiter (do not change the order of replacement)
                newNameTextBox.Text = newNameTextBox.Text.Replace(":@", "");
                newNameTextBox.Text = newNameTextBox.Text.Replace(";@", "");
                newNameTextBox.Text = newNameTextBox.Text.Replace(";@;", "");
                newNameTextBox.Text = newNameTextBox.Text.Replace("\r\n", "");
                newNameTextBox.Text = newNameTextBox.Text.Replace(" ", "_");
                newNameTextBox.Text = newNameTextBox.Text.Replace("　", "_");
                newPostTextBox.Text = newPostTextBox.Text.Replace(":@", "");
                newPostTextBox.Text = newPostTextBox.Text.Replace(";@", "");
                newPostTextBox.Text = newPostTextBox.Text.Replace(";@;", "");
                newPostTextBox.Text = newPostTextBox.Text.Replace("-@-", "");
                newPostTextBox.Text = newPostTextBox.Text.Replace("\r\n", "");
                newPostTextBox.Text = newPostTextBox.Text.Replace("\r", "-@-");  //newline delimiter
                newTitleTextBox.Text = newTitleTextBox.Text.Replace(":@", "");
                newTitleTextBox.Text = newTitleTextBox.Text.Replace(";@", "");
                newTitleTextBox.Text = newTitleTextBox.Text.Replace(";@;", "");
                newTitleTextBox.Text = newTitleTextBox.Text.Replace("\r\n", "");

                if (appSettings.Contains("UserName") && !IS_LIVE_LOGIN)
                {
                    appSettings["UserName"] = newNameTextBox.Text;
                }


                time = DateTime.Now.ToString();

                if (newPostTextBox.Text.Length < 10)
                {
                    saveText = newPostTextBox.Text;
                }
                else
                {
                    saveText = newPostTextBox.Text.Substring(0, 9);
                }

                if (BullitenFile.IsPostHack(time, saveText, file))  //figure out if saving timestamp and text is safe
                {
                    newErrorTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    appSettings.Add(time, saveText);

                    UriBuilder ub = new UriBuilder("http://fushigispace.com/faq/Bulliten/BullitenWriteHandler.ashx");
                    WebClient c = new WebClient();

                    c.UploadStringCompleted += (sender3, e3) =>
                    {
                        if (e3.Error == null)
                        {
                            //re-initialize main view
                            UriBuilder ub2 = new UriBuilder("http://fushigispace.com/faq/Bulliten/BullitenHandler.ashx");
                            WebClient c2 = new WebClient();

                            c2.UploadStringCompleted += (sender2, e2) =>
                            {
                                if (e2.Error == null)
                                {
                                    file = String.Copy(e2.Result);

                                    bulliten.Heading1 = BullitenFile.CreateHeading(1, file).Replace(":@", "");
                                    bulliten.Heading2 = BullitenFile.CreateHeading(2, file).Replace(":@", "");
                                    bulliten.Heading3 = BullitenFile.CreateHeading(3, file).Replace(":@", "");
                                    bulliten.Heading4 = BullitenFile.CreateHeading(4, file).Replace(":@", "");
                                    bulliten.Heading5 = BullitenFile.CreateHeading(5, file).Replace(":@", "");
                                    bulliten.Heading6 = BullitenFile.CreateHeading(6, file).Replace(":@", "");
                                    bulliten.Heading7 = BullitenFile.CreateHeading(7, file).Replace(":@", "");
                                    bulliten.Heading8 = BullitenFile.CreateHeading(8, file).Replace(":@", "");
                                    bulliten.Heading9 = BullitenFile.CreateHeading(9, file).Replace(":@", "");
                                    bulliten.Heading10 = BullitenFile.CreateHeading(10, file).Replace(":@", "");
                                    bulliten.Heading11 = BullitenFile.CreateHeading(11, file).Replace(":@", "");
                                    bulliten.Heading12 = BullitenFile.CreateHeading(12, file).Replace(":@", "");
                                    bulliten.Heading13 = BullitenFile.CreateHeading(13, file).Replace(":@", "");
                                    bulliten.Heading14 = BullitenFile.CreateHeading(14, file).Replace(":@", "");
                                    bulliten.Heading15 = BullitenFile.CreateHeading(15, file).Replace(":@", "");
                                    bulliten.Heading16 = BullitenFile.CreateHeading(16, file).Replace(":@", "");
                                    bulliten.Heading17 = BullitenFile.CreateHeading(17, file).Replace(":@", "");

                                    mainScrollBar.Value = 0;
                                    currentTopPosition = 0;

                                    if (numberOfPosts < 255)  //max number of posts is 255
                                    {
                                        numberOfPosts++;
                                    }
                                    else
                                    {
                                        numberOfPosts = 128;  //manualy set value
                                    }

                                    if (numberOfPosts <= 17)
                                    {
                                        mainScrollBar.Maximum = 0;
                                    }
                                    else
                                    {
                                        mainScrollBar.Maximum = 1;
                                        tempDouble = ((double)17 / (double)numberOfPosts) * (255 / numberOfPosts);
                                        mainScrollBar.ViewportSize = tempDouble;
                                    }

                                    newPostTextBox.Text = "";
                                }
                                else
                                {
                                    loadingText.Text = "最新ではありません。";
                                    loadingCanvas.Visibility = Visibility.Visible;
                                }
                            };
                            c2.UploadStringAsync(ub2.Uri, "");

                            newCanvas.Visibility = Visibility.Collapsed;
                            newCanvasLayer.Visibility = Visibility.Collapsed;
                            adminCanvas.Visibility = Visibility.Collapsed;
                            noDeleteTextBlock.Visibility = Visibility.Collapsed;
                            newErrorTextBlock.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            newErrorTextBlock.Visibility = Visibility.Visible;
                        }

                        newSendButton.IsEnabled = true;
                    };
                    c.UploadStringAsync(ub.Uri, newTitleTextBox.Text + ":@" + newNameTextBox.Text + ":@" + newPostTextBox.Text + ":@" + time + ":@" + ";@");

                    newSendButton.IsEnabled = false;

                    //newCanvas.Visibility = Visibility.Collapsed;
                    //newCanvasLayer.Visibility = Visibility.Collapsed;
                    //adminCanvas.Visibility = Visibility.Collapsed;
                    //noDeleteTextBlock.Visibility = Visibility.Collapsed;
                    //newErrorTextBlock.Visibility = Visibility.Collapsed;
                }
            }
            else if (String.IsNullOrEmpty(file))
            {
                Button_Click_1(null, null);
                newErrorTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void mainScrollBar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            currentTopPosition = (int)((numberOfPosts - 17) * e.NewValue);

            bulliten.Heading1 = BullitenFile.CreateHeading((int)(1 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading2 = BullitenFile.CreateHeading((int)(2 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading3 = BullitenFile.CreateHeading((int)(3 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading4 = BullitenFile.CreateHeading((int)(4 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading5 = BullitenFile.CreateHeading((int)(5 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading6 = BullitenFile.CreateHeading((int)(6 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading7 = BullitenFile.CreateHeading((int)(7 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading8 = BullitenFile.CreateHeading((int)(8 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading9 = BullitenFile.CreateHeading((int)(9 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading10 = BullitenFile.CreateHeading((int)(10 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading11 = BullitenFile.CreateHeading((int)(11 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading12 = BullitenFile.CreateHeading((int)(12 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading13 = BullitenFile.CreateHeading((int)(13 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading14 = BullitenFile.CreateHeading((int)(14 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading15 = BullitenFile.CreateHeading((int)(15 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading16 = BullitenFile.CreateHeading((int)(16 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading17 = BullitenFile.CreateHeading((int)(17 + currentTopPosition), file).Replace(":@", "");
            /*
                        bulliten.Tooltip1 = "名前：\r\n" + BullitenFile.GetName(1 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(1 + currentTopPosition, file);
                        bulliten.Tooltip2 = "名前：\r\n" + BullitenFile.GetName(2 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(2 + currentTopPosition, file);
                        bulliten.Tooltip3 = "名前：\r\n" + BullitenFile.GetName(3 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(3 + currentTopPosition, file);
                        bulliten.Tooltip4 = "名前：\r\n" + BullitenFile.GetName(4 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(4 + currentTopPosition, file);
                        bulliten.Tooltip5 = "名前：\r\n" + BullitenFile.GetName(5 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(5 + currentTopPosition, file);
                        bulliten.Tooltip6 = "名前：\r\n" + BullitenFile.GetName(6 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(6 + currentTopPosition, file);
                        bulliten.Tooltip7 = "名前：\r\n" + BullitenFile.GetName(7 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(7 + currentTopPosition, file);
                        bulliten.Tooltip8 = "名前：\r\n" + BullitenFile.GetName(8 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(8 + currentTopPosition, file);
                        bulliten.Tooltip9 = "名前：\r\n" + BullitenFile.GetName(9 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(9 + currentTopPosition, file);
                        bulliten.Tooltip10 = "名前：\r\n" + BullitenFile.GetName(10 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(10 + currentTopPosition, file);
                        bulliten.Tooltip11 = "名前：\r\n" + BullitenFile.GetName(11 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(11 + currentTopPosition, file);
                        bulliten.Tooltip12 = "名前：\r\n" + BullitenFile.GetName(12 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(12 + currentTopPosition, file);
                        bulliten.Tooltip13 = "名前：\r\n" + BullitenFile.GetName(13 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(13 + currentTopPosition, file);
                        bulliten.Tooltip14 = "名前：\r\n" + BullitenFile.GetName(14 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(14 + currentTopPosition, file);
                        bulliten.Tooltip15 = "名前：\r\n" + BullitenFile.GetName(15 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(15 + currentTopPosition, file);
                        bulliten.Tooltip16 = "名前：\r\n" + BullitenFile.GetName(16 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(16 + currentTopPosition, file);
                        bulliten.Tooltip17 = "名前：\r\n" + BullitenFile.GetName(17 + currentTopPosition, file) + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(17 + currentTopPosition, file);
             */
        }
        /*
                private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
                {
                    //re-initialize main view
                    UriBuilder ub = new UriBuilder("http://fushigispace.com/faq/Bulliten/BullitenHandler.ashx");
                    WebClient c = new WebClient();

                    c.UploadStringCompleted += (sender2, e2) =>
                    {
                        file = String.Copy(e2.Result);
                        bulliten.Heading1 = BullitenFile.CreateHeading(1, file);
                        bulliten.Heading2 = BullitenFile.CreateHeading(2, file);
                        bulliten.Heading3 = BullitenFile.CreateHeading(3, file);
                        bulliten.Heading4 = BullitenFile.CreateHeading(4, file);
                        bulliten.Heading5 = BullitenFile.CreateHeading(5, file);
                        bulliten.Heading6 = BullitenFile.CreateHeading(6, file);
                        bulliten.Heading7 = BullitenFile.CreateHeading(7, file);
                        bulliten.Heading8 = BullitenFile.CreateHeading(8, file);
                        bulliten.Heading9 = BullitenFile.CreateHeading(9, file);
                        bulliten.Heading10 = BullitenFile.CreateHeading(10, file);
                        bulliten.Heading11 = BullitenFile.CreateHeading(11, file);
                        bulliten.Heading12 = BullitenFile.CreateHeading(12, file);
                        bulliten.Heading13 = BullitenFile.CreateHeading(13, file);
                        bulliten.Heading14 = BullitenFile.CreateHeading(14, file);
                        bulliten.Heading15 = BullitenFile.CreateHeading(15, file);
                        bulliten.Heading16 = BullitenFile.CreateHeading(16, file);
                        bulliten.Heading17 = BullitenFile.CreateHeading(17, file);
        /*
                        bulliten.Tooltip1 = "タイトル：\r\n" + BullitenFile.GetTitle(1, file) + "\r\n名前：\r\n" + BullitenFile.GetName(1, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(1, file);
                        bulliten.Tooltip2 = "タイトル：\r\n" + BullitenFile.GetTitle(2, file) + "\r\n名前：\r\n" + BullitenFile.GetName(2, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(2, file);
                        bulliten.Tooltip3 = "タイトル：\r\n" + BullitenFile.GetTitle(3, file) + "\r\n名前：\r\n" + BullitenFile.GetName(3, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(3, file);
                        bulliten.Tooltip4 = "タイトル：\r\n" + BullitenFile.GetTitle(4, file) + "\r\n名前：\r\n" + BullitenFile.GetName(4, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(4, file);
                        bulliten.Tooltip5 = "タイトル：\r\n" + BullitenFile.GetTitle(5, file) + "\r\n名前：\r\n" + BullitenFile.GetName(5, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(5, file);
                        bulliten.Tooltip6 = "タイトル：\r\n" + BullitenFile.GetTitle(6, file) + "\r\n名前：\r\n" + BullitenFile.GetName(6, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(6, file);
                        bulliten.Tooltip7 = "タイトル：\r\n" + BullitenFile.GetTitle(7, file) + "\r\n名前：\r\n" + BullitenFile.GetName(7, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(7, file);
                        bulliten.Tooltip8 = "タイトル：\r\n" + BullitenFile.GetTitle(8, file) + "\r\n名前：\r\n" + BullitenFile.GetName(8, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(8, file);
                        bulliten.Tooltip9 = "タイトル：\r\n" + BullitenFile.GetTitle(9, file) + "\r\n名前：\r\n" + BullitenFile.GetName(9, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(9, file);
                        bulliten.Tooltip10 = "タイトル：\r\n" + BullitenFile.GetTitle(10, file) + "\r\n名前：\r\n" + BullitenFile.GetName(10, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(10, file);
                        bulliten.Tooltip11 = "タイトル：\r\n" + BullitenFile.GetTitle(11, file) + "\r\n名前：\r\n" + BullitenFile.GetName(11, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(11, file);
                        bulliten.Tooltip12 = "タイトル：\r\n" + BullitenFile.GetTitle(12, file) + "\r\n名前：\r\n" + BullitenFile.GetName(12, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(12, file);
                        bulliten.Tooltip13 = "タイトル：\r\n" + BullitenFile.GetTitle(13, file) + "\r\n名前：\r\n" + BullitenFile.GetName(13, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(13, file);
                        bulliten.Tooltip14 = "タイトル：\r\n" + BullitenFile.GetTitle(14, file) + "\r\n名前：\r\n" + BullitenFile.GetName(14, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(14, file);
                        bulliten.Tooltip15 = "タイトル：\r\n" + BullitenFile.GetTitle(15, file) + "\r\n名前：\r\n" + BullitenFile.GetName(15, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(15, file);
                        bulliten.Tooltip16 = "タイトル：\r\n" + BullitenFile.GetTitle(16, file) + "\r\n名前：\r\n" + BullitenFile.GetName(16, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(16, file);
                        bulliten.Tooltip17 = "タイトル：\r\n" + BullitenFile.GetTitle(17, file) + "\r\n名前：\r\n" + BullitenFile.GetName(17, file)
                            + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(17, file);
 

                    };
                    c.UploadStringAsync(ub.Uri, "");

                    //re-initialize scroll bar
                    UriBuilder ub2 = new UriBuilder("http://fushigispace.com/faq/Bulliten/BullitenLengthHandler.ashx");
                    WebClient c2 = new WebClient();

                    c2.UploadStringCompleted += (sender2, e2) =>
                    {
                        numberOfPosts = Convert.ToUInt16(String.Copy(e2.Result));
                    };
                    c2.UploadStringAsync(ub2.Uri, "");

                    mainScrollBar.Value = 0;
                    currentTopPosition = 0;
 
                  }
         */
        private void newEraceButton_Click(object sender, RoutedEventArgs e)
        {
            newNameTextBox.Text = "";
            newTitleTextBox.Text = "";
            newPostTextBox.Text = "";
        }

        void newItem_Expanded(object sender, RoutedEventArgs e)
        {
            string comments = BullitenFile.GetComments(headingNum, file);
            int count = 0;
            int currentCommentNum = 0; ;
            string temp = "";
            int numberOfComments = 1;
            int nextCommentNum = 0;
            int lastTreeNum = 65;  //total number of comments
            string header = "";
            int headerNum = 0;

            TreeViewItem senderItem = (TreeViewItem)sender;
            header = (string)senderItem.Header;

            headerNum = Convert.ToInt32(header.Substring(0, 2).Replace(":", ""));

            comments = comments.Remove(0, comments.IndexOf(header) + header.Length + 1);  //ignore comments that are not relavant

            if (comments.Length <= 1 || !comments.Contains("@:@")) //end of comments
            {
                senderItem.Items.Clear();
                return;
            }
            comments = comments.Replace("\r", " ");
            temp = comments.Substring(3, comments.IndexOf("@:@") + 2);
            temp = temp.Replace(":", "");
            temp = temp.Replace("@", "");
            currentCommentNum = Convert.ToInt32(temp);

            temp = comments;

            while (temp.IndexOf(":@:@") != -1)
            {
                numberOfComments++;
                temp = temp.Remove(0, temp.IndexOf(":@:@") + 1);
            }

            TreeViewItem expanded = (TreeViewItem)sender;
            expanded.Items.Clear();

            while (count < numberOfComments)
            {
                if (headerNum - currentCommentNum > 0)
                {
                    return;
                }

                if (count + 1 == numberOfComments)  //last comment
                {
                    nextCommentNum = 0;
                }
                else
                {
                    try
                    {
                        temp = comments.Substring(comments.IndexOf("@:@", comments.IndexOf("@:@" + currentCommentNum.ToString() + ":") + 1), 5).Replace(":", "");
                        temp = temp.Replace("@", "");
                        nextCommentNum = Convert.ToInt32(temp);
                    }
                    catch
                    {
                        nextCommentNum = 0;
                    }
                }

                if (headerNum < currentCommentNum && lastTreeNum > currentCommentNum)
                {
                    TreeViewItem newItem = new TreeViewItem() { Header = BullitenFile.GetComment(headingNum, file, currentCommentNum).Replace("\r", " ") };

                    newItem.Items.Add(new TreeViewItem());

                    if (nextCommentNum < currentCommentNum)
                    {
                        newItem.Items.Clear();
                    }

                    newItem.Expanded += new RoutedEventHandler(newItem_Expanded);

                    newItem.Selected += new RoutedEventHandler(newItem_Selected);

                    expanded.Items.Add(newItem);

                    lastTreeNum = currentCommentNum;
                }

                currentCommentNum = nextCommentNum;
                count++;
            }
        }

        void newItem_Selected(object sender, RoutedEventArgs e)
        {
            //            string temp = "";

            if (sender != null)
            {
                //                temp = (string)((TreeViewItem)sender).Header;
                //                inputTextBox.Text = temp;
            }
        }

        private void initializeTreeView(int headingNum)
        {
            string comments = BullitenFile.GetComments(headingNum, file);
            int count = 0;
            int currentCommentNum = 0; ;
            string temp = "";
            int numberOfComments = 1;
            int nextCommentNum = 0;
            int lastTreeNum = 64;  //total number of comments
            TextBlock postTextBlock = new TextBlock();

            settingComboBox.SelectedIndex = 0;
            settingTextBlock.Text = "新規";

            displayTreeView.Items.Clear();

            temp = BullitenFile.GetContent(headingNum, file);
            temp = temp.Substring(0, temp.IndexOf("\r\n"));
            temp = temp.Replace(":@", "");
            //temp += "\r\n";

            postTextBlock.TextWrapping = TextWrapping.Wrap;
            postTextBlock.Width = 430;
            postTextBlock.Text = temp;

            displayTreeView.Items.Add(postTextBlock);

            if (comments == "")
            {
                return;
            }

            currentCommentNum = Convert.ToInt32(comments.Substring(0, comments.IndexOf(":")));

            temp = comments;

            while (temp.IndexOf(":@:@") != -1)
            {
                numberOfComments++;
                temp = temp.Remove(0, temp.IndexOf(":@:@") + 1);
            }

            while (count < numberOfComments)
            {
                if (count + 1 == numberOfComments)  //last comment
                {
                    nextCommentNum = 0;
                }
                else
                {
                    try
                    {
                        temp = comments.Substring(comments.IndexOf("@:@", comments.IndexOf("@:@" + currentCommentNum.ToString() + ":") + 1), 5).Replace(":", "");
                        temp = temp.Replace("@", "");
                        nextCommentNum = Convert.ToInt32(temp);
                    }
                    catch
                    {
                        nextCommentNum = 0;
                    }
                }

                if (lastTreeNum > currentCommentNum)
                {
                    TreeViewItem newItem = new TreeViewItem() { Header = BullitenFile.GetComment(headingNum, file, currentCommentNum).Replace("\r", " ") };

                    newItem.Items.Add(new TreeViewItem());

                    if (nextCommentNum < currentCommentNum)
                    {
                        newItem.Items.Clear();
                    }

                    newItem.Expanded += new RoutedEventHandler(newItem_Expanded);

                    newItem.Selected += new RoutedEventHandler(newItem_Selected);

                    displayTreeView.Items.Add(newItem);

                    lastTreeNum = currentCommentNum;

                }

                currentCommentNum = nextCommentNum;
                count++;
            }
        }

        private void text2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 1, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 1;
                initializeTreeView(currentTopPosition + 1);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 1, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 1, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 1, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 1, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 1, file).Replace(":@", "");

                currentPost = currentTopPosition + 1;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition + 1 < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 2, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 2;
                initializeTreeView(currentTopPosition + 2);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 2, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 2, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 2, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 2, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 2, file).Replace(":@", "");

                currentPost = currentTopPosition + 2;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition + 2 < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 3, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 3;
                initializeTreeView(currentTopPosition + 3);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 3, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 3, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 3, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 3, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 3, file).Replace(":@", "");

                currentPost = currentTopPosition + 3;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition + 3 < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 4, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 4;
                initializeTreeView(currentTopPosition + 4);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 4, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 4, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 4, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 4, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 4, file).Replace(":@", "");

                currentPost = currentTopPosition + 4;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition + 4 < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 5, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 5;
                initializeTreeView(currentTopPosition + 5);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 5, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 5, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 5, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 5, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 5, file).Replace(":@", "");

                currentPost = currentTopPosition + 5;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition + 5 < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 6, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 6;
                initializeTreeView(currentTopPosition + 6);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 6, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 6, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 6, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 6, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 6, file).Replace(":@", "");

                currentPost = currentTopPosition + 6;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition + 6 < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 7, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 7;
                initializeTreeView(currentTopPosition + 7);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 7, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 7, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 7, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 7, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 7, file).Replace(":@", "");

                currentPost = currentTopPosition + 7;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text9_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition + 7 < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 8, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 8;
                initializeTreeView(currentTopPosition + 8);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 8, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 8, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 8, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 8, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 8, file).Replace(":@", "");

                currentPost = currentTopPosition + 8;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition + 8 < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 9, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 9;
                initializeTreeView(currentTopPosition + 9);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 9, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 9, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 9, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 9, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 9, file).Replace(":@", "");

                currentPost = currentTopPosition + 9;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text11_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition + 9 < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 10, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 10;
                initializeTreeView(currentTopPosition + 10);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 10, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 10, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 10, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 10, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 10, file).Replace(":@", "");

                currentPost = currentTopPosition + 10;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text12_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition + 10 < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 11, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 11;
                initializeTreeView(currentTopPosition + 11);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 11, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 11, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 11, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 11, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 11, file).Replace(":@", "");

                currentPost = currentTopPosition + 11;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text13_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition + 11 < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 12, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 12;
                initializeTreeView(currentTopPosition + 12);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 12, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 12, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 12, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 12, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 12, file).Replace(":@", "");

                currentPost = currentTopPosition + 12;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text14_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition + 12 < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 13, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 13;
                initializeTreeView(currentTopPosition + 13);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 13, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 13, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 13, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 13, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 13, file).Replace(":@", "");

                currentPost = currentTopPosition + 13;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text15_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition + 13 < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 14, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 14;
                initializeTreeView(currentTopPosition + 14);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 14, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 14, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 14, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 14, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 14, file).Replace(":@", "");

                currentPost = currentTopPosition + 14;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text16_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition + 14 < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 15, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 15;
                initializeTreeView(currentTopPosition + 15);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 15, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 15, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 15, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 15, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 15, file).Replace(":@", "");

                currentPost = currentTopPosition + 15;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text17_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition + 15 < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 16, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 16;
                initializeTreeView(currentTopPosition + 16);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 16, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 16, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 16, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 16, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 16, file).Replace(":@", "");

                currentPost = currentTopPosition + 16;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text18_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTopPosition + 16 < numberOfPosts)
            {
                Button_Click_1(null, null);

                if (BullitenFile.GetPost(currentTopPosition + 17, file).Contains(";@;"))
                {
                    cloneCanvas.Visibility = Visibility.Visible;
                    sendButton.Visibility = Visibility.Collapsed;
                    resetButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cloneCanvas.Visibility = Visibility.Collapsed;
                    sendButton.Visibility = Visibility.Visible;
                    resetButton.Visibility = Visibility.Visible;
                }

                headingNum = currentTopPosition + 17;
                initializeTreeView(currentTopPosition + 17);

                titleTextBlock.Text = "タイトル：" + BullitenFile.GetTitle(currentTopPosition + 17, file).Replace(":@", "") + "　名前："
                    + BullitenFile.GetName(currentTopPosition + 17, file).Replace(":@", "") + " 投稿日時："
                    + BullitenFile.GetTime(currentTopPosition + 17, file).Replace(":@", "");

                bulliten.ViewToolTip = "名前：\r\n" + BullitenFile.GetName(currentTopPosition + 17, file).Replace(":@", "")
                    + "\r\n投稿日時：\r\n" + BullitenFile.GetTime(currentTopPosition + 17, file).Replace(":@", "");

                currentPost = currentTopPosition + 17;

                settingComboBox.SelectedIndex = 0;
                displayCanvas.Visibility = Visibility.Visible;
                mainBoard.Visibility = Visibility.Collapsed;
                settingComboBox.SelectedIndex = 0;
            }
        }

        private void text2_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 1;
            if (bulliten.Heading1.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text3_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 2;
            if (bulliten.Heading2.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text4_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 3;
            if (bulliten.Heading3.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text5_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 4;
            if (bulliten.Heading4.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text6_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 5;
            if (bulliten.Heading5.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text7_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 6;
            if (bulliten.Heading6.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text8_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 7;
            if (bulliten.Heading7.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text9_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 8;
            if (bulliten.Heading8.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text10_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 9;
            if (bulliten.Heading9.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text11_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 10;
            if (bulliten.Heading10.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text12_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 11;
            if (bulliten.Heading11.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text13_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 12;
            if (bulliten.Heading12.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text14_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 13;
            if (bulliten.Heading13.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text15_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 14;
            if (bulliten.Heading14.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text16_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 15;
            if (bulliten.Heading15.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text17_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 16;
            if (bulliten.Heading16.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text18_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseHeadingNum = 17;
            if (bulliten.Heading17.Length > 43)
            {
                scrollTimer.Start();
            }
        }

        private void text2_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading1 = BullitenFile.CreateHeading((int)(1 + currentTopPosition), file).Replace(":@", "");
        }

        private void text3_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading2 = BullitenFile.CreateHeading((int)(2 + currentTopPosition), file).Replace(":@", "");
        }

        private void text4_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading3 = BullitenFile.CreateHeading((int)(3 + currentTopPosition), file).Replace(":@", "");
        }

        private void text5_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading4 = BullitenFile.CreateHeading((int)(4 + currentTopPosition), file).Replace(":@", "");
        }

        private void text6_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading5 = BullitenFile.CreateHeading((int)(5 + currentTopPosition), file).Replace(":@", "");
        }

        private void text7_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading6 = BullitenFile.CreateHeading((int)(6 + currentTopPosition), file).Replace(":@", "");
        }

        private void text8_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading7 = BullitenFile.CreateHeading((int)(7 + currentTopPosition), file).Replace(":@", "");
        }

        private void text9_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading8 = BullitenFile.CreateHeading((int)(8 + currentTopPosition), file).Replace(":@", "");
        }

        private void text10_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading9 = BullitenFile.CreateHeading((int)(9 + currentTopPosition), file).Replace(":@", "");
        }

        private void text11_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading10 = BullitenFile.CreateHeading((int)(10 + currentTopPosition), file).Replace(":@", "");
        }

        private void text12_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading11 = BullitenFile.CreateHeading((int)(11 + currentTopPosition), file).Replace(":@", "");
        }

        private void text13_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading12 = BullitenFile.CreateHeading((int)(12 + currentTopPosition), file).Replace(":@", "");
        }

        private void text14_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading13 = BullitenFile.CreateHeading((int)(13 + currentTopPosition), file).Replace(":@", "");
        }

        private void text15_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading14 = BullitenFile.CreateHeading((int)(14 + currentTopPosition), file).Replace(":@", "");
        }

        private void text16_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading15 = BullitenFile.CreateHeading((int)(15 + currentTopPosition), file).Replace(":@", "");
        }

        private void text17_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading16 = BullitenFile.CreateHeading((int)(16 + currentTopPosition), file).Replace(":@", "");
        }

        private void text18_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollTimer.Stop();
            bulliten.Heading17 = BullitenFile.CreateHeading((int)(17 + currentTopPosition), file).Replace(":@", "");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button_Click_1(null, null);
            newCanvas.Visibility = Visibility.Visible;
            newCanvasLayer.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            loadingCanvas.Visibility = Visibility.Visible;

            //re-initialize main view
            UriBuilder ub = new UriBuilder("http://fushigispace.com/faq/Bulliten/BullitenHandler.ashx");
            WebClient c = new WebClient();

            c.UploadStringCompleted += (sender2, e2) =>
            {
                if (e2.Error == null)
                {
                    file = String.Copy(e2.Result);
                    bulliten.Heading1 = BullitenFile.CreateHeading(1, file).Replace(":@", "");
                    bulliten.Heading2 = BullitenFile.CreateHeading(2, file).Replace(":@", "");
                    bulliten.Heading3 = BullitenFile.CreateHeading(3, file).Replace(":@", "");
                    bulliten.Heading4 = BullitenFile.CreateHeading(4, file).Replace(":@", "");
                    bulliten.Heading5 = BullitenFile.CreateHeading(5, file).Replace(":@", "");
                    bulliten.Heading6 = BullitenFile.CreateHeading(6, file).Replace(":@", "");
                    bulliten.Heading7 = BullitenFile.CreateHeading(7, file).Replace(":@", "");
                    bulliten.Heading8 = BullitenFile.CreateHeading(8, file).Replace(":@", "");
                    bulliten.Heading9 = BullitenFile.CreateHeading(9, file).Replace(":@", "");
                    bulliten.Heading10 = BullitenFile.CreateHeading(10, file).Replace(":@", "");
                    bulliten.Heading11 = BullitenFile.CreateHeading(11, file).Replace(":@", "");
                    bulliten.Heading12 = BullitenFile.CreateHeading(12, file).Replace(":@", "");
                    bulliten.Heading13 = BullitenFile.CreateHeading(13, file).Replace(":@", "");
                    bulliten.Heading14 = BullitenFile.CreateHeading(14, file).Replace(":@", "");
                    bulliten.Heading15 = BullitenFile.CreateHeading(15, file).Replace(":@", "");
                    bulliten.Heading16 = BullitenFile.CreateHeading(16, file).Replace(":@", "");
                    bulliten.Heading17 = BullitenFile.CreateHeading(17, file).Replace(":@", "");

                    //re-initialize scroll bar
                    UriBuilder ub2 = new UriBuilder("http://fushigispace.com/faq/Bulliten/BullitenLengthHandler.ashx");
                    WebClient c2 = new WebClient();

                    c2.UploadStringCompleted += (sender3, e3) =>
                    {
                        if (e3.Error == null)
                        {
                            numberOfPosts = Convert.ToUInt16(String.Copy(e3.Result));

                            if (numberOfPosts <= 17)
                            {
                                mainScrollBar.Maximum = 0;
                            }
                            else
                            {
                                mainScrollBar.Maximum = 1;
                                tempDouble = ((double)17 / (double)numberOfPosts) * (255 / numberOfPosts);
                                mainScrollBar.ViewportSize = tempDouble;
                            }

                            mainScrollBar.Value = 0;
                            currentTopPosition = 0;

                            loadingCanvas.Visibility = Visibility.Collapsed;
                        }
                    };
                    c2.UploadStringAsync(ub2.Uri, "");
                }
            };
            c.UploadStringAsync(ub.Uri, "");

            /*
                        UriBuilder ub3 = new UriBuilder("http://fushigispace.com/faq/Bulliten/CommentDeleteHandler.ashx");
                        WebClient c3 = new WebClient();

                        c3.UploadStringCompleted += (sender2, e2) =>
                        {
                            numberOfPosts = Convert.ToUInt16(String.Copy(e2.Result));

                            displayCanvas.Visibility = Visibility.Collapsed;
                            errorTextBlock.Visibility = Visibility.Collapsed;
                            adminCanvas.Visibility = Visibility.Collapsed;
                            mainBoard.Visibility = Visibility.Visible;

                            Button_Click_1(null, null);
                        };
                        c3.UploadStringAsync(ub3.Uri, Convert.ToString(currentPost) + ":@" + Convert.ToString(adminComboBox.SelectedIndex + 1) + ";@");
            */


        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            inputTextBox.Text = "";
        }

        private void adminButton_Click(object sender, RoutedEventArgs e)
        {
            adminComboBox.SelectedIndex = -1;
            adminCanvas.Visibility = Visibility.Visible;
            noDeleteTextBlock.Visibility = Visibility.Collapsed;
        }

        private void adminCancelButton_Click(object sender, RoutedEventArgs e)
        {
            adminCanvas.Visibility = Visibility.Collapsed;
            noDeleteTextBlock.Visibility = Visibility.Collapsed;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            string commentDate = "";
            string commentText = "";
            string postDate = "";
            string heading = "";
            int index = 0;
            //            bool tempBool = false;

            if (adminComboBox.SelectedIndex == -1) { return; }

            displayCanvas.Visibility = Visibility.Collapsed;

            postDate = BullitenFile.GetTime(currentPost, file).Replace(":@", "");
            heading = BullitenFile.GetHeading(currentPost, file);

            if (adminComboBox.SelectedIndex == 0)  //when trying to delete the post text
            {
                if (appSettings.Contains(postDate) && heading.Contains(appSettings[postDate].ToString()))
                {
                    UriBuilder ub = new UriBuilder("http://fushigispace.com/faq/Bulliten/PostDeleteHandler.ashx");
                    WebClient c = new WebClient();

                    c.UploadStringCompleted += (sender2, e2) =>
                    {
                        displayCanvas.Visibility = Visibility.Collapsed;
                        errorTextBlock.Visibility = Visibility.Collapsed;
                        adminCanvas.Visibility = Visibility.Collapsed;
                        mainBoard.Visibility = Visibility.Visible;

                        Button_Click_1(null, null);
                    };
                    c.UploadStringAsync(ub.Uri, currentPost.ToString());

                    noDeleteTextBlock.Text = "削除しています。";
                }
                else
                {
                    noDeleteTextBlock.Text = "削除に失敗しました。";
                    displayCanvas.Visibility = Visibility.Visible;
                }
            }
            else  //when trying to delete a specific comment
            {
                commentText = BullitenFile.GetComment(currentPost, file, Convert.ToInt32(adminComboBox.SelectedItem));

                if (commentText == "COMMENT_NOT_FOUND")
                {
                    noDeleteTextBlock.Text = "コメントが見つかりません。";
                    noDeleteTextBlock.Visibility = Visibility.Visible;
                    displayCanvas.Visibility = Visibility.Visible;
                    return;
                }

                index = commentText.IndexOf(" ---- ") - 9;  //position at the middle of the date and year string "2009/01/01"

                do { index--; } while (commentText.Substring(index, 1) != " ");  //go backwards and find where the date string begins

                commentDate = commentText.Substring(index + 1, commentText.IndexOf(" ---- ") - index - 1);  //include the time

                //                temp = appSettings[commentDate].ToString();
                //                tempBool = appSettings.Contains(commentDate);

                if (appSettings.Contains(commentDate) && commentText.Contains(appSettings[commentDate].ToString()))
                {
                    UriBuilder ub = new UriBuilder("http://fushigispace.com/faq/Bulliten/CommentDeleteHandler.ashx");
                    WebClient c = new WebClient();

                    c.UploadStringCompleted += (sender2, e2) =>
                    {
                        displayCanvas.Visibility = Visibility.Collapsed;
                        errorTextBlock.Visibility = Visibility.Collapsed;
                        adminCanvas.Visibility = Visibility.Collapsed;
                        mainBoard.Visibility = Visibility.Visible;

                        Button_Click_1(null, null);
                    };
                    c.UploadStringAsync(ub.Uri, Convert.ToString(currentPost) + ":@" + adminComboBox.SelectedIndex.ToString() + ";@");

                    noDeleteTextBlock.Text = "削除しています。";
                }
                else
                {
                    noDeleteTextBlock.Text = "削除に失敗しました。";
                    displayCanvas.Visibility = Visibility.Visible;
                }
            }

            noDeleteTextBlock.Visibility = Visibility.Visible;
        }

        private void adminComboBox_DropDownOpened(object sender, EventArgs e)
        {
            noDeleteTextBlock.Visibility = Visibility.Collapsed;

        }

        private void newNameTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (appSettings.Contains("UserName") && String.IsNullOrEmpty(newNameTextBox.Text))
            {
                newNameTextBox.Text = appSettings["UserName"].ToString();
            }
        }

        private void replyNameTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (appSettings.Contains("UserName") && String.IsNullOrEmpty(replyNameTextBox.Text) && replyNameTextBox.Text != (string)appSettings["UserName"])
            {
                replyNameTextBox.Text = appSettings["UserName"].ToString();
            }
        }

        private void readButton_Click(object sender, RoutedEventArgs e)
        {
            string temp = "";
            ChildWindow1 childWindow = new ChildWindow1();

            if (settingTextBlock.Text == "新規")
            {
                temp = BullitenFile.GetContent(headingNum, file);
                temp = temp.Substring(0, temp.IndexOf("\r\n"));
                temp = temp.Replace(":@", "");

                ((TextBlock)(childWindow.FindName("childWindowTextBlock"))).Text = titleTextBlock.Text + "\r\r" + temp;
                childWindow.Title = "投稿内容";
            }
            else
            {
                ((TextBlock)(childWindow.FindName("childWindowTextBlock"))).Text = BullitenFile.GetComment(headingNum, file, Convert.ToInt32(settingTextBlock.Text)).Replace(" ---- ", "\r\r");
                childWindow.Title = "コメント番号" + settingTextBlock.Text;
            }

            childWindow.Show();
        }

        private void mainBoard_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mainScrollBar.Value -= e.Delta;

            currentTopPosition = (int)((numberOfPosts - 17) * mainScrollBar.Value);

            bulliten.Heading1 = BullitenFile.CreateHeading((int)(1 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading2 = BullitenFile.CreateHeading((int)(2 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading3 = BullitenFile.CreateHeading((int)(3 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading4 = BullitenFile.CreateHeading((int)(4 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading5 = BullitenFile.CreateHeading((int)(5 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading6 = BullitenFile.CreateHeading((int)(6 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading7 = BullitenFile.CreateHeading((int)(7 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading8 = BullitenFile.CreateHeading((int)(8 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading9 = BullitenFile.CreateHeading((int)(9 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading10 = BullitenFile.CreateHeading((int)(10 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading11 = BullitenFile.CreateHeading((int)(11 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading12 = BullitenFile.CreateHeading((int)(12 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading13 = BullitenFile.CreateHeading((int)(13 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading14 = BullitenFile.CreateHeading((int)(14 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading15 = BullitenFile.CreateHeading((int)(15 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading16 = BullitenFile.CreateHeading((int)(16 + currentTopPosition), file).Replace(":@", "");
            bulliten.Heading17 = BullitenFile.CreateHeading((int)(17 + currentTopPosition), file).Replace(":@", "");
        }

        private void newNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (IS_LIVE_LOGIN)
            {
                newNameTextBox.IsReadOnly = true;
            }
        }

        private void replyNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (IS_LIVE_LOGIN)
            {
                replyNameTextBox.IsReadOnly = true;
            }
        }

        //        private void newPostTextBox_KeyDown(object sender, KeyEventArgs e)
        //        {
        //            if (e.Key == Key.Enter)
        //            {
        //                newPostTextBox.Text += "\r\n";
        //            }
        //        }
    }
}
