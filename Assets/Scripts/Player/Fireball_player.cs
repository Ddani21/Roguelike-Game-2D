using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_player : MonoBehaviour {
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    [SerializeField]private GameObject hitEffect;
    [SerializeField]private float lifetime;
    [SerializeField]private int damage;
    Vector3  currentEulerAngles;
    Quaternion currentRotation;
    private void Start() {

        StartCoroutine(DeathDelay());
        rb = GetComponent<Rigidbody2D>() ;
        sprite = GetComponent<SpriteRenderer>();
        if ( rb.velocity.x < 0 ){//stanga       
            Vector2 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }else if (rb.velocity.y < 0){//jos
            currentEulerAngles = new Vector3(0,0,270);
            currentRotation.eulerAngles = currentEulerAngles;
            transform.rotation = currentRotation;
        }else if ( rb.velocity.y > 0){//sus
            currentEulerAngles = new Vector3(0,0,90);
            currentRotation.eulerAngles = currentEulerAngles;
            transform.rotation = currentRotation;
        }
        
    }

    // Update is called once per frame
    private void Update() {
        
        
        
    }


    void OnTriggerEnter2D (Collider2D other) {
        if ( other != null ) {

            if (other.tag == "Enemy") {
                other.GetComponent<EnemyBase>().TakeDamage(damage);
                
            }
            if(other.tag == "Boss"){
                other.GetComponent<BossController>().TakeDamage(damage);
            }
            
            destroyFireball();
            
            
        }
        
    }

    public void SetLifetime( float newLifetime){
        this.lifetime = newLifetime;
   
    }
    public void SetDamage(int newDamage){
        this.damage = newDamage;

    }

    private void destroyFireball(){
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        GameObject effect = Instantiate(hitEffect,transform.position,Quaternion.identity);
        ParticleSystem effectps = effect.GetComponentInParent<ParticleSystem>();
        ParticleSystem.MainModule effectmain = effectps.main;
        //effectmain.startColor = Color.red;
        Destroy(gameObject);
    }
    IEnumerator DeathDelay(){
        yield return new WaitForSeconds(lifetime);
        destroyFireball();
    }
}
