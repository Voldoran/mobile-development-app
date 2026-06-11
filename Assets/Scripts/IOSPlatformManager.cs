#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;

public class IOSPlatformManager : IPlatformManager
{
    public void ShowSystemPopup()
    {
        ShowSystemAlertPopup();
    }

    [DllImport("__Internal")]
    private static extern void ShowSystemAlertPopup();
}
#endif