using UnityEngine;

public class AudioCaller : MonoBehaviour
{
    public void PlayMusic (string musicTrackName)
    {
        AudioPlayer.Instance.PlayMusic(musicTrackName);
    }

    public void PlaySFX (string sfxName)
    {
        AudioPlayer.Instance.PlaySFX(sfxName);
    }

    public void StopMusic()
    {
        AudioPlayer.Instance.StopMusic();
    }
}
