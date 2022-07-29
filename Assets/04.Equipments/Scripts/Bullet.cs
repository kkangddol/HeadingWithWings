using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float knockbackSize;

    public GameObject hitEffectPrefab;

    protected void HitEffect(Vector3 hitPos)
    {
        GameObject go = GameObject.Instantiate(hitEffectPrefab, hitPos, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        Destroy(go, go.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
    }
}
