using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Structure;

namespace FaceDetection
{
    public class DetectFace
    {
        private readonly HaarCascade _cascadeClassifier;

        public DetectFace(string cascadeFileName)
        {
            _cascadeClassifier = new HaarCascade(cascadeFileName);
        }

        public IEnumerable<Rectangle> Detect(Bitmap picture)
        {
            var image = new Image<Bgr, Byte>(picture);

            var detects = new List<Rectangle>();
            using (var gray = image.Convert<Gray, Byte>()) 
            {
                //normalizes brightness and increases contrast of the image
                gray._EqualizeHist();
                
                var facesDetected = gray.DetectHaarCascade(
                    _cascadeClassifier,
                    2,
                    10,
                    Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                    new Size(20, 20));
                detects.AddRange(facesDetected[0].Select(face => face.rect));
            }
            return detects;
        }        
    }
}
