using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using CefSharp;
using CefSharp.WinForms;

namespace WindowsForm
{

    public partial class UpdateImage : Form
    {
        private LoginUpdateInfo Parent;
        private string UserID;
        private string SEVER_URL = "http://localhost:51989/";

        #region Variables
        //Camera specific
        VideoCapture grabber;

        //Images for finding face
        Image<Bgr, Byte> currentFrame;
        Image<Gray, byte> result = null;
        Image<Gray, byte> gray_frame = null;

        //Classifier
        CascadeClassifier Face = new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_default.xml");//Our face detection method ;

        //For aquiring 10 images in a row
        List<Image<Gray, byte>> resultImages = new List<Image<Gray, byte>>();
        int results_list_pos = 0;
        int num_faces_to_aquire = 10;
        bool RECORD = false;

        //Saving Jpg
        List<Image<Gray, byte>> ImagestoWrite = new List<Image<Gray, byte>>();
        EncoderParameters ENC_Parameters = new EncoderParameters(1);
        EncoderParameter ENC = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100);
        ImageCodecInfo Image_Encoder_JPG;

        //Saving XAML Data file
        List<string> NamestoWrite = new List<string>();
        List<string> NamesforFile = new List<string>();
        XmlDocument docu = new XmlDocument();

        //Variables
        #endregion
        
