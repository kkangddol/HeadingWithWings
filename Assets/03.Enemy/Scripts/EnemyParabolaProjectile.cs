using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParabolaProjectile : EnemyProjectile
{
    public AnimationCurve curve = null;
    public Vector3 startPos = Vector3.zero;
    public Vector3 targetPos = Vector3.zero;
    public GameObject particle = null;

    public float duration = 1.1f;
    public float heightY = 3.0f;

    private void Start()
    {
        Destroy(this.gameObject, 10f);
        StartCoroutine(Curve(startPos, targetPos));
    }

    IEnumerator Curve(Vector3 start, Vector2 target)
    {
        float timePassed = 0f;

        Vector3 prePos = Vector3.zero;
        Vector2 end = target;
        while(timePassed < duration)
        {
            timePassed += Time.deltaTime;

            float linearT = timePassed / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0f, heightY, heightT);

            Vector3 movePos = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height);
            transform.rotation = Utilities.LookAt2(this.transform, movePos);
            transform.position = movePos;

            if(timePassed <= 0.7f)
                prePos = this.GetComponent<Rigidbody2D>().velocity;

            yield return null;
        }

        particle.SetActive(true);
    }
}
