# Unity WebGL Browser Tests

Various tests of Unity WebGL Build features:

- Audio/Muting - Unmutes connect.unity.com - js plugin
- Full Screen - 1 click js plugin
- Redirect to new window - 1 click js plugin
- Local Storage vs. Player Profiles

## Online at
- https://connect.unity.com/mg/other/webgl-tests-v0-1j-1 (version 0.1j)
- https://www.jimu.net/WebGLBrowserTests/ (version 0.1a)
- https://simmer.io/@jimu/webgl-browser-tests-v0-1c (version 0.1c)

## Usage
- Press Increment Regular and Increment Static to change class member variable values.  then press Load Scene 1 to demonstrate that Static variables persist even after a scene is reloaded
- Press [Cookies] to use javascript to return HTML cookies. connect.unity.com isolates your app in an iframe so there may be no cookies.  simmer.io does return some cookies.
- Press [Full Screen] to demonstrate that the unity fullscreen support requires two clicks
- Press [Full Screen JS] to demonstrate that fullscreen can be used with a single click by utilizing javascript's onpress
- Press [Link New Tab JS] to open a new browser tab with one click (using javascript)
- Press [Store PlayerPrefs] to store the reglar variable to PlayerPrefs
- Press [Store LocalStorage] to store the regular variable to LocalStorage (javascript)
- Press [Delete PlayerPrefs] to clear PlayerPrefs
- Press [Delete PlayerPrefs] to clear LocalStorage (javascript)
- Press [Show Persisted Values] to display PlayerPrefs and LocalStorage values
- Press [Get Document Title] to fetch the browser window/iframe document title (javascript)
- Don't bother with [Toggle Mute JS] - It doesn't work on connect.unity.com due to cross-origin restrictions (COR)
- Press [Play Music] to start playing music
- Press [Mute/Unmute Audio] to use mute music using unitys built-in audio support. Demonstrates that this does NOT work on connect.unity.com
- Press [Unmute JS] to unmute audio on connect.unity.com without needing to press the unmute icon in the lower right corner (javascript)
