using ShowMeMyMoney.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ViewModel.ViewModel AccountViewModel;
        private ViewModel.categoryViewModel CategoryViewModel;


        public Account()
        {

            this.InitializeComponent();
        }
        //  待考虑，是否可以修改
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var list = e.Parameter as ArrayList;
            foreach (var item in list)
            {
                if (item.GetType() == typeof(ViewModel.categoryViewModel))
                {
                    CategoryViewModel = (ViewModel.categoryViewModel)item;
                }
                else
                {
                    AccountViewModel = (ViewModel.ViewModel)item;
                }
            }
            /* 默认分类为支出 */
            expense.IsChecked = true;

        }
        private bool checkInput()
        {
            string warning = "";
            /*
            if (ExpenseCategory.SelectedIndex == -1 || IncomeCategory.SelectedIndex == -1)
            {
                warning += "请选择类别\n";
            }*/
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

            bool expenseOrIncome = expense.IsChecked == true ? false : true;

            string category = expenseOrIncome ? ((categoryItem)IncomeCategory.SelectedItem).name : ((categoryItem)ExpenseCategory.SelectedItem).name;

            long categoryNum = CategoryViewModel.getCategoryNum(category, expenseOrIncome);
            DateTimeOffset date = Date.Date;
            double amount = Convert.ToDouble(Amount.Text);

            bool isPocketMoney = PocketMoney.IsChecked == true ? true : false;
            if (isPocketMoney)
            {
                amount = expenseOrIncome ? amount : amount * (-1);
                /* 用了私房钱就要修改VM */
                CategoryViewModel.pocketMoneyAmount += amount;


            }
            string description = Description.Text;

            var newAccount = new accountItem(categoryNum, date, amount, isPocketMoney, expenseOrIncome, description);
            AccountViewModel.AddAccountItem(categoryNum, date, amount, isPocketMoney, expenseOrIncome, description);

            /* 修改分类总额 */
            CategoryViewModel.UpdateCategoryByAccount(newAccount);

            /* 发送修改后的viewmodel到主页*/
            Frame.Navigate(typeof(MainPage), CategoryViewModel);
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

        private void out_Checked(object sender, RoutedEventArgs e)
        {
            IncomeCategory.Visibility = Visibility.Collapsed;
            ExpenseCategory.Visibility = Visibility.Visible;
        }

        private void income_Checked(object sender, RoutedEventArgs e)
        {
            IncomeCategory.Visibility = Visibility.Visible;
            ExpenseCategory.Visibility = Visibility.Collapsed;

        }
    }
}
