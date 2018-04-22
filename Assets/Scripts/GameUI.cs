///////////////////////////////////////////////////////////////////////
//
//      GameUI.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text scoreText;
    public Text powerText;
    public Image rageBarImage;
    public Text lapText;

    RectTransform mainRect;
    RectTransform rageBarRect;

    void Start()
    {
        mainRect = GetComponent<RectTransform>();
        rageBarRect = rageBarImage.GetComponent<RectTransform>();
    }

    public void updateScore(ulong score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void updatePower(uint power)
    {
        powerText.text = "Power: " + (power +  "/300");
    }

    public void updateRageMeter(float coeff)
    {
        rageBarRect.sizeDelta = new Vector2(mainRect.rect.width * coeff, rageBarRect.sizeDelta.y);
    }

    public void updateLaps(uint laps)
    {
        if (laps == 4)
        {
            lapText.text = "Lap: ?/?";
        }
        else
        {
            lapText.text = "Lap: " + laps + "/3";
        }
    }

    public void doStartAgain()
    {
        Start();
    }
}
