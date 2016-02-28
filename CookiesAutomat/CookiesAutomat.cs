using System;
using System.Collections.Generic;

namespace CookiesAutomat
{
    /// <summary>
    /// Класс CookiesAutomat,
    /// Основной класс программы,
    /// Эмулирует работу торгового аппарата с печеньками,
    /// Обеспечивает взаимодействие между пользователем, покупателем и печеньками.
    /// </summary>
    class CookiesAutomat
    {

        private static List<Cookie> cookies;//список доступных видов печенек
        private static Consumer client; //клиент, с которым работает аппарат в данный момент
        private static Money moneyInside; //монеты, находящиеся в аппарате
        private static int clientDeposit; //количество денег клиента, хранящихся в аппарате (депозит)

        /// <summary>
        /// Метод, с которого начинается работа программы.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        public static void Main(string[] args)
        {
            //создаем текущего клиента, обнуляем его депозит
            client = new Consumer();
            clientDeposit = 0;
            Console.WriteLine("\nNew Consumer has been created.\nHis wallet consists of:\n" + client.Money);
            //показываем меню
            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("\nWhat to do (choose digit)?");
                showMenu();
                key = Console.ReadKey(true);
                //выполняем действие после выбора пункта меню
                callMenu(key);
            }
            while (key.KeyChar != '0');
        }


        /// <summary>
        /// Перечисление для пунктов основного меню.
        /// </summary>
        public enum menuItem
        {
            Quit, PutMoney, ChooseProduct, TakeChange, CheckCredit, CheckWallet
        }

        /// <summary>
        /// Отображает пункты меню в консоли.
        /// </summary>
        private static void showMenu()
        {
            foreach (var item in Enum.GetNames(typeof(menuItem)))
            {
                Console.WriteLine("\t{0} - {1}", (int)Enum.Parse(typeof(menuItem), item), item);
            }
        }
        /// <summary>
        /// Выполняет действие, соответсвующее нажатой клавише.
        /// </summary>
        /// <param name="key">Нажатая клавиша.</param>
        private static void callMenu(ConsoleKeyInfo key)
        {
            switch (key.KeyChar - '0')
            {
                case (int)menuItem.Quit: return;
                case (int)menuItem.PutMoney:
                    {
                        getMoney();
                        break;
                    }
                case (int)menuItem.ChooseProduct:
                    {
                        suggestCookie();
                        break;
                    }
                case (int)menuItem.TakeChange:
                    {
                        returnChange();
                        break;
                    }
                case (int)menuItem.CheckCredit:
                    {
                        Console.WriteLine("\nCredit: {0}", clientDeposit);
                        break;
                    }
                case (int)menuItem.CheckWallet:
                    {
                        Console.WriteLine("\nClient's wallet consists of:{0}\n", client.Money);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("\nInvalid value typed. Try 0 to 5.");
                        //исключительная ситуация, когда у автомата нет выбранного пункта меню (соответсвующего нажатой клавише)
                        break;
                    }
            }
        }

        /// <summary>
        /// Метод, возвращающий сдачу клиенту
        /// </summary>
        private static void returnChange()
        {
            Money change = moneyInside.getCoins(clientDeposit);
            int changeSum = change.Sum;
            client.takeCoins(change);

            if (changeSum < clientDeposit)
            {
                clientDeposit -= changeSum;

                Console.WriteLine("\nSorry, automat can not gather the necessary sum. Returned {0} roubles. Credit {1} roubles.",
                    changeSum, clientDeposit);//исключительная ситуация, когда у автомата не хватает монет на сдачу
            }
            else
            {
                clientDeposit -= changeSum;
                Console.WriteLine("\nAll change: {0} roubles returned succesfully.", changeSum);
            }
        }
        /// <summary>
        /// Метод, отображающий список печенек и позволяющий их достать
        /// </summary>
        private static void suggestCookie()
        {
            Console.WriteLine("\nChoose product (input it number) or 0 to Cancel:\n");
            Console.WriteLine("\t0 - *Cancel*");
            //отображение списка
            for (int i = 1; i <= cookies.Count; i++)
            {
                Console.WriteLine("\t{0} - {1} ~ {2} rub", i, cookies[i - 1], cookies[i - 1].Price);
            }

            int value = Console.ReadKey(true).KeyChar - '1';
            if (value == -1) return;//обработка пункта меню Cancel
            if (value >= 0 && value < cookies.Count)
            {
                switch (cookies[value].Take(1, ref clientDeposit))
                {
                    case Cookie.TakeResult.OK:
                        {
                            Console.WriteLine("\nProduct {0} fell out of the machine. Credit {1} roubles.", cookies[value], clientDeposit);
                            break;
                        }
                    case Cookie.TakeResult.NO_MONEY:
                        {
                            Console.WriteLine("\nNot enough money. Insert coins and try again. Credit {0} roubles.", clientDeposit);
                            break;
                        }
                    case Cookie.TakeResult.NO_PRODUCT:
                        {
                            Console.WriteLine("\nSorry, product {0} ended. You can choose another or return money.", cookies[value]);
                            break;
                        }
                }
            }
            else
            {
                //исключительная ситуация, когда у автомата нет выбранного пункта меню
                Console.WriteLine("\nNo such product. Try again.");
            }
        }
        /// <summary>
        /// Метод, получающий монеты от клиента
        /// </summary>
        private static void getMoney()
        {
            Console.WriteLine("\nConsumer's wallet consists of:{0}Please choose coin (input it value)", client.Money);
            int value;
            if (int.TryParse(Console.ReadLine(), out value))
            {
                if (client.GiveCoin(value))
                {
                    moneyInside.addCoin(value);
                    clientDeposit += value;
                    Console.WriteLine("\nCoin \"{0}\" inserted successfully. Credit {1} roubles.", value, clientDeposit);
                }
                else
                {
                    //исключительная ситуация, когда у человека в кошельке нет монеты такого номинала
                    Console.WriteLine("\nNo such coin. Try again.");
                }
            }
            else
            {
                //нет такого номинала монет
                Console.WriteLine("\nNo such coin. Try again.");
            }
        }

        /// <summary>
        /// Статический конструктор.
        /// Наполняет автомат определенными видами печенек, инициализирует содержащиеся внутри монеты.
        /// </summary>
        static CookiesAutomat()
        {

            cookies = new List<Cookie>();
            cookies.Add(new Cookie("Cupcakes", 50, 4));
            cookies.Add(new Cookie("Cookies", 10, 3));
            cookies.Add(new Cookie("Waffles", 30, 10));

            moneyInside = new Money();
        }
    }
}
