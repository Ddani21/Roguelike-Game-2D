using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombUse : MonoBehaviour{
    // Start is called before the first frame update
    
    [SerializeField]private float secondsToExplode,radius;
    [Header("To keep in Inspector")]
    //[SerializeField]private GameObject explosion;
    [SerializeField]private LayerMask enemies,player,destructible;
    [SerializeField]private int damage = 4;
    [SerializeField]private Transform center;
    [SerializeField]private GameObject explosion;
    void Start(){
        radius = 3;
        StartCoroutine(BombExplode());
    }

    private IEnumerator BombExplode(){
        
        yield return new WaitForSeconds(secondsToExplode);
        Collider2D[] enemyCollider = Physics2D.OverlapCircleAll(transform.position,radius,enemies);
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position,radius,player);
        Collider2D[] destructibleCollider = Physics2D.OverlapCircleAll(transform.position,radius,destructible); 
        for ( int i = 0 ; i < enemyCollider.Length; i++){
            if(enemyCollider[i]!=null)
                enemyCollider[i].GetComponent<EnemyBase>().TakeDamage(damage);
        }
        if ( playerCollider != null)
            playerCollider.GetComponent<Player>().TakeDamage(damage/2);
        for ( int i = 0 ; i < destructibleCollider.Length; i++){
            destructibleCollider[i].GetComponent<Destructible>().Destroy();
        }
        
        Instantiate(explosion,transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void SetStats(int newDamage,float newRadius,float newSecondsToExplode) {
        this.damage = newDamage;
        this.radius = newRadius;
        this.secondsToExplode = newSecondsToExplode;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,radius);
    }

    
}
