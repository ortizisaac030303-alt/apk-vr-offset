using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

/// <summary>
/// Manages VR controller spatial offset calculations and tracking.
/// Formula: Final Position = Real Hardware Position + Custom Offset
/// </summary>
public class ControllerOffsetManager : MonoBehaviour
{
    [Header("Offset Values")]
    [SerializeField] private float offsetX = 0f;
    [SerializeField] private float offsetY = 0f;
    [SerializeField] private float offsetZ = 0f;

    [Header("Offset Limits")]
    [SerializeField] private float minOffset = -1f;
    [SerializeField] private float maxOffset = 1f;

    [Header("Controller References")]
    [SerializeField] private Transform leftControllerModel;
    [SerializeField] private Transform rightControllerModel;

    private XRNode leftHandNode = XRNode.LeftHand;
    private XRNode rightHandNode = XRNode.RightHand;

    private Vector3 leftHandRealPosition;
    private Vector3 rightHandRealPosition;

    private Vector3 offsetVector = Vector3.zero;

    private static ControllerOffsetManager instance;

    public float OffsetX
    {
        get { return offsetX; }
        set { offsetX = Mathf.Clamp(value, minOffset, maxOffset); }
    }

    public float OffsetY
    {
        get { return offsetY; }
        set { offsetY = Mathf.Clamp(value, minOffset, maxOffset); }
    }

    public float OffsetZ
    {
        get { return offsetZ; }
        set { offsetZ = Mathf.Clamp(value, minOffset, maxOffset); }
    }

    public float MinOffset => minOffset;
    public float MaxOffset => maxOffset;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Update()
    {
        UpdateOffsetVector();
        UpdateControllerPositions();
    }

    private void UpdateOffsetVector()
    {
        offsetVector = new Vector3(offsetX, offsetY, offsetZ);
    }

    private void UpdateControllerPositions()
    {
        // Get left hand real position from XR tracking
        if (TryGetTrackingPosition(leftHandNode, out Vector3 leftRealPos))
        {
            leftHandRealPosition = leftRealPos;
            if (leftControllerModel != null)
            {
                // Apply offset: Final Position = Real Hardware Position + Custom Offset
                leftControllerModel.position = leftHandRealPosition + offsetVector;
            }
        }

        // Get right hand real position from XR tracking
        if (TryGetTrackingPosition(rightHandNode, out Vector3 rightRealPos))
        {
            rightHandRealPosition = rightRealPos;
            if (rightControllerModel != null)
            {
                // Apply offset: Final Position = Real Hardware Position + Custom Offset
                rightControllerModel.position = rightHandRealPosition + offsetVector;
            }
        }
    }

    private bool TryGetTrackingPosition(XRNode node, out Vector3 position)
    {
        position = Vector3.zero;
        InputDevice device = InputDevices.GetDeviceAtXRNode(node);

        if (!device.isValid)
            return false;

        if (device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 trackingPosition))
        {
            position = trackingPosition;
            return true;
        }

        return false;
    }

    public Vector3 GetOffsetVector()
    {
        return offsetVector;
    }

    public void SetAllOffsets(float x, float y, float z)
    {
        OffsetX = x;
        OffsetY = y;
        OffsetZ = z;
    }

    public static ControllerOffsetManager GetInstance()
    {
        return instance;
    }
}
