# VR Controller Offset APK

A standalone VR application built with Unity that allows real-time adjustment of VR controller spatial offsets (X, Y, Z coordinates) via an in-game UI overlay.

## Features

- **3D Spatial Position Tracking**: Tracks physical VR controller positions (Grip/Aim) without modifying analog inputs
- **Real-time Offset Adjustment**: Three sliders (X-Axis, Y-Axis, Z-Axis) for instant controller position adjustment
- **World-Space UI Overlay**: In-game UI for offset control
- **Android APK Export**: Fully configured for direct APK compilation

## Project Structure

```
apk-vr-offset/
├── Assets/
│   ├── Scenes/
│   │   └── VROffsetScene.unity
│   ├── Scripts/
│   │   ├── ControllerOffsetManager.cs
│   │   ├── OffsetUIManager.cs
│   │   └── VRControllerModel.cs
│   ├── Prefabs/
│   │   ├── VRControllerModel.prefab
│   │   └── OffsetUICanvas.prefab
│   └── Materials/
│       └── ControllerMaterial.mat
├── ProjectSettings/
│   ├── ProjectVersion.txt
│   ├── ProjectSettings.asset
│   └── AndroidPlayerSettings.json
├── Packages/
│   └── manifest.json
├── AndroidManifest.xml
└── README.md
```

## Setup Instructions

1. **Prerequisites**
   - Unity 2022 LTS or newer
   - OpenXR Plugin for Unity
   - Android Build Support
   - XR Plugin Management

2. **Opening the Project**
   - Open Unity Hub and add the project folder
   - Select Unity version 2022 LTS or compatible

3. **Building APK**
   - Go to File → Build Settings
   - Select Android platform
   - Click "Build" to generate APK

## Offset Formula

```
Final Position = Real Hardware Position + Custom Offset
```

Where:
- **Real Hardware Position**: Actual Grip/Aim controller coordinates (X, Y, Z)
- **Custom Offset**: User-adjustable values from the UI sliders
- **Final Position**: Rendered virtual hand position in 3D space

## UI Controls

- **X-Axis Offset**: Horizontal left/right adjustment (-1.0 to 1.0)
- **Y-Axis Offset**: Vertical up/down adjustment (-1.0 to 1.0)
- **Z-Axis Offset**: Depth forward/backward adjustment (-1.0 to 1.0)

## Technical Details

- **VR Framework**: OpenXR
- **Target Platform**: Android (API Level 23+)
- **VR Device Support**: Meta Quest, HTC Vive Focus, and other OpenXR-compatible devices
- **Input Handling**: XR Input SubSystem (hands/controllers)
