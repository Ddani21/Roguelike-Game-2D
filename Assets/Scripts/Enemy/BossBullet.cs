using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour{
    // Start is called before the first frame update
    [SerializeField]private float speed = 6f;
    private Vector3 direction;
    [SerializeField]private int damage = 2;
    private void Start(){
        Lifetime();
        direction = transform.right;
    }

    // Update is called once per frame
    private void Update() {
        transform.position += direction * speed * Time.deltaTime;

        if (!BossController.instance.gameObject.activeInHierarchy) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if( other.tag =="Player") {
            Player.instance.TakeDamage(damage);
        }
        Destroy(gameObject);
        //play sound;
    }
    private IEnumerator Lifetime(){
        float lifetime = 5f;
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

}
