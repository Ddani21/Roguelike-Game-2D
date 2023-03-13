using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFireBallSpeed : ShopItem{
    
    [SerializeField]private int itemCostOfChildFireBallSpeed = 20;
    [SerializeField]private int fireBallSpeedToBeIncrease = 1;
    protected void Start() {
        itemCost = itemCostOfChildFireBallSpeed;
    }
    protected override void ItemPurchase(int itemCost){
        Player.instance.AddFireBallSpeed(fireBallSpeedToBeIncrease);
        base.ItemPurchase(itemCost);
    }

    protected override void HealthTrade(int healthToBeTraded){
        Player.instance.AddFireBallSpeed(fireBallSpeedToBeIncrease*2);
        base.HealthTrade(healthToBeTraded);
    }
}
