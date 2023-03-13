using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour{
    // Start is called before the first frame update
    public static LevelManager instance;
    [SerializeField] private float waitToLoad;
    [SerializeField] private GameObject deathScreenMenu,whileAliveUI,HighscoreTable,fadeScreenObject;
    [SerializeField]private Image fadeScreen;
    
    public HighScoreTable highscoreTable;
    private float fadeSpeed = 0.2f;
    private bool fadeToBlack,fadeOutBlack;
    [SerializeField]private bool isBossLevel;
    private void Awake() {
        instance = this;
    }
    private void Start() {
        fadeOutBlack = true;
        fadeToBlack = false;
    }

    private void Update(){
        if(fadeOutBlack) {
            fadeScreen.color = new Color(fadeScreen.color.r,fadeScreen.color.g,fadeScreen.color.b,Mathf.MoveTowards(fadeScreen.color.a,0f,fadeSpeed*Time.deltaTime)); 
            if ( fadeScreen.color.a == 0f){
                fadeOutBlack = false;
                fadeScreenObject.SetActive(false);
            }
            
        }
        if(fadeToBlack) {
            fadeScreen.color = new Color(fadeScreen.color.r,fadeScreen.color.g,fadeScreen.color.b,Mathf.MoveTowards(fadeScreen.color.a,1f,fadeSpeed*Time.deltaTime)); 
            if ( fadeScreen.color.a == 1f){
                fadeToBlack = false;
                fadeScreenObject.SetActive(true);
            }
        }
        if(Player.instance != null){
            if (isBossLevel && HighscoreTable.activeSelf ){ // scrii in documentatie ca e din manual
                StartCoroutine(ShowTableOnWin());

            }
        }

    }

    public void StartFadeToBlack() {
        fadeToBlack = true;
        fadeOutBlack = false;
    }
    public IEnumerator LevelEnd() {
        Player.instance.PlayerCanMove(false);
        Debug.Log("intra in LevelEnd");
        StartFadeToBlack();
        
        CharacterTracker.instance.SetCharacterStatsInt(Player.instance.GetHealth(),Player.instance.GetMaxHealth(),Player.instance.GetDamage(),ScoreManager.instance.GetCoins(),Player.instance.GetBombs());
        CharacterTracker.instance.SetCharacterStatsFloat(Player.instance.GetMoveSpeed(),Player.instance.GetRange(),Player.instance.GetAttackSpeed(),Player.instance.GetFireBallSpeed());
        CharacterTracker.instance.SetCharacterScore(ScoreManager.instance.GetScore());
        
        yield return new WaitForSeconds(waitToLoad);
        Player.instance.PlayerCanMove(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    private void OnEnable(){
        Player.OnPlayerDeath += ActivateDeathMenu;


    }
    private void OnDisable(){
        Player.OnPlayerDeath -= ActivateDeathMenu;

    }

    private void ActivateDeathMenu(){
        deathScreenMenu.SetActive(true);
        
        whileAliveUI.SetActive(false);
        
        StartCoroutine(ShowHighScoreTable());


    }

    public void ActivateHighScore() {
        StartCoroutine(ShowHighScoreTable());
        Player.instance.PlayerCanMove(false);
    }

    private IEnumerator ShowHighScoreTable(){
        int finalScore = ScoreManager.instance.GetScore();
        yield return new WaitForSeconds(2f);
        HighScoreTableInputWindow.instance.Show(finalScore,(string name) => {
        highscoreTable.AddHighscoreEntry(finalScore,name);
        HighscoreTable.SetActive(true);
        });

    }

    private IEnumerator ShowTableOnWin(){
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }

}
