using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb2 : ShopItem{

    [SerializeField]private int itemCostOfChildBomb =10;
    [SerializeField]private int bombCount = 2;

    protected void Start() {
        itemCost = itemCostOfChildBomb;
    }
    protected override void ItemPurchase(int itemCost){
        Player.instance.AddBombCount(bombCount);
        base.ItemPurchase(itemCost);
        ScoreManager.instance.IncrementBombCount(bombCount);
    }
    protected override void HealthTrade(int healthToBeTraded){
        Player.instance.AddBombCount(bombCount*2);
        base.HealthTrade(healthToBeTraded);
        ScoreManager.instance.IncrementBombCount(bombCount*2);
    }


}
