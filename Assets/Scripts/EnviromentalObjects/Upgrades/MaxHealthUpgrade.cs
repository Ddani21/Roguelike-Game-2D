using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MaxHealthUpgrade : MonoBehaviour,ICollectible{
    // Start is called before the first frame update

    private int maxHealth = 2;

    public static event Action<int> OnMaxHealthUpgradeCollected;

    public void Collect() {

        OnMaxHealthUpgradeCollected?.Invoke(maxHealth);
        Destroy(gameObject);

    }
    
}
