using UnityEngine;
using UnityEngine.XR.Management;

/// <summary>
/// Initializes XR settings and manages VR scene setup.
/// </summary>
public class VRSceneSetup : MonoBehaviour
{
    [SerializeField] private bool initializeXROnStart = true;
    [SerializeField] private ControllerOffsetManager offsetManagerPrefab;
    [SerializeField] private OffsetUIManager offsetUIPrefab;

    private void Start()
    {
        if (initializeXROnStart)
        {
            InitializeXR();
        }

        EnsureManagersExist();
    }

    private void InitializeXR()
    {
        XRGeneralSettings xrSettings = XRGeneralSettings.Instance;
        if (xrSettings != null && xrSettings.Manager != null)
        {
            if (!xrSettings.Manager.isInitializationComplete)
            {
                xrSettings.Manager.InitializeLoader();
            }

            if (!xrSettings.Manager.isInitializationComplete)
            {
                Debug.LogWarning("XR initialization failed. Check XR Plugin Management settings.");
            }
        }
    }

    private void EnsureManagersExist()
    {
        // Ensure ControllerOffsetManager exists
        if (ControllerOffsetManager.GetInstance() == null)
        {
            if (offsetManagerPrefab != null)
            {
                Instantiate(offsetManagerPrefab);
            }
            else
            {
                GameObject managerObj = new GameObject("ControllerOffsetManager");
                managerObj.AddComponent<ControllerOffsetManager>();
            }
        }

        // Ensure OffsetUIManager exists
        if (FindObjectOfType<OffsetUIManager>() == null && offsetUIPrefab != null)
        {
            Instantiate(offsetUIPrefab);
        }
    }
}
