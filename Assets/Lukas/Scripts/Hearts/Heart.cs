using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

[System.Serializable]
public class Heart : MonoBehaviour
{
    [SerializeField] private Image shadowImage, toneImage;
    private float startAlpha, targetAlpha;
    private Color currentColor;
    private float shadowMaxHeight;
    private RectTransform shadowRect;
    private Vector2 shadowSize;
    void Start()
    {
        currentColor = toneImage.color;
        startAlpha = toneImage.color.a;
        targetAlpha = .5f;
        shadowRect = shadowImage.GetComponent<RectTransform>();
        shadowMaxHeight = shadowRect.rect.height;
        shadowSize = shadowRect.sizeDelta;
    }

    public void SetHeartFraction(float fraction)
    {
        fraction = Mathf.Clamp01(fraction);
        currentColor.a = fraction == 1 ? 0 : startAlpha + fraction * (targetAlpha - startAlpha);
        shadowSize.y = shadowMaxHeight * (1f - fraction);

        toneImage.color = currentColor;
        shadowRect.sizeDelta = shadowSize;
    }
}
