using ShowMeMyMoney.Model;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowMeMyMoney.Services
{
    public class CategoryTableManager
    {
        public CategoryTableManager()
        {
            LoadDatabase();
        }
        private void LoadDatabase()
        {
            // Get a reference to the SQLite database
            /*
             Practically this TABLE is UNCOMPLETED  
             */
            string sql = @"CREATE TABLE IF NOT EXISTS
                   categories  (number      VARCHAR(30) PRIMARY KEY NOT NULL,
                                   name VARCHAR(100),
                                    color VARCHAR(20),
                                    share REAL,
                                    amount REAL,
                                    inOrOut BOOLEAN 
                    );";
            /* SQL  not complete yet */
            using (var statement = App.conn.Prepare(sql))
            {
                statement.Step();
            }
        }
        // UPDATE
        public void UpdateItemInDatabase(categoryItem item)
        {
            // See if the customer already exists
            var exist = SearchDatabaseByNumber(item.number);

            var db = App.conn;
            if (exist == true)
            {
                using (var todostmt = db.Prepare("UPDATE categories SET name = ?, color = ?, share = ?, amount = ?, inOrOut = ?  WHERE number =?"))
                {
                    // NOTE when using anonymous parameters the first has an index of 1, not 0.  

                    todostmt.Bind(1, item.name);
                    todostmt.Bind(2, item.color);
                    todostmt.Bind(3, item.share);
                    todostmt.Bind(4, item.amount);
                    todostmt.Bind(5, Convert.ToInt16(item.inOrOut));
                    todostmt.Bind(6, item.number);
                    todostmt.Step();
                }
            }

        }
        public void InsertIntoDatabase(categoryItem item)
        {
            var db = App.conn;
            string SQLstmt = @"INSERT INTO categories (number, name, color, share, amount, inOrOut)" +
                " VALUES(?,?,?,?,?,?)";

            try
            {
                using (var todostmt = db.Prepare(SQLstmt))
                {
                    int i = 1;
                    todostmt.Bind(i++, item.number);
                    todostmt.Bind(i++, item.name);
                    todostmt.Bind(i++, item.color);
                    todostmt.Bind(i++, item.share);
                    todostmt.Bind(i++, item.amount);
                    todostmt.Bind(i++, Convert.ToInt16(item.inOrOut));
                    var a = todostmt.Step();
                    var b = 1;
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle error
            }
        }

        // QUERY BY ID
        public bool SearchDatabaseByNumber(long idOfItem)
        {
            var dbconn = App.conn;
            categoryItem item = null;
            string SQLstmt = "SELECT * FROM categories WHERE number = ?";
            using (var statement = dbconn.Prepare(SQLstmt))
            {
                statement.Bind(1, idOfItem);

                if (SQLiteResult.DONE == statement.Step())
                {
                    return true;
                }
            }
            return false;

        }
        // 返回所有项
        public List<categoryItem> GetWholeTable()
        {
            //  返回的TodoItem是不完整的：没有filePath 和 BitmapImage 对象
            var dbconn = App.conn;
            List<categoryItem> itemList = new List<categoryItem>();
            string SQLstmt = "SELECT * FROM categories";
 
            using (var statement = dbconn.Prepare(SQLstmt))
            {
                /*if (!wildcardFlag)
                statement.Bind(1, ti);*/

                while (statement.Step() == SQLiteResult.ROW)
                {
                    int i = 0;
                    var number = (long)statement[i++]; 
                    var name = (string)statement[i++];
                    var color = (string)statement[i++];
                    var share = (double)statement[i++];
                    var amount = (double)statement[i++];
                    var inOrOut = (bool)statement[i++];

                    categoryItem item = new categoryItem(number, name, share, color, inOrOut);
                    item.number = number;
                    item.amount = amount;
                    
                    itemList.Add(item);

                }
            }
            return itemList;


        }
    }
}
