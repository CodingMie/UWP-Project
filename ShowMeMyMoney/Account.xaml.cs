using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace ShowMeMyMoney
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Account : Page
    {
        private ViewModel.ViewModel ViewModel;
        public Account()
        {
            this.InitializeComponent();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            string category = Category.SelectedItem.ToString();
            DateTimeOffset date = Date.Date;
           
            double amount = Convert.ToDouble(Amount.Text);
            bool isPocketMoney = (category == "私房钱") ? true:false;
            bool inOrOut = (bool)income.IsChecked;
            string description = Description.Text;
            ViewModel.AddAccountItem(category, date, amount, isPocketMoney, inOrOut, description);
        }

        private void canclButton_Click(object sender, RoutedEventArgs e)
        {
            //Category.SelectedIndex = 0;
            Date.Date = DateTime.Now;
            Amount.Text = "";
            income.IsChecked = false;
            Description.Text = "";
            Frame.Navigate(typeof(MainPage));
        }
    }
}
