using Digital_Village_Api.Application.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digital_village_Api.Infrastructure.ExternalServices
{
    public class ImageService: IImageService
    {
        public  async Task<string> SaveImageAsync(IFormFile file, string folderName, Guid sellerId)
        {
            if (file == null || file.Length == 0)
                return "no image";

            var folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Images",
                folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = sellerId + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine("Images", folderName, fileName)
                       .Replace("\\", "/");
        }
    }
}

