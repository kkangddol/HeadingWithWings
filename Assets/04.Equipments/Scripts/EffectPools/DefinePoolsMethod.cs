using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefinePoolsMethod : MonoBehaviour { }

#region EffectPoolMethods
// Define EffectPool Methods
public class EffectBullet : Bullet
{
    public Color effectColor = Color.white;

    protected void ReturnEffect(ObjectPoolBase pool, GameObject obj)
    {
        pool.ReturnObject(obj, obj.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
    }

    /// <summary>
    /// 이펙트를 피격위치에 원하는 컬러로 뿌려주는 버전
    /// </summary>
    protected void HitEffect(ObjectPoolBase pool, Vector3 hitPos, Color color)
    {
        GameObject obj = pool.GetObject();
        obj.transform.position = hitPos;
        obj.GetComponent<SpriteRenderer>().color = color;
        ReturnEffect(pool, obj);
    }
    /// <summary>
    /// 이펙트를 피격위치에 뿌려주는 버전
    /// </summary>
    protected void HitEffect(ObjectPoolBase pool, Vector3 hitPos)
    {
        GameObject obj = pool.GetObject();
        obj.transform.position = hitPos;
        ReturnEffect(pool, obj);
    }

    /// <summary>
    /// 이펙트의 Transform을 리턴하는 버전
    /// </summary>
    protected Transform HitEffect(ObjectPoolBase pool)
    {
        GameObject obj = pool.GetObject();
        ReturnEffect(pool, obj);

        return obj.transform;
    }
}

public class ArchonEffectBullet : EffectBullet
{
    /// <summary>
    /// 이펙트를 피격 오브젝트에 부착시켜주는 버전
    /// </summary>
    protected void HitEffect(ObjectPoolBase pool, Transform parent)
    {
        Transform tr = HitEffect(pool);
        tr.parent = parent;
        tr.localPosition = Vector3.zero;
    }
}
#endregion