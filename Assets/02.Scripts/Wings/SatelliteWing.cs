using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteWing : MonoBehaviour
{
    private PlayerInfo playerInfo;
    public Satellite satellitePrefab;
    private Satellite[] satellites;
    public int damage;
    public float knockbackSize;
    public float orbitSpeed;
    public float orbit;

    private void Start()
    {
        Initialize();
        StartCoroutine(SummonCycle());
    }

    void Initialize()
    {
        playerInfo = GetComponent<PlayerInfo>();
        satellites = new Satellite[2];
        SummonSatellite();
    }

    void SummonSatellite()
    {
        satellites[0] = Instantiate(satellitePrefab, transform.position + (transform.forward * orbit), Quaternion.identity);
        satellites[0].transform.SetParent(transform);
        satellites[0].damage = this.damage;
        satellites[0].knockbackSize = this.knockbackSize;
        satellites[0].orbitSpeed = this.orbitSpeed;
        
        satellites[1] = Instantiate(satellitePrefab, transform.position - (transform.forward * orbit), Quaternion.identity);
        satellites[1].transform.SetParent(transform);
        satellites[1].damage = this.damage;
        satellites[1].knockbackSize = this.knockbackSize;
        satellites[1].orbitSpeed = this.orbitSpeed;
    }

    IEnumerator SummonCycle()
    {
        while(this.enabled == true)
        {
            yield return new WaitForSeconds(10.0f);
            SummonSatellite();
        }
    }
}
