using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchAnimator : MonoBehaviour
{
    [SerializeField]
    private List<Renderer> myRenderers;
    [SerializeField]
    private float minGlitchLength = .01f;
    [SerializeField]
    private float maxGlitchLength = .03f;
    [SerializeField]
    private float pauseBetweenGlitches = 2f;
    [SerializeField]
    private int minNumOfGlitches = 2;
    [SerializeField]
    private int maxNumOfGlitches = 7;

    bool glitchStartDuringUpdate = false;

    //Start is called before the first frame update
    void Start()
    {
        CallGlitchCoroutine();
    }

    void OnEnable()
    {
        CallGlitchCoroutine();
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        if(!glitchStartDuringUpdate)
        {
            CallGlitchCoroutine();
        }
    }

    public void CallGlitchCoroutine()
    {
        if (GameObject.Find("AppKiller").GetComponent<AppKiller>().TimeSinceAppStarted < GameObject.Find("AppKiller").GetComponent<AppKiller>().TimeToStartGlitching)
        {
            return;
        }

        StartCoroutine(IEAnimateGlitch());
    }

    IEnumerator IEAnimateGlitch()
    {
        glitchStartDuringUpdate = true;        

        for (int i = 0; i < Random.Range(minNumOfGlitches, maxNumOfGlitches); i++)
        {
            foreach (Renderer rend in myRenderers)
            {
                rend.material.SetFloat("_GlitchIntensity", Random.Range(-.1f, .1f));
            }

            float glitchLength = Random.Range(minGlitchLength, maxGlitchLength);
            
            yield return new WaitForSeconds(glitchLength);
        }

        foreach (Renderer rend in myRenderers)
        {
            rend.material.SetFloat("_GlitchIntensity", 0f);
        }

        pauseBetweenGlitches = Random.Range(20f, 40f);

        yield return new WaitForSeconds(pauseBetweenGlitches);

        StartCoroutine(IEAnimateGlitch());
    }
}
