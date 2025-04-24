using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button shopTab, playTab, leaderboardTab;
    public GameObject shopPanel, playPanel, leaderboardPanel;


    void Start()
    {
        shopTab.onClick.AddListener(() => 
        { 
            ShowPanel(shopPanel);
            PunchButton(shopTab);
        });
        playTab.onClick.AddListener(() =>
        {
            ShowPanel(playPanel);
           PunchButton(playTab);
        });
        leaderboardTab.onClick.AddListener(() => 
        { 
            ShowPanel(leaderboardPanel);
            PunchButton(leaderboardTab);
            
        });
        ShowPanel(playPanel);
    }

    void ShowPanel(GameObject panel)
    {
        shopPanel.SetActive(panel == shopPanel);
        playPanel.SetActive(panel == playPanel);
        leaderboardPanel.SetActive(panel == leaderboardPanel);
    }
    public void PunchButton(Button button)
    {
        DOTween.Kill(button);
        button.transform.localScale = Vector3.one;
        button.transform.DOPunchScale(button.transform.localScale * 1.05f, 0.3f);
    }
}