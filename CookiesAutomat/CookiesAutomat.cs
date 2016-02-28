using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookiesAutomat
{
    class CookiesAutomat
    {
        private static Dictionary<String, Cookie> cookies;
        private static Consumer client;
        private static Money moneyGiven;

        public static void Main(string[] args)
        {

            client = new Consumer();
            Console.WriteLine("New Consumer has been created.\nHis wallet consists of:\n" + client.Money);
            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("What he will do (choose digit)?\n");
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
                        returnMoney();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid value typed. Try 0 to 3.");
                        break;
                    }
            }
        }

        private static void returnMoney()
        {
            throw new NotImplementedException();
        }

        private static void suggestCookie()
        {
            throw new NotImplementedException();
        }

        private static void getMoney()
        {
            Console.WriteLine("Consumer's wallet consists of:{0} Please choose coin (input it value)", client.Money);
            int value;
            if (int.TryParse(Console.ReadLine(), out value))
            {
                if (client.GiveCoin(value))
                {
                    moneyGiven.addCoin(value);
                    Console.WriteLine("Coin \"{0}\" inserted successfully", value);
                }
                else
                {
                    Console.WriteLine("No such coin. Try again.");
                    getMoney();
                }
            }
            else
            {
                Console.WriteLine("No such coin. Try again.");
                getMoney();
            }
        }

        public static bool giveItem(String name, int count, int money)
        {
            return false;
        }

        public static Money giveChange(int change)
        {
            return null;
        }

        static CookiesAutomat()
        {
            cookies = new Dictionary<string, Cookie>();
            cookies.Add("Cupcakes", new Cookie("Cupcakes", 50, 4));
            cookies.Add("Cookies", new Cookie("Cookies", 10, 3));
            cookies.Add("Waffles", new Cookie("Waffles", 30, 10));

            moneyGiven = new Money();
        }
    }
}
