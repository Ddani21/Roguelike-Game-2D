using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ShopItem : MonoBehaviour{
    [SerializeField]private GameObject buyMessage;
    // Start is called before the first frame update
    private bool inBuyZone = false;
    protected bool isBomb = false;
    protected int itemCost = 10;
    private void Update() {
        if(inBuyZone){
            if (Input.GetKeyDown(KeyCode.E)){ // to be changed to a key itself
                if(ScoreManager.instance.GetCoins() >= itemCost) {
                    ItemPurchase(itemCost);
                }
            }else if(Input.GetKeyDown(KeyCode.H) && !isBomb) {
                if(Player.instance.GetHealth() > 2){
                    HealthTrade(2);
                    Destroy(gameObject);
                }
            }

        }
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if ( other.tag == "Player"){
            buyMessage.SetActive(true);
            inBuyZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if ( other.tag == "Player"){
            buyMessage.SetActive(false);
            
            inBuyZone = false;
        }
    }
    protected virtual void ItemPurchase(int itemCost) {


        
            
            ScoreManager.instance.DecreaseCoins(itemCost);
            Destroy(gameObject);
                
       
    } 

    protected virtual void HealthTrade(int healthToBeTraded){
        Player.instance.TakeDamage(healthToBeTraded);
    }

}
