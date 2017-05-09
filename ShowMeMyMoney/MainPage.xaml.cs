using Newtonsoft.Json;
using ShowMeMyMoney.Model;
using System;
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
            this.ViewModel = new ShowMeMyMoney.ViewModel.ViewModel();
            remainedProportion = 100;
                initializeCategoryTable();

            /* reset, for debugging  
            categoryTable = new ObservableCollection<categoryItem>();
            categoryTable.Clear();
            categoryTable.Add(new categoryItem(-1, "Total", 100, "Black"));
            //    saveCategoryTable();*/

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
        private ObservableCollection<categoryItem> categoryTable;
        private int categoryCount;
        private double remainedProportion;
        ViewModel.ViewModel ViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {

                if (e.Parameter.GetType() == typeof(ViewModel.ViewModel))
                {
                    this.ViewModel = ((ViewModel.ViewModel)e.Parameter);
                }
                ViewModel.SelectedItem = null;
            }
        }
        private void ShowCategory_Click(object sender, ItemClickEventArgs e)
        {
            /* 将分类item发送到accountsListViewPage */
            categoryItem citem = ((categoryItem)(e.ClickedItem));
            Frame.Navigate(typeof(AccountsListViewPage), citem);
        }


        private async void initializeCategoryTable()
        {
            categoryTable = new ObservableCollection<categoryItem>();
            categoryCount = 0;
            try
            {
                /*  读取json文件  */
                var Folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var item = await Folder.TryGetItemAsync("categoryTable.json");
                if (item == null)
                {
                    /* 第一次打开应用, 则无需进行后续操作 */
                    /* 手动添加一个代表总开支的item */
                    categoryTable.Add(new categoryItem(-1, "Total", 100, "Black"));
                    return;
                }




                var file = await Folder.GetFileAsync("categoryTable.json");

                var data = await file.OpenReadAsync();

                using (StreamReader r = new StreamReader(data.AsStream()))
                {
                    string text = r.ReadToEnd();

                    /* 如果json是空的，也不能进行后续操作*/
                    if (text == "") return;
                    /* 将json文件中的东西反序列化，加入到catagoryItem的数组   */
                    categoryItem[] p = JsonConvert.DeserializeObject<categoryItem[]>(text);
                    foreach (var i in p)
                    {
                        categoryTable.Add(i);
                        /* 不断调整remainedProportion */
                        if (i.number != -1)
                        remainedProportion -= i.share;
                        categoryCount++;
                    }
                }

                foreach (var c in categoryTable)
                {
                    addShareBar(c);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async void saveCategoryTable()
        {
            try
            {
                var Folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var file = await Folder.CreateFileAsync("categoryTable.json", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                var data = await file.OpenStreamForWriteAsync();

                using (StreamWriter r = new StreamWriter(data))
                {
                    var serelizedfile = JsonConvert.SerializeObject(categoryTable);
                    r.Write(serelizedfile);

                }

            }
            catch (Exception e)
            {
                throw e;
            }
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
            foreach (var item in categoryTable)
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
            categoryTable.Add(newCategory);
            categoryCount++;
            
            remainedProportion -= newCategory.share;
            saveCategoryTable();
            AddNewCategoryDialog.Hide();
            /* 添加shareBar的条条 */
            addShareBar(newCategory);
            /* 修改滑块的上限 */

            shareSlider.Maximum = remainedProportion;
            //  ViewModel.AddCategoryItemm(newCategory);
        }

        private void AddNewCategoryDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            AddNewCategoryDialog.Hide();
        }

        private void AddNewAccountButton_Click(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(Account), categoryTable);
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
