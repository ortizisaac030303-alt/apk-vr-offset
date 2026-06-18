# Android APK Export Quick Start

## Prerequisites

- Unity 2022 LTS installed with Android Build Support
- Android SDK and NDK configured in Unity Preferences
- JDK 11 or newer installed
- Minimum 5GB free disk space

## Quick Build Steps

### 1. Configure Build Settings (1 minute)

```
File > Build Settings
```

- Click "Android" in platform list
- Click "Switch Platform"
- Add scene: File > Build Settings > Add Open Scenes

### 2. Configure Player Settings (2 minutes)

```
Edit > Project Settings > Player
```

**General:**
- Company Name: Your Company
- Product Name: VR Controller Offset
- Version: 1.0
- Bundle Version Code: 1

**Android:**
- Minimum API Level: 23
- Target API Level: 34
- Scripting Backend: IL2CPP
- IL2CPP Code Generation: Faster (smaller, runtime AOT)

**XR Plug-in Management:**
- Enable OpenXR plugin
- Set OpenXR Features per device needs

### 3. Build APK (5-15 minutes)

```
File > Build Settings > Build
```

- Choose output directory
- Filename: `VRControllerOffset.apk`
- Click "Build"
- Wait for completion

### 4. Install on Device

```bash
adb install -r VRControllerOffset.apk
```

## Build Output Files

- **VRControllerOffset.apk**: Ready to install and distribute
- **VRControllerOffset.symbols.zip**: Debug symbols (keep for crash analysis)

## Common Issues & Fixes

| Issue | Solution |
|-------|----------|
| "XR Plugin not found" | Install com.unity.xr.openxr package via Package Manager |
| "Android SDK not found" | Set SDK path: Edit > Preferences > External Tools > Android |
| "Build fails with IL2CPP" | Update Unity to latest 2022 LTS version |
| "Controllers not tracking" | Verify XR Plugin Management OpenXR is enabled |
| "Out of memory" | Close other applications, increase system RAM |

## Performance Tips

- Use IL2CPP backend for 2-3x performance gain over Mono
- Enable GPU Instancing on materials
- Profile on actual device using Unity Profiler
- Reduce draw calls for UI canvas

## Distribution

- **Direct Installation**: Use `adb install` for testing
- **App Store**: Submit .apk to Meta Quest Store or Google Play
- **Sideloading**: Distribute .apk file directly to users
- **Enterprise**: Use Mobile Device Management for fleet deployment

## Next Steps

1. Test APK on VR device
2. Gather user feedback on offset ranges
3. Iterate on UI/UX
4. Prepare for production release

For detailed configuration, see **BUILD_CONFIGURATION.md**
