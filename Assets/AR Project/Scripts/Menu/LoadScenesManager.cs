using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenesManager : MonoBehaviour
{
    public static LoadScenesManager instance;

    void Awake()
    {
        instance = this;
    }

    public void LoadScene(string scene)
    {
        switch (scene)
        {
            case "ar":
                SceneManager.LoadScene("ARScene");
                break;
            case "shorts":
                SceneManager.LoadScene("Shorts - Android");
                break;
            case "menu":
                SceneManager.LoadScene("Main Menu - Android");
                break;
        }
    }

}
