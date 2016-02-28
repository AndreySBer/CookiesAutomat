using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookiesAutomat
{
    class CookiesAutomat
    {
        private static List<Cookie> cookies;
        private static Consumer client;
        private static Money moneyInside;
        private static int clientDeposit;

        public static void Main(string[] args)
        {

            client = new Consumer();
            clientDeposit = 0;
            Console.WriteLine("\nNew Consumer has been created.\nHis wallet consists of:\n" + client.Money);
            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("\nWhat to do (choose digit)?");
                showMenu();
                key = Console.ReadKey(true);
                callMenu(key);
            }
            while (key.KeyChar != '0');
        }



        public enum menuItem
        {
            Quit, PutMoney, ChooseProduct, TakeChange, CheckCredit, CheckWallet
        }

        private static void showMenu()
        {
            foreach (var item in Enum.GetNames(typeof(menuItem)))
            {
                Console.WriteLine("\t{0} - {1}", (int)Enum.Parse(typeof(menuItem), item), item);
            }
        }

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
                        break;
                    }
            }
        }

        private static void returnChange()
        {
            Money change = moneyInside.getCoins(clientDeposit);
            int changeSum = change.Sum;
            client.takeCoins(change);

            if (changeSum < clientDeposit)
            {
                clientDeposit -= changeSum;

                Console.WriteLine("\nSorry, automat can not gather the necessary sum. Returned {0} roubles. Credit {1} roubles.",
                    changeSum, clientDeposit);

            }
            else
            {
                clientDeposit -= changeSum;
                Console.WriteLine("\nAll change returned succesfully.");
            }
        }

        private static void suggestCookie()
        {
            Console.WriteLine("\nChoose product (input it number) or 0 to Cancel:\n");
            Console.WriteLine("\t0 - *Cancel*");
            for (int i = 1; i <= cookies.Count; i++)
            {
                Console.WriteLine("\t{0} - {1} ~ {2} rub", i, cookies[i - 1], cookies[i - 1].Price);
            }

            int value = Console.ReadKey(true).KeyChar - '1';
            if (value == -1) return;
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
                Console.WriteLine("\nNo such product. Try again.");
            }
        }

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
                    Console.WriteLine("\nNo such coin. Try again.");
                }
            }
            else
            {
                Console.WriteLine("\nNo such coin. Try again.");
            }
        }

        public static Money giveChange(int change)
        {
            return null;
        }

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
