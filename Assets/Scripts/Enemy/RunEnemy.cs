using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyRunEnemy : EnemyBase{
    // Start is called before the first frame update

    private int tinyRHealth = 5;

    private int tinyRMoveSpeed = 7;
    private float tinyRStunDuration = 4;

    private int tinyRStunHealth =  5;
    private int tinyRScore = 150;

    protected override void Start(){
        base.Start();
        SetCurrentHealth(tinyRHealth);
        SetMoveSpeed(tinyRMoveSpeed);
        SetStunHealth(tinyRStunHealth);
        SetStunDuration(tinyRStunDuration);
        SetScore(tinyRScore);
    }
    


}
