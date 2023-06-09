﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IImageService
    {
        Task<string?> SavePictureAsync(IFormFile pictureFile, string rootPath);
        void DeletePicture(string pictureUrl);
    }
}
