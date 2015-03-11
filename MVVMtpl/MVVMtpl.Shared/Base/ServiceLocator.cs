namespace MVVMtpl.Base
{
    using Microsoft.Practices.Unity;
    using MVVMtpl.Services;    
    using MVVMtpl.ViewModels;
    public class ServiceLocator
    {
        /// <summary>
        /// Unity container
        /// </summary>
        private UnityContainer container = new UnityContainer();

        /// <summary>
        /// Register all Services an ViewModels <see cref="ServiceLocator"/>
        /// </summary>
        public ServiceLocator()
        {
            // Services
            this.container.RegisterType<NavigationService>();
            this.container.RegisterType<NetworkService>();
            this.container.RegisterType<StorageService>();
            this.container.RegisterType<JsonService>();
            this.container.RegisterType<ICameraService, CameraService>();
            this.container.RegisterType<IImageService, ImageService>();
            this.container.RegisterType<SqliteService>();
            this.container.RegisterType<MobileService>();

            // ViewModels
            this.container.RegisterType<MainViewModel>();
            this.container.RegisterType<ShopViewModel>();
            this.container.RegisterType<OrdersViewModel>();
            this.container.RegisterType<OrdersSqlViewModel>();
            this.container.RegisterType<ProductsMobileViewModel>();
        }

        /// <summary>
        /// Gets an instance of <see cref="MainViewModel"/>
        /// </summary>
        public MainViewModel MainViewModel
        {
            get { return this.container.Resolve<MainViewModel>(); }
        }

        public ShopViewModel ShopViewModel
        {
            get { return this.container.Resolve<ShopViewModel>(); }
        }

        public OrdersViewModel OrdersViewModel
        {
            get { return this.container.Resolve<OrdersViewModel>(); }
        }

        public OrdersSqlViewModel OrdersSqlViewModel
        {
            get { return this.container.Resolve<OrdersSqlViewModel>(); }
        }

        public ProductsMobileViewModel ProductsMobileViewModel
        {
            get { return this.container.Resolve<ProductsMobileViewModel>(); }
        }
    }
}