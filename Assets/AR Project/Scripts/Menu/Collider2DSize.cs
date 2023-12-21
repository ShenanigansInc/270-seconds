using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Collider2DSize : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;
    public RectTransform rectTransform;
    public TextMeshProUGUI buttonText;
    public Image image;
    public bool isVertical = false;
    public int factor = 1;

    void Start()
    {
        Rect rect = rectTransform.rect;
        boxCollider2D.size = new Vector2(rect.width, rect.height);
        float x = (isVertical) ? rect.width / 2 : 0;
        float y = (isVertical) ? 0 : rect.height / 2;

        boxCollider2D.offset = new Vector2(x, y) * factor;
    }
}
