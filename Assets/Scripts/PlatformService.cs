using UnityEngine;
using UnityEngine.UI;

public class PlatformService : MonoBehaviour
{
    public Button Button;
    
    private IPlatformManager platformManager;
    
    private void Awake()
    {
        gameObject.name = nameof(PlatformService);
        
#if UNITY_ANDROID && !UNITY_EDITOR
        platformManager = new AndroidPlatformManager();
#elif  UNITY_IOS && !UNITY_EDITOR
        platformManager = new IOSPlatformManager();
#endif
        
        Button.onClick.AddListener(OnButtonClicked);
    }

    public void OnSystemAlertPopupResult(string result)
    {
        Debug.Log($"{nameof(PlatformService)} ## Native Popup Result: {result}");
    }

    private void OnButtonClicked()
    {
        platformManager?.ShowSystemPopup();
    }
}