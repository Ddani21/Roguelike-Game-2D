using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyZombie : EnemyBase {
    // Start is called before the first frame update
    [Header("TinyZombie Settings")]
    [SerializeField]private GameObject tinyZDeathEffect;
    private int tinyZMaxHealth = 11;
    private float tinyZMoveSpeed = 3f;
    
    private int tinyZStunHealth = 3;
    private float zRadius = 0.7f;
    private float zAttackRadius = 0.8f;
    private int rangeToChasePlayerTiny = 10;

    protected override void Start() {
        base.Start();
        SetCurrentHealth(tinyZMaxHealth);
        //SetDeathEffect(tinyZDeathEffect);
        SetMoveSpeed(tinyZMoveSpeed);
        SetStunHealth(tinyZStunHealth);
        SetContactDamage(2);
        SetRadius(zAttackRadius);
        SetOffset(zRadius);
        SetRangeToChasePlayer(rangeToChasePlayerTiny);
    }


    

}
