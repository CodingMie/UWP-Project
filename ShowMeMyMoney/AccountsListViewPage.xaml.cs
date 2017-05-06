using ShowMeMyMoney.Model;
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
            this.InitializeComponent();
        }
        public ViewModel.ViewModel ViewModel;
        public categoryItem ListCategory;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            /*  根据传过来的参数，确定展示所有的账目or展示某个分类的账目
             -1：所有账目
             其他：展示相应分类的账目 */

            if (e.Parameter.GetType() == typeof(categoryItem))
            {
                this.ListCategory = (categoryItem)(e.Parameter);
                /* 根据指定分类去数据库取出数据 */
                 ViewModel.getItemsFromDB(ListCategory);
            }
            


        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }



}
