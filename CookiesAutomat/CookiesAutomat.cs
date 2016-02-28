using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookiesAutomat
{
    class CookiesAutomat
    {
        //private static Dictionary<String, Cookie> cookies;
        private static List<Cookie> cookies;
        private static Consumer client;
        private static Money moneyInside;
        private static int clientDeposit;

        public static void Main(string[] args)
        {

            client = new Consumer();
            clientDeposit = 0;
            Console.WriteLine("New Consumer has been created.\nHis wallet consists of:\n" + client.Money);
            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("What to do (choose digit)?\n");
                showMenu();
                key = Console.ReadKey(true);
                if (key.KeyChar == '0') continue;
                callMenu(key);
            }
            while (key.KeyChar != '0');
        }



        public enum menuItem
        {
            Quit, PutMoney, ChooseProduct, TakeChange
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
            switch (key.KeyChar)
            {
                case '1':
                    {
                        getMoney();
                        break;
                    }
                case '2':
                    {
                        suggestCookie();
                        break;
                    }
                case '3':
                    {
                        returnChange();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid value typed. Try 0 to 3.");
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

                Console.WriteLine("Sorry, automat can not gather the necessary sum. Returned {0} roubles. Credit {1} roubles.",
                    changeSum, clientDeposit);

            }
            else
            {
                clientDeposit -= changeSum;
                Console.WriteLine("All change returned succesfully.");
            }
        }

        private static void suggestCookie()
        {
            Console.WriteLine("Choose product (input it number):\n");
            for (int i = 0; i < cookies.Count; i++)
            {
                Console.WriteLine("\t{0} - {1} ~ {2} rub", i, cookies[i], cookies[i].Price);
            }

            int value = Console.ReadKey(true).KeyChar - '0';
            if (value >= 0 && value < cookies.Count)
            //if (int.TryParse(Console.ReadLine(), out value) && value >= 0 && value < cookies.Count)

            {
                switch (cookies[value].Take(1, ref clientDeposit))
                {
                    case Cookie.TakeResult.OK:
                        {
                            Console.WriteLine("Product {0} fell out of the machine. Credit {1} roubles.", cookies[value], clientDeposit);
                            break;
                        }
                    case Cookie.TakeResult.NO_MONEY:
                        {
                            Console.WriteLine("Not enough money. Insert coins and try again. Credit {0} roubles.", clientDeposit);
                            break;
                        }
                    case Cookie.TakeResult.NO_PRODUCT:
                        {
                            Console.WriteLine("Sorry, product {0} ended. You can choose another or return money.", cookies[value]);
                            break;
                        }
                }
            }
            else
            {
                Console.WriteLine("No such product. Try again.");
                //suggestCookie();
            }
        }

        private static void getMoney()
        {
            Console.WriteLine("Consumer's wallet consists of:{0}Please choose coin (input it value)", client.Money);
            int value;
            if (int.TryParse(Console.ReadLine(), out value))
            {
                if (client.GiveCoin(value))
                {
                    moneyInside.addCoin(value);
                    clientDeposit += value;
                    Console.WriteLine("Coin \"{0}\" inserted successfully. Credit {1} roubles.", value, clientDeposit);
                }
                else
                {
                    Console.WriteLine("No such coin. Try again.");
                    //getMoney();
                }
            }
            else
            {
                Console.WriteLine("No such coin. Try again.");
                //getMoney();
            }
        }

        /*public static bool giveItem(int position)
        {
            if (cookies[position]>0 && moneyGiven.getSum>=cookies[position].)
        }*/

        public static Money giveChange(int change)
        {
            return null;
        }

        static CookiesAutomat()
        {
            /*cookies = new Dictionary<string, Cookie>();
            cookies.Add("Cupcakes", new Cookie("Cupcakes", 50, 4));
            cookies.Add("Cookies", new Cookie("Cookies", 10, 3));
            cookies.Add("Waffles", new Cookie("Waffles", 30, 10));*/

            cookies = new List<Cookie>();
            cookies.Add(new Cookie("Cupcakes", 50, 4));
            cookies.Add(new Cookie("Cookies", 10, 3));
            cookies.Add(new Cookie("Waffles", 30, 10));

            moneyInside = new Money();
        }
    }
}
