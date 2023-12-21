using UnityEngine;

public class AppKiller : MonoBehaviour
{
    [SerializeField]
    private GameObject deathAnimCanvas;    
    [SerializeField]
    float timeToStartGlitching = 30f;
    [SerializeField]
    float timeToDie = 270f;

    float timeSinceAppStarted = 0f;

    public float TimeSinceAppStarted { get => timeSinceAppStarted; }
    public float TimeToStartGlitching { get => timeToStartGlitching; }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAppStarted += Time.deltaTime;

        if (timeSinceAppStarted >= timeToDie)
            deathAnimCanvas.SetActive(true);
    }
}