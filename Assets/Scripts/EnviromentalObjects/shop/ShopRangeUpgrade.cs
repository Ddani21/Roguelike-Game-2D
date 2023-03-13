using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopRangeUpgrade : ShopItem{
    
    [SerializeField]private int itemCostOfChildRange = 20;
    [SerializeField]private int rangeToIncrease=2;

    protected void Start() {
        itemCost = itemCostOfChildRange;
    }

    protected override void ItemPurchase(int itemCost){
        Player.instance.AddRange(rangeToIncrease);
        base.ItemPurchase(itemCost);

    }
    protected override void HealthTrade(int healthToBeTraded){
        Player.instance.AddRange(rangeToIncrease*2);
        base.HealthTrade(healthToBeTraded);
    }

}
