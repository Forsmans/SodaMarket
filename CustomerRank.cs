using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMarket2._0
{
    //Customer Rank classes
    //************************************************************************************
    public class CustomerGold : Customer
    {
        public CustomerGold(string username, string password, string rank) : base(username, password, rank)
        {
            Rank = rank;
        }

        public override double GetDiscount()
        {
            return 0.15;
        }

        public override string GetCustomerLevel()
        {
            return "Gold";
        }
    }
    //************************************************************************************
    public class SilverCustomer : Customer
    {
        public SilverCustomer(string username, string password, string rank) : base(username, password, rank)
        {
            Rank = rank;
        }

        public override double GetDiscount()
        {
            return 0.10;
        }

        public override string GetCustomerLevel()
        {
            return "Silver";
        }
    }
    //************************************************************************************
    public class BronzeCustomer : Customer
    {
        public BronzeCustomer(string username, string password, string rank) : base(username, password, rank)
        {
            Rank = rank;
        }

        public override double GetDiscount()
        {
            return 0.05;
        }

        public override string GetCustomerLevel()
        {
            return "Bronze";
        }
    }
}
