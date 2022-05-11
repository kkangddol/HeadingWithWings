using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private HealthBar healthBar;
    [SerializeField]
    private float healthPoint = 1000;
    public float HealthPoint
    {
        set
        {
            healthPoint = value;
            HealthPointEvent(healthPoint);
        }
        get
        {
            return healthPoint;
        }
    }

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

    private void HealthPointEvent(float healthPoint)
    {
        healthBar.SetHealth(healthPoint);
    }
    
    
}
