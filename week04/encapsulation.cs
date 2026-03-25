using System;
using System.Collections.Generic;

namespace ProductOrderingSystem
{
    // Address class
    class Address
    {
        private string street;
        private string city;
        private string stateProvince;
        private string country;

        public Address(string street, string city, string stateProvince, string country)
        {
            this.street = street;
            this.city = city;
            this.stateProvince = stateProvince;
            this.country = country;
        }

        public bool IsInUSA()
        {
            return country.ToUpper() == "USA";
        }

        public string GetFullAddress()
        {
            return $"{street}\n{city}, {stateProvince}\n{country}";
        }
    }

    // Customer class
    class Customer
    {
        private string name;
        private Address address;

        public Customer(string name, Address address)
        {
            this.name = name;
            this.address = address;
        }

        public bool LivesInUSA()
        {
            return address.IsInUSA();
        }

        public string GetName()
        {
            return name;
        }

        public string GetAddressString()
        {
            return address.GetFullAddress();
        }
    }

    // Product class
    class Product
    {
        private string name;
        private string productId;
        private double price;
        private int quantity;

        public Product(string name, string productId, double price, int quantity)
        {
            this.name = name;
            this.productId = productId;
            this.price = price;
            this.quantity = quantity;
        }

        public double GetTotalCost()
        {
            return price * quantity;
        }

        public string GetPackingInfo()
        {
            return $"{name} (ID: {productId})";
        }
    }

    // Order class
    class Order
    {
        private List<Product> products = new List<Product>();
        private Customer customer;

        public Order(Customer customer)
        {
            this.customer = customer;
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public double GetTotalPrice()
        {
            double total = 0;
            foreach (Product product in products)
            {
                total += product.GetTotalCost();
            }

            // Shipping cost
            total += customer.LivesInUSA() ? 5 : 35;
            return total;
        }

        public string GetPackingLabel()
        {
            string label = "Packing Label:\n";
            foreach (Product product in products)
            {
                label += product.GetPackingInfo() + "\n";
            }
            return label;
        }

        public string GetShippingLabel()
        {
            return $"Shipping Label:\n{customer.GetName()}\n{customer.GetAddressString()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create addresses
            Address addr1 = new Address("123 Main St", "New York", "NY", "USA");
            Address addr2 = new Address("456 Queen St", "Toronto", "ON", "Canada");

            // Create customers
            Customer cust1 = new Customer("Alice Johnson", addr1);
            Customer cust2 = new Customer("Bob Smith", addr2);

            // Create orders
            Order order1 = new Order(cust1);
            order1.AddProduct(new Product("Laptop", "P001", 1200.00, 1));
            order1.AddProduct(new Product("Mouse", "P002", 25.00, 2));

            Order order2 = new Order(cust2);
            order2.AddProduct(new Product("Phone", "P003", 800.00, 1));
            order2.AddProduct(new Product("Headphones", "P004", 150.00, 1));
            order2.AddProduct(new Product("Charger", "P005", 20.00, 3));

            // Display results
            Console.WriteLine(order1.GetPackingLabel());
            Console.WriteLine(order1.GetShippingLabel());
            Console.WriteLine($"Total Price: ${order1.GetTotalPrice()}\n");

            Console.WriteLine(order2.GetPackingLabel());
            Console.WriteLine(order2.GetShippingLabel());
            Console.WriteLine($"Total Price: ${order2.GetTotalPrice()}");
        }
    }
}
