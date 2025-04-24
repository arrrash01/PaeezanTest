using UnityEngine;
using System.Collections.Generic;

public class LeaderboardDataProvider : MonoBehaviour
{ 
    public class Entry
    {
        public int rank;
        public string name;
        public int score;
    }
    public string playerName;
    public Color playerColor;
    private List<Entry> dailyEntries;
    private List<Entry> weeklyEntries;
    private List<Entry> allTimeEntries;

    void Awake()
    {
        dailyEntries = new List<Entry>(5001);
        weeklyEntries = new List<Entry>(5001);
        allTimeEntries = new List<Entry>(5001);
        for (int i = 1; i <= 5000; i++)
        {
            dailyEntries.Add(new Entry
            {
                rank = i,
                name = "Player" + i,
                score = Random.Range(0, 100)
            });
            weeklyEntries.Add(new Entry
            {
                rank = i,
                name = "Player" + i,
                score = Random.Range(0, 100)
            });
            allTimeEntries.Add(new Entry
            {
                rank = i,
                name = "Player" + i,
                score = Random.Range(0, 100)
            });
        }
        dailyEntries.Add(new Entry
        {
            rank = dailyEntries.Count + 1,
            name = playerName,
            score = PlayerPrefs.GetInt("HighScore", 0)
        });
        weeklyEntries.Add(new Entry
        {
            rank = dailyEntries.Count + 1,
            name = playerName,
            score = PlayerPrefs.GetInt("HighScore", 0)
        });
        allTimeEntries.Add(new Entry
        {
            rank = dailyEntries.Count + 1,
            name = playerName,
            score = PlayerPrefs.GetInt("HighScore", 0)
        });

        dailyEntries.Sort((a, b) => b.score.CompareTo(a.score));
        weeklyEntries.Sort((a, b) => b.score.CompareTo(a.score));
        allTimeEntries.Sort((a, b) => b.score.CompareTo(a.score));

        for (int i = 0; i < dailyEntries.Count; i++)
        {
            dailyEntries[i] = new Entry
            {
                rank = i + 1,
                name = dailyEntries[i].name,
                score = dailyEntries[i].score
            };
            weeklyEntries[i] = new Entry
            {
                rank = i + 1,
                name = weeklyEntries[i].name,
                score = weeklyEntries[i].score
            };
            allTimeEntries[i] = new Entry
            {
                rank = i + 1,
                name = allTimeEntries[i].name,
                score = allTimeEntries[i].score
            };

        }        
    }

    public int TotalCount => dailyEntries.Count;
    public Entry GetDailyEntry(int index) => dailyEntries[index];
    public Entry GetWeeklyEntry(int index) => weeklyEntries[index];
    public Entry GetAllTimeEntry(int index) => allTimeEntries[index];
}
