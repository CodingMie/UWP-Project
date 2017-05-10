using Newtonsoft.Json;
using ShowMeMyMoney.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowMeMyMoney.ViewModel
{
    class categoryViewModel
    {
        public ObservableCollection<categoryItem> AllCatagoryItems { get { return this.allCatagoryItems; } set { this.allCatagoryItems = value; } }
        public ObservableCollection<categoryItem> allCatagoryItems = new ObservableCollection<categoryItem>();

        public void AddCategoryItem(int index, string name, double _share, string color)
        {
            categoryItem categoryItem = new categoryItem(index, name, _share, color);
            allCatagoryItems.Add(categoryItem);
        }
        public void AddCategoryItem(categoryItem newCategory)
        {
            allCatagoryItems.Add(newCategory);
        }

        public int getCategoryNum(string categoryName)
        {
            foreach (var item in allCatagoryItems)
            {
                if (item.name == categoryName)
                {
                    return item.number;
                }
            }
            return -1;
        }
        private async void initializeCategoryTable()
        {
            try
            {
                /*  读取json文件  */
                var Folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var item = await Folder.TryGetItemAsync("categoryTable.json");
                if (item == null)
                {
                    /* 第一次打开应用, 则无需进行后续操作 */
                    /* 手动添加一个代表总开支的item */
                    allCatagoryItems.Add(new categoryItem(-1, "Total", 100, "Black"));
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
                        allCatagoryItems.Add(i);
                    }
                }
                if (allCatagoryItems[0].number == -1)
                {
                    allCatagoryItems.RemoveAt(0);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async void saveCategoryTable()
        {
            try
            {
                var Folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var file = await Folder.CreateFileAsync("categoryTable.json", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                var data = await file.OpenStreamForWriteAsync();

                using (StreamWriter r = new StreamWriter(data))
                {
                    var serelizedfile = JsonConvert.SerializeObject(allCatagoryItems);
                    r.Write(serelizedfile);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
