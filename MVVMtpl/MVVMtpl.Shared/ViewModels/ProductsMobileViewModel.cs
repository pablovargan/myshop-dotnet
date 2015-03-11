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

    public class ProductsMobileViewModel : ObservableObject
    {
        private NavigationService navigationService;
        private NetworkService networkService;
        private MobileService mobileService;

        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get { return this.products; }
            set { this.Set(ref this.products, value); }
        }

        public ICommand GetOrdersCommand { get; private set; }

        public ProductsMobileViewModel(NavigationService navigationService, NetworkService networkService, MobileService mobileService)
        {
            this.navigationService = navigationService;
            this.networkService = networkService;
            this.mobileService = mobileService;

            GetOrdersCommand = new RelayCommand(async () => await GetOrdersExecuteAsync());
        }

        private async Task GetOrdersExecuteAsync()
        {
            Products = await this.mobileService.LoadProducts();
        }
    }
}
