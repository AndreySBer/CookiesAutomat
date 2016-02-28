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
            ONE = 1, TWO = 2, FIVE = 5, TEN = 10
        }

        private Dictionary<Coin, int> coins;

        public bool getCoin(int value)//to add check of value of coin on exsistance
        {
            Coin valueCoin = GetCoinByValue(value);
            if (coins.ContainsKey(valueCoin) && coins[valueCoin] > 0)
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
        public void addCoins(Money money)
        {
            for (var i = 0; i < money.coins.Count; i++)
            {
                Coin key = money.coins.Keys.ToArray()[i];
                while (money.coins[key] > 0)
                {
                    addCoin((int)key);
                    money.coins[key]--;
                }
            }
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
            money -= count * (int)Coin.TEN;

            count = rnd.Next(0, (money + 1) / (int)Coin.FIVE);
            coins[Coin.FIVE] = count;
            money -= count * (int)Coin.FIVE;

            count = rnd.Next(0, (money + 1) / (int)Coin.TWO);
            coins[Coin.TWO] = count;
            money -= count * (int)Coin.TWO;

            coins[Coin.ONE] = money;
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

        public Money getCoins(int clientDeposit)
        {
            Money result = new Money();
            while (clientDeposit >= (int)Coin.TEN && getCoin((int)Coin.TEN))
            {
                clientDeposit -= (int)Coin.TEN;
                result.addCoin((int)Coin.TEN);
            }
            while (clientDeposit >= (int)Coin.FIVE && getCoin((int)Coin.FIVE))
            {
                clientDeposit -= (int)Coin.FIVE;
                result.addCoin((int)Coin.FIVE);
            }
            while (clientDeposit >= (int)Coin.TWO && getCoin((int)Coin.TWO))
            {
                clientDeposit -= (int)Coin.TWO;
                result.addCoin((int)Coin.TWO);
            }
            while (clientDeposit >= (int)Coin.ONE && getCoin((int)Coin.ONE))
            {
                clientDeposit -= (int)Coin.ONE;
                result.addCoin((int)Coin.ONE);
            }
            return result;
        }

        public override string ToString()
        {
            String s = "";
            foreach (var coin in coins)
            {
                s += String.Format("\tCoin {0}: x{1}\n", coin.Key, coin.Value);
            }
            return s;
        }
    }
}
