using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

#pragma warning disable 0649

public class GameManager : MonoBehaviour
{
    static readonly string DATA_KEY = "WebGLBrowserTests.value";

    int value = 0;
    static int svalue = 0;

    Text outputText;
    static readonly Vector2 bottom = new Vector2(1f, 0f);

    [SerializeField] ScrollRect scrollRect;


    private void Awake()
    {

        outputText = GameObject.Find("Output").GetComponent<Text>();
        Debug.Log(outputText);
        Output("Application.unityVersion: " + Application.unityVersion);
        Output("Application.version: " + Application.version);
        Output("Press ABOUT for help.\n");
    }

    private void Start()
    {
        DisplayValues();
        ShowPersistantValues();
        UpdateMuteButtonLabel();
        ReportSoundStatus();
    }

    public void OnLoadScenePressed()
    {
        Debug.Log("LoadScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnAboutPressed()
    {
        Debug.Log("LoadScene");
        Output("\nHELP\n\nCertain Unity operations do not work as expected in WebGL builds. " +
            "Browsers restrict operations prone to abuse such as opening new browser windows, " +
            "switching to full screen and automatically playing sound. " +
            "These are called \"privileged operations\" and implementing " +
            "them properly requires special handling. " +
            "Sometimes this can be accomplished by using an external .jslib module.");
        Output("FULLSCREEN: Trying to switch to full screen using standard method \"Screen.fullScreen(...)\" in a WebGL build requires the user to press a button and then click a second time anywhere in the browser window");
        Output("Switching to full screen with a single click can be accomplished by using an external .jslib module");
        Output("OPEN BROWSER PAGE IN NEW TAB: Opening a web page in a new browser tab is a privileged operation but can be accomplished using an external .jslib module");
        Output("COOKIES: Some browser cookies are readable via jslib, but possibly limited by cross-origin/iframe restrictions.");
        Output("PLAYER PREFS: Trying to persist data using PlayerPrefs typically doesn't work across builds; the Unity framework " +
            "may consider your new version a different application, in which case your updated version will be unable to access the data you were " +
            "trying to persist (like player name and highscore).");
        Output("The browser's LocalStorage mechanism can be used to persist data across all applications  (hosted on a particular domain?).");
        Output("LOADSCENE: Static member variables retain their values after executing SceneManager.LoadScene(...)");
    }

    public void OnIncrementRegularPressed()
    {
        value++;
        DisplayValues();
    }
    public void OnIncrementStaticPressed()
    {
        svalue++;
        DisplayValues();
    }

    void DisplayValues()
    {
        GameObject.Find("RegularText").GetComponent<Text>().text =
            "Regular: " + value.ToString();
        GameObject.Find("StaticText").GetComponent<Text>().text =
            "Static: " + svalue.ToString();
        Output("Member variables.  Regular: " + value.ToString() + "  Static: " + svalue.ToString());
    }


    public void OnFullScreenPressed()
    {
        string message = "FULLSCREEN: fullScreen=" + Screen.fullScreen + " fullScreenMode=" + Screen.fullScreenMode;
        Screen.fullScreen = !Screen.fullScreen;
        message += "  ==>  fullScreen=" + Screen.fullScreen + " fullScreenMode=" + Screen.fullScreenMode;
        Output(message);
    }

    public void OnCookiesPressed()
    {
        string cookieValue = HttpCookie.GetCookie("SERVERID");
        Output("Cookies: SERVERID=" + cookieValue);
        cookieValue = HttpCookie.GetCookies();
        Output("Cookies: " + cookieValue);
    }


    public void OnStorePlayerPrefs()
    {
        Output("Storing regular value (" + value + ") to PlayerPrefs key \"WebGLBrowserTests.value\"");
        SetPlayerPrefsValue(value.ToString());
        ShowPersistantValues();
    }
    public void OnStoreLocalStorage()
    {
        Output("Storing regular value (" + value + ") to LocalStorage key \"WebGLBrowserTests.value\"");
        SetLocalStorageValue(value.ToString());
        ShowPersistantValues();
    }

    public void ShowPersistantValues()
    {
        Output("Using Localstorage: " + (Jammer.FileIO.UsingLocalStorage() ? "YES" : "NO") +
            "   PlayerPrefs=" + GetPlayerPrefsValue() +
            "   LocalStorage=" + GetLocalStorageValue());
    }

    string SetLocalStorageValue(string n)
    {
        Jammer.FileIO.SetString(DATA_KEY, value.ToString());
        Jammer.FileIO.Save();
        return n;
    }

    public void OnDeleteLocalStorageValue()
    {
        Jammer.FileIO.DeleteKey(DATA_KEY);
        Jammer.FileIO.Save();
    }

    public void OnDeletePlayerPrefsValue()
    {
        PlayerPrefs.DeleteKey(DATA_KEY);
        PlayerPrefs.Save();
    }

    string SetPlayerPrefsValue(string n)
    {
        PlayerPrefs.SetString(DATA_KEY, n);
        PlayerPrefs.Save();
        return n;
    }


    string GetLocalStorageValue()
    {
        return !Jammer.FileIO.UsingLocalStorage() ?
            "NO_LOCALSTORAGE" :
            Jammer.FileIO.HasKey(DATA_KEY) ?
            Jammer.FileIO.GetString(DATA_KEY):
            "NO_SUCH_KEY";
    }

    string GetPlayerPrefsValue()
    {
        return PlayerPrefs.GetString(DATA_KEY, "NO_SUCH_KEY");
    }
    public void Output(string text)
    {
        outputText.text += text + "\n";
        StartCoroutine(ScrollToBottom());
    }
 
    IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition = 0;
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)scrollRect.transform);
    }

    public void OnGetDocumentTitle()
    {
        Output(Opiframe.GetTitle());     
    }

    public void OnToggleMuteJS()
    {
        Output("This feature does not as intended due to cross-domain messaging restrictions");
        Opiframe.ToggleMute();
    }

    public void UnmuteJS()
    {
        Output("Your audio should be unmuted. Press \"Play Music\" if necessary.");
        Opiframe.Unmute();
    }

    public void OnToggleMusic()
    {
        AudioSource audio = GetComponent<AudioSource>();
        Text buttonLabel = GameObject.Find("ToggleMusicButtonLabel").GetComponent<Text>();

        if (!audio.isPlaying)
            audio.Play();
        else
            audio.Stop();

        buttonLabel.text = audio.isPlaying ? "Stop Music" : "Play Music";

        ReportSoundStatus();
    }

    public void OnToggleMute()
    {

        AudioSource audio = GetComponent<AudioSource>();

        audio.mute = !audio.mute;

        UpdateMuteButtonLabel();
        ReportSoundStatus();
    }

    private void UpdateMuteButtonLabel()
    {
        AudioSource audio = GetComponent<AudioSource>();
        Text buttonLabel = GameObject.Find("ToggleMuteButtonLabel").GetComponent<Text>();

        buttonLabel.text = audio.mute ? "Unmute Audio" : "Mute Audio";
    }

    private void ReportSoundStatus()
    {
        AudioSource audio = GetComponent<AudioSource>();

        Output("Music " + (audio.isPlaying ? "playing" : "stopped") +
               " and " + (audio.mute ? "muted" : "unmuted"));
    }


}


