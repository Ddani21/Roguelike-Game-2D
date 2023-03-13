using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour{
    // Start is called before the first frame update    [SerializeField]private GameObject[] objects;
    [SerializeField]private GameObject[] statUpgrades;
    [SerializeField]private GameObject[] dropItems;

    protected virtual void Start() {
        


        int random = Random.Range(0,4);
        if (random == 0){
            int randomUpgrade = Random.Range(0,statUpgrades.Length);
            Instantiate(statUpgrades[randomUpgrade],transform.position,Quaternion.identity);

        }else{
            int randomDrop = Random.Range(0,dropItems.Length);
            Instantiate(dropItems[randomDrop],transform.position,Quaternion.identity);

        }
  
    }
    
    
}
