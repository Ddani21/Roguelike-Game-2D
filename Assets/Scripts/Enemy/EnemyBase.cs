using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class EnemyBase : MonoBehaviour {
    
    [Header("Default Settings")]
    [SerializeField] private LayerMask player;
    private float attackRadius = 1;
    private float offset = 1f;
    [SerializeField]protected GameObject stunEffect;
    protected Animator enemyAnimator;
    [SerializeField]protected SpriteRenderer enemySpriteRenderer;
    protected Transform target;
    [SerializeField]protected int currentHealth;
    protected float rangeToChasePlayer=6,moveSpeed=1.5f,attackSpeed,stunDuration=3f,stunHealth;
    private int contactDamage = 1,damage;
    private float meleeAttackSpeed =1f,canAttack =1f;
    
    [SerializeField]protected bool isStunned,wasAlreadyStunned;
    
    [SerializeField]protected GameObject projectile,spawnEffect,effectRefference;
    private GameObject projectileStats;
    private float fireRate;
    private float fireCounter;
    private int scoreValue = 100;
    [SerializeField]private GameObject[] rewards;

    private void Awake(){
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    protected virtual void Start() {

        
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<Animator>();
        isStunned = false;
        wasAlreadyStunned = false;
        
    }

    // Update is called once per frame
    protected virtual void Update() {

        if (!isStunned){
            if(target !=null){
                
                TurnDirection();
                EnemyMovement();
            
                Attack();
            }


        }
    }
    
    protected virtual void EnemyMovement() {
        
        

                if(Vector2.Distance(transform.position, target.position) < rangeToChasePlayer && (Vector2.Distance(transform.position,target.position) > offset*1.7f )){
                    transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed*Time.deltaTime );//Time.deltaTime
                    
                    enemyAnimator.SetBool("isRunning",true);
                }else{
                    transform.position = transform.position;
                    enemyAnimator.SetBool("isRunning",false);
                }

    }



    private void TurnDirection() {
        
        if ( transform.position.x > target.position.x) {
            
            enemySpriteRenderer.flipX = true;
        
        } else {

            enemySpriteRenderer.flipX = false;
        }


    }
    //setters

    protected virtual void Die() {
    
        int rand = Random.Range(0,3);
        if (rand == 1){
            
                int random = Random.Range(0,rewards.Length);
                Instantiate(rewards[random],transform.position,Quaternion.identity);
           
        }
        if ( effectRefference!= null){
            Destroy(effectRefference);
        }
        Destroy(gameObject);
        if (ScoreManager.instance != null){
            ScoreManager.instance.IncrementScore(scoreValue);
        }

    }

    public virtual void Kill() {
        
        if ( isStunned){
            this.currentHealth = 0;
            Die();
            Player.instance.AddHealth(1);
            scoreValue = scoreValue*2;
        }
    }


    protected virtual void Attack(){
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position,attackRadius,player);
        if(playerCollider!=null) {
            if (meleeAttackSpeed <= canAttack ) {
                playerCollider.GetComponent<Player>().TakeDamage(contactDamage);
                canAttack = 0.0f;
                
            }else{
                
                canAttack += Time.deltaTime;
                
            }
        }
    }

    protected virtual void Shoot(){
        fireCounter -=Time.deltaTime;
        if(fireCounter<=0) {
            fireCounter = fireRate;
            GameObject bullet = Instantiate(projectile,transform.position,transform.rotation);
            EnemyProjectile projectileStats = bullet.GetComponent<EnemyProjectile>();
            projectileStats.SetDamage(damage);
        }
    }

    public virtual void TakeDamage(int newDamage){
        this.currentHealth -= newDamage;
        
        StartCoroutine(HitEffect());
        if ( (currentHealth < stunHealth) && (!wasAlreadyStunned) ) {
            StartCoroutine(StunEffect());
            wasAlreadyStunned = true;
            effectRefference = Instantiate(stunEffect,transform.position,Quaternion.identity);

            
        }
        if ( (currentHealth <= 0) ){
            if(wasAlreadyStunned){
                Die();
            }else{
                currentHealth = 1;
                StartCoroutine(StunEffect());
                wasAlreadyStunned = true;
                effectRefference = Instantiate(stunEffect,transform.position,Quaternion.identity);
            }
        }


        
    }

    private IEnumerator StunEffect() {
        isStunned = true;

        enemyAnimator.SetBool("isRunning",false);
        enemyAnimator.SetBool("isStunned",true);

        
        

        yield return new WaitForSeconds(stunDuration);

        enemyAnimator.SetBool("isStunned",false);
        isStunned = false;
        
    }

    private IEnumerator HitEffect() {
        
        enemySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.20f);
        enemySpriteRenderer.color = Color.white;

    }


    //setters

    protected void SetCurrentHealth(int newCurrentHealth){
        this.currentHealth = newCurrentHealth;
    }
    protected void SetMoveSpeed(float newMoveSpeed){
        this.moveSpeed = newMoveSpeed;
    }
    protected void SetDamage( int newDamage){
        this.damage = newDamage;
    }

    protected void SetAttackSpeed( float newAttackSpeed) {
        this.attackSpeed = newAttackSpeed;
    }

    protected void SetStunDuration( float newStunDuration){
        this.stunDuration = newStunDuration;
    }
    protected void SetStunHealth(int newStunHealth){
        this.stunHealth = newStunHealth;
    }
    public void SetScore(int newScore){
        this.scoreValue = newScore;
    }

    public void SetRangeToChasePlayer(int newRange){
        this.rangeToChasePlayer = newRange;
    }
    public void SetStun(){
        StartCoroutine(StunEffect());
        effectRefference = Instantiate(stunEffect,transform.position,Quaternion.identity);
    }
    public void SetFireRate(float newFireRate) {
        this.fireRate = newFireRate;
    }
    public void SetContactDamage(int newContactDamage) {
        this.contactDamage = newContactDamage;
    }
    public void SetRadius(float newAttackRadius){
        this.attackRadius = newAttackRadius;
    }
    public void SetOffset(float newOffset){
        this.offset = newOffset;
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRadius);
    }
    
}
