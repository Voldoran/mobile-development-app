#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine;

public class AndroidPlatformManager : IPlatformManager
{
    public void ShowSystemPopup()
    {
        using var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            var builder = new AndroidJavaObject("android.app.AlertDialog$Builder", activity);
            builder.Call<AndroidJavaObject>("setTitle", "Warning");
            builder.Call<AndroidJavaObject>("setMessage", "Some important message");
            builder.Call<AndroidJavaObject>("setPositiveButton", "Confirm", new DialogClickListener());
            
            var dialog = builder.Call<AndroidJavaObject>("create");
            dialog.Call("show");
        }));
    }
}

public class DialogClickListener : AndroidJavaProxy
{
    public DialogClickListener() : base("android.content.DialogInterface$OnClickListener")
    { }

    public void onClick(AndroidJavaObject dialog, int which)
    {
        Debug.Log($"AndroidPlatformManager ## OnClick: {which}");        
    }
}
#endif