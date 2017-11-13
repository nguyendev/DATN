using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRecognizer
{
    public partial class MainForm : Form
    {
        public CascadeClassifier _face = new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_default.xml");//Our face detection method 
        Classifier_Train _recognition = new Classifier_Train();

        Image<Bgr, Byte> currentFrame; //current image aquired from webcam for display
        Image<Gray, byte> result, TrainedFace = null; //used to store the result image and trained face
        Image<Gray, byte> gray_frame = null; //grayscale current image aquired from webcam for processing

        VideoCapture grabber; //This is our capture variable
        public MainForm()
        {
            InitializeComponent();
            if (_recognition.IsTrained)
            {
                message_bar.Text = "Training Data loaded";
            }
            else
            {
                message_bar.Text = "No training data found, please train program using Train menu option";
            }
            initialise_capture();
        }

        private void trainingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stop_capture();

            //OpenForm
            TrainingForm TF = new TrainingForm(this);
            TF.Show();
        }
        private void stop_capture()
        {
            //if (parrellelToolStripMenuItem.Checked)
            //{
            //    Application.Idle -= new EventHandler(FrameGrabber_Parrellel);
            //}
            //else
            //{
            Application.Idle -= new EventHandler(FrameGrabber_Standard);
            //}
            if (grabber != null)
            {
                grabber.Dispose();
            }
        }
        public void retrain()
        {
            _recognition = new Classifier_Train();
            if (_recognition.IsTrained)
            {
                message_bar.Text = "Training Data loaded";
            }
            else
            {
                message_bar.Text = "No training data found, please train program using Train menu option";
            }
        }

        //Camera Start Stop
        public void initialise_capture()
        {
            grabber = new VideoCapture();
            grabber.QueryFrame();
            //Initialize the FrameGraber event
            //if (parrellelToolStripMenuItem.Checked)
            //{
            //    Application.Idle += new EventHandler(FrameGrabber_Parrellel);
            //}
            //else
            //{
            Application.Idle += new EventHandler(FrameGrabber_Standard);
            //}
        }
        //Process Frame
        void FrameGrabber_Standard(object sender, EventArgs e)
        {
            //Get the current frame form capture device
            currentFrame = grabber.QueryFrame().ToImage<Bgr, byte>().Resize(320, 240, Emgu.CV.CvEnum.Inter.Cubic);

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
                            ADD_Face_Found(result, name, match_value);
                        }

                    }
                    catch
                    {
                        //do nothing as parrellel loop buggy
                        //No action as the error is useless, it is simply an error in 
                        //no data being there to process and this occurss sporadically 
                    }
                });

                //for (int i = 0; i < facesDetected.Length; i++)// (Rectangle face_found in facesDetected)
                //{
                //    facesDetected[i].X += (int)(facesDetected[i].Height * 0.15);
                //        facesDetected[i].Y += (int)(facesDetected[i].Width * 0.22);
                //        facesDetected[i].Height -= (int)(facesDetected[i].Height * 0.3);
                //        facesDetected[i].Width -= (int)(facesDetected[i].Width * 0.35);

                //        result = currentFrame.Copy(facesDetected[i]).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.Inter.Cubic);
                //        result._EqualizeHist();
                //        //draw the face detected in the 0th (gray) channel with blue color
                //        currentFrame.Draw(facesDetected[i], new Bgr(Color.Red), 2);

                //        if (_recognition.IsTrained)
                //        {
                //            string name = _recognition.Recognise(result);
                //            int match_value = (int)_recognition.Get_Fisher_Distance;

                //            //Draw the label for each face detected and recognized
                //            currentFrame.Draw(name + " ", new Point(facesDetected[i].X - 2, facesDetected[i].Y - 2), Emgu.CV.CvEnum.FontFace.HersheyComplex, 1, new Bgr(Color.LightGreen));
                //            ADD_Face_Found(result, name, match_value);
                //        }

                //};
                //Show the faces procesed and recognized
                image_PICBX.Image = currentFrame.ToBitmap();
            }
        }
        //ADD Picture box and label to a panel for each face
        int faces_count = 0;
        int faces_panel_Y = 0;
        int faces_panel_X = 0;
        void Clear_Faces_Found()
        {
            this.Faces_Found_Panel.Controls.Clear();
            faces_count = 0;
            faces_panel_Y = 0;
            faces_panel_X = 0;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SF = new SaveFileDialog();
            //As there is no identification in files to recogniser type we will set the extension ofthe file to tell us
            SF.Filter = "FisherFaceRecognizer File (*.FFR)|*.FFR";
            if (SF.ShowDialog() == DialogResult.OK)
            {
                _recognition.Save_Fisher_Recogniser(SF.FileName);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OF = new OpenFileDialog();
            OF.Filter = "FisherFaceRecognizer File (*.FFR)|*.FFR";
            if (OF.ShowDialog() == DialogResult.OK)
            {
                _recognition.Load_Fisher_Recogniser(OF.FileName);
            }
        }

        void ADD_Face_Found(Image<Gray, Byte> img_found, string name_person, int match_value)
        {
            PictureBox PI = new PictureBox();
            PI.Location = new Point(faces_panel_X, faces_panel_Y);
            PI.Height = 80;
            PI.Width = 80;
            PI.SizeMode = PictureBoxSizeMode.StretchImage;
            PI.Image = img_found.ToBitmap();
            Label LB = new Label();
            LB.Text = name_person + " " + match_value.ToString();
            LB.Location = new Point(faces_panel_X, faces_panel_Y + 80);
            //LB.Width = 80;
            LB.Height = 15;

            this.Faces_Found_Panel.Controls.Add(PI);
            this.Faces_Found_Panel.Controls.Add(LB);
            faces_count++;
            if (faces_count == 2)
            {
                faces_panel_X = 0;
                faces_panel_Y += 100;
                faces_count = 0;
            }
            else faces_panel_X += 85;

            if (Faces_Found_Panel.Controls.Count > 10)
            {
                Clear_Faces_Found();
            }

        }

    }
}