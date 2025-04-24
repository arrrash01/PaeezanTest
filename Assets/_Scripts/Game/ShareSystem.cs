using UnityEngine;
using UnityEngine.Networking;

public class ShareSystem : MonoBehaviour
{
    public void ShareHighScore()
    {
        int hs = PlayerPrefs.GetInt("HighScore", 0);
        string message = "I scored " + hs + " points in Tricky Ring! Can you beat me?";

        string shareUrl = "mailto:?subject=" + UnityWebRequest.EscapeURL("My High Score") +
                          "&body=" + UnityWebRequest.EscapeURL(message);
        Application.OpenURL(shareUrl);
    }
}
