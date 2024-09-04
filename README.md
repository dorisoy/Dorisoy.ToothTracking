# 使用 Yolo5 进行口腔牙齿追踪和目标中心点检测

使用 YOLOv5（You Only Look Once version 5）深度学习目标检测模型，进行了端到端目标检测算法， 用了 295 张嘴巴，牙齿采样照片，通过自动学习的锚点标记，可以完美适应训练数据集中对象的形状，使用该技术，外加机械臂，可以运用到我们治疗机的口腔灯自动探照感应上，实现光源对焦和避让.

**环境**

Python: 3.8.12
PyTorch: 1.10.2
OpenCV-Python: 4.5.5.62

**中心点检测**

在本代码中，使用[YOLOv5 算法](https://github.com/ultralytics/yolov5)来检测一些对象，然后找到它们的 **中心点**。中心点位于像素坐标中。此代码从 USB 网络摄像头获取 **实时摄像头反馈** ，然后检测其中的对象和中心点。

**训练**

使用自定义数据集训练了 YOLO 模型, 要训练你的自定义数据集，请按照本指南进行操作: [Train Custom Data](https://github.com/ultralytics/yolov5/wiki/Train-Custom-Data)

**C#测试代码**

```csharp
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
```

**Python 测试代码**

```py

if __name__ == "__main__":    
    while(1):
        try:
            #Start reading camera feed (https://answers.opencv.org/question/227535/solvedassertion-error-in-video-capturing/))
            cap = cv2.VideoCapture(0, cv2.CAP_DSHOW)
            
            cap.set(cv2.CAP_PROP_FRAME_WIDTH, 1024)
            cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 768)
   
            #Now Place the base_plate_tool on the surface below the camera.
            while(1):
                _,frame = cap.read()
                #frame = undistortImage(frame)
                #cv2.imshow("Live" , frame)
                k = cv2.waitKey(5)
                if k == 27: #exit by pressing Esc key
                    cv2.destroyAllWindows()
                    sys.exit()
                #if k == 13: #execute detection by pressing Enter key           
                image = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB) # OpenCV image (BGR to RGB)
                
                # Inference
                results = model(image, size=720) #includes NMS

                # Results
                #results.print()  # .print() , .show(), .save(), .crop(), .pandas(), etc.
                #results.show()

                results.xyxy[0]  # im predictions (tensor)
                results.pandas().xyxy[0]  # im predictions (pandas)
                #      xmin    ymin    xmax   ymax  confidence  class    name
                # 0  749.50   43.50  1148.0  704.5    0.874023      0  person
                # 2  114.75  195.75  1095.0  708.0    0.624512      0  person
                # 3  986.00  304.00  1028.0  420.0    0.286865     27     tie
                
                #Results in JSON
                json_results = results.pandas().xyxy[0].to_json(orient="records") # im predictions (JSON)
                #print(json_results)
                
                results.render()  # updates results.imgs with boxes and labels                    
                output_image = results.imgs[0] #output image after rendering
                output_image = cv2.cvtColor(output_image, cv2.COLOR_RGB2BGR)
                
                output_image = draw_centroids_on_image(output_image, json_results) # Draw Centroids on the deteted objects and returns updated image
                
                cv2.imshow("Output", output_image) #Show the output image after rendering
                #cv2.waitKey(1)
                    
                    
                    
        except Exception as e:
            print("Error in Main Loop\n",e)
            cv2.destroyAllWindows()
            sys.exit()
    
    cv2.destroyAllWindows()
  

```

**屏幕**

<img src="https://github.com/dorisoy/Dorisoy.ToothTracking/blob/main/Screen/01.png"/>

<img src="https://github.com/dorisoy/Dorisoy.ToothTracking/blob/main/Screen/02.png"/>

<img src="https://github.com/dorisoy/Dorisoy.ToothTracking/blob/main/Screen/03.png"/>

<img src="https://github.com/dorisoy/Dorisoy.ToothTracking/blob/main/Screen/04.png"/>

<img src="https://github.com/dorisoy/Dorisoy.ToothTracking/blob/main/Screen/05.png"/>

<img src="https://github.com/dorisoy/Dorisoy.ToothTracking/blob/main/Screen/06.png"/>

<img src="https://github.com/dorisoy/Dorisoy.ToothTracking/blob/main/Screen/07.png"/>

<img src="https://github.com/dorisoy/Dorisoy.ToothTracking/blob/main/Screen/08.png"/>
