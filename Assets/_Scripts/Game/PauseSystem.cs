using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    private bool paused = false;
    public void TogglePause()
    {
        paused = !paused;
        Time.timeScale = paused ? 0f : 1f;
    }
}