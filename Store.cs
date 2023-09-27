using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMarket2._0
{
    public class Store
    {
        //Fill the store with sodas
        //************************************************************************************
        public List<StoreItem> Sodas;
        public Currency SelectedCurrency;
        public Store()
        {
            Sodas = new List<StoreItem>
                    {
                        new StoreItem("Nocco", 24.99, 1.99, 1.99),
                        new StoreItem("Redbull", 24.99, 1.99, 1.99),
                        new StoreItem("Monster", 24.99, 1.99, 1.99),
                        new StoreItem("Coca Cola", 14.99, 1.29, 1.29),
                        new StoreItem("Fanta", 14.99, 1.29, 1.29),
                        new StoreItem("Sprite", 14.99, 1.29, 1.29),
                        new StoreItem("Pepsi", 14.99, 1.29, 1.29),
                        new StoreItem("Water", 9.99, 0.99, 0.99)
                    };
            SelectedCurrency = Currency.SEK;
        }

        //Methods
        //************************************************************************************
        public void StoreMenu(Customer customer)
        {
            SelectCurrency();

            while (true)
            {
                Startscreen.Welcome();

                int itemNumber = 1;
                Console.WriteLine($"Long time no see {customer.Username}!\n".ToUpper());
                Console.WriteLine("Please select an option:\n");

                foreach (var soda in Sodas)
                {
                    double priceInSelectedCurrency;

                    switch (SelectedCurrency)
                    {
                        case Currency.SEK:
                            priceInSelectedCurrency = soda.PriceSEK;
                            break;
                        case Currency.EUR:
                            priceInSelectedCurrency = soda.PriceEUR;
                            break;
                        case Currency.USD:
                            priceInSelectedCurrency = soda.PriceUSD;
                            break;
                        default:
                            priceInSelectedCurrency = soda.PriceSEK;
                            break;
                    }

                    Console.WriteLine($"({itemNumber}) Soda: {soda.Name}, Price: {priceInSelectedCurrency} {SelectedCurrency}");
                    itemNumber++;
                }

                Console.WriteLine($"\n(0) View Shopping Cart");
                Console.WriteLine("(55) Log out");
                Console.WriteLine("(99) Exit\n");

                string optionStr = Console.ReadLine();
                int option;

                if (int.TryParse(optionStr, out option))
                {
                    switch (option)
                    {
                        case 0:
                            Startscreen.Welcome();
                            DisplayCart(SelectedCurrency, customer);
                            break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            Console.WriteLine("How many would you like?");
                            int amount;

                            if (int.TryParse(Console.ReadLine(), out amount))
                            {
                                if (option >= 1 && option <= Sodas.Count)
                                {
                                    StoreItem selectedSoda = Sodas[option - 1];
                                    double priceInSelectedCurrency;

                                    switch (SelectedCurrency)
                                    {
                                        case Currency.SEK:
                                            priceInSelectedCurrency = selectedSoda.PriceSEK;
                                            break;
                                        case Currency.EUR:
                                            priceInSelectedCurrency = selectedSoda.PriceEUR;
                                            break;
                                        case Currency.USD:
                                            priceInSelectedCurrency = selectedSoda.PriceUSD;
                                            break;
                                    }

                                    for (int i = 0; i < amount; i++)
                                    {
                                        customer.AddToCart(selectedSoda);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid soda option");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid amount. Please enter a valid number.");
                            }
                            break;
                        case 55:
                            customer.ClearCart();
                            Startscreen.Start();
                            break;
                        case 99:
                            Console.WriteLine($"\nThank you for shopping at SodaMarket. Welcome back anytime!");
                            return;
                        default:
                            Console.WriteLine($"\nWrong input");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
        }

        //************************************************************************************
        public void SelectCurrency()
        {
            Console.Clear();

            Startscreen.Welcome();

            Console.WriteLine("Login successful!\n");
            Console.WriteLine("(1) SEK");
            Console.WriteLine("(2) EUR");
            Console.WriteLine("(3) USD");
            Console.Write("\nSelect a currency: ");

            if (int.TryParse(Console.ReadLine(), out int currencyOption))
            {
                switch (currencyOption)
                {
                    case 1:
                        SelectedCurrency = Currency.SEK;
                        break;
                    case 2:
                        SelectedCurrency = Currency.EUR;
                        break;
                    case 3:
                        SelectedCurrency = Currency.USD;
                        break;
                    default:
                        Console.WriteLine("Invalid currency option");
                        return; // Restart the currency selection loop
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid currency option.");
                SelectCurrency();
            }
        }

        //************************************************************************************
        public void DisplayCart(Currency currency, Customer customer)
        {
            double sum = 0;
            Console.WriteLine($"Shopping Cart Contains:\n");

            var itemGroups = customer.Cart.GroupBy(item => item.Name);

            foreach (var group in itemGroups)
            {
                int quantity = group.Count();
                StoreItem item = group.First();
                double price = 0;

                switch (currency)
                {
                    case Currency.SEK:
                        price = item.PriceSEK;
                        break;
                    case Currency.EUR:
                        price = item.PriceEUR;
                        break;
                    case Currency.USD:
                        price = item.PriceUSD;
                        break;
                }

                Console.WriteLine($"{item.Name}, Price: {price} {currency} x {quantity}");
                sum += price * quantity;
            }

            double discount = customer.GetDiscount();
            double discountedPrice = sum * (1 - discount);
            if (discount != 0)
            {
                Console.WriteLine($"\nThe total of the cart is: {sum.ToString("n2")} ({currency})\n");
                Console.WriteLine($"But with your '{customer.GetCustomerLevel()} Membership' you get a {discount * 100}% discount");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nThe total of the cart after discount is: {discountedPrice.ToString("n2")} ({currency})\n");
                Console.ResetColor();
                Console.WriteLine("Press enter to continue shopping...");
                Console.ReadLine();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nThe total of the cart is: {sum.ToString("n2")} ({currency})\n");
                Console.ResetColor();
                Console.WriteLine("Press enter to continue shopping...");
                Console.ReadLine();
            }
        }


    }

}
