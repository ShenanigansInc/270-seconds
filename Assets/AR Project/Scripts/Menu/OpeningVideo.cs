using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class OpeningVideo : MonoBehaviour
{
    [Header("Canvas Group")]
    public CanvasGroup fadeOutObj;
    public FadeOutCanvas fadeOutObjScript;
    public CanvasGroup fadeForText;
    public CanvasGroup fadeInObj;

    [Header("Video Player")]
    public VideoPlayer videoPlayer;
    
    [SerializeField]
    private float timeToOpen = 0;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(timeToOpen);

        Application.targetFrameRate = Screen.currentResolution.refreshRate;

        AudioPlayer.Instance.PlayMusic("bgm_menu");

        fadeOutObj.enabled = true;
        fadeOutObjScript.enabled = true;

        yield return new WaitUntil(() => fadeOutObj.alpha == 0);

        fadeOutObj.gameObject.SetActive(false);

        videoPlayer.Play();

        yield return new WaitForSeconds((float)videoPlayer.length);

        fadeInObj.gameObject.SetActive(true);

        yield return new WaitUntil(() => fadeInObj.alpha == 1);

        LoadScenesManager.instance.LoadScene("menu");
    }
}