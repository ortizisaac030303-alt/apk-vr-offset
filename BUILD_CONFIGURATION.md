# VR Controller Offset APK - Build Configuration Guide

## Android Build Settings

### Player Settings Configuration

1. **Company Name**: Your Company
2. **Product Name**: VR Controller Offset
3. **Default Icon**: Set your app icon (512x512 PNG)

### Android-Specific Settings

```
File > Build Settings > Android
```

- **Minimum API Level**: 23 (Android 6.0)
- **Target API Level**: 34 (Android 14)
- **Scripting Backend**: IL2CPP (recommended for performance)
- **ARM64 Architecture**: Enabled
- **Texture Compression**: ASTC

### XR Plugin Configuration

1. Go to **Edit > Project Settings > XR Plug-in Management**
2. Select **Android** tab
3. Enable **OpenXR**
4. In OpenXR settings, ensure these interaction profiles are enabled:
   - Meta Quest Touch Controller Profile
   - Generic XR Controller
   - Hand Interaction Profiles (if needed)

### Required Package Dependencies

```json
Required packages (auto-installed from manifest.json):
- com.unity.xr.openxr
- com.unity.xr.interaction.toolkit
- com.unity.xr.management
- com.unity.inputsystem
- com.unity.textmeshpro
```

## Building the APK

### Step-by-Step Instructions

1. **Open Build Settings**
   ```
   File > Build Settings
   ```

2. **Select Android Platform**
   - Click "Android" in the Scenes In Build list
   - Click "Switch Platform"

3. **Add Scenes to Build**
   - Click "Add Open Scenes" to add VROffsetScene.unity
   - Or manually drag scene into Scenes in Build

4. **Configure Build Settings**
   - Development Build: Disabled (for release)
   - Autoconnect Profiler: Disabled
   - Deep Profiling: Disabled

5. **Build APK**
   - Click **"Build"**
   - Choose output folder and filename (e.g., `VRControllerOffset.apk`)
   - Wait for build to complete

### Advanced Build Options

- **Export as Gradle Project**: Useful for further optimization or signing
- **Development Build**: Enable for debugging and testing
- **Scripts Only Build**: For rapid iteration

## Signing the APK (Release Build)

For distribution, you must sign the APK:

1. Go to **Edit > Project Settings > Player**
2. Navigate to **Android > Publishing Settings**
3. Create or use an existing keystore:
   - **Keystore Path**: Path to your keystore file
   - **Keystore Password**: Your keystore password
   - **Key Alias**: Your key alias
   - **Key Password**: Your key password

## Installing APK on Device

```bash
# Using ADB (Android Debug Bridge)
adb install -r VRControllerOffset.apk

# Or for force install over existing app
adb install -r -g VRControllerOffset.apk
```

## Troubleshooting

### Build Fails with XR Error
- Ensure XR Plugin Management is properly configured
- Verify OpenXR package is installed
- Check that Android SDK and NDK are properly installed

### Controllers Not Detected at Runtime
- Verify device is connected and XR is initialized
- Check `VRSceneSetup.cs` initialization logs
- Ensure correct interaction profiles are enabled

### UI Not Appearing
- Verify Canvas is set to WorldSpace rendering
- Check that camera is tagged as "MainCamera"
- Ensure TextMeshPro is properly imported

## Performance Optimization

- Use IL2CPP scripting backend for better performance
- Enable GPU instancing on controller materials
- Limit UI updates to necessary frames
- Profile with Deep Profiler in Development Build mode
