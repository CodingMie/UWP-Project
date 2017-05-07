using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
        //  待考虑，是否可以修改
       protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = ((ViewModel.ViewModel)e.Parameter);
        }
            private bool checkInput()
        {
            string warning = "";
            if (Category.SelectedIndex == -1)
            {
                warning += "请选择类别\n";
            }
            if (!Regex.IsMatch(Amount.Text, @"^(-?\d+)(\.\d+)?$"))
            {
                warning += "金额输入有误\n";
            }
          //  bool isPocketMoney = (category == "私房钱") ? true : false;
         //   bool inOrOut = (bool)income.IsChecked;
            if (Description.Text == "")
            {
                warning += "请输入描述\n";
            }
            if (warning != "")
            {
                var i = new MessageDialog(warning).ShowAsync();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            if (!checkInput()) return;
            string category = Category.SelectedItem.ToString();
            DateTimeOffset date = Date.Date;
           
            double amount = Convert.ToDouble(Amount.Text);
            bool isPocketMoney = (category == "私房钱") ? true:false;
            bool inOrOut = (bool)income.IsChecked;
            string description = Description.Text;
            ViewModel.AddAccountItem(category, date, amount, isPocketMoney, inOrOut, description);
            Frame.Navigate(typeof(MainPage), ViewModel);
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
