using System;

namespace CookiesAutomat
{
    /// <summary>
    /// Класс, представляющий данные и поведение печенек в автомате.
    /// </summary>
    class Cookie
    {
        private String name;//название "печеньки"
        private int price;//цена
        private int count;//количество в автомате
        /// <summary>
        /// Конструктор, задающий имя, цену и количество.
        /// </summary>
        /// <param name="name">Название.</param>
        /// <param name="price">Цена.</param>
        /// <param name="count">Количество.</param>
        public Cookie(String name, int price, int count)
        {
            this.name = name;
            this.price = price;
            this.count = count;
        }
        /// <summary>
        /// Метод, выдающий печеньки из автомата, или ошибки, если печенек или денег не хватает.
        /// </summary>
        /// <param name="count">Количество печенек, которое надо взять.</param>
        /// <param name="money">Количество денег клиента в автомате.</param>
        /// <returns>Результат выполнения операции. ОК - печеньки выданы пользователю.</returns>
        public TakeResult Take(int count, ref int money)
        {
            if (money >= price * count)
            {
                if (count <= this.count)
                {
                    money -= price * count;
                    this.count -= count;
                    return TakeResult.OK;
                }
                else//not enough products
                {
                    //у автомата нет столько печенек данного типа, сколько нужно клиенту
                    return TakeResult.NO_PRODUCT;
                }
            }
            else
            {
                //у пользователя нет столько денег, чтобы купить такое количество этик печенек
                return TakeResult.NO_MONEY;
            }
        }
        /// <summary>
        /// Перечисление, представляющий возвращаемый ответ методом Take.
        /// </summary>
        public enum TakeResult
        {
            OK, NO_MONEY, NO_PRODUCT
        }
        /// <summary>
        /// Свойство-getter, возвращающий цену данной печеньки.
        /// </summary>
        public int Price
        {
            get
            {
                return this.price;
            }
        }
        /// <summary>
        /// Возвращает имя печеньки.
        /// </summary>
        /// <returns>Имя печеньки.</returns>
        public override string ToString()
        {
            return name;
        }
    }
}
