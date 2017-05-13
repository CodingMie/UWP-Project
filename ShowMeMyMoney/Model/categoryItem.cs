using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowMeMyMoney.Model
{
    public class categoryItem
    {
        public string name { get; set; }
        public long number { get; set; }
        public string color;
        public double share;/* 分类当前所占百分比 */
        public double amount; /* 分类当前总额 */
        public bool inOrOut;
        /* 0 表示支出，1表示收入*/


        [JsonConstructor]
        public categoryItem(long i, string s, double _share, string c, bool ioo)
        {
            name = s;
            number = Guid.NewGuid().GetHashCode() * (i + 1);
            color = c;
            share = _share;
            amount = 0;
            inOrOut = ioo;
        }
        public categoryItem(string s, long i, string c)
        {
            name = s;
            number = Guid.NewGuid().GetHashCode();
            color = c;
            amount = 0;
        }
    }
}
