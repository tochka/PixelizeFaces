using System.Collections.Generic;
using System.Drawing;

namespace FaceDetection
{
    public interface IFiltering
    {
        Image Apply(Bitmap picture, IEnumerable<Rectangle> rects);
    }
}