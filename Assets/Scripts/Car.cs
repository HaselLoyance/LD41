///////////////////////////////////////////////////////////////////////
//
//      Car.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    public float acceleration = 25.0f;
    public float maxVelocity = 150.0f;
    public float turnSpeed = 50.0f;
    public float drag = 10.0f;
    public float wallUnstuck = 10;
    public bool isDead = false;
    public float firePower = 0.01f; //max 3.0f
    public float fireRate = 5.0f;
    public float rageMeter = 5.0f; //max 10
    public GameObject carBullet1;
    public GameObject carBullet2;
    public GameObject followTurret;
    public FollowTurret ft;
    public GameObject trackTurret;
    public TrackTurret tt;
    public ParticleSystem psThruster;
    public ParticleSystem psAdd1;
    public ParticleSystem psAdd2;
    public CircleCollider2D magnet;
    public GameObject gameOver;

    float currentVelocity = 0.0f;
    float rageMultiplier = 1.5f;
    float magnetSizeOrig = 0.0f;
    Vector3 initialPos;
    Vector3 initialRot;
    public Rigidbody2D rb;
    public bool lockTrackTurret = false;
    public bool rageActive = false;
    public bool lockControls = false;
    public bool unlimitedRage = false;
    public bool unlimitedPower = false;

    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation.eulerAngles;
        magnetSizeOrig = magnet.radius;
        rb = GetComponent<Rigidbody2D>();
        ft = followTurret.GetComponent<FollowTurret>();
        tt = trackTurret.GetComponent<TrackTurret>();
    }

    void Update()
    {
        if (lockControls)
        {
            return;
        }

        magnet.radius = magnetSizeOrig + firePower * 30.0f;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            unlimitedRage = !unlimitedRage;
            if(unlimitedRage)
            {
                GameManager.Instance.sm.PlaySound("Agree");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            unlimitedPower = !unlimitedPower;
            if (unlimitedPower)
            {
                GameManager.Instance.sm.PlaySound("Agree");
            }
        }

        if (unlimitedRage)
        {
            if (rageMeter < 10.0f)
            {
                rageMeter = 10.0f;
                GameManager.Instance.RedrawRageMeter();
            }
        }

        if (unlimitedPower)
        {
            if (firePower < 3.0f)
            {
                firePower = 3.0f;
                GameManager.Instance.RedrawPower();
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (currentVelocity < 0.0f)
            {
                currentVelocity += acceleration * Time.deltaTime * 3.0f;
            }
            else
            {
                currentVelocity = Mathf.Min(maxVelocity, currentVelocity + acceleration * Time.deltaTime);
            }

            if (!psThruster.isPlaying)
            {
                psThruster.Play();
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (currentVelocity > 0.0f)
            {
                currentVelocity -= acceleration * Time.deltaTime * 3.0f;
            }
            else
            {
                currentVelocity = Mathf.Max(-maxVelocity / 2.0f, currentVelocity - acceleration * Time.deltaTime / 1.25f);
            }

            if (!psThruster.isPlaying)
            {
                psThruster.Play();
            }
        }
        else
        {
            currentVelocity += -Mathf.Sign(currentVelocity) * drag * Time.deltaTime;

            if (psThruster.isPlaying)
            {
                psThruster.Stop();
            }
        }

        if (currentVelocity != 0.0f && !psAdd1.isPlaying)
        {
            psAdd1.Play();
            psAdd2.Play();
        }
        else if (Mathf.Abs(currentVelocity) < 5.0f && psAdd1.isPlaying && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            currentVelocity = 0.0f;
            psAdd1.Stop();
            psAdd2.Stop();
        }

        rb.velocity = Utils.GetVelocityVector(currentVelocity, transform.rotation.eulerAngles.z);
        rb.angularVelocity = 0;

        if (Mathf.Abs(currentVelocity) > 2.0f)
        {
            if (Input.GetKey(KeyCode.A))
            {
                Quaternion q = rb.transform.rotation;
                q.eulerAngles += new Vector3(0, 0,turnSpeed * (currentVelocity * 0.65f / maxVelocity) * 1.5f * Time.deltaTime);
                rb.transform.rotation = q;
            }

            if (Input.GetKey(KeyCode.D))
            {
                Quaternion q = rb.transform.rotation;
                q.eulerAngles += new Vector3(0, 0, -turnSpeed * (currentVelocity * 0.65f / maxVelocity) * 1.5f * Time.deltaTime);
                rb.transform.rotation = q;
            }
        }

        if (firePower >= 3.0f && !ft.turretEnabled)
        {
            ft.turretEnabled = true;
        }
        else if (firePower < 3.0f && ft.turretEnabled)
        {
            ft.turretEnabled = false;
        }

        if (Input.GetMouseButtonDown(0) && !lockTrackTurret)
        {
            Invoke("DoShooty2", 1 / (fireRate + firePower * 2.0f));
        }

        if (Input.GetMouseButtonUp(0) && !lockTrackTurret)
        {
            CancelInvoke("DoShooty2");
        }

        if (Input.GetMouseButtonDown(1) && !lockTrackTurret && rageMeter >=3.0f)
        {
            GameManager.Instance.sm.PlaySound("Ultra");
            lockTrackTurret = true;
            CancelInvoke("DoShooty2");
            rageMeter -= 3.0f;
            GameManager.Instance.RedrawRageMeter();
            tt.Bomb();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("DoShooty1", 1 / (fireRate + firePower * 2.0f));
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("DoShooty1");
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && !rageActive && rageMeter > 0.0f)
        {
            rageActive = true;
            maxVelocity *= rageMultiplier;
            turnSpeed *= rageMultiplier;
            acceleration *= rageMultiplier;
            fireRate *= rageMultiplier;
        }

        if (Input.GetKey(KeyCode.LeftShift) && rageMeter >0.0f && rageActive)
        {
            rageMeter -= Time.deltaTime * 0.1f;
            GameManager.Instance.RedrawRageMeter();
        }
        else if (Input.GetKey(KeyCode.LeftShift) && rageMeter < 0.0f && rageActive)
        {
            rageMeter = 0.0f;
            GameManager.Instance.RedrawRageMeter();
            ResetRageStats();
            return;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && rageActive)
        {
            ResetRageStats();
        }
    }

    void ResetRageStats()
    {
        rageActive = false;
        maxVelocity /= rageMultiplier;
        turnSpeed /= rageMultiplier;
        acceleration /= rageMultiplier;
        fireRate /= rageMultiplier;
    }

    void DoShooty1()
    {
        GameManager.Instance.sm.PlaySound("PlayerShoot");

        if (firePower < 1.0f || firePower >= 2.0f)
        {
            Vector3 centerOffset = Utils.GetVelocityVector(20.0f, transform.rotation.eulerAngles.z);
            Instantiate(carBullet1, transform.position + centerOffset, transform.rotation);
        }

        if (firePower >= 1.0f)
        {
            Vector3 leftRot = transform.rotation.eulerAngles;
            Vector3 leftOffset = Utils.GetVelocityVector(20.0f, transform.rotation.eulerAngles.z + 7.0f);

            leftRot.z += 5.0f;
            Instantiate(carBullet2, transform.position + leftOffset, Quaternion.Euler(leftRot));

            Vector3 rightRot = transform.rotation.eulerAngles;
            Vector3 rightOffset = Utils.GetVelocityVector(20.0f, transform.rotation.eulerAngles.z - 7.0f);

            rightRot.z -= 5.0f;
            Instantiate(carBullet2, transform.position + rightOffset, Quaternion.Euler(rightRot));
        }

        if (ft.turretEnabled)
        {
            ft.DoShooty();
        }

        if (Input.GetKey(KeyCode.Space) && !isDead)
        {
            Invoke("DoShooty1", 1 / (fireRate + firePower * 2.0f));
        }
    }

    void DoShooty2()
    {
        tt.hujak();
        GameManager.Instance.sm.PlaySound("UltraNormalShot");

        if (Input.GetMouseButton(0) && !isDead)
        {
            Invoke("DoShooty2", 1 / (fireRate + firePower * 2.0f) * 10.0f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "LevelEdge" || collision.gameObject.tag == "BossTransition1" || collision.gameObject.tag == "BossTransition2")
        {
            BlinkOnContact bon = collision.gameObject.GetComponent<BlinkOnContact>();
            if (bon!=null)
            {
                bon.Blink();
            }
            transform.position += (Vector3)Utils.GetVelocityVector(-wallUnstuck * Mathf.Sign(currentVelocity), transform.rotation.eulerAngles.z);

            firePower = Mathf.Max(firePower * 0.9f, 0.01f);
            GameManager.Instance.RedrawPower();

            rb.velocity = Vector2.zero;
            currentVelocity = 0;
            rb.angularVelocity = 0;
        }
    }

    public void Die()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("CarBullet"))
        {
            Destroy(go);
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("CarBulletUltra"))
        {
            Destroy(go);
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            go.GetComponent<Enemy>().Die();
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            Destroy(go);
        }

        isDead = true;
        lockControls = true;
        currentVelocity = 0.0f;
        lockTrackTurret = false;
        rageActive = false;
        GetComponent<ParticleSystem>().Emit(100);
        transform.position += (Vector3)Vector2.one * 7000;
        rageMeter = 0.0f;
        rb.velocity = Vector2.zero;
        
        GameManager.Instance.CalcScore();

        GameManager.Instance.sm.PlaySound("PlayerDeath");

        Destroy(FindObjectOfType<Canvas>().gameObject);

        GameObject go1 = Instantiate(gameOver);
        go1.transform.GetChild(go1.transform.childCount - 1).GetComponent<Text>().text = "Score: " + GameManager.Instance.SCORE;
        go1.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        go1.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
    }

    public void Revive()
    {
        isDead = false;
        lockControls = false;
        transform.position = initialPos;

        acceleration = 90.0f;
        maxVelocity = 300.0f;
        turnSpeed = 150.0f;
        drag = 120.0f;
        wallUnstuck = 2;
        lockTrackTurret = false;
        firePower = 0.01f;
        fireRate = 5.0f;
        rageMeter = 10.0f;
        currentVelocity = 0.0f;
        magnet.radius = magnetSizeOrig;
        transform.rotation = Quaternion.Euler(initialRot);
        FindObjectOfType<LapController>().ResetTrack();
    }
}
