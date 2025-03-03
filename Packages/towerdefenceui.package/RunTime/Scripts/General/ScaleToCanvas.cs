using UnityEngine;
using UnityEngine.UI;
public class ScaleToCanvas : MonoBehaviour
{
    public static void ScaleToFitCanvas(RectTransform objectToTransform)
    {
        Canvas canvas = FindFirstObjectByType<Canvas>();

        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        objectToTransform.sizeDelta = new Vector2(canvasRect.rect.width, canvasRect.rect.height);
    }

    public static void ScaleHeightCanvas(RectTransform objectTransform)
    {
        Canvas canvas = FindFirstObjectByType<Canvas>();
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        float canvasHeight = canvasRect.rect.height;

        float localHeight = canvasHeight / canvasRect.localScale.y;

        objectTransform.sizeDelta = new Vector2(objectTransform.sizeDelta.x, localHeight);
    }
}

