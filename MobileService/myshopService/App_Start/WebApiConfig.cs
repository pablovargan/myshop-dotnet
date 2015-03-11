using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using myshopService.DataObjects;
using myshopService.Models;

namespace myshopService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            
            Database.SetInitializer(new myshopInitializer());
        }
    }

    public class myshopInitializer : ClearDatabaseSchemaIfModelChanges<myshopContext>
    {
        protected override void Seed(myshopContext context)
        {
            Product product1 = new Product() { Id = Guid.NewGuid().ToString(), Name = "Blusa", Description = "A la moda", Price = 30 };
            Product product2 = new Product() { Id = Guid.NewGuid().ToString(), Name = "Zapatillas", Description = "Deportivas", Price = 40 };
            Product product3 = new Product() { Id = Guid.NewGuid().ToString(), Name = "Sudadera", Description = "Zara", Price = 25 };
            Product product4 = new Product() { Id = Guid.NewGuid().ToString(), Name = "Camiseta", Description = "Deportivas", Price = 50 };

            context.Set<Product>().Add(product1);
            context.Set<Product>().Add(product2);
            context.Set<Product>().Add(product3);
            context.Set<Product>().Add(product4);

            OrderLine orderline1 = new OrderLine() { Id = Guid.NewGuid().ToString(), Product = product1 };
            OrderLine orderline2 = new OrderLine() { Id = Guid.NewGuid().ToString(), Product = product2 };

            context.Set<OrderLine>().Add(orderline1);
            context.Set<OrderLine>().Add(orderline2);

            List<OrderLine> orderlinesList1 = new List<OrderLine>();
            orderlinesList1.Add(orderline1);
            orderlinesList1.Add(orderline2);

            Order order1 = new Order() { Id = Guid.NewGuid().ToString(), OrderLines = orderlinesList1 };
            context.Set<Order>().Add(order1);

            OrderLine orderline3 = new OrderLine() { Id = Guid.NewGuid().ToString(), Product = product3 };
            OrderLine orderline4 = new OrderLine() { Id = Guid.NewGuid().ToString(), Product = product4 };

            List<OrderLine> orderlinesList2 = new List<OrderLine>();
            orderlinesList2.Add(orderline3);
            orderlinesList2.Add(orderline4);

            Order order2 = new Order() { Id = Guid.NewGuid().ToString(), OrderLines = orderlinesList2 };
            context.Set<Order>().Add(order2);

            base.Seed(context);
        }
    }
}

