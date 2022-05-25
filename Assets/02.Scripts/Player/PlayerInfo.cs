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
    public float damage;
    public float attackDelay;
    public float attackSize;
    public float itemTakeRange;

    public List<GameObject> attackItems;
    public List<GameObject> abilityItems;
    public GameObject wingItem;

    [SerializeField]
    private Transform attackItemsParent;
    [SerializeField]
    private Transform wingItemParent;
    [SerializeField]
    private Transform wingModelParent;

    private void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        attackItems = new List<GameObject>();
        abilityItems = new List<GameObject>();
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
        // GameObject.FindWithTag("GAMEMANAGER").GetComponent<GameManager>().OnGameOver();
        GameManager.Instance.OnGameOver();
    }

    public void TakeAttackItem(GameObject newAttackItem)
    {
        attackItems.Add(newAttackItem);
        GameObject newItem = Instantiate(newAttackItem);
        newItem.transform.SetParent(attackItemsParent);
    }
    public void TakeAbilityItem(GameObject newAbilityItem)
    {
        abilityItems.Add(newAbilityItem);
        foreach(var change in newAbilityItem.GetComponents<AbilityChange>())
        {
            change.ApplyChange();
        }
    }
    public void TakeWingItem(GameObject newWingItemObject, GameObject newWingItemModel)
    {
        Destroy(wingItemParent.GetChild(0));
        Destroy(wingModelParent.GetChild(0));
        
        wingItem = newWingItemObject;

        GameObject newItem = Instantiate(newWingItemObject);
        newItem.transform.SetParent(wingItemParent);
        GameObject newModel = Instantiate(newWingItemModel);
        newModel.transform.SetParent(wingModelParent);
    }
    
}
