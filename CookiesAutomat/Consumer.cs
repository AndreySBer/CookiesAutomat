using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookiesAutomat
{
    class Consumer
    {
        private Money money;

        public Consumer()
        {
            money = new Money();
            money.generateRandomCoins(150);
        }

        public bool GiveCoin(int value)
        {
            return money.getCoin(value);
        }

        public void takeCoins(Money coins)
        {
            money.addCoins(coins);
        }

        //public CookiesAutomat.menuItem ChooseMenuItem() { return 0; }

        public String Money
        {
            get { return money.ToString(); }
        }
    }
}
