using UnityEngine;

public class ButtonCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "TopBorder":
                collision.GetComponent<Collider2DSize>().image.CrossFadeAlpha(0.3f, 0.5f, false);
                ARMenuManager.instance.topBorder = true;
                ARMenuManager.instance.bottomBorder = false;
                break;
            case "BottomBorder":
                collision.GetComponent<Collider2DSize>().image.CrossFadeAlpha(0.3f, 0.5f, false);
                ARMenuManager.instance.topBorder = false;
                ARMenuManager.instance.bottomBorder = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "TopBorder":
                collision.GetComponent<Collider2DSize>().image.CrossFadeAlpha(1.0f, 0.5f, false);
                ARMenuManager.instance.topBorder = false;
                break;
            case "BottomBorder":
                collision.GetComponent<Collider2DSize>().image.CrossFadeAlpha(1.0f, 0.5f, false);
                ARMenuManager.instance.bottomBorder = false;
                break;
        }
    }
}
