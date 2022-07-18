using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Satellites
{
    public Satellites(GameObject go, Vector3 v3)
    {
        this.go = go;
        this.v3 = v3;
    }

    public GameObject go { get; set; }
    public Vector3 v3 { get; set; }
}

public class SatelliteAttack : Equipment
{
    PlayerInfo playerInfo;
    public Bullet bullet;
    private List<Satellites> satellites = new List<Satellites>();
    private float angle = 0.0f;
    private bool isSpread = true;

    public float damageMultiplier;
    public float attackDelayMultiplier;
    public float attackRange = 2f;
    public float knockbackSize;
    public float bulletSpeed;

    public float attackDuration;
    public int satelliteCount = 5;

    private Transform targetTransform;
    private bool isCoolDown = false;
    private const int MAXLEVEL = 5;

    void Start()
    {
        Initialize();
        StartCoroutine(FireCycle());
    }

    private void Update()
    {
        if(isSpread == false) return;

        angle += Time.deltaTime * bulletSpeed;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void SetSatellites()
    {
        int index = 0;
        float deg = 360f / satelliteCount;

        for (float i = 0f; i < 360f; i += deg)
        {
            float rad = i * Mathf.Deg2Rad;
            if(index < satellites.Count)
            {
                Satellites temp = new Satellites();
                temp.go = satellites[index].go;
                Bullet tempBullet = temp.go.GetComponent<Bullet>();
                tempBullet.damage = playerInfo.damage * damageMultiplier;
                tempBullet.knockbackSize = knockbackSize;
                temp.go.transform.localPosition = Vector3.zero;
                temp.go.transform.localRotation = Quaternion.identity;
                temp.v3 = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * attackRange;
                satellites[index] = temp;
            }
            else
            {
                Satellites temp = new Satellites();
                // Instantiate Prefab
                temp.go = Object.Instantiate(bullet.gameObject);
                Bullet tempBullet = temp.go.GetComponent<Bullet>();
                tempBullet.damage = playerInfo.damage * damageMultiplier;
                tempBullet.knockbackSize = knockbackSize;
                temp.v3 = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * attackRange;
                temp.go.transform.SetParent(this.transform);
                temp.go.transform.localPosition = Vector3.zero;
                temp.go.transform.localRotation = Quaternion.identity;
                temp.go.SetActive(false);
                satellites.Add(temp);
            }

            index++;
        }
    }

    IEnumerator Spreading()
    {
        if(satellites.Count != satelliteCount)  SetSatellites();

        SetActiveAll(true);
        while (Vector2.Distance(satellites[0].go.transform.position, this.transform.position) <= attackRange)
        {
            SpreadingPosition();

            yield return null;
        }

        isSpread = true;
    }
    private void SpreadingPosition()
    {
        float spreadSpeed = Time.deltaTime * 2.5f;
        foreach (Satellites satellite in satellites)
        {
            satellite.go.transform.localPosition += satellite.v3 * spreadSpeed;
        }
    }

    IEnumerator Gathering()
    {
        while (satellites[0].go.transform.position != this.transform.position)
        {
            GatheringPosition();

            yield return null;
        }

        SetActiveAll(false);
        isSpread = false;
        StartCoroutine(CoolDown());
    }
    private void GatheringPosition()
    {
        float spreadSpeed = Time.deltaTime * 5f;
        foreach (Satellites satellite in satellites)
        {
            satellite.go.transform.position = Vector2.MoveTowards(satellite.go.transform.position, this.transform.position, spreadSpeed);
        }
    }

    private void SetActiveAll(bool isActive)
    {
        foreach (Satellites satellite in satellites)
        {
            satellite.go.SetActive(isActive);
        }
    }

    IEnumerator Fire()
    {
        isCoolDown = true;
        
        StartCoroutine(Spreading());

        yield return new WaitForSeconds(attackDuration);

        if(this.level == MAXLEVEL)
            yield break;

        StartCoroutine(Gathering());
    }

    void Initialize()
    {
        playerInfo = GameManager.playerInfo;
        if(bullet == null) return;
        SetSatellites();
    }

    IEnumerator FireCycle()
    {
        while (true)
        {
            yield return null;
            
            if (!isCoolDown)
            {
                StartCoroutine(Fire());
            }
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(playerInfo.attackDelay * attackDelayMultiplier);
        isCoolDown = false;
    }

    public override void SetLevel(int newLevel)
    {
        this.level = newLevel;

        //220527 하드코딩이므로 DataManager 이용할 것.
        switch (level)
        {
            case 1:
                {
                    damageMultiplier = 0.10f;
                    attackDelayMultiplier = 1.00f;
                    satelliteCount = 1;
                    break;
                }
            case 2:
                {
                    damageMultiplier = 0.15f;
                    attackDelayMultiplier = 0.95f;
                    satelliteCount = 2;
                    break;
                }
            case 3:
                {
                    damageMultiplier = 0.20f;
                    attackDelayMultiplier = 0.90f;
                    satelliteCount = 3;
                    break;
                }
            case 4:
                {
                    damageMultiplier = 0.25f;
                    attackDelayMultiplier = 0.85f;
                    satelliteCount = 4;
                    break;
                }
            case 5:
                {
                    damageMultiplier = 0.30f;
                    attackDelayMultiplier = 0.80f;
                    satelliteCount = 5;
                    break;
                }
            default:
                break;
        }
    }
}
