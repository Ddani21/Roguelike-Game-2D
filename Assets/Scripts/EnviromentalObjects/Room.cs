using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Room : MonoBehaviour{
    // Start is called before the first frame update
    [SerializeField]private bool closeWhenEntered, roomActive,hasReward;// maybe when pick up is collected sau se deschid cand inamici sunt cleared
    [SerializeField] private GameObject[] doors;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();//alegem lista intrucat e mai usor de corectat
    [SerializeField] private GameObject enemy;
    [SerializeField]private GameObject reward;
    void Start(){
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if ( other.tag == "Player"){
            CameraController.instance.ChangeTarget(transform);

            if (closeWhenEntered)
                CloseRoom();
            if(!roomActive && reward !=null)
                reward.SetActive(true);

                
        }
    }

    private void Update() {
        
        if ( roomActive )
            StartCoroutine(TimeToOpenDoors());
            if (enemies.Count > 0) {
                
                for ( int i = 0; i < enemies.Count; i++){

                    if ( enemies[i] == null){
                        enemies.RemoveAt(i); // o cauti in manual
                        i--;
                    }

                }
            }
            
            if ( enemies.Count == 0  ) {
                
                foreach (GameObject door in doors) {
                    door.SetActive(false);
                    closeWhenEntered = false;

                }
                reward.SetActive(true);
                Destroy(enemy); // distrugem gameObject-ul in sine

            }

    }

    private void CloseRoom() {

        foreach(GameObject door in doors){
            door.SetActive(true);
        }
        roomActive = true;

    }
    //another event when room opens and enemies are dead;
    private void OnTriggerExit2D(Collider2D other) {

        if(other.tag == "Player") {
            roomActive = false;
        }

    }

    public bool GetRoomActive(){
        return roomActive;
    }
    private IEnumerator TimeToOpenDoors() {
        
        yield return new WaitForSeconds(30.5f);
        foreach (GameObject door in doors) {
            door.SetActive(false);
            closeWhenEntered = false;
            reward.SetActive(true);

        }

    }

}
