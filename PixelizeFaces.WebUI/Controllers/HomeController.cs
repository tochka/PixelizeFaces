using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FaceDetection;
using PixelizeFaces.Domain.Abstract;
using PixelizeFaces.Domain.Entities;

namespace PixelizeFaces.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly DetectFace _detectFace;
        private readonly IFiltering _filters;
        private readonly IPictureRepository _repository;

        public HomeController(DetectFace detectFace, IFiltering fiters, IPictureRepository repository)
        {
            _detectFace = detectFace;
            _filters = fiters;
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {            
            return View();
        }

        public FileContentResult GetImage(int pictureId = 0, bool original = true)
        {
            var picture = _repository.Picture.FirstOrDefault(item => item.PictureId == pictureId);
            if (picture == null)
                return null;

            return File(original ? picture.BeforePicture : picture.AfterPicture, picture.ImageMimeType);
        }

        [HttpPost]
        public RedirectToRouteResult AddPicture(HttpPostedFileBase image)
        {
            if (image == null)
                return RedirectToAction("Index");
            
            var picture = new Picture
                              {
                                  ImageMimeType = image.ContentType,
                                  BeforePicture = new byte[image.ContentLength],
                                  AfterPicture = new byte[image.ContentLength],
                                  Name = image.FileName
                              };

            image.InputStream.Read(picture.BeforePicture, 0, image.ContentLength);

            var pictureAfter = new Bitmap(image.InputStream);
            var rects = _detectFace.Detect(pictureAfter);
            _filters.Apply(pictureAfter, rects);

            var converter = new ImageConverter();
            picture.AfterPicture = (byte[]) converter.ConvertTo(pictureAfter, typeof (byte[]));            

            _repository.SavePicture(picture);
            return RedirectToAction("ViewPictures", new { pictureId = picture.PictureId });
        }

        public ActionResult ViewPictures(int pictureId = 0)
        {
            var picture = _repository.Picture.FirstOrDefault(item => item.PictureId == pictureId);
            if (picture == null)
                return null;
            return View(picture);
        }

        public FileStreamResult Download(int pictureId = 0)
        {
            var picture = _repository.Picture
                .FirstOrDefault(item => item.PictureId == pictureId);
            if (picture == null)
                return null;

            var stream = new MemoryStream(picture.AfterPicture);
            return File(stream, picture.ImageMimeType, picture.Name);
        }
    }
}
