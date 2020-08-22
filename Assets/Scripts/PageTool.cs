// REF: https://forum.unity.com/threads/enable-fullscreen-webgl.441174/#post-4949471


using UnityEngine;
using System.Runtime.InteropServices;

#pragma warning disable 0649
#pragma warning disable 0414


public class PageTool : MonoBehaviour
{
    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        
    }
    public void ToggleFullscreen()
    {
        ActivateFullscreen(!Screen.fullScreen);
    }


    public void ActivateFullscreen(bool isFullScreen = true)
    {
        if (!isFullScreen)
            Screen.fullScreen = false;
        else
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            GoFullscreen(isFullScreen);
#else
            Screen.fullScreen = true;
#endif
        }

    }

    [DllImport("__Internal")]
    private static extern void GoFullscreen(bool isFullScreen);

}
