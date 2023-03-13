using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Coin : MonoBehaviour, ICollectible {
    // Start is called before the first frame update
    private int coinValue = 1;
    private int scoreValue = 100;

    public static event Action<int> OnCoinCollected;
    public void Collect(){
        
        OnCoinCollected?.Invoke(coinValue);
        if( ScoreManager.instance != null){
            ScoreManager.instance.IncrementScore(scoreValue);
        }

        Destroy(gameObject);

    }

    
}
