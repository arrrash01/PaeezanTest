using UnityEngine;
using UnityEngine.UI;

public class ExitManager : MonoBehaviour
{
    public GameObject exitPopup;
    public Button yesButton;
    public Button noButton;

    void Awake()
    {
        exitPopup.SetActive(false);
        yesButton.onClick.AddListener(ConfirmExit);
        noButton.onClick.AddListener(CancelExit);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PlayerPrefs.GetInt("Vibration_On", 1) == 1)
                Handheld.Vibrate();
            ShowExitPopup();
        }
    }

    public void ShowExitPopup()
    {
        exitPopup.SetActive(true);
    }

    public void CancelExit()
    {
        exitPopup.SetActive(false);
    }

    public void ConfirmExit()
    {
        Application.Quit();
        System.Diagnostics.Process.GetCurrentProcess().Kill();
    }
}
