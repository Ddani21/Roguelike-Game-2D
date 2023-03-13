using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveSpeedUpgrade : MonoBehaviour,ICollectible{

    private float moveSpeedIncrease = 1f;

    public static event Action<float> OnMoveSpeedUpgradeCollected;

    public void Collect() {

        OnMoveSpeedUpgradeCollected?.Invoke(moveSpeedIncrease);
        Destroy(gameObject);
        
    }

}
