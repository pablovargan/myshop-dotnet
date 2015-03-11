namespace MVVMtpl.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using MVVMtpl.Base;
    using MVVMtpl.Models;
    using MVVMtpl.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Windows.Input;
    using System.Threading.Tasks;

    public class OrdersSqlViewModel : ObservableObject
    {
        private NavigationService navigationService;
        private NetworkService networkService;
        private SqliteService sqliteService;

        private ObservableCollection<Order> orders;

        public ObservableCollection<Order> Orders
        {
            get { return this.orders; }
            set { this.Set(ref this.orders, value); }
        }

        public ICommand InsertSQLCommand { get; private set; }
        public ICommand GetSqlCommand { get; private set; } 

        public OrdersSqlViewModel(NavigationService navigationService, NetworkService networkService, SqliteService sqliteService)
        {
            this.navigationService = navigationService;
            this.networkService = networkService;
            this.sqliteService = sqliteService;

            InsertSQLCommand = new RelayCommand(async () => await InsertSQLExecuteAsync());
            GetSqlCommand = new RelayCommand(async () => await GetSqlExecuteAsync());
        }

        private async Task InsertSQLExecuteAsync()
        {
            await this.sqliteService.ClearDataBaseAsync();

            var orderSeed = Seed();
            foreach (var item in orderSeed)
            {
                await this.sqliteService.SaveOrder(item);
            }
        }

        private async Task GetSqlExecuteAsync()
        {
            Orders = new ObservableCollection<Order>(await this.sqliteService.LoadOrders());
        }

        private List<Order> Seed()
        {
            var product1 = new Product { Id = "1", Name = "Blusa", Description = "Sigue siendo la tendencia.", Price = 15, OrderId = "1" };
            var product2 = new Product { Id = "2", Name = "Pantalones", Description = "Pantalon de modelo clásico por su avivar de estilos inconformistas. ", Price = 20, OrderId = "1" };
            var product3 = new Product { Id = "3", Name = "Deportivos", Description = "Deportivos de sport", Price = 33, OrderId = "1" };
            var product4 = new Product { Id = "4", Name = "Reloj", Description = "Ancho de la pulsera: 1.2 cm", Price = 23, OrderId = "2" };
            var prodcut5 = new Product { Id = "5", Name = "Camiseta", Description = "Camiseta sport", Price = 12, OrderId = "2" };

            var ol1 = new OrderLine() { Id = "1", OrderId = "1", Product = product1 };
            var ol2 = new OrderLine() { Id = "2", OrderId = "1", Product = product2 };
            var ol3 = new OrderLine() { Id = "3", OrderId = "1", Product = product3 };

            var orderlines1 = new List<OrderLine>();
            orderlines1.Add(ol1);
            orderlines1.Add(ol2);
            orderlines1.Add(ol3);

            var order1 = new Order()
            {
                Id = "1",
                OrderLines = orderlines1
            };

            var ol4 = new OrderLine() { Id = "1", OrderId = "2", Product = product4 };
            var ol5 = new OrderLine() { Id = "2", OrderId = "2", Product = prodcut5 };

            var orderlines2 = new List<OrderLine>();
            orderlines2.Add(ol4);
            orderlines2.Add(ol5);

            var order2 = new Order()
            {
                Id = "2",
                OrderLines = orderlines2
            };

            var orderslistSeed = new List<Order>();
            orderslistSeed.Add(order1);
            orderslistSeed.Add(order2);

            return orderslistSeed;
        }
    }
}
