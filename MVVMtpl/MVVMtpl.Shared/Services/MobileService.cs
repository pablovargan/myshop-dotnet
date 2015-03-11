namespace MVVMtpl.Services
{
    using Microsoft.WindowsAzure.MobileServices;
    using MVVMtpl.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Threading.Tasks;

    public class MobileService
    {
        private static string appLocal = "http://localhost:50776";

        private static string appUrl = "appUrl";
        private static string appKey = "appKey";

        public async Task<ObservableCollection<Product>> LoadProducts()
        {
            using (MobileServiceClient mobileService = new MobileServiceClient(appUrl, appKey))
            {
                var productList = await mobileService.GetTable<Product>().ToListAsync();
                return new ObservableCollection<Product>(productList);
            }
        }

    }
}
