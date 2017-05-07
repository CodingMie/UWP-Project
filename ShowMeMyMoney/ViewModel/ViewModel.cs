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

        private accountItem selectedItem = default(accountItem);
        public accountItem SelectedItem { get { return selectedItem; } set { this.selectedItem = value; } }

        public ObservableCollection<categoryItem> AllCatagoryItem = new ObservableCollection<categoryItem>();
       // public ObservableCollection<string> allCategoryName = new ObservableCollection<string>();
        public DBManager dbManager;
        public ViewModel()
        {
            dbManager = new DBManager();
            AllCatagoryItem.Add(new categoryItem("play", 1, "red"));
      //      public categoryItem(int i, string s, double _share, string c)
        }
        public async void getItemsFromDB(categoryItem ci)
        {
            /*  初始载入时连接到数据库，加载数据 */
        }

        /* public async void AddAccountItem(accountItem item) {
             /*  添加item并插入到数据库  */

        //}
        public async void AddAccountItem(string category, DateTimeOffset date, double amount, 
                            bool isPocketMoney, bool inOrOut, string description)
        {
            int categoryNum=0;
            foreach (categoryItem item in AllCatagoryItem)
            {
                if (item.name == category)
                {
                    categoryNum = item.number;
                }
            }
            accountItem accountItem = new accountItem(categoryNum, date, amount, isPocketMoney, inOrOut, description);
            allItems.Add(accountItem);
            dbManager.InsertIntoDatabase(accountItem);
        }
        public async void AddCategoryItemm(int index, string name, double _share, string color)
        {
            categoryItem categoryItem = new categoryItem(index, name, _share, color);
            AllCatagoryItem.Add(categoryItem);
        }
        public async void AddCategoryItemm(categoryItem newCategory)
        {
            AllCatagoryItem.Add(newCategory);
        }
        public async void RemoveAccountItem(string id)
        {

            /*删除item并同步数据库*/

        }
    }
}
