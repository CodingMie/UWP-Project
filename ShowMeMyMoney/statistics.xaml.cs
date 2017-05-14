using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using System.Collections;
using Windows.UI.Xaml.Shapes;
using Windows.UI;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ShowMeMyMoney
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class statistics : Page
    {
        
        public statistics()
        {
            this.AccountViewModel = new ViewModel.ViewModel();
            this.CategoryViewModel = new ViewModel.categoryViewModel();
            this.InitializeComponent();

        }

        ViewModel.ViewModel AccountViewModel { get; set; }
        ViewModel.categoryViewModel CategoryViewModel { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter == null)
            {

            }
            if (e.Parameter != null)
            {

                if (e.Parameter.GetType() == typeof(ViewModel.ViewModel))
                {
                    this.AccountViewModel = ((ViewModel.ViewModel)e.Parameter);
                }
                else if (e.Parameter.GetType() == typeof(ViewModel.categoryViewModel))
                {
                    this.CategoryViewModel = ((ViewModel.categoryViewModel)e.Parameter);
                    /* 更新总收入/总支出 
                    locked = true;
                    maintainMetadata();*/
                }
                AccountViewModel.SelectedItem = null;
            }

            /*  y轴  */
            TextBlock[] yMoney = new TextBlock[5];
            for (int i = 0; i < 5; i++)
            {
                yMoney[i] = new TextBlock();
                yMoney[i].Text = (300 * (i + 1)).ToString();
                Canvas.SetLeft(yMoney[i], -32);
                Canvas.SetTop(yMoney[i], 149 - i * 40);
                year_cvs.Children.Add(yMoney[i]);
            }

            /*  x轴 */
            TextBlock[] xMon = new TextBlock[12];
            for (int i = 0; i < 12; i++)
            {
                xMon[i] = new TextBlock();
                xMon[i].Text = (i + 1).ToString();
                Canvas.SetLeft(xMon[i], 6 + i * 29);
                Canvas.SetTop(xMon[i], 204);
                year_cvs.Children.Add(xMon[i]);
            }

            /*  设置横行分割线 */
            Line[] yLine = new Line[5];
            for (int i = 0; i < 5; i++)
            {
                yLine[i] = new Line();
                yLine[i].X1 = 0;
                yLine[i].Y1 = 0 + i * 40;
                yLine[i].Y2 = 0 + i * 40;
                yLine[i].X1 = 370;
                yLine[i].Stroke = new SolidColorBrush(Colors.LightPink);
                yLine[i].StrokeThickness = 1;
                Canvas.SetLeft(xMon[i], 6 + i * 29);
                Canvas.SetTop(xMon[i], 204);
                year_cvs.Children.Add(yLine[i]);
            }
            yLine[0].StrokeThickness = 4;
        }

        private async void chooseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var year = "2017";
            var month = "5";
            if (chooseList.SelectedValue.ToString().Equals("年"))
            {
                showYear(year);
            } else if (chooseList.SelectedValue.ToString().Equals("月"))
            {
                datePicker.MonthVisible = true;
                showMonth(year, month);
            } else
            {
                var dialog = new MessageDialog("请选择统计方式", "消息提示");
                await dialog.ShowAsync();
            }
            
        }

        private void showMonth(string year, string month)
        {
            throw new NotImplementedException();
        }

        private async void showYear(string year)
        {
            if (AccountViewModel.AllItems != null)
            {
                double[] monthsOut = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                double[] monthsIn = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                double max = 0, index = 0, totalIn = 0, totalOut = 0;
                double pocketmoneyIn = 0;
                double pocketmoneyOut = 0;
                var count = AccountViewModel.AllItems.Count;
                /* 计算各月收支 */
                for (int i = 0; i < count; i++)
                {
                    var now = AccountViewModel.AllItems[i];
                    if (!now.isPocketMoney)
                    {
                        /* 0 表示支出，1表示收入 */
                        if (now.inOrOut)
                        {
                            monthsIn[i] += now.amount;
                            totalIn += now.amount;
                            if (monthsIn[i] > max)
                            {
                                max = monthsIn[i];
                                index = i;
                            }
                        }
                        else
                        {
                            monthsOut[i] += now.amount;
                            totalOut += now.amount;
                            if (monthsOut[i] > max)
                            {
                                max = monthsOut[i];
                                index = i;
                            }
                        }
                    }
                    else
                    {
                        /* 0 表示支出，1表示收入 */
                        if (now.inOrOut)
                        {
                            pocketmoneyIn += now.amount;
                        }
                        else
                        {
                            pocketmoneyOut += now.amount;
                        }
                    }
                }


                /*  绘制条形图: 绘制条形图: 设置百分比，调整高度 预算固定为￥1500 */
               double highest = 1500;
               Line[] month = new Line[12];
               Line[] income = new Line[12];
               for (int i = 0; i < 12; i++)
               {
                   month[i] = new Line();
                   month[i].X1 = month[i].X2 = 10 + i*30;
                   month[i].Y1 = 200;
                   month[i].Y2 = monthsOut[i] > highest ? -(monthsOut[i] - highest) / 75 : 200 - (monthsOut[i] / highest * 200);
                   month[i].Y2 = month[i].Y2 > 196 ? 196 : month[i].Y2;
                   month[i].Stroke = new SolidColorBrush(Colors.Teal); // 水鸭色
                   month[i].StrokeThickness = 10;
                   year_cvs.Children.Add(month[i]);

                   income[i] = new Line();
                   income[i].X1 = income[i].X2 = 10 + i * 30;
                   income[i].Y1 = 200;
                   income[i].Y2 = monthsIn[i] > highest ? -(monthsIn[i] - highest) / 75 : 200 - (monthsIn[i] / highest * 200);
                   income[i].Y2 = income[i].Y2 > 196 ? 196 : income[i].Y2;
                   income[i].Stroke = new SolidColorBrush(Colors.Teal); // 水鸭色
                    income[i].StrokeThickness = 10;
                   year_cvs.Children.Add(income[i]);
                }

               /* 修改文字内容 */
                allIn.Text = "日常总收入" + totalIn.ToString() + "元";
                allOut.Text = "日常总收入" + totalOut.ToString() + "元";
                pcIn.Text = "私房钱总收入" + pocketmoneyIn.ToString() + "元";
                pcOut.Text = "私房钱总支出" + pocketmoneyOut.ToString() + "元";
            }
            else
            {
                var dialog = new MessageDialog("记录为空", "消息提示");
                await dialog.ShowAsync();
            }


        }

        private async void DatePicker_DropCompleted(UIElement sender, DropCompletedEventArgs args)
        {
            if (chooseList.SelectedValue.ToString().Equals("年"))
            {
                var year = datePicker.ToString().Substring(0, 3);
                showYear(year);
            }
            else if (chooseList.SelectedValue.ToString().Equals("月"))
            {
                var year = datePicker.ToString().Substring(0, 3);
                var month = datePicker.ToString().Substring(5, 6);
                datePicker.MonthVisible = true;
                showMonth(year, month);
            }
            else
            {
                var dialog = new MessageDialog("请选择统计方式", "消息提示");
                await dialog.ShowAsync();
            }
        }

        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            datePicker.MaxYear = DateTimeOffset.Now;
        }
    }
}
