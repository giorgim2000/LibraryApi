using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ImageService : IImageService
    {
        public async Task<string?> SavePictureAsync(IFormFile pictureFile)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(pictureFile.FileName);
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "BookImages");
            Directory.CreateDirectory(folderPath);
            string filePath = Path.Combine(folderPath, fileName);
            try
            {
                using var stream = new FileStream(filePath, FileMode.Create);
                await pictureFile.CopyToAsync(stream);
            }
            catch (Exception)
            {
                return null;
            }

            return "/BookImages/" + fileName;
        }

        public void DeletePicture(string pictureUrl)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), pictureUrl);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
