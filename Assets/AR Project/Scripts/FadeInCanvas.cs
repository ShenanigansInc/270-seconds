using UnityEngine;

public class FadeInCanvas : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float speed;

    private void OnEnable()
    {
        canvasGroup.alpha = 0;
    }

    private void OnDisable()
    {
        canvasGroup.alpha = 0;
    }

    private void Update()
    {
        if (canvasGroup.alpha == 1) return;

        canvasGroup.alpha += speed * Time.deltaTime;
    }
}
