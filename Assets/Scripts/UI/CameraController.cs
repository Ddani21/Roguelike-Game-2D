using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{
    // Start is called before the first frame update
    public static CameraController instance;
    [SerializeField]private float moveSpeed = 30;
    
    private Transform target;
    
    [SerializeField]private bool isBossRoom;
    private void Awake() {
        instance = this;
       
    }
    private void Start() {
        if ( target != null) {
            
            transform.position = new Vector3(Player.instance.transform.position.x,Player.instance.transform.position.y,Player.instance.transform.position.z-10);
            
        }
        if(isBossRoom){
            target = Player.instance.transform;
        }
    }

    // Update is called once per frame
    void Update(){
        
        if ( target != null){
            
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(target.position.x,target.position.y,-10),moveSpeed * Time.deltaTime);
        
        }

    }
    public void ChangeTarget(Transform newTarget) {
        this.target = newTarget;
    }

    
}
