///////////////////////////////////////////////////////////////////////
//
//      EnemyBarrier.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;

public class EnemyBarrier : MonoBehaviour
{
    public bool deactivated = false;
    public float areaDifficultyMp = 1.0f;

    Car c;
    public SpriteRenderer sr;
    List<Enemy> enemies = new List<Enemy>();
    public EdgeCollider2D block2d;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        c = FindObjectOfType<Car>();
        enemies = new List<Enemy>(transform.parent.GetComponentsInChildren<Enemy>());
        block2d = transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<EdgeCollider2D>();
    }

    public void ReviveAll()
    {
        foreach(Enemy e in enemies)
        {
            e.Revive();
        }
    }

    void Update()
    {
        if (deactivated && sr.color.a > Mathf.Epsilon)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime * 2);
        }

        if (!deactivated)
        {
            bool allEnemiesAreDead = true;
            foreach(Enemy e in enemies)
            {
                if (!e.isDead)
                {
                    allEnemiesAreDead = false;
                    break;
                }
            }

            if (allEnemiesAreDead)
            {
                GameManager.Instance.sm.PlaySound("EnemyBarrierDown");

                GetComponentInChildren<ParticleSystem>().Play();
                deactivated = true;
                block2d.enabled = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Car")
        {
            if (!deactivated)
            {
                c.firePower = Mathf.Max(c.firePower * 0.7f, 0.01f);
                GameManager.Instance.RedrawPower();

                GameManager.Instance.sm.PlaySound("EnemyBarrierHit");

                GetComponentInChildren<ParticleSystem>().Play();
                deactivated = true;
                block2d.enabled = false;
                foreach (Enemy e in enemies)
                {
                    if (!e.isDead)
                    {
                        e.Die();
                    }
                }
            }
        }
    }
}
