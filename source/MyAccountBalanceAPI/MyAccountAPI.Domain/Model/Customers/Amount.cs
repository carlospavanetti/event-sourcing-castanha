﻿namespace MyAccountAPI.Domain.Model.Customers
{
    public class Amount
    {
        public double Value { get; private set; }

        public Amount(double value)
        {
            this.Value = value;
        }

        public static Amount Create(double value)
        {
            return new Amount(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static Amount operator +(Amount amount1, Amount amount2)
        {
            return new Amount(amount1.Value + amount2.Value);
        }

        public static Amount operator -(Amount amount1, Amount amount2)
        {
            return new Amount(amount1.Value - amount2.Value);
        }
    }
}