using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : Destructible{
    // Start is called before the first frame update
    [SerializeField]private GameObject coin;
    public override void Destroy(){
        
        int rand = Random.Range(0,10);
        if (rand == 1){
            Instantiate(coin,transform.position,Quaternion.identity);
        }
        base.Destroy();
    }

}
