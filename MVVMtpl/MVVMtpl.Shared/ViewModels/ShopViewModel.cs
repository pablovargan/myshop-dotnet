namespace MVVMtpl.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Auth;
    using Microsoft.WindowsAzure.Storage.Blob;
    using MVVMtpl.Base;
    using MVVMtpl.Models;
    using MVVMtpl.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.Media.Capture;
    using Windows.Storage;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;

    public class ShopViewModel : ObservableObject
    {
        private NavigationService navigationService;
        private NetworkService networkService;
        private ICameraService cameraService;
        private IImageService imageService;

        private BitmapImage image;
        public BitmapImage Image { get { return image; } set { Set(ref image, value); } }

        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products { get { return products; } set { Set(ref products, value); } }

        public ShopViewModel(NavigationService navigationService, NetworkService networkService, 
            ICameraService cameraService, IImageService imageService)
        {
            this.navigationService = navigationService;
            this.networkService = networkService;
            this.cameraService = cameraService;
            this.imageService = imageService;

            LoadProductsCommand = new RelayCommand(LoadProductsExecute);
        }

        public RelayCommand LoadProductsCommand { get; set; }

        public async Task TakePicture()
        {
            await this.cameraService.TakePictureFromCamera("josevi");
            await SetImageSource();
            //LoadProductsExecute();
        }

        private async Task<BitmapImage> LoadLocalPicture(string name)
        {
            return await this.imageService.LoadLocalPicture(name);
        }

        public async Task SetImageSource()
        {
            Image = await LoadLocalPicture("josevi");
        }

        public void LoadProductsExecute()
        {
            Products = new ObservableCollection<Product> { new Product { Id = "1", Name = "Camisa", Picture = Image, Price = 15 },
                new Product { Id = "2", Name = "Blusa", Picture = Image, Price = 15 },
                new Product { Id = "3", Name = "Pantalones", Picture = Image, Price = 15 },
                new Product { Id = "4", Name = "Deportivos", Picture = Image, Price = 15 },
                new Product { Id = "5", Name = "Reloj", Picture = Image, Price = 15 },
                new Product { Id = "6", Name = "Guantes", Picture = Image, Price = 15 },
                new Product { Id = "7", Name = "Chaqueta", Picture = Image, Price = 15 },
                new Product { Id = "8", Name = "Calcetines", Picture = Image, Price = 15 },
                new Product { Id = "9", Name = "Gorro", Picture = Image, Price = 15 } };
        }

        public async Task DownloadFile()
        {
            string accountName = "";
            string accountKey = "";
            try
            {
                StorageCredentials creds = new StorageCredentials
                (accountName, accountKey);
                CloudStorageAccount storageAccount = new CloudStorageAccount(creds, useHttps: true);

                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                CloudBlobContainer sampleContainer = blobClient.GetContainerReference("josevidotnetsample");
                CloudBlockBlob blockBlob = sampleContainer.GetBlockBlobReference("fre4.jpg");

                StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("fre4.jpg", CreationCollisionOption.ReplaceExisting);

                await blockBlob.DownloadToFileAsync(file);
                await blockBlob.UploadFromFileAsync(file);
                ImageSource imgSource = new BitmapImage();
            }
        }
    }
}
