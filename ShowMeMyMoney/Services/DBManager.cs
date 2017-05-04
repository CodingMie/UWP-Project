using System;
using System.Collections.Generic;
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
             Practically this TABLE is UNCOMPLETED as
             this db does not include the field of 'completed'.
             Technically, it should be included.
             
             */
            string sql = @"CREATE TABLE IF NOT EXISTS
                          accounts  (Id      VARCHAR(30) PRIMARY KEY NOT NULL,
                                    description    VARCHAR( 400 ),
                                    createDate VARCHAR( 30 ),
                    );";
            /* SQL  not complete yet */
            using (var statement = App.conn.Prepare(sql))
            {
                statement.Step();
            }
        }
    }
}
