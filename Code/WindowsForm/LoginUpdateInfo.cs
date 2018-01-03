using DevExpress.XtraSplashScreen;
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

namespace WindowsForm
{
    public partial class LoginUpdateInfo : Form
    {
        private Random rand = new Random();
        private string SEVER_URL = "http://localhost:51989/";
        public LoginUpdateInfo()
        {

            InitializeComponent();
            //Open Wait Form
            Global.ShowWaitForm(this);
            CreateImage();
        }
      
        #region Capcha
        private void CreateImage()
        {
            string code = GetRandomText();

            Bitmap bitmap = new Bitmap(200, 50, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Color.Yellow);
            Rectangle rect = new Rectangle(0, 0, 200, 50);

            SolidBrush b = new SolidBrush(Color.Black);
            SolidBrush White = new SolidBrush(Color.White);

            int counter = 0;

            g.DrawRectangle(pen, rect);
            g.FillRectangle(b, rect);

            for (int i = 0; i < code.Length; i++)
            {
                g.DrawString(code[i].ToString(), new Font("Georgia", 10 + rand.Next(14, 18)), White, new PointF(10 + counter, 10));
                counter += 20;
            }

            DrawRandomLines(g);

            if (File.Exists("d:/tempimage.bmp"))
            {

                try
                {
                    File.Delete("d:/tempimage.bmp");
                    bitmap.Save("d:/tempimage.bmp");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                bitmap.Save("d:/tempimage.bmp");

            }

            g.Dispose();
            bitmap.Dispose();
            IMG_Capcha.Image = System.Drawing.Image.FromFile("d:/tempimage.bmp");

        }
        private void DrawRandomLines(Graphics g)
        {
            SolidBrush green = new SolidBrush(Color.Green);
            //For Creating Lines on The Captcha
            for (int i = 0; i < 20; i++)
            {
                g.DrawLines(new Pen(green, 2), GetRandomPoints());
            }

        }
        private Point[] GetRandomPoints()
        {
            Point[] points = { new Point(rand.Next(10, 150), rand.Next(10, 150)), new Point(rand.Next(10, 100), rand.Next(10, 100)) };
            return points;
        }

        string code;
        private string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();

            if (String.IsNullOrEmpty(code))
            {
                string alphabets = "abcdefghijklmnopqrstuvwxyz1234567890";

                Random r = new Random();
                for (int j = 0; j <= 5; j++)
                {

                    randomText.Append(alphabets[r.Next(alphabets.Length)]);
                }

                code = randomText.ToString();
            }

            return code;
        }
        #endregion

        private void LoginUpdateInfo_Load(object sender, EventArgs e)
        {
        }

        private void btnReloadCapcha_Click(object sender, EventArgs e)
        {
            IMG_Capcha.Image.Dispose();
            code = "";
            CreateImage();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            txtCodeUpdate.Text = "";
            txtProtectCode.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lbNotify.Text = "Đang tiến hành đăng nhập....";
            lbNotify.ForeColor = Color.Orange;
            if (txtProtectCode.Text == code.ToString())
            {
                admin_thitoeicEntities1 db = new admin_thitoeicEntities1();
                
                try
                {

                    bool responseLogin = db.AspNetUsers.Any(p => p.Code == txtCodeUpdate.Text);
                    if (responseLogin)
                    {
                        lbNotify.Text = "Đăng nhập thành công, đang chuyển";
                        lbNotify.ForeColor = Color.Green;
                        //admin_thitoeicEntities db = new admin_thitoeicEntities();
                        //var user = db.AspNetUsers.Single(p => p.Email == txtUserName.Text);
                        ////var role = db.ASP.Single(p => p.)
                        
                        UpdateImage updateForm = new UpdateImage(this, txtCodeUpdate.Text);
                        updateForm.Show();
                        Thread.Sleep(5000);
                        this.Hide();
                    }
                    else
                    {
                        lbNotify.Text = "Sai mã cập nhật, vui lòng kiểm tra lại";
                        lbNotify.ForeColor = Color.Red;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error",ex.Message);
                }

                
            }
            else
            {
                MessageBox.Show("Mã bảo vệ không đúng");
            }
            lbNotify.Text = "";
        }
        public void Clear()
        {
            IMG_Capcha.Image.Dispose();
            code = "";
            CreateImage();
            txtCodeUpdate.Text = "";
            txtProtectCode.Text = "";
            lbNotify.Text = "";
        }
    }
}
