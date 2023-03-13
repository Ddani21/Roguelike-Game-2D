using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMoveSpeed : ShopItem{
    
    [SerializeField] private int itemCostOfChildMoveSpeed = 20;
    [SerializeField] private int moveSpeedToBeIncreased = 1;

    protected void Start() {
        itemCost = itemCostOfChildMoveSpeed;

    }

    protected override void ItemPurchase(int itemCost){
        Player.instance.AddSpeed(moveSpeedToBeIncreased);
        base.ItemPurchase(itemCost);
    }

    protected override void HealthTrade(int healthToBeTraded){
        Player.instance.AddSpeed(moveSpeedToBeIncreased);
        base.HealthTrade(healthToBeTraded);
    }
}
