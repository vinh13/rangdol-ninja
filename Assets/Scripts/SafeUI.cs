using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeUI : MonoBehaviour
{
    RectTransform rect;
    Rect safeArea;
    Vector2 minArchor;
    Vector2 maxArchor;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        safeArea = Screen.safeArea;
        minArchor = safeArea.position;
        maxArchor = minArchor + safeArea.size;

        minArchor.x /= Screen.width;
        minArchor.y /= Screen.height;
        maxArchor.x /= Screen.width;
        maxArchor.y /= Screen.height;
        rect.anchorMin = minArchor;
        rect.anchorMax = maxArchor;
    }
}
