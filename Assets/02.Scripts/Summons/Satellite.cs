using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    const string ENEMY = "ENEMY";
    public int damage;
    public float knockbackSize;
    public float orbitSpeed;

    private void Start() {
        Destroy(gameObject, 5.0f);
    }

    private void Update()
    {
        transform.RotateAround(transform.parent.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ENEMY)
        {
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
            Destroy(gameObject);
        }
    }
}
