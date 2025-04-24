using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public float innerRadius = 2f;
    public float outerRadius = 3f;
    public Transform centerPoint;
    public GameObject collectiblePrefab;
    public ObstacleController obstaclePrefab;
    public Transform obstacleParent;
    public int obstaclesPerWave = 2;
    private int score = 0;
    private List<ObstacleController> obstacles = new List<ObstacleController>();
    public PlayerController playerController;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI highScoreText;
    private int highscore=0;
    public GameObject endGameUI;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }
    private void Start()
    {
        SpawnCollectible();
        SpawnObstacles(obstaclesPerWave);
        highscore = PlayerPrefs.GetInt("HighScore", 0);
        endGameUI.SetActive(false);
    }
    public void OnPointCollected()
    {
        score++;
        updateScore();
        playerController.angularSpeed += 5f;
        SpawnCollectible();
        foreach (var obs in obstacles)
            obs.RandomFlip();
        if (score % 10 == 0)
            SpawnObstacles(1);
    }

    private void updateScore()
    {
        scoreText.text = score.ToString();
    }

    public void SpawnCollectible()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float radius = (Random.value < .5f ? innerRadius : outerRadius);
        Vector2 pos = new Vector2(
            centerPoint.position.x + Mathf.Cos(angle) * radius,
            centerPoint.position.y + Mathf.Sin(angle) * radius
        );
        Instantiate(collectiblePrefab, pos, Quaternion.identity);
    }
    public void SpawnObstacles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float angleRad = Random.Range(0f, Mathf.PI * 2f); 
            
            //float radius = (Random.value < .5f ? innerRadius : outerRadius);
            Vector2 pos = (Vector2) centerPoint.position + new Vector2(
                Mathf.Cos(angleRad) * (outerRadius),
                Mathf.Sin(angleRad) * (outerRadius)
            );
            Vector2 radialDir = (pos - (Vector2)centerPoint.position).normalized;
            var obs = Instantiate(obstaclePrefab, pos, Quaternion.identity,obstacleParent);
            obs.transform.up = radialDir;
            obs.pointingOutward = Random.value < .5f;
            obs.obstacleTransform.localPosition = new Vector3(0f, (obs.pointingOutward ? 0f : -0.35f));
            obstacles.Add(obs);
        }
    }
    public void EndGame()
    {
        Time.timeScale = 0f;

        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("HighScore", highscore);
        }
        endScoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highscore;
        PlayerPrefs.SetInt("LastScore", score);
        PlayerPrefs.Save();
        endGameUI.SetActive(true);

    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void PunchButton(Button button)
    {
        button.transform.DOPunchScale(button.transform.localScale * 1.05f, 0.3f);
    }
}
