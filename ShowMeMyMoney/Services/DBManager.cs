using ShowMeMyMoney.Model;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowMeMyMoney.Services
{
    public class DBManager
    {
        public DBManager()
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
                          accounts  (Id      VARCHAR(30) PRIMARY KEY NOT NULL,
                                amount   REAL,
                                createDate VARCHAR( 30 ),
                                category INTEGER,
                                isPocketMoney BOOLEAN,
                                inOrOut BOOLEAN,
                                description    VARCHAR( 400 )
                    );";
            /* SQL  not complete yet */
            using (var statement = App.conn.Prepare(sql))
            {
                statement.Step();
            }
        }

        public void InsertIntoDatabase(Model.accountItem item)
        {
            var db = App.conn;
            string SQLstmt = @"INSERT INTO accounts (Id, amount, createDate, "
                + "category, isPocketMoney,inOrOut,"
                + "description) VALUES (?, ?, ?, ?, ?, ?, ?)";
            try
            {
                using (var todostmt = db.Prepare(SQLstmt))
                {
                    todostmt.Bind(1, item.id);
                    todostmt.Bind(2, item.amount);
                    todostmt.Bind(3, item.createDate.ToString());
                    todostmt.Bind(4, item.category);
                    todostmt.Bind(5, Convert.ToInt16(item.isPocketMoney));
                    todostmt.Bind(6, Convert.ToInt16(item.inOrOut));
                    todostmt.Bind(7, item.description);
                    var a = todostmt.Step();
                    var b = 1;
                }
            }
            catch (Exception ex)
            {
                // TODO: Handle error
            }
        }
        public void DeleteItemInDatabase(string idOfItem)
        {
            var db = App.conn;
            using (var statement = db.Prepare("DELETE FROM accounts WHERE Id = ?"))
            {
                statement.Bind(1, idOfItem);
                statement.Step();
            }
        }
        // QUERY BY categoryNumber
        public List<accountItem> SearchDatabaseById(long numberOfCategory)
        {
            var dbconn = App.conn;
            List<accountItem> li = new List<accountItem>();
            using (var statement = dbconn.Prepare("SELECT * FROM accounts WHERE category = ?"))
            {
                statement.Bind(1, numberOfCategory);

                while (statement.Step() == SQLiteResult.ROW)
                {
                    accountItem i = new accountItem((string)statement[0]);


/*<<<<<<< HEAD
                    int k = 0;
=======*/
                    int k = 1;
//>>>>>> 714ed49f59e4e0edee427eaacafa0d48b29c3316
                    i.amount = (double)statement[k++];
                    i.createDate = DateTimeOffset.Parse((string)statement[k++]);
                    i.category = (long)statement[k++];
                    i.isPocketMoney = ((long)statement[k++] == 0)?false:true;
                    i.inOrOut = ((long)statement[k++] == 0) ? false : true;
                    i.description = (string)statement[k++];


                    li.Add(i);
                }
            }
            return li;


        }

        public ObservableCollection<accountItem> LoadAllItems()
        {
            var dbconn = App.conn;
            ObservableCollection<accountItem> li = new ObservableCollection<accountItem>();
            using (var statement = dbconn.Prepare("SELECT * FROM accounts"))
            {

                while (statement.Step() == SQLiteResult.ROW)
                {
                    accountItem i = new accountItem((string)statement[0]);


                    /*<<<<<<< HEAD
                                        int k = 0;
                    =======*/
                    int k = 1;
                    //>>>>>> 714ed49f59e4e0edee427eaacafa0d48b29c3316
                    i.amount = Math.Abs((double)statement[k++]);
                    i.createDate = DateTimeOffset.Parse((string)statement[k++]);
                    i.category = (long)statement[k++];
                    i.isPocketMoney = ((long)statement[k++] == 0) ? false : true;
                    i.inOrOut = ((long)statement[k++] == 0) ? false : true;
                    i.description = (string)statement[k++];


                    li.Add(i);
                }
            }
            return li;
        }
    }
}
