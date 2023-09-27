using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMarket2._0
{

    public class Customer
    {
        //Fields
        //************************************************************************************
        private string _username;
        private string _password;
        private List<StoreItem> _cart;
        private string _rank;
        //Constructor
        //************************************************************************************
        public Customer(string username, string password,string rank)
        {
            _username = username;
            _password = password;
            _cart = new List<StoreItem>();
            _rank = rank;
        }
        //Properties
        //************************************************************************************
        public string Username { get { return _username; } set { _username = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public List<StoreItem> Cart { get { return _cart; } }
        public string Rank { get { return _rank; } set { _rank = value; } }
        //Methods
        //************************************************************************************
        public void AddToCart(StoreItem item)
        {
            _cart.Add(item);
        }
        //************************************************************************************
        public void ClearCart()
        {
            _cart.Clear();
        }
        //************************************************************************************
        public virtual double GetDiscount()
        {
            return 0;
        }
        //************************************************************************************
        public virtual string GetCustomerLevel()
        {
            return _rank;
        }
        //************************************************************************************
        public static (string Username, string Password, string Rank) LoginMenu()
        {
            
            bool loggedIn = false;
            string username = "";
            string password = "";
            string rank = "";

            while (!loggedIn)
            {
                Console.Clear();
                Startscreen.Welcome();
                Console.WriteLine("\nDo you want to register a new member? (new)");
                Console.WriteLine("Do you want to log in to an existing account? (login)\n");
                string option = Console.ReadLine().ToLower();

                if (option == "new")
                {
                    (username, password, rank) = NewMember();
                }
                else if (option == "login")
                {
                    Customer customer = Login(out username, out password);
                    if (customer != null)
                    {
                        loggedIn = true;
                        return (username, password, customer.GetCustomerLevel());
                    }
                    else
                    {
                        Console.WriteLine("Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("\nWrong input, try again.");
                }
            }

            return (username, password, rank);
        }
        //************************************************************************************
        public static (string Username, string Password, string rank) NewMember()
        {
            string username = "";
            string password = "";
            string rank = "";

            Console.Write($"\nEnter Username: ");
            username = Console.ReadLine().ToLower();

            Console.Write($"Enter Password: ");
            password = Console.ReadLine();

            bool done = false;
            while(!done)
            {
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    
                    Console.Write("Enter Membership Level (gold, silver, bronze, regular): ");
                    rank = Console.ReadLine().ToLower();

                    
                    if (IsMembershipValid(rank))
                    {
                        using (StreamWriter writer = new StreamWriter("registredmember.txt", true))
                        {
                            writer.WriteLine("Username: " + username);
                            writer.WriteLine("Password: " + password);
                            writer.WriteLine("Membership Level: " + rank); 
                            Console.WriteLine("\nUsernames and passwords saved... Press any key to continue.");
                            Console.ReadKey();
                            return (username, password, rank); 
                        }
                        done = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid membership level. Please enter 'gold', 'silver', 'bronze', or 'regular'.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Both Username and Password must be non-empty. Please try again.");
                }

            }

            return NewMember(); 
        }
        //************************************************************************************
        private static bool IsMembershipValid(string membershipLevel)
        {
            return membershipLevel.Equals("gold", StringComparison.OrdinalIgnoreCase) ||
                   membershipLevel.Equals("silver", StringComparison.OrdinalIgnoreCase) ||
                   membershipLevel.Equals("bronze", StringComparison.OrdinalIgnoreCase) ||
                   membershipLevel.Equals("regular", StringComparison.OrdinalIgnoreCase);
        }
        //************************************************************************************
        public static Customer Login(out string username, out string password)
        {
            Console.Write("\nEnter Username: ");
            username = Console.ReadLine().ToLower();
            Console.Write("Enter Password: ");
            password = Console.ReadLine();

            if (CheckCredentials(username, password, out string rank))
            {
                Console.WriteLine("\nLogin successful!");

               
                switch (rank.ToLower())
                {
                    case "gold":
                        return new CustomerGold(username, password, rank);
                    case "silver":
                        return new SilverCustomer(username, password, rank);
                    case "bronze":
                        return new BronzeCustomer(username, password, rank);
                    default:
                        return new Customer(username, password, rank);
                }
            }
            else
            {
                Console.WriteLine("\nLogin failed...");
                return null;
            }
        }

        //************************************************************************************
        public static bool CheckCredentials(string inputUsername, string inputPassword, out string rank)
        {
            rank = null;

            using (StreamReader reader = new StreamReader("registredmember.txt"))
            {
                string line;
                string storedUsername = null;
                string storedPassword = null;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("Username: "))
                    {
                        storedUsername = line.Replace("Username: ", "").Trim();
                    }
                    else if (line.StartsWith("Password: "))
                    {
                        storedPassword = line.Replace("Password: ", "").Trim();
                    }
                    else if (line.StartsWith("Membership Level: "))
                    {
                        rank = line.Replace("Membership Level: ", "").Trim();

                        if (string.Equals(storedUsername, inputUsername, StringComparison.OrdinalIgnoreCase) &&
                            string.Equals(storedPassword, inputPassword))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}