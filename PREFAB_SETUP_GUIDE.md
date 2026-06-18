# Prefab Setup Guide

This guide provides templates for creating reusable prefabs from the scene components.

## Creating Prefabs from Existing Scene Objects

### VRControllerModel Prefab

1. **Prepare Scene Object**
   - In scene, select LeftControllerModel (or RightControllerModel)
   - Ensure it has:
     - VRControllerModel.cs script attached
     - Child cube with Material assigned
     - Proper transforms configured

2. **Create Prefab**
   - Drag object from Hierarchy to `Assets/Prefabs/`
   - Name it "VRControllerModel.prefab"
   - Select "Original" to keep scene instance linked

3. **Configure Prefab**
   - Open prefab by double-clicking
   - Verify all child objects are correct
   - Ensure script inspector shows all fields properly assigned
   - Save prefab

### OffsetUICanvas Prefab

1. **Prepare Canvas**
   - In scene, select Canvas (world-space UI)
   - Ensure it has:
     - OffsetUIManager.cs script
     - All slider and text components as children
     - Background panel configured

2. **Create Prefab**
   - Drag Canvas from Hierarchy to `Assets/Prefabs/`
   - Name it "OffsetUICanvas.prefab"
   - Select "Original" to keep scene instance linked

3. **Verify Prefab**
   - Double-click to open in edit mode
   - Check all UI hierarchy is intact
   - Verify OffsetUIManager script references are set
   - Save prefab

### ControllerOffsetManager Prefab

1. **Prepare Manager**
   - Select ControllerOffsetManager GameObject in scene
   - Ensure script is properly configured with all fields assigned

2. **Create Prefab**
   - Drag to `Assets/Prefabs/`
   - Name it "ControllerOffsetManager.prefab"
   - Select "Original"

3. **Note**: This prefab has scene references, so update references when instantiating in new scenes

## Using Prefabs

### In New Scenes

1. **Drag Prefab into Scene**
   - Simply drag prefab from `Assets/Prefabs/` into Hierarchy
   - Prefab instance is created

2. **Update References if Needed**
   - For ControllerOffsetManager: assign new controller model references
   - For OffsetUICanvas: verify slider references are correct

### Prefab Variants

Create variants for different configurations:

1. **Right-Hand Focused Variant**
   - Right-click on VRControllerModel.prefab
   - Create Prefab Variant
   - Name: "VRControllerModel_RightHand.prefab"
   - Set ControllerNode to RightHand by default

2. **Left-Hand Focused Variant**
   - Create variant named "VRControllerModel_LeftHand.prefab"
   - Set ControllerNode to LeftHand by default

## Updating Prefabs

When modifying a prefab in the scene:

1. **Make Changes to Instance**
   - Modify the instance in hierarchy

2. **Apply Changes to Prefab**
   - Right-click instance in Hierarchy
   - Select "Prefab" > "Overrides" > "Apply All"
   - Or click "Overrides" dropdown in Inspector and select "Apply All"

3. **Revert Changes**
   - Right-click instance
   - Select "Prefab" > "Revert"

## Prefab File Structure

Expected folder structure:

```
Assets/Prefabs/
├── VRControllerModel.prefab
├── VRControllerModel_RightHand.prefab
├── VRControllerModel_LeftHand.prefab
├── OffsetUICanvas.prefab
├── ControllerOffsetManager.prefab
└── (other prefabs as needed)
```

## Best Practices

1. **Keep Prefabs Clean**
   - Remove debug components before saving
   - Verify all scripts are attached and configured
   - Ensure materials and textures are properly assigned

2. **Document Prefab Usage**
   - Add comments in scripts about prefab requirements
   - Include setup notes in prefab Inspector

3. **Version Control**
   - Commit prefab changes separately from scene changes
   - Use meaningful commit messages for prefab updates

4. **Performance**
   - Keep prefab complexity reasonable
   - Use LOD (Level of Detail) for complex models
   - Optimize material and shader usage
