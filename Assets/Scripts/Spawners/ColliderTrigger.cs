using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderTrigger : MonoBehaviour{
    // Start is called before the first frame update
    
    public event EventHandler OnPlayerEnterTrigger;
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){

            OnPlayerEnterTrigger?.Invoke(this,EventArgs.Empty);
            
            

        }
    }
}
