using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour{
    // Start is called before the first frame update

    private float speed = 6;
    private float lifetime = 5;
    private int damage = 1;
    private Vector3 direction;

    private void Start() {
        direction = Player.instance.transform.position - transform.position;
        direction.Normalize();
        StartCoroutine(DeathDelay());
    }

    private void Update(){
        transform.position += direction * speed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player") {
            Player.instance.TakeDamage(damage);
            Destroy(gameObject);
        }

        
    }

    IEnumerator DeathDelay(){
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    public void SetDamage(int newDamage){
        this.damage = newDamage;
    }

}
