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

**屏幕**

<img src="https://github.com/dorisoy/Dorisoy.ToothTracking/blob/main/Screen/01.png"/>

<img src="https://github.com/dorisoy/Dorisoy.ToothTracking/blob/main/Screen/02.png"/>

<img src="https://github.com/dorisoy/Dorisoy.ToothTracking/blob/main/Screen/03.png"/>

<img src="https://github.com/dorisoy/Dorisoy.ToothTracking/blob/main/Screen/04.png"/>

<img src="https://github.com/dorisoy/Dorisoy.ToothTracking/blob/main/Screen/05.png"/>

<img src="https://github.com/dorisoy/Dorisoy.ToothTracking/blob/main/Screen/06.png"/>

<img src="https://github.com/dorisoy/Dorisoy.ToothTracking/blob/main/Screen/07.png"/>

<img src="https://github.com/dorisoy/Dorisoy.ToothTracking/blob/main/Screen/08.png"/>
