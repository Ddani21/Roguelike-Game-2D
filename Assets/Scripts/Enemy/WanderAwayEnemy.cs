using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAwayEnemy : EnemyBase{
    // Start is called before the first frame update
    private float wanderLength = 4,pauseLength = 1.25f,wanderCounter,pauseCounter;


    private Vector2 wanderDirection;

    private int wanderEnemyHealth = 12;
    private float wanderAwayEnemyOffset = 0.5f*2;
    private float wanderAwayEnemyMoveSpeed =  2.5f; 
    private int contactWanderEnemyDamage = 2;
    private float wanderEnemyRadius = 0.8f;
    private int wanderEnemyRangeToChasePlayer = 10;
    protected override void Start(){
        base.Start();
        SetContactDamage(contactWanderEnemyDamage);
        SetMoveSpeed(wanderAwayEnemyMoveSpeed);
        SetOffset(wanderAwayEnemyOffset);
        SetCurrentHealth(wanderEnemyHealth);
        SetRadius(wanderEnemyRadius);
        SetRangeToChasePlayer(wanderEnemyRangeToChasePlayer);
        pauseCounter = Random.Range(pauseLength * 0.75f, pauseLength * 1.25f);
        wanderCounter = Random.Range(wanderLength * 0.75f, wanderLength*1.25f);
        SetStunHealth(wanderEnemyHealth/2);

    }


    protected override void EnemyMovement(){
        if (!isStunned && target!=null){
            if(Vector2.Distance(transform.position, target.position) < rangeToChasePlayer && (Vector2.Distance(transform.position,target.position) > wanderAwayEnemyOffset )){
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                enemyAnimator.SetBool("isRunning",true);
            }else{

                if(wanderCounter >0){
                    
                    wanderCounter -= Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position,wanderDirection,moveSpeed * Time.deltaTime) ;
                    if(wanderCounter <= 0){
                        
                        pauseCounter = Random.Range(pauseLength * 0.75f, pauseLength * 1.25f);
                    }
                    enemyAnimator.SetBool("isRunning",true);
                }

                if ( pauseCounter > 0 ) {

                    pauseCounter -= Time.deltaTime;
                    if ( pauseCounter <= 0 ) {
                        wanderCounter = Random.Range(wanderLength * 0.75f, wanderLength*1.25f);
                           
                        wanderDirection = new Vector2( Random.Range(transform.position.x-2f,transform.position.x+2f),Random.Range(transform.position.y-2f,transform.position.y+2f));
                    }
                    enemyAnimator.SetBool("isRunning",false);
                }

            }
                
        }
    }
}


