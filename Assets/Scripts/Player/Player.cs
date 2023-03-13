using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public  class Player : MonoBehaviour {
    public static Player instance;
    [SerializeField]private LayerMask enemies;
    [SerializeField]private float attackRadius = 1.5f;
    private bool playerCanMove = true;
    private bool facingRight = true;
    private bool flipDonebyShoot = false;
    private bool invincible = false;
    private bool isAttacking = false;
    private BoxCollider2D boxCollider;
    [SerializeField]private Rigidbody2D rb;
    private Vector2 movement; // pentru a avea miscarea intr-un singur vector
    private Animator animator;
    private GameObject healthBar;
    private SpriteRenderer spriteRenderer;
    //components needed for refference
    [SerializeField]private GameObject swordSpriteRenderer;
    [SerializeField] private GameObject fireBallPrefab,bombPrefab;
    [SerializeField]private int damage = 2;
    private float fireBallSpeed = 10.0f;
    private float attackSpeed = 0.6f;
    private float range = 1.0f;
    private float moveSpeed = 5f;
    private int currentHealth;
    private int maxHealth = 6;
    public static event Action OnPlayerDeath;
    
    private int bombCount;//sa faci cum sa se transmita si in ScoreManager

    private int bombDamage;
    private float bombTimeToExplode = 3f;
    //timers
    private float lastFireBall,lastFireBall2,attackCounter;

    private void Awake(){
        instance = this;
        
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    protected virtual void Start() {
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        maxHealth = CharacterTracker.instance.GetCharacterTrackerMaxHealth();
        currentHealth = CharacterTracker.instance.GetCharacterTrackerCurrentHealth();
        damage = CharacterTracker.instance.GetCharacterTrackerDamage();

        bombCount = CharacterTracker.instance.GetCharacterTrackerBombCount();

        moveSpeed = CharacterTracker.instance.GetCharacterTrackerMoveSpeed();
        range = CharacterTracker.instance.GetCharacterTrackerRange();
        attackSpeed = CharacterTracker.instance.GetCharacterTrackerAttackSpeed();
        fireBallSpeed = CharacterTracker.instance.GetCharacterTrackerFireBallSpeed();

        lastFireBall = attackSpeed;
        lastFireBall2 = attackSpeed*3;
        healthBar.GetComponent<HealthHeartBar>().SetMaxHealth(maxHealth);
        healthBar.GetComponent<HealthHeartBar>().SetCurrentHealth(currentHealth);
    }

    void Update() {

        if (playerCanMove){
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            
        
            if ( (movement.x != 0) || (movement.y != 0) ){
                animator.SetFloat("speed",1);
            }else{
                animator.SetFloat("speed",0);
            }
            if(!flipDonebyShoot){
                if( movement.x > 0  ) {
                    if (facingRight ==false)
                        Flip();
            
                }else if ( movement.x < 0 ) {
                    if ( facingRight == true)
                        Flip();

                }
            }

            float shootHorizontal = Input.GetAxisRaw("ShootHorizontal");
            float shootVertical = Input.GetAxisRaw("ShootVertical");
            if ( Input.GetButton("Melee")  ) { // verifica constant daca e apasata tasta
                
                MeleeAttack();
            }else if ( (shootHorizontal != 0 || shootVertical != 0 ) && (lastFireBall > attackSpeed) && !isAttacking){
            
                Shoot(shootHorizontal, shootVertical);
                lastFireBall = 0; // stergem  secundele inregistrate
                if (shootHorizontal > 0 ){
                    if ( facingRight == false)
                        Flip();
                    lastFireBall2 = 0;
                }else if ( shootHorizontal < 0){
                    if ( facingRight == true)
                    Flip();
                    lastFireBall2 = 0;
                }
                flipDonebyShoot = true;
            }else { // verifica eventual
                lastFireBall += Time.deltaTime; // inregistram  secundele
                lastFireBall2 += Time.deltaTime;
            }
        
        
            if (lastFireBall2  > attackSpeed*3 ){
                flipDonebyShoot = false;
            }


            if ( Input.GetButtonDown("PlaceBomb") && bombCount > 0){
            
                PlaceBomb(); 

            }
        }
    }
   
    private void FixedUpdate() {
        //Resetam miscarea
        if (playerCanMove){
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
    
    private void Shoot(float x, float y) {
        
        GameObject fireball = Instantiate(fireBallPrefab, transform.position, transform.rotation);
        Fireball_player firestats = fireball.GetComponent<Fireball_player>();
        firestats.SetDamage(damage);
        firestats.SetLifetime(range);
        //isShooting = true;
        fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(
            (x < 0) ? Mathf.Floor(x) * fireBallSpeed : Mathf.Ceil(x) * fireBallSpeed,
            (y < 0) ? Mathf.Floor(y) * fireBallSpeed : Mathf.Ceil(y)* fireBallSpeed
        );


    }

    private void PlaceBomb() {
        GameObject bombUse =Instantiate(bombPrefab,transform.position,Quaternion.identity);
        bombUse.GetComponent<BombUse>().SetStats(4,2,bombTimeToExplode);
        bombCount--;
        ScoreManager.instance.DecreaseBombCount(1);
    }
    private void Flip(){
        
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }

    private IEnumerator FlashAfterDamage() {

        float flashDelay = 0.09f;
        for ( int i = 0; i < 6; i++){
            //spriteRenderer.enabled = false; nu a mers
            //spriteRenderer.color = new Color(1,1,1,0);
            spriteRenderer.color = Color.clear;
            yield return new WaitForSeconds(flashDelay);
            //spriteRenderer.enabled = true;
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashDelay);
        }
        invincible = false;
    }

    void OnTriggerEnter2D(Collider2D collision){
        ICollectible collectible = collision.GetComponent<ICollectible>();
        if ( collectible != null) {
            
            collectible.Collect();
        
        }
    }


    private void Death() {
        
        Destroy(gameObject);
        OnPlayerDeath?.Invoke();
        

    }
    

    public void TakeDamage(int damageTaken) {
        if ( !invincible){
            
            currentHealth -= damageTaken;
            if (currentHealth <= 0) {
                Death();
            }
            invincible = true;
            StartCoroutine(FlashAfterDamage());
        }
        healthBar.GetComponent<HealthHeartBar>().SetCurrentHealth(currentHealth);

        
    }
    private void MeleeAttack(){
        Collider2D[] enemyCollider = Physics2D.OverlapCircleAll(transform.position,attackRadius,enemies);
        foreach(Collider2D enemy in enemyCollider){
            enemy.GetComponent<EnemyBase>().Kill();
        }
        swordSpriteRenderer.SetActive(true);
        animator.SetTrigger("Attack");

        StartCoroutine(CantMove());

    }

    private IEnumerator CantMove() {
        
        invincible = true;
        playerCanMove = false;
        yield return new WaitForSeconds(0.8f);
        invincible = false;
        playerCanMove = true;
        swordSpriteRenderer.SetActive(false);
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position,attackRadius);
    }

    //usual setters
    
    public void SetDamage(int newDamage){
        this.damage = newDamage;
    }
    public void SetIsAttacking() {
        this.isAttacking = !this.isAttacking;
    }
    public void SetFireBallSpeed(float newFireBallSpeed){
        this.fireBallSpeed = newFireBallSpeed;
    }
    public void SetAttackSpeed(float newAttackSpeed){
        this.attackSpeed = newAttackSpeed;
    }
    public void SetRange(float newRange){
        this.range = newRange;
    }
    public void SetMoveSpeed(float newMoveSpeed){
        this.moveSpeed = newMoveSpeed;
    }
    
    public void SetCurrentHealth(int newCurrentHealth){
        this.currentHealth = newCurrentHealth;
        healthBar.GetComponent<HealthHeartBar>().SetCurrentHealth(maxHealth);
    }
    public void SetMaxHealth(int newMaxHealth) {
        this.maxHealth = newMaxHealth;
        healthBar.GetComponent<HealthHeartBar>().SetMaxHealth(maxHealth);

    }
    
    // +setters
    public void AddSpeed(float speedModifier) {
        // ne mai gandim
        this.moveSpeed += speedModifier;
    }

    public void AddDamage(int nwDamage) {
        this.damage += nwDamage;
    }

    public void AddFireBallSpeed( int nwFireBallSpeed) {
        this.fireBallSpeed += nwFireBallSpeed;
    }

    public void AddAttackSpeed( float nwAttackSpeed) {
        if(this.attackSpeed >= 0.25f)
            this.attackSpeed -= nwAttackSpeed;
        
    }

    public void AddRange( float nwRange){
        this.range += nwRange/10;
    }
    
    public void AddBombCount(int nwBombs){
        this.bombCount += nwBombs;
    }
    
    public void  AddHealth(int heal){
        if(currentHealth == maxHealth){
            return;
        }else{
            currentHealth +=heal ;
            healthBar.GetComponent<HealthHeartBar>().SetCurrentHealth(currentHealth);
        }
    }
    public void AddMaxHealth(int bonusHealth){
        maxHealth += bonusHealth;
        healthBar.GetComponent<HealthHeartBar>().SetMaxHealth(maxHealth);
    }

    public void PlayerCanMove(bool move){
        playerCanMove = move;
    }






    //getters
    public int GetHealth(){
        return this.currentHealth;
    }
    public int GetMaxHealth() {
        return this.maxHealth;
    }

    public int GetBombs() {
        return this.bombCount;
    }
    public int GetDamage() {
        return this.damage;
    }
    public float GetMoveSpeed(){
        return this.moveSpeed;
    }
    public float GetRange(){
        return this.range;
    }
    public float GetAttackSpeed(){
        return this.attackSpeed;
    }
    public float GetFireBallSpeed(){
        return this.fireBallSpeed;
    }

    //events
    private void OnEnable() {

        Bomb.OnBombCollected += IncrementBombCount;
        DamageUpgrade.OnDamageUpgradeCollected += IncrementDamage;
        MaxHealthUpgrade.OnMaxHealthUpgradeCollected += IncrementMaxHealth;
        MoveSpeedUpgrade.OnMoveSpeedUpgradeCollected += IncrementMoveSpeed;
        FireBallSpeedUpgrade.OnFireBallSpeedUpgradeCollected += IncrementFireBallSpeed;
        AttackSpeedIncrease.OnAttackSpeedIncreaseCollected += IncrementAttackSpeed;
        RangeUpgrade.OnRangeUpgradeCollected += IncrementRange;
        HealthResto.OnHealthCollected += IncrementHealth;
    }

    private void OnDisable() {

        Bomb.OnBombCollected -= IncrementBombCount;
        DamageUpgrade.OnDamageUpgradeCollected -= IncrementDamage;
        MaxHealthUpgrade.OnMaxHealthUpgradeCollected -= IncrementMaxHealth;
        MoveSpeedUpgrade.OnMoveSpeedUpgradeCollected -= IncrementMoveSpeed;
        FireBallSpeedUpgrade.OnFireBallSpeedUpgradeCollected -= IncrementFireBallSpeed;
        AttackSpeedIncrease.OnAttackSpeedIncreaseCollected -= IncrementAttackSpeed;
        RangeUpgrade.OnRangeUpgradeCollected -= IncrementRange;
        HealthResto.OnHealthCollected -= IncrementHealth;
    }
    

    private void IncrementBombCount(int moreBomb){
        bombCount += moreBomb;
    }

    private void IncrementDamage(int moreDamage) {
        this.damage += moreDamage;
    }

    private void IncrementMaxHealth(int addedHealth){
        this.maxHealth += addedHealth;
        this.currentHealth += addedHealth;
        healthBar.GetComponent<HealthHeartBar>().SetMaxHealth(maxHealth);
        healthBar.GetComponent<HealthHeartBar>().SetCurrentHealth(currentHealth);
    }
    private void IncrementHealth(int ahealth) {
        AddHealth(ahealth);
    }
    private void IncrementMoveSpeed(float moveSpeedIncreased){
        this.moveSpeed += moveSpeedIncreased;
    }

    private void IncrementFireBallSpeed(float fireballSpeedIncreased){
        this.fireBallSpeed += fireballSpeedIncreased;
    }

    private void IncrementAttackSpeed(float attackSpeedIncreased) {
        if(this.attackSpeed >=0.25)
            this.attackSpeed -=attackSpeedIncreased;
    
    }

    private void IncrementRange(float rangeIncreased) {
        
        this.range += rangeIncreased;

    }
}
