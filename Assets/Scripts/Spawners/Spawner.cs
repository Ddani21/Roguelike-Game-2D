using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField]private GameObject enemy;
    [SerializeField]private GameObject[] spawnSpots;
    private GameObject currentPoint;
    private float timer;
    private int Roomtype;
    [SerializeField]private float maxTime;
    private int numberOfEnemies,index;
    private void Start() {
        /*
        int randPos = Random.Range(0,spawnSpots.Length);
        currentPoint = spawnSpots[randPos];
        Instantiate(enemy,currentPoint.transform.position,Quaternion.identity);//no rotation Quaternion.identity
        //Instantiate(enemies[Random.Range(0,enemies.Length)], currentPoint.transform.position,Quaternion.identity);
        */
    }

    // Update is called once per frame
    private void Update() {
        //spawn one by one
        if ( timer <=0) {
            int randPos = Random.Range(0,spawnSpots.Length);
            currentPoint = spawnSpots[randPos];
            Instantiate(enemy,currentPoint.transform.position,Quaternion.identity);//no rotation Quaternion.identity
            //Instantiate(enemies[Random.Range(0,enemies.Length)], currentPoint.transform.position,Quaternion.identity);
            timer = maxTime;
        }else{
            timer -= Time.deltaTime;
        }
    }
    private void setTimer(float newTimer,float newMaxTime) {
        this.maxTime = newMaxTime;
        this.timer = newTimer;
    }
    private void setRoomtype(int newRoomtype){
        this.Roomtype = newRoomtype;
    }
    
}
