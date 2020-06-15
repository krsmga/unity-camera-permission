# Camera-Permission

This script was developed to facilitate the implementation of the code that requests permission to use the camera on Android and iOS devices, in applications developed with Unity.

## Script settings in Inspector
![](../master/Inspector.png)

## Steps for use
1. Attach the **CameraPermission.cs** script to any **GameObject** in the Scene that starts the application.

2. The **Text Not Authorized IOS** and **Text Not Authorized Android** parameters can be used to attach **GameObjects** or **Prefabs** with graphics or text to inform the user of the need to allow access to the camera. ***(Optional)***

3. The **On Authorized (Boolean)** event returns **true** and allows you to enter any method that will be executed after the user allows access to the camera. ***(Optional)***

4. The **On Not Authorized (Boolean)** event returns **false** and allows you to enter any method that will be executed after the user denies access to the camera. ***(Optional)***

Regardless of the use of the script parameters, it will request permission from the application's camera.