        public UpdateImage(LoginUpdateInfo mainForm, string code)
        {
            Parent = mainForm;
            InitializeComponent();
            LoadFormImage(code);
            ENC_Parameters.Param[0] = ENC;
            Image_Encoder_JPG = GetEncoder(ImageFormat.Jpeg);
            initialise_capture();
        }
        private void LoadFormImage(string code)
        {
            admin_thitoeicEntities db = new admin_thitoeicEntities();
            var user = db.AspNetUsers.Single(p => p.Code == code);
            bool contact = db.Contacts.Any(p => p.OwnerID == user.Id);
            if (contact)
            {
                
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(SEVER_URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var login = new Dictionary<string, string>
            {
               { "UserID",  user.Id}
            };
                var content = new FormUrlEncodedContent(login);

                var response = client.PostAsync("api/ContactAPI", content).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                if (responseString == "true")
                {

                }
                else
                {
                    MessageBox.Show("Tạo danh sách liên hệ thất bại");
                }
            }
            UserID = user.Id;
        }


        //Camera Start Stop
        public void initialise_capture()
        {
            grabber = new VideoCapture();
            if(grabber.IsOpened)
            { 
                grabber.QueryFrame();
                //Initialize the FrameGraber event
                Application.Idle += new EventHandler(FrameGrabber);
            }
            PREV_btn.Visible = false;
            NEXT_BTN.Visible = false;
            BTN_REMOVEALL.Visible = false;
            ADD_ALL.Visible = false;
            Single_btn.Visible = false;
        }
        private void stop_capture()
        {
            Application.Idle -= new EventHandler(FrameGrabber);
            if (grabber != null)
            {
                grabber.Dispose();
            }
            //Initialize the FrameGraber event
        }

        //Process Frame
        void FrameGrabber(object sender, EventArgs e)
        {
            //Get the current frame form capture device
            currentFrame = grabber.QueryFrame().ToImage<Bgr, byte>().Resize(320, 240, Emgu.CV.CvEnum.Inter.Cubic);

            //Convert it to Grayscale
            if (currentFrame != null)
            {
                gray_frame = currentFrame.Convert<Gray, Byte>();

                //Face Detector
                //MCvAvgComp[][] facesDetected = gray_frame.DetectHaarCascade(Face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20)); //old method
                Rectangle[] facesDetected = Face.DetectMultiScale(gray_frame, 1.2, 10, new Size(25, 25),new Size(800,800));

                //Action for each element detected
                for (int i = 0; i < facesDetected.Length; i++)// (Rectangle face_found in facesDetected)
                {
                    //This will focus in on the face from the haar results its not perfect but it will remove a majoriy
                    //of the background noise
                    //facesDetected[i].X += (int)(facesDetected[i].Height * 0.15);
                    //facesDetected[i].Y += (int)(facesDetected[i].Width * 0.22);
                    //facesDetected[i].Height -= (int)(facesDetected[i].Height * 0.3);
                    //facesDetected[i].Width -= (int)(facesDetected[i].Width * 0.35);

                    result = currentFrame.Copy(facesDetected[i]).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.Inter.Cubic);
                    result._EqualizeHist();
                    face_PICBX.Image = result.ToBitmap();
                    //draw the face detected in the 0th (gray) channel with blue color
                    currentFrame.Draw(facesDetected[i], new Bgr(Color.Red), 2);

                }
                if (RECORD && facesDetected.Length > 0 && resultImages.Count < num_faces_to_aquire)
                {
                    resultImages.Add(result);
                    count_lbl.Text = "Count: " + resultImages.Count.ToString();
                    pos_lb.Text = "Pos: " + results_list_pos.ToString();
                }
                if (resultImages.Count == num_faces_to_aquire)
                {
                    ADD_BTN.Visible = false;
                    NEXT_BTN.Visible = true;
                    PREV_btn.Visible = true;
                    count_lbl.Visible = false;
                    Single_btn.Visible = true;
                    ADD_ALL.Visible = true;
                    BTN_REMOVEALL.Visible = true;
                    RECORD = false;
                    count_lbl.Text = "Count: " + resultImages.Count.ToString();
                    pos_lb.Text = "Pos: " + results_list_pos.ToString();
                    Application.Idle -= new EventHandler(FrameGrabber);
                    MessageBox.Show("Error", "Error in saving file info. Training data not saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                count_lbl.Text = "Count: " + resultImages.Count.ToString();
                pos_lb.Text = "Pos: " + results_list_pos.ToString();
                image_PICBX.Image = currentFrame.ToBitmap();
            }
        }

        //Saving The Data
        private bool save_training_data(System.Drawing.Image face_data)
        {
            try
            {
                Random rand = new Random();
                bool file_create = true;
                string facename = "face_" + UserID + "_" + rand.Next().ToString() + ".jpg";
                while (file_create)
                {

                    if (!File.Exists(Application.StartupPath + "/TrainedFaces/" + facename))
                    {
                        file_create = false;
                    }
                    else
                    {
                        facename = "face_" + UserID + "_" + rand.Next().ToString() + ".jpg";
                    }
                }


                if (Directory.Exists(Application.StartupPath + "/TrainedFaces/"))
                {
                    face_data.Save(Application.StartupPath + "/TrainedFaces/" + facename, ImageFormat.Jpeg);
                }
                else
                {
                    Directory.CreateDirectory(Application.StartupPath + "/TrainedFaces/");
                    face_data.Save(Application.StartupPath + "/TrainedFaces/" + facename, ImageFormat.Jpeg);
                }
                if (File.Exists(Application.StartupPath + "/TrainedFaces/TrainedLabels.xml"))
                {
                    //File.AppendAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", NAME_PERSON.Text + "\n\r");
                    bool loading = true;
                    while (loading)
                    {
                        try
                        {
                            docu.Load(Application.StartupPath + "/TrainedFaces/TrainedLabels.xml");
                            loading = false;
                        }
                        catch
                        {
                            docu = null;
                            docu = new XmlDocument();
                            Thread.Sleep(10);
                        }
                    }

                    //Get the root element
                    XmlElement root = docu.DocumentElement;

                    XmlElement face_D = docu.CreateElement("FACE");
                    XmlElement name_D = docu.CreateElement("NAME");
                    XmlElement file_D = docu.CreateElement("FILE");

                    //Add the values for each nodes
                    //name.Value = textBoxName.Text;
                    //age.InnerText = textBoxAge.Text;
                    //gender.InnerText = textBoxGender.Text;
                    name_D.InnerText = UserID;
                    file_D.InnerText = facename;

                    //Construct the Person element
                    //person.Attributes.Append(name);
                    face_D.AppendChild(name_D);
                    face_D.AppendChild(file_D);

                    //Add the New person element to the end of the root element
                    root.AppendChild(face_D);

                    //Save the document
                    docu.Save(Application.StartupPath + "/TrainedFaces/TrainedLabels.xml");
                    //XmlElement child_element = docu.CreateElement("FACE");
                    //docu.AppendChild(child_element);
                    //docu.Save("TrainedLabels.xml");
                }
                else
                {
                    FileStream FS_Face = File.OpenWrite(Application.StartupPath + "/TrainedFaces/TrainedLabels.xml");
                    using (XmlWriter writer = XmlWriter.Create(FS_Face))
                    {
                        writer.WriteStartDocument();
                        writer.WriteStartElement("Faces_For_Training");

                        writer.WriteStartElement("FACE");
                        writer.WriteElementString("NAME", UserID);
                        writer.WriteElementString("FILE", facename);
                        writer.WriteEndElement();

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                    }
                    FS_Face.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        //Delete all the old training data by simply deleting the folder
        private void Delete_Data_BTN_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + "/TrainedFaces/"))
            {
                Directory.Delete(Application.StartupPath + "/TrainedFaces/", true);
                Directory.CreateDirectory(Application.StartupPath + "/TrainedFaces/");
            }
        }

        //Add the image to training data
        private void ADD_BTN_Click(object sender, EventArgs e)
        {
            if (resultImages.Count == num_faces_to_aquire)
            {
                ADD_ALL.Visible = true;
            }
            else
            {

                stop_capture();
                resultImages.Add(result);
                NEXT_BTN.Visible = true;
                if (results_list_pos > 2)
                    PREV_btn.Visible = true;
                ADD_ALL.Visible = false;
                Single_btn.Visible = true;
                BTN_REMOVEALL.Visible = true;
                initialise_capture();
            }
            count_lbl.Text = "Count: " + resultImages.Count.ToString();
            pos_lb.Text = "Pos: " + results_list_pos.ToString();
        }
        private void Single_btn_Click(object sender, EventArgs e)
        {
            RECORD = false;
            List<Image<Gray, byte>> temp = resultImages;
            temp.RemoveAt(results_list_pos);
            
            int j = 0;
            for (int i = 0; i < resultImages.Count - 1; i++)
            {
                if (i != results_list_pos)
                {
                    resultImages[j] = temp[i];
                    j++;
                }
            }
            Application.Idle += new EventHandler(FrameGrabber);
            if(resultImages.Count > 1)
                Single_btn.Visible = true;
            if (results_list_pos > 0)
                results_list_pos--;
            if (results_list_pos > 0)
            {
                NEXT_BTN.Visible = true;
                PREV_btn.Visible = true;
            }
            else
            {
                PREV_btn.Visible = false;
            }
                count_lbl.Text = "Count: " + resultImages.Count.ToString();
            pos_lb.Text = "Pos: " + results_list_pos;
            //Application.Idle += new EventHandler(PREV_btn_Click);
            count_lbl.Visible = true;
            ADD_BTN.Visible = true;
            ADD_ALL.Visible = false;
        }
        //Get 10 image to train
        private void RECORD_BTN_Click(object sender, EventArgs e)
        {
            if (RECORD)
            {
                RECORD = false;
            }
            else
            {
                if (resultImages.Count == 10)
                {
                    //resultImages.Clear();
                    //Application.Idle += new EventHandler(FrameGrabber);
                    
                }
                RECORD = true;
                ADD_BTN.Visible = false;
                if(resultImages.Count > 1)
                    BTN_REMOVEALL.Visible = true;
            }
            count_lbl.Text = "Count: " + resultImages.Count.ToString();
            pos_lb.Text = "Pos: " + results_list_pos.ToString();
        }
        private void NEXT_BTN_Click(object sender, EventArgs e)
        {
            if (results_list_pos < resultImages.Count - 1)
            {
                face_PICBX.Image = resultImages[results_list_pos].ToBitmap();
                results_list_pos++;
                PREV_btn.Visible = true;
            }
            else
            {
                NEXT_BTN.Visible = false;
            }
            count_lbl.Text = "Count: " + resultImages.Count.ToString();
            pos_lb.Text = "Pos: " + results_list_pos.ToString();
        }
        private void PREV_btn_Click(object sender, EventArgs e)
        {
            if (results_list_pos > 0)
            {
                results_list_pos--;
                face_PICBX.Image = resultImages[results_list_pos].ToBitmap();
                NEXT_BTN.Visible = true;
            }
            else
            {
                PREV_btn.Visible = false;
            }
            count_lbl.Text = "Count: " + resultImages.Count.ToString();
            pos_lb.Text = "Pos: " + results_list_pos.ToString();
        }
        private void ADD_ALL_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < resultImages.Count; i++)
            {
                face_PICBX.Image = resultImages[i].ToBitmap();
                if (!save_training_data(face_PICBX.Image))
                    MessageBox.Show("Error", "Error in saving file info. Training data not saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                   
                }
                Thread.Sleep(100);
            }
            ADD_ALL.Visible = false;
            ADD_BTN.Visible = true;
            //restart single face detection
            Single_btn_Click(null, null);
            Hide();
            UpdateInfo updateInfo = new UpdateInfo(this);
            updateInfo.Show();
        }

        private void UpdateInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            stop_capture();
            Parent.Show();        
        }

        private void BTN_REMOVEALL_Click(object sender, EventArgs e)
        {
            resultImages.Clear();
            results_list_pos = 0;
            pos_lb.Text = "Pos: " + results_list_pos.ToString();
            count_lbl.Text = "Count: " + resultImages.Count.ToString();
            BTN_REMOVEALL.Visible = false;
            ADD_BTN.Visible = true;
            Application.Idle += new EventHandler(FrameGrabber);
        }
    }
}
