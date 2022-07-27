using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackParabola : MonoBehaviour
{
    public GameObject bullet = null;
    public Transform playerTr = null;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Test());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Test()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);

            Vector3 toPlayerV3 = playerTr.position - this.transform.position;
            toPlayerV3.x *= 0.5f;
            toPlayerV3.y *= 2.0f;

            GameObject go = Instantiate(bullet, this.transform.position, Utilities.LookAt2(this.transform, toPlayerV3));
            go.GetComponent<Rigidbody2D>().AddForce(go.transform.right * toPlayerV3.magnitude, ForceMode2D.Impulse);
            go.GetComponent<Rigidbody2D>().AddTorque(1f);
        }
    }
}
