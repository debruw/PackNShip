using System;

namespace GameTemplate.Systems.Currencies
{
    public class CurrencyArgs : EventArgs
    {
        public int currencyId;
        public int changeAmount;

        public CurrencyArgs(int currencyId, int changeAmount)
        {
            this.currencyId = currencyId;
            this.changeAmount = changeAmount;
        }
    }
}

