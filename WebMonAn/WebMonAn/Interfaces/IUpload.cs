using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMonAn.Interfaces
{
    public interface IUpload
    {
        bool UploadFile(IFormFile file);
    }
}
