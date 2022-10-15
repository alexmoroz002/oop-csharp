﻿namespace Shops.Entities;
public class Person
{
    private decimal _money;

    public Person(decimal money)
    {
        Money = money;
    }

    public decimal Money
    {
        get => _money;
        private set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            _money = value;
        }
    }

    internal void DeductMoney(decimal price) => Money -= price;
}
