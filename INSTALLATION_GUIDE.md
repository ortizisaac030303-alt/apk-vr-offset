# Installation & Getting Started

## System Requirements

### For Development
- **Operating System**: Windows 10+, macOS 11+, or Linux
- **Unity**: 2022.3 LTS or newer
- **RAM**: 16GB minimum (8GB+ for Android builds)
- **Disk Space**: 30GB+ for full development setup
- **Android SDK**: API Level 23+ (installed via Unity Hub)

### For Running APK
- **Android Device**: Android 6.0+ (API 23+)
- **VR Headset**: OpenXR-compatible (Meta Quest 2/3, HTC Vive Focus, etc.)
- **Controller Support**: 6-DOF hand controllers with position tracking

## Installation Steps

### 1. Clone Repository

```bash
git clone https://github.com/ortizisaac030303-alt/apk-vr-offset.git
cd apk-vr-offset
```

### 2. Install Unity

1. Download Unity Hub from [unity.com](https://unity.com/download)
2. Install Unity 2022.3.20f1 LTS
3. During installation, include:
   - Android Build Support
   - Android SDK & NDK tools
   - OpenJDK

### 3. Open Project in Unity

1. Open Unity Hub
2. Click "Add Project from Disk"
3. Select the cloned repository folder
4. Click "Open" to load project
5. Wait for initial import (~5-10 minutes)

### 4. Install Required Packages

Packages are auto-installed from `Packages/manifest.json`:

- XR Plugin Management
- OpenXR Plugin
- XR Interaction Toolkit
- Input System
- TextMeshPro

If missing, install via:
```
Window > TextMeshPro > Import TMP Essential Resources
Window > Package Manager
```

### 5. Load Scene

1. In Project window, navigate to `Assets/Scenes/`
2. Double-click `VROffsetScene.unity`
3. Scene loads with all components

## Running the Application

### In Editor (Testing)

1. Connect VR headset to computer (Meta Link or equivalent)
2. Press Play in Unity Editor
3. Application runs in Play mode
4. Use sliders in UI to adjust offsets
5. Observe controller position changes in real-time

### Building APK

See **QUICK_START_APK.md** for fast build instructions
Or **BUILD_CONFIGURATION.md** for detailed configuration

### Installing on Device

```bash
# Prerequisites: Android Debug Bridge (adb) installed

# Connect device via USB
adb devices

# Install APK
adb install -r VRControllerOffset.apk

# Run app
adb shell am start -n com.vrcontroller.offsetapp/.UnityPlayerActivity

# View logs (optional)
adb logcat | grep Unity
```

## First Run

1. **Enable Developer Mode** on Android device if not already enabled
2. **Allow USB Debugging** when prompted on device
3. **Launch Application**:
   - App starts with VR scene
   - Controllers should appear as cubes
   - World-space UI overlay visible in front
4. **Test Offset Sliders**:
   - X-Axis: Move controller side-to-side
   - Y-Axis: Move controller up-and-down
   - Z-Axis: Move controller forward-and-backward

## Troubleshooting First Run

| Problem | Solution |
|---------|----------|
| Blank screen in VR | Check XR Plugin initialized; restart app |
| Controllers not visible | Verify controller models in ControllerOffsetManager |
| UI not visible | Ensure Canvas WorldSpace position in front of camera |
| Sliders not responding | Check OffsetUIManager script is attached to Canvas |
| No controller tracking | Enable hand tracking in device settings |

## Project Structure Overview

```
apk-vr-offset/
├── Assets/
│   ├── Scenes/
│   │   └── VROffsetScene.unity           # Main VR scene
│   ├── Scripts/
│   │   ├── ControllerOffsetManager.cs    # Core offset logic
│   │   ├── OffsetUIManager.cs            # UI slider management
│   │   ├── VRControllerModel.cs          # Controller visuals
│   │   └── VRSceneSetup.cs               # Scene initialization
│   ├── Prefabs/                          # Reusable components
│   └── Materials/                        # Shaders and materials
├── Packages/
│   └── manifest.json                     # Unity package dependencies
├── ProjectSettings/                      # Unity project config
├── AndroidManifest.xml                   # Android app manifest
├── README.md                             # Project overview
├── QUICK_START_APK.md                    # Fast APK build guide
├── BUILD_CONFIGURATION.md                # Detailed build settings
├── SCENE_SETUP_GUIDE.md                  # Scene configuration
└── CONTRIBUTING.md                       # Development guidelines
```

## Next Steps

1. **Explore**: Open VROffsetScene.unity and review the hierarchy
2. **Understand**: Read script comments in `Assets/Scripts/`
3. **Test**: Build APK and test on actual VR device
4. **Customize**: Modify offset ranges, UI styling, or controller models
5. **Deploy**: Share APK with users or submit to app store

## Support & Resources

- **Unity XR Documentation**: https://docs.unity3d.com/Manual/XRPlugin-Management.html
- **OpenXR Spec**: https://www.khronos.org/openxr/
- **Meta Quest Developer**: https://developer.oculus.com/
- **Project Issues**: GitHub Issues in this repository

## Hardware Compatibility

✅ **Tested On:**
- Meta Quest 2 / 3 / Pro
- HTC Vive Focus
- Other OpenXR-compatible devices

✅ **Expected Support:**
- Any Android VR headset with 6-DOF controllers
- Devices supporting OpenXR standard

❌ **Not Supported:**
- 3-DOF controllers (rotation-only)
- Non-OpenXR proprietary systems
