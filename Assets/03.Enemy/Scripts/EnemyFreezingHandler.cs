using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFreezingHandler : MonoBehaviour
{

    private EnemyInfo enemyInfo;
    private SpriteRenderer enemySprite;
    private float enemySpeed = 0.0f;
    private Color freezeColor = new Color(0f, 1f, 1f, 0.95f);

    // Start is called before the first frame update
    void Start()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        enemySprite = GetComponentInChildren<SpriteRenderer>();
        enemySpeed = enemyInfo.moveSpeed;
    }

    public void SlowMove(float speedMultiplier, float duration = 1.5f)
    {
        StartCoroutine(EnemySetSlow(speedMultiplier, duration));
    }

    IEnumerator EnemySetSlow(float speedMultiplier, float duration)
    {
        // 느려지는 동안 색 변경도 이 안에 추가
        enemySprite.color = freezeColor;
        enemyInfo.moveSpeed = enemySpeed * speedMultiplier;
        yield return new WaitForSeconds(duration);
        enemyInfo.moveSpeed = enemySpeed;
        enemySprite.color = Color.white;
    }
}
