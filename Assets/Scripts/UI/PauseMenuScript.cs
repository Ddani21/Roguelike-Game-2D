using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuScript : MonoBehaviour{
    public static bool gameIsPaused = false;

    [SerializeField]private GameObject pauseMenuUI;

    private void Update() {
        if( Input.GetKeyDown(KeyCode.Escape)) {

            if ( gameIsPaused){

                Resume();

            }else{
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause(){

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
       
    }

    public void QuitMenu(){

        Application.Quit();

    }

    public void Retry(){

        SceneManager.LoadScene("Level1");
    }
    

}
