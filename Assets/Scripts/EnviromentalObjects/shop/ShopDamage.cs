using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDamage : ShopItem{
    
    [SerializeField]private int itemCostOfChildDamage = 20;
    [SerializeField]private int damageToIncrease = 1;

    protected void start(){
        itemCost = itemCostOfChildDamage;
    }

    protected override void ItemPurchase(int itemCost){
        Player.instance.AddDamage(damageToIncrease);
        base.ItemPurchase(itemCost);
    }

    protected override void HealthTrade(int healthToBeTraded) {
        Player.instance.AddDamage(damageToIncrease*2);
        base.HealthTrade(healthToBeTraded);
    }

}
