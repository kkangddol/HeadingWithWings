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
        get
        {
            return healthPoint;
        }
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
    }

    [HideInInspector]
    public float maxOxygen;
    [HideInInspector]
    public float oxygen;
    public float moveSpeed;
    public float damage;
    public float attackDelay;
    //public float attackSize;
    public float itemTakeRange;
    public float healAmount;
    public float skillDelay;

    public GameObject[] attackEquipments;
    public int[] abilityEquipments;
    public GameObject wingEquipment;
    public GameObject wingModel;
    public int wingNumber = -1;

    public Transform attackEquipmentsParent;
    public Transform wingEquipmentParent;
    public Transform wingModelParent;

    public float headAngle;
    public Vector2 headVector;

    public int startAttack;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        int attackEquipmentsCount = EquipmentManager.Instance.attackEquipmentObjects.Length;
        int abilityEquipmentsCount = EquipmentManager.Instance.abilityEquipmentObjects.Length;

        attackEquipments = new GameObject[attackEquipmentsCount];
        abilityEquipments = new int[abilityEquipmentsCount];
        EquipmentManager.Instance.TakeAttackEquipment(startAttack);
    }

    private void OnMaxHealthPointChange(float maxHealthPoint)
    {
        healthBar.SetMaxHealth(maxHealthPoint);
        if(maxHealthPoint <= healthPoint)
        {
            healthPoint = maxHealthPoint;
        }
    }
    private void OnHealthPointChange(float healthPoint)
    {
        healthBar.SetHealth(healthPoint);
    }
    
    
    private void PlayerDie()
    {
        // GameObject.FindWithTag("GAMEMANAGER").GetComponent<GameManager>().OnGameOver();
        GameManager.Instance.OnGameOver();
    }    
}
