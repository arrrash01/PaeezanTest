using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class SplashScreenController : MonoBehaviour
{
    public Slider loadingBar;
    public TextMeshProUGUI percentageText;
    public string sceneToLoad = "MainMenu";

    private void Awake()
    {
        Input.backButtonLeavesApp = false;
    }
    void Start()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneToLoad);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            loadingBar.value = progress;
            percentageText.text = (progress*100).ToString()+"%";

            if (op.progress >= 0.9f)
            {
                loadingBar.value = 1f;
                yield return new WaitForSeconds(0.5f);
                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
