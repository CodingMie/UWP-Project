using Newtonsoft.Json;
using ShowMeMyMoney.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace ShowMeMyMoney
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.accountViewModel = new ViewModel.ViewModel();
            this.categoryViewModel = new ViewModel.categoryViewModel();
            remainedProportion = 100;
            makeColorPicker();
            initializeShareSlider();
        }

        private void addShareBar(categoryItem c)
        {
            /* 从categoryItem里读取分类的比例和颜色，并设置到 “ShareBar”的各个长方形中  */

            if (c.number == -1) return;
            Rectangle r = new Rectangle();
            r.Fill = new SolidColorBrush((Color)typeof(Colors).GetProperty(c.color).GetValue(this));
            r.Width = c.share / 100 * 400;
            shareBar.Children.Insert(0, r);


        }
        private int categoryCount;
        private double remainedProportion;
        ViewModel.ViewModel accountViewModel { get; set; }
        ViewModel.categoryViewModel categoryViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {

                if (e.Parameter.GetType() == typeof(ViewModel.ViewModel))
                {
                    this.accountViewModel = ((ViewModel.ViewModel)e.Parameter);
                }
                else if (e.Parameter.GetType() == typeof(ViewModel.categoryViewModel))
                {
                    this.categoryViewModel = ((ViewModel.categoryViewModel)e.Parameter);
                }
                accountViewModel.SelectedItem = null;
            }
        }
        private void ShowCategory_Click(object sender, ItemClickEventArgs e)
        {
            /* 将分类item发送到accountsListViewPage */
            categoryItem citem = ((categoryItem)(e.ClickedItem));
            Frame.Navigate(typeof(AccountsListViewPage), citem);
        }




        private async void AddNewCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = AddNewCategoryDialog;
            shareSlider.Header = "# 所占预算比例，还剩" + remainedProportion + "%可用";
            await dialog.ShowAsync();
        }

        private async void AddNewCategoryDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            /*
            if (remainedProportion < Convert.ToDouble(categoryShare.Text))
            {
               //  /* 判断新添加的分类比例是否合法 
                AddNewCategoryDialog.Hide();
                ContentDialog alert = new ContentDialog()
                {
                    Title = "错误提示",
                    Content = "新设置的比例太大啦！只剩" + remainedProportion + "%可用。",
                    IsPrimaryButtonEnabled = true,
                    PrimaryButtonText = "OK"

                };
                await alert.ShowAsync();
                return;
            } 
        */


            categoryItem newCategory = new categoryItem(categoryCount, categoryName.Text, shareSlider.Value, categoryColor.Text);

             /* 分类名称重复约束 */
            foreach (var item in categoryViewModel.AllCatagoryItems)
            {
                if (item.name == newCategory.name) {
                    AddNewCategoryDialog.Hide();
                    ContentDialog alert = new ContentDialog()
                    {
                        Title = "错误提示",
                        Content = "分类名字重复！换个名字吧~",
                        IsPrimaryButtonEnabled = true,
                        PrimaryButtonText = "OK"

                    };
                    await alert.ShowAsync();
                    return;
                }
                    
            }

            /* 添加新分类 */
            categoryViewModel.AddCategoryItem(newCategory);
            categoryCount++;
            remainedProportion -= newCategory.share;
            categoryViewModel.saveCategoryTable();
            AddNewCategoryDialog.Hide();
            /* 添加shareBar的条条 */
            addShareBar(newCategory);
            /* 修改滑块的上限 */
            shareSlider.Maximum = remainedProportion;
        }

        private void AddNewCategoryDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            AddNewCategoryDialog.Hide();
        }

        private void AddNewAccountButton_Click(object sender, RoutedEventArgs e)
        {
            ArrayList list = new ArrayList();
            list.Add(accountViewModel);
            list.Add(categoryViewModel);
            Frame.Navigate(typeof(Account), list);
        }
        private void makeColorPicker()
        {
            var colors = typeof(Colors).GetTypeInfo().DeclaredProperties;

           

            foreach (var item in colors)
            {
                /* TODO: 删除已经被选走的颜色 。 要实现该功能，还要在新增分类时维护颜色表。  */
                ColorPallete_Combo.Items.Add(item);
            }

            
        }
        private void ColorPalleteCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ColorPallete_Combo.SelectedIndex != -1)
            {
                var pi = ColorPallete_Combo.SelectedItem as PropertyInfo;
                ColorPallete_Combo.BorderBrush = new SolidColorBrush((Color)pi.GetValue(null));
                var colorName = pi.Name;
                categoryColor.Text = colorName;
            }
        }

        private void shareSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
           /* 好像不需要干嘛 */
        }

        private void initializeShareSlider()
        {
            shareSlider.Maximum = remainedProportion;
        }
    }

}
