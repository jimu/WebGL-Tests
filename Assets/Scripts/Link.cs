using UnityEngine;
using System.Runtime.InteropServices;

#pragma warning disable 0649
#pragma warning disable 0414


public class Link : MonoBehaviour
{
    [SerializeField] string[] urls;
    public void OpenLinkJSPlugin(int iUrl = 0)
    {
#if !UNITY_EDITOR
openWindow(urls[iUrl]);
#endif
    }

    [DllImport("__Internal")]
    private static extern void openWindow(string url);

}
