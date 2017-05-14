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

        /* 开支表 */
        public ObservableCollection<categoryItem> AllExpenseCatagoryItems { get { return this.allExpenseCatagoryItems; } set { this.allExpenseCatagoryItems = value; } }
        public ObservableCollection<categoryItem> allExpenseCatagoryItems = new ObservableCollection<categoryItem>();

        /* 收入表 */
        public ObservableCollection<categoryItem> AllIncomeCatagoryItems { get { return this.allIncomeCatagoryItems; } set { this.allIncomeCatagoryItems = value; } }
        public ObservableCollection<categoryItem> allIncomeCatagoryItems = new ObservableCollection<categoryItem>();

        private categoryItem selectedCategory = default(categoryItem);
        public categoryItem SelectedCategory { get { return selectedCategory; } set { this.selectedCategory = value; } }

        static bool EXPENSE = false, INCOME = true;

        public double pocketMoneyAmount;

        public categoryViewModel()
        {
            /* 读入本地json文件 */
            initializeCategoryTable();
            /* todo : 从本地读入私房钱数额 */
            pocketMoneyAmount = 800;
        }

        private void initializeCategoryTable()
        {
            readFromTable("ExpenseCategoryTable");
            readFromTable("IncomeCategoryTable");
        }

        public void AddCategoryItem(long index, string name, double _share, string color, bool b)
        {
            categoryItem categoryItem = new categoryItem(index, name, _share, color, b);
            AddCategoryItem(categoryItem);
        }
        public void AddCategoryItem(categoryItem newCategory)
        {
            if (newCategory.inOrOut == EXPENSE)
            {
                allExpenseCatagoryItems.Add(newCategory);
            }
            else
            {
                allIncomeCatagoryItems.Add(newCategory);
            }
        }
        public long getCategoryNum(string categoryName, bool expOrInc)
        {
            var items = expOrInc ? allIncomeCatagoryItems : allExpenseCatagoryItems;
            foreach (var item in items)
            {
                if (item.name.Equals(categoryName))
                {
                    return item.number;
                }
            }
            return -1;
        }
        private async void readFromTable(string nameOfTable)
        {
          //  try
            //{
                /*  读取json文件  */
                var Folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var item = await Folder.TryGetItemAsync(nameOfTable + ".json");
                if (item == null)
                {
                    /* 第一次打开应用, 则无需进行后续操作 */
                    /* 手动添加一个代表总开支的item */
                    return;
                }
                var file = await Folder.GetFileAsync(nameOfTable + ".json");

                var data = await file.OpenReadAsync();

                using (StreamReader r = new StreamReader(data.AsStream()))
                {
                    string text = r.ReadToEnd();

                    /* 如果json是空的，也不能进行后续操作*/
                    if (text == "" || text == "[]") return;
                    /* 将json文件中的东西反序列化，加入到catagoryItem的数组   */
                    categoryItem[] p = JsonConvert.DeserializeObject<categoryItem[]>(text);
                    if (nameOfTable == EXPENSE_TABLE)
                    {
                        foreach (var i in p)
                            allExpenseCatagoryItems.Add(i);
                    }
                    else if (nameOfTable == INCOME_TABLE)
                    {
                        foreach (var i in p)
                            allIncomeCatagoryItems.Add(i);
                    }
                }
         //   }
         /*   catch (Exception e)
            {
                throw e;
            }*/
        }

        internal void UpdateCategoryByAccount(accountItem account, bool addOrDel)
        {
            bool expenseOrIncome = account.inOrOut;
            var items = expenseOrIncome ? allIncomeCatagoryItems : allExpenseCatagoryItems;
            /* 如果是私房钱，不增加开支，只增加收入 */
            if (account.isPocketMoney == true && expenseOrIncome == EXPENSE) return;
            foreach (var i in items)
            {
                 if (account.category == i.number)
                {
                    if (addOrDel)
                    {
                        i.amount += account.amount;
                    }
                    else
                    {
                        i.amount -= account.amount;
                    }
                    saveCategoryTable(expenseOrIncome);
                    return;
                }
            }

        }

        static string EXPENSE_TABLE = "ExpenseCategoryTable";
        static string INCOME_TABLE = "IncomeCategoryTable";
        public async void saveCategoryTable(Boolean incomeOrExpense)
        {
            string nameOfTable = incomeOrExpense ? INCOME_TABLE : EXPENSE_TABLE;
            ObservableCollection<categoryItem> table = incomeOrExpense ? allIncomeCatagoryItems : allExpenseCatagoryItems;
            try
            {
                var Folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var file = await Folder.CreateFileAsync(nameOfTable + ".json", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                var data = await file.OpenStreamForWriteAsync();

                using (StreamWriter r = new StreamWriter(data))
                {
                    var serelizedfile = JsonConvert.SerializeObject(table);
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