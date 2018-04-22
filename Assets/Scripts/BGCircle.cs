///////////////////////////////////////////////////////////////////////
//
//      BGCircle.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class BGCircle : MonoBehaviour
{
    float alphaSpeed = 0.014f;
    float scaleSpeed = 0.016f;
    bool fadeFlag = false;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Color c = sr.color;

        if (c.a < 0.5f && !fadeFlag)
        {
            c.a += alphaSpeed;
            sr.color = c;
        }
        else if (c.a >= 0.5f && !fadeFlag)
        {
            fadeFlag = true;
        }
        else if (c.a > 0.0f && fadeFlag)
        {
            c.a -= alphaSpeed;
            sr.color = c;
        }
        else if (c.a <= 0.0f && fadeFlag)
        {
            Destroy(gameObject);
            return;
        }

        transform.localScale += (Vector3)Vector2.one * scaleSpeed;
    }
}
