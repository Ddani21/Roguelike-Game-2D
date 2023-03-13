using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireBallSpeedUpgrade : MonoBehaviour,ICollectible{

    private float fireBallSpeed = 0.5f;    

    public static event Action<float> OnFireBallSpeedUpgradeCollected;

    public void Collect() {

        OnFireBallSpeedUpgradeCollected?.Invoke(fireBallSpeed);
        Destroy(gameObject);

    }

}
