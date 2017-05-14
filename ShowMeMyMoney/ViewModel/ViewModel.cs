using ShowMeMyMoney.Model;
using ShowMeMyMoney.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace ShowMeMyMoney.ViewModel
{
    public class ViewModel
    {
        private ObservableCollection<accountItem> allItems = new ObservableCollection<accountItem>();
        public ObservableCollection<accountItem> AllItems { get { return this.allItems; } set { this.allItems = value; } }

        private ObservableCollection<accountItem> displayItems = new ObservableCollection<accountItem>();
        public ObservableCollection<accountItem> DisplayItems { get { return this.displayItems; } set { this.displayItems = value; } }

        private accountItem selectedItem = default(accountItem);
        public accountItem SelectedItem { get { return selectedItem; } set { this.selectedItem = value; } }
        public DBManager dbManager;

        public ViewModel()
        {
            dbManager = new DBManager();

            /* 从数据库读取全部items */
            if (allItems.Count == 0)
            {
                allItems = dbManager.LoadAllItems();
            }
        }

        /* 从allItems中选出特定类别的items */
        public void queryDisplayItems(categoryItem ci)
        {
            displayItems.Clear();
            for (int i = 0; i < allItems.Count; i++)
            {
                if (allItems[i].category == ci.number)
                {
                    displayItems.Add(allItems[i]);
                }
            }
        }

        /* public async void AddAccountItem(accountItem item) {
             /*  添加item并插入到数据库  */

        //}
        public async void AddAccountItem(long categoryNum, DateTimeOffset date, double amount,
                            bool isPocketMoney, bool inOrOut, string description)
        {
            accountItem accountItem = new accountItem(categoryNum, date, amount, isPocketMoney, inOrOut, description);
            allItems.Add(accountItem);
            dbManager.InsertIntoDatabase(accountItem);
        }

        public async void RemoveAccountItem(string id)
        {
            foreach (var item in displayItems)
            {
                if (item.id == id)
                {
                    displayItems.Remove(item);
                    allItems.Remove(item);
                    dbManager.DeleteItemInDatabase(id);
                    break;
                }
            }
        }
    }
}
