using UnityEngine;

public class RepeatButton : MonoBehaviour
{
    [SerializeReference]
    AudioCaller audioCaller;
    [SerializeReference]
    GameObject headNormal;
    [SerializeReference]
    GameObject headReverse;
    [SerializeReference]
    string audioNormal;
    [SerializeReference]
    string audioReverse;

    public void Repeat()
    {
        if(headNormal.activeSelf)
        {
            headNormal.GetComponent<Animator>().Play(0);
            audioCaller.PlayMusic(audioNormal);
        }

        if (headReverse.activeSelf)
        {
            headReverse.GetComponent<Animator>().Play(0);
            audioCaller.PlayMusic(audioReverse);
        }
    }
}
