using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookiesAutomat
{
    class Consumer
    {
        private Money money;

        public Consumer()
        {
            money = new Money(150);
        }

        public bool GiveMoney(int money) { return false; }

        public CookiesAutomat.menuItem ChooseMenuItem() { return 0; }
    }
}
