using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RangeUpgrade : MonoBehaviour,ICollectible{
    // Start is called before the first frame update

    private float rangeIncrease = 0.2f;

    public static event Action<float> OnRangeUpgradeCollected;

    public void Collect() {

        OnRangeUpgradeCollected?.Invoke(rangeIncrease);
        Destroy(gameObject);
    } 


}
