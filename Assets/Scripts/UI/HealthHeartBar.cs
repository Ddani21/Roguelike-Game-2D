using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartBar : MonoBehaviour{

    [SerializeField]private GameObject heartPrefab;

    private int health, maxHealth;
    private GameObject player;
    List<HealthHearts> hearts = new List<HealthHearts>();

    private void Start() {
        // punem hp-ul de la inceput
        // to find the player at runtime
        DrawHearts();
    }

    private void Update() {

        // verificam in real time hp-ul
    }
    private void DrawHearts() {
        //8 -> 4
        ClearHearts();
        //determine how many hearts to make total
        // based of the max health
        int maxHealthRemainder = maxHealth%2;
        int heartsToMake = (maxHealth / 2) + maxHealthRemainder;

        for ( int i = 0; i< heartsToMake; i++) {
            CreateEmptyHeart();
        }

        for ( int i = 0; i <hearts.Count;i++) {
            int heartStatusRemainder =(int) Mathf.Clamp(health - (i*2),0,2);//nr maximuri
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }

    private void CreateEmptyHeart() {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHearts heartComponent = newHeart.GetComponent<HealthHearts>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);

    }
    private void ClearHearts() {

        foreach( Transform t in transform) {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHearts>();
    }

    public void SetMaxHealth(int newMaxHealth){
        this.maxHealth = newMaxHealth;
        DrawHearts();
    }
    public void SetCurrentHealth(int newCurrentHealth){
        this.health = newCurrentHealth;
        DrawHearts();
    }
}
