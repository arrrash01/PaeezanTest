using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayPanelController : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public Button playButton;

    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
        playButton.onClick.AddListener(() => {
            SceneManager.LoadScene("GameScene");
        });
    }
}
