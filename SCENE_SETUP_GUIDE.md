# VR Offset Scene Setup - Manual Configuration Guide

## Creating the VROffsetScene.unity Scene

This guide walks through manually creating the scene in Unity since we're providing scripts and configuration.

### Step 1: Create Base Scene Structure

1. **Create New Scene**
   - File > New Scene
   - Choose "3D Scene" template
   - Save as `Assets/Scenes/VROffsetScene.unity`

2. **Remove Default Objects**
   - Delete the default Directional Light and Main Camera
   - We'll use XR Origin for camera management

### Step 2: Set Up XR Origin

1. **Create XR Origin**
   - Right-click in Hierarchy > 3D Object > XR > XR Origin
   - This provides VR camera tracking and controller input

2. **Configure XR Origin**
   - Set Position to (0, 0, 0)
   - Set Scale to (1, 1, 1)
   - Child objects should include:
     - Camera Offset (child)
     - Main Camera (grandchild of Camera Offset)

### Step 3: Create Controller Models

1. **Left Controller Model**
   - Create Empty GameObject named "LeftControllerModel"
   - Parent to XR Origin
   - Add 3D Cube as child (scale: 0.05, 0.15, 0.08)
   - Attach `VRControllerModel.cs` script
   - Set Controller Node to "LeftHand"
   - Create Material with color for visual distinction

2. **Right Controller Model**
   - Create Empty GameObject named "RightControllerModel"
   - Parent to XR Origin
   - Add 3D Cube as child (scale: 0.05, 0.15, 0.08)
   - Attach `VRControllerModel.cs` script
   - Set Controller Node to "RightHand"

3. **Visual Feedback (Materials)**
   - Create Material "ControllerMaterial"
   - Set Shader to Standard
   - Assign to both controller cubes
   - Active Color: Green (0, 1, 0)
   - Inactive Color: Gray (0.5, 0.5, 0.5)

### Step 4: Add Offset Manager

1. **Create Offset Manager**
   - Create Empty GameObject named "ControllerOffsetManager"
   - Attach `ControllerOffsetManager.cs` script
   - Set as root-level GameObject in scene

2. **Configure Offset Manager**
   - Drag LeftControllerModel into "Left Controller Model" field
   - Drag RightControllerModel into "Right Controller Model" field
   - Set Min Offset: -1.0
   - Set Max Offset: 1.0

### Step 5: Create World-Space UI Canvas

1. **Create Canvas**
   - Right-click in Hierarchy > UI > Canvas
   - Set Canvas to "World Space" rendering mode
   - Attach `OffsetUIManager.cs` script

2. **Canvas Configuration**
   - Rect Transform:
     - Position: (0, 1.5, 0.75) - In front of player at eye level
     - Rotation: Face player (rotate 180° on Y-axis)
     - Scale: (0.01, 0.01, 0.01) - Scale down for world space
     - Width: 1920, Height: 1080
   - Canvas component:
     - Render Mode: World Space
     - Canvas Scaler: Dynamic Pixels Per Unit = 10

3. **Add UI Background Panel**
   - Create Image as child of Canvas
   - Name: "BackgroundPanel"
   - Color: Black with ~80% transparency (RGBA: 0, 0, 0, 0.8)
   - Rect Transform:
     - Anchors: Stretch, Stretch
     - Offset Left: -20, Offset Right: -20
     - Offset Top: -20, Offset Bottom: -20

### Step 6: Add X-Axis Offset Controls

1. **Create X-Axis Container**
   - Create Vertical Layout Group as child of Canvas
   - Name: "XAxisContainer"
   - Rect Transform: Set appropriate spacing

2. **Add X-Axis Label**
   - Create Text (TextMeshPro) child
   - Name: "XAxisLabel"
   - Text: "X-Axis Offset"
   - Font Size: 50

3. **Add X-Axis Slider**
   - Create Slider (UI) as child
   - Name: "XAxisSlider"
   - Slider component:
     - Min Value: -1
     - Max Value: 1
     - Value: 0
     - Whole Numbers: False
   - Configure handle and background styling

4. **Add X-Axis Value Display**
   - Create Text (TextMeshPro) child
   - Name: "XAxisValueText"
   - Text: "X: 0.000"
   - Font Size: 40

