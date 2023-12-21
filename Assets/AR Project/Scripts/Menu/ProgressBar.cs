using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class ProgressBar : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [Header("Video Player")]
    public VideoPlayer videoPlayer;
    public Camera maincamera;

    [Header("Progress Bar")]
    public RectTransform progressBarRect;
    public Image progressBarSliderImg;

    void Start()
    {
        
    }

    void Update()
    {
        if (videoPlayer == null) return;

        if (videoPlayer.clip != null && videoPlayer.frameCount > 0)
        {
            progressBarSliderImg.fillAmount = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Skip(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Skip(eventData);
    }

    void Skip(PointerEventData eventData)
    {
        Vector2 clickedPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(progressBarRect, eventData.position, maincamera, out clickedPoint))
        {
            float percentage = Mathf.InverseLerp(progressBarRect.rect.xMin, progressBarRect.rect.xMax, clickedPoint.x);
            videoPlayer.frame = (long)(videoPlayer.frameCount * percentage);
        }
    }

}
