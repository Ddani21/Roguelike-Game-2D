using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chests : MonoBehaviour{

    [SerializeField]private GameObject[] objects;
    [SerializeField] private GameObject buyMessage;
    private bool inRange = false;

    private void Update(){
        if(inRange) {
            if (Input.GetKeyDown(KeyCode.E)){
                int rand = Random.Range(0,objects.Length);
                Instantiate(objects[rand],transform.position,Quaternion.identity);
                Destroy(gameObject);
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if ( other.tag == "Player") {
            buyMessage.SetActive(true);
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            buyMessage.SetActive(false);
            inRange = false;
        }
    }
}
