using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour{
    // Start is called before the first frame update
    private int  damage = 1;
    private bool isOnCooldown;
    private Animator animator;

    private void Start(){
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other){
        
        if( other != null || isOnCooldown ){
            animator.SetTrigger("activated");
            
            if(other.CompareTag("Player"))  {
                
                other.GetComponent<Player>().TakeDamage(damage);
                
            }

            if(other.CompareTag("Enemy"))  {
                
                other.GetComponent<EnemyBase>().TakeDamage(damage);
                other.GetComponent<EnemyBase>().SetStun();
                
            }
        

        }

    }
    private IEnumerator CoolDown(){
        isOnCooldown = true;
        float cooldownDuration = 0.5f;
        yield return new WaitForSeconds(cooldownDuration);
        isOnCooldown = false;
    }

}
