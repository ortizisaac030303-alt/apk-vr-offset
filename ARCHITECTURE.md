# Project Architecture & Technical Overview

## System Architecture

### Core Components

```
┌─────────────────────────────────────┐
│   XR Origin (VR Camera & Tracking)  │
│  ┌──────────────────────────────┐   │
│  │  Left Controller Model       │   │
│  │  Right Controller Model      │   │
│  └──────────────────────────────┘   │
└─────────────────────────────────────┘
           ↓
┌─────────────────────────────────────┐
│  ControllerOffsetManager            │
│  ┌──────────────────────────────┐   │
│  │ Reads XR Input (Position)    │   │
│  │ Applies Offset Formula       │   │
│  │ Updates Model Positions      │   │
│  └──────────────────────────────┘   │
└─────────────────────────────────────┘
     ↑                    ↓
┌────────────────┐  ┌──────────────┐
│ OffsetUIManager│  │ Controller   │
│ (UI Sliders)   │  │ Visuals      │
└────────────────┘  └──────────────┘
```

## Class Diagram

### ControllerOffsetManager

```
ControllerOffsetManager : MonoBehaviour
├── Properties:
│   ├── offsetX : float
│   ├── offsetY : float
│   ├── offsetZ : float
│   ├── minOffset : float = -1.0
│   └── maxOffset : float = 1.0
├── Methods:
│   ├── Update() : void
│   ├── UpdateOffsetVector() : void
│   ├── UpdateControllerPositions() : void
│   ├── TryGetTrackingPosition(XRNode, out Vector3) : bool
│   ├── SetAllOffsets(float, float, float) : void
│   └── GetOffsetVector() : Vector3
└── Singleton Instance
```

### OffsetUIManager

```
OffsetUIManager : MonoBehaviour
├── UI References:
│   ├── xAxisSlider : Slider
│   ├── yAxisSlider : Slider
│   ├── zAxisSlider : Slider
│   ├── xAxisValueText : TextMeshProUGUI
│   ├── yAxisValueText : TextMeshProUGUI
│   └── zAxisValueText : TextMeshProUGUI
├── Methods:
│   ├── Start() : void
│   ├── InitializeSliders() : void
│   ├── OnXAxisChanged(float) : void
│   ├── OnYAxisChanged(float) : void
│   ├── OnZAxisChanged(float) : void
│   └── UpdateDisplay() : void
└── Manages world-space canvas positioning
```

### VRControllerModel

```
VRControllerModel : MonoBehaviour
├── Properties:
│   ├── controllerNode : XRNode
│   ├── trackRotation : bool = true
│   ├── modelScale : float = 1.0
│   ├── activeColor : Color
│   └── inactiveColor : Color
├── Methods:
│   ├── Update() : void
│   ├── UpdateControllerDevice() : void
│   ├── UpdateControllerRotation() : void
│   ├── UpdateVisualFeedback() : void
│   └── IsTracked : bool (property)
└── Handles visual representation
```

## Data Flow

### Initialization Flow

```
Scene Load
   ↓
VRSceneSetup.Start()
   ├→ InitializeXR()
   │   └→ XRGeneralSettings.Manager.InitializeLoader()
   ├→ EnsureManagersExist()
   │   ├→ Create ControllerOffsetManager if missing
   │   └→ Create OffsetUIManager if missing
   ↓
ControllerOffsetManager.Awake()
   └→ Register singleton instance
   ↓
OffsetUIManager.Start()
   ├→ Get ControllerOffsetManager instance
   ├→ Initialize sliders with offset ranges
   └→ Register slider callbacks
   ↓
VRControllerModel.Start()
   └→ Get controller device from XR Input System
   ↓
Runtime Ready
```

### Runtime Data Flow (Every Frame)

```
ControllerOffsetManager.Update()
   ├→ UpdateOffsetVector()
   │   └→ offsetVector = (offsetX, offsetY, offsetZ)
   ├→ UpdateControllerPositions()
   │   ├→ Get Left Hand Position from XR Tracking
   │   │   └→ finalPos = trackingPos + offsetVector
   │   ├→ Get Right Hand Position from XR Tracking
   │   │   └→ finalPos = trackingPos + offsetVector
   │   └→ Update controller model positions
   ↓
VRControllerModel.Update()
   ├→ Get controller rotation from XR Tracking
   ├→ Update model rotation and visual feedback
   └→ Indicate tracking status (color)
   ↓
UI Slider Interaction
   ├→ User adjusts slider
   ├→ OnValueChanged callback fires
   ├→ OffsetUIManager.OnAxisChanged(value)
   │   └→ ControllerOffsetManager.OffsetX/Y/Z = value
   └→ Display updated offset value on text
```

