using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    private float meteorRadius = 3f;
    private int meteorPower = 10;
    private int meteorDamage = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y < 0.7f)
        {
            // Particle System을 추가해야함

            Collider[] hitColliders = Physics.OverlapSphere(new Vector3(transform.position.x, 0, transform.position.z), meteorRadius);

            foreach (var hitCollider in hitColliders)
            {
                EnemyPrototype enemy = hitCollider.gameObject.GetComponent<EnemyPrototype>();
                if (enemy == null)
                {
                    continue;
                }

                enemy.TakeDamage(transform, meteorDamage, meteorPower);
            }
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            MeteorAttack.ReturnObject(this);
        }
    }
}
