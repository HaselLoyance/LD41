///////////////////////////////////////////////////////////////////////
//
//      LapCheckpoint.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class LapCheckpoint : MonoBehaviour
{
    public bool completed = false;
    public int number = 0;

    LapController lc;
    Car c;
    public SpriteRenderer sr;

    void Awake()
    {
        lc = FindObjectOfType<LapController>();
        sr = GetComponent<SpriteRenderer>();
        c = FindObjectOfType<Car>();
    }

    void Update()
    {
        if (completed && sr.color.a > Mathf.Epsilon)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime * 2);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Car" && !completed && lc.LastSet(number))
        {
            GameManager.Instance.sm.PlaySound("CheckpointFinish");
            c.rageMeter += 0.5f;
            GetComponentInChildren<ParticleSystem>().Play();

            if (c.rageMeter > 10.0f)
            {
                GameManager.Instance.excessRage = c.rageMeter - 10.0f;
                c.rageMeter = 10.0f;
            }

            GameManager.Instance.RedrawRageMeter();
            GameManager.Instance.RedrawScore();

            completed = true;
        }
    }
}
