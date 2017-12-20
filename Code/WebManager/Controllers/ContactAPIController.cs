//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using DataAccess;
//using WebManager.Models.ContactViewModels;
//using Microsoft.AspNetCore.Authorization;
//using System.Net.Http;
//using Microsoft.AspNetCore.Http;
//using System.Net.Http.Headers;
//using System.IO;
//using Microsoft.AspNetCore.Hosting.Internal;
//using ImageSharp;
//using Microsoft.AspNetCore.Hosting;

//// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace WebManager.Controllers
//{
//    [Route("api/ContactAPI")]
//    public class ContactAPIController : Controller
//    {
//        private readonly ApplicationDbContext _context;
//        public IHostingEnvironment HostingEnvironment { get; set; }
//        public ContactAPIController(ApplicationDbContext context,
//            IHostingEnvironment HostingEnvironment)
//        {
//            _context = context;
//        }
//        // GET: /<controller>/
//        [HttpPost()]
//        public async Task<bool> CreateContactAsync(CreateContactViewModel model)
//        {
//            try
//            {
//                Contact contact = new Contact { OwnerID = model.UserID };
//                _context.Add(contact);
//                await _context.SaveChangesAsync();
//            }
//            catch
//            {
//                return false;
//            }
//            return true;
//        }
//        [Route("user/PostUserImage")]
//        [AllowAnonymous]
//        public async Task<bool> PostImage (IEnumerable<IFormFile> files)
//        {
//            // The Name of the Upload component is "files"
//            if (files != null)
//            {
//                foreach (var file in files)
//                {
//                    var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

//                    // Some browsers send file names with full path.
//                    // We are only interested in the file name.
//                    var fileName = Path.GetFileName(fileContent.FileName.Trim('"'));
//                    var physicalPath = Path.Combine(HostingEnvironment.WebRootPath, DIR_IMAGE, fileName);
//                    if (file.Length > 0)
//                    {
//                        using (var stream = new FileStream(physicalPath, FileMode.Create))
//                        {
//                            await file.CopyToAsync(stream);
//                            // Image.Load(string path) is a shortcut for our default type. Other pixel formats use Image.Load<TPixel>(string path))
//                        }

//                        using (Image<ImageSharp.Rgba32> image = ImageSharp.Image.Load(physicalPath))
//                        {
//                            var folderSave = Path.Combine(HostingEnvironment.WebRootPath, DIR_IMAGE + "\\Video", fileName);
//                            image.Resize(300, 170)
//                                 .Save(folderSave); // automatic encoder selected based on extension.
//                            folderSave = Path.Combine(HostingEnvironment.WebRootPath, DIR_IMAGE + "\\Book", fileName);
//                            image.Resize(280, 400)
//                                 .Save(folderSave); // automatic encoder selected based on extension.
//                            folderSave = Path.Combine(HostingEnvironment.WebRootPath, DIR_IMAGE + "\\People", fileName);
//                            image.Resize(220, 240)
//                                 .Save(folderSave); // automatic encoder selected based on extension.

//                            var user = await GetCurrentUserAsync();
//                            _context.Add(new Models.Images
//                            {
//                                CreateDT = System.DateTime.Now,
//                                Name = fileName,
//                                PicFull = "\\" + DIR_IMAGE + "\\Full\\" + fileName,
//                                PicBook = "\\" + DIR_IMAGE + "\\Book\\" + fileName,
//                                PicPeople = "\\" + DIR_IMAGE + "\\People\\" + fileName,
//                                PicVideo = "\\" + DIR_IMAGE + "\\Video\\" + fileName,
//                                Active = "A",
//                                Approved = "A",
//                                AuthorID = user.Id,
//                                IsDeleted = false,
//                            });
//                            await _context.SaveChangesAsync();
//                        }
//                    }
//                }
//            }

//            // Return an empty string to signify success
//            return true("");
//        }
//    }
//}
