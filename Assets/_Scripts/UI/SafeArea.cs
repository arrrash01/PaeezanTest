using UnityEngine;
using UnityEngine.UI;
public class SafeArea : MonoBehaviour
{
    void Start()
    {
        var panel = GetComponent<RectTransform>();
        var safe = Screen.safeArea;
        Vector2 anchorMin = safe.position;
        Vector2 anchorMax = safe.position + safe.size;
        anchorMin.x /= Screen.width; anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width; anchorMax.y /= Screen.height;
        panel.anchorMin = anchorMin;
        panel.anchorMax = anchorMax;
    }
}