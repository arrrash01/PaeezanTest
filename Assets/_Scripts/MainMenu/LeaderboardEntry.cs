using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image backgroundImage;

    public void Initialize(int rank, string playerName, int score, Color highlightColor, bool isCurrent)
    {
        rankText.text = rank.ToString();
        nameText.text = playerName;
        scoreText.text = score.ToString();
        backgroundImage.color = isCurrent ? highlightColor : Color.clear;
    }
}
