using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
//using UnityEngine.UI;
public class HighScoreTableInputWindow : MonoBehaviour{

    public static HighScoreTableInputWindow instance;

    private Action<string> onNameSubmitted;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI scoreText;
    private TMP_InputField nameInputField;
    private void Awake(){
        instance = this;
        
        nameText = transform.Find("nameText").GetComponent<TextMeshProUGUI>();
        scoreText = transform.Find("scoreText").GetComponent<TextMeshProUGUI>();
        nameInputField = transform.Find("nameText").GetComponent<TMP_InputField>();
        nameInputField.onValidateInput = (string text, int charIndex, char addedChar)=> addedChar.ToString().ToUpper()[0];
        gameObject.SetActive(false);
    }
    private void Update(){
        if (nameText.text.Length >= 6 || (Input.GetKeyDown(KeyCode.KeypadEnter))){
            onNameSubmitted(nameText.text);
            gameObject.SetActive(false);
        }
    }

    public  void Show(int score, Action<string> onNameSubmitted){
        instance.gameObject.SetActive(true);
        instance.scoreText.text = "Score: " + ScoreManager.instance.GetScore();
        instance.onNameSubmitted = onNameSubmitted;
        instance.nameInputField.text = "";
        instance.nameInputField.Select();
        instance.nameInputField.ActivateInputField();
    }
    

}
