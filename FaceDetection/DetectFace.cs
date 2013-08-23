using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceDetection
{
    class DetectFace
    {
        private  HaarCascade _faceHaar;

        public DetectFace(string haarcascadePath)
        {
            _faceHaar = new HaarCascade(haarcascadePath);
        }

        public IEnumerable<Rectangle> Detect(Bitmap picture)
        {
             var lstFaceDetect =new List<Rectangle>();

             Image<Bgr, Byte> image = new Image<Bgr, byte>(picture);
             Image<Gray, Byte> gray = image.Convert<Gray, Byte>(); 
             
             gray._EqualizeHist();
             
             var facesDetected = gray.DetectHaarCascade(
                _faceHaar, 
                1.1, 
                10, 
                Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, 
                new Size(20, 20));

            foreach (var f in facesDetected[0])            
                lstFaceDetect.Add(f.rect);               
            return lstFaceDetect;
        }
    }
}
