using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour{
    // Start is called before the first frame update
    public virtual void Destroy(){
        Destroy(gameObject);
    }

}
