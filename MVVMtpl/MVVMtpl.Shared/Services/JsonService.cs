namespace MVVMtpl.Services
{
    using MVVMtpl.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.Storage;

    public class JsonService 
    {
        private static string fileName = "data.json";

        public async Task<bool> CheckIfExists()
        {
            bool jsonExists = true;
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
            }
            catch
            {
                jsonExists = false;
            }

            return jsonExists;
        }

        public async Task<IEnumerable<Order>> LoadOrders()
        {
            var folder = ApplicationData.Current.LocalFolder;

            StorageFile file = null;
            try
            {
                file = await folder.GetFileAsync(fileName);
            }
            catch
            {
                file = null;
            }

            if (file != null)
            {
                var json = await FileIO.ReadTextAsync(file);
                var shop = JsonConvert.DeserializeObject<Shop>(json);
                return shop.Orders;
            }

            return Enumerable.Empty<Order>();
        }

        public async Task SetOrders(Shop shop)
        {
            var jsonContent = JsonConvert.SerializeObject(shop);

            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,
                CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(file, jsonContent);
        }

    }
}
