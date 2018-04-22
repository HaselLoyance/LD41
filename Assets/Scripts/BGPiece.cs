///////////////////////////////////////////////////////////////////////
//
//      BGPiece.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class BGPiece : MonoBehaviour
{
    public Color[] colors = new Color[6];
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Invoke("ChangeColor", 2.0f);
    }

    void ChangeColor()
    {
        int val = Random.Range(0, 4);
        sr.color = colors[val];
        Quaternion q = transform.rotation;
        q.eulerAngles += new Vector3(0, 0, 30);
        transform.rotation = q;
        Invoke("ChangeColor", 2.0f);
    }
}
