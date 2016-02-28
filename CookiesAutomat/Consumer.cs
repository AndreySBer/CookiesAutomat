using System;

namespace CookiesAutomat
{
    /// <summary>
    /// Класс, представляющий данные и поведение клиента автомата. 
    /// </summary>
    class Consumer
    {
        private Money money;//деньги клиента

        /// <summary>
        /// Конструктор, инициализирующий деньги и генерирующий случайный набор монет.
        /// </summary>
        public Consumer()
        {
            money = new Money();
            // генерирует случайный набор монет, составляющий в сумме 150
            money.generateRandomCoins(150);
        }

        /// <summary>
        /// Метод, отвечающий за передачу денег автомату.
        /// </summary>
        /// <param name="value">Количество денег.</param>
        /// <returns>Возвращает успешна ли операция (false, если нет запрашиваемого количества денег).</returns>
        public bool GiveCoin(int value)
        {
            return money.getCoin(value);
        }

        /// <summary>
        /// Метод, отвечающий за взятие денег из автомата.
        /// </summary>
        /// <param name="coins">Взимаемые монеты.</param>
        public void takeCoins(Money coins)
        {
            money.addCoins(coins);
        }

        /// <summary>
        /// Метод, возвращающий строковое описание монет, которые есть у клиента, для последующего отображения в консоли.
        /// </summary>
        public String Money
        {
            get { return money.ToString(); }
        }
    }
}
