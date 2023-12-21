using UnityEngine;

public class FadeOutCanvas : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float speed;
    public bool disable = false;

    private void Start()
    {
        canvasGroup.alpha = 1;
    }

    private void Update()
    {
        if (canvasGroup.alpha <= 0)
        {
            if (disable)
            {
                gameObject.SetActive(false);
            }

            return;
        }
        canvasGroup.alpha -= speed * Time.deltaTime;
    }
}
