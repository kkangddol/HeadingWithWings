using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    private int explodeRadius = 3;
    private int explodeKnockBackSize = 10;
    private int meteorDamage = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Terrain")
        {
            //Collider[] hitColliders = Physics.OverlapSphere(new Vector3(transform.position.x, 0, transform.position.z), explodeRadius);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explodeRadius);

            foreach (var hitCollider in hitColliders)
            {
                EnemyPrototype enemy = hitCollider.gameObject.GetComponent<EnemyPrototype>();
                if (enemy == null)
                {
                    continue;
                }

                enemy.TakeDamage(transform, meteorDamage, explodeKnockBackSize);
            }
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            MeteorPool.ReturnObject(this);
        }
    }
}
