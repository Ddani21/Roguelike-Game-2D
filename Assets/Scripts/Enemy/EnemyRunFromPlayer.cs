using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunFromPlayer : EnemyBase{


    [SerializeField]private Rigidbody2D rb;
    private Vector2 moveDirection;
    private int runRange = 8;
    private int ShootRange = 10;
    private int ERFPMoveSpeed = 2;
    private float ERFPfireRate= 0.8f;
    protected override void Start(){
        base.Start();
        SetMoveSpeed(ERFPMoveSpeed);
        SetFireRate(ERFPfireRate);
    }

    protected override void Update(){
        if(target!=null || !isStunned){
            EnemyMovement();
            if(Vector2.Distance(transform.position,target.position) < ShootRange){
                Shoot();
            }
        }
        
    }

    protected override void EnemyMovement(){
        
        if (Vector2.Distance(transform.position,target.position) < runRange) {
            moveDirection = transform.position - target.position;
            moveDirection.Normalize();
            rb.velocity = moveDirection * moveSpeed ;
        }else{
            rb.velocity = Vector3.zero;
        }

        
    }


}
