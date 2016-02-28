using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookiesAutomat
{
    class Cookie
    {
        private String name;
        private int price;
        private int count;

        public Cookie(String name, int price, int count)
        {
            this.name = name;
            this.price = price;
            this.count = count;
        }

        public void Put(int count) { }

        //public void Take(int count) { }

        public TakeResult Take(int count, ref int money) {
            if (money >= price * count)
            {
                if (count <= this.count)
                {
                    money -= price * count;
                    this.count -= count;
                    return TakeResult.OK;
                }
                else//not enough products
                {
                    return TakeResult.NO_PRODUCT;
                }
            }
            else
            {
                return TakeResult.NO_MONEY;
            }
        }

        public enum TakeResult
        {
            OK, NO_MONEY, NO_PRODUCT
        }

        public int Price
        {
            get
            {
                return this.price;
            }
        }

        public override string ToString()
        {
            return name;
        }
    }
}
