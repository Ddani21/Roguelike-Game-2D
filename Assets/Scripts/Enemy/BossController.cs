using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossController : EnemyBase{

    public static BossController instance;
    [SerializeField]private BossAction[] actions;
    private int currentAction;
    private float actionCounter;
    private float shotCounter;
    [SerializeField] private BossHealthBar healthBar;
    [SerializeField] private GameObject healthBarObject;
    private Vector2 moveDirection;
    [SerializeField]private Rigidbody2D rb;
 
    private int bossHealth = 200;

    [SerializeField]private GameObject levelExit;
    [SerializeField]private BossSequence[] sequences;
    [SerializeField]private bool bossCanMove;
    public int currentSequence;
    private int bossScore = 1500;
    
    protected void Awake() {
        instance = this;
    }

    protected override void Start() {
        base.Start();
        actions = sequences[currentSequence].GetActions();
        actionCounter = actions[currentAction].ActionLength();
        currentHealth = bossHealth;
        healthBar.SetMaxHealth(currentHealth);
        wasAlreadyStunned=true;
        SetScore(bossScore);
        StartCoroutine(StartBattle());
    }
    
    protected override void Update() {


        if (Player.instance != null && bossCanMove ){
            if (actionCounter>0){
            
                actionCounter -= Time.deltaTime;
            //se ocupa de miscare
                moveDirection = Vector2.zero;

                if ( actions[currentAction].ShouldMove()) {
                
                    if ( actions[currentAction].ShouldChasePlayer()){
                        moveDirection = Player.instance.transform.position - transform.position;
                        moveDirection.Normalize();
                    }

                    if ( actions[currentAction].MoveToPoints()){
                        moveDirection = actions[currentAction].PointToMoveto().position - transform.position;
                        moveDirection.Normalize();
                    }
                }

                rb.velocity = moveDirection * actions[currentAction].MoveSpeed();
                if( actions[currentAction].MoveSpeed() > 0){
                    enemyAnimator.SetBool("isRunning",true);
                }else{
                    enemyAnimator.SetBool("isRunning",false);
                }
                //se ocupa de impuscat player-ul
                if(actions[currentAction].ShouldShoot()) {
                    shotCounter -= Time.deltaTime;
                    if(shotCounter <= 0){
                    
                        shotCounter = actions[currentAction].TimeBetweenShots();
                    
                        foreach(Transform t in actions[currentAction].ShotPoints()) {
                            Instantiate(actions[currentAction].ItemToShoot(),t.position,t.rotation);
                        }

                    }
                }
                //action summon allies;



            } else {
                currentAction++;
                if ( currentAction >= actions.Length){
                    currentAction = 0;
                }
                actionCounter = actions[currentAction].ActionLength();
            }
        }

        
    }



    public override void TakeDamage(int newDamage){
        base.TakeDamage(newDamage);
        if(!bossCanMove) {
            bossCanMove = true;
        }
        if (currentHealth <= 0){
            gameObject.SetActive(false);
            if (Vector3.Distance(Player.instance.transform.position,levelExit.transform.position) < 2f){
                levelExit.transform.position += new Vector3(4f,0f,0f);
            }
            healthBarObject.SetActive(false);
            LevelManager.instance.ActivateHighScore();
            
        }
        else{
            if(currentHealth <= sequences[currentSequence].GetEndSeQuenceHealth() && currentSequence<sequences.Length - 1){
                currentSequence++;
                actions = sequences[currentSequence].GetActions();
                currentAction = 0;
                actionCounter = actions[currentAction].ActionLength();
            }
        }
        healthBar.SetHealth(currentHealth);
    }


    //getters

    public float GetBossHealth() {
        
        return currentHealth;
    }

    private IEnumerator StartBattle(){
        yield return new WaitForSeconds(4f);
        bossCanMove = true;
    }
    
}

[System.Serializable]
public class BossAction {

    [Header("Action")]
    [SerializeField]private float actionLength;
    
    [SerializeField]private bool shouldMove;
    [SerializeField]private bool shouldChasePlayer;
    [SerializeField]private bool moveToPoints;
    [SerializeField]private float moveSpeed;
    [SerializeField]private Transform pointToMoveTo;
    [SerializeField]private bool shouldShoot;
    [SerializeField]private GameObject itemToShoot;
    [SerializeField]private float timeBetweenShots;
    [SerializeField]private Transform[] shotPoints;

    public float ActionLength(){
        return actionLength;
    }
    public float MoveSpeed(){
        return moveSpeed;
    }
    public bool ShouldMove(){
        return shouldMove;
    }
    public bool ShouldChasePlayer(){
        return shouldChasePlayer;
    }

    public Transform PointToMoveto(){
        return pointToMoveTo;
    }
    public bool MoveToPoints(){
        return moveToPoints;
    }
    public bool ShouldShoot(){
        return shouldShoot;
    }
    public float TimeBetweenShots(){
        return timeBetweenShots;
    }
    public Transform[] ShotPoints(){
        return shotPoints;
    }
    public GameObject ItemToShoot(){
        return itemToShoot;
    }

}

[System.Serializable]
public class BossSequence{
    [Header("Sequence")]
    [SerializeField]private BossAction[] actions;

    [SerializeField]private int endSequenceHealth;
    
    public BossAction[] GetActions(){
        return this.actions;
    }
    
    public int GetEndSeQuenceHealth(){
        return endSequenceHealth;
    }
}