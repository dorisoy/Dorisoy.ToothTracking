using System;
using System.IO;
using System.Net;
using System.Text;

public class Program
{
    static void Main(string[] args)
    {
        byte[] imageArray = System.IO.File.ReadAllBytes(@"E:\GameDev\Robot\物体检测与重心查找\yolo5-object-detection-and-centroid-finding-main\Pic\m (1).jpeg");
        string encoded = Convert.ToBase64String(imageArray);
        byte[] data = Encoding.ASCII.GetBytes(encoded);
        string API_KEY = "LSsQT7q9VW5hSF4SCUAY";
        string DATASET_NAME = "tooth-tracking";

        // Construct the URL
        string uploadURL =
                "https://api.roboflow.com/dataset/" +
                        DATASET_NAME + "/upload" +
                        "?api_key=" + API_KEY +
                        "&name=YOUR_IMAGE.jpg" +
                        "&split=train";

        // Service Request Config
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        // Configure Request
        WebRequest request = WebRequest.Create(uploadURL);
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;

        // Write Data
        using (Stream stream = request.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }

        // Get Response
        string responseContent = null;
        using (WebResponse response = request.GetResponse())
        {
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader sr99 = new StreamReader(stream))
                {
                    responseContent = sr99.ReadToEnd();
                }
            }
        }

        Console.WriteLine(responseContent);

    }
}