///////////////////////////////////////////////////////////////////////
//
//      BossTransition1.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class BossTransition1 : MonoBehaviour
{
    SpriteRenderer sr;
    BoxCollider2D col;
    SpriteMask sm;

    public bool transition = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        sm = GetComponentInChildren<SpriteMask>();

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.0f);
        col.enabled = false;
        sm.enabled = false;
    }

    void Update()
    {
        if (transition && sr.color.a <= Mathf.Epsilon)
        {
            col.enabled = true;
            sm.enabled = true;
        }

        if (transition && sr.color.a < 1.0f - Mathf.Epsilon)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + Time.deltaTime * 1.5f);
        }
    }

    public void StartTransition()
    {
        transition = true;
    }
}
