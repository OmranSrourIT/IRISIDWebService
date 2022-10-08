using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;

namespace IRIS_Camera_web.Controllers
{
    public class IRIS_CAPTUREController : Controller
    {
        // GET: IRIS_CAPTURE
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Save_Eyes_Capture(string base64imageEyes)
        {
            try
            {


                var strPathTest = @"\\10.130.149.107\Shared\MediaUploader";

                Random rnd = new Random();
                int num = rnd.Next();

                var ImageName = "Image_Capture" + num.ToString() + ".jpg";
                string imgPath = Path.Combine(strPathTest, ImageName);

                byte[] imageBytes = Convert.FromBase64String(base64imageEyes);


                using (var ms = new MemoryStream(imageBytes))
                {
                    Image image = Image.FromStream(ms);
                    image.Save(imgPath);
                    image.Dispose();
                    var RetrunFullPath = imgPath;
                    return RetrunFullPath;

                }



            }
            catch (Exception ex)
            {
                return ex.Message;
            }



            return "The picture has not been Capture yet";
        }


        public string GetIRISImageLink(string LinkRightImage, string LinkLeftImage)
        {
            var RightImageBase64 = "";
            var LeftImageBase64 = "";
            var ResultResponseEyeMatch = "";

            if (LinkRightImage != "")
            {
                using (var ms = new MemoryStream())
                {
                    using (var bitmap = new Bitmap(LinkRightImage))
                    {
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        RightImageBase64 = Convert.ToBase64String(ms.GetBuffer()); //Get Base64



                    }
                }

            }


            if (LinkLeftImage != "")
            {
                using (var ms = new MemoryStream())
                {
                    using (var bitmap = new Bitmap(LinkLeftImage))
                    {
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        LeftImageBase64 = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                    }
                }

            }

         

            var BaseAddress = new Uri("http://localhost:1234/api/Home/VerifyIRISEyesCapture");


            IList<string> postData = new List<string> {
                LeftImageBase64 , RightImageBase64 ,  
                };


            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.PostAsync(BaseAddress, postData, new JsonMediaTypeFormatter()).Result;
                 ResultResponseEyeMatch = response.Content.ReadAsStringAsync().Result;
                  
            }


            return ResultResponseEyeMatch;

        }



    }
}