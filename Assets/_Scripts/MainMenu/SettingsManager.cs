using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [System.Serializable]
    public class SettingsData
    {
        public bool sfxOn;
        public bool musicOn;
        public bool vibrationOn;
    }

    [System.Serializable]
    public class PlayerProfile
    {
        public string playerName;
        public int lastScore;
        public int highScore;
        public int currentRank;
        public SettingsData settings;
    }

    public AudioMixer mixer;
    public Toggle toggleSFX, toggleMusic, toggleVibration;
    [SerializeField] private Button exportButton;

    void Awake()
    {
        toggleSFX.onValueChanged.AddListener(SetSFX);
        toggleMusic.onValueChanged.AddListener(SetMusic);
        toggleVibration.onValueChanged.AddListener(SetVibration);
        exportButton.onClick.AddListener(ExportProfile);
    }
    private void Start()
    {
        bool sfxOn = PlayerPrefs.GetInt("SFX_On", 1) == 1;
        bool musOn = PlayerPrefs.GetInt("Music_On", 1) == 1;
        bool vibOn = PlayerPrefs.GetInt("Vibration_On", 1) == 1;

        SetSFX(sfxOn);
        SetMusic(musOn);
        toggleSFX.isOn = sfxOn;
        toggleMusic.isOn = musOn;
        toggleVibration.isOn = vibOn;

    }

    public void ExportProfile()
    {
        PlayerProfile profile = new PlayerProfile
        {
            playerName = PlayerPrefs.GetString("PlayerName", "You"),
            lastScore = PlayerPrefs.GetInt("LastScore", 0),
            highScore = PlayerPrefs.GetInt("HighScore", 0),
            currentRank = PlayerPrefs.GetInt("PlayerRank", 9999),
            settings = new SettingsData
            {
                sfxOn = PlayerPrefs.GetInt("SFX_On", 1) == 1,
                musicOn = PlayerPrefs.GetInt("Music_On", 1) == 1,
                vibrationOn = PlayerPrefs.GetInt("Vibration_On", 1) == 1
            }
        };

        string json = JsonUtility.ToJson(profile, true);

        string path = Path.Combine(Application.persistentDataPath, "PlayerProfile.json");
        File.WriteAllText(path, json);
        Debug.Log("Profile exported to: " + path);
        ShareFile(path);
    }
    private void ShareFile(string path)
    {
        if (Application.platform != RuntimePlatform.Android) return;

        // Android ACTION_SEND intent
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent");
        intent.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

        // Create a java.io.File for our JSON and get a Uri
        AndroidJavaObject fileObj = new AndroidJavaObject("java.io.File", path);
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uri = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObj);

        // Attach the file and set MIME type
        intent.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uri);
        intent.Call<AndroidJavaObject>("setType", "application/json");

        // Wrap in chooser
        AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>(
            "createChooser", intent, "Share Player Profile");

        // Launch from Unity’s current Activity
        AndroidJavaObject unityActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
            .GetStatic<AndroidJavaObject>("currentActivity");
        unityActivity.Call("startActivity", chooser);
    }

    public void SetSFX(bool on)
    {
        mixer.SetFloat("SFXVolume", on ? 0f : -80f);
        PlayerPrefs.SetInt("SFX_On", on ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetMusic(bool on)
    {
        mixer.SetFloat("MusicVolume", on ? 0f : -80f);
        PlayerPrefs.SetInt("Music_On", on ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetVibration(bool on)
    {
        PlayerPrefs.SetInt("Vibration_On", on ? 1 : 0);
        PlayerPrefs.Save();
    }
}
