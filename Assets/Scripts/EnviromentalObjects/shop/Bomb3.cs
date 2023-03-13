using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb3 : ShopItem{

    [SerializeField]private int itemCostOfChildBomb = 15;
    [SerializeField]private int bombCount = 3;

    protected void Start() {
        itemCost = itemCostOfChildBomb;
        isBomb = true;
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
