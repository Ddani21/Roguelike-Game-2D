using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyDemon : EnemyBase{
    // Start is called before the first frame update
    private int tinyDemonHealth = 11;

    private float tinyDemonMoveSpeed = 2.5f;

    
    private int tinyDemonRangeToChasePlayer = 12;
    private int tinyDemonStunHealth = 4;
    private int tinyDemonScore = 150;
    private int rangeToShoot = 7;
    private float tinyDemonFireRate = 1.2f;
    private float tinyDemonAttackRadius = 0.5f;
    private float tinyDemonRadius = 0.5f;
    private int tinyDemonAttack = 1;
    
    

    protected override void Start(){
        base.Start();
        SetCurrentHealth(tinyDemonHealth);
        SetMoveSpeed(tinyDemonMoveSpeed);
        SetStunHealth(tinyDemonStunHealth);
        SetScore(tinyDemonScore);
        SetStunDuration(tinyDemonStunHealth);
        SetRangeToChasePlayer(tinyDemonRangeToChasePlayer);
        SetFireRate(tinyDemonFireRate);
        SetRadius(tinyDemonAttackRadius);
        SetDamage(tinyDemonAttack);
    }
    protected override void Update(){

        base.Update();
        if(target != null){
            if (!isStunned) {
                if ( Vector2.Distance(transform.position,target.position ) < rangeToShoot) {
                    Shoot();
                
                }
            }
        }
    
    }
        protected override void EnemyMovement(){
                if(Vector2.Distance(transform.position, target.position) < rangeToChasePlayer && (Vector2.Distance(transform.position,target.position) > tinyDemonRadius*2-0.1f )){
                    transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed*Time.deltaTime );//Time.deltaTime
                    
                    enemyAnimator.SetBool("isRunning",true);
                }else{
                    transform.position = transform.position;
                    enemyAnimator.SetBool("isRunning",false);
                }
    }



}
