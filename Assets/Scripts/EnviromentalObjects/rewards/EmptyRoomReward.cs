using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyRoomReward : Reward{
    // Start is called before the first frame update

    protected override void Start(){
        int randomChanceForReward = Random.Range(0,2);
        if (randomChanceForReward == 0){
            base.Start();
        }
    }

}
