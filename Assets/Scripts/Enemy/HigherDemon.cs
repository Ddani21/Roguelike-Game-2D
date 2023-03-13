using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HigherDemon : TinyDemon{
    // Start is called before the first frame update
    private int demonHealth = 16;

    private float demonMoveSpeed = 3f;

    
    private int demonRangeToChasePlayer = 12;
    private int demonStunHealth = 7;
    private int demonScore = 350;
    
    private float demonFireRate = 1.2f;
    private float demonAttackRadius = 0.8f;
    
    private int demonAttack = 2;

    protected override void Start(){
        base.Start();
        SetCurrentHealth(demonHealth);
        SetMoveSpeed(demonMoveSpeed);
        SetStunHealth(demonStunHealth);
        SetScore(demonScore);
        SetStunDuration(demonStunHealth);
        SetRangeToChasePlayer(demonRangeToChasePlayer);
        SetFireRate(demonFireRate);
        SetRadius(demonAttackRadius);
        SetDamage(demonAttack);
    }

}
