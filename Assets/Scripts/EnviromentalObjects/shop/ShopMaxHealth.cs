using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMaxHealth : ShopItem{

    
    [SerializeField]private int itemCostOfChildHealth = 15;
    [SerializeField]private int maxHealthToBeIncrease = 2;
    protected void Start(){
        itemCost = itemCostOfChildHealth;
    }
    protected override void  ItemPurchase(int itemCost){
        Player.instance.AddMaxHealth(maxHealthToBeIncrease);
        base.ItemPurchase(itemCost);
        

    }
    protected override void HealthTrade(int healthToBeTraded){
        Player.instance.AddMaxHealth(maxHealthToBeIncrease*2);
        base.HealthTrade(healthToBeTraded);
    }
    
}