5. **Link to OffsetUIManager**
   - In scene OffsetUIManager component:
     - Drag XAxisSlider to "X Axis Slider" field
     - Drag XAxisValueText to "X Axis Value Text" field

### Step 7: Add Y-Axis Offset Controls

Repeat Step 6 for Y-Axis:
- Container: "YAxisContainer"
- Label: "Y-Axis Offset"
- Slider: "YAxisSlider"
- Value Display: "YAxisValueText"
- Link YAxisSlider and YAxisValueText to OffsetUIManager

### Step 8: Add Z-Axis Offset Controls

Repeat Step 6 for Z-Axis:
- Container: "ZAxisContainer"
- Label: "Z-Axis Offset"
- Slider: "ZAxisSlider"
- Value Display: "ZAxisValueText"
- Link ZAxisSlider and ZAxisValueText to OffsetUIManager

### Step 9: Add Scene Setup Manager

1. **Create Setup Manager**
   - Create Empty GameObject named "VRSceneSetup"
   - Attach `VRSceneSetup.cs` script
   - Set as root-level in scene

2. **Configure Setup Manager**
   - Initialize XR On Start: Enabled (checked)
   - Assign prefabs if available

### Step 10: Final Scene Hierarchy

Your scene hierarchy should look like:

```
VROffsetScene
├── XR Origin
│   ├── Camera Offset
│   │   └── Main Camera (tag: "MainCamera")
│   ├── LeftControllerModel
│   │   └── Cube (controller visual)
│   └── RightControllerModel
│       └── Cube (controller visual)
├── ControllerOffsetManager
├── Canvas (World Space)
│   ├── BackgroundPanel
│   ├── XAxisContainer
│   │   ├── XAxisLabel
│   │   ├── XAxisSlider
│   │   └── XAxisValueText
│   ├── YAxisContainer
│   │   ├── YAxisLabel
│   │   ├── YAxisSlider
│   │   └── YAxisValueText
│   └── ZAxisContainer
│       ├── ZAxisLabel
│       ├── ZAxisSlider
│       └── ZAxisValueText
└── VRSceneSetup
```

### Step 11: Configure XR Plugin Management

1. **Open XR Plugin Management**
   - Edit > Project Settings > XR Plug-in Management

2. **General Settings Tab**
   - Check "Initialize XR on Startup"

3. **Android Tab**
   - Enable "OpenXR"
   - Set OpenXR Features:
     - Meta Quest Support
     - Hand Tracking
     - Eye Tracking (optional)

4. **OpenXR Settings**
   - Set Rendering Mode: Multi-Pass or Single-Pass (device dependent)
   - Depth Submission Mode: Depth 16-bit
   - Update XR Loader: OpenXR Loader

### Step 12: Test in Editor

1. **Play Mode**
   - Press Play to test scene
   - You should see:
     - XR camera with Main Camera active
     - Controller cubes (gray until tracking)
     - World-space UI overlay in front

2. **Simulator Mode (if available)**
   - In Play Mode, use OpenXR Simulator (if installed)
   - Move mouse to simulate controller position
   - Drag with mouse to rotate

### Step 13: Save and Export

1. **Save Scene**
   - File > Save Scene
   - Verify saved as `Assets/Scenes/VROffsetScene.unity`

2. **Add to Build Settings**
   - File > Build Settings
   - Drag VROffsetScene.unity into Scenes in Build
   - Should be index 0 (first scene)

3. **Build APK**
   - Follow BUILD_CONFIGURATION.md instructions

## Troubleshooting Scene Setup

### Canvas Not Visible
- Verify Canvas Render Mode is "World Space"
- Check Canvas position is in front of camera (positive Z)
- Ensure Canvas Scaler has proper DPI settings
- Verify UI elements have proper Layout Groups configured

### Controllers Not Tracking
- Ensure XR Origin is in scene
- Verify Main Camera is child of XR Origin
- Check XR Plugin Management is configured
- Verify controller models are assigned to ControllerOffsetManager

### Sliders Not Responding
- Verify slider components have OnValueChanged listeners
- Check OffsetUIManager has sliders assigned
- Ensure ControllerOffsetManager is in scene
- Verify scripts are attached to correct GameObjects

### Performance Issues
- Reduce UI complexity or poly count of controller models
- Disable unnecessary components on controllers
- Use simpler materials (avoid complex shaders)
- Profile with Unity Profiler to identify bottlenecks
