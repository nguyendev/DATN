//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace KhoaLuanTotNghiep
//{
//    class Save
//    {
//        private void ProcessFrame(object sender, EventArgs arg)
//        {
//            Mat frame = new Mat();
//            Mat frame1 = new Mat();
//            grabber.Retrieve(frame, 0);
//            grabber.Retrieve(frame1, 0);
//            Mat image = frame; //Read the files as an 8-bit Bgr image  
//            long detectionTime;
//            List<Rectangle> faces = new List<Rectangle>();
//            List<Rectangle> eyes = new List<Rectangle>();

//            //The cuda cascade classifier doesn't seem to be able to load "haarcascade_frontalface_default.xml" file in this release
//            //disabling CUDA module for now
//            //bool tryUseCuda = false;
//            //bool tryUseOpenCL = true;


//            DetectFace.Detect(
//              image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml",
//              faces, eyes,
//              out detectionTime);


//            foreach (Rectangle face in faces)
//            {
//                CvInvoke.Rectangle(image, face, new Bgr(Color.Purple).MCvScalar, 3);
//                Bitmap c = frame.Bitmap;
//                Bitmap bmp = new Bitmap(face.Size.Width, face.Size.Height);
//                Graphics g = Graphics.FromImage(bmp);
//                g.DrawImage(c, 0, 0, face, GraphicsUnit.Pixel);


//            }


//            foreach (Rectangle eye in eyes)
//                CvInvoke.Rectangle(image, eye, new Bgr(Color.Green).MCvScalar, 2);

//            imageBox.Image = frame;
//            //pictureBox.Image = frame1.ToImage<Bgr, byte>().Bitmap;
//            Thread.Sleep((int)grabber.GetCaptureProperty(CapProp.Fps));
//        }

//    }
//}
