using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMarket2._0
{

    public class StoreItem
    {
        //Properties
        //************************************************************************************
        public string Name { get; }
        public double PriceSEK { get; }
        public double PriceEUR { get; }
        public double PriceUSD { get; }

        //Constructor
        //************************************************************************************

        public StoreItem(string name, double priceSEK, double priceEUR, double priceUSD)
        {
            Name = name;
            PriceSEK = priceSEK;
            PriceEUR = priceEUR;
            PriceUSD = priceUSD;
        }
    }
    //************************************************************************************
    public enum Currency
    {
        SEK,
        EUR,
        USD
    }
}

