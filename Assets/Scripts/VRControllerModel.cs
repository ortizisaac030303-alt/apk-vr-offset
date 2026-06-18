using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// Represents a VR controller model with tracking and visual representation.
/// Position is updated by ControllerOffsetManager with applied offsets.
/// </summary>
public class VRControllerModel : MonoBehaviour
{
    [Header("Controller Settings")]
    [SerializeField] private XRNode controllerNode = XRNode.RightHand;
    [SerializeField] private bool trackRotation = true;
    [SerializeField] private float modelScale = 1f;

    [Header("Visual Settings")]
    [SerializeField] private Material controllerMaterial;
    [SerializeField] private Color activeColor = Color.green;
    [SerializeField] private Color inactiveColor = Color.gray;

    private InputDevice controllerDevice;
    private Renderer modelRenderer;
    private bool isTracked = false;

    private void Start()
    {
        modelRenderer = GetComponentInChildren<Renderer>();
        UpdateControllerDevice();

        // Set initial scale
        transform.localScale = Vector3.one * modelScale;
    }

    private void Update()
    {
        UpdateControllerDevice();
        UpdateControllerRotation();
        UpdateVisualFeedback();
    }

    private void UpdateControllerDevice()
    {
        controllerDevice = InputDevices.GetDeviceAtXRNode(controllerNode);
    }

    private void UpdateControllerRotation()
    {
        if (!controllerDevice.isValid)
            return;

        if (trackRotation && controllerDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation))
        {
            transform.rotation = rotation;
        }
    }

    private void UpdateVisualFeedback()
    {
        if (controllerDevice.isValid && modelRenderer != null)
        {
            isTracked = true;
            if (controllerMaterial != null)
            {
                modelRenderer.material.color = activeColor;
            }
        }
        else
        {
            isTracked = false;
            if (modelRenderer != null && controllerMaterial != null)
            {
                modelRenderer.material.color = inactiveColor;
            }
        }
    }

    public bool IsTracked => isTracked;
    public XRNode ControllerNode => controllerNode;
}
