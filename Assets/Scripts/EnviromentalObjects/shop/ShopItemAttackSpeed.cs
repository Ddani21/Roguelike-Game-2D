using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemAttackSpeed : ShopItem{
    
    [SerializeField]private int itemCostOfChildSpeed = 20;
    private float attackSpeedToBeIncreased = 0.08f;

    protected void Start(){
        itemCost = itemCostOfChildSpeed;
    }

    protected override void ItemPurchase(int itemCost){
        Player.instance.AddAttackSpeed(attackSpeedToBeIncreased);
        base.ItemPurchase(itemCost);
    }
    protected override void HealthTrade(int healthToBeTraded)
    {
        Debug.Log("attackspeed are val"+attackSpeedToBeIncreased);
        Player.instance.AddAttackSpeed(attackSpeedToBeIncreased * 2);
        base.HealthTrade(healthToBeTraded);
    }

}
