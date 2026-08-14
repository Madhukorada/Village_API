using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digital_Village_Api.Application.Interface
{
    public interface IImageService
    {
        public Task<string> SaveImageAsync(IFormFile file, string folderName, string ShopGovtRegistrationId);
    }
}
