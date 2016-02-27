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

        public void Take(int count) { }

        public bool Take(int count, int money) { return false; } 
    }
}
