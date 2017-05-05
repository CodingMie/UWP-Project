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
        public string name;
        public int number;
        public string color;
        public double share;/* 分类当前所占百分比 */
        [JsonConstructor]
        public categoryItem(int i, string s,  double _share, string c)
        {
            name = s;
            number = i;
            color = c;
            share = _share;
        }
        public categoryItem(string s, int i, string c)
        {
            name = s;
            number = i;
            color = c; 
        }
    }
}
