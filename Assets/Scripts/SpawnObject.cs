using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour{
    // Start is called before the first frame update
    [SerializeField]private GameObject[] objects;

    private void Start() {
        int rand = Random.Range(0,objects.Length);
        Instantiate(objects[rand],transform.position,Quaternion.identity);
    }
    //prima mereu max health upgrade, a 2-a mereu ceva cu bombe, a 3-a random fara max health ugprade??? asa facem?

}
