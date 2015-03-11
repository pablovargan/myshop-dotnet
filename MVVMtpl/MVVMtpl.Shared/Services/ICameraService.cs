using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MVVMtpl.Services
{
    public interface ICameraService
    {
        Task TakePictureFromCamera(string name);
    }
}
