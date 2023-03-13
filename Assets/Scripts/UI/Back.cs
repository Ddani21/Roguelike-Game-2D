using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour{
    // Start is called before the first frame update
    [SerializeField]private GameObject mainMenu;
    [SerializeField]private GameObject highScoreTable;
    void Update(){
        if(Input.GetKeyDown(KeyCode.Tab)) {
            mainMenu.SetActive(true);
            highScoreTable.SetActive(false);
        }   
    }
}
