///////////////////////////////////////////////////////////////////////
//
//      Enemy.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 20.0f;
    public float diffCoeff = 1.0f;
    public GameObject pickup;
    public GameObject powerup;
    public GameObject special;

    public bool distanceLimited = false;
    EnemyBarrier eb;
    float health;
    Vector3 initialPos;
    public bool isDead = false;
    public Car c;
    List<SpriteRenderer> srs = new List<SpriteRenderer>();
    
    void Start()
    {
        c = FindObjectOfType<Car>();
        eb = transform.parent.GetComponentInChildren<EnemyBarrier>();
        initialPos = transform.position;
        srs = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());

        RecalcDiff();
        health = maxHealth;
    }

    void Update()
    {
        float d = Vector3.Distance(transform.position, c.transform.position);
        if (d > 600.0f && !distanceLimited)
        {
            distanceLimited = true;
        }
        else if (d < 600.0f && distanceLimited)
        {
            distanceLimited = false;
        }

        if (distanceLimited)
        {
            return;
        }

        if (health <= 0.0f && !isDead)
        {
            ParticleSystem ps = GetComponent<ParticleSystem>();
            ps.Emit(30);

            GameManager.Instance.kills++;
            GameManager.Instance.RedrawScore();

            int max = Random.Range(8, 14);

            for (int i =0; i < max; i++)
            {
                float r = Random.Range(0.0f, 1.0f);

                if (r < 0.50f)
                {
                    Instantiate(pickup, transform.position + new Vector3(Random.Range(-48.0f, 48.0f), Random.Range(-48.0f, 48.0f)), Quaternion.identity);
                }
                else if (r <0.87f)
                {
                    Instantiate(powerup, transform.position + new Vector3(Random.Range(-48.0f, 48.0f), Random.Range(-48.0f, 48.0f)), Quaternion.identity);
                }
                else
                {
                    Instantiate(special, transform.position + new Vector3(Random.Range(-48.0f, 48.0f), Random.Range(-48.0f, 48.0f)), Quaternion.identity);
                }
            }

            Die();
            return;
        }
    }

    public void RecalcDiff()
    {
        maxHealth /= diffCoeff;

        diffCoeff = eb.areaDifficultyMp * (GameManager.Instance.laps / 1.5f + 1.0f);

        maxHealth *= diffCoeff;
    }

    public void Revive()
    {
        foreach (SpriteRenderer sr in srs)
        {
            sr.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        transform.position = initialPos;
        isDead = false;
        health = maxHealth;
        RecalcDiff();
    }

    public void Die()
    {
        GameManager.Instance.sm.PlaySound("EnemyDeath");
        health = 0.0f;
        isDead = true;
        transform.position -= (Vector3)(Vector2.one) * 5000;
        RecalcDiff();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CarBullet" || collision.gameObject.tag == "CarBulletUltra")
        {
            CarBullet cb = collision.gameObject.GetComponent<CarBullet>();

            health -= cb.damage;

            foreach(SpriteRenderer sr in srs)
            {
                sr.color = new Color(sr.color.r, health / maxHealth, health / maxHealth, 1.0f);
            }
            
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag =="Car")
        {
            c.rageMeter -= 5.0f * (diffCoeff / 2.0f);
            c.GetComponent<BlinkOnContact>().Blink();

            GameManager.Instance.sm.PlaySound("PlayerHit");
            if (c.rageMeter < 0.0f && !c.unlimitedRage)
            {
                c.rageMeter = 0.0f;

                c.Die();
            }
        }
    }
}
