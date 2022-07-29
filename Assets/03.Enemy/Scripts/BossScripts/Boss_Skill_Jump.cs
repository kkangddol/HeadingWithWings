using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

[RequireComponent(typeof(Rigidbody2D))]

public class Boss_Skill_Jump : MonoBehaviour, IBoss_Skill
{
    Boss_Skill_Manager skillManager;
    private Rigidbody2D playerRigid;
    public ShakeData WhaleImpactShake;
    TrailRenderer trailRenderer;
    EnemyInfo enemyInfo;

    [SerializeField] AnimationCurve curveY;
    Rigidbody2D rb;
    Vector2 movement;
    Vector2 currentPos;
    Vector2 landingPos;
    float landingDis;
    float speed = 5f;
    float timeElapsed = 0f;
    bool onGround = true;
    bool jump = false;


    public GameObject effect;
    bool isEffect = false;
    IEnemyStopHandler stopHandler;

    Collider2D col;

    public GameObject damageZone = null;
    public AudioClip[] audioClips;

    public void ActivateSkill()
    {
        jump = true;
    }

    IEnumerator ShowDamageZone(Vector3 damagePos)
    {
        damageZone.transform.position = damagePos;
        damageZone.transform.parent = null;
        damageZone.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        damageZone.transform.parent = this.transform;
        damageZone.SetActive(false);
    }

    private void Start() {
        playerRigid = GameObject.FindWithTag("PLAYER").GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        enemyInfo = GetComponent<EnemyInfo>();
        stopHandler = GetComponent<IEnemyStopHandler>();
        trailRenderer = GetComponent<TrailRenderer>();
        col = GetComponent<Collider2D>();
        skillManager = GetComponent<Boss_Skill_Manager>();
    }

    private void FixedUpdate() {
        if(jump)
        {
            JumpHandler();
        }
    }

    void JumpHandler()
    {
        if(onGround)
        {
            stopHandler.StopMove();
            currentPos = rb.position;
            landingPos = playerRigid.position;
            StartCoroutine(ShowDamageZone(landingPos));
            landingDis = Vector2.Distance(landingPos,currentPos);
            timeElapsed = 0f;
            onGround = false;
            trailRenderer.emitting = true;

            col.enabled = false;
            skillManager.audioSource.PlayOneShot(audioClips[0]);
        }
        else
        {
            timeElapsed += Time.fixedDeltaTime * speed / landingDis;
            speed += Time.fixedDeltaTime;

            if(timeElapsed >= 1f && !isEffect)
            {
                GameObject newEffect = Instantiate(effect, new Vector3(currentPos.x, currentPos.y - 1, 0), Quaternion.identity);
                Destroy(newEffect, 1f);
                CameraShakerHandler.Shake(WhaleImpactShake);
                trailRenderer.emitting = false;
                isEffect = true;

                Collider2D[] targets = Physics2D.OverlapCircleAll(currentPos, 4f);
                foreach(var target in targets)
                {
                    if(target.CompareTag("PLAYER") || target.CompareTag("ENEMY"))
                    target.GetComponent<ITakeBossAttack>().TakeBossAttack(transform, skillManager.skillDamage, skillManager.skillKnockBackSize);
                }
            }
            

            if(timeElapsed <= 1.7f)
            {
                currentPos = Vector2.MoveTowards(currentPos, landingPos, Time.fixedDeltaTime * speed);
                rb.MovePosition(new Vector2(currentPos.x, currentPos.y + curveY.Evaluate(timeElapsed)));
            }
            else
            {
                jump = false;
                onGround = true;
                isEffect = false;
                speed = 5f;

                col.enabled = true;

                Boss_Skill_Manager.isSkillEnd = true;
                Boss_Skill_Manager.animator.SetTrigger("reset");

                stopHandler.ResumeMove();
            }
        }
    }


}
