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
    private List<Entry> entries;

    void Awake()
    {
        entries = new List<Entry>(5001);
        for (int i = 1; i <= 5000; i++)
        {
            entries.Add(new Entry
            {
                rank = i,
                name = "Player" + i,
                score = Random.Range(0, 100)
            });
        }

        entries.Sort((a, b) => b.score.CompareTo(a.score));

        for (int i = 0; i < entries.Count; i++)
        {
            entries[i] = new Entry
            {
                rank = i + 1,
                name = entries[i].name,
                score = entries[i].score
            };
        }

        entries.Add(new Entry
        {
            rank = entries.Count + 1,
            name = playerName,
            score = PlayerPrefs.GetInt("HighScore", 0)
        });
    }

    public int TotalCount => entries.Count;
    public Entry GetEntry(int index) => entries[index];
}
