///////////////////////////////////////////////////////////////////////
//
//      Utils.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public static class Utils
{
    public static Vector2 GetVelocityVector(float velocity, float angle)
    {
        return new Vector2(velocity * Mathf.Cos(angle * Mathf.Deg2Rad),
                                  velocity * Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    public static IEnumerator Fade(AudioSource audioSource, float from, float to, float fadeTime)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (to < Mathf.Epsilon)
        {
            audioSource.Pause();
        }

        audioSource.volume = to;

        yield return null;
    }
}