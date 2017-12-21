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
using DevExpress.XtraSplashScreen;

namespace WindowsForm
{

    public partial class UpdateImage : Form
    {
        private LoginUpdateInfo _parent;
        private string _userID;

        #region Variables
        //Camera specific
        private VideoCapture _grabber;

        //Images for finding face
        private Image<Bgr, Byte> _currentFrame;
        private Image<Gray, byte> _result = null;
        private Image<Gray, byte> _gray_frame = null;

        //Classifier
        private CascadeClassifier _face = new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_default.xml");//Our face detection method ;

        //For aquiring 10 images in a row
        private List<Image<Gray, byte>> _resultImages = new List<Image<Gray, byte>>();
        private int _results_list_pos = 0;
        private int _NUM_FACES_TO_AQUIRE = 3;
        private bool _RECORD = false;

        //Saving Jpg
        private List<Image<Gray, byte>> _imagestoWrite = new List<Image<Gray, byte>>();
        private EncoderParameters _ENC_Parameters = new EncoderParameters(1);
        private EncoderParameter _ENC = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100);
        private ImageCodecInfo _Image_Encoder_JPG;

        //Saving XAML Data file
        private List<string> NamestoWrite = new List<string>();
        private List<string> NamesforFile = new List<string>();
        private XmlDocument docu = new XmlDocument();

        //Variables
        private string _code;
        #endregion
        
        public UpdateImage(LoginUpdateInfo mainForm, string code)
        {
            _parent = mainForm;
            InitializeComponent();
            Global.ShowWaitForm(this);
            //LoadFormImage(code);
            _ENC_Parameters.Param[0] = _ENC;
            _code = code;
            _Image_Encoder_JPG = GetEncoder(ImageFormat.Jpeg);
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
                client.BaseAddress = new Uri(Global.SEVER_URL);

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
            _userID = user.Id;
        }


        //Camera Start Stop
        public void initialise_capture()
        {
            _grabber = new VideoCapture();
            if(_grabber.IsOpened)
            { 
                _grabber.QueryFrame();
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
            if (_grabber != null)
            {
                _grabber.Dispose();
            }
            //Initialize the FrameGraber event
        }

        //Process Frame
        void FrameGrabber(object sender, EventArgs e)
        {
            //Get the current frame form capture device
            _currentFrame = _grabber.QueryFrame().ToImage<Bgr, byte>().Resize(320, 240, Emgu.CV.CvEnum.Inter.Cubic);

            //Convert it to Grayscale
            if (_currentFrame != null)
            {
                _gray_frame = _currentFrame.Convert<Gray, Byte>();

                //Face Detector
                //MCvAvgComp[][] facesDetected = gray_frame.DetectHaarCascade(Face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20)); //old method
                Rectangle[] facesDetected = _face.DetectMultiScale(_gray_frame, 1.2, 10, new Size(25, 25),new Size(800,800));

                //Action for each element detected
                for (int i = 0; i < facesDetected.Length; i++)// (Rectangle face_found in facesDetected)
                {
                    //This will focus in on the face from the haar results its not perfect but it will remove a majoriy
                    //of the background noise
                    //facesDetected[i].X += (int)(facesDetected[i].Height * 0.15);
                    //facesDetected[i].Y += (int)(facesDetected[i].Width * 0.22);
                    //facesDetected[i].Height -= (int)(facesDetected[i].Height * 0.3);
                    //facesDetected[i].Width -= (int)(facesDetected[i].Width * 0.35);

                    _result = _currentFrame.Copy(facesDetected[i]).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.Inter.Cubic);
                    _result._EqualizeHist();
                    face_PICBX.Image = _result.ToBitmap();
                    //draw the face detected in the 0th (gray) channel with blue color
                    _currentFrame.Draw(facesDetected[i], new Bgr(Color.Red), 2);

                }
                if (_RECORD && facesDetected.Length > 0 && _resultImages.Count <= _NUM_FACES_TO_AQUIRE)
                {
                    _resultImages.Add(_result);
                    count_lbl.Text = "Count: " + _resultImages.Count.ToString();
                    pos_lb.Text = "Pos: " + _results_list_pos.ToString();
                }
                if (_resultImages.Count == _NUM_FACES_TO_AQUIRE)
                {
                    ADD_BTN.Visible = false;
                    NEXT_BTN.Visible = true;
                    PREV_btn.Visible = true;
                    count_lbl.Visible = false;
                    Single_btn.Visible = true;
                    ADD_ALL.Visible = true;
                    BTN_REMOVEALL.Visible = true;
                    _RECORD = false;
                    count_lbl.Text = "Count: " + _resultImages.Count.ToString();
                    pos_lb.Text = "Pos: " + _results_list_pos.ToString();
                    Application.Idle -= new EventHandler(FrameGrabber);
                }
                count_lbl.Text = "Count: " + _resultImages.Count.ToString();
                pos_lb.Text = "Pos: " + _results_list_pos.ToString();
                image_PICBX.Image = _currentFrame.ToBitmap();
            }
        }

