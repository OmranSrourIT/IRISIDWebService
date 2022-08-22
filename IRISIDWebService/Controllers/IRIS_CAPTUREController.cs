using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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



    }
}