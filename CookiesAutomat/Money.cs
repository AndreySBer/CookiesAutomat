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

        public Money()
        {
            coins = new Dictionary<Coin, int>();
            //coins.Add(Coin.ONE, 0);
            //coins.Add(Coin.TWO, 0);
            //coins.Add(Coin.FIVE, 0);
            //coins.Add(Coin.TEN, 0);
        }

        public void generateRandomCoins(int money = 0)
        {
            Random rnd = new Random();
            int count = rnd.Next(0, (money + 1) / (int)Coin.TEN);
            coins.Add(Coin.TEN, count);
            money -= count* (int)Coin.TEN;

            count = rnd.Next(0, (money + 1) / (int)Coin.FIVE);
            coins.Add(Coin.FIVE, count);
            money -= count*(int)Coin.FIVE;

            count = rnd.Next(0, (money + 1) / (int)Coin.TWO);
            coins.Add(Coin.TWO, count);
            money -= count * (int)Coin.TWO;

            coins.Add(Coin.ONE, money);
        }

        public override string ToString()
        {
            String s = "";
            foreach(var coin in coins)
            {
                s += String.Format("\tCoin {0}: x{1}\n",coin.Key,coin.Value);
            }
            return s;
        }
    }
}
