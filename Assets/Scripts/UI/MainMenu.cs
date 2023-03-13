using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour{


    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void Update() {
        if (CharacterTracker.instance != null) {
           CharacterTracker.instance.DestroyCharacter();
        }
        
    }
    public void QuitGame(){
        
        Application.Quit();
    }


}