## Coordinate Systems

### World Space Coordinates
- **X-Axis**: Left (-) to Right (+)
- **Y-Axis**: Down (-) to Up (+)
- **Z-Axis**: Back (-) to Forward (+)

### Offset Formula

```csharp
FinalPosition = RealHardwarePosition + CustomOffset
where:
  RealHardwarePosition = XR tracking device position (X, Y, Z)
  CustomOffset = (offsetX, offsetY, offsetZ) from sliders
  FinalPosition = Rendered virtual hand position
```

### Tracking Pipeline

```
Physical VR Controller
         ↓
XR Tracking System
         ↓
InputDevice.TryGetFeatureValue(CommonUsages.devicePosition)
         ↓
RealHardwarePosition (X, Y, Z)
         ↓
Add CustomOffset
         ↓
FinalPosition → Render Controller Visual
```

## Input Handling

### XR Input System

```
InputDevices.GetDeviceAtXRNode(XRNode)
   ↓
InputDevice
   ├→ TryGetFeatureValue(CommonUsages.devicePosition, out Vector3)
   ├→ TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion)
   └→ TryGetFeatureValue(CommonUsages.trigger, out float) [NOT USED]
```

### UI Input

```
Slider.onValueChanged
   ├→ OnXAxisChanged(float)
   ├→ OnYAxisChanged(float)
   └→ OnZAxisChanged(float)
   ↓
Update ControllerOffsetManager values
```

## Performance Considerations

### Frame Timing
- **Update Loop**: ~16.67ms per frame (60 FPS target)
- **XR Tracking**: Native XR subsystem handles tracking
- **Position Calculation**: Simple vector addition (minimal CPU cost)
- **UI Updates**: Only when slider changes

### Memory Usage
- **Offset Values**: 3 × 4 bytes (float) = 12 bytes
- **Vector3 Cache**: 3 × 4 bytes = 12 bytes
- **UI Canvas**: ~500KB-1MB depending on complexity
- **Total**: <5MB for application logic

### Optimization Tips

1. **Cache offsetVector** to avoid recalculation
2. **Update UI only on value change** (not every frame)
3. **Use IL2CPP** scripting backend for ~2-3x performance
4. **Disable unnecessary components** on controller models

## Error Handling

### XR Device Not Found
```csharp
if (!device.isValid)
{
    // Handle gracefully, show UI indicator
    // Set model color to inactive
    return false;
}
```

### Missing Manager References
```csharp
if (offsetManager == null)
{
    Debug.LogError("ControllerOffsetManager not found!");
    return; // Skip updates
}
```

### Invalid Offset Values
```csharp
public float OffsetX
{
    get { return offsetX; }
    set { offsetX = Mathf.Clamp(value, minOffset, maxOffset); }
}
```

## Extension Points

### Adding New Features

1. **Velocity Offset**: Track and apply velocity vectors
2. **Rotation Offset**: Add rotation modifications
3. **Preset Profiles**: Save/load offset configurations
4. **Calibration Mode**: Auto-adjust offsets for device
5. **Recording/Playback**: Record offset sequences

### Customization Areas

- **UI Styling**: Modify canvas materials and layouts
- **Controller Models**: Replace cubes with custom meshes
- **Offset Ranges**: Adjust min/max values
- **Update Frequency**: Modify UpdateControllerPositions timing

## Testing Strategy

### Unit Testing Areas
- Offset calculation formula
- Vector clamping logic
- Singleton pattern

### Integration Testing
- XR device communication
- UI slider callbacks
- Position updates

### Device Testing
- Actual controller tracking
- UI responsiveness on target hardware
- Performance profiling
- Battery consumption

## Deployment Pipeline

```
Develop → Test → Build → Sign → Deploy
   ↓        ↓      ↓      ↓       ↓
Editor   Device  APK   Keystore Android
PlayMode  Debug         Device
```
