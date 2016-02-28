using System;
using System.Collections.Generic;
using System.Linq;

namespace CookiesAutomat
{
    /// <summary>
    /// Класс, представляющий модель хранения монет и действия над ними.
    /// </summary>
    class Money
    {
        /// <summary>
        /// Перечисление, представлющее номиналы монет.
        /// </summary>
        enum Coin
        {
            ONE = 1, TWO = 2, FIVE = 5, TEN = 10
        }

        private Dictionary<Coin, int> coins;//Словарь Номинал-Количество для хранения монет

        /// <summary>
        /// Метод, изымающий монету с номиналом value.
        /// </summary>
        /// <param name="value">Номинал монеты.</param>
        /// <returns>Успех операции (false, если такой монеты нет).</returns>
        public bool getCoin(int value)
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
        /// <summary>
        /// Метод, изымающий монеты, переданные в качестве параметра.
        /// Использование метода подразумевает наличие этих монет в данном экземпляре класса Money.
        /// </summary>
        /// <param name="money">Изымаемые монеты. Не изменяется.</param>
        public void removeCoins(Money money)
        {
            for (var i = 0; i < money.coins.Count; i++)
            {
                Coin key = money.coins.Keys.ToArray()[i];
                while (money.coins[key] > 0)
                {
                    getCoin((int)key);
                }
            }
        }

        /// <summary>
        /// Метод, добавляющий монету с номиналом value.
        /// </summary>
        /// <param name="value">Номинал монеты.</param>
        public void addCoin(int value)
        {
            Coin valueCoin = GetCoinByValue(value);
            coins[valueCoin]++;
        }
        /// <summary>
        /// Метод, добавляющий монеты, переданные в качестве параметра.
        /// </summary>
        /// <param name="money">Добавляемые монеты. Из экземпляра-параметра они удаляются.</param>
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
        /// <summary>
        /// Метод, возвращающий экземпляр перечисления по числовому значению номинала.
        /// </summary>
        /// <param name="value">Числовое значение номинала.</param>
        /// <returns>Номинал-перечисление.</returns>
        private static Coin GetCoinByValue(int value)
        {
            return (Coin)value;
        }

        /// <summary>
        /// Конструктор без параметров, инициализирующий количество монет значением 0.
        /// </summary>
        public Money()//set value as 0
        {
            coins = new Dictionary<Coin, int>();
            coins.Add(Coin.ONE, 0);
            coins.Add(Coin.TWO, 0);
            coins.Add(Coin.FIVE, 0);
            coins.Add(Coin.TEN, 0);
        }

        /// <summary>
        /// Метод, случайно инициализирующий количество монет так, чтобы их стоимость равнялась значению параметра.
        /// </summary>
        /// <param name="money">Стоимость монет.</param>
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

        /// <summary>
        /// Свойство-getter, возвращающий стоимость всех монет в экземпляре класса.
        /// </summary>
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

        /// <summary>
        /// Метод, набирающий сумму, равную или наиболее близкую из возможных к значению параметра из существующих монет.
        /// Сначала для экономии времени используется алгоритм жадной упаковки, затем, в случае неуспеха,
        /// алгоритм динамического программирования, обеспечивающий оптимальный результат.
        /// </summary>
        /// <param name="clientDeposit">Набираемая сумма.</param>
        /// <returns>Список взятых монет.</returns>
        public Money getCoins(int clientDeposit)
        {
            Money result = new Money();
            int deposit = clientDeposit;//copy for later dp usage
            //try greedy algorithm
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

            if (clientDeposit > 0)
            {
                //if we can't to return all change with greedy algorithm
                //return all taken coins back
                addCoins(result);
                //and try dynamic programming for more right solution of classical Knapsack problem
                result = getCoinsDP(deposit);
                //now take all coins in result from this money
                removeCoins(result);
            }
            return result;
        }
        /// <summary>
        /// Метод, набирающий монеты стоимостью, наиболее близкой к deposit методом динамического программирования.
        /// Используется в методе public Money getCoins(int clientDeposit).
        /// </summary>
        /// <param name="deposit">Набираемая сумма.</param>
        /// <returns>Список взятых монет.</returns>
        private Money getCoinsDP(int deposit)
        {
            List<int> cs = CoinsDictToList(this.coins);
            int n = cs.Count;
            int[,] M = new int[n, deposit + 1];
            int i, j;
            //table filling
            for (i = 0; i < n; i++)
            {
                for (j = 0; j <= deposit; j++)
                {
                    int coin = cs[i];
                    if (coin > j) M[i, j] = (i == 0) ? 0 : M[i - 1, j];
                    else
                    {
                        if (i == 0) M[i, j] = coin;
                        else
                        {
                            int pr = M[i - 1, j];
                            int now = M[i - 1, j - coin] + coin;
                            M[i, j] = (pr > now) ? pr : now;
                        }
                    }
                }
            }
            //result getting
            Money result = new Money();
            i = n - 1;
            j = deposit;
            while (M[i, j] > 0 && i >= 0)
            {
                if (i == 0)
                {
                    result.addCoin(cs[i]);
                    j -= cs[i];
                }
                else
                {
                    if (M[i, j] == M[i - 1, j]) i--;
                    else
                    {
                        result.addCoin(cs[i]);
                        j -= cs[i];
                        i--;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Вспомогательный метод для private Money getCoinsDP(int deposit).
        /// Преобразует словарь, описывающий монеты в список.
        /// </summary>
        /// <param name="cs">Словарь, содержащий монеты.</param>
        /// <returns>Список, содержащий номиналы монет кратно их количеству в словаре.</returns>
        private List<int> CoinsDictToList(Dictionary<Coin, int> cs)
        {
            List<int> result = new List<int>();
            for (var i = 0; i < cs.Count; i++)
            {
                Coin key = cs.Keys.ToArray()[i];
                for (int j = 0; j < cs[key]; j++)
                {
                    result.Add((int)key);
                }
            }
            return result;
        }
        /// <summary>
        /// Метод, возвращающий количество монет каждого номинала и их общую стоимость.
        /// </summary>
        /// <returns>Строки с информацией о монетах.</returns>
        public override string ToString()
        {
            String s = "";
            foreach (var coin in coins)
            {
                s += String.Format("\tCoin {0}: x{1}\n", coin.Key, coin.Value);
            }
            s += String.Format("Totally {0} roubles", Sum);
            return s;
        }
    }
}
