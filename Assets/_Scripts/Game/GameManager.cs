using UnityEngine;
using System.Collections.Generic;
using TMPro;
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
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float radius = (Random.value < .5f ? innerRadius : outerRadius);
            Vector2 pos = new Vector2(
                centerPoint.position.x + Mathf.Cos(angle) * radius,
                centerPoint.position.y + Mathf.Sin(angle) * radius
            );
            var o = Instantiate(obstaclePrefab, pos, Quaternion.identity, obstacleParent);
            obstacles.Add(o);
        }
    }
}
