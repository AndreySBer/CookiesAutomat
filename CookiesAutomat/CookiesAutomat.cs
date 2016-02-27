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

        public static void Main(string[] args)
        {
            Consumer client = new Consumer();
            Console.WriteLine("New Consumer has been created.\nHis wallet consists of:\n" + client.Money);

            Console.ReadKey();
        }

        public enum menuItem
        {
            
        }

        public static bool getItem(String name, int count, int money)
        {
            return false;
        }

        public static Money getChange(int change)
        {
            return null;
        }
        
        static CookiesAutomat()
        {
            cookies = new Dictionary<string, Cookie>();
            cookies.Add("Cupcakes", new Cookie("Cupcakes", 50, 4));
            cookies.Add("Cookies", new Cookie("Cookies", 10, 3));
            cookies.Add("Waffles", new Cookie("Waffles", 30, 10));
        }
    }
}
