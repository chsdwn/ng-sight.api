using System;
using System.Collections.Generic;
using System.Linq;
using Advantage.API.Models;

namespace Advantage.API
{
    public class DataSeed
    {
        private readonly ApiContext context;

        public DataSeed(ApiContext context)
        {
            this.context = context;
        }

        public void SeedData(int customersCount, int ordersCount)
        {
            if(!context.Customers.Any())
            {
                SeedCustomers(customersCount);
                context.SaveChanges();
            }

            if(!context.Orders.Any())
            {
                SeedOrders(ordersCount);
                context.SaveChanges();
            }

            if(!context.Servers.Any())
            {
                SeedServers();
                context.SaveChanges();
            }
        }

        private void SeedCustomers(int count)
        {
            List<Customer> customers = BuildCustomerList(count);

            foreach(var customer in customers)
            {
                context.Customers.Add(customer);
            }
        }

        private void SeedOrders(int count)
        {
            List<Order> orders = BuildOrderList(count);

            foreach(var order in orders)
            {
                context.Orders.Add(order);
            }
        }

        private void SeedServers()
        {
            List<Server> servers = BuildServerList();

            foreach(var server in servers)
            {
                context.Servers.Add(server);
            }
        }
        
        private List<Customer> BuildCustomerList(int customerCount)
        {
            var customers = new List<Customer>();
            var names = new List<string>();

            for(int i = 1; i <= customerCount; i++)
            {
                var name = Helpers.MakeUniqueCustomerName(names);
                names.Add(name);

                customers.Add(new Customer{
                    Id = i,
                    Name = name,
                    Email = Helpers.MakeCustomerEmail(),
                    State = Helpers.GetRandomState()
                });
            }

            return customers;
        }
        
        private List<Order> BuildOrderList(int orderCount)
        {
            var orders = new List<Order>();
            var rand = new Random();

            for(int i = 1; i <= orderCount; i++)
            {
                var placed = Helpers.GetRandomOrderPlaced();
                var randCustomerId = rand.Next(1, context.Customers.Count());

                orders.Add(new Order{
                    Id = i,
                    Customer = context.Customers.SingleOrDefault(c => c.Id == randCustomerId),
                    OrderTotal = Helpers.GetRandomOrderTotal(),
                    Placed = placed,
                    Completed = Helpers.GetRandomOrderCompleted(placed)
                });
            }

            return orders;
        }

        private List<Server> BuildServerList()
        {
            return new List<Server>()
            {
                new Server
                {
                    Id = 1,
                    Name = "Dev-Web",
                    IsOnline = true
                },
                new Server
                {
                    Id = 2,
                    Name = "Dev-Mail",
                    IsOnline = false
                },
                new Server
                {
                    Id = 3,
                    Name = "Dev-Services",
                    IsOnline = true
                },
                new Server
                {
                    Id = 4,
                    Name = "QA-Web",
                    IsOnline = true
                },
                new Server
                {
                    Id = 5,
                    Name = "QA-Mail",
                    IsOnline = false
                },
                new Server
                {
                    Id = 6,
                    Name = "QA-Services",
                    IsOnline = true
                },
                new Server
                {
                    Id = 7,
                    Name = "Prod-Web",
                    IsOnline = true
                },
                new Server
                {
                    Id = 8,
                    Name = "Prod-Mail",
                    IsOnline = true
                },
                new Server
                {
                    Id = 9,
                    Name = "Prod-Services",
                    IsOnline = true
                },
            };
        }
    }
}