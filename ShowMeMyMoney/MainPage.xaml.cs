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

using Windows.UI.Notifications;
using NotificationsExtensions.Tiles;
using NotificationsExtensions;
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using System.Text.RegularExpressions;

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
            monthlyBudget = 1500;
            this.InitializeComponent();



            this.accountViewModel = new ViewModel.ViewModel();
            this.categoryViewModel = new ViewModel.categoryViewModel();
            remainedProportion = 100;
            makeColorPicker();
            initializeShareSlider();
            initializeShareBar();
            createTile();
            DataTransferManager DataTrans = DataTransferManager.GetForCurrentView();
            DataTrans.DataRequested += OnShareDataRequested;
        }

        async void OnShareDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var dp = args.Request.Data;
            var deferral = args.Request.GetDeferral();
            string uri = "ms-appx://" + ((BitmapImage)pic.Source).UriSource.LocalPath;
            var img = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uri));
            dp.Properties.Title = "【紧急求助】";
            dp.SetText("本月" + TotalBudgetProportion.Text + "," + totalExpense + "元，" + "\nFrom ShowMeMyMoney");
            dp.SetStorageItems(new List<StorageFile> { img });
            deferral.Complete();

        }


        private void updateMetadataViews()

        {
            TotalExpenseAmount.Text = totalExpense.ToString();
            TotalIncomeAmount.Text = totalIncome.ToString();
            PocketMoneyAmount.Text = totalPocketMoney.ToString();
            TotalBudgetProportion.Text = "已使用" + Math.Truncate(totalExpense / monthlyBudget * 100) + "%";
            if (totalExpense / monthlyBudget < 20)
            {
                pic.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic3.jpg", UriKind.RelativeOrAbsolute));
            }
            else if (totalExpense / monthlyBudget < 50)
            {
                pic.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic2.jpg", UriKind.RelativeOrAbsolute));
            }
            else
            {
                pic.Source = new BitmapImage(new Uri("ms-appx:///Assets/pic1.jpg", UriKind.RelativeOrAbsolute));
            }
            createTile();
        }

        /*------ metadata -----*/
        private double totalExpense, totalIncome, totalPocketMoney, monthlyBudget;
        private bool locked = false;
        private Dictionary<string, double> metadata = new Dictionary<string, double>();
        private void maintainMetadata()
        {
            /* 根据ViewModel更新元数据：包括本月预算，总收入，总开支，私房钱
               */
            totalExpense = totalIncome = 0;
            foreach (var item in categoryViewModel.allExpenseCatagoryItems)
            {
                remainedProportion -= item.share;
                totalExpense += item.amount;

            }
            foreach (var item in categoryViewModel.allIncomeCatagoryItems)
            {
                totalIncome += item.amount;
            }

            totalPocketMoney = categoryViewModel.pocketMoneyAmount;
            locked = true;
            /* 将更新后的metadata写入文件保存 */
            writeMetadataToFile();
            updateMetadataViews();
        }


        private async void readMetadataFromFile()
        {
            try
            {
                /*  读取json文件  */
                var Folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var item = await Folder.TryGetItemAsync("metadata.json");
                var a = 1;
                if (item == null)
                {

                    metadata.Add("totalExpense", totalExpense);
                    metadata.Add("totalIncome", totalIncome);
                    metadata.Add("totalPocketMoney", totalPocketMoney);
                    metadata.Add("monthlyBudget", monthlyBudget);

                    /* 第一次打开应用, 则无需进行后续操作 */
                    return;
                }
                var file = await Folder.GetFileAsync("metadata.json");

                var data = await file.OpenReadAsync();

                using (StreamReader r = new StreamReader(data.AsStream()))
                {
                    string text = r.ReadToEnd();

                    /* 如果json是空的，也不能进行后续操作*/
                    if (text == "" || text == "[]") return;
                    /* 将json文件中的东西反序列化，加入到metadata   */
                    Dictionary<string, double> p = JsonConvert.DeserializeObject<Dictionary<string, double>>(text);
                    metadata = p;

                    totalExpense = metadata["totalExpense"];
                    totalIncome = metadata["totalIncome"];
                    totalPocketMoney = metadata["totalPocketMoney"];
                    monthlyBudget = metadata["monthlyBudget"];



                    updateMetadataViews();
                    locked = false;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        private async void writeMetadataToFile()
        {
            try
            {
                var Folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var file = await Folder.CreateFileAsync("metadata.json", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                var data = await file.OpenStreamForWriteAsync();

                metadata["totalExpense"] = totalExpense;
                metadata["totalIncome"] = totalIncome;
                metadata["totalPocketMoney"] = totalPocketMoney;
                metadata["monthlyBudget"] = monthlyBudget;

                using (StreamWriter r = new StreamWriter(data))
                {

                    var serelizedfile = JsonConvert.SerializeObject(metadata);
                    r.Write(serelizedfile);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void initializeShareBar()
        {
            foreach (var item in categoryViewModel.allExpenseCatagoryItems)
            {
                addShareBar(item);
            }
        }

        public void addShareBar(categoryItem c)
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

            if (e.Parameter == null)
            {

            }
            if (e.Parameter != null)
            {

                if (e.Parameter.GetType() == typeof(ViewModel.ViewModel))
                {
                    this.accountViewModel = ((ViewModel.ViewModel)e.Parameter);
                }
                else if (e.Parameter.GetType() == typeof(ViewModel.categoryViewModel))
                {
                    this.categoryViewModel = ((ViewModel.categoryViewModel)e.Parameter);
                    /* 更新总收入/总支出 */
                    locked = true;
                    maintainMetadata();
                }
                accountViewModel.SelectedItem = null;



            }

            /* 希望只读一次 */
            if (!locked)
            {
                readMetadataFromFile();
            }
            DataTransferManager DataTrans = DataTransferManager.GetForCurrentView();
            DataTrans.DataRequested += OnShareDataRequested;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            DataTransferManager DataTrans = DataTransferManager.GetForCurrentView();
            DataTrans.DataRequested -= OnShareDataRequested;
        }


        private void ShowCategory_Click(object sender, ItemClickEventArgs e)
        {
            /* 将分类item发送到accountsListViewPage */
            categoryItem citem = ((categoryItem)(e.ClickedItem));
            categoryViewModel.SelectedCategory = citem;
            Frame.Navigate(typeof(AccountsListViewPage), categoryViewModel);
        }




        private async void AddNewCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = AddNewCategoryDialog;
            shareSlider.Header = "# 所占预算比例，还剩" + remainedProportion + "%可用";
            expenseButton.IsChecked = false;
            incomeButton.IsChecked = false;
            categoryName.Text = "";
            shareSlider.Value = 0;
            ColorPallete_Combo.SelectedIndex = -1;

            await dialog.ShowAsync();
        }

        private async void AddNewCategoryDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            /* 确定收入还是支出 */

            bool incomeOrExpense = incomeButton.IsChecked == true ? true : false;
            double shareValue = shareSlider.Value;
            if (incomeOrExpense)
            {
                // 如果是收入，不设置比例
                shareValue = 0;
            }
            categoryItem newCategory = new categoryItem(categoryCount, categoryName.Text, shareValue, categoryColor.Text, incomeOrExpense);
            /* 分类名称重复约束 */
            foreach (var item in categoryViewModel.AllCatagoryItems)
            {
                if (item.name == newCategory.name)
                {
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

            if (incomeOrExpense == false)
            {

                remainedProportion -= newCategory.share;
                /* 如果添加了支出，修改滑块的上限 */
                shareSlider.Maximum = remainedProportion;
                /* 添加shareBar的条条 */
                addShareBar(newCategory);
            }
            /* 添加新分类 */
            categoryViewModel.AddCategoryItem(newCategory);
            categoryCount++;


            AddNewCategoryDialog.Hide();
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
        private void viewStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            /* 跳转到统计页 */
            Frame.Navigate(typeof(statistics));
        }

        private void initializeShareSlider()
        {
            shareSlider.Maximum = remainedProportion;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(newAmount.Text, @"^(-?\d+)(\.\d+)?$"))
            {
                BudgetAmount.Text = newAmount.Text;
                monthlyBudget = Double.Parse(BudgetAmount.Text);
                writeMetadataToFile();
                updateMetadataViews();
                BudgetFlyout.Hide();
                newAmount.Text = "";
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(PocketAmount.Text, @"^(-?\d+)(\.\d+)?$"))
            {
                PocketMoneyAmount.Text = PocketAmount.Text;
                totalPocketMoney = Double.Parse(PocketMoneyAmount.Text);
                writeMetadataToFile();
                updateMetadataViews();
                PocketMoneyFlyout.Hide();
                PocketAmount.Text = "";
            }
        }
        private void cancl_Button_Click_1(object sender, RoutedEventArgs e)
        {
            newAmount.Text = "";
            BudgetFlyout.Hide();
        }
        private void cancl_Button_Click_2(object sender, RoutedEventArgs e)
        {
            PocketAmount.Text = "";
            PocketMoneyFlyout.Hide();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        private void out_Checked(object sender, RoutedEventArgs e)
        {
            shareSlider.Visibility = Visibility.Visible;
        }

        private void in_Checked(object sender, RoutedEventArgs e)
        {
            shareSlider.Visibility = Visibility.Collapsed;
        }

        //  创建磁贴
        public void createTile()
        {
            string from = (monthlyBudget + totalIncome - totalExpense).ToString();
            string subject = "本月余额";
            string body = "点此来记账";
            string picSource = ((BitmapImage)pic.Source).UriSource.LocalPath;
            picSource = picSource.Substring(1);


            // Construct the tile content
            TileContent content = new TileContent()
            {
                Visual = new TileVisual()
                {
                    TileMedium = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            PeekImage = new TilePeekImage()
                            {
                                Source = picSource
                            },
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = subject,
                                               HintStyle = AdaptiveTextStyle.SubtitleSubtle,
                HintAlign = AdaptiveTextAlign.Center
                                },
                                new AdaptiveText()
                                {
                                    Text = from,
                                                    HintStyle = AdaptiveTextStyle.Title,
                HintAlign = AdaptiveTextAlign.Center
                                },


                                new AdaptiveText()
                                {
                                    Text = body,
                                    HintStyle = AdaptiveTextStyle.CaptionSubtle,
                HintAlign = AdaptiveTextAlign.Center
                                }
                            }
                        }
                    },

                    TileWide = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = subject,
                                               HintStyle = AdaptiveTextStyle.SubtitleSubtle,
                HintAlign = AdaptiveTextAlign.Center
                                },
                                new AdaptiveText()
                                {
                                    Text = from,
                                                    HintStyle = AdaptiveTextStyle.Title,
                HintAlign = AdaptiveTextAlign.Center
                                },

                                new AdaptiveText()
                                {
                                    Text = body,
                                    HintStyle = AdaptiveTextStyle.CaptionSubtle,
                HintAlign = AdaptiveTextAlign.Center
                                }
                            }
                        }
                    },
                    TileLarge = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            TextStacking = TileTextStacking.Center,
                            Children =
        {
            new AdaptiveGroup()
            {
                Children =
                {
                    new AdaptiveSubgroup() { HintWeight = 1 },
                    new AdaptiveSubgroup()
                    {
                        HintWeight = 2,
                        Children =
                        {
                            new AdaptiveImage()
                            {
                                Source = picSource,
                                HintCrop = AdaptiveImageCrop.Circle
                            }
                        }
                    },
                    new AdaptiveSubgroup() { HintWeight = 1 }
                }
            },
            new AdaptiveText()
            {
                Text = from,
                HintStyle = AdaptiveTextStyle.Title,
                HintAlign = AdaptiveTextAlign.Center
            },
            new AdaptiveText()
            {
                Text = body,
                HintStyle = AdaptiveTextStyle.SubtitleSubtle,
                HintAlign = AdaptiveTextAlign.Center
            }
        }
                        }
                    }
                }
            };

            //TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            var notification = new TileNotification(content.GetXml());
            // And send the notification
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);

        }

    }

}