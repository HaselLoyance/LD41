///////////////////////////////////////////////////////////////////////
//
//      CarBullet.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class CarBullet : MonoBehaviour
{
    public float speed = 100.0f;
    public float scaleSpeed = 10.0f;
    public float damage = 1.0f;
    Rigidbody2D rb;
    Vector3 wantedScale;

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

        if (collision.gameObject.tag == "EnemyBullet" && gameObject.tag == "CarBulletUltra")
        {
            Destroy(collision.gameObject);
        }
    }

    void OnDestroy()
    {
        return;

        GameObject go = transform.GetChild(0).gameObject;
        go.transform.parent = null;
        go.GetComponent<ParticleSystem>().Play();
        go.GetComponent<GarboCollectDestruct>().enabled = true;
    }
}
