using System.Linq;
using PixelizeFaces.Domain.Entities;

namespace PixelizeFaces.Domain.Abstract
{
    public interface IPictureRepository
    {
        IQueryable<Picture> Picture { get; }
        void SavePicture(Picture picture);
    }
}