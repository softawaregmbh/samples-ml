# Use InkRecognizer in a WPF App

![](screenshot.png)

## WPF App with Ink Input
* Create WPF App (.NET Core)
* Add NuGet package **Microsoft.Toolkit.Wpf.UI.Controls**
* Add an **app.manifest** file with the following content

```xml
<?xml version="1.0" encoding="utf-8"?>
<assembly manifestVersion="1.0" xmlns="urn:schemas-microsoft-com:asm.v1">
  <compatibility xmlns="urn:schemas-microsoft-com:compatibility.v1">
    <application>

      <!-- Windows 10 -->
      <maxversiontested Id="10.0.18358.0"/>
      <supportedOS Id="{8e0f7a12-bfb3-4fe8-b9a5-48fd50a15a9a}" />

    </application>
  </compatibility>
</assembly>
```

* Select the **app.manifest** in the WPF App project settings (Application / Icon and manifest)

* Add a **InkCanvas** control to your MainWindow

```xml
<Window
  xmlns:controls="clr-namespace:Microsoft.Toolkit.Wpf.UI.Controls;assembly=Microsoft.Toolkit.Wpf.UI.Controls"/>
        
<controls:InkCanvas x:Name="inkCanvas" Loaded="inkCanvas_Loaded" />
```

* Enable all input device types in the code-behind file
```cs
private void inkCanvas_Loaded(object sender, RoutedEventArgs e)
{
    inkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Touch | CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Mouse;
}
```

* See [InkRecognizer.cs](InkRecognizerWpf/InkRecognizer.cs) for accessing the Ink Recognizer API
* See [InkViewModel.cs](InkRecognizerWpf/InkViewModel.cs) for calling the Ink Recognizer in a ViewModel