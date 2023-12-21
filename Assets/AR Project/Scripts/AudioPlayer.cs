using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private static AudioPlayer instance;
    private AudioSource musicPlayer = null;
    private AudioSource sfxPlayer = null;
    private float currentMusicTime;
    private IEnumerator fadeOutSoundCoroutine = null;

    public static AudioPlayer Instance { get => instance; }
    public AudioSource MusicPlayer { get => musicPlayer; }
    public float CurrentMusicTime { get => currentMusicTime; set => currentMusicTime = value; }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(this);
    }

    public void PlaySFX(string sfxClipName)
    {
        sfxPlayer = new GameObject("SFX").AddComponent<AudioSource>();
        sfxPlayer.transform.SetParent(this.transform);

        sfxPlayer.clip = Resources.Load<AudioClip>("Audio/SFX/" + sfxClipName);

        sfxPlayer.Play();

        Destroy(sfxPlayer.gameObject, sfxPlayer.clip.length);
    }

    public void PlayMusic(string musicTrackName)
    {
        if(musicPlayer == null)
        {
            musicPlayer = new GameObject("Music Track").AddComponent<AudioSource>();
            musicPlayer.transform.SetParent(this.transform);
        }
        else if (fadeOutSoundCoroutine != null)
        {
            StopCoroutine(fadeOutSoundCoroutine);
            fadeOutSoundCoroutine = null;
        }

        musicPlayer.clip = Resources.Load<AudioClip>("Audio/Music/" + musicTrackName);
        musicPlayer.loop = true;
        musicPlayer.volume = 1.0f;

        musicPlayer.Play();
    }

    public void StopMusic()
    {
        if (musicPlayer == null || fadeOutSoundCoroutine != null)
        {
            return;
        }

        fadeOutSoundCoroutine = FadeOutSound();

        StartCoroutine(fadeOutSoundCoroutine);        
    }

    IEnumerator FadeOutSound()
    {
        while(musicPlayer.volume > 0)
        {
            musicPlayer.volume -= 0.4f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        GameObject musicPlayerGameObject = musicPlayer.gameObject;
        musicPlayer = null;
        fadeOutSoundCoroutine = null;
        Destroy(musicPlayerGameObject);        
    }
}