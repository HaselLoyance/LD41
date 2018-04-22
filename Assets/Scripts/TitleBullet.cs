///////////////////////////////////////////////////////////////////////
//
//      TitleBullet.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class TitleBullet : MonoBehaviour
{
    float speed = 450.0f;
    float scaleSpeed = 3.0f;

    Rigidbody2D rb;
    Vector3 wantedScale;
    float t;

    void Awake()
    {
        wantedScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Utils.GetVelocityVector(speed, transform.rotation.eulerAngles.z);
    }

    void Update()
    {
        t += Time.deltaTime;

        if (transform.localScale.x < wantedScale.x)
        {
            transform.localScale += (Vector3)Vector2.one * Time.deltaTime * scaleSpeed;
        }
        else
        {
            transform.localScale = wantedScale;
        }

        if (t > 2.5f)
        {
            Destroy(gameObject);
        }
    }
}
