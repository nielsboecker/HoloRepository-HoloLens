# HoloRepository
The COMP0111 project MSGOSHHOLO, where we develop a software engineering solution for Microsoft and GOSH.

## HoloLens Application
### Setup the environment
This demo application is developed by Unity3D, please setup the environment before running the App:
<p align="center">
    <img src="./HoloRepositoryDemoApplication/Images/windows10_logo.png" height="100">
</p>

**Windows 10 SDK:** To run the HoloLens App, please make sure you have installed the Windows 10 SDK version 18362 or later [Download Link](https://developer.microsoft.com/en-US/windows/downloads/windows-10-sdk).

<p align="center">
    <img src="./HoloRepositoryDemoApplication/Images/unity_logo.png" height="100">
</p>

**Unity3D:** The current recommendation is to use Unity 2018.4.x, which is the LTS build required for MRTK v2, The version we used to develop the App is **2018.4.2f1**, which you can download [here](https://unity3d.com/unity/whats-new/2018.4.2). Meanwhile, please make sure you have installed the **Universal Windows Platform (UWP)** and **.NET** support in unity.
<p align="center">
    <img src="./HoloRepositoryDemoApplication/Images/visualstudio_logo.png" height="100">
</p>

**Visual Studio 2017:** In order to support the TextMeshPro UI in Unity, please make sure your Visual Studio 2017 version is later than  15.7.5. The current using version is **15.9.14**, which is highly recommended.[Download Link](https://visualstudio.microsoft.com/downloads/).
<p align="center">
    <img src="./HoloRepositoryDemoApplication/Images/mrtkicon.jpg" height="100">
</p>

**MRTK V2.0:** Initially, the MRTK v2.0 has already been imported into this project, you don't need to import the toolkit again. if you want to develop the HoloLens App on your own, please visit their [website](https://github.com/microsoft/MixedRealityToolkit-Unity).


The detail of related development tools could be found in [microsoft mixed reality offical site](https://docs.microsoft.com/en-us/windows/mixed-reality/install-the-tools).

### Start to run the App