using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class IntroManager : MonoBehaviour
{
    [Header("Canvas Group")]
    public CanvasGroup fadeOutObj;
    public FadeOutCanvas fadeOutObjScript;
    public CanvasGroup fadeForText;
    public CanvasGroup fadeInObj;

    [Header("Text to Show")]
    public GameObject textContainer;
    public CanvasGroup text1CanvasGroup;
    public CanvasGroup text2CanvasGroup;
    public CanvasGroup text3CanvasGroup;

    [Header("Timeline")]
    public PlayableDirector introAnim;
    public float timeToOpen = 0;
    public float extraTime = 0;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(timeToOpen);

        Application.targetFrameRate = Screen.currentResolution.refreshRate;

        Debug.Log(string.Format("refresh: {0}, frame rate: {1}", Screen.currentResolution.refreshRate, Application.targetFrameRate));

        AudioPlayer.Instance.PlayMusic("narration_intro");

        fadeOutObj.enabled = true;
        fadeOutObjScript.enabled = true;

        yield return new WaitUntil(() => fadeOutObj.alpha == 0);

        fadeOutObj.gameObject.SetActive(false);

        introAnim.Play();

        yield return new WaitForSeconds((float)introAnim.duration + extraTime);

        textContainer.SetActive(true);
        text1CanvasGroup.gameObject.SetActive(true);

        yield return new WaitUntil(() => text1CanvasGroup.alpha == 1);

        text2CanvasGroup.gameObject.SetActive(true);

        yield return new WaitUntil(() => text2CanvasGroup.alpha == 1);

        text3CanvasGroup.gameObject.SetActive(true);

        yield return new WaitUntil(() => text3CanvasGroup.alpha == 1);
        yield return new WaitForSeconds(extraTime);

        fadeInObj.gameObject.SetActive(true);

        yield return new WaitUntil(() => fadeInObj.alpha == 1);

        LoadScenesManager.instance.LoadScene("menu");

    }


}
