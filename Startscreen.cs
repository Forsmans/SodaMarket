using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMarket2._0
{
    public static class Startscreen
    {
        //Start methods and graphic header
        //************************************************************************************
        public static void Welcome()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("************************************************************************************");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                    WELCOME TO THE");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(@"███████  ██████  ██████   █████  ███    ███  █████  ██████  ██   ██ ███████ ████████ 
██      ██    ██ ██   ██ ██   ██ ████  ████ ██   ██ ██   ██ ██  ██  ██         ██    
███████ ██    ██ ██   ██ ███████ ██ ████ ██ ███████ ██████  █████   █████      ██    
     ██ ██    ██ ██   ██ ██   ██ ██  ██  ██ ██   ██ ██   ██ ██  ██  ██         ██    
███████  ██████  ██████  ██   ██ ██      ██ ██   ██ ██   ██ ██   ██ ███████    ██    ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("************************************************************************************");
            Console.ResetColor();
            Console.WriteLine();
        }

        //************************************************************************************
        public static void Start()
        {
            string username;
            string password;
            string rank;

            (username, password, rank) = Customer.LoginMenu();

            Customer customer;

            switch (rank.ToLower())
            {
                case "gold":
                    customer = new CustomerGold(username, password, rank);
                    break;
                case "silver":
                    customer = new SilverCustomer(username, password, rank);
                    break;
                case "bronze":
                    customer = new BronzeCustomer(username, password, rank);
                    break;
                default:
                    customer = new Customer(username, password, rank); 
                    break;
            }

            Store store = new Store();

            store.StoreMenu(customer);
        }
    }

}
