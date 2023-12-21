using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    // Update is called once per frame
    void Update()
    {
        if(canvasGroup.alpha == 1)
        {
#if UNITY_ANDROID
            SceneManager.LoadScene("Main Menu - Android");
#endif
#if UNITY_WINDOWS
            SceneManager.LoadScene("Main Menu - Windows");
#endif
        }
    }
}
