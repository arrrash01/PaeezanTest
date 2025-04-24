using UnityEngine;

public class ShareSystem : MonoBehaviour
{
    public void ShareHighScore()
    {
        int hs = PlayerPrefs.GetInt("HighScore", 0);
        string message = "I scored " + hs + " points in Tricky Ring! Can you beat me?";

        string shareUrl = "mailto:?subject=" + WWW.EscapeURL("My High Score") +
                          "&body=" + WWW.EscapeURL(message);
        Application.OpenURL(shareUrl);
    }
}
