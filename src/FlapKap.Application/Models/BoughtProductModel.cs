using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Application.Models
{
    public class BoughtProductModel
    {
        public double TotalCost { get; set; }
        public int ProductBoughtCount { get; set; }
        public ChangeCoinModel Change { get; set; }
        public ProductModel Product { get; set; }
    }

    public class ChangeCoinModel
    {
        public ChangeCoinModel()
        {
            Coins = new Dictionary<int, long>();
        }
        public Dictionary<int, long> Coins { get; set; }
    }
}
