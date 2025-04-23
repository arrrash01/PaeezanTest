using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(ScrollRect))]
public class VirtualizedScrollList : MonoBehaviour
{
    [SerializeField] private RectTransform viewport;
    [SerializeField] private RectTransform content;
    [SerializeField] private LeaderboardEntry itemPrefab;
    [SerializeField] private LeaderboardDataProvider dataProvider;
    [SerializeField] private int buffer = 5;

    private ScrollRect scrollRect;
    private int poolSize;
    private int visibleCount;
    private List<LeaderboardEntry> pool;
    private float entryHeight;
    void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.onValueChanged.AddListener(_ => UpdatePoolItems());
    }

    void Start()
    {
        Canvas.ForceUpdateCanvases();
        entryHeight = itemPrefab.GetComponent<RectTransform>().rect.height;
        float contentH = dataProvider.TotalCount * entryHeight;
        content.sizeDelta = new Vector2(content.sizeDelta.x, contentH);

        visibleCount = Mathf.CeilToInt(viewport.rect.height / entryHeight);
        poolSize = visibleCount + buffer * 2;

        pool = new List<LeaderboardEntry>(poolSize);
        for (int i = 0; i < poolSize; i++)
            pool.Add(Instantiate(itemPrefab, content));

        UpdatePoolItems();
    }

    void UpdatePoolItems()
    {
        float scrollY = content.anchoredPosition.y;

        int firstVisibleIndex = Mathf.FloorToInt(scrollY / entryHeight);
        firstVisibleIndex = Mathf.Clamp(
            firstVisibleIndex,
            0,
            dataProvider.TotalCount - visibleCount
        );

        int poolStartIndex = firstVisibleIndex - buffer;
        poolStartIndex = Mathf.Clamp(
            poolStartIndex,
            0,
            dataProvider.TotalCount - poolSize
        );

        for (int i = 0; i < poolSize; i++)
        {
            int dataIndex = poolStartIndex + i;
            if (dataIndex < 0 || dataIndex >= dataProvider.TotalCount)
            {
                pool[i].gameObject.SetActive(false);
                continue;
            }

            pool[i].gameObject.SetActive(true);
            float y = -dataIndex * entryHeight;
            pool[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, y);

            var e = dataProvider.GetEntry(dataIndex);
            bool isMe = e.name == dataProvider.playerName;
            pool[i].Initialize(e.rank, e.name, e.score, dataProvider.playerColor, isMe);
        }
    }
}
