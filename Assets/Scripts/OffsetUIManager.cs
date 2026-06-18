using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages the world-space UI overlay for real-time offset adjustment.
/// Provides three sliders for X, Y, and Z axis offset control.
/// </summary>
public class OffsetUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider xAxisSlider;
    [SerializeField] private Slider yAxisSlider;
    [SerializeField] private Slider zAxisSlider;

    [SerializeField] private TextMeshProUGUI xAxisValueText;
    [SerializeField] private TextMeshProUGUI yAxisValueText;
    [SerializeField] private TextMeshProUGUI zAxisValueText;

    [Header("Canvas Settings")]
    [SerializeField] private Canvas worldSpaceCanvas;
    [SerializeField] private float canvasDistance = 0.75f;
    [SerializeField] private float canvasHeight = 1.5f;

    private ControllerOffsetManager offsetManager;

    private void Start()
    {
        offsetManager = ControllerOffsetManager.GetInstance();

        if (offsetManager == null)
        {
            Debug.LogError("ControllerOffsetManager not found in scene!");
            return;
        }

        InitializeSliders();
        PositionCanvasInFrontOfPlayer();
    }

    private void InitializeSliders()
    {
        if (xAxisSlider != null)
        {
            xAxisSlider.minValue = offsetManager.MinOffset;
            xAxisSlider.maxValue = offsetManager.MaxOffset;
            xAxisSlider.value = offsetManager.OffsetX;
            xAxisSlider.onValueChanged.AddListener(OnXAxisChanged);
        }

        if (yAxisSlider != null)
        {
            yAxisSlider.minValue = offsetManager.MinOffset;
            yAxisSlider.maxValue = offsetManager.MaxOffset;
            yAxisSlider.value = offsetManager.OffsetY;
            yAxisSlider.onValueChanged.AddListener(OnYAxisChanged);
        }

        if (zAxisSlider != null)
        {
            zAxisSlider.minValue = offsetManager.MinOffset;
            zAxisSlider.maxValue = offsetManager.MaxOffset;
            zAxisSlider.value = offsetManager.OffsetZ;
            zAxisSlider.onValueChanged.AddListener(OnZAxisChanged);
        }
    }

    private void PositionCanvasInFrontOfPlayer()
    {
        if (worldSpaceCanvas != null)
        {
            // Position canvas in front of player at camera position
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                Vector3 canvasPos = mainCam.transform.position + mainCam.transform.forward * canvasDistance;
                canvasPos.y = canvasHeight;
                worldSpaceCanvas.transform.position = canvasPos;
                worldSpaceCanvas.transform.rotation = Quaternion.LookRotation(mainCam.transform.forward);
            }
        }
    }

    private void OnXAxisChanged(float value)
    {
        offsetManager.OffsetX = value;
        UpdateXAxisDisplay();
    }

    private void OnYAxisChanged(float value)
    {
        offsetManager.OffsetY = value;
        UpdateYAxisDisplay();
    }

    private void OnZAxisChanged(float value)
    {
        offsetManager.OffsetZ = value;
        UpdateZAxisDisplay();
    }

    private void UpdateXAxisDisplay()
    {
        if (xAxisValueText != null)
        {
            xAxisValueText.text = $"X: {offsetManager.OffsetX:F3}";
        }
    }

    private void UpdateYAxisDisplay()
    {
        if (yAxisValueText != null)
        {
            yAxisValueText.text = $"Y: {offsetManager.OffsetY:F3}";
        }
    }

    private void UpdateZAxisDisplay()
    {
        if (zAxisValueText != null)
        {
            zAxisValueText.text = $"Z: {offsetManager.OffsetZ:F3}";
        }
    }
}
