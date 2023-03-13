using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackSpeedIncrease : MonoBehaviour,ICollectible{

    private float attackSpeedIncrease = 0.08f;
    
    public static event Action<float>  OnAttackSpeedIncreaseCollected;

    public void Collect() {

        OnAttackSpeedIncreaseCollected?.Invoke(attackSpeedIncrease);
        Destroy(gameObject);
    }
}
