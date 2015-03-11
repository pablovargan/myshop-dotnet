namespace MVVMtpl.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using MVVMtpl.Base;
    using MVVMtpl.Models;
    using MVVMtpl.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class OrdersViewModel : ObservableObject
    {
        private JsonService jsonService;
        private NavigationService navigationService;
        private NetworkService networkService;

        private Shop shop;
        private ObservableCollection<Order> orders;

        public ObservableCollection<Order> Orders
        {
            get { return this.orders; }
            set { this.Set(ref this.orders, value); }
        }

        public ICommand GetJsonCommand { get; set; }

        public OrdersViewModel(NavigationService navigationService, NetworkService networkservice, JsonService jsonService)
        {
            this.jsonService = jsonService;
            this.navigationService = navigationService;
            this.networkService = networkservice;

            this.orders = new ObservableCollection<Order>();
            this.shop = new Shop() { IsPublic = true, Orders = this.orders };

            GetJsonCommand = new RelayCommand(async () => await GetJsonExecuteAsync());
        }

        private async Task GetJsonExecuteAsync()
        {
            bool exits = await this.jsonService.CheckIfExists();
            if (!exits)
            {
                Seed();
                await this.jsonService.SetOrders(this.shop);
            }

            Orders = new ObservableCollection<Order>(await this.jsonService.LoadOrders());
        }

        private void Seed()
        {
            var product1 = new Product { Id = "2", Name = "Blusa", Description = "Sigue siendo la tendencia.", Price = 15 };
            var product2 = new Product { Id = "3", Name = "Pantalones", Description = "Pantalon de modelo clásico por su avivar de estilos inconformistas. ", Price = 20 };
            var product3 = new Product { Id = "4", Name = "Deportivos", Description = "Deportivos de sport", Price = 33 };
            var product4 = new Product { Id = "5", Name = "Reloj", Description = "Ancho de la pulsera: 1.2 cm", Price = 23 };

            var ol1 = new OrderLine() { Id = "1", Product = product1 };
            var ol2 = new OrderLine() { Id = "2", Product = product2 };
            var ol3 = new OrderLine() { Id = "3", Product = product3 };
            var ol4 = new OrderLine() { Id = "4", Product = product4 };

            var orderlines1 = new List<OrderLine>();
            orderlines1.Add(ol1);
            orderlines1.Add(ol2);
            orderlines1.Add(ol3);
            var orderlines2 = new List<OrderLine>();
            orderlines2.Add(ol1);
            orderlines2.Add(ol2);
            var orderlines3 = new List<OrderLine>();
            orderlines3.Add(ol1);
            orderlines3.Add(ol2);
            orderlines3.Add(ol3);
            orderlines3.Add(ol4);

            this.orders = new ObservableCollection<Order>()
            {
                new Order { Id = "1", OrderLines = orderlines1 },
                new Order { Id = "2", OrderLines = orderlines2 },
                new Order { Id = "3", OrderLines = orderlines3 }
            };

            this.shop.Orders = this.orders;
        }
    }
}
