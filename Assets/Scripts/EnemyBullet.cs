///////////////////////////////////////////////////////////////////////
//
//      EnemyBullet.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 50.0f;
    public float scaleSpeed = 10.0f;
    public float damage = 1.0f;
    Rigidbody2D rb;
    Vector3 wantedScale;
    public float diffCoef = 1.0f;
    Car c;

    void Awake()
    {
        wantedScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void Start()
    {
        c = FindObjectOfType<Car>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Utils.GetVelocityVector(speed, transform.rotation.eulerAngles.z);
    }

    void Update()
    {
        if (transform.localScale.x < wantedScale.x)
        {
            transform.localScale += (Vector3)Vector2.one * Time.deltaTime * scaleSpeed;
        }
        else
        {
            transform.localScale = wantedScale;
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LevelEdge" || collision.gameObject.tag == "BossTransition1" || collision.gameObject.tag == "BossTransition2")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Car")
        {
            GameManager.Instance.sm.PlaySound("PlayerHit");
            c.rageMeter -= damage * (diffCoef/2.0f);
            c.GetComponent<BlinkOnContact>().Blink();

            if (c.rageMeter <0.0f && !c.unlimitedRage)
            {
                c.rageMeter = 0.0f;

                c.Die();
            }

            GameManager.Instance.RedrawRageMeter();

            Destroy(gameObject);
        }
    }
    
    void OnDestroy()
    {
        return;

        if (transform.childCount == 0)
        {
            return;
        }
        
        GameObject go = transform.GetChild(0).gameObject;
        if (go != null)
        {
            go.transform.parent = null;
            go.GetComponent<ParticleSystem>().Play();
            go.GetComponent<GarboCollectDestruct>().enabled = true;
        }
    }
}
