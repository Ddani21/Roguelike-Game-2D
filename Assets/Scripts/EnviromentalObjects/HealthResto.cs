using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HealthResto : MonoBehaviour,ICollectible{
    // Start is called before the first frame update

    [SerializeField]private int healthToBeRestored = 1 ;

    public static event Action<int> OnHealthCollected;
    public void Collect() {

        OnHealthCollected?.Invoke(healthToBeRestored);
        Destroy(gameObject);
    }


}
