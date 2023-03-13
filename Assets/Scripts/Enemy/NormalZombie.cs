 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombie : TinyZombie {
    // Start is called before the first frame update

    private int zombieHealth = 15;
    private float zombieMoveSpeed = 2.4f;
    
    private int zombieStunHealth = 4;
    private int zombieScore = 300;
    private int zombieContactDamage = 3;
    private int rangeToChasePlayerZombie = 12;
    private float zombieRadius = 0.7f;
    protected override void Start(){
        base.Start();
        SetCurrentHealth(zombieHealth);
        SetMoveSpeed(zombieMoveSpeed);
        SetStunHealth(zombieStunHealth);
        
        SetScore(zombieScore);
        SetContactDamage(zombieContactDamage);
        SetRangeToChasePlayer(rangeToChasePlayerZombie);
        SetRadius(zombieRadius+0.1f);
        SetOffset(zombieRadius);
    }
    protected override void Update(){
        base.Update();

    }


    


}
