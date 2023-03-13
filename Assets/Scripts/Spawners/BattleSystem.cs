using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour{
    // Start is called before the first frame update
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private ColliderTrigger colliderTrigger;
    void Start()
    {
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e){
        StartBattle();
    }
    // Update is called once per frame
    private void StartBattle(){
        Debug.Log("StartBattle");
    }

}
