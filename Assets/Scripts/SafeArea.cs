using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteAlways]
public class SafeArea : MonoBehaviour
{
    // AppId: ca-app-pub-3940256099942544~3347511713
    // UnitId RewardedAds: ca-app-pub-3940256099942544/5224354917
    public RectTransform RectTransform;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI PlatformText;
    
    [Range(0.1f, 1.0f)]
    public float ResolutionScaling = 1.0f;

    private Rect safeArea;

    private void Awake()
    {
        ApplySafeArea();
        
        PlayerInput playerInput = GetComponent<PlayerInput>();
        playerInput.onActionTriggered += OnInputActionTriggered;
        
        #if UNITY_EDITOR
        PlatformText.text = "Editor";
        #elif MOBILE
        PlatformText.text = "Mobile";
        #endif
    }

    private void OnInputActionTriggered(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Click":
                TitleText.text = $"TouchState: {context.action.ReadValue<float>()}";
                break;
        }
    }

    public void Update()
    {
        if (safeArea != Screen.safeArea)
        {
            ApplySafeArea();
        }
        
        // ScalableBufferManager.ResizeBuffers(ResolutionScaling, ResolutionScaling);
    }

    private void ApplySafeArea()
    {
        if (RectTransform == null)
        {
            return;
        }
        
        safeArea = Screen.safeArea;
        
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        
        RectTransform.anchorMin = anchorMin;
        RectTransform.anchorMax = anchorMax;
    }
}