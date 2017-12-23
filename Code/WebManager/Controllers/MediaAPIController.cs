using DataAccess;
using ImageSharp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebManager.Controllers
{
    [Route("api/Media")]
    public class MediaAPIController : Controller
    {
        public static string DIR_FACE_IMAGE = "upload\\faces";
        private ApplicationDbContext _context;
        private UserManager<Member> _userManager;
        private readonly IFileProvider _fileProvider;
        public IHostingEnvironment HostingEnvironment { get; set; }

        public MediaAPIController(IHostingEnvironment hostingEnvironment,
            ApplicationDbContext context,
            UserManager<Member> userManager)
        {
            HostingEnvironment = hostingEnvironment;
            _fileProvider = hostingEnvironment.WebRootFileProvider;
            _context = context;
            _userManager = userManager;
        }
        [HttpPost("{code}/{number}")]
        public async Task<bool> Save(string code, int number)
        {
            try
            {
                var file = await Request.ReadFormAsync();
                var fileName = Request.Headers.FirstOrDefault();
                var physicalPath = Path.Combine(HostingEnvironment.WebRootPath, DIR_FACE_IMAGE, file.Files.First().FileName);
                if (file.Files.First().Length > 0)
                {
                    using (var stream = new FileStream(physicalPath, FileMode.Create))
                    {
                        await file.Files.First().CopyToAsync(stream);
                    }

                    var user = await _context.Users.SingleOrDefaultAsync(p => p.Code == code);
                    var imageURL = "\\" + DIR_FACE_IMAGE + "\\" + fileName;

                    #region if image users exits
                    if (_context.Image.Any(p => p.OwnerID == user.Id))
                    {
                        var imageUser = await _context.Image.SingleOrDefaultAsync(p => p.OwnerID == user.Id);
                        imageUser = ChangeImage(imageUser, imageURL, number);
                        _context.Image.Update(imageUser);
                    }
                    #endregion
                    #region if image users not exits
                    else
                    {
                        DataAccess.Image imageUser = new DataAccess.Image
                        {
                            OwnerID = user.Id,
                        };
                        imageUser = ChangeImage(imageUser, imageURL, number);
                        _context.Image.Add(imageUser);
                    }
                    #endregion
                    await _context.SaveChangesAsync();
                }
            }

            catch
            {
                return false;
            }
            // Return an empty string to signify success
            return true;
        }
        private DataAccess.Image ChangeImage(DataAccess.Image image,string imageURL, int number)
        {
            switch (number)
            {
                case 1:
                    image.Pic1 = imageURL;
                    break;
                case 2:
                    image.Pic2 = imageURL;
                    break;
                case 3:
                    image.Pic3 = imageURL;
                    break;
                case 4:
                    image.Pic4 = imageURL;
                    break;
                case 5:
                    image.Pic5 = imageURL;
                    break;
                case 6:
                    image.Pic6 = imageURL;
                    break;
                case 7:
                    image.Pic7 = imageURL;
                    break;
                case 8:
                    image.Pic8 = imageURL;
                    break;
                case 9:
                    image.Pic9 = imageURL;
                    break;
                case 10:
                    image.Pic10 = imageURL;
                    break;
                default:
                    break;
            }
            return image;
        }
    }
}
