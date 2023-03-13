using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterEnemy : EnemyBase{
    // Start is called before the first frame update
    
    private float moveTimer;
    private float moveRate = 2f;
    private float firecounter ;
    private float firerate =1.75f ;
    private int mageDamage = 4;
    private int mageHealth = 20;
    private int mageHealthStun =6; 
    [SerializeField]private Transform shootPoint,baseTransform;
    private float minX = -5,maxX = 5,minY = 0,maxY = 4;
    //notezi undeva diferenta
    protected override void Start(){
        base.Start();
        SetFireRate(moveRate);
        SetCurrentHealth(mageHealth);
        SetDamage(mageDamage);
        SetStunHealth(mageHealthStun);
        baseTransform = transform;
        
    }
    protected override void Update(){
        if (!isStunned){
            RandomMove();
            if(target !=null){
                Shoot();
            }
        }
    }
    private void RandomMove() {
        moveTimer += Time.deltaTime;
        if(!isStunned){
            if( moveTimer > moveRate ) {
                transform.position = new Vector3(baseTransform.position.x+Random.Range(minX,maxX),baseTransform.position.y+Random.Range(minY,maxY),0);
                moveTimer = 0;
            }
        }

    }
    protected override void Shoot(){
        firecounter -=Time.deltaTime;
        if(firecounter<=0) {
            firecounter = firerate;
            GameObject bullet = Instantiate(projectile,shootPoint.position,transform.rotation);
            EnemyProjectile projectileStats = bullet.GetComponent<EnemyProjectile>();
            projectileStats.SetDamage(mageDamage);
        } 
    }
}
