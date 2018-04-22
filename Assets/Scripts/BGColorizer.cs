///////////////////////////////////////////////////////////////////////
//
//      BGColorizer.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BGColorizer : MonoBehaviour
{
    public GameObject hugeAssCircle;
    public Color bg;
    public Color fg;

    Camera c;
    float h1;
    float s1;
    float v1;
    float h2;
    float s2;
    float v2;
    List<SpriteRenderer> fgObjs = new List<SpriteRenderer>();
    List<SpriteRenderer> fgObjs1 = new List<SpriteRenderer>();

    void SpawnSpiro()
    {
        Instantiate(
            hugeAssCircle,
            c.transform.position + new Vector3(
                Random.Range(-768.0f, 768.0f),
                Random.Range(-768.0f, 768.0f),
                98
            ),
            Quaternion.identity
        );

        Invoke("SpawnSpiro", 0.75f);
    }

    void Start()
    {
        c = FindObjectOfType<Camera>();
        Color.RGBToHSV(bg, out h1, out s1, out v1);
        Color.RGBToHSV(fg, out h2, out s2, out v2);
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("LevelEdge"))
        {
            fgObjs.Add(go.GetComponent<SpriteRenderer>());
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Checkpoint"))
        {
            fgObjs1.Add(go.GetComponent<SpriteRenderer>());
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("BossTransition1"))
        {
            fgObjs1.Add(go.GetComponent<SpriteRenderer>());
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("BossTransition2"))
        {
            fgObjs1.Add(go.GetComponent<SpriteRenderer>());
        }
        SpawnSpiro();
    }

    void Update()
    {
        c.backgroundColor = Color.HSVToRGB(h1, s1, v1 + Mathf.Sin(Time.time / 2.0f) / 7.0f);
        
        Color c1 = Color.HSVToRGB(h2, s2, v2 + Mathf.Sin(Time.time / 2.0f) / 7.0f);

        foreach (SpriteRenderer s in fgObjs)
        {
            if (s != null)
            {
                s.color = c1;
            }
        }

        foreach (SpriteRenderer s in fgObjs1)
        {
            if (s != null)
            {
                s.color = new Color(c1.r, c1.g, c1.b, s.color.a);
            }
        }
    }
}
