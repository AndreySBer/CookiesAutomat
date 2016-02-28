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

        public bool getCoin(int value)//to add check of value of coin on exsistance
        {
            Coin valueCoin = GetCoinByValue(value);
            if (coins.ContainsKey(valueCoin) && coins[valueCoin]>0)
            {
                coins[valueCoin]--;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void addCoin(int value)
        {
            Coin valueCoin = GetCoinByValue(value);
            coins[valueCoin]++;
        }

        private Coin GetCoinByValue(int value) 
        {
            return (Coin)value;
        }

        public Money()//set value as 0
        {
            coins = new Dictionary<Coin, int>();
            coins.Add(Coin.ONE, 0);
            coins.Add(Coin.TWO, 0);
            coins.Add(Coin.FIVE, 0);
            coins.Add(Coin.TEN, 0);
        }

        public void generateRandomCoins(int money = 0)
        {
            Random rnd = new Random();
            int count = rnd.Next(0, (money + 1) / (int)Coin.TEN);
            coins[Coin.TEN] = count;
            //coins.Add(Coin.TEN, count);
            money -= count* (int)Coin.TEN;

            count = rnd.Next(0, (money + 1) / (int)Coin.FIVE);
            coins[Coin.FIVE] = count;
            //coins.Add(Coin.FIVE, count);
            money -= count*(int)Coin.FIVE;

            count = rnd.Next(0, (money + 1) / (int)Coin.TWO);
            coins[Coin.TWO] = count;
            //coins.Add(Coin.TWO, count);
            money -= count * (int)Coin.TWO;

            coins[Coin.ONE] = money;
            //coins.Add(Coin.ONE, money);
        }

        public int Sum
        {
            get
            {
                int sum = 0;
                foreach (var i in coins)
                {
                    sum += i.Value * (int)i.Key;
                }
                return sum;
            }
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
