///////////////////////////////////////////////////////////////////////
//
//      GameManager.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    static public readonly uint TILE_SIZE_UNITS = 32;
    static public readonly uint N_TILES_HOR = 34;
    static public readonly uint N_TILES_VER = 19;
    
    static GameManager _instance = null;
    GameUI gui;
    
    public uint kills = 0;
    public uint laps = 0;
    public uint pickups = 0;
    public float timeMp = 0.0f;
    public float excessRage = 0.0f;

    public uint SCORE = 0;
    uint powerMultiplier = 1;
    Car c = null;
    public SoundManager sm;

    public void RedrawScore()
    {
        if (c == null)
        {
            c = FindObjectOfType<Car>();
        }

        CalcScore();
        gui.updateScore(SCORE);
    }
    
    public void RedrawPower()
    {
        if (c == null)
        {
            c = FindObjectOfType<Car>();
        }

        CalcPower();
        gui.updatePower(powerMultiplier);
    }

    public void RedrawRageMeter()
    {
        if (c == null)
        {
            c = FindObjectOfType<Car>();
        }

        CalcPower();
        gui.updateRageMeter(CalcRageCoeff());
    }

    public void RedrawLaps()
    {
        if (c == null)
        {
            c = FindObjectOfType<Car>();
        }
        
        gui.updateLaps(laps);
    }

    public void CalcScore()
    {
        SCORE = powerMultiplier * (kills * 12321 + laps * 55535 + pickups * 1337 + (uint)(timeMp * 1234567.0f) + (uint)(excessRage * 4815.0f));
    }
    
    public void CalcPower()
    {
        powerMultiplier = uint.Parse(c.firePower.ToString("n2").Remove(1, 1));
    }

    public float CalcRageCoeff()
    {
        return c.rageMeter / 10.0f;
    }

    void Awake()
    {
        gui = FindObjectOfType<GameUI>();
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        sm = GetComponent<SoundManager>();
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnLevelWasLoaded()
    {
        Resolution[] rs = Screen.resolutions;
        Resolution r = rs[rs.Length - 1];
        Screen.SetResolution(r.width, r.height, true);
        pickups = 0;
        laps = 0;
        kills = 0;
        timeMp = 0.0f;
        excessRage = 0.0f;
        SCORE = 0;
        gui = FindObjectOfType<GameUI>();

        if (gui!=null)
            gui.doStartAgain();

        if (sm!= null)
        {
            sm.LoadData();
            sm.PlayLevelMusic(SceneManager.GetActiveScene().name);
        }
    }
}
