using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBase{
    // Start is called before the first frame update
    private bool aura = false;
    private int slimeHealth = 17;

    private float slimeMoveSpeed = 2.5f;
    [SerializeField]private GameObject auraEffect;
    private float slimeStunDuration = 2, sRadius;

    private int slimeStunHealth = 6,slimeContactDamage = 2;
    private int slimeToChasePlayer = 14;
    private int slimeAuraDamage = 2;
    private int slimeScore = 250;

    
    [SerializeField]private LayerMask players;
    protected override void Start(){
        base.Start();
        sRadius = 1.5f;

        SetCurrentHealth(slimeHealth);
        SetMoveSpeed(slimeMoveSpeed);
        SetStunHealth(slimeStunHealth);
        SetScore(slimeScore);
        SetStunDuration(slimeStunDuration);
        SetContactDamage(slimeContactDamage);
        SetRangeToChasePlayer(slimeToChasePlayer);
        SetStunDuration(slimeStunDuration);
    }

    protected override void Update() {
        base.Update();
        if (aura == false){
            StartCoroutine(Aura());
        }
    }

    private IEnumerator Aura(){
        float auraTimer = 1;
        aura = true;
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position,sRadius,players);
        if ( playerCollider != null)
            playerCollider.GetComponent<Player>().TakeDamage(slimeAuraDamage);
        Instantiate(auraEffect,transform.position,Quaternion.identity);
        yield return new WaitForSeconds(auraTimer);
        aura = false;
    }
    
}
