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
using CefSharp.DevTools.IO;
using CefSharp;
using CefSharp.Wpf;

namespace MyChromium
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string HomePage = "www.baidu.com";

        List<string> History;

        int Current;




        public MainWindow()
        {
            InitializeComponent();
            History = new List<string>();
            Current = -1;

            

            LoadWebPage(HomePage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="link"></param>
        /// <param name="saveToHistory"></param>
        void LoadWebPage(string link,bool saveToHistory=true)
        {
            

            AddressBar.Text = link;
            Chrome.Address = link;

            

            if (saveToHistory)
            {
                Current++;
                History.Add(link);

                MenuItem historyItem = new MenuItem();
                historyItem.Click += HistoryItem_Click;
                historyItem.Header = link;
                historyItem.Width = 185;

                HistoryMenu.Items.Add(historyItem);
            }
        }

        

        private void AddressBar_KeyDown(object sender, KeyEventArgs e) 
        {
            if (e.Key==Key.Enter)
            {
                LoadWebPage(AddressBar.Text);
            }
        }

        private void ToggleWebPage(object sender, RoutedEventArgs e)
        {
            string option = (sender as Button).Content.ToString();
            switch (option)
            {
                case "🢨":
                    if ((History.Count+Current-1)>=History.Count)
                    {
                        Current--;
                        LoadWebPage(History[Current],false);
                    }
                    break;
                case "🢩":
                    if ((History.Count - Current - 1) != 0)
                    {
                        Current++;
                        LoadWebPage(History[Current], false);
                    }
                    break;

            }
        }

        private void RefreshWebPage(object sender, RoutedEventArgs e)
        {
            LoadWebPage(History[Current],false);
        }

        private void GoToHomePage(object sender, RoutedEventArgs e)
        {
            LoadWebPage(HomePage);
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (History.Count!=0)
            {
                HistoryMenu.PlacementTarget = HistoryButton;
                HistoryMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                HistoryMenu.HorizontalOffset = -155;
                HistoryMenu.IsOpen = true;
            }
        }

        private void HistoryItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem historyItem = (MenuItem)sender;
            LoadWebPage(historyItem.Header.ToString());
        }

        private void HistoryButton_RightClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void HistoryButton_LeftClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

    }
}
