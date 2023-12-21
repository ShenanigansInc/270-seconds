using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ShortsManager : MonoBehaviour
{

    [Header("Canvas Group")]
    public CanvasGroup fadeOutObj;
    public FadeOutCanvas fadeOutObjScript;
    public CanvasGroup fadeInObj;

    [Header("Video Player")]
    public VideoPlayer videoPlayer;
    public Transform videoImageTransform; 
    public List<VideoClip> videosToShowPlayer = new List<VideoClip>();
    public Button playBtn;
    public TextMeshProUGUI playBtnText;

    [Header("Video Player Bar")]
    public bool isVideoPlayerBarVisible = true;
    public GameObject videoPlayerBar;
    public float timeToFadeOut = 4.0f;
    private float fadeOutCountdown;

    [Header("Playlist")]
    public bool isPlaylistOpen = true;
    public GameObject playlistObj;
    public GameObject playlistBtnObj;
    public TextMeshProUGUI playlistBtnText;


    [Header("Times")]
    public float timeToOpen = 0;
    public int videoIndex = 0;

    private string playTextCode = "\uf04b";
    private string pauseTextCode = "\uf04c";
    private string menuNormalTextCode = "\uf0c9";
    private string menuOpenedTextCode = "\uf00d";

    IEnumerator Start()
    {
        SetPlaylist();

        StartVideoPlayer();

        yield return new WaitForSeconds(timeToOpen);

        fadeOutObj.enabled = true;
        fadeOutObjScript.enabled = true;

        yield return new WaitUntil(() => fadeOutObj.alpha == 0);

        fadeOutObj.gameObject.SetActive(false);
    }

    void PlayVideo()
    {
        AdjustVideoPlayerSize();

        videoPlayer.Play();

        playBtnText.text = pauseTextCode;

        playBtn.onClick.RemoveAllListeners();
        playBtn.onClick.AddListener( delegate { PauseVideo(); } );
    }

    void AdjustVideoPlayerSize()
    {
        Debug.Log(videosToShowPlayer[videoIndex].name);

        if (isPlaylistOpen) return;

        if(videosToShowPlayer[videoIndex].name.EndsWith("_LMode"))
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            videoImageTransform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
            videoImageTransform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
    }

    void PauseVideo()
    {
        videoPlayer.Pause();

        playBtnText.text = playTextCode;

        playBtn.onClick.RemoveAllListeners();
        playBtn.onClick.AddListener(delegate { PlayVideo(); });
    }

    public void OnClickNext()
    {
        videoIndex = ((videoIndex + 1) >= videosToShowPlayer.Count) ? 0 : videoIndex + 1;

        OnVideoChange();
    }

    public void OnClickBack()
    {
        videoIndex = ((videoIndex - 1) < 0) ? videosToShowPlayer.Count - 1 : videoIndex - 1;

        OnVideoChange();
    }

    void OnVideoChange()
    {
        AdjustVideoPlayerSize();

        videoPlayer.Stop();
        videoPlayer.time = 0;

        ClearOutRenderTexture(videoPlayer.targetTexture);

        videoPlayer.clip = videosToShowPlayer[videoIndex];
        videoPlayer.Play();

        playBtnText.text = pauseTextCode;

        playBtn.onClick.RemoveAllListeners();
        playBtn.onClick.AddListener(delegate { PauseVideo(); });
    }

    void StartVideoPlayer()
    {
        ClearOutRenderTexture(videoPlayer.targetTexture);

        videoPlayer.clip = videosToShowPlayer[videoIndex];
        videoPlayer.time = 0;
        videoPlayer.enabled = true;

        playBtnText.text = playTextCode;

        playBtn.onClick.RemoveAllListeners();
        playBtn.onClick.AddListener(delegate { PlayVideo(); });
    }

    public void ResetVideoPlayer()
    {
        videoPlayer.Stop();
        videoPlayer.time = 0;

        ClearOutRenderTexture(videoPlayer.targetTexture);

        playBtnText.text = playTextCode;

        playBtn.onClick.RemoveAllListeners();
        playBtn.onClick.AddListener(delegate { PlayVideo(); });
    }

    void ClearOutRenderTexture(RenderTexture renderTexture)
    {
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rt;
    }

    public void VideoPlayerBar()
    {
        isVideoPlayerBarVisible = !isVideoPlayerBarVisible;

        videoPlayerBar.SetActive(isVideoPlayerBarVisible);
    }

    public void ResetTimeToFadeOut()
    {
        fadeOutCountdown = timeToFadeOut;
    }

    private void Update()
    {
        Debug.Log(fadeOutCountdown);

        if (!videoPlayer.isPlaying)
            return;

        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Debug.Log("touched");
            ResetTimeToFadeOut();
            if (!videoPlayerBar.activeSelf)
                VideoPlayerBar();
        }

        if (fadeOutCountdown <= 0)
            return;

        fadeOutCountdown -= Time.deltaTime;
        if (fadeOutCountdown <= 0)
            VideoPlayerBar();
    }

    public void GoToMenu()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        fadeInObj.gameObject.SetActive(true);

        yield return new WaitUntil(() => fadeInObj.alpha == 1);

        LoadScenesManager.instance.LoadScene("menu");
    }

    void SetPlaylist()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        playlistBtnText.text = menuNormalTextCode;

        playlistObj.SetActive(true);

        playlistBtnObj.SetActive(false);
    }

    public void OnPlaylistClick()
    {
        if(!playlistBtnObj.activeSelf) playlistBtnObj.SetActive(true);

        isPlaylistOpen = !isPlaylistOpen;

        playlistBtnText.text = (isPlaylistOpen) ? menuOpenedTextCode : menuNormalTextCode;

        playlistObj.SetActive(isPlaylistOpen);

        if(isPlaylistOpen) Screen.orientation = ScreenOrientation.Portrait;
    }

    public void OnClipClick(int index)
    {
        videoIndex = index;

        if (!playlistBtnObj.activeSelf) playlistBtnObj.SetActive(true);

        isPlaylistOpen = false;

        playlistObj.SetActive(isPlaylistOpen);

        playlistBtnText.text = menuNormalTextCode;

        OnVideoChange();
    }

}
