using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace WindowsForm.UC
{
    public partial class UCLoginWithCamera : UserControl
    {
        public CascadeClassifier _face = new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_default.xml");//Our face detection method 
        Classifier_Train _recognition = new Classifier_Train();


        VideoCapture grabber; //This is our capture variable

        Image<Bgr, Byte> currentFrame; //current image aquired from webcam for display
        Image<Gray, byte> result, TrainedFace = null; //used to store the result image and trained face
        Image<Gray, byte> gray_frame = null; //grayscale current image aquired from webcam for processing
        private static UCLoginWithCamera _instance;
        public static UCLoginWithCamera Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UCLoginWithCamera();
                return _instance;
            }
        }
        public UCLoginWithCamera()
        {
            InitializeComponent();
            Initialise_capture();
        }

        public void Initialise_capture()
        {
            grabber = new VideoCapture();
            grabber.QueryFrame();
            //Initialize the FrameGraber event
            Application.Idle += new EventHandler(FrameGrabber_Standard);
        }

        //Process Frame
        void FrameGrabber_Standard(object sender, EventArgs e)
        {
            //Get the current frame form capture device
            currentFrame = grabber.QueryFrame().ToImage<Bgr, byte>().Resize(259, 190, Emgu.CV.CvEnum.Inter.Cubic);

            //Convert it to Grayscale
            if (currentFrame != null)
            {
                gray_frame = currentFrame.Convert<Gray, Byte>();

                //Face Detector
                Rectangle[] facesDetected = _face.DetectMultiScale(gray_frame, 1.2, 10, new Size(50, 50), Size.Empty);

                //Action for each element detected
                Parallel.For(0, facesDetected.Length, i =>
                {
                    try
                    {
                        //facesDetected[i].X += (int)(facesDetected[i].Height * 0.15);
                        //facesDetected[i].Y += (int)(facesDetected[i].Width * 0.22);
                        //facesDetected[i].Height -= (int)(facesDetected[i].Height * 0.3);
                        //facesDetected[i].Width -= (int)(facesDetected[i].Width * 0.35);

                        result = currentFrame.Copy(facesDetected[i]).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.Inter.Cubic);
                        result._EqualizeHist();
                        //draw the face detected in the 0th (gray) channel with blue color
                        currentFrame.Draw(facesDetected[i], new Bgr(Color.Red), 2);

                        if (_recognition.IsTrained)
                        {
                            string name = _recognition.Recognise(result);
                            int match_value = (int)_recognition.Get_Fisher_Distance;

                            //Draw the label for each face detected and recognized
                            currentFrame.Draw(name + " ", new Point(facesDetected[i].X - 2, facesDetected[i].Y - 2), Emgu.CV.CvEnum.FontFace.HersheyComplex, 1, new Bgr(Color.LightGreen));
                        }

                    }
                    catch
                    {
                        //do nothing as parrellel loop buggy
                        //No action as the error is useless, it is simply an error in 
                        //no data being there to process and this occurss sporadically 
                    }
                });
                image_PICBX.Image = currentFrame.ToBitmap();
            }
        }
    }
}
