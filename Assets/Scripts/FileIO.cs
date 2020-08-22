//using System;
//using System.Collections;
//using System.Collections.Generic;
using System.Runtime.InteropServices;
//using System.Linq;

using UnityEngine;

namespace Jammer
{

    /// <summary>
    /// UnityEngine.PlayerPrefs wrapper for WebGL LocalStorage
    /// </summary>
    public static class FileIO
    {
        public static bool UsingLocalStorage()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return true;
#else
            return false;
#endif

        }
        public static void DeleteKey(string key)
        {
            Debug.Log(string.Format("Jammer.PlayerPrefs.DeleteKey(key: {0})", key));

#if UNITY_WEBGL && !UNITY_EDITOR
        RemoveFromLocalStorage(key: key);
#else
            UnityEngine.PlayerPrefs.DeleteKey(key: key);
#endif
        }

        public static bool HasKey(string key)
        {
            Debug.Log(string.Format("Jammer.PlayerPrefs.HasKey(key: {0})", key));

#if UNITY_WEBGL && !UNITY_EDITOR
        return (HasKeyInLocalStorage(key) == 1);
#else
            return (UnityEngine.PlayerPrefs.HasKey(key: key));
#endif
        }

        public static string GetString(string key)
        {
            Debug.Log(string.Format("Jammer.PlayerPrefs.GetString(key: {0})", key));

#if UNITY_WEBGL && !UNITY_EDITOR
        return LoadFromLocalStorage(key: key);
#else
            return (UnityEngine.PlayerPrefs.GetString(key: key));
#endif
        }

        public static void SetString(string key, string value)
        {
            Debug.Log(string.Format("Jammer.PlayerPrefs.SetString(key: {0}, value: {1})", key, value));

#if UNITY_WEBGL && !UNITY_EDITOR
        SaveToLocalStorage(key: key, value: value);
#else
            UnityEngine.PlayerPrefs.SetString(key: key, value: value);
#endif

        }

        public static void Save()
        {
            Debug.Log(string.Format("Jammer.PlayerPrefs.Save()"));

#if !UNITY_WEBGL && !UNITY_EDITOR
            UnityEngine.PlayerPrefs.Save();
#endif
        }

#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
      private static extern void SaveToLocalStorage(string key, string value);

      [DllImport("__Internal")]
      private static extern string LoadFromLocalStorage(string key);

      [DllImport("__Internal")]
      private static extern void RemoveFromLocalStorage(string key);

      [DllImport("__Internal")]
      private static extern int HasKeyInLocalStorage(string key);
#endif
    }
}