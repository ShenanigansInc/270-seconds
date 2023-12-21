using UnityEngine;

public class SmoothFollowUI : MonoBehaviour
{
    public RectTransform buttonTransf;
    public RectTransform trackTransf;

    public float smoothTime = 0.3F;
    public Vector3 velocity = Vector3.zero;

    void Start()
    {
        trackTransf.position = buttonTransf.position;
    }

    void Update()
    {
        Vector3 targetPosition = buttonTransf.TransformPoint(new Vector3(0, 5, -10));

        trackTransf.position = Vector3.SmoothDamp(trackTransf.position, targetPosition, ref velocity, smoothTime);
    }

}
