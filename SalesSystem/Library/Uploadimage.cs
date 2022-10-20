using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Library
{
    public class Uploadimage
    {
        public async Task<byte[]> ByteAvatarImageAsync(IFormFile Avatarimage, IWebHostEnvironment environment,string image)
        {

            //string image = "images/images/default.png";
            if(Avatarimage !=null)
            {
                using (var memoryStream=new MemoryStream())
                {
                    await Avatarimage.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            else
            {
                var archivoOrigen = $"{environment.ContentRootPath}/wwwroot/{image}";
                return File.ReadAllBytes(archivoOrigen);
            }
        }
    }
}
