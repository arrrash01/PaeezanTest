using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    private bool paused = false;
    public GameObject pausePanel;
    public void TogglePause()
    {
        paused = !paused;
        pausePanel.SetActive(paused);
        Time.timeScale = paused ? 0f : 1f;
    }
}