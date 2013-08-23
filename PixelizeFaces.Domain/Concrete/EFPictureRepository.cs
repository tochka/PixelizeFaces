using System.Linq;
using PixelizeFaces.Domain.Abstract;
using PixelizeFaces.Domain.Entities;

namespace PixelizeFaces.Domain.Concrete
{
    public class EFPictureRepository : IPictureRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IQueryable<Picture> Picture
        {
            get { return _context.Picture; }
        }

        public void SavePicture(Picture picture)
        {
            if (picture.PictureId == 0)
                _context.Picture.Add(picture);
            _context.SaveChanges();
        }
    }
}