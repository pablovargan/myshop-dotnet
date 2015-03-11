using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Storage;

namespace MVVMtpl.Services
{
    public class CameraService : ICameraService
    {
        public async Task TakePictureFromCamera(string name)
        {
            CameraCaptureUI cameraUI = new CameraCaptureUI();

            cameraUI.PhotoSettings.AllowCropping = false;
            cameraUI.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.MediumXga;

            StorageFile capturedMedia =
                await cameraUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (capturedMedia != null)
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                await capturedMedia.MoveAsync(localFolder);
                try
                {
                    StorageFile file = await localFolder.GetFileAsync(name + ".jpg");
                    await file.DeleteAsync();
                }
                catch
                {

                }
                await capturedMedia.RenameAsync(name + ".jpg");
            }
        }
    }
}
