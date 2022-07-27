using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_IronWall : Bullet
{
    private Animator anim = null;
    private const string ENEMY = "ENEMY";
    private const string ATTACK = "ATTACK";
    private const string IDLE = "IDLE";
    public int collisionCount = 0;
    public IronWallAttack ironWallAttack = null;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag(ENEMY))  return;

        if(!anim.GetCurrentAnimatorStateInfo(0).IsName(ATTACK))
        {
            anim.CrossFade(ATTACK, 1.0f);
        }
        other.GetComponent<EnemyTakeDamage>().TakeDamage(this.transform, damage, knockbackSize);

        if(--collisionCount <= 0)
        {
            Broken();
        }
    }
    public void Broken()
    {
        collisionCount = 0;
        anim.CrossFade(IDLE, 0.0f);
        if(ironWallAttack != null)
        {
            ironWallAttack.WallBroken();
        }
        this.gameObject.SetActive(false);
    }

    public void TakeDamage()
    {
        collisionCount--;
    }
}
