using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private float meteorRadius; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y < 1f)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var hitCollider in hitColliders)
            {
                hitCollider.SendMessage("AddDamage");
            }
            Destroy(gameObject);
        }
    }
}
