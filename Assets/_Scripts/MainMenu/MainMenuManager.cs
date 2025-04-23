using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button shopTab, playTab, leaderboardTab;
    public GameObject shopPanel, playPanel, leaderboardPanel;

    void Start()
    {
        shopTab.onClick.AddListener(() => ShowPanel(shopPanel));
        playTab.onClick.AddListener(() => ShowPanel(playPanel));
        leaderboardTab.onClick.AddListener(() => ShowPanel(leaderboardPanel));
        ShowPanel(playPanel);
    }

    void ShowPanel(GameObject panel)
    {
        shopPanel.SetActive(panel == shopPanel);
        playPanel.SetActive(panel == playPanel);
        leaderboardPanel.SetActive(panel == leaderboardPanel);
    }
}