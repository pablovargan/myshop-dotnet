using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace MVVMtpl.Services
{
    public interface IImageService
    {
        Task<BitmapImage> LoadLocalPicture(string name);
    }
}
