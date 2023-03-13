using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour{
    // Start is called before the first frame update
    [SerializeField]private EnemyBase enemy;

    public void TakeDamage(int damage){
        enemy.TakeDamage(damage);
    }

}
