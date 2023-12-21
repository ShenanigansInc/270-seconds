using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class ARMenuManager : MonoBehaviour
{
    public static ARMenuManager instance;

    [Header("Camara")]
    public Camera mainCamera;
    public bool canDrag = false;

    [Header("Button Info")]
    public RectTransform buttonTransform;

    [Header("Borders")]
    public bool topBorder = false;
    public bool bottomBorder = false;

    [Header("Screen Info")]
    public Vector2 screenPosition = Vector2.zero;
    public Vector3 worldPosition = Vector3.zero;
    public Vector3 startPos;

    [Header("Canvas Group")]
    public CanvasGroup fadeOutObj;
    public FadeOutCanvas fadeOutObjScript;
    public CanvasGroup fadeInObj;

    [Header("Timeline")]
    public PlayableDirector buttonIdleAnim;
    public float timeToOpen = 0;
    public float extraTime = 0;

    [SerializeField]
    private string audioToPlay;

    void Awake()
    {
        instance = this;
    }

    IEnumerator Start()
    {
        startPos = buttonTransform.anchoredPosition;

        yield return new WaitForSeconds(timeToOpen);

        fadeOutObj.enabled = true;
        fadeOutObjScript.enabled = true;

        yield return new WaitUntil(() => fadeOutObj.alpha == 0);

        fadeOutObj.gameObject.SetActive(false);
        buttonIdleAnim.gameObject.SetActive(true);

        if(AudioPlayer.Instance.MusicPlayer == null || !AudioPlayer.Instance.MusicPlayer.clip.name.Equals(audioToPlay))
            AudioPlayer.Instance.PlayMusic(audioToPlay);

        if(AudioPlayer.Instance.CurrentMusicTime != 0)
            AudioPlayer.Instance.MusicPlayer.time = AudioPlayer.Instance.CurrentMusicTime;
    }

    void Update()
    {
        ButtonMoveUpdate();
    }

    void ButtonMoveUpdate()
    {
        if (canDrag)
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            GetMousePosition();
#elif UNITY_ANDROID
            GetTouchPosition();
#endif
        }
    }

    void GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        screenPosition = new Vector2(mousePosition.x, mousePosition.y);

        worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;
    }

    void GetTouchPosition()
    {
        screenPosition = Input.GetTouch(0).position;

        worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;
    }

    public void OnButtonDrag()
    {
        buttonTransform.position = new Vector2(worldPosition.x, worldPosition.y);
        CheckAnimationObject();
    }

    public void OnButtonEndDrag()
    {
        canDrag = false;

        screenPosition = Vector2.zero;
        worldPosition = Vector3.zero;

        if (!topBorder && !bottomBorder)
        {
            buttonTransform.anchoredPosition = startPos;
        }
        else
        {
            if (topBorder)
            {
                AudioPlayer.Instance.CurrentMusicTime = AudioPlayer.Instance.MusicPlayer.time;
                StartCoroutine(LoadScene("ar"));
            }
            else if (bottomBorder)
            {
                AudioPlayer.Instance.CurrentMusicTime = AudioPlayer.Instance.MusicPlayer.time;
                AudioPlayer.Instance.StopMusic();
                StartCoroutine(LoadScene("shorts"));

            }
        }

        buttonIdleAnim.gameObject.SetActive(true);
    }

    public void OnButtonClick()
    {
        canDrag = true;
        CheckAnimationObject();
    }

    void CheckAnimationObject()
    {
        if (buttonIdleAnim.gameObject.activeSelf)
        {
            buttonIdleAnim.Stop();
            buttonIdleAnim.time = 0;
            buttonIdleAnim.Evaluate();

            buttonIdleAnim.gameObject.SetActive(false);
        }
    }

    IEnumerator LoadScene(string scene)
    {
        fadeInObj.gameObject.SetActive(true);

        yield return new WaitUntil(() => fadeInObj.alpha == 1);

        LoadScenesManager.instance.LoadScene(scene);
    }
}
