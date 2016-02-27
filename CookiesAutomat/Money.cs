using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookiesAutomat
{
    class Money
    {
        enum Coin
        {
            ONE=1,TWO=2,FIVE=5,TEN=10
        }

        private Dictionary<Coin, int> coins;

        public int getSumm()
        {
            return 0;
        }

        public void addSumm(int summ)
        {

        }

        public Money(int money=0)
        {
            coins = new Dictionary<Coin, int>();
            coins.Add(Coin.ONE, 0);
            coins.Add(Coin.TWO, 0);
            coins.Add(Coin.FIVE, 0);
            coins.Add(Coin.TEN, 0);
        }
    }
}
