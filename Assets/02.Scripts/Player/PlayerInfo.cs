using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private HealthBar healthBar;
    [SerializeField]
    private float maxHealthPoint;
    public float MaxHealthPoint
    {
        set
        {
            maxHealthPoint = value;
            OnMaxHealthPointChange(maxHealthPoint);
        }
        get
        {
            return maxHealthPoint;
        }
    }

    [SerializeField]
    private float healthPoint;
    public float HealthPoint
    {
        set
        {
            healthPoint = value;
            if(healthPoint > maxHealthPoint)
            {
                healthPoint = maxHealthPoint;
            }
            OnHealthPointChange(healthPoint);
            if(healthPoint <= 0)
            {
                PlayerDie();
            }
        }
        get
        {
            return healthPoint;
        }
    }



    public float maxOxygen;
    public float oxygen;
    public float moveSpeed;
    public int minDamage = 1;
    public int maxDamage = 3;
    public int damage = 2;
    public float attackDelay;

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    private void OnMaxHealthPointChange(float maxHealthPoint)
    {
        healthBar.SetMaxHealth(maxHealthPoint);
    }
    private void OnHealthPointChange(float healthPoint)
    {
        healthBar.SetHealth(healthPoint);
    }
    
    
    private void PlayerDie()
    {
        GameObject.FindWithTag("GAMEMANAGER").GetComponent<GameManager>().OnGameOver();
    }
    
}
