using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DetectEnemy : MonoBehaviour
{
    public Transform FindNearestEnemy(string tag)
    {
        // 탐색할 오브젝트 목록을 List 로 저장합니다.
        var objects = GameObject.FindGameObjectsWithTag(tag).ToList();
        // 이 부분 나중에 Enemy 다루는 오브젝트를 넘겨줄 예정

        // LINQ 메소드를 이용해 가장 가까운 적을 찾습니다.
        var neareastObject = objects
        .OrderBy(obj =>
        {
            return Vector3.Distance(transform.position, obj.transform.position);
        })
        .FirstOrDefault();

        if(neareastObject == null)
        {
            return transform;
        }

        return neareastObject.transform;
    }

    public Transform[] FindNearestEnemies(string tag, int enemyCount = 2, float attackRange = 3.0f)
    {
        Transform[] returnTransforms = new Transform[0];
        // 탐색할 오브젝트 목록을 List 로 저장합니다.
        var objects = GameObject.FindGameObjectsWithTag(tag).ToList();
        // 이 부분 나중에 Enemy 다루는 오브젝트를 넘겨줄 예정

        // LINQ 메소드를 이용해 가장 가까운 적을 찾습니다.
        var neareastObjects = objects
        .OrderBy(obj =>
        {
            return Vector3.Distance(transform.position, obj.transform.position);
        });

        if(neareastObjects.Count() != 0)
        {
            foreach (var obj in neareastObjects)
            {
                returnTransforms.Append(obj.transform);
            }
        }
        
        for (int i = 0; i < enemyCount - returnTransforms.Length; i++)
        {
            Transform randTr = new GameObject().GetComponent<Transform>();
            randTr.position = Position.GetRandomInCircle(transform.position, attackRange);
            returnTransforms.Append(randTr);
        }

            return returnTransforms;
    }
}
