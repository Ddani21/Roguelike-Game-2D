using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTracker : MonoBehaviour{
    // Start is called before the first frame update
    public static CharacterTracker instance;

    [SerializeField]private int currentHealth = 6,maxHealth = 6,damage = 2,coinCount = 0,bombCount = 3,score = 0;
    [SerializeField]private float moveSpeed = 7,range = 1.0f,attackSpeed = 0.7f,fireBallSpeed = 8.0f;
    private void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SetCharacterStatsInt(int newCurrentHealth,int newMaxHealth,int newDamage, int newCoinCount,int newBombCount){
        this.currentHealth = newCurrentHealth;
        this.maxHealth = newMaxHealth;
        this.damage = newDamage;
        this.coinCount = newCoinCount;
        this.bombCount = newBombCount;
        Debug.Log("S-au inregistrat floats");
    }
    public void SetCharacterStatsFloat(float newMoveSpeed,float newRange,float newAttackSpeed,float newFireBallSpeed){
        this.moveSpeed = newMoveSpeed;
        this.range = newRange;
        this.attackSpeed = newAttackSpeed;
        this.fireBallSpeed = newFireBallSpeed;
        Debug.Log("S-au inregistrat floats");
    }

    public void DestroyCharacter(){
        Destroy(gameObject);
    }
    public void SetCharacterScore(int newScore){
        this.score = newScore;
    }
    public int GetCharacterTrackerScore(){
        return score;
    }
    public int GetCharacterTrackerCurrentHealth(){
        return currentHealth;
    }
    public int GetCharacterTrackerMaxHealth(){
        return maxHealth;
    }
    public int GetCharacterTrackerDamage(){
        return damage;
    }
    public int GetCharacterTrackerCoinCount(){
        return coinCount;
    }
    public int GetCharacterTrackerBombCount(){
        return bombCount;
    }
    public float GetCharacterTrackerMoveSpeed(){
        return moveSpeed;
    }
    public float GetCharacterTrackerRange(){
        return range;
    }
    public float GetCharacterTrackerAttackSpeed(){
        return attackSpeed;
    }
    public float GetCharacterTrackerFireBallSpeed(){
        return fireBallSpeed;
    }

}
