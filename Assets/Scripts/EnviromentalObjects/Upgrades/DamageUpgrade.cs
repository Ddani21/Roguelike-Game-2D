using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageUpgrade : MonoBehaviour,ICollectible{
    // Start is called before the first frame update

    private int damage = 1;
    public static event Action<int> OnDamageUpgradeCollected;

    public void Collect() {

        OnDamageUpgradeCollected?.Invoke(damage);
        Destroy(gameObject);
    }
}
