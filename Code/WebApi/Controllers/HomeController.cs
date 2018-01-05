using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Camera()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Preview(HttpPostedFileBase file)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var picture = System.Web.HttpContext.Current.Request.Files["file"];
                //Image _gray_frame = picture.

                //Face Detector
                //MCvAvgComp[][] facesDetected = gray_frame.DetectHaarCascade(Face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20)); //old method
                //Rectangle[] facesDetected = _face.DetectMultiScale(_gray_frame, 1.2, 10, new Size(25, 25), new Size(800, 800));

                ////Action for each element detected
                //for (int i = 0; i < facesDetected.Length; i++)// (Rectangle face_found in facesDetected)
                //{
                //    //This will focus in on the face from the haar results its not perfect but it will remove a majoriy
                //    //of the background noise
                //    facesDetected[i].X += (int)(facesDetected[i].Height * 0.15);
                //    facesDetected[i].Y += (int)(facesDetected[i].Width * 0.22);
                //    facesDetected[i].Height -= (int)(facesDetected[i].Height * 0.3);
                //    facesDetected[i].Width -= (int)(facesDetected[i].Width * 0.35);

                //    _result = _currentFrame.Copy(facesDetected[i]).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.Inter.Cubic);
                //    _result._EqualizeHist();
                //    face_PICBX.Image = _result.ToBitmap();
                //    //draw the face detected in the 0th (gray) channel with blue color
                //    _currentFrame.Draw(facesDetected[i], new Bgr(Color.Red), 2);

                //}
                if (picture.ContentLength > 0)
                    return Json("Success");
            }
            return Json("Error");
        }
    }
}
