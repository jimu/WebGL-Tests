using System.Runtime.InteropServices;
using UnityEngine;

public static class Opiframe
{
    public static string GetTitle()
    {
        string title;

        #if UNITY_WEBGL && !UNITY_EDITOR
                title = GetDocumentTitle();
        #else
                title = "n/a";
        #endif

        Debug.Log(string.Format("GetTitle: " + title));
        
        return title;
    }


    public static void ToggleMute()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            PostMuteMessage();
#else
        Debug.Log("ToggleMute: Do Nothing");
#endif
    }

    public static void Unmute()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            UnmuteJS();
#else
        Debug.Log("Unmute: Do Nothing");
#endif
    }


#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern string GetDocumentTitle();

    [DllImport("__Internal")]
    private static extern void PostMuteMessage();

    [DllImport("__Internal")]
    private static extern void UnmuteJS();
#endif
}
