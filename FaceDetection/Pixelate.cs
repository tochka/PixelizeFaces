using System;
using System.Collections.Generic;
using System.Drawing;

namespace FaceDetection
{
    public class Pixelate : IFiltering
    {
        private readonly int _pixY;
        private readonly int _pixX;

        public Pixelate(int pixelsX, int pixelsY)
        {
            _pixX = pixelsX;
            _pixY = pixelsY;
        }

        public Image Apply(Bitmap picture, IEnumerable<Rectangle> rects)
        {
            foreach (var rect in rects)
            {
                var pixX = rect.Width / _pixX;
                var pixY = rect.Height / _pixY;
                PixelateRectangle(ref picture, rect, pixX, pixY);
            }
            return picture;
        }

        private static void PixelateRectangle(ref Bitmap image, Rectangle rectangle, Int32 pixelateSizeX, Int32 pixelateSizeY)
        {
            for (var xx = rectangle.X; xx < rectangle.X + rectangle.Width && xx < image.Width; xx += pixelateSizeX)
                for (var yy = rectangle.Y; yy < rectangle.Y + rectangle.Height && yy < image.Height; yy += pixelateSizeY)
                {
                    var offsetX = pixelateSizeX / 2;
                    var offsetY = pixelateSizeY / 2;
         
                    while (xx + offsetX >= image.Width) offsetX--;
                    while (yy + offsetY >= image.Height) offsetY--;

                    var pixel = image.GetPixel(xx + offsetX, yy + offsetY);

                    for (var x = xx; x < xx + pixelateSizeX && x < image.Width; x++)
                        for (var y = yy; y < yy + pixelateSizeY && y < image.Height; y++)
                            image.SetPixel(x, y, pixel);
                }
        }        
    }
}