        //Saving The Data
        private bool save_training_data(System.Drawing.Image face_data)
        {
            try
            {
                Random rand = new Random();
                bool file_create = true;
                string facename = "face_" + _userID + "_" + rand.Next().ToString() + ".jpg";
                while (file_create)
                {

                    if (!File.Exists(Application.StartupPath + "/TrainedFaces/" + facename))
                    {
                        file_create = false;
                    }
                    else
                    {
                        facename = "face_" + _userID + "_" + rand.Next().ToString() + ".jpg";
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
                    name_D.InnerText = _userID;
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
                        writer.WriteElementString("NAME", _userID);
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
            if (face_PICBX.Image != null)
            {
                if (_resultImages.Count == _NUM_FACES_TO_AQUIRE)
                {
                    ADD_ALL.Visible = true;
                }
                else
                {

                    stop_capture();
                    _resultImages.Add(_result);
                    NEXT_BTN.Visible = true;
                    if (_results_list_pos > 2)
                        PREV_btn.Visible = true;
                    ADD_ALL.Visible = false;
                    Single_btn.Visible = true;
                    BTN_REMOVEALL.Visible = true;
                    initialise_capture();
                }

                count_lbl.Text = "Count: " + _resultImages.Count.ToString();
                pos_lb.Text = "Pos: " + _results_list_pos.ToString();
                face_PICBX.Image = null;
            }
            else
            {
                MessageBox.Show("Vui lòng nhận diện hình ảnh của bạn trước");
            }
        }
        private void Single_btn_Click(object sender, EventArgs e)
        {
            _RECORD = false;
            List<Image<Gray, byte>> temp = _resultImages;
            temp.RemoveAt(_results_list_pos);
            
            int j = 0;
            for (int i = 0; i < _resultImages.Count - 1; i++)
            {
                if (i != _results_list_pos)
                {
                    _resultImages[j] = temp[i];
                    j++;
                }
            }
            Application.Idle += new EventHandler(FrameGrabber);
            if(_resultImages.Count > 1)
                Single_btn.Visible = true;
            if (_results_list_pos > 0)
                _results_list_pos--;
            if (_results_list_pos > 0)
            {
                NEXT_BTN.Visible = true;
                PREV_btn.Visible = true;
            }
            else
            {
                PREV_btn.Visible = false;
            }
                count_lbl.Text = "Count: " + _resultImages.Count.ToString();
            pos_lb.Text = "Pos: " + _results_list_pos;
            //Application.Idle += new EventHandler(PREV_btn_Click);
            count_lbl.Visible = true;
            ADD_BTN.Visible = true;
            ADD_ALL.Visible = false;
        }
        //Get 10 image to train
        private void RECORD_BTN_Click(object sender, EventArgs e)
        {
            try {
                if (_RECORD)
                {
                    _RECORD = false;
                }
                else
                {
                    if (_resultImages.Count == _NUM_FACES_TO_AQUIRE)
                    {
                        //resultImages.Clear();
                        //Application.Idle += new EventHandler(FrameGrabber);

                    }
                    _RECORD = true;
                    ADD_BTN.Visible = false;
                    if (_resultImages.Count > 1)
                        BTN_REMOVEALL.Visible = true;
                }
                count_lbl.Text = "Count: " + _resultImages.Count.ToString();
                pos_lb.Text = "Pos: " + _results_list_pos.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void NEXT_BTN_Click(object sender, EventArgs e)
        {
            if (_results_list_pos < _resultImages.Count - 1)
            {
                face_PICBX.Image = _resultImages[_results_list_pos].ToBitmap();
                _results_list_pos++;
                PREV_btn.Visible = true;
            }
            else
            {
                NEXT_BTN.Visible = false;
            }
            count_lbl.Text = "Count: " + _resultImages.Count.ToString();
            pos_lb.Text = "Pos: " + _results_list_pos.ToString();
        }
        private void PREV_btn_Click(object sender, EventArgs e)
        {
            if (_results_list_pos > 0)
            {
                _results_list_pos--;
                face_PICBX.Image = _resultImages[_results_list_pos].ToBitmap();
                NEXT_BTN.Visible = true;
            }
            else
            {
                PREV_btn.Visible = false;
            }
            count_lbl.Text = "Count: " + _resultImages.Count.ToString();
            pos_lb.Text = "Pos: " + _results_list_pos.ToString();
        }
        private void ADD_ALL_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _resultImages.Count; i++)
            {
                face_PICBX.Image = _resultImages[i].ToBitmap();
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
            UpdateInfo updateInfo = new UpdateInfo(this,this._parent,_code);
            updateInfo.Show();
            _grabber.Stop();
        }

        private void UpdateInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            stop_capture();
            _parent.Show();
            _parent.Clear();
        }

        private void BTN_REMOVEALL_Click(object sender, EventArgs e)
        {
            _resultImages.Clear();
            _results_list_pos = 0;
            pos_lb.Text = "Pos: " + _results_list_pos.ToString();
            count_lbl.Text = "Count: " + _resultImages.Count.ToString();
            BTN_REMOVEALL.Visible = false;
            ADD_BTN.Visible = true;
            Application.Idle += new EventHandler(FrameGrabber);
        }
    }
}
