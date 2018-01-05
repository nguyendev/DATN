using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class VideosController : System.Web.Http.ApiController
    {
        public HttpResponseMessage Get(string filename, string ext)
        {
            var video = new VideoStream(filename, ext);

            var response = Request.CreateResponse();
            //response.Content = new PushStreamContent(video.WriteToStream, new MediaTypeHeaderValue("video/" + ext));

            return response;
        }
    }
}