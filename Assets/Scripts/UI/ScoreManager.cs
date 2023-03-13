using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour{
    // Start is called before the first frame update

    public static ScoreManager instance;
    [SerializeField]private TextMeshProUGUI textCoin,textBomb,textScore;
    [SerializeField]private int coinCount,bombCount,score;
    private void Start(){
        IncrementCoinCount(CharacterTracker.instance.GetCharacterTrackerCoinCount()) ;
        IncrementBombCount(CharacterTracker.instance.GetCharacterTrackerBombCount()) ;
        score = 0;
        IncrementScore(CharacterTracker.instance.GetCharacterTrackerScore());
        
        if ( instance == null){
            instance = this;
        }
 
    }

    
    //getters
    public int GetCoins(){
        return coinCount;
    }
    public int GetScore() {
        return score;
    }
    public void DecreaseCoins(int coins) {
        this.coinCount-=coins;

        textCoin.text = "X"+coinCount.ToString();
    }
    //events
    private void OnEnable() {
        Coin.OnCoinCollected += IncrementCoinCount;
        Bomb.OnBombCollected += IncrementBombCount;
    }

    private void OnDisable() {
        Coin.OnCoinCollected -= IncrementCoinCount;
        Bomb.OnBombCollected -= IncrementBombCount;
    }
    // Update is called once per frame
    public void IncrementCoinCount(int coin){
        coinCount += coin;
        textCoin.text = "X"+coinCount.ToString();
    }
    public void IncrementBombCount( int bomb) {
        bombCount += bomb ;
        textBomb.text = "X"+bombCount.ToString();
    }

    public void DecreaseBombCount( int bomb) {
        bombCount -=bomb;
        textBomb.text = "X"+bombCount.ToString();
    }
    public void DecreaseCoinCount(int coin) {
        coinCount -= coin;
        textCoin.text = "X"+coinCount.ToString();
    }

    public void IncrementScore(int newScore){
        score += newScore;
        textScore.text = "Score:"+score.ToString(); 
    }


    
}
