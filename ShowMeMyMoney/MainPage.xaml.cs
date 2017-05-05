using Newtonsoft.Json;
using ShowMeMyMoney.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            initializeCategoryTable();
        }
        private void setProPortionBar()
        {
            /* 从categoryTable里读取分类的比例和颜色，并设置到 “proportionBar”的各个长方形中  */
        }
        private ObservableCollection<categoryItem> categoryTable;
        private int categoryCount;

        private void ShowCategory_Click(object sender, ItemClickEventArgs e)
        {
            /* 将分类编号发送到accountsListViewPage */
            int Category_number = ((categoryItem)(e.ClickedItem)).number;
            Frame.Navigate(typeof(NewPage), Category_number);
        }


        private async void initializeCategoryTable() {
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
                    return;
                }
                var file = await Folder.GetFileAsync("categoryTable.json");
                
                var data = await file.OpenReadAsync();

                using (StreamReader r = new StreamReader(data.AsStream()))
                {
                    string text = r.ReadToEnd();
                    /* 将json文件中的东西反序列化，加入到catagoryItem的数组   */
                    categoryItem[] p = JsonConvert.DeserializeObject<categoryItem[]>(text);
                    foreach (var i in p)
                    {
                        categoryTable.Add(i);
                        categoryCount++;
                    }
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async void saveCategoryTable() {
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
            await dialog.ShowAsync();
        }

        private void AddNewCategoryDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            /* 添加新分类 */
            categoryItem newCategory = new categoryItem(categoryCount, categoryName.Text, Convert.ToDouble(categoryShare.Text), categoryColor.Text);
            categoryTable.Add(newCategory);
            categoryCount++;
            saveCategoryTable();
            AddNewCategoryDialog.Hide();

        }

        private void AddNewCategoryDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            AddNewCategoryDialog.Hide();
        }

        private void AddNewAccountButton_Click(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(NewPage));
        }
    }

}
