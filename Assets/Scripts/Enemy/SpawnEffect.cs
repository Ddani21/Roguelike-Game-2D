using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour{
    // Start is called before the first frame update
    [SerializeField]private GameObject spawnEffect,enemy;
    
    [SerializeField]private bool start = false,hasntStarted = true;
    [SerializeField]private GameObject room;
    private Room thisRoom;
    private void Start(){
        thisRoom = room.GetComponent<Room>();
    }
    private void Update(){
        if (thisRoom != null){
            thisRoom = room.GetComponent<Room>();
            if (hasntStarted){
                
                start =  thisRoom.GetRoomActive();
                if (start == true ){
                    StartCoroutine(Spawn());
                    Debug.Log("a inceput corutina");
                    hasntStarted = false;
                }
            }
        }
    }

    private IEnumerator Spawn(){
        yield return new WaitForSeconds(1f);
        Instantiate (spawnEffect,transform.position,Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        enemy.SetActive(true);
    }



}   
