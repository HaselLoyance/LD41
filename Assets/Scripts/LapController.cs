///////////////////////////////////////////////////////////////////////
//
//      LapController.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;

public class LapController : MonoBehaviour
{
    public List<GameObject> checkpoints = new List<GameObject>();
    Car c;

    int lapNumber = 0;
    List<LapCheckpoint> _checkpoints = new List<LapCheckpoint>();
    List<EnemyBarrier> _barriers = new List<EnemyBarrier>();
    float t = 0.0f;
    float optimalT = 140.0f;
    void Start()
    {
        c = FindObjectOfType<Car>();
        foreach (GameObject go in checkpoints)
        {
            _checkpoints.Add(go.GetComponent<LapCheckpoint>());
        }

        _barriers = new List<EnemyBarrier>(FindObjectsOfType<EnemyBarrier>());
    }

    void Update()
    {
        t += Time.deltaTime;
    }

    public bool LastSet(int number)
    {
        if (!_checkpoints[0].completed && number == 0)
        {
            return true;
        }
        else if (number != 0 && _checkpoints[number-1].completed)
        {
            return true;
        }

        return false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Car")
        {
            if (lapNumber == 0 || _checkpoints[_checkpoints.Count - 1].completed)
            {
                if (lapNumber != 0 && optimalT - t > 0.0f)
                {
                    GameManager.Instance.timeMp += (optimalT - t) / 2.0f;
                }

                GetComponentInChildren<ParticleSystem>().Play();
                lapNumber++;
                GameManager.Instance.laps++;

                GameManager.Instance.sm.PlaySound("LapFinish");

                if (lapNumber == 4)
                {
                    foreach(GameObject go in GameObject.FindGameObjectsWithTag("BossTransition1"))
                    {
                        go.GetComponent<BossTransition1>().StartTransition();
                    }
                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("BossTransition2"))
                    {
                        go.GetComponent<BossTransition2>().StartTransition();
                    }
                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
                    {
                        Destroy(go);
                    }
                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemyBullet"))
                    {
                        Destroy(go);
                    }
                }

                if (c.rageMeter > 10.0f)
                {
                    GameManager.Instance.excessRage = c.rageMeter - 10.0f;
                    c.rageMeter = 10.0f;
                }

                GameManager.Instance.RedrawRageMeter();
                GameManager.Instance.RedrawScore();
                GameManager.Instance.RedrawLaps();

                GetComponent<BlinkOnContact>().Blink();

                foreach(EnemyBarrier eb in _barriers)
                {
                    eb.ReviveAll();
                    eb.deactivated = false;
                    eb.block2d.enabled = true;
                    eb.sr.color = new Color(eb.sr.color.r, eb.sr.color.g, eb.sr.color.b, 1.0f);
                }
            }

            foreach (LapCheckpoint c in _checkpoints)
            {
                c.completed = false;
                c.sr.color = Color.white;
            }
        }
    }

    public void ResetTrack()
    {
        lapNumber = 0;

        foreach (EnemyBarrier eb in _barriers)
        {
            eb.ReviveAll();
            eb.deactivated = false;
            eb.block2d.enabled = true;
            eb.sr.color = new Color(eb.sr.color.r, eb.sr.color.g, eb.sr.color.b, 1.0f);
        }

        foreach (LapCheckpoint c in _checkpoints)
        {
            c.completed = false;
            c.sr.color = Color.white;
        }

        t = 0.0f;
    }
}
