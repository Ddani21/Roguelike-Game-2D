using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bomb : MonoBehaviour,ICollectible{
    // Start is called before the first frame update
    private int bomb = 1;
    public static event Action<int> OnBombCollected;

    public void Collect() {

        OnBombCollected?.Invoke(bomb);
        Destroy(gameObject);
    }

    // poate un card unde fiecare collectible are niste valori prestabilite
    

}
