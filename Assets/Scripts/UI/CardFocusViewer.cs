using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(GraphicRaycaster))]
public class CardFocusViewer : MonoBehaviour
{
    private Vector3 originalScale;
    private bool zoomed = false;
    private Canvas cardCanvas;

    void Start()
    {
        originalScale = transform.localScale;
        cardCanvas = GetComponent<Canvas>();

        Button btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(Toggle);
    }

    void Toggle()
    {
        zoomed = !zoomed;

        if (zoomed)
        {
            // Set layer to front
            cardCanvas.overrideSorting = true;
            cardCanvas.sortingOrder = 100;

            transform.DOScale(originalScale * 1.6f, 0.3f).SetEase(Ease.OutBack);
        }
        else
        {
            transform.DOScale(originalScale, 0.2f).SetEase(Ease.InQuad).OnComplete(() =>
            {
                // Reset layer only after shrinking
                cardCanvas.overrideSorting = false;
                cardCanvas.sortingOrder = 0;
            });
        }
    }
}