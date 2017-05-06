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


        public DBManager dbManager;
        public ViewModel()
        {
            dbManager = new DBManager(); 
        }
        public async void getItemsFromDB(categoryItem ci)
        {
            /*  初始载入时连接到数据库，加载数据 */
        }

        public async void AddAccountItem(accountItem item) {
            /*  添加item并插入到数据库  */
        }
        public async void RemoveAccountItem(string id)
        {

            /*删除item并同步数据库*/

        }
    }
}
