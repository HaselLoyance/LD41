///////////////////////////////////////////////////////////////////////
//
//      PointPickup.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class PointPickup : MonoBehaviour
{
    SpriteRenderer sr;
    ParticleSystem ps;

    bool fading = false;
    Vector3 wantedScale;

    void Awake()
    {
        wantedScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponentInChildren<ParticleSystem>();
        Invoke("StartDestruction", 5.0f);
    }

    void Update()
    {
        if (transform.localScale.x < wantedScale.x)
        {
            transform.localScale += (Vector3)Vector2.one * Time.deltaTime * 1.0f;
        }
        else
        {
            transform.localScale = wantedScale;
        }

        if (fading)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime / 3.0f);
        }
        
        if (sr.color.a < Mathf.Epsilon)
        {
            Destroy(gameObject);
        }
    }

    void StartDestruction()
    {
        fading = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Magnet")
        {
            GameManager.Instance.sm.PlaySound("Pickups");
            GameManager.Instance.pickups++;
            GameManager.Instance.RedrawScore();

            ps.transform.parent = null;
            ps.Emit(10);

            Destroy(gameObject);
        }
    }
}
