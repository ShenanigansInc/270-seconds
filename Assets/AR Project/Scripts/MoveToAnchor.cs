using UnityEngine;

public class MoveToAnchor : MonoBehaviour
{

    public GameObject anchorObject;

    public float smoothTime = 0.2f;
    private Vector3 positionVelocity;

    private void OnEnable()
    {
        transform.SetPositionAndRotation(anchorObject.transform.position, anchorObject.transform.rotation);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, anchorObject.transform.position) > 0.08f)
        {
            transform.SetPositionAndRotation(anchorObject.transform.position, anchorObject.transform.rotation);
            return;
        }

        transform.SetPositionAndRotation(Vector3.SmoothDamp(transform.position, anchorObject.transform.position, ref positionVelocity, smoothTime),
            Quaternion.Lerp(transform.rotation, anchorObject.transform.rotation, smoothTime));
    }
}
