using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMonAn.Interfaces;

namespace WebMonAn.Helpers
{
    public class UploadHelper : IUpload
    {
        private readonly IWebHostEnvironment _environment;
        public UploadHelper(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public bool UploadFile(IFormFile file)
        {
            if (file != null)
            {
                try
                {
                    var filePath = Path.Combine(_environment.WebRootPath, "images", "Food", file.FileName);
                    //if (File.Exists(filePath))
                    //{
                    //    return null;
                    //}
                    using (var filestream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }                    
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}
