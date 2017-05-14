using ShowMeMyMoney.Model;
using ShowMeMyMoney.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ShowMeMyMoney
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AccountsListViewPage : Page
    {
        public AccountsListViewPage()
        {
            this.ViewModel = new ViewModel.ViewModel();
            this.InitializeComponent();
            var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Windows.UI.Colors.CornflowerBlue;
            viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.CornflowerBlue;
            // ExpenseList.Items.Add();
            
        }
        public ViewModel.ViewModel ViewModel;

        ViewModel.categoryViewModel categoryViewModel { get; set; }
        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            /*  根据传过来的参数，确定展示所有的账目or展示某个分类的账目
             -1：所有账目
             其他：展示相应分类的账目 */

            if (e.Parameter.GetType() == typeof(categoryViewModel))
            {
                this.categoryViewModel = (categoryViewModel)(e.Parameter);
                ViewModel.queryDisplayItems(categoryViewModel.SelectedCategory);
            }

            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            }
            else
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }

        }

        private void categoryItemClicked(object sender, ItemClickEventArgs e)
        {
            categoryViewModel.SelectedCategory = (categoryItem)(e.ClickedItem);
            ViewModel.queryDisplayItems(categoryViewModel.SelectedCategory);
            this.InitializeComponent();
        }
        
        private void accountItemClicked(object sender, ItemClickEventArgs e)
        {
            ViewModel.SelectedItem = (accountItem)(e.ClickedItem);
            category.Text = category0.Text = categoryViewModel.SelectedCategory.name;
            amount.Text = amount0.Text = Math.Abs(ViewModel.SelectedItem.amount) + "元";
            inOrOut.Text = inOrOut0.Text = (ViewModel.SelectedItem.inOrOut) ? "收入" : "支出";
            description.Text = description0.Text = ViewModel.SelectedItem.description;
            date.Text = date0.Text = ViewModel.SelectedItem.createDate.Year + "年"
                    + ViewModel.SelectedItem.createDate.Month + "月"
                    + ViewModel.SelectedItem.createDate.Day + "日";
            isPocketMoney.Text = isPocketMoney0.Text = ViewModel.SelectedItem.isPocketMoney ? "是" : "否";
        }



        // edit the item
        private void edit_click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedItem != null)
            {
                ArrayList list = new ArrayList();
                list.Add(ViewModel);
                list.Add(categoryViewModel);
                Frame.Navigate(typeof(Account), list);
            }
        }

        //delete the item
        private void delete_click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedItem != null)
            {
                ViewModel.RemoveAccountItem(ViewModel.SelectedItem.id);
                ViewModel.SelectedItem = null;
                category.Text = category0.Text = "";
                amount.Text = amount0.Text = "";
                inOrOut.Text = inOrOut0.Text = "";
                description.Text = description0.Text = "";
                date.Text = date0.Text = "";
                isPocketMoney.Text = isPocketMoney0.Text = "";
            }
        }
        
    }



}
