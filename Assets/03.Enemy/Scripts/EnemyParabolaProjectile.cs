using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParabolaProjectile : EnemyProjectile
{
    public AnimationCurve curve = null;
    [HideInInspector]
    public Vector3 startPos = Vector3.zero;
    [HideInInspector]
    public Vector3 targetPos = Vector3.zero;
    public GameObject particle = null;

    public float duration = 1.1f;
    public float heightY = 3.0f;

    private Coroutine co = null;

    private void Start()
    {
        pool = CupidProjectilePool.Instance;
    }

    private void OnEnable() {
        Invoke("ReturnProjectile", 10.0f);
        co = StartCoroutine(Curve(startPos, targetPos));
    }

    private void OnDisable()
    {
        CancelInvoke();
        StopCoroutine(co);
        particle.SetActive(false);
    }

    IEnumerator Curve(Vector3 start, Vector2 target)
    {
        float timePassed = 0f;

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

            yield return null;
        }

        particle.SetActive(true);
    }
}
