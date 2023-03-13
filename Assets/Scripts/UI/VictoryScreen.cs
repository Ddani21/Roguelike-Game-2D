using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour{


    public void LoadMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit(){

        Application.Quit();

    }
    
}
