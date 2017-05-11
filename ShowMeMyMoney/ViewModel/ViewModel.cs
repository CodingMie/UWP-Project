using ShowMeMyMoney.Model;
using ShowMeMyMoney.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowMeMyMoney.ViewModel
{
    public class ViewModel
    {
        private ObservableCollection<accountItem> allItems = new ObservableCollection<accountItem>();
        public ObservableCollection<accountItem> AllItems { get { return this.allItems; } set { this.allItems = value; } }

        private accountItem selectedItem = default(accountItem);
        public accountItem SelectedItem { get { return selectedItem; } set { this.selectedItem = value; } }

        public ObservableCollection<categoryItem> AllCatagoryItem = new ObservableCollection<categoryItem>();
        public ObservableCollection<string> allCategoryName = new ObservableCollection<string>();

        private categoryItem selectedCategory = default(categoryItem);
        public categoryItem SelectedCategory { get { return selectedCategory; } set { this.selectedCategory = value; } }


        public DBManager dbManager;
        public ViewModel()
        {
            dbManager = new DBManager(); 
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
            /*
                add to database
             */
        }

        public async void RemoveAccountItem(string id)
        {

            /*删除item并同步数据库*/

        }

        public ObservableCollection<categoryItem> getItemsByCategory(categoryItem c)
        {

            /* 获得某分类的全部item */
            return null;

        }

        public categoryItem getCategoryByNumber(int num)
        {
            /* 由编号获得category */
            return null;
        }
    }
}
